using Baritone.MappyBird_Sprint5.Utils;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Baritone.MappyBird_Sprint5.Wrappers.Game.Levels.One.Sound
{
    public class L1SoundHandler : SoundHandler
    {

        public Song AxelF { get; private set; }
        public SoundEffect Death { get; private set; }
        public SoundEffect Coin { get; private set; }
        public SoundEffect Pipe { get; private set; }

        public L1SoundHandler(Level level) : base()
        {
            this.AxelF = level.Game.Content.Load<Song>("Music/axelf");
            this.Death = level.Game.Content.Load<SoundEffect>("Music/death");
            this.Coin = level.Game.Content.Load<SoundEffect>("Music/coin");
            this.Pipe = level.Game.Content.Load<SoundEffect>("Music/OneUp");
        }

    }
}
