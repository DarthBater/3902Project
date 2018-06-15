using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Baritone.MappyBird_Sprint5.Wrappers.Game
{
    public class Layer: IDrawable, ILoadable, IRestorable
    {

        public bool Paused { get; set; }

        public Level Level { get; private set; }
        public Sprint5 Game { get { return this.Level.Game; } }

        public Rectangle Bounds { get; set; }
        public SamplerState SamplerMode { get; private set; }
        public BlendState BlendMode { get; private set; }

        public ICollection<Sprite> Sprites { get; private set; }
        public ICollection<SpriteText> SpriteTexts { get; private set; }
        public ICollection<InputController> InputControllers { get; private set; }

        public float Paralax { get; private set; }

        protected Layer(Level level, float paralax, SamplerState samplerState, BlendState blendState)
        {
            this.Level = level;
            this.Paralax = paralax;
            this.SamplerMode = samplerState;
            this.BlendMode = blendState;
            this.Bounds = new Rectangle(0, 0, this.Game.Viewport.Width, this.Game.Viewport.Height); //Start bounds at Left

            this.InputControllers = new List<InputController>();
            this.Sprites = new List<Sprite>();
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
                    foreach (Sprite sprite in this.Sprites)
                    {
                        sprite.Update(gameTime);
                    }
                }
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch, float paralax)
        {
            lock (this)
            {
                spriteBatch.Begin(SpriteSortMode.Deferred, this.BlendMode, this.SamplerMode, null, null);

                foreach (Sprite sprite in this.Sprites)
                {
                    sprite.Draw(spriteBatch, this.Paralax);
                }

                foreach (SpriteText text in this.SpriteTexts)
                {
                    text.Draw(spriteBatch, this.Paralax);
                }

                spriteBatch.End();
            }
        }

        public virtual void AddSprite(Sprite sprite)
        {
            lock (this)
            {
                this.Sprites.Add(sprite);
            }
        }

        public virtual bool RemoveSprite(Sprite sprite)
        {
            lock (this)
            {
                return this.Sprites.Remove(sprite);
            }

            //Do this in an override
            /*if (this == CollisionLayer)
            {
                if (sprite.isStatic())
                {
                    CollisionHandler.statics.Remove(sprite);
                }
                else
                {
                    CollisionHandler.dynamics.Remove(sprite);
                }
            }*/
        }

        public void AddSpriteText(SpriteText spriteText)
        {
            spriteText.Paralax = this.Paralax;
            this.SpriteTexts.Add(spriteText);
        }

        public bool RemoveSpriteText(SpriteText spriteText)
        {
            return this.SpriteTexts.Remove(spriteText);
        }

        //Load sprites into this layer on-demand
        public virtual void Load()
        {
        }

        public virtual void Restore()
        {
            //Reset this layer
            foreach (Sprite sprite in this.Sprites)
            {
                if (sprite is IRestorable)
                {
                    (sprite as IRestorable).Restore();
                }
            }
        }
    }
}
