using Baritone.MappyBird_Sprint5.Wrappers.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Baritone.Bird_Sprint5;
using Microsoft.Xna.Framework;
using System.Threading.Tasks;

namespace Baritone.MappyBird_Sprint5.Wrappers.Sprites
{
    public class SpritePipe : Sprite, ICollidable, IRestorable
    {

        private int maxY, minY;
        public static float Speed { get; set; }
        
        public int Offset
        {
            set
            {
                maxY = (int) (this.Position.Y + value);
                minY = (int) (this.Position.Y - value);
            }
        }

        public SpritePipe(Layer layer) : base(layer, 1, 1)
        {
            this.SetDefault("Sprites/obstacles/pipe_tall");

        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (this.Position.Y < minY)
            {
                this.Velocity = new Vector2(0, Speed);
            }
            else if (this.Position.Y > maxY)
            {
                this.Velocity = new Vector2(0, -Speed);
            }
        }

        public void OnCollision(Direction d, Sprite other, Rectangle intersection)
        {
            //Handled by SpriteMappy
        }

        public void Restore()
        {
            Speed = 0f;
        }
    }
}
