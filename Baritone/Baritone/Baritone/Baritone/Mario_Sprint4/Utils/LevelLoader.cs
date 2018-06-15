using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Baritone.Wrappers;
using Baritone.Sprites;
using System.Text.RegularExpressions;
using Microsoft.Xna.Framework;

namespace Baritone.Utils
{
    public static class LevelLoader
    {

        public static void LoadLevel(Sprint4 game, string path)
        {
            if (File.Exists(path))
            {
                int maxX = 0;

                //Console.WriteLine("Loading level from {0}", path);
                string[] lines = File.ReadAllLines(path);

                string type, x, y;
                string boundLeft, boundRight, boundTop, boundBottom;
                string arg1, arg2, arg3;

                SpriteLayer currentLayer = null;

                foreach (string line in lines)
                {
                    if (line.Length > 0 && !line.StartsWith("//"))
                    {
                        if (line.StartsWith("Layer"))
                        {
                            string[] tokens = Regex.Split(line, ":");
                            if (tokens.Length >= 2)
                            {
                                string layer = tokens[1];
                                if (layer.Equals("Collision"))
                                {
                                    currentLayer = SpriteLayer.CollisionLayer;
                                }
                                else if (layer.Equals("Background"))
                                {
                                    currentLayer = SpriteLayer.BackgroundLayer;
                                }
                                else if (layer.Equals("HuD"))
                                {
                                    currentLayer = SpriteLayer.HuDLayer;
                                }
                            }
                            continue;
                        }

                        if (currentLayer == null)
                        {
                            Console.WriteLine("[Error] We're starting to load sprites, but a Layer:<LAYER> has not been identified.");
                        }

                        ReadFile(line, out type, out x, out y, out boundLeft, out boundRight, out boundTop, out boundBottom, out arg1, out arg2, out arg3);
                        //Console.WriteLine("Params: {0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}",
                          //  type, x, y, boundLeft, boundRight, boundTop, boundBottom, arg1, arg2, arg3);

                        if (type.Length == 0 || x.Length == 0 || y.Length == 0)
                        {
                            Console.WriteLine("Invalid parameters provided. type, x, and y must be defined.");
                            continue;
                        }

                        SpriteCollection sprite;

                        switch(type)
                        {
                            case "floor":
                                sprite = SpriteFactory.CreateFloorTile(game);
                                break;

                            case "stair":
                                sprite = SpriteFactory.CreateStairBlock(game);
                                break;

                            case "used":
                                sprite = SpriteFactory.CreateBrick(game);
                                sprite.SetSheetState(SpriteStates.Sheets.USED);
                                sprite.SetSpriteState(SpriteStates.Sprites.IDLE);
                                break;
                            case "brick":
                                sprite = SpriteFactory.CreateBrick(game);
                                break;

                            case "hidden":
                                sprite = SpriteFactory.CreateHiddenBlock(game);
                                break;

                            case "question":
                                sprite = SpriteFactory.CreateQuestionBlock(game);
                                break;

                            case "coin":
                                sprite = SpriteFactory.CreateCoin(game);
                                break;

                            case "fireflower":
                                sprite = SpriteFactory.CreateFireFlower(game);
                                break;

                            case "mushroom":
                                sprite = SpriteFactory.CreateMushroom(game);
                                break;

                            case "oneup":
                                sprite = SpriteFactory.Create1UP(game);
                                break;

                            case "goomba":
                                sprite = SpriteFactory.CreateGoomba(game);
                                break;

                            /*case "koopa":
                                if (arg1.Equals("Green") || arg1.Equals("Red"))
                                {
                                    sprite = SpriteFactory.CreateKoopa(game, arg1);
                                }
                                else
                                {
                                    Console.WriteLine("[Error] Invalid arg1 for Koopa. Must be Green or Red");
                                    continue;
                                }
                                break;//*/
                            case "pipe":
                                sprite = SpriteFactory.CreatePipe(game);
                                break;
                            case "background":
                                sprite = SpriteFactory.CreateBackground(game);
                                break;
                            case "mario":
                                sprite = SpriteFactory.CreateHuDMario(game);
                                break;
                            case "flag":
                                sprite = SpriteFactory.CreateFlag(game);
                                game.flag = (sprite as SpriteFlag);
                                break;
                            case "flagpole":
                                sprite = SpriteFactory.CreateFlagpole(game);
                                break;

                            default:
                                Console.WriteLine("Unrecognized type provided: {0}", type);
                                continue;
                        }

                        string[] xTokens = Regex.Split(x, "[+\\-\\*/]");
                        string[] yTokens = Regex.Split(y, "[+\\-\\*/]");

                        int x1 = resolveValue(game, xTokens[0], sprite);
                        int y1 = resolveValue(game, yTokens[0], sprite);

                        if (xTokens.Length > 1)
                        {
                            if (xTokens.Length != 2)
                            {
                                Console.WriteLine("[Error] x position with an operator provided, but no second value.");
                                continue;
                            }
                            int x2 = resolveValue(game, xTokens[1], sprite);

                            if (x.Contains("+"))
                            {
                                x1 += x2;
                            }
                            else if (x.Contains("-"))
                            {
                                x1 -= x2;
                            }
                            else if (x.Contains("*"))
                            {
                                x1 *= x2;
                            }
                            else if (x.Contains("/"))
                            {
                                x1 /= x2;
                            }

                        }

                        if (yTokens.Length > 1)
                        {
                            if (yTokens.Length != 2)
                            {
                                Console.WriteLine("[Error] y position with an operator provided, but no second value.");
                                continue;
                            }
                            int y2 = resolveValue(game, yTokens[1], sprite);

                            if (y.Contains("+"))
                            {
                                y1 += y2;
                            }
                            else if (y.Contains("-"))
                            {
                                y1 -= y2;
                            }
                            else if (y.Contains("*"))
                            {
                                y1 *= y2;
                            }
                            else if (y.Contains("/"))
                            {
                                y1 /= y2;
                            }
                        }

                        //Console.WriteLine("Setting position to ({0}, {1})", x1, y1);
                        sprite.SetPosition(x1, y1);
                        if (sprite is SpriteFlagpole)
                        {
                            game.flagposition = (int)sprite.Info.position.X;
                        }
                        if (boundLeft.Length > 0)
                        {
                            //should have already caught it if not all 4 bounds were provided
                            //defaults are already assigned as well

                            int bl, br, bt, bb;

                            bl = resolveValue(game, boundLeft, sprite);
                            br = resolveValue(game, boundRight, sprite);
                            bt = resolveValue(game, boundTop, sprite);
                            bb = resolveValue(game, boundBottom, sprite);

                            //Console.WriteLine("Setting bounds of {0} to {1}, {2}, {3}, {4}", sprite.name, bl, bt, (br - bl), (bb - bt));
                            sprite.Info.bounds = new Rectangle(bl, bt, (br - bl), (bb - bt));
                        }

                        if (arg1 != null)
                        {
                            if (sprite is SpriteBlock)
                            {
                                switch (arg1)
                                {
                                    case "coin":
                                        (sprite as SpriteBlock).item = new SpriteCoin(game);
                                        int count = 1;
                                        if (arg2 != null)
                                        {
                                            count = resolveValue(game, arg2, sprite);
                                        }
                                        (sprite as SpriteBlock).itemCount = count;
                                        break;

                                    case "fireflower":
                                        (sprite as SpriteBlock).item = new SpriteFireFlower(game);
                                        break;

                                    case "mushroom":
                                        (sprite as SpriteBlock).item = new SpriteMushroom(game, "Sprites/Items/supershroom");
                                        break;

                                    case "oneup":
                                        (sprite as SpriteBlock).item = new SpriteMushroom(game, "Sprites/Items/oneupshroom");
                                        break;

                                    case "star":
                                        (sprite as SpriteBlock).item = new SpriteStar(game);
                                        break;
                                }
                            }
                        }

                        currentLayer.AddSprite(sprite);

                        if (sprite.Info.x + sprite.Info.spriteWidth > maxX)
                        {
                            maxX = sprite.Info.x + sprite.Info.spriteWidth;
                        }
                    }
                }

                //Console.WriteLine("Setting game bounds and Collision grid");
                game.bounds = new Rectangle(0, 0, maxX, game.GetViewport().Height);

                foreach (SpriteLayer sl in new SpriteLayer[] { SpriteLayer.BackgroundLayer, SpriteLayer.HuDLayer, SpriteLayer.GameOverLayer }) {
                    foreach (SpriteCollection sprite in sl.Sprites)
                    {
                        sprite.Info.bounds = game.bounds;
                    }
                }

                int rows = (int)Math.Ceiling(maxX / 32D) + 1;
                int columns = (int)Math.Ceiling(game.bounds.Height / 32D) + 1;
                CollisionHandler.grid = new List<SpriteCollection>[rows, columns];
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < columns; j++)
                    {
                        CollisionHandler.grid[i, j] = new List<SpriteCollection>();
                    }
                }
                //Console.WriteLine("Creating grid with {0} rows and {1} columns", rows, columns);
                //Set grids
                foreach (SpriteCollection st in CollisionHandler.statics) {
                    List<int[]> grids = SpriteUtils.DetermineGrids(st);
                    foreach (int[] grid in grids)
                    {
                        CollisionHandler.grid[grid[0], grid[1]].Add(st);
                    }
                    st.Info.bounds = game.bounds;
                }

