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
    public class GeometryManager
        : DrawableGameComponent
    {
        protected List<GeometryFigure> figures;
        protected SpriteBatch spriteBatch;

        public GeometryManager(Game game) 
            : base(game)
        {
            game.Components.Add(this);
            figures = new List<GeometryFigure>();
        }

        public void AddFigure(GeometryFigure figure)
        {
            figures.Add(figure);
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            spriteBatch.Begin();
            foreach (var fig in figures)
            {
                fig.Draw(spriteBatch);
            }
            spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            foreach (var fig in figures)
            {
                fig.Update(gameTime);
            }
        }
    }
}
