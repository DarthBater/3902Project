using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Baritone.Wrappers;
using Baritone.Input;
using Baritone.Input.Impl;
using Baritone.Sprites;
using Baritone.Utils;
using System;
using Baritone.Wrappers.Listeners;
using System.Threading;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Baritone.Wrappers.Layers;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;

namespace Baritone
{

    public class Sprint4 : Game
    {
        public const int TIME_LIMIT = 400;
        public const int TIME_WARNING = 100;

        public static Sprint4 Instance { get; private set; }

        public static readonly bool debug = false;

        private const string LEVEL = "Data/Levels/Sprint4Level.txt";

        private Rectangle viewport { get; set; }
        public int checkpoint { get; set; }
        private int numberOfCheckpoints { get; set; }

        public Song backgroundSong { get; private set; }
        public Song invincibleSong { get; private set; }
        public Song spedBackgroundSong { get; private set; }
        public Rectangle bounds { get; set;  }
        private GraphicsDeviceManager graphics { get; set; }
        private SpriteBatch spriteBatch { get; set; }
        public PointTextGenerator pointGenerator { get; set; }

        public int CameraX
        {
            get; set;
        }

        public int CameraY
        {
            get; set;
        }

        public List<SpriteLayer> Layers { get; private set; }
        public SpriteMario Mario { get; private set; }
        public SpriteFlag flag { get; set; }
        public int flagposition { get; set; }
        public int TimeLeft { get; set; }
        private double currentTime { get; set; }

        public Action RunAfterUpdate { get; set; }

        public Sprint4()
        {
            Instance = this;
            Content.RootDirectory = "Content";
            graphics = new GraphicsDeviceManager(this);


            if (!debug)
            {
                Console.SetOut(TextWriter.Null);
                Console.SetError(TextWriter.Null);
            }
        }

        protected override void Initialize()
        {
            base.Initialize();

            this.CameraX = 0;
            this.CameraY = 0;
            checkpoint = 0;
            numberOfCheckpoints = 3;
            this.TimeLeft = TIME_LIMIT;
        }

        protected override void LoadContent()
        {
            currentTime = 0;
            spriteBatch = new SpriteBatch(GraphicsDevice);

            SpriteLayer.CollisionLayer = new CollisionLayer(this);
            SpriteLayer.GameOverLayer = new GameOverLayer(this);
            SpriteLayer.YouWinLayer = new YouWinLayer(this);
            SpriteLayer.HuDLayer = new HuDLayer(this);

            this.Layers = new List<SpriteLayer>();

            //Ideally, the layers are drawn in this order
            this.Layers.Add(SpriteLayer.BackgroundLayer);
            this.Layers.Add(SpriteLayer.CollisionLayer);
            this.Layers.Add(SpriteLayer.GameOverLayer);
            this.Layers.Add(SpriteLayer.YouWinLayer);
            this.Layers.Add(SpriteLayer.HuDLayer);

            LevelLoader.LoadLevel(this, LEVEL);

            SpriteLayer.CollisionLayer.AddSprite(this.Mario = SpriteFactory.CreateMario(this));

            this.backgroundSong = Content.Load<Song>("Music/background");
            this.invincibleSong = Content.Load<Song>("Music/invincible");
            this.spedBackgroundSong = Content.Load<Song>("Music/spedbackground");
            MediaPlayer.Play(this.backgroundSong);
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = 0.3f;

            pointGenerator = new PointTextGenerator(this);

        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            foreach (SpriteLayer layer in this.Layers)
            {
                layer.Update(gameTime);
            }

            pointGenerator.Update(gameTime);

            CollisionHandler.ScanCollisions();

            float vy = this.Mario.Info.velocity.Y;
            if (vy > 1)
            {
                this.Mario.StateMachineAction.CurrentState.ToFall();
            }

            if (this.Mario.Info.position.X > checkpoint + (this.bounds.Width / numberOfCheckpoints))
            {
                checkpoint += this.bounds.Width / numberOfCheckpoints;
            }

            base.Update(gameTime);

            if (this.RunAfterUpdate != null)
            {
                this.RunAfterUpdate();
                this.RunAfterUpdate = null;
            }

            if (this.TimeLeft != 0 && this.Mario.SpriteState != SpriteStates.Sprites.DEAD && !this.Mario.won)
            {
                if (!this.Mario.won)
                {
                    //Console.WriteLine("Mario state: {0}, (dead={1})", this.Mario.SpriteState, SpriteStates.Sprites.DEAD);
                    if (!Layers.TrueForAll(new Predicate<SpriteLayer>(LayersPaused)))
                    {
                        currentTime += gameTime.ElapsedGameTime.TotalSeconds;
                        if (this.currentTime >= 1)
                        {
                            this.currentTime -= 1;
                            this.TimeLeft -= 1;
                            if (this.TimeLeft == TIME_WARNING)
                            {
                                SoundFactory.PlaySoundEffect(SoundFactory.TimeEnding());
                                MediaPlayer.Stop();
                                MediaPlayer.Play(spedBackgroundSong);
                                MediaPlayer.IsRepeating = true;
                            }
                        }
                    }
                }
            }
            else if (this.Mario.won)
            {

            }
            else
            {
                this.Mario.StateMachineAction.CurrentState.ToDead();
            }
            
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            foreach (SpriteLayer layer in this.Layers)
            {
                this.spriteBatch.Begin(layer.SortMode, layer.BlendMode, layer.SamplerState, null, null);
                layer.Draw(this.spriteBatch);
                this.spriteBatch.End();
            }

            base.Draw(gameTime);
        }

