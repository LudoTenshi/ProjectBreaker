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
using SharpDX.Direct3D11;


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
            GameObject Bar = GameObject.FindByName("Bar")[0];
            foreach (GameObject ball in _lvlGenerate.listBall)
            {
                if (ball.ColliderBox(Bar)) ball.ColliderEffect(Bar);

                foreach (GameObject brique in _lvlGenerate.listBrique)
                {
                    if (brique.enabled) 
                    {
                        if (ball.ColliderBox(brique)) ball.ColliderEffect(brique);
                    }
                }
            }
            List<GameObject> ListBuff = GameObject.FindByName("Buff");
            foreach (GameObject args in ListBuff) 
            {
                Buff buff = (Buff)args;
                if (buff.ColliderBox(Bar))
                {
                    buff.ColliderEffect(Bar);
                    switch (buff.typeB)
                    {
                        case Buff.type.Ball:
                            _lvlGenerate.GenerateBall();
                            ServiceLocator.GetService<TextManager>().AddNewText("MultiBall", Color.White, new Vector2(Bar.bounds.Center.X, Bar.bounds.Top), true);
                            break;
                        case Buff.type.Star:
                            _lvlGenerate.listBall.ToList().ForEach(b => b.starPower = true);
                            ServiceLocator.GetService<TextManager>().AddNewText("StarPower", Color.White, new Vector2(Bar.bounds.Center.X, Bar.bounds.Top), true);
                            break;
                        case Buff.type.Power:
                            _lvlGenerate.listBall.ToList().ForEach(b => b.damage += 10);
                            ServiceLocator.GetService<TextManager>().AddNewText("Power Up", Color.White, new Vector2(Bar.bounds.Center.X, Bar.bounds.Top), true);
                            break;
                        case Buff.type.Lucky:
                            ServiceLocator.GetService<TextManager>().AddNewText("Lucky Lucky", Color.White, new Vector2(Bar.bounds.Center.X, Bar.bounds.Top), true);
                            _lvlGenerate.level++;
                            _lvlGenerate.GenerateBrique();
                            _lvlGenerate.GenerateBall();
                            break;
                    }
                }
            }
            _lvlGenerate.listBrique.RemoveAll(b => b.enabled == false);
            _lvlGenerate.listBall.RemoveAll(b => b.enabled == false);
            if (_lvlGenerate.listBall.Count == 0)
            {
                GamesState.ChangeScene(GamesState.SceneType.GameOver);
            }
            if (_lvlGenerate.listBrique.Count == 0)
            {
                _lvlGenerate.level++;
                if (_lvlGenerate.level >= 2)
                {
                    GamesState.ChangeScene(GamesState.SceneType.Victory);
                }
                else
                {
                    _lvlGenerate.GenerateBrique();
                    _lvlGenerate.GenerateBall();
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
