using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectBreaker.DesignPartern
{
    class AnimationManager
    {
        private readonly Texture2D _texture;
        private readonly Vector2 _origin;
        private readonly List<Rectangle> _sourceRectangles = new List<Rectangle>();
        private readonly int _frames;
        private int _frame;
        private readonly float _frameTime;
        private float _frameTimeLeft;
        private bool _active = true;

        public AnimationManager(Texture2D texture, int frames, float frameTime,int col, int row = 1)
        {
            this._texture = texture;
            this._frameTime = frameTime;
            this._frameTimeLeft = this._frameTime;
            this._frames = frames;
            var frameWidth = this._texture.Width / col;
            var frameHeight = this._texture.Height / row;
            this._origin = new Vector2(frameWidth *0.5f, frameHeight * 0.5f);
            for (int i = 0; i < row; i++) 
            {
                for (int j = 0; j < col; j++)
                {
                    this._sourceRectangles.Add(new(j * frameWidth,i * frameHeight, frameWidth, frameHeight));
                }
            }
        }

        public void Stop()
        {
            _active = false;
        }

        public void Start()
        {
            _active = true;
        }

        public void Reset()
        {
            _frame = 0;
            _frameTimeLeft = _frameTime;
        }

        public void Update(GameTime gameTime)
        {
            if (!_active) return;

            _frameTimeLeft -= (float)gameTime.ElapsedGameTime.TotalSeconds; ;

            if (_frameTimeLeft <= 0)
            {
                _frameTimeLeft += _frameTime;
                _frame = (_frame + 1) % _frames;
            }
        }

        public void Draw(Vector2 pos)
        {
            ServiceLocator.GetService<SpriteBatch>().Draw(this._texture, pos, this._sourceRectangles[_frame], Color.White, 0, this._origin, Vector2.One, SpriteEffects.None, 1);
        }
    }
}
