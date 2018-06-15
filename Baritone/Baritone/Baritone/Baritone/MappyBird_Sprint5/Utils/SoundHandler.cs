using Baritone.MappyBird_Sprint5;
using Baritone.MappyBird_Sprint5.Wrappers.Game;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Baritone.MappyBird_Sprint5.Utils
{
    public abstract class SoundHandler
    {

        private static SoundEffectInstance lastSoundEffect;

        static SoundHandler()
        {
            MediaPlayer.Volume = 0.30f;
        }

        protected SoundHandler()
        {
        }

        public static void PlaySong(Song song)
        {
            MediaPlayer.Stop();
            MediaPlayer.Play(song);
            MediaPlayer.IsRepeating = true;
        }

        public static void PlaySoundEffect(SoundEffect effect)
        {
            if (!MediaPlayer.IsMuted)
            {
                (lastSoundEffect = effect.CreateInstance()).Play();
            }
        }

        public static void StopLastSoundEffect()
        {
            if (lastSoundEffect != null)
            {
                lastSoundEffect.Stop();
            }
        }

        
    }
}
