using Baritone.MappyBird_Sprint5.Wrappers.Game;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Baritone.MappyBird_Sprint5.Utils
{
    public static class SpriteUtils
    {
        public static Rectangle GetHitbox(Sprite sprite)
        {
            return new Rectangle((int) (sprite.Position.X), (int) (sprite.Position.Y), (int) (sprite.Width * sprite.Scale), (int) (sprite.Height * sprite.Scale));
        }

        public static ICollection<int[]> DetermineGrids(Sprite sprite)
        {
            ICollection<int[]> grids = new List<int[]>();

            int x1 = (int)sprite.Position.X;
            int y1 = (int)sprite.Position.Y;

            int w = sprite.Width;
            int h = sprite.Height;

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

        public static ICollection<Sprite> GenerateRandomBackground(Layer layer, int startX, int endX, float frequency, params Func<Layer, Sprite>[] spriteCreators)
        {
            ICollection<Sprite> result = new List<Sprite>();

            if (spriteCreators == null || spriteCreators.Count() == 0) return result;
            if (frequency < 0.1f) frequency = 0.1f;
            if (frequency > 1f) frequency = 1f;

            int xFixed = Math.Min(startX, layer.Bounds.Right);
            int yFixed = Math.Min(30, layer.Bounds.Bottom);
            Random rand = new Random();

            while (xFixed < endX)
            {
                Sprite s = spriteCreators[rand.Next(0, spriteCreators.Count())](layer);
                if (s.Width <= 0)
                {
                    Console.WriteLine("[Error] GenerateRandomBackground just loaded a sprite with Width=0. ({0})", s.GetType().Name);
                    break;
                }
                s.Position = new Vector2(xFixed + (rand.Next(-15, 15)), yFixed + (rand.Next(-15, 45)));
                result.Add(s);
                xFixed += (int) (s.Width / frequency);
            }

            return result;
        }
    }

}
