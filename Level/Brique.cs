using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using SharpDX.Direct3D9;
using System.Diagnostics;

namespace ProjectBreaker
{
    public class Brique : GameObject
    {
        private Dictionary<int, Rectangle> _typeBri;
        public int type;
        private int _hp;
        private int _hpMax = 10;
        public bool _collider = false;
        public bool takeDamage = false;
        public Buff buff;
        public Brique(Vector2 position,int type) : base("Brique")
        {
            this.type = type;
            switch (this.type)
            {
                case 4:
                    this.texture2D = ServiceLocator.GetService<ContentManager>().Load<Texture2D>("Brique/coffre");
                    this.sizeW = this.texture2D.Width;
                    this.sizeH = this.texture2D.Height;
                    this.position = position;
                    this.position.X *= sizeW + 17;
                    this.position.Y *= sizeH + 10;
                    this._typeBri = new Dictionary<int, Rectangle>();
                    this._typeBri[0] = new Rectangle(0,0, this.sizeW, this.sizeH);
                    this.bounds = new Rectangle((int)this.position.X, (int)this.position.Y, this.sizeW, this.sizeH);
                    this._hp = _hpMax;
                    this.buff = new Buff(new Vector2 (this.bounds.Center.X, this.bounds.Center.Y));
                    this.type = 0;
                    break;
                default:
                    this.texture2D = ServiceLocator.GetService<ContentManager>().Load<Texture2D>("Brique/Brique2");
                    this.sizeW = (int)(this.texture2D.Width * 0.5f);
                    this.sizeH = (int)(this.texture2D.Height * 0.5f);
                    this.position = position;
                    this.position.X *= sizeW + 10;
                    this.position.Y *= sizeH + 10;
                    this._typeBri = new Dictionary<int, Rectangle>();
                    this._typeBri[0] = new Rectangle(0, 0, this.sizeW, this.sizeH);
                    this._typeBri[1] = new Rectangle(this.sizeW, 0, this.sizeW, this.sizeH);
                    this._typeBri[2] = new Rectangle(0, this.sizeH, this.sizeW, this.sizeH);
                    this._typeBri[3] = new Rectangle(this.sizeW, this.sizeH, this.sizeW, this.sizeH);
                    this.bounds = new Rectangle((int)this.position.X, (int)this.position.Y, this.sizeW, this.sizeH);
                    this._hp = _hpMax * (type + 1);
                    break;
            }
        }

        public override void Update(GameTime gameTime)
        {

            base.Update(gameTime);
        }

        public override void Draw()
        {
            ServiceLocator.GetService<SpriteBatch>().Draw(this.texture2D, this.bounds, this._typeBri[type], Color.White * (float)(this._hp / (this._hpMax * (type + 1f))));
            //Brique.DrawRectangle(ServiceLocator.GetService<SpriteBatch>(), this.bounds, Color.White, 1);
        }
       
        public bool TakeDamage(int damage = 0)
        {
            if (damage > 0)
            {
                List<GameObject> LstBrique = GameObject.FindByName("Brique");
                foreach (Brique b in LstBrique)
                {
                    b.takeDamage = false;
                }
                this.takeDamage = true;
                this._hp -= damage;
                ServiceLocator.GetService<TextManager>().AddNewText("- " + damage.ToString(),Color.Red,new Vector2(this.bounds.Center.X,this.bounds.Top),true);
                if (this._hp <= 0)
                {
                    if (this.buff != null)
                    {
                        this.buff.active = true;
                    }
                    return true;
                }
                
            }
            return false;
        }
    }
}
