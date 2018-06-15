using Baritone.MappyBird_Sprint5.Wrappers;
using Baritone.MappyBird_Sprint5.Wrappers.Game;
using Baritone.MappyBird_Sprint5.Wrappers.Game.Levels.One;
using Baritone.MappyBird_Sprint5.Wrappers.Game.Listeners;
using Baritone.MappyBird_Sprint5.Wrappers.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Baritone.MappyBird_Sprint5
{
    public class Sprint5 : Game
    {

        private Rectangle viewport;
        public Rectangle Viewport { get { return this.viewport; } }

        private SpriteBatch spriteBatch;

        public Level CurrentLevel { get; private set; }

        public ICollection<InputController> InputControllers { get; private set; }

        public Sprint5()
        {
            new GraphicsDeviceManager(this);
            Console.WriteLine("Constructor");
            Content.RootDirectory = "Content";

            this.InputControllers = new List<InputController>();

        }

        protected override void Initialize()
        {
            Console.WriteLine("Initialize");
            base.Initialize();

        }

        protected override void LoadContent()
        {
            Console.WriteLine("LoadContent");
            this.spriteBatch = new SpriteBatch(this.GraphicsDevice);
            this.viewport = this.GraphicsDevice.Viewport.TitleSafeArea;

            this.CurrentLevel = new DynamicLevel(this);
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            foreach (InputController inputController in this.InputControllers)
                inputController.ScanInputs();
            
            this.CurrentLevel.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            this.CurrentLevel.Draw(this.spriteBatch, 1f);
        }

    }
}
