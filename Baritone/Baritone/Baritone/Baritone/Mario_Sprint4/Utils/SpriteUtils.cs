using Baritone.Sprites;
using Baritone.Wrappers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Baritone.Utils
{
    public static class SpriteUtils
    {
        //Make a call to this from the overridden Game.Update function
        //for each sprite.
        public static void UpdateAnimation(SpriteInfo info)
        {
            if (info != null)
            {
                if (info.framesSinceUpdate >= info.frameDelay)
                {
                    info.currentFrame++;
                    if (info.currentFrame >= info.numFrames)
                    {
                        info.currentFrame = 0;
                    }
                    info.framesSinceUpdate = 0;
                }
                info.framesSinceUpdate++;
            }
        }

        public static void MoveSprite(SpriteCollection sprite)
        {
            SpriteInfo info = sprite.Info;
            if (info != null && (!info.manual || sprite is IRevealable))
            {

                if (info.bounds != default(Rectangle))
                {

                    if (info.position.X + info.velocity.X + info.spriteWidth >= info.bounds.Right || info.position.X + info.velocity.X <= info.bounds.Left)
                    {
                        if (info.bounce)
                        {
                            info.velocity.X *= -1;
                            Console.WriteLine("Bouncing, changing spriteEffects");
                            info.spriteEffects = info.spriteEffects == SpriteEffects.None ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
                        }
                        else
                        {
                            //info.position.X -= info.velocity.X;
                        }
                    }
                    if (info.position.Y + info.velocity.Y + info.spriteHeight >= info.bounds.Bottom || info.position.Y + info.velocity.Y <= info.bounds.Top)
                    {
                        if (info.bounce)
                        {
                            info.velocity.Y *= -1;
                        }
                        else
                        {
                            //info.position.Y -= info.velocity.Y;
                        }
                    }
                }


                info.velocity += info.acceleration;

                info.velocity.X = MathHelper.Clamp(info.velocity.X, SpriteCollection.MIN_VELOCITY, SpriteCollection.MAX_VELOCITY);
                //info.velocity.Y = MathHelper.Clamp(info.velocity.Y, -10, 5);

                float vx = info.velocity.X, vy = info.velocity.Y;

                if (vy <= -1 || vy >= 1)
                {
                    info.position.Y += vy;
                }
                info.position.X += vx;

                if (info.position.X < info.bounds.Left)
                {
                    info.position.X = info.bounds.Left;
                    if (info.bounce)
                    {
                        info.velocity.X = -info.velocity.X;
                    }
                }

                if (info.position.X + info.spriteWidth > info.bounds.Right)
                {
                    info.position.X = info.bounds.Right - info.spriteWidth;
                    if (info.bounce)
                    {
                        info.velocity.X = -info.velocity.X;
                    }
                }

                if (info.position.Y < info.bounds.Top)
                {
                    info.position.Y = info.bounds.Top;
                    info.velocity.Y = 0;
                }

                if (info.position.Y + info.spriteHeight > info.bounds.Bottom)
                {
                    info.position.Y = info.bounds.Bottom - info.spriteHeight;
                    info.velocity.Y = 0;
                }

                if (sprite is ITrackable)
                {
                    //Technically this calls whether we're moving or not
                    //Add a info.velocity.X > 0 || Y > 0 if you want a call when the sprite actually moves
                    (sprite as ITrackable).OnMove(info.x, info.y);
                    
                    if (info.velocity.X != 0 || info.velocity.Y != 0)
                    {
                        sprite.Info.hitbox = default(Rectangle); //Force collision to re-calculate the hitbox.
                    }
                }

            }
        }

        public static Task<bool> RevealItem(Sprint4 game, SpriteCollection item)
        {
            return Task.Factory.StartNew(() => revealItem(game, item));
        }

        private static bool revealItem(Sprint4 game, SpriteCollection item)
        {
            if (item is IRevealable)
            {
                int destY = (((int)(item.Info.position.Y)) - item.Info.spriteHeight) - 1;
                if (item.Info.bounds == null || item.Info.bounds == default(Rectangle))
                {
                    item.Info.bounds = game.bounds;
                }
                item.Info.manual = true;
                item.Info.velocity.Y = -1;
                SpriteLayer.CollisionLayer.AddSprite(item);
                while (item.Info.position.Y > destY)
                {
                    Console.WriteLine("Waiting for {0} to be revealed. Its velocity is {1}", item.name, item.Info.velocity.Y);
                    Thread.Sleep(200);
                }
                Console.WriteLine("{0} has been revealed", item.name);
                item.Info.velocity.Y = 0;
                item.Info.manual = false;
                return true;
            }
            return false;
        }

        /*
        * @harmfulIndex - 1 if harmful, 0 if neutral, -1 if helpful

          The hitbox is assuming the next frame of motion, so it adds the velocity to the location
        */
        public static Rectangle GetHitbox(SpriteCollection sprite)
        {
            //if (sprite.Info.hitbox != default(Rectangle))
            {
                //return sprite.Info.hitbox;
            }
            Rectangle spriteBounds = new Rectangle((int) (sprite.Info.position.X + sprite.Info.velocity.X), (int) (sprite.Info.position.Y + sprite.Info.velocity.Y), sprite.Info.spriteWidth, sprite.Info.spriteHeight);

            double offX = 0, offY = 0;

            if (sprite.Info.harmfulIndex > 0)
            {
                //Harmful, shrink the box by 10%
                offX = -(spriteBounds.Width * 0.1);
                offY = -(spriteBounds.Height * 0.1);
            }
            else if (sprite.Info.harmfulIndex < 0)
            {
                offX = spriteBounds.Width * 0.1;
                offY = spriteBounds.Height * 0.1;
            }

            spriteBounds.X -= (int)offX;
            spriteBounds.Width += (int)(offX * 2);

            spriteBounds.Y -= (int)offY;
            spriteBounds.Height += (int)(offY * 2);

            return sprite.Info.hitbox = spriteBounds;
        }

        //Determines all of the grids that this sprite occupies.
        public static List<int[]> DetermineGrids(SpriteCollection sprite)
        {
            List<int[]> grids = new List<int[]>();

            int x1 = (int)sprite.Info.position.X;
            int y1 = (int)sprite.Info.position.Y;
            int w = sprite.Info.spriteWidth;
            int h = sprite.Info.spriteHeight;

            Point tl = new Point((int)Math.Floor(x1 / 32D), (int)Math.Floor(y1 / 32D));
            Point tr = new Point((int)Math.Floor((x1 + w) / 32D), (int)Math.Floor(y1 / 32D));
            Point bl = new Point((int)Math.Floor(x1 / 32D), (int)Math.Floor((y1 + h) / 32D));
            Point br = new Point((int)Math.Floor((x1 + w) / 32D), (int)Math.Floor((y1 + h) / 32D));

            foreach (Point p in new Point[] { tl, tr, bl, br })
            {
                grids.Add(new int[] { p.X, p.Y });
            }

            return grids;
        }

        //Determines all grids that are adjacent to this sprite, including all of the grids
        //that this sprite currently occupies.
        public static List<int[]> GetNearbyGrids(SpriteCollection sprite)
        {
            List<int[]> spriteGrids = DetermineGrids(sprite);

            int rows = CollisionHandler.grid.GetLength(0);
            int columns = CollisionHandler.grid.GetLength(1);
            int[] maxes = { rows, columns };

            List<int[]> adjacent;
            List<int[]> nearby = new List<int[]>();
            bool found;

            foreach (int[] grid in spriteGrids)
            {
                adjacent = findGrids(grid, maxes);

                found = false;
                foreach (int[] n in adjacent)
                {
                    //Check to see if we already added this grid.
                    foreach (int[] a in nearby)
                    {
                        if (a[0] == n[0] && a[1] == n[1])
                        {
                            found = true;
                        }
                    }

                    if (!found)
                    {
                        nearby.Add(n);
                    }
                }
            }

            return nearby;
        }

        //Given a grid, finds all adjacent grids that are within the game bounds
        private static List<int[]> findGrids(int[] grid, int[] maxes)
        {
            int x = grid[0], y = grid[1];
            int rows = maxes[0], columns = maxes[1];

            List<int[]> grids = new List<int[]>();

            grids.Add(new int[] { x, y });

            if (x > 0)
            {
                grids.Add(new int[] { x - 1, y });
                if (y > 0)
                {
                    grids.Add(new int[] { x - 1, y - 1 });
                }
                if (y < columns)
                {
                    grids.Add(new int[] { x - 1, y + 1 });
                }
            }

            if (x < rows)
            {
                grids.Add(new int[] { x + 1, y });
                if (y > 0)
                {
                    grids.Add(new int[] { x + 1, y - 1 });
                }
                if (y < columns)
                {
                    grids.Add(new int[] { x + 1, y + 1 });
                }
            }

            if (y > 0)
            {
                grids.Add(new int[] { x, y - 1 });
            }

            if (y < columns)
            {
                grids.Add(new int[] { x, y + 1 });
            }

            return grids;
        }

        public static void SetDefaultProperties(SpriteCollection sc, string path)
        {
            sc.RegisterState(SpriteStates.Sheets.NORMAL, SpriteStates.Sprites.IDLE, path);
            sc.SetSheetState(SpriteStates.Sheets.NORMAL);
            sc.SetSpriteState(SpriteStates.Sprites.IDLE);
        }

    }

}
