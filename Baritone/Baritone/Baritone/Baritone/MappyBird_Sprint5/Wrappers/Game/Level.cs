//using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Baritone.MappyBird_Sprint5.Utils;
using System;
using Baritone.MappyBird_Sprint5.Wrappers.Game.Levels.One;

namespace Baritone.MappyBird_Sprint5.Wrappers.Game
{
    public class Level : IDrawable, ILoadable, IRestorable
    {

        public Sprint5 Game { get; private set; }
        public Point Camera { get; set; }
        public int Score { get; set; }
        public int timeLeft { get; set; }
        private double currentTime { get; set; }

        public ICollection<Layer> Layers { get; private set; }
        public ICollection<InputController> InputControllers { get; private set; }

        public Level(Sprint5 game)
        {
            this.Game = game;
            this.Camera = new Point(0, 0);
            this.Score = 0;
            this.timeLeft = 10;
            this.currentTime = 0;
            this.Layers = new List<Layer>();
            this.InputControllers = new List<InputController>();
        }

        public virtual void Update(GameTime gameTime)
        {
            
            //InputControllers check whether they're paused or not
            foreach (InputController input in this.InputControllers)
            {
                input.ScanInputs();
            }

            lock (this)
            {
                foreach (Layer layer in this.Layers)
                {
                    layer.Update(gameTime);
                }
            }
            if (!this.Layers.Contains((this as DynamicLevel).WelcomeLayer) && !this.Layers.Contains((this as DynamicLevel).GameOverLayer))
            {
                if (this.timeLeft > 0)
                {
                    this.currentTime += gameTime.ElapsedGameTime.TotalSeconds;
                    if (this.currentTime >= 1)
                    {
                        Console.WriteLine("Time Left: " + this.timeLeft);
                        this.currentTime -= 1;
                        this.timeLeft -= 1;
                    }
                }
                else
                {
                    (this as DynamicLevel).Stop();
                }
            }
        }

        public void updateScore(object sender, EventArgs eventArgs)
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch, float paralax)
        {
            lock (this)
            {
                foreach (Layer layer in this.Layers)
                {
                    layer.Draw(spriteBatch, 1f);
                }
            }
        }

        public void AddLayer(Layer layer)
        {
            lock (this)
            {
                this.Layers.Add(layer);
            }
        }

        public void RemoveLayer(Layer layer)
        {
            lock (this)
            {
                this.Layers.Remove(layer);
            }
        }

        public virtual void Load()
        {
        }

        public virtual void Restore()
        {
            //Reset the level

            this.Camera = new Point(0, 0);
            this.Score = 0;
            this.currentTime = 0;
            this.timeLeft = 10;
            foreach (Layer layer in this.Layers)
            {
                layer.Restore();
            }
        }
    }
}
