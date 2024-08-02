using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace ProjectBreaker
{
    internal class SceneGame : SceneManager
    {
        private Bar _bar;
        public SceneGame(MainGame pGame) : base(pGame)
        {
            Debug.WriteLine("New SceneGame");
            _bar = new Bar();
        }

        public override void Load()
        {
            Debug.WriteLine("Load SceneGame");
            Texture2D newText = this.mainGame.Content.Load<Texture2D>("Barre");
            _bar.Init(newText, new Vector2(mainGame.TargetWidth * 0.5f,mainGame.TargetHeight - newText.Height));
            base.Load();
        }

        public override void UnLoad()
        {
            Debug.WriteLine("UnLoad SceneGame");
            base.UnLoad();
        }

        public override void Update(GameTime gameTime)
        {
            _bar.Update(gameTime);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            _bar.Draw(gameTime,mainGame.spriteBatch);
            base.Draw(gameTime);
        }
    }
}
