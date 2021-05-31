using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ProjectMonoGame01.Helpers;

namespace ProjectMonoGame01.Geometry
{
    public class GeometryRectangle
        : GeometryFigure
    {
        public ReferenceRectangle RefRect { get; set; }
        protected Vector2 dot1, dot2, dot3, dot4;

        public void Update(float angle, Vector2 position)
        {
            Vector2[] dots = { dot1, dot2, dot3, dot4 };
            // 1) находим центр прямоугольника
            Vector2 center = (dot1 + dot3) / 2; 
            // 2) поворачиваем все точки
            for (int i = 0; i < dots.Length; i++)
            {
                // векторная разница
                var diagonal = dots[i] - center;
                float length = diagonal.Length();
                float alpha = (float) Math.Atan2(diagonal.Y, diagonal.X);
                // поворачиваем точку на угол (относительно центра)
                dots[i].X = (float)(length * Math.Cos(alpha + (angle)));
                dots[i].Y = (float)(length * Math.Sin(alpha + (angle)));
            }
            // 4) ищем мин X, Y и макс X, Y
            float minX = dots[0].X;
            float minY = dots[0].Y;
            float maxX = dots[2].X;
            float maxY = dots[2].Y;
            for (int i = 0; i < dots.Length; i++)
            {
                if (dots[i].X <= minX) minX = dots[i].X;
                if (dots[i].Y <= minY) minY = dots[i].Y;
                if (dots[i].X >= maxX) maxX = dots[i].X;
                if (dots[i].Y >= maxY) maxY = dots[i].Y;
            }
            // 5) выставляем точкам min и max        
            dots[0].X = minX;
            dots[0].Y = minY;
            dots[1].X = maxX;
            dots[1].Y = minY;
            dots[2].X = maxX;
            dots[2].Y = maxY;
            dots[3].X = minX;
            dots[3].Y = maxY;
            for (int i = 0; i < edges.Count; i++)
            {
                dots[i] = dots[i] + position;
            }
            // 6) назначаем прямогульнику новые точки
            for (int i = 0; i < edges.Count-1; i++)
            {
                edges[i].StartDot = dots[i];
                edges[i].EndDot = dots[i+1];
            }
            edges[edges.Count - 1].StartDot = dots[3];
            edges[edges.Count - 1].EndDot = dots[0];

            RefRect.UpdatePoints(dots);
        }


        protected List<GeometryLine> edges;
        protected Rectangle rectangle;

        public override Color Color
        {
            get
            {
                return base.Color;
            }
            set
            { 
                base.Color = value;
                foreach (var edge in edges)
                {
                    edge.Color = value;
                }
            }
        }

        public override int LineWidth
        {
            get
            {
                return base.LineWidth;
            }
            set
            {
                base.LineWidth = value;
                foreach (var edge in edges)
                {
                    edge.LineWidth = value;
                }
            }
        }

        public GeometryRectangle() : base()
        {
            edges = new List<GeometryLine>();
        }

        public GeometryRectangle(Rectangle rect) : this()
        {
            this.rectangle = rect;
            RefRect = new ReferenceRectangle(rect);
            dot1 = RefRect.Dot1;
            dot2 = RefRect.Dot2;
            dot3 = RefRect.Dot3;
            dot4 = RefRect.Dot4;

            var edge1 = new GeometryLine(RefRect.Dot1, RefRect.Dot2);
            var edge2 = new GeometryLine(RefRect.Dot2, RefRect.Dot3);
            var edge3 = new GeometryLine(RefRect.Dot3, RefRect.Dot4);
            var edge4 = new GeometryLine(RefRect.Dot4, RefRect.Dot1);

            edges.Add(edge1);
            edges.Add(edge2);
            edges.Add(edge3);
            edges.Add(edge4);
        }


        public override void Draw(SpriteBatch batch)
        {
            foreach (var edge in edges)
            {
                edge.Draw(batch);
            }
        }

        public override void Transposition(Vector2 delta)
        {
            foreach (var edge in edges)
            {
                edge.Transposition(delta);
            }
        }
    }
}
