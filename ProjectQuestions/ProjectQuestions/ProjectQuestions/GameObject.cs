using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ProjectQuestions
{
    public class GameObject : GameObjectBase
    {
        public virtual void UpdateRenderTarget()
        {
        }

        public Rectangle Bounds
        {
            get
            {
                boundingBox.X = (int)(position.X - boundingBox.Width / 2);
                boundingBox.Y = (int)(position.Y - boundingBox.Height / 2);
                return boundingBox;
            }
        }

        private bool showBoundingBox;

        public bool ShowBoundingBox
        {
            get { return showBoundingBox; }
            set { showBoundingBox = value; }
        }

        protected Rectangle boundingBox;

        protected bool isPositionChanges;
        protected bool isAngleChanges;

        public Vector2 GetOffset()
        {
            return position - previousPosition;
        }

        public bool HasChanges
        {
            get
            {
                return IsPositionChanges || IsAngleChanges;
            }
        }

        public bool IsPositionChanges
        {
            get
            {
                if (isPositionChanges)
                {
                    // isPositionChanges = false;
                    return true;
                }
                return false;
            }
        }

        public bool IsAngleChanges
        {
            get
            {
                if (isAngleChanges)
                {
                    // isAngleChanges = false;
                    return true;
                }
                return false;
            }
        }

        protected Vector2 previousPosition;
        protected Vector2 deltaMove;
        protected long _lifeTime;

        // для корабля       
        protected Vector2 position;
        protected Vector2 velocity;
        protected float scale;
        protected float angle;
        protected Vector2 origin;

        public float Angle
        {
            get { return angle; }
            set
            {
                if (value != angle)
                {
                    isAngleChanges = true;
                    angle = value;
                    UpdateRenderTarget();
                }
            }
        }

        public void Move(Vector2 delta)
        {
            if (delta.X == 0 && delta.Y == 0) return;

            previousPosition = position;
            deltaMove = delta;
            position += delta;

            isPositionChanges = true;
        }

        public float Scale
        {
            get { return scale; }
            set
            {
                if (value != scale)
                {
                    scale = value;
                    boundingBox.Width = (int)scale * boundingBox.Width;
                    boundingBox.Height = (int)scale * boundingBox.Height;
                    isPositionChanges = true;
                    UpdateRenderTarget();
                }
            }
        }

        public Vector2 Position
        {
            get { return position; }
            set
            {
                if (value.X != position.X ||
                    value.Y != position.Y)
                {
                    //previousPosition = position;
                    position = value;
                    isPositionChanges = true;
                }
            }
        }

        public virtual void SetScale(float scale)
        {
            if (scale != this.scale)
            {
                this.scale = scale;
                boundingBox.Width = (int)(scale * boundingBox.Width);
                boundingBox.Height = (int)(scale * boundingBox.Height);
                isPositionChanges = true;
                UpdateRenderTarget();
            }
        }

        public void Rotate(int degrees)
        {
            angle += (float)(degrees * (Math.PI / 180));
            isAngleChanges = true;
            UpdateRenderTarget();
        }

        public void SetVelocity(float vx, float vy)
        {
            velocity.X = vx;
            velocity.Y = vy;
        }

        public void SetPosition(int x, int y)
        {
            position.X = x;
            position.Y = y;
            previousPosition = position;
            // isPositionChanges = true;
        }

        public GameObject(Texture2D texture) : base(texture)
        {
            isPositionChanges = false;
            _lifeTime = 0;
            position = new Vector2(0, 0);
            velocity = new Vector2(0f, 0f);
            scale = 1f;
            angle = 0;
            origin = new Vector2(
                texture.Width / 2,
                texture.Height / 2
                );
            boundingBox = new Rectangle(
                    -texture.Width / 2,
                    -texture.Height / 2,
                    texture.Width,
                    texture.Height);
        }

        public override void Update(GameTime gameTime)
        {
            _lifeTime += gameTime.ElapsedGameTime.Milliseconds;
            // физика прямолинейного движения
            if (velocity.X != 0 && velocity.Y != 0)
            {
                previousPosition = position;
                position += velocity;
                isPositionChanges = true;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            // spriteBatch.Draw(texSpaceship, position, Color.White);
            spriteBatch.Draw(
                texture, // картинка
                position, // координаты
                null, // прямоугольник - вырезание из картиника
                Color.White, // цвет смешивания
                angle, // угол поворота
                origin, // точка поворота
                scale, // коэф масшт
                SpriteEffects.None, // эффекты
                1 // слой
                );
        }
    }
}
