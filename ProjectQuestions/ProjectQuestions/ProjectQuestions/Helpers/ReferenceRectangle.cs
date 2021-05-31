using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ProjectQuestions.Helpers
{
    public class ReferenceRectangle
    {
        private Vector2[] _dots;

        public Vector2 Dot1
        {
            get { return _dots[0]; }
            set { _dots[0] = value; }
        }

        public Vector2 Dot2
        {
            get { return _dots[1]; }
            set { _dots[1] = value; }
        }

        public Vector2 Dot3
        {
            get { return _dots[2]; }
            set { _dots[2] = value; }
        }

        public Vector2 Dot4
        {
            get { return _dots[3]; }
            set { _dots[3] = value; }
        }


        public ReferenceRectangle()
        {
            _dots = new Vector2[4];
        }

        public ReferenceRectangle(Rectangle rect) : this()
        {
            _dots[0] = new Vector2(rect.Left, rect.Top);
            _dots[1] = new Vector2(rect.Right, rect.Top);
            _dots[2] = new Vector2(rect.Right, rect.Bottom);
            _dots[3] = new Vector2(rect.Left, rect.Bottom);
        }

        public ReferenceRectangle(float x, float y, float w, float h) : this()
        {
            _dots[0] = new Vector2(x, y);
            _dots[1] = new Vector2(x + w, y);
            _dots[2] = new Vector2(x + w, y + h);
            _dots[3] = new Vector2(x, y + h);
        }

        public ReferenceRectangle(Vector2 v1, Vector2 v2, Vector2 v3, Vector2 v4) : this()
        {
            _dots[0] = v1;
            _dots[1] = v2;
            _dots[2] = v3;
            _dots[3] = v4;
        }

        public float Left
        {
            get
            {
                return _dots[0].X;
            }
        }

        public float Right
        {
            get
            {
                return _dots[1].X;
            }
        }

        public float Top
        {
            get
            {
                return _dots[0].Y;
            }
        }

        public void UpdatePoints(Vector2[] dots)
        {
            for (int i = 0; i < dots.Length; i++)
            {
                _dots[i].X = dots[i].X;
                _dots[i].Y = dots[i].Y;
            }
        }

        public float Bottom
        {
            get
            {
                return _dots[2].Y;
            }
        }

        public float Width
        {
            get
            {
                return _dots[1].X - _dots[0].X;
            }
            set
            {
                _dots[1].X = _dots[0].X + value;
                _dots[2].X = _dots[0].X + value;
            }
        }

        public float Height
        {
            get
            {
                return _dots[3].Y - _dots[0].Y;
            }
            set
            {
                _dots[2].X = _dots[0].Y + value;
                _dots[3].X = _dots[0].Y + value;
            }
        }

        public bool IsIntersect(ReferenceRectangle rect2)
        {
            float left = Math.Min(this.Left, rect2.Left);
            float right = Math.Max(this.Right, rect2.Right);
            float top = Math.Min(this.Top, rect2.Top);
            float bottom = Math.Max(this.Bottom, rect2.Bottom);

            float widthArea = right - left;
            float heightArea = bottom - top;

            float widthBoth = this.Width + rect2.Width;
            float heightBoth = this.Height + rect2.Height;

            /*
            Console.WriteLine("WA={0}, HA={1}, WB={2}, HB={3}", 
                widthArea, heightArea, widthBoth, heightBoth);*/

            return (widthArea < widthBoth) && (heightArea < heightBoth);
        }

        public bool IsContains(Vector2 dot)
        {
            return
                dot.X >= Left &&
                dot.X <= Right &&
                dot.Y >= Top &&
                dot.Y <= Bottom;
        }

        public Rectangle DefineRectangleIntersect(ReferenceRectangle rect2)
        {
            ReferenceRectangle rect1 = this;

            float left = Math.Max(rect1.Left, rect2.Left);
            float right = Math.Min(rect1.Right, rect2.Right);
            float top = Math.Max(rect1.Top, rect2.Top);
            float bottom = Math.Min(rect1.Bottom, rect2.Bottom);

            int width = (int)(right - left);
            int height = (int)(bottom - top);

            if (width < 0 || height < 0)
            {
                width = 0;
                height = 0;
            }

            return new Rectangle((int)left, (int)top, width, height);
        }
    }
}
