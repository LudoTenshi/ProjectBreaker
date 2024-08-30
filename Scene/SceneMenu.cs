using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace ProjectBreaker
{
    internal class SceneMenu : SceneManager
    {
        public SceneMenu() 
        {
            Debug.WriteLine("New SceneMenu");
        }

        public override void Load()
        {
            Debug.WriteLine("Load SceneMenu");
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
            base.Draw();
        }
    }
}
