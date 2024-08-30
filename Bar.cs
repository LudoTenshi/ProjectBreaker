using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace ProjectBreaker
{
    internal class Bar : GameObject
    {
        public Bar(Vector2 pvector2) : base("Bar")
        {
            this.texture2D = ServiceLocator.GetService<ContentManager>().Load<Texture2D>("Barre"); ;
            this.position = pvector2;
            this.sizeH = this.texture2D.Height;
            this.sizeW = this.texture2D.Width;
            this.position.X -= this.texture2D.Width * 0.5f;
            this.position.Y -= this.texture2D.Height;
            this.bounds = new Rectangle((int)this.position.X, (int)this.position.Y, this.texture2D.Width, this.texture2D.Height);
            
        }

        public override void Update(GameTime pgameTime) 
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Q))
            {
                position.X -= 15;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                position.X += 15;
            }

            Vector2 collider = this.ColliderScreen();
            
            if(collider.X == 1)
            {
                position.X = 0;
            }
            if(collider.X == -1)
            {
                position.X = MainGame.TargetWidth - texture2D.Width;
            }
            base.Update(pgameTime);
        }
        public override void Draw()
        {
            ServiceLocator.GetService<SpriteBatch>().Draw(this.texture2D,this.position,Color.White);
        }

    }
}
