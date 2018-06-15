using Baritone.MappyBird_Sprint5.Wrappers.Game;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Baritone.Bird_Sprint5;
using Baritone.MappyBird_Sprint5.Wrappers.Game.Levels.One;

namespace Baritone.MappyBird_Sprint5.Wrappers.Sprites
{
    public class SpriteMappy : Sprite, ICollidable, IRestorable
    {

        public override bool IsStatic { get { return false; } }

        public event EventHandler OnMove;

        public SpriteMappy(Layer layer) : base(layer, 1, 1)
        {
            this.SetDefault("Sprites/Mario/mappy");
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            this.Velocity = new Vector2(this.Velocity.X, MathHelper.Clamp(this.Velocity.Y, -10, 10));
            float central = (float) (Math.PI / 4);
            central += (this.Velocity.Y / 15f) * (float) (Math.PI / 2);
            central = MathHelper.Clamp(central, 0f, (float) Math.PI * (3f/4f));
            this.Rotation = central;

            if (this.Position.Y > (this.Layer.Bounds.Bottom + (this.Height * this.Scale)))
            {
                this.Position = new Vector2(this.Position.X, (this.Layer.Bounds.Bottom + (this.Height * this.Scale)));
                if (!(this.Level as DynamicLevel).Running)
                {
                    (this.Level as DynamicLevel).Stop();
                }
            }

            if (this.Position.Y < (0 - (this.Height * this.Scale)))
            {
                this.Position = new Vector2(this.Position.X, 0 - (this.Height * this.Scale));
            }

            if (this.OnMove != null)
            {
                this.OnMove(this, EventArgs.Empty);
            }
        }

        //Basically a separate update loop called from the level
        //while the welcome screen is active
        public void Bobble()
        {
            int minY = (int) (this.Layer.Bounds.Height / 2) - (this.Height / 2) - 20;
            int maxY = (int) (this.Layer.Bounds.Height / 2) + (this.Height / 2) + 20;

            if (this.Velocity.Y == 0f)
            {
                this.Velocity = new Vector2(0, 1);
            }

            if (this.Position.Y < minY)
            {
                Console.WriteLine("Going down");
                this.Velocity = new Vector2(0, 1);
            }
            else if (this.Position.Y + this.Height > maxY)
            {
                Console.WriteLine("Going back up");
                this.Velocity = new Vector2(0, -1);
            }



        }

        public void Flap()
        {
            this.Velocity = new Vector2(this.Velocity.X, -8f);
        }

        public void OnCollision(Direction d, Sprite other, Rectangle intersection)
        {
            if (other is SpritePipe)
            {
                (this.Level as DynamicLevel).Stop();
            }
        }

        public void Restore()
        {
            this.Position = new Vector2((this.Game.Viewport.Width / 2) - (this.Width / 2), (this.Game.Viewport.Height / 2) - (this.Height / 2));
            this.Velocity = new Vector2(3f, -8f);
            this.Acceleration = new Vector2(0f, 0.5f);
        }
    }
}
