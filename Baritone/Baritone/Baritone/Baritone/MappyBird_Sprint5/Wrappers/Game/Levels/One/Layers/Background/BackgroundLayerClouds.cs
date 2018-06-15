using Baritone.MappyBird_Sprint5.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baritone.MappyBird_Sprint5.Wrappers.Game.Levels.One.Layers.Background
{

    internal class BackgroundLayerClouds : Layer
    {
        private Random random = new Random();

        private int nextCloud;
        private int previousY;

        public BackgroundLayerClouds(Level level) : base(level, 0.6f, null, BlendState.AlphaBlend)
        {
            this.nextCloud = 0;
            this.previousY = 30;
        }

        public override void Load()
        {
            int scale = (int)(64 * 6f);

            int current = this.Level.Camera.X;
            int np = this.nextCloud * scale;
            int twoViewports = this.Game.Viewport.Width * 2;

            if ((np - current) < twoViewports)
            {

                int y = this.previousY + this.random.Next(-20, 30);

                Sprite cloud = this.random.Next(0, 3) == 0 ? SpriteFactory.CreateCloudSmall(this) : SpriteFactory.CreateCloudLarge(this);
                cloud.Position = new Vector2(np + this.random.Next(0, 30), y);

                (this.Level as DynamicLevel).Mappy.OnMove += cloud.CheckForRemoval;

                this.AddSprite(cloud);
                this.Bounds = new Rectangle(0, 0, this.Bounds.Width + scale, this.Bounds.Height);
                this.nextCloud++;
            }
        }

        public override void Restore()
        {
            base.Restore();

            Task.Factory.StartNew(delegate() {
                lock (this) {

                    foreach (Sprite sprite in this.Sprites)
                    {
                        (this.Level as DynamicLevel).Mappy.OnMove -= sprite.CheckForRemoval;
                    }

                    this.Sprites.Clear();
                }

                this.nextCloud = 0;
                this.previousY = 30;
            });
            this.Bounds = this.Game.Viewport;
        }
    }
}
