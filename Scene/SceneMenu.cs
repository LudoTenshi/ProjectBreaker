﻿using System;
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

namespace ProjectBreaker
{
    internal class SceneMenu : SceneManager
    {
        private Texture2D _fond;
        public SceneMenu() 
        {
            Debug.WriteLine("New SceneMenu");
        }

        public override void Load()
        {
            _fond = ServiceLocator.GetService<ContentManager>().Load<Texture2D>("Menu");
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
            base.Update(gameTime);
        }

        public override void Draw()
        {
            ServiceLocator.GetService<SpriteBatch>().Draw(_fond, new Vector2(0, 0), Color.White * 0.5f);
            base.Draw();
        }
    }
}
