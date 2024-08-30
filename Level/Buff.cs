using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectBreaker
{
    public class Buff : GameObject
    {
        private int _minBuff = 1;
        private int _maxBuff = 4;
        public bool active = false;
        private Vector2 _origin;
        public enum type
        {
            Power,
            Ball,
            Lucky,
            Star,

        }
        public type typeB;
        public Buff(Vector2 pposition) : base("Buff") 
        { 
            Random random = new Random();
            int buffR = random.Next(_minBuff, _maxBuff);
            switch (buffR)
            {
                case 1:
                    this.texture2D = ServiceLocator.GetService<ContentManager>().Load<Texture2D>("Buff/battle");
                    this.typeB = type.Power;
                    break;
                case 2:
                    this.texture2D = ServiceLocator.GetService<ContentManager>().Load<Texture2D>("Buff/lucky");
                    this.typeB = type.Lucky;
                    break;
                case 3:
                    this.texture2D = ServiceLocator.GetService<ContentManager>().Load<Texture2D>("Buff/rune");
                    this.typeB = type.Ball;
                    break;
                case 4 :
                    this.texture2D = ServiceLocator.GetService<ContentManager>().Load<Texture2D>("Buff/star");
                    this.typeB = type.Star;
                    break;
            }
            this.sizeW = this.texture2D.Width;
            this.sizeH = this.texture2D.Height;
            this.position = pposition;
            this.bounds = new Rectangle((int)this.position.X, (int)this.position.Y, this.sizeW, this.sizeH);
            this._origin = new Vector2(this.sizeW * 0.5f, this.sizeH * 0.5f);
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (this.active)
            {
                this.position.Y += 2 ;
            }
        }
        public override void Draw()
        {
            if (this.active) 
            {
                ServiceLocator.GetService<SpriteBatch>().Draw(this.texture2D, this.bounds, null, Color.White,0,this._origin,SpriteEffects.None,0);
            }
        }
    }
}
