using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Baritone.Utils;
using Baritone.Sprites;

namespace Baritone.Wrappers
{
    public abstract class SpriteCollection : IDrawable
    {

        public static readonly int MIN_VELOCITY = -4;
        public static readonly int MAX_VELOCITY = 4;

        protected Dictionary<int, Dictionary<int, Texture2D>> stateMap = new Dictionary<int, Dictionary<int, Texture2D>>();
        protected Texture2D current { get; set; }
        private Texture2D hitboxT { get; set; }

        public SpriteInfo Info { get; protected set; }
        public string name { get; set; }

        public Sprint4 game
        {
            get; protected set;
        }
        public int SheetState { get; private set; }
        public int SpriteState { get; private set; }

        protected SpriteCollection(Sprint4 game)
        {
            name = this.GetType().Name;
            this.game = game;
            this.Info = new SpriteInfo(game);
            this.Info.position.X = 50;
            this.Info.position.Y = 200;
        }

        public void RegisterState(int sheetState, int spriteState, string texture)
        {
            if (!this.stateMap.ContainsKey(sheetState))
            {
                this.stateMap.Add(sheetState, new Dictionary<int, Texture2D>());
            }

            //Console.WriteLine("RegisterState for {0}: {1}, {2}, {3}", this.name, sheetState, spriteState, texture);
            stateMap[sheetState].Add(spriteState, SpriteFactory.loadTexture(this.game, texture));
        }

        public void SetSheetState(int sheetState)
        {
            this.SheetState = sheetState;
            if (this.stateMap.ContainsKey(sheetState))
            {
                if (this.stateMap[sheetState].ContainsKey(this.SpriteState))
                {
                    this.current = this.stateMap[this.SheetState][this.SpriteState];
                    this.Info.currentFrame = 0;
                    this.Info.numFrames = this.current.Width / (this.Info.spriteWidth / 2);
                    this.Info.spriteHeight = this.current.Height;
                }
            }
        }

        public void SetSpriteState(int spriteState)
        {
            this.SpriteState = spriteState;
            
            if (this.stateMap.ContainsKey(this.SheetState) && this.stateMap[this.SheetState].ContainsKey(spriteState))
            {
                //Console.WriteLine("Setting {0} to SpriteState={1}", this.name, spriteState);
                this.current = this.stateMap[this.SheetState][spriteState];
                this.Info.currentFrame = 0;
                this.Info.numFrames = this.current.Width / (this.Info.spriteWidth / 2);
                this.Info.spriteHeight = this.current.Height;
            }
            else
            {
                Console.WriteLine("[Error] stateMap does not contain {0}, {1}, {2}", this.SheetState, spriteState, this.name);
            }
        }

        public virtual int determineSpriteState()
        {
            return SpriteStates.Sprites.IDLE; //any class where 1 should not be the default state needs to override
        }

        public abstract bool isStatic();

        public void SetPosition(int x, int y)
        {
            this.Info.position.X = x;
            this.Info.position.Y = y;
        }

        public virtual void Update(GameTime gameTime)
        {
            if (this.current != null)
            {
                SpriteUtils.UpdateAnimation(this.Info);
                SpriteUtils.MoveSprite(this);
            }
            else
            {
                Console.WriteLine("[Animate] current is null");
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (this.current != null)
            {
                Vector2 position = new Vector2(this.Info.x, this.Info.y);
                Rectangle source = new Rectangle(this.Info.currentFrame * this.Info.spriteWidth / 2, 0, this.Info.spriteWidth / 2, this.Info.spriteHeight / 2);

                spriteBatch.Draw(
                    this.current,
                    position,
                    source,
                    Color.White,
                    0f,
                    new Vector2(0, 0),
                    this.Info.scale,
                    this.Info.spriteEffects,
                    this.Info.layer);

                if (this is ICollidable)
                {
                    Rectangle hitbox = SpriteUtils.GetHitbox(this);
                    this.hitboxT = new Texture2D(this.game.GraphicsDevice, hitbox.Width, hitbox.Height);
                    Color[] data = new Color[hitbox.Width * hitbox.Height];
                    for (int i = 0; i < data.Length; i++)
                    {
                        data[i] = this.Info.collisionColor;
                    }
                    this.hitboxT.SetData(data);

                    /*spriteBatch.Draw(hitboxT,
                        new Rectangle(hitbox.Left - this.game.CameraX, hitbox.Top, hitbox.Width, hitbox.Height),
                        new Rectangle(hitbox.Left, hitbox.Top, hitbox.Width, hitbox.Height),
                        Color.White,
                        0f,
                        new Vector2(0, 0),
                        SpriteEffects.None,
                        0f);//*/
                }

                
            }
            else
            {
                Console.WriteLine("[Draw] current is null");
            }
        }
    }
}
