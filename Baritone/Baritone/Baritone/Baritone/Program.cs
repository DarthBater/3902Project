using Baritone.MappyBird_Sprint5;
using System;

namespace Baritone
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            using (Sprint5 game = new Sprint5())
            {
                game.Run();
            }
        }
    }
#endif
}

