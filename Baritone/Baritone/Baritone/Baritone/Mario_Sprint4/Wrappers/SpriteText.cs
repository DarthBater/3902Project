using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace Baritone.Wrappers
{
    public class SpriteText : IDrawable
    {

        public static SpriteFont FONT_ARIAL;
        //public static SpriteFont FONT_MARIO;

        private Sprint4 game;
        private int x, y;
        private Vector2 pos;
        public float paralaxLayer = 0f;

        public int X {
            get {
                return this.x;
            }
            set {
                this.x = value;
                pos = new Vector2(this.x, this.y);
            }
        }

        public int Y {
            get
            {
                return this.y;
            }
            set {
                this.y = value;
                pos = new Vector2(this.x, this.y);
            }
        }
        public Color Color { get; set; }
        public Func<string> Text { get; set; }

        public SpriteCollection Icon { get; set; }

        public SpriteText(Sprint4 game, int x, int y, Color c)
        {
            if (FONT_ARIAL == null)
            {
                FONT_ARIAL = game.Content.Load<SpriteFont>("Fonts/arial");
            }

            /*if (FONT_MARIO == null)
            {
                FONT_MARIO = game.Content.Load<SpriteFont>("Fonts/mariofont");
            }//*/

            this.game = game;
            this.X = x;
            this.Y = y;
            this.Color = c;
            Text = () => "";
        }

        public void Update(GameTime gameTime)
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(FONT_ARIAL, 
                this.Text(), 
                new Vector2(this.pos.X - (this.game.CameraX * this.paralaxLayer), this.pos.Y), 
                this.Color);
        }
    }
}
