using Baritone.Input;
using Baritone.Input.Impl;
using Baritone.Wrappers.Listeners;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Baritone.Wrappers.Layers
{
    public class HuDLayer : SpriteLayer
    {

        private InputController gameInputK;
        private InputController gameInputG;

        public HuDLayer(Sprint4 game) : base(0f)
        {
            this.name = "HuD";
            this.gameInputK = new InputControllerKeyboard();
            this.gameInputG = new InputControllerGamePad(PlayerIndex.One);

            KeyListener exit = new ExitKeyListener(game);
            this.gameInputK.RegisterKeyListener(Keys.Q, exit);
            this.gameInputG.RegisterKeyListener(Buttons.Back, exit);
            this.gameInputK.RegisterKeyListener(Keys.R, new ResetKeyListener(game));

            this.InputControllers.Add(this.gameInputK);
            this.InputControllers.Add(this.gameInputG);

            SpriteText gameScore = new SpriteText(game, 15, 15, Color.Black);
            SpriteText gameCoins = new SpriteText(game, 215, 15, Color.Black);
            SpriteText gameLives = new SpriteText(game, 465, 15, Color.Black);
            SpriteText gameTime = new SpriteText(game, 700, 15, Color.Black);

            gameScore.Text = delegate ()
            {
                int numZeros = 6 - (game.Mario.points.ToString().Length);
                if (game.Mario.points == 0)
                {
                    numZeros = 6;
                }

                string points = "Mario\n";

                for (int i = 0; i < numZeros; i++)
                {
                    points += "0";
                }

                return string.Format(points + "{0}", game.Mario.points);
            };

            gameCoins.Text = delegate ()
            {
                return string.Format(" x {0}", game.Mario.coins);
            };

            gameLives.Text = delegate ()
            {
                return string.Format(" x {0}", game.Mario.lives);
            };

            gameTime.Text = delegate ()
            {
                return string.Format("Time\n {0}", game.TimeLeft);
            };

            this.AddSpriteText(gameScore);
            this.AddSpriteText(gameCoins);
            this.AddSpriteText(gameLives);
            this.AddSpriteText(gameTime);
        }

    }
}
