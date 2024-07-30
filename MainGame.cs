using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ProjectBreaker
{
    public class MainGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private RenderTarget2D _renderTarget;

        public GamesState _gameState;

        public int TargetWidth;
        public int TargetHeight;
        public bool bSampling = false;
        public KeyboardState previousState;

        public MainGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            TargetWidth = 1280;
            TargetHeight = 720;

            _graphics.PreferredBackBufferWidth = TargetWidth;
            _graphics.PreferredBackBufferHeight = TargetHeight;

            _gameState = new GamesState(this);
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

            _gameState.ChangeScene(GamesState.SceneType.Menu);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (_gameState.currentScene != null) 
            {
                _gameState.currentScene.Update(gameTime);
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
            GraphicsDevice.Clear(Color.Black);

            if (_gameState.currentScene != null)
            {
                _gameState.currentScene.Draw(gameTime);
            }

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
