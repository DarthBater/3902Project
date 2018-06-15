using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Baritone.MappyBird_Sprint5.Wrappers
{
    public interface IDrawable
    {

        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch, float paralax);

    }
}
