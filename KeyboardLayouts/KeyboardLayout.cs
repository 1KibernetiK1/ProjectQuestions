using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ProjectMonoGame01.KeyboardLayouts;

namespace ProjectMonoGame01.KeyboardLayouts
{
    public class KeyboardLayout
    {
        private List<KeyAction> keyActions;
        public List<KeyAction> KeyActions
        {
            get { return keyActions; }
            set { keyActions = value; }
        }

        public KeyboardLayout()
        {
            keyActions = new List<KeyAction>();
        }

        public void AddKey(Keys key, ActionType direction)
        {
            KeyAction keyAction = new KeyAction(key, direction);
            keyActions.Add(keyAction);
        }
    }
}
