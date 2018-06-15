using Baritone.MappyBird_Sprint5.Utils;
using Baritone.MappyBird_Sprint5.Wrappers.Input;
using Baritone.MappyBird_Sprint5.Wrappers.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baritone.MappyBird_Sprint5.Wrappers.Game.Levels.One.Layers.Collision
{
    public sealed class CollisionLayer : Layer
    {

        private Random random = new Random();
        private int sign = 1;

        private int nextPipe;
        private int previousY;

        private int coinChance = 0;

        public InputController Keyboard { get; private set; }
        public InputController GamePad { get; private set; }
        public InputController Mouse { get; private set; }

        public CollisionHandler CollisionHandler { get; private set; }

        public CollisionLayer(Level level) : base(level, 1f, null, BlendState.AlphaBlend)
        {
            this.nextPipe = 4;
            this.previousY = (this.Game.Viewport.Height / 2) + 50;

            this.Keyboard = new InputControllerKeyboard();
            this.GamePad = new InputControllerGamePad(PlayerIndex.One);
            this.Mouse = new InputControllerMouse();

            this.InputControllers.Add(this.Keyboard);
            this.InputControllers.Add(this.GamePad);
            this.InputControllers.Add(this.Mouse);

            this.CollisionHandler = new CollisionHandler(this);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if ((this.Level as DynamicLevel).Running)
            {
                this.CollisionHandler.ScanCollisions();
            }
        }

        public override void AddSprite(Sprite sprite)
        {
            base.AddSprite(sprite);

            lock (this)
            {
                if (sprite.IsStatic)
                    this.CollisionHandler.Statics.Add(sprite);
                else
                    this.CollisionHandler.Dynamics.Add(sprite);
            }
        }

        public override bool RemoveSprite(Sprite sprite)
        {
            bool result = base.RemoveSprite(sprite);

            lock (this)
            {
                if (sprite.IsStatic)
                    this.CollisionHandler.Statics.Remove(sprite);
                else
                    this.CollisionHandler.Dynamics.Remove(sprite);
            }

            return result;
        }

        //Load pipes into the collision layer
        public override void Load()
        {
            int scale = (int) (46 * 5.5f);
            int buffer = 150;
            int gap = 150;

            int current = this.Level.Camera.X;
            int np = this.nextPipe * scale;
            int twoViewports = this.Game.Viewport.Width * 2;

            if ((np - current) < twoViewports)
            {

                int y = this.previousY + this.random.Next(-150, 150);

                if (y < (100 + buffer))
                    y = (100 + buffer + this.random.Next(0, 75));
                if (y > (this.Game.Viewport.Bottom - buffer))
                    y = (this.Game.Viewport.Bottom - buffer - this.random.Next(0, 75));

                this.previousY = y;

                SpritePipe pipeB = SpriteFactory.CreatePipe(this);
                pipeB.Position = new Vector2(np, y);

                SpritePipe pipeT = SpriteFactory.CreatePipe(this);
                pipeT.Effects = SpriteEffects.FlipVertically;
                pipeT.Position = new Vector2(np, y - (pipeT.Height * pipeT.Scale) - gap);

                pipeB.Offset = 60;
                pipeT.Offset = 60;

                Random r = new Random();
                pipeB.Velocity = new Vector2(0, SpritePipe.Speed * this.sign);
                pipeT.Velocity = new Vector2(0, SpritePipe.Speed * this.sign);
                this.sign *= -1;
                
                int n = r.Next(0, 100);
                if (n < 70 + coinChance)
                {
                    coinChance = 0;

                    SpriteCoin coin = SpriteFactory.CreateCoin(this);
                    //middle of gap
                    int yCoin = y - pipeB.Height + (int)gap;
                    int xCoin = np + pipeB.Width + (scale/2);

                    xCoin += this.random.Next(-25, 15);
                    yCoin += this.random.Next(-80, 80);

                    if (coin != null)
                    {
                        coin.Position = new Vector2(xCoin, yCoin);
                        (this.Level as DynamicLevel).Mappy.OnMove += coin.CheckForRemoval;
                        this.AddSprite(coin);
                    }
                } else
                {
                    coinChance += 10;
                }

                (this.Level as DynamicLevel).Mappy.OnMove += pipeB.CheckForRemoval;
                (this.Level as DynamicLevel).Mappy.OnMove += pipeT.CheckForRemoval;
                (this.Level as DynamicLevel).Mappy.OnMove += pipeB.CheckForPassing;

                this.AddSprite(pipeB);
                this.AddSprite(pipeT);
                this.Bounds = new Rectangle(0, 0, this.Bounds.Width + scale, this.Bounds.Height);
                this.nextPipe++;
            }

        }

        public override void Restore()
        {
            base.Restore();

            Task.Factory.StartNew(delegate () {
                lock (this) {

                    foreach (Sprite sprite in this.Sprites)
                    {
                        (this.Level as DynamicLevel).Mappy.OnMove -= sprite.CheckForRemoval;
                    }

                    this.Sprites.Clear();
                    this.CollisionHandler.Dynamics.Clear();
                    this.CollisionHandler.Statics.Clear();

                    this.nextPipe = 3;
                    this.previousY = (this.Game.Viewport.Height / 2) + 50;
                }
                this.AddSprite((this.Level as DynamicLevel).Mappy);
            });

            this.Bounds = this.Game.Viewport;
        }

    }
}
