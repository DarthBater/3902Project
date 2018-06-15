using Baritone.MappyBird_Sprint5.Wrappers.Game.Listeners;
using Baritone.MappyBird_Sprint5.Wrappers.Input;
using Baritone.MappyBird_Sprint5.Wrappers.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baritone.MappyBird_Sprint5.Wrappers.Game.Levels.One.Layers.Background.Listeners
{
    public class FlapListener : KeyListener
    {

        private SpriteMappy mappy;
        private DynamicLevel level;
        private Layer welcomeLayer;

        public FlapListener(SpriteMappy mappy, DynamicLevel level, Layer welcomeLayer) : base(mappy.Layer)
        {
            this.mappy = mappy;
            this.level = level;
            this.welcomeLayer = welcomeLayer;
        }

        public override void OnHold(int key)
        {
        }

        public override void OnPress(int key)
        {
            Console.WriteLine("Flap!");
            this.mappy.Velocity = new Vector2(this.mappy.Velocity.X, -8f);
            if (this.level.Layers.Contains(this.welcomeLayer))
            {
               

                Task.Factory.StartNew(delegate () {
                    this.level.RemoveLayer(this.welcomeLayer);
                    this.level.Start();
                }); 
            }
        }

        public override void OnRelease(int key)
        {
        }
    }
}
