using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ProjectQuestions
{
    public class GameObjectBase
    {
        public event GameObjectEvent Finish;

        protected Texture2D texture;

        protected GameObjectBase()
        {

        }

        protected GameObjectBase(Texture2D texture)
        {
            this.texture = texture;
        }

        public abstract void Draw(SpriteBatch batch);

        public abstract void Update(GameTime gameTime);
    }
}
