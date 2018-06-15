using Baritone.Sprites;
using Baritone.States.Mario;
using Baritone.Wrappers;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Baritone.Utils
{
    public class CollisionHandler
    {

        public static readonly List<SpriteCollection> LastScan = new List<SpriteCollection>();

        public static readonly List<SpriteCollection> statics = new List<SpriteCollection>();
        public static readonly List<SpriteCollection> dynamics = new List<SpriteCollection>();
        //grid is a 2D array of 32x32 sections. Each section is a list of Statics
        public static List<SpriteCollection>[,] grid;

        public static void ScanCollisions()
        {

            List<Tuple<SpriteCollection, SpriteCollection>> matches = FindMatches();
            List<SpriteCollection> handled = new List<SpriteCollection>();

            foreach (var match in matches)
            {
                Rectangle hb1 = SpriteUtils.GetHitbox(match.Item1);
                Rectangle hb2 = SpriteUtils.GetHitbox(match.Item2);

                if (hb1.Intersects(hb2))
                {
                    Rectangle intersection = Rectangle.Intersect(hb1, hb2);

                    //Send the callbacks for each sprite
                    if (intersection.Right == hb1.Right 
                        && intersection.Height > intersection.Width 
                        && (match.Item1.Info.velocity.X > 0 || match.Item2.Info.velocity.X < 0)) //1 is seeing a collision on the right
                    {

                        handled.Add(match.Item1);
                        handled.Add(match.Item2);
                        (match.Item1 as ICollidable).OnCollision(Direction.RIGHT, match.Item2, intersection);
                        (match.Item2 as ICollidable).OnCollision(Direction.LEFT, match.Item1, intersection);
                    }
                    else if (intersection.Top == hb1.Top 
                        && intersection.Width > intersection.Height
                        && (match.Item1.Info.velocity.Y < 0 || match.Item2.Info.velocity.Y > 0)) //1 is seeing a collision on the top
                    {

                        handled.Add(match.Item1);
                        handled.Add(match.Item2);
                        (match.Item1 as ICollidable).OnCollision(Direction.TOP, match.Item2, intersection);
                        (match.Item2 as ICollidable).OnCollision(Direction.BOTTOM, match.Item1, intersection);
                    }
                    else if (intersection.Bottom == hb1.Bottom 
                        && intersection.Width > intersection.Height
                        && (match.Item1.Info.velocity.Y > 0 || match.Item2.Info.velocity.Y < 0)) //1 is seeing a collision on the bottom
                    {

                        handled.Add(match.Item1);
                        handled.Add(match.Item2);
                        (match.Item1 as ICollidable).OnCollision(Direction.BOTTOM, match.Item2, intersection);
                        (match.Item2 as ICollidable).OnCollision(Direction.TOP, match.Item1, intersection);
                    }
                    else if (intersection.Left == hb1.Left 
                        && intersection.Height > intersection.Width
                        && (match.Item1.Info.velocity.X < 0 || match.Item2.Info.velocity.X > 0)) //1 is seeing a collision on the left
                    {

                        handled.Add(match.Item1);
                        handled.Add(match.Item2);
                        (match.Item1 as ICollidable).OnCollision(Direction.LEFT, match.Item2, intersection);
                        (match.Item2 as ICollidable).OnCollision(Direction.RIGHT, match.Item1, intersection);
                    }
                }
            }

            LastScan.Clear();
            LastScan.AddRange(handled);
        }


        //We're comparing every dynamic to each other since there are so few.
        //We're comparing every dynamic to nearby statics, this number should also be small
        private static List<Tuple<SpriteCollection, SpriteCollection>> FindMatches()
        {
            List<Tuple<SpriteCollection, SpriteCollection>> matches = new List<Tuple<SpriteCollection, SpriteCollection>>();

            lock (SpriteLayer.CollisionLayer)
            {

                //Compare all dynamics
                SpriteCollection current, temp;
                Rectangle hb1, hb2;
                for (int i = 0; i < dynamics.Count; i++)
                {
                    current = dynamics[i];
                    if (current is ICollidable && !current.Info.manual)
                    {
                        hb1 = SpriteUtils.GetHitbox(current);
                        for (int j = i + 1; j < dynamics.Count; j++)
                        {
                            temp = dynamics[j];
                            if (temp is ICollidable)
                            {
                                hb2 = SpriteUtils.GetHitbox(temp);
                                if (hb1.Intersects(hb2))
                                {
                                    matches.Add(new Tuple<SpriteCollection, SpriteCollection>(current, temp));
                                }
                            }
                        }
                    }

                }

                List<int[]> gridsToCheck;
                foreach (SpriteCollection dynamic in dynamics)
                {
                    if (dynamic is ICollidable && !dynamic.Info.manual)
                    {
                        gridsToCheck = SpriteUtils.GetNearbyGrids(dynamic);
                        hb1 = SpriteUtils.GetHitbox(dynamic);
                        foreach (int[] grid in gridsToCheck)
                        {
                            try
                            {
                                if (grid[0] >= 0 && grid[1] < CollisionHandler.grid.GetLength(1))
                                {
                                    List<SpriteCollection> spritesInGrid = CollisionHandler.grid[grid[0], grid[1]];
                                    if (spritesInGrid != null)
                                    {
                                        foreach (SpriteCollection spr in spritesInGrid)
                                        {
                                            if (SpriteLayer.CollisionLayer.Sprites.Contains(spr) && spr is ICollidable)
                                            {
                                                hb2 = SpriteUtils.GetHitbox(spr);
                                                if (hb1.Intersects(hb2))
                                                {
                                                    matches.Add(new Tuple<SpriteCollection, SpriteCollection>(dynamic, spr));
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            catch (IndexOutOfRangeException)
                            {
                            }
                        }
                    }
                }
            }
            return matches;
        }

        public static void HandleStop(Direction direction, SpriteCollection sprite, SpriteCollection other)
        {
            Rectangle bounds1 = new Rectangle((int) sprite.Info.position.X, (int) sprite.Info.position.Y, sprite.Info.spriteWidth, sprite.Info.spriteHeight);
            Rectangle bounds2 = new Rectangle((int)other.Info.position.X, (int)other.Info.position.Y, other.Info.spriteWidth, other.Info.spriteHeight);

            Rectangle intersection = Rectangle.Intersect(bounds1, bounds2);

            if (direction == Direction.LEFT || direction == Direction.RIGHT)
            {
                if (sprite.Info.bounce)
                {
                    sprite.Info.acceleration.X = -sprite.Info.acceleration.X;
                    sprite.Info.velocity.X = -sprite.Info.velocity.X;
                }
                else
                {

                    if (direction == Direction.LEFT)
                        sprite.Info.position.X -= intersection.Width;
                    else
                        sprite.Info.position.X += intersection.Width;

                    sprite.Info.acceleration.X = 0;
                    sprite.Info.velocity.X = 0;
                }
            }
            else if (direction == Direction.TOP || direction == Direction.BOTTOM)
            {

                if (direction == Direction.TOP)
                    sprite.Info.position.Y -= intersection.Height;
                else
                    sprite.Info.position.Y += intersection.Height;

                sprite.Info.acceleration.Y = 0;
                sprite.Info.velocity.Y = 0;

                if (sprite is SpriteMario)
                {
                    SpriteMario m = sprite as SpriteMario;
                    StateMario currentState = m.StateMachineAction.CurrentState;

                    //Mario is landing
                    if (direction == Direction.TOP)
                    {
                        if (!(currentState is StateMarioWalking))
                        {
                            currentState.ToIdle();
                        }
                    }
                    //Mario is bumping his head
                    else
                    {
                        currentState.ToFall();
                    }
                }
            }
        }

    }
}
