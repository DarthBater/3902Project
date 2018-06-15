using Baritone.Utils;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Baritone.Wrappers;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace Baritone.Sprites
{
    public abstract class SpriteBlock : SpriteCollection, ICollidable
    {

        private SpriteCollection i;
        public SpriteCollection item
        {
            set
            {
                if (value is IRevealable)
                {
                    Console.WriteLine("Setting {0} position to {1}", value.name, this.Info.position);
                    i = value;
                    i.Info.position = this.Info.position;
                }
                else if (value != null)
                {
                    Console.WriteLine("[Error] SetItem called but the given SpriteCollection does not implement IRevealable.");
                }
            }
            get {
                return i;
            }
        }
        public int itemCount
        {
            get; set;
        }
        public bool Bumping {
            get; protected set;
        }

        protected SpriteBlock(Sprint4 game) : base(game)
        {
            this.Info.harmfulIndex = 0;
            this.Bumping = false;
        }

        public override bool isStatic()
        {
            return true;
        }

        public void Bump()
        {
            if (this.SheetState == SpriteStates.Sheets.USED)
            {
                return;
            }
            if (!this.Bumping)
            {
               Console.WriteLine("Bumping the block.");
                this.Bumping = true;

                SoundFactory.PlaySoundEffect(SoundFactory.BlockBump());

                if (this.SheetState == SpriteStates.Sheets.HIDDEN)
                {
                    this.SetSheetState(SpriteStates.Sheets.QUESTION);
                }

                new Thread(new ThreadStart(delegate ()
                {
                    Console.WriteLine("Starting bump, current spriteState: {0}", this.SpriteState);
                    int startY = this.Info.y;
                    this.Info.velocity.Y = -1;
                    Thread.Sleep(100);
                    this.Info.velocity.Y = 1;
                    Thread.Sleep(100);
                    this.Info.velocity.Y = 0;
                    this.Info.velocity.X = 0;
                    this.Bumping = false;
                    this.Info.position.Y = startY;
                })).Start();
            }
            else
            {
                Console.WriteLine("Bump is busy!");
            }
        }

        public abstract void Handle();

        public void RevealItem()
        {
            if (this.item != null && this.SheetState != SpriteStates.Sheets.USED)
            {
                this.SetSpriteState(SpriteStates.Sprites.IDLE);

                if (!(this.item is SpriteCoin))
                {
                    SoundFactory.PlaySoundEffect(SoundFactory.PowerUpAppear());
                }

                Thread t = new Thread(new ThreadStart(
                    delegate ()
                    {
                        this.item.Info.position = this.Info.position;
                        this.itemCount--;
                        (this.item as IRevealable).Reveal();
                        if (this.itemCount <= 0)
                        {
                            this.item = null;
                            this.SetSheetState(SpriteStates.Sheets.USED);
                        }

                        Console.WriteLine("Reveal finished");
                    }
                ));
                t.IsBackground = true;
                t.Start();
            }
        }

        public virtual void OnCollision(Direction direction, SpriteCollection other, Rectangle intersection)
        {
            if (other.SpriteState != SpriteStates.Sprites.DEAD)
            {
                if (this.SheetState != SpriteStates.Sheets.BROKEN)
                {
                    CollisionHandler.HandleStop(direction, other, this);
                    if (direction == Direction.BOTTOM && other is SpriteMario)
                    {
                        this.Handle();
                    }
                }

                if (other is SpriteStar && direction == Direction.TOP)
                {
                    other.Info.velocity.Y = -4;
                }
            }
        }
    }
}
