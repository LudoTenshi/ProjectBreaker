using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ProjectBreaker
{
    public class MainGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private RenderTarget2D _renderTarget;


        public static int TargetWidth;
        public static int TargetHeight;
        public bool bSampling = false;
        public KeyboardState previousState;

        public MainGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            TargetWidth = 980;
            TargetHeight = 1280;

            _graphics.PreferredBackBufferWidth = TargetWidth;
            _graphics.PreferredBackBufferHeight = TargetHeight;

        }

        protected override void Initialize()
        {
            PresentationParameters pp = _graphics.GraphicsDevice.PresentationParameters;
            _renderTarget = new RenderTarget2D(_graphics.GraphicsDevice,
                TargetWidth, TargetHeight,
                false,
                SurfaceFormat.Color,
                DepthFormat.None,
                pp.MultiSampleCount,
                RenderTargetUsage.DiscardContents);

            

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            ServiceLocator.RegisterService<ContentManager>(Content);
            ServiceLocator.RegisterService<SpriteBatch>(_spriteBatch);

            TextManager textManager = new TextManager();
            ServiceLocator.RegisterService<TextManager>(textManager);

            GamesState.ChangeScene(GamesState.SceneType.Menu);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape)) Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.Space) && !previousState.IsKeyDown(Keys.Space) && GamesState.currentSceneType == GamesState.SceneType.Menu)
            {
                GamesState.ChangeScene(GamesState.SceneType.Game);
            }
            if (GamesState.currentScene != null) 
            {
                GamesState.currentScene.Update(gameTime);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S) && !previousState.IsKeyDown(Keys.S))
            {
                bSampling = !bSampling;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.F) && !previousState.IsKeyDown(Keys.F))
            {
                if (!_graphics.IsFullScreen)
                {
                    _graphics.PreferredBackBufferWidth = _graphics.GraphicsDevice.DisplayMode.Width;
                    _graphics.PreferredBackBufferHeight = _graphics.GraphicsDevice.DisplayMode.Height;
                }
                else
                {
                    _graphics.PreferredBackBufferWidth = TargetWidth;
                    _graphics.PreferredBackBufferHeight = TargetHeight;
                }

                _graphics.ToggleFullScreen();
            }

            previousState = Keyboard.GetState();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {

            GraphicsDevice.SetRenderTarget(_renderTarget);
            GraphicsDevice.Clear(Color.Black);
            ServiceLocator.GetService<SpriteBatch>().Begin();

            if (GamesState.currentScene != null)
            {
                GamesState.currentScene.Draw();
            }
            ServiceLocator.GetService<SpriteBatch>().End();
            GraphicsDevice.SetRenderTarget(null);
            RenderDraw();

            base.Draw(gameTime);
        }

        protected void RenderDraw()
        {
            float ratio = 1;
            int marginV = 0;
            int marginH = 0;
            float currentAspect = Window.ClientBounds.Width / (float)Window.ClientBounds.Height;
            float virtualAspect = (float)TargetWidth / (float)TargetHeight;
            if (TargetHeight != this.Window.ClientBounds.Height)
            {
                if (currentAspect > virtualAspect)
                {
                    ratio = Window.ClientBounds.Height / (float)TargetHeight;
                    marginH = (int)((Window.ClientBounds.Width - TargetWidth * ratio) / 2);
                }
                else
                {
                    ratio = Window.ClientBounds.Width / (float)TargetWidth;
                    marginV = (int)((Window.ClientBounds.Height - TargetHeight * ratio) / 2);
                }
            }

            Rectangle dst = new Rectangle(marginH, marginV, (int)(TargetWidth * ratio), (int)(TargetHeight * ratio));

            if (!bSampling)
                _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, null, null);
            else
                _spriteBatch.Begin();

            _spriteBatch.Draw(_renderTarget, dst, Color.White);

            _spriteBatch.End();
        }
    }
}
