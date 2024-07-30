using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace ProjectBreaker
{
    abstract public class SceneManager
    {
        protected MainGame mainGame;

        public SceneManager(MainGame pGame) 
        { 
            this.mainGame = pGame;
        }

        public virtual void Load()
        {

        }

        public virtual void UnLoad()
        {

        }

        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void Draw(GameTime gameTime)
        {

        }
    }
}
