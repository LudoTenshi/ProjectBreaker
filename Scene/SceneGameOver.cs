using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ProjectBreaker
{
    internal class SceneGameOver : SceneManager
    {
        private Texture2D _fond;
        public SceneGameOver() 
        {
            Debug.WriteLine("New SceneMenu");
        }

        public override void Load()
        {
            _fond = ServiceLocator.GetService<ContentManager>().Load<Texture2D>("GameOver");
            ServiceLocator.GetService<TextManager>().AddNewText("Press space",Color.White,new Vector2(MainGame.TargetWidth * 0.5f,MainGame.TargetHeight * 0.5f),false);
            base.Load();
        }

        public override void UnLoad()
        {
            Debug.WriteLine("UnLoad SceneMenu");
            base.UnLoad();
        }

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Space)) GamesState.ChangeScene(GamesState.SceneType.Menu);
            base.Update(gameTime);
        }

        public override void Draw()
        {
            ServiceLocator.GetService<SpriteBatch>().Draw(_fond, new Vector2(0, 0), Color.White);
            base.Draw();
        }
    }
}
