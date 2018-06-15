using Baritone.Bird_Sprint5;
using Baritone.MappyBird_Sprint5.Wrappers;
using Baritone.MappyBird_Sprint5.Wrappers.Game;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Baritone.MappyBird_Sprint5.Utils
{
    public class CollisionHandler
    {

        private Layer layer;
        public ICollection<Sprite> Statics { get; private set; }
        public ICollection<Sprite> Dynamics { get; private set; }

        public CollisionHandler(Layer layer)
        {
            this.layer = layer;
            this.Statics = new List<Sprite>();
            this.Dynamics = new List<Sprite>();
        }

        public void ScanCollisions()
        {
            List<Tuple<Sprite, Sprite>> matches = FindMatches();

            SendCallbacks(matches);
        }


        //Given the nature of this game, we can safely compare every 
        //static sprite to every dynamic sprite
        //Only ~30 static sprites will be active at once
        //Only 1 dynamic sprite will be active at once
        //Result: ~30 iterations each cycle. Basically nothing
        private List<Tuple<Sprite, Sprite>> FindMatches()
        {
            List<Tuple<Sprite, Sprite>> matches = new List<Tuple<Sprite, Sprite>>();

            lock (this.layer)
            {
                foreach (Sprite spriteD in this.Dynamics)
                {
                    Rectangle hbD = SpriteUtils.GetHitbox(spriteD);

                    foreach (Sprite spriteS in this.Statics)
                    {
                        Rectangle hbS = SpriteUtils.GetHitbox(spriteS);
                        if (hbS.Intersects(hbD))
                            matches.Add(new Tuple<Sprite, Sprite>(spriteS, spriteD));
                    }
                }
            }
            
            return matches;
        }

        private static void SendCallbacks(List<Tuple<Sprite, Sprite>> matches)
        {
            foreach (var match in matches)
            {
                if (match.Item1 is ICollidable && match.Item2 is ICollidable)
                {
                    Rectangle hb1 = SpriteUtils.GetHitbox(match.Item1);
                    Rectangle hb2 = SpriteUtils.GetHitbox(match.Item2);

                    if (hb1.Intersects(hb2))
                    {
                        Rectangle intersection = Rectangle.Intersect(hb1, hb2);

                        //Send the callbacks for each sprite
                        if (intersection.Right == hb1.Right
                            && intersection.Height > intersection.Width
                            && (match.Item1.Velocity.X > 0 || match.Item2.Velocity.X < 0)) //1 is seeing a collision on the right
                        {
                            (match.Item1 as ICollidable).OnCollision(Direction.RIGHT, match.Item2, intersection);
                            (match.Item2 as ICollidable).OnCollision(Direction.LEFT, match.Item1, intersection);
                        }
                        else if (intersection.Top == hb1.Top
                            && intersection.Width > intersection.Height
                            && (match.Item1.Velocity.Y < 0 || match.Item2.Velocity.Y > 0)) //1 is seeing a collision on the top
                        {

                            (match.Item1 as ICollidable).OnCollision(Direction.TOP, match.Item2, intersection);
                            (match.Item2 as ICollidable).OnCollision(Direction.BOTTOM, match.Item1, intersection);
                        }
                        else if (intersection.Bottom == hb1.Bottom
                            && intersection.Width > intersection.Height
                            && (match.Item1.Velocity.Y > 0 || match.Item2.Velocity.Y < 0)) //1 is seeing a collision on the bottom
                        {

                            (match.Item1 as ICollidable).OnCollision(Direction.BOTTOM, match.Item2, intersection);
                            (match.Item2 as ICollidable).OnCollision(Direction.TOP, match.Item1, intersection);
                        }
                        else if (intersection.Left == hb1.Left
                            && intersection.Height > intersection.Width
                            && (match.Item1.Velocity.X < 0 || match.Item2.Velocity.X > 0)) //1 is seeing a collision on the left
                        {

                            (match.Item1 as ICollidable).OnCollision(Direction.LEFT, match.Item2, intersection);
                            (match.Item2 as ICollidable).OnCollision(Direction.RIGHT, match.Item1, intersection);
                        }
                    }
                }
            }
        }
    }
}
