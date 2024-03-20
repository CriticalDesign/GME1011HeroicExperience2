using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GME1011HeroicExperience
{
    internal class Hero
    {
        private int _health;
        private string _name;
        private Texture2D _currentSprite, _healthSprite;
        private SpriteFont _heroFont;
        private float _heroX, _heroY, _speed;

        public Hero(int health, string name, Texture2D currentSprite, Texture2D healthSprite, SpriteFont heroFont)
        {
            _health = health;
            _name = name;
            _currentSprite = currentSprite;
            _healthSprite = healthSprite;
            _heroX = 100;
            _heroY = 10;
            _heroFont = heroFont;
            _speed = 2.5f;
         }

        public int GetHealth() {  return _health; }
        public string GetName() { return _name; }   

        public int DealDamage() { Random _rng = new Random(); return _rng.Next(3, 10);  }
        public void TakeDamage(int damage) { _health -= damage; }
        public void Heal(int healme) { _health += healme; }

        public void Update(GameTime gameTime) 
        {
            
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
                _heroY -= _speed;
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
                _heroY += _speed;

           
        }

        public bool CollidesWithCanonball(Canonball danger)
        {
            if(danger == null) {  return false; }
            Rectangle myBounds = GetBounds();
            Rectangle dangerBounds = danger.GetBounds();
            if (myBounds.Intersects(dangerBounds))
            {
                return true;
            }
            else
                return false;
        }


        public bool CollidesWithHealth(Health saveme)
        {
            if (saveme == null) { return false; }
            Rectangle myBounds = GetBounds();
            Rectangle savemeBounds = saveme.GetBounds();
            if (myBounds.Intersects(savemeBounds))
            {
                return true;
            }
            else
                return false;
        }


        public Rectangle GetBounds()
        {
            return new Rectangle((int)_heroX, (int)_heroY, _currentSprite.Width, _currentSprite.Height);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            //spriteBatch.DrawString(_heroFont, _health + "", new Vector2(_currentSprite.Width / 2, _heroY - 60), Color.White);
            
            spriteBatch.Draw(_currentSprite, new Vector2(_heroX, _heroY), Color.White);


            //draw health
            for (int i = 0; i < _health; i++)
            {
                spriteBatch.Draw(_healthSprite,   //sprite
                new Vector2(75 + i * 40, _heroY - 50),  //location
                null, //rectangle
                Color.White, //color
                0f, //rotation
                new Vector2(0, 0),  //origin
                0.15f,   //scale
                SpriteEffects.None,  //effects
                0   //layer
                );
            }



            spriteBatch.End();
        }

        public override string ToString() { return "Hero[" + _name + "," + _health + "]";}

    }
}
