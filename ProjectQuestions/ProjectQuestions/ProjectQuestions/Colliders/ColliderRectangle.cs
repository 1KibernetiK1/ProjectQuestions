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
    public class ColliderRectangle : ColliderBase
    {
        public override bool IsCollision(ColliderBase collider)
        {
            var another = (ColliderRectangle)collider;

            return this.Bounds.IsIntersect(another.Bounds);
        }

        public override void SetScale(float scale)
        {
            bounds.Width = (int)(scale * bounds.Width);
            bounds.Height = (int)(scale * bounds.Height);
        }

        public override void Update(Vector2 position)
        {

        }

        public ColliderRectangle(ReferenceRectangle rectangle)
        {
            this.bounds = rectangle;
        }

        public override Rectangle DefineRectangleIntersect(ColliderBase collider)
        {
            var colliderRectangle = collider as ColliderRectangle;
            if (colliderRectangle == null) return Rectangle.Empty;

            return bounds.DefineRectangleIntersect(colliderRectangle.bounds);
        }
    }
}
