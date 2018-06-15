using Baritone.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Baritone.Wrappers;
using System.Threading;
using Baritone.States.Powerups;
using Microsoft.Xna.Framework.Audio;

namespace Baritone.Sprites
{
    public class PointTextGenerator
    {
        List<SpritePointText> TextElements;
        Sprint4 game;

        public PointTextGenerator(Sprint4 game) 
        {
            this.TextElements = new List<SpritePointText>();
            this.game = game;
        }

        public void Add(int points, SpriteCollection sprite)
        {
            SpritePointText text = new SpritePointText(this.game, points, sprite);
            TextElements.Add(text);
            SpriteLayer.CollisionLayer.AddSpriteText(text.text);
        }

        public void Update(GameTime gameTime)
        {
            if (!this.game.Layers.TrueForAll(new Predicate<SpriteLayer>(this.game.LayersPaused)))
            {
                foreach (SpritePointText element in this.TextElements)
                {
                    element.currentTime += gameTime.ElapsedGameTime.TotalSeconds;
                    if (element.currentTime >= 1)
                    {
                        SpriteLayer.CollisionLayer.RemoveSpriteText(element.text);
                    }
                    else
                    {
                        element.text.Y--;
                    }
                }
            }
        }
    }
}
