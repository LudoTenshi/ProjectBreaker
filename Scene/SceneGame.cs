using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace ProjectBreaker
{
    internal class SceneGame : SceneManager
    {
        private Bar _bar;
        private LvlGenerate _lvlGenerate;
        public SceneGame()
        {
            Debug.WriteLine("New SceneGame");
            _bar = new Bar(new Vector2(MainGame.TargetWidth * 0.5f, MainGame.TargetHeight));
        }

        public override void Load()
        {
            Debug.WriteLine("Load SceneGame");

            _lvlGenerate = new LvlGenerate();
            _lvlGenerate.GenerateBrique();
            _lvlGenerate.GenerateBall();
            base.Load();
        }

        public override void UnLoad()
        {
            Debug.WriteLine("UnLoad SceneGame");
            base.UnLoad();
        }

        public override void Update(GameTime gameTime)
        {
            foreach (GameObject ball in _lvlGenerate.listBall)
            {
                GameObject Bar = GameObject.FindByName("Bar")[0];
                if (ball.ColliderBox(Bar))
                {
                    ball.ColliderEffect(Bar);
                }
                foreach (GameObject brique in _lvlGenerate.listBrique)
                {
                    if (brique.enabled) 
                    {
                        if (ball.ColliderBox(brique))
                        {
                            ball.ColliderEffect(brique);
                        }
                    }
                }
            }
            base.Update(gameTime);
        }

        public override void Draw()
        {
            _lvlGenerate.Draw();
            base.Draw();
        }
    }
}
