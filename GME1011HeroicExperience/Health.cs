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
    internal class Health
    {
        private float _speed, _scale;
        private Texture2D _healthSprite;
        private float _healthX, _healthY;
        Random _rng;

        public Health(Texture2D healthSprite)
        {
            _rng = new Random();
            _healthSprite = healthSprite;
            _healthX = 1000;
            _healthY = _rng.Next(50, 401);
            _speed = _rng.Next(3, 8);
            _scale = _rng.Next(25, 51) / 100f;
        }
        public float GetX() { return _healthX; }

        public void Update(GameTime gameTime)
        {
            _healthX -= _speed;
        }

        public Rectangle GetBounds()
        {
            return new Rectangle((int)_healthX, (int)_healthY, (int)(_healthSprite.Width * _scale), (int)(_healthSprite.Height * _scale));
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            //public void Draw(Texture2D texture, Vector2 position, Rectangle? sourceRectangle, Color color, float rotation, Vector2 origin, Vector2 scale, SpriteEffects effects, float layerDepth)

            spriteBatch.Draw(_healthSprite,   //sprite
                new Vector2(_healthX, _healthY),  //location
                null, //rectangle
                Color.White, //color
                0f, //rotation
                new Vector2(0, 0),  //origin
                _scale,   //scale
                SpriteEffects.None,  //effects
                0   //layer
                );
            spriteBatch.End();
        }
    }

}
