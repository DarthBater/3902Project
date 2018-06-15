using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Baritone.MappyBird_Sprint5.Wrappers.Game.Levels.One.Sound;
using Baritone.MappyBird_Sprint5.Wrappers.Game.Levels.One.Layers.Background;
using Baritone.MappyBird_Sprint5.Wrappers.Game.Levels.One.Layers.Collision;
using Baritone.MappyBird_Sprint5.Wrappers.Sprites;
using Microsoft.Xna.Framework.Input;
using Baritone.MappyBird_Sprint5.Wrappers.Game.Levels.One.Layers.Background.Listeners;
using Microsoft.Xna.Framework.Media;
using Baritone.MappyBird_Sprint5.Utils;
using Baritone.MappyBird_Sprint5.Wrappers.Game.Levels.One.Layers.Welcome;
using Baritone.MappyBird_Sprint5.Wrappers.Game.Levels.One.Layers.GameOver;
using Baritone.MappyBird_Sprint5.Wrappers.Input;
using Baritone.MappyBird_Sprint5.Wrappers.Game.Listeners;

namespace Baritone.MappyBird_Sprint5.Wrappers.Game.Levels.One
{
    public class DynamicLevel : Level
    {

        public L1SoundHandler L1SoundHandler { get; private set; }

        public WelcomeLayer WelcomeLayer { get; private set; }
        public BackgroundLayer BackgroundLayer { get; private set; }
        public CollisionLayer CollisionLayer { get; private set; }

        public GameOverLayer GameOverLayer { get; private set; }
        public HuDLayer HuDLayer { get; private set; }

        public SpriteMappy Mappy { get; private set; }

        public bool Running { get; set; }

        private InputController keyboard;

        public DynamicLevel(Sprint5 game) : base(game)
        {
            this.L1SoundHandler = new L1SoundHandler(this);
            this.Layers.Add(this.BackgroundLayer = new BackgroundLayer(this));
            this.Layers.Add(this.CollisionLayer = new CollisionLayer(this));
            this.Layers.Add(this.WelcomeLayer = new WelcomeLayer(this));
            this.GameOverLayer = new GameOverLayer(this);
            this.Layers.Add(this.HuDLayer = new HuDLayer(this));

            this.Mappy = new SpriteMappy(this.CollisionLayer);
            this.Mappy.Position = new Vector2((this.Game.Viewport.Width / 2) - (this.Mappy.Width / 2), this.Game.Viewport.Height / 2 - (this.Mappy.Height / 2));

            KeyListener flap = new FlapListener(this.Mappy, this, this.WelcomeLayer);
            this.CollisionLayer.Keyboard.RegisterKeyListener(Keys.Space, flap);
            this.CollisionLayer.Keyboard.RegisterKeyListener(Keys.Up, flap);

            this.CollisionLayer.GamePad.RegisterKeyListener(Buttons.A, flap);

            (this.CollisionLayer.Mouse as InputControllerMouse).SetOnLeftClick(delegate ()
            {
                flap.OnPress(0);
            });

            this.CollisionLayer.AddSprite(this.Mappy);

            this.keyboard = new InputControllerKeyboard();
            this.keyboard.RegisterKeyListener(Keys.R, new ResetListener(this));
            this.keyboard.RegisterKeyListener(Keys.Q, new ExitListener(this));

            this.Running = false;
        }

        public override void Update(GameTime gameTime)
        {
            if(this.Running == false && !this.Layers.Contains(this.GameOverLayer) && this.CollisionLayer.Keyboard.Paused == true)
            {
                this.Layers.Add(this.GameOverLayer);
            }
            else if(this.Running == true && this.Layers.Contains(this.GameOverLayer))
            {
                this.Layers.Remove(this.GameOverLayer);
            }

            base.Update(gameTime);
            this.Camera = new Point((int) this.Mappy.Position.X - (this.Game.Viewport.Width / 2), 0); //The camera will follow Mappy
            this.Load();

            if (this.Layers.Contains(this.WelcomeLayer))
            {
                this.Mappy.Bobble();
            }
            else if (!this.InputControllers.Contains(this.keyboard))
            {
                Console.WriteLine("Adding reset");
                this.InputControllers.Add(this.keyboard);
            }
        }

        public override void Load()
        {
            foreach (Layer layer in this.Layers)
            {
                layer.Load();
            }
        }

        public override void Draw(SpriteBatch spriteBatch, float paralax)
        {
            this.Game.GraphicsDevice.Clear(Color.CornflowerBlue);
            base.Draw(spriteBatch, paralax);
        }

        public void Start()
        {
            if (!this.Running)
            {

                this.CollisionLayer.Keyboard.Paused = false;
                this.Mappy.Velocity = new Vector2(3f, -8f);
                this.Mappy.Acceleration = new Vector2(0f, 0.5f);
                SoundHandler.PlaySong(this.L1SoundHandler.AxelF);
                this.Running = true;
            }
        }

        public void Stop()
        {
            if (this.Running)
            {
                foreach (Layer layer in this.Layers)
                {
                    foreach (InputController ic in layer.InputControllers)
                    {
                        ic.Paused = true;
                    }
                }
                this.Mappy.Velocity = new Vector2(0f, -5f);
                MediaPlayer.Stop();
                SoundHandler.PlaySoundEffect(this.L1SoundHandler.Death);
                this.Running = false;
            }
        }

        public override void Restore()
        {
            base.Restore();
            foreach (Layer layer in this.Layers)
            {
                foreach (InputController ic in layer.InputControllers)
                {
                    ic.Paused = false;
                }
            }
            MediaPlayer.Stop();
            SoundHandler.PlaySong(this.L1SoundHandler.AxelF);
            this.Running = true;
        }

    }
}
