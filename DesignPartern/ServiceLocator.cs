using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBreaker
{
    public static class ServiceLocator
    {
        private static Dictionary<Type, object> _services = new Dictionary<Type, object>();

        public static void RegisterService<T>(T service) where T : class
        {
            if (_services.ContainsKey(typeof(T)))
            { 
               throw new InvalidOperationException($"Service of type {typeof(T)} already registered");
            }
            _services[typeof(T)] = service;
        }

        public static T GetService<T>()
        {
            if (!_services.ContainsKey(typeof(T)))
            {
                throw new InvalidOperationException($"Service of type {typeof(T)} not registered");
            }
            return (T)_services[typeof(T)];
        }
    }
    /*static Texture2D _pointTexture;
        public static void DrawRectangle(SpriteBatch spriteBatch, Rectangle rectangle, Color color, int lineWidth)
        {
            if (_pointTexture == null)
            {
                _pointTexture = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
                _pointTexture.SetData<Color>(new Color[] { Color.White });
            }

            spriteBatch.Draw(_pointTexture, new Rectangle(rectangle.X, rectangle.Y, lineWidth, rectangle.Height + lineWidth), color);
            spriteBatch.Draw(_pointTexture, new Rectangle(rectangle.X, rectangle.Y, rectangle.Width + lineWidth, lineWidth), color);
            spriteBatch.Draw(_pointTexture, new Rectangle(rectangle.X + rectangle.Width, rectangle.Y, lineWidth, rectangle.Height + lineWidth), color);
            spriteBatch.Draw(_pointTexture, new Rectangle(rectangle.X, rectangle.Y + rectangle.Height, rectangle.Width + lineWidth, lineWidth), color);
        }*/
}
