using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectBreaker
{
    internal class Bar
    {
        private Texture2D _texture2D;
        private Vector2 _vector2;

        public Bar() {}

        public void Init(Texture2D ptexture2D, Vector2 pvector2)
        {
            _texture2D = ptexture2D;
            _vector2 = pvector2;
        }
        public void Update(GameTime pgameTime) 
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Q))
            {
                _vector2.X -= 15;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                _vector2.X += 15;
            }
        }
        public void Draw(GameTime pgameTime,SpriteBatch pspriteBatch)
        {
            pspriteBatch.Begin();
            pspriteBatch.Draw(this._texture2D,this._vector2,Color.White);
            pspriteBatch.End();
        }

    }
}
