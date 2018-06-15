using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Baritone.MappyBird_Sprint5.Wrappers
{
    public class SpriteMap
    {

        private Dictionary<SpriteSheet, Dictionary<SpriteState, Texture2D>> map;
        
        internal SpriteMap()
        {
            this.map = new Dictionary<SpriteSheet, Dictionary<SpriteState, Texture2D>>();
        }

        public bool AddSprite(SpriteSheet sheet, SpriteState sprite, Texture2D texture)
        {
            bool wasAdded = false;
            
            if (!this.map.ContainsKey(sheet))
            {
                this.map[sheet] = new Dictionary<SpriteState, Texture2D>();
                this.map[sheet][sprite] = texture;
                wasAdded = true;
            }
            else if (!this.map[sheet].ContainsKey(sprite))
            {
                this.map[sheet][sprite] = texture;
                wasAdded = true;
            }
            else
            {
                //Overwrite the current value, but return false because
                //a value already existed in that location
                this.map[sheet][sprite] = texture;
            }

            return wasAdded;
        }

        internal Texture2D GetSprite(SpriteSheet? sheet, SpriteState? sprite)
        {
            if (!sheet.HasValue || !sprite.HasValue)
            {
                return null;
            }

            if (this.map.ContainsKey(sheet.Value) && this.map[sheet.Value].ContainsKey(sprite.Value))
            {
                return this.map[sheet.Value][sprite.Value];
            }
            return null;
        }

    }
}
