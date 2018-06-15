using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Baritone.Utils
{
    public static class SpriteStates
    {

        public static class Sheets
        {
            public const int NORMAL = 1;
            public const int SUPER = 2;
            public const int FIRE = 3;
            public const int STAR_NORMAL = 4;
            public const int USED = 5;
            public const int BRICK = 6;
            public const int QUESTION = 7;
            public const int HIDDEN = 8;
            public const int BROKEN = 9;
            public const int STAR_SUPER = 10;
            public const int SHELL = 11;
        }

        public static class Sprites
        {
            public const int IDLE = 1;
            public const int CROUCHING = 2;
            public const int WALKING = 3;
            public const int JUMPING = 4;
            public const int FALLING = 5;
            public const int DEAD = 6;
            public const int SHELLED = 7;
            public const int WINGED = 8;
            public const int BROKEN = 9;
        }
        /*
        //Mario Movement states
        public const int MOVEMENT_IDLE = 1;
        public const int MOVEMENT_CROUCHING = 2;
        public const int MOVEMENT_WALKING = 3;
        public const int MOVEMENT_RUNNING = 4;
        public const int MOVEMENT_JUMPING = 5;
        public const int MOVEMENT_FALLING = 6;
        public const int MOVEMENT_DYING = 7;
        public const int MOVEMENT_THROWING = 8;

        //Mario states
        public const int MARIO_NORMAL_LITTLE = 1;
        public const int MARIO_NORMAL_SUPER = 2;
        public const int MARIO_FIRE_SUPER = 3;
        public const int MARIO_INVINCIBLE_LITTLE = 4;
        public const int MARIO_INVINCIBLE_SUPER = 5;
        public const int MARIO_DEAD = 6;

        //Goomba states
        public const int GOOMBA_NORMAL = 1;
        public const int GOOMBA_DEAD = 2;

        //Goomba movement states
        public const int GOOMBA_WALKING = 1;
        public const int GOOMBA_DYING = 2;        
        
        //Koopa states
        public const int KOOPA_NORMAL = 1;
        public const int KOOPA_SHELL = 2;
        public const int KOOPA_WINGED = 3;

        //Koopa movement states
        public const int KOOPA_WALKING = 1;
        public const int KOOPA_SHELLED = 2;
        public const int KOOPA_FLYING = 3;

        //Coin State
        public const int COIN_STILL = 1;

        //Mushroom State
        public const int MUSHROOM_NORMAL = 1;

        //Block States
        public const int BRICK = 1;
        public const int BROKEN_BRICK = 2;
        public const int QUESTION_BLOCK = 3;
        public const int HIDDEN_BLOCK = 4;
        public const int USED_BLOCK = 5;

        //Obstacle States
        public const int FLOOR_BLOCK = 1;
        public const int STAIR_BLOCK = 2;

        //Block "movement states"
        public const int BLOCK_STILL = 1;
        public const int BLOCK_BREAK = 2;

        //Star State
        public const int STAR_NORMAL = 1;

        //Star State
        public const int STAR_OBTAINED = 2;

        //Fireflower State
        public const int FLOWER_NORMAL = 1;
        public const int FLOWER_OBTAINED = 2;

        //Fireball State
        public const int FIREBALL_NORMAL = 1;*/
    }
}