        public Rectangle GetViewport()
        {
            if (viewport == default(Rectangle))
            {
                viewport = new Rectangle(
                    graphics.GraphicsDevice.Viewport.X,
                    graphics.GraphicsDevice.Viewport.Y,
                    graphics.GraphicsDevice.Viewport.Width,
                    graphics.GraphicsDevice.Viewport.Height);
            }
            return viewport;
        }

        private bool ClearSprite(SpriteCollection sprite)
        {
            if (sprite != this.Mario)
            {
                return true;
            }
            return false;
        }

        public bool LayersPaused(SpriteLayer layer)
        {
            if (layer.Paused)
            {
                return true;
            }
            return false;
        }

        public static void Win()
        {
            Console.WriteLine("We won!");
            MediaPlayer.Stop();

            SoundFactory.PlaySoundEffect(SoundFactory.YouWin());

            Task.Factory.StartNew(delegate ()
            {
                Thread.Sleep(7200);
                SpriteLayer.YouWinLayer.AddSprite(SpriteLayer.YouWinLayer.youWin);
                int dy = 250;
                SpriteLayer.YouWinLayer.youWin.Info.bounds = new Rectangle(SpriteLayer.YouWinLayer.youWin.Info.x - 2, SpriteLayer.YouWinLayer.youWin.Info.y - dy, SpriteLayer.YouWinLayer.youWin.Info.spriteWidth + 2, SpriteLayer.YouWinLayer.youWin.Info.spriteHeight + dy);
                SpriteLayer.YouWinLayer.youWin.SetPosition(SpriteLayer.YouWinLayer.youWin.Info.x, SpriteLayer.YouWinLayer.youWin.Info.y - dy);
                SpriteLayer.YouWinLayer.youWin.Info.acceleration.Y = 0.15f;

                foreach (InputController ic in SpriteLayer.CollisionLayer.InputControllers)
                {
                    ic.Paused = true;
                }
            });
        }

        public void Reset()
        {
            lock (SpriteLayer.CollisionLayer)
            {
                SpriteLayer.CollisionLayer.Sprites.RemoveAll(ClearSprite);
            }

            //remove the coin and mario sprites
            lock (SpriteLayer.HuDLayer)
            {
                SpriteLayer.HuDLayer.Sprites.RemoveAll(ClearSprite);
            }

            lock (SpriteLayer.HuDLayer)
            {
                SpriteLayer.HuDLayer.Sprites.Clear();
            }

            lock (SpriteLayer.YouWinLayer)
            {
                SpriteLayer.YouWinLayer.Sprites.Clear();
            }

            CollisionHandler.dynamics.RemoveAll(new Predicate<SpriteCollection>(ClearSprite));
            CollisionHandler.statics.RemoveAll(new Predicate<SpriteCollection>(ClearSprite));
            //Camera must be set to 0 when loading level
            this.CameraX = 0;
            this.CameraY = 0;
            LevelLoader.LoadLevel(this, LEVEL);

            this.Mario.won = false;
            this.Mario.SetPosition(this.checkpoint, this.Mario.GetStartHeight());
            if (this.checkpoint >= this.bounds.Width/numberOfCheckpoints)
            {
                this.CameraX = this.checkpoint - GraphicsDevice.Viewport.Width / 2;
            }
            else
            {
                this.CameraX = (int)this.checkpoint;
            }


            this.Mario.SetSheetState(SpriteStates.Sheets.NORMAL);
            this.Mario.StateMachineAction.Idle.Enter(null);

            this.Mario.StateMachinePowerup.Normal.Enter(null);

            MediaPlayer.Stop();
            MediaPlayer.Play(backgroundSong);

            foreach (SpriteLayer layer in this.Layers)
            {
                layer.Paused = false;
                foreach (InputController ic in layer.InputControllers)
                {
                    ic.Paused = false;
                }
            }

            this.TimeLeft = TIME_LIMIT;
        }
    }
}
