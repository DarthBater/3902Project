using Baritone.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Baritone.Wrappers;

namespace Baritone.Wrappers
{
    public class SpriteInfo
    {

        private Sprint4 game;

        //Position fields
        public bool manual = false;
        public Vector2 position = new Vector2(0, 0);
        public Vector2 velocity = new Vector2(0, 0);
        public Vector2 acceleration = new Vector2(0, 0);

        public int x
        {
            get
            {
                return (int) (this.position.X - (this.game.CameraX * this.paralaxLayer));
            }
        }

        public int y
        {
            get
            {
                return (int) this.position.Y;
            }
        }

        public bool bounce = false;

        //Drawing information
        private int sw, sh;
        public int spriteWidth
        {
            get
            {
                return (int) (sw * this.scale);
            }
            set
            {
                sw = value;
            }
        }

        public int spriteHeight
        {
            get
            {
                return (int) (sh * this.scale);
            }
            set
            {
                sh = value;
            }
        }

        public float layer = 1f;
        public float scale = 2f;

        //Animation fields
        public int numFrames; //How many frames are in this spritesheet. Negligible for a non-animated sprite

        private int cf;
        public int currentFrame
        {
            set
            {
                cf = value;
            }
            get
            {
                return cf;
            }
        }
        //Index of the current frame to draw
        public int frameDelay = 3; //The number of frames a sprite should be drawn for before moving to the next frame
        public int framesSinceUpdate = 0; //Used in Sprites class to update animation
        public Rectangle bounds; //Binding information for where this sprite can move.

        //Collision fields
        public int harmfulIndex = 0;
        public Rectangle hitbox = default(Rectangle);
        public Color collisionColor = Color.Green;
        public float paralaxLayer = 1; //1 is Collidable, Must be 0-1
        public bool extendHorizontal = false;

        public SpriteInfo(Sprint4 game)
        {
            this.game = game;
            this.spriteWidth = 16;
        }

        public SpriteInfo(Sprint4 game, Texture2D spriteSheet) : this(game)
        {
            this.spriteHeight = spriteSheet.Height;
        }

        public SpriteEffects spriteEffects = SpriteEffects.None;

    }
}
