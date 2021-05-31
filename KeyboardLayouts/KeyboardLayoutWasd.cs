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
    public class KeyboardLayoutWasd
        : KeyboardLayout
    {
        public KeyboardLayoutWasd()
        {
            KeyActions = new List<KeyAction>();
            KeyActions.Add(new KeyAction(Keys.W, ActionType.Up));
            KeyActions.Add(new KeyAction(Keys.A, ActionType.Left));
            KeyActions.Add(new KeyAction(Keys.S, ActionType.Down));
            KeyActions.Add(new KeyAction(Keys.D, ActionType.Right));
        }
    }
}
