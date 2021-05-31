using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ProjectQuestions.GameMap
{
    public class GameEngine : DrawableGameComponent
    {
        protected ManagerGameScreens _managerScreens;

        protected MapEngine map;

        protected MouseGameObject mouse;

        protected UiManager ui;

        protected List<LightSource> lightSources;

        public GameBackground Background { get; set; }

        protected GeometryFillRectangle intersect;

        protected GeometryManager geometry;

        protected SpriteBatch spriteBatch;
        protected List<ControllerBase> controllers;
        protected List<GameObject> gameObjects;

        // список юнитов, для которых проверка столкновений
        protected List<GameUnit> units;

        public void AddGameScreen(GameScreen screen)
        {
            _managerScreens.Add(screen);
        }

        protected void InitializeUi()
        {
            ui.PointerDelta = new Vector2(-20, -35);

            var wnd = new UiWindowMessage(ui);
            wnd.Position = new Vector2(600, 100);

            var btn1 = new UiButton();
            btn1.Position = new Vector2(200, 200);
            btn1.Text = "Принять";
            btn1.SetSize(70, 25);
            btn1.Click += Btn1_Click;
            // btn1.Texture = texWooden;

            ui.AddControl(btn1);

            var btn2 = new UiButton();
            btn2.Position = new Vector2(300, 200);
            btn2.Text = "Отмена";
            btn2.SetSize(70, 25);
            ui.AddControl(btn2);
        }

        protected int counter;
        private void Btn1_Click(UiControlBase control, Vector2 position)
        {
            counter++;
            control.Text = "Принять " + counter.ToString();
        }

        public GameEngine(Game game, bool useMap = false)
            : base(game)
        {
            if (useMap)
            {
                map = new MapEngine(game, 8, 6);
                map.SetAlgorithm(new AlgorithmWaves());
            }

            geometry = new GeometryManager(game);
            ui = new UiManager(game);

            GlobalsItems.BulletsManager = new Armory.ManagerBullets();
            game.Components.Add(this);
            controllers = new List<ControllerBase>();
            gameObjects = new List<GameObject>();
            units = new List<GameUnit>();
            lightSources = new List<LightSource>();

            // включаем менеджер экранов
            _managerScreens = new ManagerGameScreens();
            var engineScreen = new GameScreenEngine(EngineDraw, EngineUpdate);
            _managerScreens.Add(engineScreen);
        }

        public void AddLight(LightSource light)
        {
            lightSources.Add(light);
        }

        public override void Initialize()
        {
            base.Initialize();
            intersect = new GeometryFillRectangle(new Rectangle(10, 10, 10, 10));
            intersect.Color = Color.Red;
            geometry.AddFigure(intersect);

            // InitializeUi();
        }

        private void CheckColissions()
        {
            // проверяем столкновение каждый с каждый
            for (int i = 0; i < units.Count; i++)
            {
                GameUnit unitI = units[i];
                ColliderBase colliderI = unitI.Collider;

                if (map != null)
                {
                    map.MapCheckCollision(unitI);
                }

                if (unitI.HasChanges)
                {
                    for (int j = 0; j < units.Count; j++)
                    {
                        if (j == i) continue;
                        GameUnit unitJ = units[j];
                        ColliderBase colliderJ = unitJ.Collider;

                        var rect = colliderI.DefineRectangleIntersect(colliderJ);
                        intersect.Modify(rect);

                        if (colliderI.IsCollision(colliderJ))
                        {
                            // столкновение
                            Vector2 delta = unitI.Rollback();
                            break;
                        }
                    }
                }
            }
        }

        private void Add(GameObject obj)
        {
            if (obj is GameUnitPathfinder)
            {
                var unit = obj as GameUnitPathfinder;
                unit.SetMap(map);
            }

            if (obj is MouseGameObject)
            {
                mouse = obj as MouseGameObject;
                return;
            }

            var fig = new GeometryBoundingBox(obj);
            geometry.AddFigure(fig);

            if (obj is GameUnit)
            {
                GameUnit unit = obj as GameUnit;
                unit.Batch = this.spriteBatch;
                units.Add(unit);
                // var collider = new ColliderPerPixel(fig.RefRect, unit.Texture);
                var collider = new ColliderRectangle(fig.RefRect);
                // var collider = new ColliderPerPixelAdvanced(fig.RefRect, unit.Texture);
                unit.BindCollider(collider);
                unit.UpdateRenderTarget();
            }
            gameObjects.Add(obj);
        }

        public void AddObject(GameObject obj)
        {
            Add(obj);
        }

        public void AddObject(GameObject obj, ControllerBase controller)
        {
            Add(obj);
            controllers.Add(controller);
            controller.Attach(obj);
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            map.LoadContent();
        }

        protected void EngineUpdate(GameTime gameTime)
        {
            if (Background != null)
                Background.Update(gameTime);

            CheckColissions();
            base.Update(gameTime);

            GlobalsItems.BulletsManager.Update(gameTime);
            foreach (var obj in gameObjects)
            {
                obj.Update(gameTime);
            }
            foreach (var c in controllers)
            {
                c.Update(gameTime);
            }
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();
            if (ks.IsKeyDown(Keys.Space))
            {
                _managerScreens.SetActiveScreen(EnumGameScreen.PauseType);
            }
            else
            if (ks.IsKeyDown(Keys.Escape))
            {
                _managerScreens.SetActiveScreen(EnumGameScreen.GameEngineType);
            }

            _managerScreens.Update(gameTime);
        }

        protected GameTime currentGameTime;

        protected void EngineDraw(SpriteBatch batch)
        {
            base.Draw(currentGameTime);
            map.Draw(batch);

            if (Background != null)
                Background.Draw(spriteBatch);

            spriteBatch.Begin(/*SpriteSortMode.Deferred, BlendState.Additive,null,null,null,null*/);
            foreach (var obj in gameObjects)
            {
                obj.Draw(spriteBatch);
            }
            GlobalsItems.BulletsManager.Draw(spriteBatch);
            spriteBatch.End();

            foreach (var l in lightSources)
            {
                l.Draw(spriteBatch);
            }

            ui.DrawUi();

            spriteBatch.Begin();
            mouse.Draw(spriteBatch);
            spriteBatch.End();
        }

        public override void Draw(GameTime gameTime)
        {
            currentGameTime = gameTime;
            _managerScreens.Draw(spriteBatch);
        }
    }
}
