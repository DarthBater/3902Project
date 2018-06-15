using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Baritone.MappyBird_Sprint5;
using Baritone.MappyBird_Sprint5.Utils;

namespace Baritone.MappyBird_Sprint5.Wrappers.Game
{
    public class SpriteText : IDrawable
    {

        private Sprint5 game;
        
        public Vector2 Position { get; set; }
        public float Paralax { get; set; }
        public Color Color { get; set; }
        public Func<string> Text { get; set; }

        public Sprite Icon { get; set; }

        public SpriteText(Sprint5 game, int x, int y, Color c)
        {
            this.game = game;
            this.Position = new Vector2(x, y);
            this.Color = c;
            Text = () => "";
        }

        public void Update(GameTime gameTime)
        {
        }

        public void Draw(SpriteBatch spriteBatch, float paralax)
        {
            spriteBatch.DrawString(FontFactory.LoadFont(this.game, "Fonts/arial"), 
                this.Text(), 
                new Vector2(this.Position.X - (this.game.CurrentLevel.Camera.X * this.Paralax), this.Position.Y), 
                this.Color);
        }
    }
}
