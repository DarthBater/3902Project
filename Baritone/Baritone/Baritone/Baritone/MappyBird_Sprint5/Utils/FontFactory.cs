using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Baritone.MappyBird_Sprint5.Utils
{
    public static class FontFactory
    {

        private static Dictionary<string, SpriteFont> cache = new Dictionary<string, SpriteFont>();

        public static SpriteFont LoadFont(Game game, string path)
        {
            if (cache.ContainsKey(path))
            {
                return cache[path];
            }

            return cache[path] = game.Content.Load<SpriteFont>(path);
        }
    }
}
