using Baritone.Utils;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Baritone.Wrappers;
using System.Threading;
using Microsoft.Xna.Framework;
using System.Threading.Tasks;
using Baritone.States.Mario;
using Baritone.States.Powerups;
using Baritone.Input;

namespace Baritone.Sprites
{
    //The master class for our mario avatar
    public class SpriteMario : SpriteCollection, ITrackable, ICollidable
    {

        public StateMachineMarioAction StateMachineAction
        {
            get; private set;
        }

        public StateMachineMarioPowerup StateMachinePowerup
        {
            get; private set;
        }

        public bool won = false;

        public int coins = 0;
        public int nextLife = 1;
        public int lives = 3;
        public long starTime = 0;
        public int previousStarState = 0;

        public int points = 0;
        public bool endgame = false;

        public SpriteMario(Sprint4 game) : base(game)
        {
            this.Info.frameDelay = 7;

            this.RegisterStates();

            this.SetSheetState(SpriteStates.Sheets.NORMAL);

            this.StateMachineAction = new StateMachineMarioAction(this);
            this.StateMachinePowerup = new StateMachineMarioPowerup(this);

            this.Info.bounds = game.bounds;
            this.SetPosition(5, this.GetStartHeight()); //2 floor tiles, 2 scale
        }

        public int GetStartHeight()
        {
            return this.game.GetViewport().Bottom - (16 * 2) * 2 - this.Info.spriteHeight - 1;
        }

        private void RegisterStates()
        {
            this.RegisterState(SpriteStates.Sheets.NORMAL, SpriteStates.Sprites.IDLE, "Sprites/Mario/Normal/Small/idle");
            this.RegisterState(SpriteStates.Sheets.NORMAL, SpriteStates.Sprites.WALKING, "Sprites/Mario/Normal/Small/walking");
            this.RegisterState(SpriteStates.Sheets.NORMAL, SpriteStates.Sprites.JUMPING, "Sprites/Mario/Normal/Small/jumping");
            this.RegisterState(SpriteStates.Sheets.NORMAL, SpriteStates.Sprites.DEAD, "Sprites/Mario/Normal/Small/dead");

            this.RegisterState(SpriteStates.Sheets.SUPER, SpriteStates.Sprites.IDLE, "Sprites/Mario/Normal/Large/idle");
            this.RegisterState(SpriteStates.Sheets.SUPER, SpriteStates.Sprites.WALKING, "Sprites/Mario/Normal/Large/walking");
            this.RegisterState(SpriteStates.Sheets.SUPER, SpriteStates.Sprites.JUMPING, "Sprites/Mario/Normal/Large/jumping");
            this.RegisterState(SpriteStates.Sheets.SUPER, SpriteStates.Sprites.CROUCHING, "Sprites/Mario/Normal/Large/crouching");

            this.RegisterState(SpriteStates.Sheets.FIRE, SpriteStates.Sprites.IDLE, "Sprites/Mario/Fire/Large/idle");
            this.RegisterState(SpriteStates.Sheets.FIRE, SpriteStates.Sprites.WALKING, "Sprites/Mario/Fire/Large/walking");
            this.RegisterState(SpriteStates.Sheets.FIRE, SpriteStates.Sprites.JUMPING, "Sprites/Mario/Fire/Large/jumping");
            this.RegisterState(SpriteStates.Sheets.FIRE, SpriteStates.Sprites.CROUCHING, "Sprites/Mario/Fire/Large/crouching");

            this.RegisterState(SpriteStates.Sheets.STAR_NORMAL, SpriteStates.Sprites.IDLE, "Sprites/Mario/Star/Small/idle");
            this.RegisterState(SpriteStates.Sheets.STAR_NORMAL, SpriteStates.Sprites.WALKING, "Sprites/Mario/Star/Small/walking");
            this.RegisterState(SpriteStates.Sheets.STAR_NORMAL, SpriteStates.Sprites.JUMPING, "Sprites/Mario/Star/Small/jumping");
            this.RegisterState(SpriteStates.Sheets.STAR_NORMAL, SpriteStates.Sprites.DEAD, "Sprites/Mario/Star/Small/dead");

            this.RegisterState(SpriteStates.Sheets.STAR_SUPER, SpriteStates.Sprites.IDLE, "Sprites/Mario/Star/Large/idle");
            this.RegisterState(SpriteStates.Sheets.STAR_SUPER, SpriteStates.Sprites.WALKING, "Sprites/Mario/Star/Large/walking");
            this.RegisterState(SpriteStates.Sheets.STAR_SUPER, SpriteStates.Sprites.JUMPING, "Sprites/Mario/Star/Large/jumping");
            this.RegisterState(SpriteStates.Sheets.STAR_SUPER, SpriteStates.Sprites.CROUCHING, "Sprites/Mario/Star/Large/crouching");
        }

