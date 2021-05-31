using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ProjectQuestions.Units;

namespace ProjectQuestions.GameMap
{
    class MapEngine : GameObjectBase
    {
        protected Game game;

        protected AlgorithmPathSearch pathSearch;

        protected int[] cellTypes = { 29, 9 };

        protected SpriteBatch spriteBatch;
        protected Texture2D textureTileSet;
        protected Rectangle cellRectangle;
        protected int columnSet;
        protected int rowSet;
        protected int cellWidth;
        protected int cellHeight;

        protected int[,] map =
        {
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 1, 0, 0, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 1, 1, 1, 1, 0, 1, 1, 1, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }
        };

        public List<Vector2> FindWay(Vector2 p1, Vector2 p2)
        {
            var start = new Point();
            var finish = new Point();

            start.X = (int)(p1.X / cellWidth);
            start.Y = (int)(p1.Y / cellHeight);

            finish.X = (int)(p2.X / cellWidth);
            finish.Y = (int)(p2.Y / cellHeight);

            var points = pathSearch.FindWay(start, finish);
            var vectors = new List<Vector2>();

            foreach (var pt in points)
            {
                var v = new Vector2();
                v.X = (pt.X) * cellWidth + cellWidth / 2;
                v.Y = (pt.Y) * cellHeight + cellHeight / 2;

                vectors.Add(v);
            }

            return vectors;
        }

        public void SetAlgorithm(AlgorithmPathSearch algorithm)
        {
            pathSearch = algorithm;
            pathSearch.SetMap(map);
        }

        public MapEngine(Game game, int columns, int rows)
        {
            this.game = game;
            columnSet = columns;
            rowSet = rows;
        }

        public void LoadContent()
        {
            spriteBatch = new SpriteBatch(game.GraphicsDevice);
            using (var fs = new FileStream("tilesMap.jpg", FileMode.Open))
            {
                textureTileSet = Texture2D.FromStream(game.GraphicsDevice, fs);
            }

            cellWidth = textureTileSet.Width / columnSet;
            cellHeight = textureTileSet.Height / rowSet;

            cellRectangle = new Rectangle(0, 0, cellWidth, cellHeight);
        }

        public override void Draw(SpriteBatch batch)
        {
            Vector2 position = new Vector2(0, 0);
            spriteBatch.Begin();
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    int cellType = cellTypes[map[i, j]];

                    int row = cellType / columnSet;
                    int column = cellType % columnSet;

                    cellRectangle.X = column * cellWidth;
                    cellRectangle.Y = row * cellHeight;

                    spriteBatch.Draw(
                        textureTileSet,
                        position,
                        cellRectangle,
                        Color.White);

                    position.X += cellWidth;
                }
                // переходим на следующую строку
                position.Y += cellHeight;
                position.X = 0;
            }
            spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {

        }

        private static Point[] shift =
        {
            new Point(0, 0),
            new Point(-1, -1),
            new Point(0, -1),
            new Point(1, -1),
            new Point(1,  0),
            new Point(1, 1),
            new Point(0,  1),
            new Point(-1, 1),
            new Point(-1, 0)
        };

        public void MapCheckCollision(GameUnit unit)
        {
            // переведём координаты персонажа (экранные)
            // в номер строки и столбца ячейки карты
            int column = (int)(unit.Position.X / cellWidth);
            int row = (int)(unit.Position.Y / cellHeight);

            var collider = unit.Collider;

            for (int i = 0; i < shift.Length; i++)
            {
                int c = column + shift[i].X;
                int r = row + shift[i].Y;

                // смотрим тип ячейки
                if (c < 0 || c > map.GetLength(1) - 1 ||
                    r < 0 || r > map.GetLength(0) - 1) continue;

                int cellType = cellTypes[map[r, c]];

                // в зависимости от типа ячейки - воздействие на персонаж
                if (cellType == 29) continue; // нет столкновения

                cellRectangle.X = c * cellWidth;
                cellRectangle.Y = r * cellHeight;

                if (collider.CollisionWithRectangle(cellRectangle))
                {
                    unit.Rollback();
                    break;
                }
            }
        }
    }
}
