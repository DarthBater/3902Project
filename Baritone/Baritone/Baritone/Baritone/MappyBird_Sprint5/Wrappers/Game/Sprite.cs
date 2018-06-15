using Baritone.MappyBird_Sprint5.Utils;
using Baritone.MappyBird_Sprint5.Wrappers.Game.Levels.One;
using Baritone.MappyBird_Sprint5.Wrappers.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baritone.MappyBird_Sprint5.Wrappers.Game
{
    public abstract class Sprite : IDrawable
    {

        public Layer Layer { get; private set; }
        public Level Level { get { return this.Layer.Level; } }
        public Sprint5 Game { get { return this.Level.Game; } }

        #region Animation fields
        protected int NumFrames { get; private set; }
        protected int Frame { get; private set; }
        private int millisPerFrame;
        private double remainingMillisForFrame;
        #endregion Animation fields

        #region Physics fields
        private Vector2 position; //position is a Vector2, but the values need to be floored before drawing
        public Vector2 Position { get { return this.position; } set { this.position = value; } }

        private Vector2 velocity;
        public Vector2 Velocity { get { return this.velocity; } set { this.velocity = value; } }

        private Vector2 acceleration;
        public Vector2 Acceleration { get { return this.acceleration; } set { this.acceleration = value; } }

        public int Width { get; private set; }
        public int Height { get; private set; }

        #endregion Physics fields

        #region Collision fields
        public virtual bool IsStatic { get { return true; } }

        #endregion Collision fields

        #region Drawing fields
        protected SpriteMap SpriteMap { get; private set; }
        private Texture2D current;

        public float Scale { get; set; }
        public float Rotation { get; protected set; }

        protected SpriteSheet SpriteSheet { get; private set; }
        protected SpriteState SpriteState { get; private set; }
        public SpriteEffects Effects { get; set; }
        #endregion Drawing fields

        protected Sprite(Layer layer, int numFrames, int millisPerFrame)
        {
            this.Layer = layer;
            this.NumFrames = numFrames;
            this.Frame = 0;
            this.remainingMillisForFrame = (this.millisPerFrame = millisPerFrame);

            this.SpriteMap = new SpriteMap();

            this.position = Vector2.Zero;
            this.velocity = Vector2.Zero;
            this.acceleration = Vector2.Zero;
            this.Rotation = 0f;
            this.Scale = 2f;

            this.Effects = SpriteEffects.None;
        }

        public void SetSpriteSheet(SpriteSheet spriteSheet)
        {
            Texture2D expected = this.SpriteMap.GetSprite(spriteSheet, this.SpriteState);
            this.SpriteSheet = spriteSheet;
            if (expected != null)
            {
                if (this.current != null)
                {
                    int previousHeight = this.current.Height;
                    int newHeight = expected.Height;
                    this.position.Y -= (newHeight - previousHeight);
                }

                this.current = expected;
                this.Width = this.current.Width;
                this.Height = this.current.Height;
            }
        }

        public void SetSpriteState(SpriteState spriteState)
        {
            Texture2D expected = this.SpriteMap.GetSprite(this.SpriteSheet, spriteState);
            this.SpriteState = spriteState;
            if (expected != null)
            {
                if (this.current != null)
                {
                    int previousHeight = this.current.Height;
                    int newHeight = expected.Height;
                    this.position.Y -= (newHeight - previousHeight);
                }
                this.current = expected;
                this.Width = this.current.Width;
                this.Height = this.current.Height;
            }
        }

        public virtual void Update(GameTime gameTime)
        {
            #region Manage Animation
            this.remainingMillisForFrame -= gameTime.ElapsedGameTime.TotalMilliseconds;
            if (this.remainingMillisForFrame <= 0)
            {
                if (++Frame >= NumFrames)
                {
                    Frame = 0;
                }
                this.remainingMillisForFrame = this.millisPerFrame;
            }
            #endregion Manage Animation

            #region Manage Position
            this.velocity += this.acceleration;
            this.position += this.velocity;
            #endregion Manage Position
        }

        public virtual void Draw(SpriteBatch spriteBatch, float paralax)
        {
            Vector2 pos = new Vector2(
                (this.position.X + this.Width) - (this.Level.Camera.X * paralax), 
                this.position.Y + this.Height);

            Rectangle source = new Rectangle(
                (this.Frame * (this.Width / this.NumFrames)),
                0, 
                this.Width / this.NumFrames, 
                this.Height);

            spriteBatch.Draw(
                this.SpriteMap.GetSprite(this.SpriteSheet, this.SpriteState),
                pos,
                source,
                Color.White,
                this.Rotation,

                //Vector2.Zero,
                new Vector2(this.Width / 2, this.Height / 2),

                this.Scale,
                this.Effects,
                0f
                );

            /*if (this is ICollidable)
            {
                Rectangle hitbox = SpriteUtils.GetHitbox(this);
                Texture2D t = new Texture2D(this.Game.GraphicsDevice, hitbox.Width, hitbox.Height);
                Color[] data = new Color[hitbox.Width * hitbox.Height];
                for (int i = 0; i < data.Length; i++)
                {
                    data[i] = Color.Blue;
                }
                t.SetData(data);

                spriteBatch.Draw(t,
                    new Rectangle(hitbox.Left - this.Level.Camera.X, hitbox.Top, hitbox.Width, hitbox.Height),
                    new Rectangle(hitbox.Left, hitbox.Top, hitbox.Width, hitbox.Height),
                    Color.White,
                    0f,
                    new Vector2(0, 0),
                    SpriteEffects.None,
                    0f);//
            }*/
        }

        public void CheckForRemoval(object sender, EventArgs eventArgs)
        {
            int cameraX = this.Level.Camera.X;
            int maxX = (int) ((this.Position.X + (this.Width * this.Scale)) / this.Layer.Paralax);
            if (maxX < cameraX)
            {
                Console.WriteLine("Removing a cloud");
                Task.Factory.StartNew(() => this.Layer.RemoveSprite(this));
                if (sender is SpriteMappy)
                {
                    (sender as SpriteMappy).OnMove -= this.CheckForRemoval;
                }
            }
        }

        public void CheckForPassing(object sender, EventArgs eventArgs)
        {
            int mappyX = (int) (this.Level as DynamicLevel).Mappy.Position.X;
            int pipeX = (int) (int)((this.Position.X + (this.Width * this.Scale) / 2));

            if (mappyX > pipeX)
            {
                Console.WriteLine("Pass Pipe");
                SoundHandler.PlaySoundEffect((this.Level as DynamicLevel).L1SoundHandler.Pipe);
                this.Level.Score++;
                if (sender is SpriteMappy)
                {
                    (sender as SpriteMappy).OnMove -= this.CheckForPassing;
                    (sender as SpriteMappy).Velocity = new Vector2((sender as SpriteMappy).Velocity.X + 0.1f, (sender as SpriteMappy).Velocity.Y);
                    SpritePipe.Speed += 0.03f;
                    foreach (Sprite sprite in (this.Level as DynamicLevel).CollisionLayer.Sprites)
                    {
                        if (sprite is SpritePipe)
                        {
                            sprite.velocity = new Vector2(0, SpritePipe.Speed);
                        }
                    }
                }
            }
        }

        protected void SetDefault(string path)
        {
            this.SpriteMap.AddSprite(SpriteSheet.NORMAL, SpriteState.IDLE, SpriteFactory.LoadTexture(this.Game, path));
            this.SetSpriteSheet(SpriteSheet.NORMAL);
            this.SetSpriteState(SpriteState.IDLE);
        }
    }
}
