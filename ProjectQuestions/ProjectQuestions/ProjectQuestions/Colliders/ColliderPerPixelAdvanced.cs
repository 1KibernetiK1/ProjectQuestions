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
    class ColliderPerPixelAdvanced : ColliderPerPixel
    {
        public override void Update(Texture2D texture)
        {
            this.Texture = texture;
            Pixels = new Color[texture.Width * texture.Height];
            texture.GetData<Color>(Pixels);
        }

        public ColliderPerPixelAdvanced(ReferenceRectangle rectangle, Texture2D texture)
            : base(rectangle, texture)
        {
        }

        protected override bool IsPerPixelCollision(
            ColliderPerPixel collider1,
            ColliderPerPixel collider2)
        {
            Color[] pixels1 = collider1.Pixels;
            Color[] pixels2 = collider2.Pixels;
            ReferenceRectangle rect1 = collider1.Bounds;
            ReferenceRectangle rect2 = collider2.Bounds;

            Rectangle intersect = rect1.DefineRectangleIntersect(rect2);

            for (int row = 0; row < intersect.Height; row++)
            {
                for (int col = 0; col < intersect.Width; col++)
                {
                    int row1 = row + (int)(intersect.Top - rect1.Top);
                    int col1 = col + (int)(intersect.Left - rect1.Left);

                    if (row1 < 0 || col1 < 0) continue;

                    // достаём нужную точку из 1й текстуры
                    int i1 = row1 * (int)rect1.Width + col1;

                    if (i1 > pixels1.Length - 1) continue;

                    Color color1 = pixels1[i1];
                    if (color1 == Color.Transparent) continue;

                    // вычисляем коор-ты 1й точки на экране
                    int x1 = (int)rect1.Left + col1;
                    int y1 = (int)rect1.Top + row1;

                    // 2) находим соотв точку на 2-й текстуре
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
                    // 5)
                    if (color2 != Color.Transparent)
                    {
                        pixels2[i2] = Color.Red;
                        collider2.Texture.SetData<Color>(pixels2);
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
