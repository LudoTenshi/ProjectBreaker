using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectBreaker
{
    class TextManager
    {
        private SpriteFont _font;
        private int _animationMax = 20;
        public List<Text> Texts;
        public TextManager() 
        {
            this.Texts = new List<Text>();
            this._font = ServiceLocator.GetService<ContentManager>().Load<SpriteFont>("font");
        }

        public void AddNewText(string ptext,Color pcolor, Vector2 pposition,bool panimation)
        {
            Text text = new Text(ptext,pposition,pcolor,panimation);
            this.Texts.Add(text);
        }
        public void Update(GameTime gameTime)
        {
            foreach(Text text in this.Texts)
            {
                if (text.animation)
                {
                    text.position.Y--;
                    text.animationValue++;
                }
            }

            this.Texts.RemoveAll(text => text.animationValue >= this._animationMax);
        }
        public void Draw()
        {
            foreach (Text text in this.Texts)
            {
                ServiceLocator.GetService<SpriteBatch>().DrawString(_font,text.text, text.position ,text.color,0,_font.MeasureString(text.text) * 0.5f,1,SpriteEffects.None,0);
            }
            
        }
    }

    class Text
    {
        public Color color;
        public Vector2 position;
        public string text;
        public bool animation;
        public int animationValue;
        public Text(string ptext,Vector2 pposition,Color pcolor,bool panimation) 
        { 
            this.text = ptext;
            this.position = pposition;
            this.color = pcolor;
            this.animation = panimation;
            this.animationValue = 0;
        }
    }
}
