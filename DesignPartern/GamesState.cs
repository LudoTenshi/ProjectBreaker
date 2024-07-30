using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBreaker
{
    public class GamesState
    {
        public enum SceneType
        {
            Menu,
            Game,
            GameOver,
        }

        protected MainGame mainGame;
        public SceneManager currentScene {  get; set; }
        public GamesState(MainGame pGame)
        {
            this.mainGame = pGame; 
        }

        public void ChangeScene(SceneType ptype) 
        {
            if (currentScene != null) 
            {
                currentScene.UnLoad();
                currentScene = null;
            }

            switch (ptype)
            {
                case SceneType.Menu:
                    currentScene = new SceneMenu(mainGame);
                    break;
                case SceneType.Game:
                    break;
                case SceneType.GameOver:
                    break;
                default:
                    break;
            }

            currentScene.Load();
        }
    }

}
