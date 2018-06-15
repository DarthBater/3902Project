using Baritone.Utils;
using Baritone.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Baritone.Sprites
{
    public class SpriteBackground : SpriteCollection
    {

        public SpriteBackground(Sprint4 game) : base(game)
        {
            SpriteUtils.SetDefaultProperties(this, "Sprites/Background");

            //this.Info.scale = 1.5f;
            this.Info.layer = 0;
            this.Info.scale = 1f;
            this.Info.numFrames = 1;
            this.Info.spriteWidth = 515;
            this.Info.paralaxLayer = 0.6f;
        }

        public override bool isStatic()
        {
            return true;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Vector2 position = new Vector2(0 - (this.game.CameraX * this.Info.paralaxLayer), 0);
            Rectangle source = new Rectangle(0, 0, this.game.bounds.Width, this.Info.spriteHeight);

            spriteBatch.Draw(
                    this.current,
                    position,
                    source,
                    Color.White,
                    0f,
                    new Vector2(0, 0),
                    this.Info.scale,
                    this.Info.spriteEffects, 
                    0);
        }
    }
}
