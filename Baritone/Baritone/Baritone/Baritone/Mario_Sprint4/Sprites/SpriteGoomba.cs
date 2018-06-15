using Baritone.Utils;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Baritone.Wrappers;
using System.Threading;
using Microsoft.Xna.Framework;
using Baritone.States.Powerups;
using Microsoft.Xna.Framework.Audio;
using System.Threading.Tasks;

namespace Baritone.Sprites
{
    public class SpriteGoomba : SpriteCollection, ICollidable
    {

        long dirChangeTimer = 0;

        public SpriteGoomba(Sprint4 game) : base(game)
        {
            this.Info.frameDelay = 8;
            this.Info.harmfulIndex = 1;

            this.RegisterState(SpriteStates.Sheets.NORMAL, SpriteStates.Sprites.IDLE, "Sprites/Enemies/Goomba/idle");
            this.RegisterState(SpriteStates.Sheets.NORMAL, SpriteStates.Sprites.DEAD, "Sprites/Enemies/Goomba/dead");

            this.SetSheetState(SpriteStates.Sheets.NORMAL);
            this.SetSpriteState(SpriteStates.Sprites.IDLE);

            this.Info.bounce = true;
        }

        public override bool isStatic()
        {
            return false;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (this.Info.y + this.Info.spriteHeight >= (this.game.GetViewport().Bottom - 3))
            {
                Task.Factory.StartNew(delegate ()
                {
                    SpriteLayer.CollisionLayer.RemoveSprite(this);
                });
            }
        }

        //Gets killed by fireball or shell
        public void Die()
        {
            SoundFactory.PlaySoundEffect(SoundFactory.Kick());

            this.Info.spriteEffects = SpriteEffects.FlipVertically;
            this.Info.bounce = false;
            this.Info.manual = true;
            Thread t = new Thread(new ThreadStart(delegate ()
            {
                this.Info.velocity.Y = -30;
                Thread.Sleep(200);
                this.Info.velocity.Y = -10;
                Thread.Sleep(50);
                this.Info.velocity.Y = 10;
                Thread.Sleep(50);
                this.Info.velocity.Y = 30;
                Thread.Sleep(200);
                this.Info.velocity.Y = 50;
                Thread.Sleep(1000);
                SpriteLayer.CollisionLayer.RemoveSprite(this); 
            }));
            t.IsBackground = true;
            t.Start();
        }

        //Jumped on by Mario.
        public void GetStomped()
        {
            SoundFactory.PlaySoundEffect(SoundFactory.Kick());

            Console.WriteLine("Killing Goomba");
            this.SetSheetState(SpriteStates.Sheets.NORMAL);
            this.SetSpriteState(SpriteStates.Sprites.DEAD);
            this.Info.velocity.X = 0;
            Thread t = new Thread(new ThreadStart(delegate ()
            {
                Thread.Sleep(1000);
                SpriteLayer.CollisionLayer.RemoveSprite(this);
            }));
            t.IsBackground = true;
            t.Start();
        }

        public void OnCollision(Direction direction, SpriteCollection other, Rectangle intersection)
        {
            if (this.SpriteState != SpriteStates.Sprites.DEAD)
            {
                if (other is SpriteMario)
                {
                    if (other.SpriteState != SpriteStates.Sprites.DEAD) {
                        if (direction == Direction.TOP)
                        {
                            Console.WriteLine("Goomba collided with Mario. Mario on TOP");
                            SpriteMario mario = (SpriteMario)other;
                            if (mario.SpriteState != SpriteStates.Sprites.DEAD)
                            {
                                Console.WriteLine("Killing Goomba!");
                                this.GetStomped();
                                mario.Bounce();
                                mario.points += 100;
                                this.game.pointGenerator.Add(100, this);
                            }
                            else
                            {
                                Console.WriteLine("[Err] Goomba collision on TOP by Mario, but Mario is dead.");
                            }
                        }
                        else
                        {
                            SpriteMario mario = (SpriteMario) other;

                            if (mario.StateMachinePowerup.CurrentState == mario.StateMachinePowerup.Star)
                            {
                                this.Die();
                            }
                            else
                            {
                                mario.StateMachinePowerup.CurrentState.TakeDamage();
                            }


                            //Reason for this is we get a bounce effect.
                            //If we don't have a timer, the collision will happen repeatedly
                            //and goomba will "stick" to mario
                            if (dirChangeTimer < DateTime.Now.Ticks)
                            {
                                //if mario is not dead and they're both not traveling same direction. Goomba should
                                //change directions upon collision.
                                if (this.Info.velocity.X * mario.Info.velocity.X <= 0)
                                    this.Info.velocity.X *= -1;
                                dirChangeTimer = DateTime.Now.Ticks + 15000000;
                            }

                        }
                    }
                }
                
                else if (other is SpriteBlock && (other as SpriteBlock).Bumping)
                {
                    if (direction == Direction.BOTTOM)
                    {
                        this.Die();
                    }
                }
            }
            else
            {
                Console.WriteLine("Goomba is already dead!");
            }
        }

        public void move()
        {
            if (this.Info.position.X > this.game.Mario.Info.position.X)
            {
                this.Info.velocity.X = -1;
            } else
            {
                this.Info.velocity.X = 1;
            }
        }

    }
}
