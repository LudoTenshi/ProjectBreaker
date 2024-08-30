using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBreaker
{
    public static class GamesState
    {
        public enum SceneType
        {
            Menu,
            Game,
            GameOver,
            Victory,
        }

        public static SceneManager currentScene {  get; set; }
        public static SceneType currentSceneType { get; set; }

        public static void ChangeScene(SceneType ptype) 
        {
            GameObject.DestroyAll();
            if (currentScene != null) 
            {
                currentScene.UnLoad();
                currentScene = null;
            }
            currentSceneType = ptype;
            switch (ptype)
            {
                case SceneType.Menu:
                    currentScene = new SceneMenu();
                    break;
                case SceneType.Game:
                    currentScene = new SceneGame();
                    break;
                case SceneType.GameOver:
                    currentScene = new SceneGameOver();
                    break;
                case SceneType.Victory:
                    currentScene = new SceneVictory();
                    break;
                default:
                    break;
            }

            currentScene.Load();
        }
    }

}
