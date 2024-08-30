using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ProjectBreaker.DesignPartern;

namespace ProjectBreaker
{
    internal class Ball : GameObject
    {
        public int damage;
        public bool starPower = false;
        private float timerStarPower = 0;
        private float maxtimer = 10;
        private Texture2D _effectStarPower;
        private AnimationManager _animationManager;
        public Ball() : base("Ball")
        {
            this.texture2D = ServiceLocator.GetService<ContentManager>().Load<Texture2D>("ball");
            this._effectStarPower = ServiceLocator.GetService<ContentManager>().Load<Texture2D>("starPower");
            this.damage = 10;
            this.sizeH = this.texture2D.Height;
            this.sizeW = this.texture2D.Width;
            this.position = new Vector2(MainGame.TargetWidth * 0.5f,MainGame.TargetHeight * 0.5f);
            this.bounds = new Rectangle((int)this.position.X, (int)this.position.Y,this.texture2D.Width,this.texture2D.Height);
            this.direction = new Vector2(1, 1);
            _animationManager = new(this._effectStarPower,60, 0.1f, 5, 12);
            _animationManager.Stop();
        }


        public override void Update(GameTime gameTime)
        {
            Vector2 Collider = this.ColliderScreen();
            if (Collider != Vector2.Zero) 
            {
                if (Collider.X != 0) this.direction.X = -this.direction.X;
                if (Collider.Y != 0) this.direction.Y = -this.direction.Y;
                if (this.position.Y + this.sizeH >= MainGame.TargetHeight) this.enabled = false;
            }
            if (this.enabled) 
            { 
                if (this.starPower)
                {
                    _animationManager.Start();
                    this.timerStarPower += gameTime.ElapsedGameTime.Seconds;
                    if(this.timerStarPower > this.maxtimer)
                    {
                        this.starPower = false;
                    }
                }
                else
                {
                    _animationManager.Stop();
                }

                this.position.X += MathF.Cos(this._angle) * this._speed * this.direction.X;
                this.position.Y += MathF.Sin(this._angle) * this._speed * this.direction.Y;
            }
            _animationManager.Update(gameTime);
            base.Update(gameTime);
        }

        public override void Draw()
        {
            if (this.enabled)
            {
                ServiceLocator.GetService<SpriteBatch>().Draw(this.texture2D, this.position, Color.White);
                _animationManager.Draw(new Vector2(this.position.X + this.sizeW * 0.5f, this.position.Y + this.sizeH * 0.5f));
            }
        
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
                    List<GameObject> LstBrique = GameObject.FindByName("Brique");
                    foreach (Brique b in LstBrique)
                    {
                        b.takeDamage = false;
                    }
                    break;
                case "ProjectBreaker.Brique":
                    Brique bg = (Brique)g;
                    if (!bg.takeDamage)
                    {
                        bg._collider = true;
                        var depthX = Math.Min(this.bounds.Right - g.bounds.Left, this.bounds.Right - g.bounds.Left);
                        var depthY = Math.Min(this.bounds.Bottom - g.bounds.Top, this.bounds.Bottom - g.bounds.Top);

                        if (!this.starPower)
                        {
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
                        }
                        
                        if (bg.TakeDamage(this.damage))
                        {
                            bg.OnDisable();
                        }
                    }
                    break;
            }
        }
    }
}
