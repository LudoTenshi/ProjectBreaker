using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectBreaker
{
    abstract public class SceneManager
    {

        public SceneManager() 
        { 
        }

        public virtual void Load()
        {

        }

        public virtual void UnLoad()
        {

        }

        public virtual void Update(GameTime gameTime)
        {
            foreach (KeyValuePair<string,List<GameObject>> listG in GameObject._objects) 
            {
                foreach (GameObject gameObject in listG.Value) 
                {
                    if (gameObject.enabled) gameObject.Update(gameTime);
                }
            }
            ServiceLocator.GetService<TextManager>().Update(gameTime);
        }

        public virtual void Draw()
        {
            foreach (KeyValuePair<string, List<GameObject>> listG in GameObject._objects)
            {
                foreach (GameObject gameObject in listG.Value)
                {
                    if (gameObject.enabled) gameObject.Draw();
                    
                }
            }
            ServiceLocator.GetService<TextManager>().Draw();
        }
    }
}
