using System;
using Baritone.MappyBird_Sprint5.Utils;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Baritone.MappyBird_Sprint5.Wrappers.Game.Levels.One.Layers.GameOver
{
    public sealed class GameOverLayer : Layer
    {

        public GameOverLayer(Level level) : base(level, 0f, null, null)
        {
            Sprite gameover = SpriteFactory.CreateYouLoseScreen(this);
            Console.WriteLine(gameover.Width);
            gameover.Position = new Vector2(
                (this.Game.Viewport.Width / 2) - (gameover.Width / 2 * gameover.Scale),
                (this.Game.Viewport.Height / 2) - (gameover.Height / 2 * gameover.Scale) - 100);
            

            SpriteText text = new SpriteText(Game, this.Game.Viewport.Width / 2 - 80, this.Game.Viewport.Height / 2, Color.Black);
            text.Text = delegate ()
            {
                return "Press 'R' to Start Over\nPress 'Q' to Quit";
            };

            this.AddSpriteText(text);
            this.AddSprite(gameover);
        }


    }
}
