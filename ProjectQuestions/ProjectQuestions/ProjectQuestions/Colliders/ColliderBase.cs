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
    public abstract class ColliderBase
    {
        public virtual bool CollisionWithRectangle(Rectangle rect)
        {
            float left = Math.Max(bounds.Left, rect.Left);
            float right = Math.Min(bounds.Right, rect.Right);
            float top = Math.Max(bounds.Top, rect.Top);
            float bottom = Math.Min(bounds.Bottom, rect.Bottom);

            int width = (int)(right - left);
            int height = (int)(bottom - top);

            return width >= 0 &&
                height >= 0 &&
                width <= (bounds.Width + rect.Width) &&
                height <= (bounds.Height + rect.Height);
        }


        public virtual void Update(Texture2D texture)
        {

        }

        protected ReferenceRectangle bounds;
        public ReferenceRectangle Bounds
        {
            get { return bounds; }
            set { bounds = value; }
        }

        public virtual Rectangle DefineRectangleIntersect(ColliderBase collider)
        {
            return Rectangle.Empty;
        }

        public abstract bool IsCollision(ColliderBase collider);
        public abstract void Update(Vector2 position);
        public abstract void SetScale(float scale);
    }
}
