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
    public class KeyboardLayoutArrows 
        : KeyboardLayout
    {
        public KeyboardLayoutArrows()
        {
            KeyActions = new List<KeyAction>();
            KeyActions.Add(new KeyAction(Keys.Left,  ActionType.Left));
            KeyActions.Add(new KeyAction(Keys.Right, ActionType.Right));
            KeyActions.Add(new KeyAction(Keys.Up,    ActionType.Up));
            KeyActions.Add(new KeyAction(Keys.Down,  ActionType.Down));
        }
    }
}
