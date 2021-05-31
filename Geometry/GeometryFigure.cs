using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ProjectMonoGame01.Geometry
{
    public abstract class GeometryFigure
    {
        protected static Texture2D texturePixel = null;

        private Color color;
        public virtual Color Color
        {
            get { return color; }
            set { color = value; }
        }

        private int lineWidth;
        public virtual int LineWidth
        {
            get { return lineWidth; }
            set { lineWidth = value; }
        }

        public GeometryFigure()
        {
            color = Color.White;
            lineWidth = 1;
            if (texturePixel == null)
            {
                texturePixel = new Texture2D(GlobalsItems.Graphics.GraphicsDevice, 1, 1);
                texturePixel.SetData<Color>(new Color[] { Color.White });
            }
        }

        public abstract void Draw(SpriteBatch batch);

        public abstract void Transposition(Vector2 delta);

        public virtual void Update(GameTime time)
        { }
    }
}
