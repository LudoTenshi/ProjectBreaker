using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;

namespace ProjectBreaker
{
    internal class LvlGenerate
    {
        public List<Brique> listBrique;
        public List<Ball> listBall;
        public int level = 1;
        private List<LevelManager> _levelManager;
        private Texture2D _textureFond;
        public LvlGenerate()
        {
            this.listBrique = new List<Brique>();
            this.listBall = new List<Ball>();
            this._textureFond = ServiceLocator.GetService<ContentManager>().Load<Texture2D>("fond");

        }

        public void GenerateBrique() 
        {
            this.listBrique.Clear();
            GameObject.DeleteByName("Brique");
            this.listBall.Clear();
            GameObject.DeleteByName("Ball");
            ServiceLocator.GetService<TextManager>().Texts.Clear(); 
            string workingDirectory = Environment.CurrentDirectory;
            string path = Directory.GetParent(workingDirectory).Parent.Parent.FullName;

            if (File.Exists(path + "\\Level\\Level" + level + ".json"))
            {
                this._levelManager = new List<LevelManager>();
                StreamReader reader = new StreamReader(path + "\\Level\\Level" + level + ".json");
                string jsontext = reader.ReadToEnd();

                _levelManager = JsonConvert.DeserializeObject<List<LevelManager>>(jsontext);

                foreach (LevelManager args in _levelManager)
                {
                    Brique brique = new Brique(new Vector2((int)args.C, (int)args.L), (int)args.Type);
                    listBrique.Add(brique);
                }
            }
        }

        public void GenerateBall()
        {
            Ball ball = new Ball();
            this.listBall.Add(ball);
        }

        public void Draw() 
        {
            ServiceLocator.GetService<SpriteBatch>().Draw(_textureFond, new Vector2(0,0),Color.White * 0.5f);

        }
    }
}