                foreach (SpriteCollection dy in CollisionHandler.dynamics)
                {
                    if (dy.Info.bounds == null || dy.Info.bounds == default(Rectangle))
                    {
                        //Console.WriteLine("Setting {0} bounds to default", dy.name);
                        dy.Info.bounds = game.bounds;
                    }
                }

            }
            else
            {
                //.WriteLine("[Error] File not found: {0}", path);
            }

        }

        private static int resolveValue(Sprint4 game, string token, SpriteCollection sprite)
        {

            int scale = 1;

            Match m = Regex.Match(token, "([\\d]+)[a-zA-Z]+");
            if (m.Length > 0)
            {
                scale = int.Parse(Regex.Replace(token, "[^\\d]", ""));
                token = Regex.Replace(token, "[\\d]", "");
            }

            int value;
            if (token == "w")
            {
                value = sprite.Info.spriteWidth;
            }
            else if (token == "h")
            {
                value = sprite.Info.spriteHeight;
            }
            else if (token == "left")
            {
                value = game.GetViewport().Left;
            }
            else if (token == "right")
            {
                value = game.GetViewport().Right;
            }
            else if (token == "top")
            {
                value = game.GetViewport().Top;
            }
            else if (token == "bottom")
            {
                value = game.GetViewport().Bottom;
            }
            else
            {
                //Console.WriteLine("parsing {0}", token);
                value = int.Parse(token);
            }

            return value * scale;
        }

        private static void ReadFile(string line, out string type, out string x, out string y, out string boundLeft, out string boundRight, out string boundTop, out string boundBottom, out string arg1, out string arg2, out string arg3)
        {
            

            type = "";
            x = "";
            y = "";
            boundLeft = "";
            boundRight = "";
            boundTop = "";
            boundBottom = "";
            arg1 = "";
            arg2 = "";
            arg3 = "";

            string[] tokens = line.Split(':');
            type = tokens[0];
            x = tokens[1];
            y = tokens[2];

            if (tokens.Length > 3)
            {
                //More than 3, expecting 4 bounds
                if (tokens.Length < 7)
                {
                    //.WriteLine("[Error] Sprite has >3 parameters provided, but not all 4 bounds were specified.");
                    return;
                }
                boundLeft = tokens[3];
                boundRight = tokens[4];
                boundTop = tokens[5];
                boundBottom = tokens[6];

                if (boundLeft.Length == 0)
                    boundLeft = "left";
                if (boundRight.Length == 0)
                    boundRight = "right";
                if (boundTop.Length == 0)
                    boundTop = "top";
                if (boundBottom.Length == 0)
                    boundBottom = "bottom";
            }
            if (tokens.Length > 7)
            {
                //arg1 specified
                arg1 = tokens[7];
            }
            if (tokens.Length > 8)
            {
                //arg2 specified
                arg2 = tokens[8];
            }
            if (tokens.Length > 9)
            {
                //arg3 specified
                arg3 = tokens[9];
            }
        }

    }
}
