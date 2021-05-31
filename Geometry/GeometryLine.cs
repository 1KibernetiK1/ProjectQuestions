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
    public class GeometryLine
        : GeometryFigure
    {
        private Vector2 startDot;

        public Vector2 StartDot
        {
            get { return startDot; }
            set
            {
                startDot = value;
                CalculateLine();
            }
        }

        private Vector2 endDot;

        public Vector2 EndDot
        {
            get { return endDot; }
            set
            {
                endDot = value;
                CalculateLine();
            }
        }

        protected float angle;
        protected float length;

        public GeometryLine() 
            : base()
        {
            angle = 0;
            length = 0;
        }

        protected void CalculateLine()
        {
            Vector2 section = endDot - startDot;
            length = section.Length();
            angle = (float)Math.Atan2(section.Y, section.X);
        }

        public GeometryLine(Vector2 first, Vector2 second)
            : this()
        {
            startDot = first;
            endDot = second;
            CalculateLine();
        }


        public override void Draw(SpriteBatch batch)
        {
            Vector2 scale = new Vector2(length, LineWidth);
            batch.Draw(
                texturePixel, 
                startDot, 
                null, 
                Color,
                angle,
                Vector2.Zero,
                scale,
                SpriteEffects.None,
                1
                );
        }

        public override void Transposition(Vector2 delta)
        {
            startDot += delta;
            endDot += delta;
        }
    }
}
