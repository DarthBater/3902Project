using Baritone.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Baritone.Wrappers;
using System.Threading;
using Baritone.States.Powerups;
using Microsoft.Xna.Framework.Audio;

namespace Baritone.Sprites
{
    public class SpriteKoopa : SpriteCollection, ICollidable
    {
        bool dead = false;
        public SpriteKoopa(Sprint3 game, string color) : base(game)
        {
            this.Info.frameDelay = 10;
            this.Info.bounce = true;
            this.Info.harmfulIndex = 1;

            this.RegisterState(SpriteStates.Sheets.NORMAL, SpriteStates.Sprites.WALKING, "Sprites/Enemies/Koopa/" + color + "/moving");
            this.RegisterState(SpriteStates.Sheets.SHELL, SpriteStates.Sprites.SHELLED, "Sprites/Enemies/Koopa/" + color + "/shell");
            this.RegisterState(SpriteStates.Sheets.NORMAL, SpriteStates.Sprites.JUMPING, "Sprites/Enemies/Koopa/" + color + "/flying");

            this.SetSheetState(SpriteStates.Sheets.NORMAL);
            this.SetSpriteState(SpriteStates.Sprites.WALKING);
        }

        public override bool isStatic()
        {
            return false;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (this.Info.velocity.X > 0)
            {
                this.Info.spriteEffects = SpriteEffects.FlipHorizontally;
            }
            else
            {
                this.Info.spriteEffects = SpriteEffects.None;
            }
        }

        //used for red koopa.
        public void SetBounds(int x, int y, int width, int height)
        {
            Rectangle newBounds = new Rectangle(x, y, width, height);
            this.Info.bounds = newBounds;
        }

        public void Move()
        {
            if(this.SpriteState == SpriteStates.Sprites.SHELLED)
            {
                if (this.Info.velocity.X != 0)
                {
                    this.Info.velocity.X = 0;
                    this.Info.velocity.Y = 0;
                    this.Info.numFrames = 1;
                }
                else
                {
                    this.Info.velocity.X = 3;
                    this.Info.velocity.Y = 0;
                    this.Info.numFrames = 1;
                }
            }
            else if (this.SpriteState != SpriteStates.Sprites.WALKING)
            {
                this.Info.velocity.X = -1;
                this.Info.bounce = true;
            }
        }


        public void LandOnKoopa()
        {
            SoundFactory.PlaySoundEffect(SoundFactory.Kick());

            if (this.SpriteState == SpriteStates.Sprites.JUMPING)
            {
                this.SetSheetState(SpriteStates.Sheets.NORMAL);
                this.SetSpriteState(SpriteStates.Sprites.WALKING);
                this.Move();
            }
            else if (this.SpriteState == SpriteStates.Sprites.WALKING)
            {
                int walkingSpriteHeight = this.Info.spriteHeight;
                this.SetSpriteState(SpriteStates.Sprites.SHELLED);
                this.SetSheetState(SpriteStates.Sheets.SHELL);
                this.Info.velocity.X = 0;
            }
            else
            {
                if (this.Info.velocity.X < 3)
                {
                    this.Info.velocity.X = 3;
                }
                else
                {
                    this.Info.velocity.X = 0;
                }
            }
        }

        public void Die()
        {
            SoundFactory.PlaySoundEffect(SoundFactory.Kick());

            Thread t = new Thread(new ThreadStart(delegate ()
            {
                this.Info.velocity.Y = -6;
                Thread.Sleep(1000);
                SpriteLayer.CollisionLayer.RemoveSprite(this);

            }));
            t.IsBackground = true;
            t.Start();
        }

        public void OnCollision(Direction direction, SpriteCollection other, Rectangle intersection)
        {

            if (direction == Direction.TOP && other is SpriteMario)
            {
                SpriteMario mario = (SpriteMario) other;
                mario.Bounce();
                mario.points += 100;
                this.game.pointGenerator.Add(200, this);
                this.LandOnKoopa();


            }
            else if (other is SpriteMario)
            {
                SpriteMario mario = (SpriteMario)other;

                if (mario.StateMachinePowerup.CurrentState is StatePowerupStar)
                {
                    if (!dead)
                    {
                        dead = true;
                        this.Die();
                    }
                }
                else
                {
                    mario.StateMachinePowerup.CurrentState.TakeDamage();
                }
            }
        }

        public void move()
        {
            if (this.Info.position.X > game.Mario.Info.position.X)
            {
                this.Info.velocity.X = -1;
            } else
            {
                this.Info.velocity.X = 1;
            }

            if(this.SpriteState == SpriteStates.Sprites.SHELLED)
            {
                this.Info.velocity.X = 0;
            }

        }
    }
}