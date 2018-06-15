using Baritone.MappyBird_Sprint5.Utils;
using Baritone.MappyBird_Sprint5.Wrappers.Input;
using Baritone.MappyBird_Sprint5.Wrappers.Game.Levels.One.Layers.Background.Listeners;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Baritone.MappyBird_Sprint5.Wrappers.Game.Levels.One.Layers.Background
{
    public class BackgroundLayer : Layer
    {

        private InputController keyboard = new InputControllerKeyboard();
        private List<Layer> backgrounds = new List<Layer>();

        public BackgroundLayer(Level level) : base(level, 0.6f, null, BlendState.AlphaBlend)
        {
            this.InputControllers.Add(this.keyboard);

            this.keyboard.RegisterKeyListener(Keys.M, new MuteListener(this));

            this.backgrounds.Add(new BackgroundLayerClouds(level));
            this.backgrounds.Add(new BackgroundLayerMountains(level));
        }

        //Load clouds into the background layer
        public override void Load()
        {
            foreach (Layer layer in this.backgrounds)
            {
                layer.Load();
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            foreach (Layer layer in this.backgrounds)
            {
                layer.Update(gameTime);
            }
        }

        public override void Draw(SpriteBatch spriteBatch, float paralax)
        {
            foreach (Layer layer in this.backgrounds)
            {
                layer.Draw(spriteBatch, paralax);
            }
        }

        public override void Restore()
        {
            foreach (Layer layer in this.backgrounds)
            {
                layer.Restore();
            }
        }

    }
}
