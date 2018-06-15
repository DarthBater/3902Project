using Baritone.Sprites;
using Baritone.Wrappers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Baritone.Utils
{
    public static class SpriteFactory
    {
        private static Dictionary<string, Texture2D> cache = new Dictionary<string, Texture2D>();
        private static SpriteMario mario;

        public static Texture2D loadTexture(Game game, string path)
        {
            if (cache.ContainsKey(path))
            {
                return cache[path];
            }
            Texture2D t = game.Content.Load<Texture2D>(path);
            cache[path] = t;
            return t;
        }

        public static SpriteMario CreateMario(Sprint4 game)
        {
            if (SpriteFactory.mario != null)
            {
                return SpriteFactory.mario;
            }

            return SpriteFactory.mario = new SpriteMario(game);
        }

        public static SpriteMario CreateHuDMario(Sprint4 game)
        {
            SpriteMario sprite = new SpriteMario(game);
            sprite.Info.manual = true;
            return sprite;
        }

        public static SpriteFlag CreateFlag(Sprint4 game)
        {
            return new SpriteFlag(game);
        }

        public static SpriteFlagpole CreateFlagpole(Sprint4 game)
        {
            return new SpriteFlagpole(game);
        }

        public static SpriteGoomba CreateGoomba(Sprint4 game)
        {
            return new SpriteGoomba(game);
        }

        /*public static SpriteKoopa CreateKoopa(Sprint3 game, string color)
        {
            if (!color.Equals("Red") && !color.Equals("Green"))
            {
                Console.WriteLine("[Error] CreateKoopa called with color not Red or Green");
                return null;
            }

            return new SpriteKoopa(game, color);
        }//*/

        public static SpriteCoin CreateCoin(Sprint4 game)
        {
            return new SpriteCoin(game);
        }

        public static SpriteMushroom CreateMushroom(Sprint4 game)
        {
            return new SpriteMushroom(game, "Sprites/Items/supershroom");
        }

        public static SpriteMushroom Create1UP(Sprint4 game)
        {
            return new SpriteMushroom(game, "Sprites/Items/oneupshroom");
        }

        public static SpriteStar CreateStar(Sprint4 game)
        {
            SpriteStar star = new SpriteStar(game);

            return star;
        }

        public static SpriteBrickBlock CreateBrick(Sprint4 game)
        {
            return new SpriteBrickBlock(game);
        }

        public static SpriteBrickBlock CreateBrickItem(Sprint4 game, SpriteCollection item)
        {
            SpriteBrickBlock sprite = new SpriteBrickBlock(game);
            sprite.item = item;
            return sprite;
        }

        public static SpriteHiddenBlock CreateHiddenBlock(Sprint4 game)
        {
            return new SpriteHiddenBlock(game);
        }

        public static SpriteHiddenBlock CreateHiddenBlockItem(Sprint4 game, SpriteCollection item)
        {
            SpriteHiddenBlock sprite = new SpriteHiddenBlock(game);
            sprite.item = item;
            return sprite;
        }

        /*public static SpriteUsedBlock CreateUsedBlock(Sprint1 game)
        {
            return new SpriteUsedBlock(game);
        }*/

        public static SpriteQuestionBlock CreateQuestionBlock(Sprint4 game)
        {
            return new SpriteQuestionBlock(game);
        }

        public static SpriteQuestionBlock CreateQuestionBlockItem(Sprint4 game, SpriteCollection item)
        {
            SpriteQuestionBlock sprite = new SpriteQuestionBlock(game);
            sprite.item = item;
            return sprite;
        }

        public static SpriteFloor CreateFloorTile(Sprint4 game)
        {
            return new SpriteFloor(game);
        }

        public static SpriteStair CreateStairBlock(Sprint4 game)
        {
            return new SpriteStair(game);
        }

        public static SpriteFireFlower CreateFireFlower(Sprint4 game)
        {
            return new SpriteFireFlower(game);
        }

        public static SpriteBackground CreateBackground(Sprint4 game)
        {
            return new SpriteBackground(game);
        }

        public static SpriteCollection CreateGameOver(Sprint4 game)
        {
            return new SpriteGameOver(game);
        }

        public static SpriteCollection CreateYouWin(Sprint4 game)
        {
            return new SpriteYouWin(game);
        }

        public static SpriteCollection CreatePipe(Sprint4 game)
        {
            return new SpritePipe(game);
        }

    }
}
