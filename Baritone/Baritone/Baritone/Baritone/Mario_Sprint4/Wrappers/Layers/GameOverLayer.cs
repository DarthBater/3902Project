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
    public class GameOverLayer : SpriteLayer
    {

        private Sprint4 game;
        private SpriteCollection gameOver;

        public GameOverLayer(Sprint4 game) : base(0f)
        {
            this.game = game;

            this.SortMode = SpriteSortMode.Deferred;
            this.BlendMode = BlendState.Opaque;

            this.gameOver = SpriteFactory.CreateGameOver(game);
            int w = this.gameOver.Info.spriteWidth;
            int h = this.gameOver.Info.spriteHeight;
            int cx = game.GetViewport().Right / 2;
            int cy = game.GetViewport().Bottom / 2;
            this.gameOver.SetPosition(cx - (w / 2), cy - (h / 2));
        }

        public override void Update(GameTime gametime)
        {
            base.Update(gametime);
            if (this.game.Mario.lives == 0 && !this.Sprites.Contains(this.gameOver))
            {
                SoundFactory.PlaySoundEffect(SoundFactory.GameOver());
                this.AddSprite(this.gameOver);
                int dy = 250;
                this.gameOver.Info.bounds = new Rectangle(this.gameOver.Info.x - 2, this.gameOver.Info.y - dy, this.gameOver.Info.spriteWidth + 2, this.gameOver.Info.spriteHeight + dy);
                this.gameOver.SetPosition(this.gameOver.Info.x, this.gameOver.Info.y - dy);
                this.gameOver.Info.acceleration.Y = 0.15f;
                
                foreach (InputController ic in SpriteLayer.CollisionLayer.InputControllers)
                {
                    ic.Paused = true;
                }
            }
            else if (this.game.Mario.lives > 0 && this.Sprites.Contains(this.gameOver))
            {
                this.RemoveSprite(this.gameOver);
                foreach (InputController ic in SpriteLayer.CollisionLayer.InputControllers)
                {
                    ic.Paused = false;
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Color c = Color.Black;
            if (this.Sprites.Count() > 0)
            {
                c = Color.White;
                this.game.GraphicsDevice.Clear(Color.Black);
                foreach (SpriteCollection sprite in this.Sprites)
                {
                    sprite.Draw(spriteBatch);
                }
            }
            foreach (SpriteText text in SpriteLayer.HuDLayer.SpriteTexts)
            {
                text.Color = c;
            }
        }

    }
}
