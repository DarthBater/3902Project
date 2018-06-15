using Baritone.MappyBird_Sprint5.Wrappers.Sprites;
using Baritone.MappyBird_Sprint5.Wrappers.Game;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Baritone.MappyBird_Sprint5.Utils
{
    public static class SpriteFactory
    {

        private static Dictionary<string, Texture2D> cache = new Dictionary<string, Texture2D>();

        public static Texture2D LoadTexture(Game game, string path)
        {
            if (cache.ContainsKey(path))
            {
                return cache[path];
            }
            return cache[path] = game.Content.Load<Texture2D>(path);
        }


        public static Sprite CreateCloudSmall(Layer layer)
        {
            return new SpriteCloudSmall(layer);
        }

        public static Sprite CreateCloudLarge(Layer layer)
        {
            return new SpriteCloudLarge(layer);
        }

        public static SpritePipe CreatePipe(Layer layer)
        {
            return new SpritePipe(layer);
        }

        public static Sprite CreateMountains(Layer layer)
        {
            return new SpriteMountains(layer);
        }

        public static SpriteCoin CreateCoin(Layer layer)
        {
            return new SpriteCoin(layer);
        }

        public static Sprite CreateWelcomeScreen(Layer layer)
        {
            return new SpriteWelcomeScreen(layer);
        }

        public static Sprite CreateYouLoseScreen(Layer layer)
        {
            return new SpriteGameOver(layer);
        }

    }
}
