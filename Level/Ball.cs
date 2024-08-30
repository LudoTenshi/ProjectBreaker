using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectBreaker
{
    internal class Ball : GameObject
    {
        private int _damage;
        public Ball() : base("Ball")
        {
            this.texture2D = ServiceLocator.GetService<ContentManager>().Load<Texture2D>("ball");
            this._damage = 10;
            this.sizeH = this.texture2D.Height;
            this.sizeW = this.texture2D.Width;
            this.position = new Vector2(MainGame.TargetWidth * 0.5f,MainGame.TargetHeight * 0.5f);
            this.bounds = new Rectangle((int)this.position.X, (int)this.position.Y,this.texture2D.Width,this.texture2D.Height);
            this.direction = new Vector2(1, 1);
        }


        public override void Update(GameTime gameTime)
        {
            Vector2 Collider = this.ColliderScreen();
            if (Collider != Vector2.Zero) 
            {
                if (Collider.X != 0) this.direction.X = -this.direction.X;
                if (Collider.Y != 0) this.direction.Y = -this.direction.Y;
                
            }

            this.position.X += MathF.Cos(this._angle) * this._speed * this.direction.X;
            this.position.Y += MathF.Sin(this._angle) * this._speed * this.direction.Y;

            base.Update(gameTime);
        }

        public override void Draw()
        {
            ServiceLocator.GetService<SpriteBatch>().Draw(this.texture2D, this.position, Color.White);
        }

        public override void ColliderEffect(GameObject g)
        {
            switch (g.GetType().ToString()) 
            {
                case "ProjectBreaker.Bar":

                    float BallImpact = this.bounds.Center.X - g.bounds.Left;
                    float angleImpact = (float)-((MathF.PI / 180f) * (179f * BallImpact / (g.bounds.Right - g.bounds.Left) - 180 ));

                    this._angle = angleImpact;
                    if (this.direction.Y > 0)
                    {
                        this.direction.Y = -this.direction.Y;
                    }
                    this.direction.X = -this.direction.X;
                    break;
                case "ProjectBreaker.Brique":
                    Brique bg = (Brique)g;
                    if (!bg.takeDamage)
                    {
                        bg._collider = true;
                        var depthX = Math.Min(this.bounds.Right - g.bounds.Left, this.bounds.Right - g.bounds.Left);
                        var depthY = Math.Min(this.bounds.Bottom - g.bounds.Top, this.bounds.Bottom - g.bounds.Top);

                        if (depthX > depthY)
                        {
                            if ((this.bounds.Right > g.bounds.Left && this.bounds.Left < g.bounds.Left)
                                || (this.bounds.Left < g.bounds.Right && this.bounds.Right > g.bounds.Right))
                            {
                                this.direction.X = -this.direction.X;
                            }
                            this.position.X += MathF.Cos(this._angle) * this._speed * this.direction.X;
                        }
                        else
                        {
                            if ((this.bounds.Bottom > g.bounds.Top && this.bounds.Top < g.bounds.Top)
                                || (this.bounds.Top < g.bounds.Bottom && this.bounds.Bottom > g.bounds.Bottom))
                            {
                                this.direction.Y = -this.direction.Y;
                            }
                            this.position.Y += MathF.Sin(this._angle) * this._speed * this.direction.Y;
                        }
                    
                        if (bg.TakeDamage(this._damage))
                        {
                            bg.OnDisable();
                        }
                    }
                    break;
            }
        }
    }
}
