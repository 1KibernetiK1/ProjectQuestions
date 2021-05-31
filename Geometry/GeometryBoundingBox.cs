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
    public class GeometryBoundingBox 
        : GeometryRectangle
    {
        protected GameObject gameObject;

        public GeometryBoundingBox(GameObject gameObject)
            : base(gameObject.Bounds)
        {
            this.gameObject = gameObject;
        }

        public override void Update(GameTime time)
        {
            //if (gameObject.HasChanges)
            {
                this.Update(gameObject.Angle, gameObject.Position);
            }
        }

        public override void Draw(SpriteBatch batch)
        {
            if (gameObject.ShowBoundingBox)
            {
                base.Draw(batch);
            }
        }
    }
}
