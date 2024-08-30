using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectBreaker
{
    public abstract class GameObject
    {
        public string name { get; set; }
        public Texture2D texture2D;
        public int sizeW;
        public int sizeH;
        public Vector2 position;
        public Vector2 direction;
        public Rectangle bounds;
        public float _angle = 1;
        protected int _speed = 5;
        public bool enabled = true;

        public static Dictionary<string,List<GameObject>> _objects = new Dictionary<string,List<GameObject>>();

        public GameObject(string name) { 
            this.name = name;
            if (!GameObject._objects.ContainsKey(name))
            {
                GameObject._objects[name] = new List<GameObject>();
            }
            GameObject._objects[name].Add(this);

        }

        public bool ColliderBox(GameObject g)
        {
            return this.bounds.Intersects(g.bounds);
        }

        public virtual void ColliderEffect(GameObject g) { }

        public Vector2 ColliderScreen()
        {
            if(this.position.X <= 0) return new Vector2(1, 0);
            if(this.position.X + this.sizeW  > MainGame.TargetWidth) return new Vector2(-1, 0);
            if(this.position.Y <= 0) return new Vector2(0, 1);
            if(this.position.Y + this.sizeH > MainGame.TargetHeight) return new Vector2(0, -1);

            return Vector2.Zero;
        }

        public virtual void Update(GameTime gameTime)
        {
            this.bounds = new Rectangle((int)this.position.X, (int)this.position.Y, this.sizeW, this.sizeH);
        }

        public abstract void Draw();

        public static List<GameObject> FindByName(string name)
        {
            if (_objects.ContainsKey(name))
            {
                return _objects[name];
            }
            return null;
        }

        public void OnDisable()
        {
            this.enabled = false;
        }
    }
}
