using Baritone.Input;
using Baritone.Sprites;
using Baritone.Utils;
using Baritone.Wrappers.Layers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Baritone.Wrappers
{
    public class SpriteLayer: IDrawable
    {

        public static readonly SpriteLayer BackgroundLayer = new SpriteLayer(0.6f);
        public static SpriteLayer CollisionLayer;
        public static YouWinLayer YouWinLayer;
        public static SpriteLayer GameOverLayer;
        public static SpriteLayer HuDLayer;

        static SpriteLayer()
        {
            BackgroundLayer.SortMode = SpriteSortMode.Deferred;
            BackgroundLayer.BlendMode = BlendState.AlphaBlend;
            BackgroundLayer.SamplerState = SamplerState.LinearWrap;

            BackgroundLayer.name = "Background";
        }

        public bool Paused { get; set; }
        public float Paralax { get; private set; }
        public SpriteSortMode SortMode { get; protected set; }
        public SamplerState SamplerState { get; protected set; }
        public BlendState BlendMode { get; protected set; }

        protected string name;

        public List<SpriteCollection> Sprites
        {
            get; private set;
        }
        public List<SpriteText> SpriteTexts
        {
            get; private set;
        }

        public List<InputController> InputControllers
        {
            get; private set;
        }

        protected SpriteLayer(float paralax)
        {
            this.Paralax = paralax;
            this.InputControllers = new List<InputController>();
            this.Sprites = new List<SpriteCollection>();
            this.SpriteTexts = new List<SpriteText>();
        }

        public virtual void Update(GameTime gameTime)
        {
            //InputControllers check whether they're paused or not
            foreach (InputController input in this.InputControllers)
            {
                input.ScanInputs();
            }

            if (!this.Paused)
            {
                lock (this)
                {
                    foreach (SpriteCollection sprite in this.Sprites)
                    {
                        sprite.Update(gameTime);

                        if (this == CollisionLayer)
                        {
                            if (!CollisionHandler.LastScan.Contains(sprite) && !sprite.isStatic() && !sprite.Info.manual)
                            {
                                sprite.Info.acceleration.Y = 0.2f;
                            }

                            if (sprite is SpriteGoomba)
                            {
                                if (sprite.SpriteState != SpriteStates.Sprites.DEAD && sprite.Info.velocity.X == 0 && Sprint4.Instance.GetViewport().Contains(sprite.Info.x, sprite.Info.y))
                                {
                                    (sprite as SpriteGoomba).move();
                                }
                            }
                            /*if (sprite is SpriteKoopa)
                            {
                                if (sprite.Info.velocity.X == 0 && Sprint3.Instance.GetViewport().Contains(sprite.Info.x, sprite.Info.y))
                                {
                                    (sprite as SpriteKoopa).move();
                                }
                            }//*/

                        }
                    }
                }
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            lock (this)
            {
                foreach (SpriteCollection sprite in this.Sprites)
                {
                    sprite.Draw(spriteBatch);
                }

                foreach (SpriteText text in this.SpriteTexts)
                {
                    text.Draw(spriteBatch);
                }
            }
        }

        public void AddSprite(SpriteCollection sprite)
        {
            lock (this)
            {
                Console.WriteLine("Adding sprite {0} to layer {1}", sprite.name, this.name);
                sprite.Info.paralaxLayer = this.Paralax;
                this.Sprites.Add(sprite);
            }
            if (this == CollisionLayer)
            {
                if (sprite.isStatic())
                {
                    if (CollisionHandler.grid != null)
                    {
                        List<int[]> grids = SpriteUtils.DetermineGrids(sprite);
                        foreach (int[] grid in grids)
                        {
                            CollisionHandler.grid[grid[0], grid[1]].Add(sprite);
                        }
                    }
                    CollisionHandler.statics.Add(sprite);
                }
                else
                {
                    CollisionHandler.dynamics.Add(sprite);
                }
            }
        }

        public void RemoveSprite(SpriteCollection sprite)
        {
            lock (this)
            {
                this.Sprites.Remove(sprite);
            }
            if (this == CollisionLayer)
            {
                if (sprite.isStatic())
                {
                    CollisionHandler.statics.Remove(sprite);
                }
                else
                {
                    CollisionHandler.dynamics.Remove(sprite);
                }
            }
        }

        public void AddSpriteText(SpriteText spriteText)
        {
            spriteText.paralaxLayer = this.Paralax;
            this.SpriteTexts.Add(spriteText);
        }

        public void RemoveSpriteText(SpriteText spriteText)
        {
            this.SpriteTexts.Remove(spriteText);
        }

    }
}
