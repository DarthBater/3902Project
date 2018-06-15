using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Baritone.Utils
{
    public static class SoundFactory
    {

        private static Dictionary<string, SoundEffect> cache = new Dictionary<string, SoundEffect>();

        private static SoundEffect loadSound(Game game, string path)
        {
            if (cache.ContainsKey(path))
            {
                return cache[path];
            }
            SoundEffect t = game.Content.Load<SoundEffect>(path);
            cache[path] = t;
            return t;
        }

        public static void PlaySoundEffect(SoundEffect effect)
        {
            if (!MediaPlayer.IsMuted)
            {
                effect.Play();
            }
        }

        public static SoundEffect BlockBump()
        {
            return loadSound(Sprint4.Instance, "Music/blockbump");
        }

        public static SoundEffect BrickBreak()
        {
            return loadSound(Sprint4.Instance, "Music/brickbreak");
        }

        public static SoundEffect Coin()
        {
            return loadSound(Sprint4.Instance, "Music/coin");
        }

        public static SoundEffect Death()
        {
            return loadSound(Sprint4.Instance, "Music/death");
        }

        public static SoundEffect Jump()
        {
            return loadSound(Sprint4.Instance, "Music/jump");
        }

        public static SoundEffect Kick()
        {
            return loadSound(Sprint4.Instance, "Music/kick");
        }

        public static SoundEffect OneUp()
        {
            return loadSound(Sprint4.Instance, "Music/OneUp");
        }

        public static SoundEffect Pause()
        {
            return loadSound(Sprint4.Instance, "Music/pause");
        }

        public static SoundEffect PowerDown()
        {
            return loadSound(Sprint4.Instance, "Music/powerdown");
        }

        public static SoundEffect PowerUp()
        {
            return loadSound(Sprint4.Instance, "Music/powerup");
        }

        public static SoundEffect PowerUpAppear()
        {
            return loadSound(Sprint4.Instance, "Music/powerupappear");
        }

        public static SoundEffect TimeEnding()
        {
            return loadSound(Sprint4.Instance, "Music/timeending");
        }

        public static SoundEffect GameOver()
        {
            return loadSound(Sprint4.Instance, "Music/gameover");
        }

        public static SoundEffect YouWin()
        {
            return loadSound(Sprint4.Instance, "Music/youwin");
        }

    }
}
