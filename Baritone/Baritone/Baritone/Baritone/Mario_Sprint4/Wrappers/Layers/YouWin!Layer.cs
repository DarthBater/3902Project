using Baritone.Input;
using Baritone.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Baritone.Wrappers.Layers
{
    public class YouWinLayer : SpriteLayer
    {

        private Sprint4 game;
        public SpriteCollection youWin
        {
            get; private set;
        }

        public YouWinLayer(Sprint4 game) : base(0f)
        {
            this.game = game;

            this.SortMode = SpriteSortMode.Deferred;
            this.BlendMode = BlendState.AlphaBlend;

            this.youWin = SpriteFactory.CreateYouWin(game);
            int w = this.youWin.Info.spriteWidth;
            int h = this.youWin.Info.spriteHeight;
            int cx = game.GetViewport().Right / 2;
            int cy = game.GetViewport().Bottom / 2;
            this.youWin.SetPosition(cx - (w / 2), cy - (h / 2));
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (this.Sprites.Count() > 0)
            {
                this.game.GraphicsDevice.Clear(Color.CornflowerBlue);
                foreach (SpriteCollection sprite in this.Sprites)
                {
                    sprite.Draw(spriteBatch);
                }
                foreach (SpriteText text in SpriteLayer.HuDLayer.SpriteTexts)
                {
                    text.Color = Color.Black;
                }
            }
            
        }

    }
}
