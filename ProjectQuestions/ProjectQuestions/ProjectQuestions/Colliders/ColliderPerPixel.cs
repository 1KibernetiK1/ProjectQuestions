using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ProjectQuestions.Helpers;

namespace ProjectQuestions.Colliders
{
    public class ColliderPerPixel : ColliderRectangle
    {
        public Texture2D Texture { get; set; }
        public Color[] Pixels { get; set; }

        public ColliderPerPixel(ReferenceRectangle rectangle, Texture2D texture)
            : base(rectangle)
        {
            this.Texture = texture;
            // из текстуры извлекаем точки
            Pixels = new Color[texture.Width * texture.Height];
            texture.GetData<Color>(Pixels);
        }

        public override bool IsCollision(ColliderBase collider)
        {
            var colliderRectangle = collider as ColliderRectangle;
            if (colliderRectangle == null) return false;

            ReferenceRectangle rect1 = this.bounds;
            ReferenceRectangle rect2 = colliderRectangle.Bounds;

            if (rect1.IsIntersect(rect2))
            {
                // проверяем попиксельно...
                ColliderPerPixel collider1 = this;
                var collider2 = collider as ColliderPerPixel;
                if (collider2 == null) return false;

                return IsPerPixelCollision(collider1, collider2);
            }

            return false;
        }

        protected virtual bool IsPerPixelCollision(ColliderPerPixel collider1, ColliderPerPixel collider2)
        {
            Color[] pixels1 = collider1.Pixels;
            Color[] pixels2 = collider2.Pixels;
            ReferenceRectangle rect1 = collider1.Bounds;
            ReferenceRectangle rect2 = collider2.Bounds;

            for (int i1 = 0; i1 < pixels1.Length; i1++)
            {
                Color color1 = pixels1[i1];
                if (color1 == Color.Transparent) continue;
                // 1) вычисляем координаты этого пикселя на экране
                int x1 = (int)(i1 % rect1.Width + rect1.Left);
                int y1 = (int)(i1 / rect1.Width + rect1.Top);
                // 2) находим строку и столбец соотвеств точки из 2-й текстуре
                int col2 = (int)(x1 - rect2.Left);
                int row2 = (int)(y1 - rect2.Top);
                // 3) отсекаем несовпадение
                if (col2 < 0 || row2 < 0 ||
                    col2 > rect2.Width || row2 > rect2.Height)
                    continue;
                // 4) находим цвет точки из 2-й текстуры
                int i2 = (int)(row2 * rect2.Width + col2);

                if (i2 > pixels2.Length - 1) continue;
                Color color2 = pixels2[i2];
                // 5) сверка цветов точек

                if (color2 != Color.Transparent)
                    return true;
            }

            return false;
        }
    }
}