        public override bool isStatic()
        {
            return false;
        }

        public new void SetSheetState(int sheetState)
        {

            int currentHeight = this.Info.spriteHeight;

            base.SetSheetState(sheetState);

            int newHeight = this.Info.spriteHeight;

            Console.WriteLine("New height: {0}, Old height: {1}, Subtracting {2}", newHeight, currentHeight, (newHeight - currentHeight));
            this.Info.position.Y -= (newHeight - currentHeight);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            this.StateMachinePowerup.CurrentState.Update(gameTime);

            int vWidth = this.game.GetViewport().Right - this.game.GetViewport().Left;
            double percentH = ((double) this.Info.x / vWidth) * 100D;

            double dx = percentH - 50;

            if (Math.Abs(dx) > 5)
            {
                this.game.CameraX += (Math.Max(2, (int)Math.Abs(this.Info.velocity.X)) * Math.Sign(dx));
                if (this.game.CameraX < 0)
                {
                    this.game.CameraX = 0;
                }
            }
            

            /*int vHeight = this.game.GetViewport().Bottom;
            double percentV = ((double)this.Info.y / vHeight) * 100D;

            double dy = percentV - 50;
    
            if (Math.Abs(dy) > 5)
            {
                this.game.CameraY += (Math.Max(2, (int)Math.Abs(this.Info.velocity.Y)) * Math.Sign(dy));
                if (this.game.CameraY > 0)
                {
                    this.game.CameraY = 0;
                }
            }//*/
        }

        private long lastThrown = Environment.TickCount;
        public void ThrowFireball()
        {
            long now = Environment.TickCount;
            if ((now - lastThrown) > 250)
            {
                lastThrown = now;
                SpriteCollection fireball = new SpriteFireball(this.game);
                fireball.Info.bounds = this.game.bounds;
                fireball.Info.position = this.Info.position;
                fireball.Info.position.X += (8 * (this.Info.spriteEffects == SpriteEffects.None ? 1 : -1));
                fireball.Info.velocity.X = 4 * (this.Info.spriteEffects == SpriteEffects.None ? 1 : -1);
                fireball.SetSheetState(SpriteStates.Sheets.NORMAL);
                fireball.SetSpriteState(SpriteStates.Sprites.IDLE);
                SpriteLayer.CollisionLayer.AddSprite(fireball);
            }
        }

        public void OnMove(int x, int y)
        {
            lock (this)
            {
                //We fell off, die
                if (y + this.Info.spriteHeight >= this.game.bounds.Bottom)
                {
                    this.StateMachineAction.CurrentState.ToDead();
                }

                if (this.Info.position.X + this.Info.spriteWidth >= game.flagposition)
                {

                    if (!this.won)
                    {
                        this.StateMachineAction.CurrentState.ToIdle();

                        foreach (InputController ic in SpriteLayer.CollisionLayer.InputControllers)
                        {
                            ic.Paused = true;
                        }

                        if (this.Info.position.Y < game.flag.Info.position.Y)
                        {
                            this.Info.position.Y = MathHelper.Clamp(this.Info.position.Y, game.flag.Info.position.Y, game.GetViewport().Bottom);
                        }
                        else
                        {
                            game.flag.Info.position.Y = MathHelper.Clamp(game.flag.Info.position.Y, this.Info.position.Y, game.GetViewport().Bottom);
                        }

                        double points = (double)game.GetViewport().Height - 96.0 - (double)game.flag.Info.position.Y;
                        double percent = points / 160.0;
                        if (percent == 1)
                        {
                            this.lives++;
                        }
                        points = percent * 5000;

                        Task.Factory.StartNew(delegate() {
                            while (points > 10)
                            {
                                this.points += 10;
                                points -= 10;
                                Thread.Sleep(5);
                            }
                            while (points > 0)
                            {
                                this.points += 1;
                                points -= 1;
                                Thread.Sleep(5);
                            }
                        });

                        Task.Factory.StartNew(delegate ()
                        {
                            while (game.TimeLeft > 0)
                            {
                                this.points += 1;
                                game.TimeLeft -= 1;
                                Thread.Sleep(5);
                            }
                        });

                        this.won = true;
                        Sprint4.Win();
                    }

                    this.Info.velocity.Y = MathHelper.Clamp(this.Info.velocity.Y, 0, 1);
                    this.game.flag.Info.position.Y = this.Info.position.Y;
                }
            }
        }
        
        public void OnCollision(Direction dir, SpriteCollection other, Rectangle intersection)
        {
            this.Info.collisionColor = Color.Red;
        }

        //Used when jumping on an enemy.
        public void Bounce()
        {
            this.Info.velocity.Y = -3;
        }

    }
}
