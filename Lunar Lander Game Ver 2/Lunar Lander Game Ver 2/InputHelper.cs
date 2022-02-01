using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Lunar_Lander_Game_Ver_2
{
    public static class InputHelper
    {
        static private KeyboardState currentKeyboardstate;

        public static void Update()
        {
            currentKeyboardstate = Keyboard.GetState();
        }

        public static bool KeyPressed(Keys k)
        {
            return currentKeyboardstate.IsKeyDown(k);
        }
    }
}
