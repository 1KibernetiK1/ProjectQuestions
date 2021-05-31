using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ProjectMonoGame01.KeyboardLayouts
{
    public class KeyAction
    {
        public Keys Key;
        public ActionType Direction;

        public KeyAction()
        { }

        public KeyAction(Keys key, ActionType direction)
        {
            Key = key;
            Direction = direction;
        }
    }
}
