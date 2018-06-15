using Baritone.MappyBird_Sprint5.Wrappers.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Baritone.MappyBird_Sprint5.Wrappers.Sprites
{
    public class SpriteMountains : Sprite
    {

        public SpriteMountains(Layer layer) : base(layer, 1, 1)
        {
            this.SetDefault("Sprites/background");
            this.Position = new Vector2(0, layer.Bounds.Bottom - (this.Height * this.Scale));
        }

        public override void Draw(SpriteBatch spriteBatch, float paralax)
        {
            Vector2 pos = new Vector2(
               (this.Position.X + this.Width) - (this.Level.Camera.X * paralax),
               this.Position.Y + this.Height);

            Rectangle source = new Rectangle(
                0,
                0,
                this.Level.Camera.X + this.Game.Viewport.Width,
                this.Height);

            spriteBatch.Draw(
                this.SpriteMap.GetSprite(this.SpriteSheet, this.SpriteState),
                pos,
                source,
                Color.White,
                this.Rotation,

                //Vector2.Zero,
                new Vector2(this.Width / 2, this.Height / 2),

                this.Scale,
                this.Effects,
                0f
                );
        }

    }
}
