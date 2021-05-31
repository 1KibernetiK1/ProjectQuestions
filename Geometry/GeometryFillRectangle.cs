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
    public class GeometryFillRectangle 
        : GeometryRectangle
    {
        public GeometryFillRectangle(Rectangle rect) 
            : base(rect)
        {

        }

        public void Modify(Rectangle rect)
        {
            dot1.X = rect.Left;
            dot1.Y = rect.Top;
            dot2.X = rect.Right;
            dot2.Y = rect.Top;
            dot3.X = rect.Right;
            dot3.Y = rect.Bottom;
            dot4.X = rect.Left;
            dot4.Y = rect.Bottom;
        }

        public override void Draw(SpriteBatch batch)
        {
            var rect = new Rectangle(
                (int) dot1.X, 
                (int) dot1.Y,
                (int) (dot2.X - dot1.X), 
                (int) (dot3.Y - dot1.Y));

            batch.Draw(texturePixel, rect, this.Color);
        }
    }
}
