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
    internal class Canonball
    {
        private float _speed, _scale;
        private Texture2D _canonballSprite;
        private float _canonballX, _canonballY;
        Random _rng;

        public Canonball(Texture2D canonballSprite)
        {
            _rng = new Random();
            _canonballSprite = canonballSprite;
            _canonballX = 1000;
            _canonballY = _rng.Next(50, 401);
            _speed = _rng.Next(3, 8);
            _scale = _rng.Next(25, 51) / 100f;
        }
        public float GetX() { return _canonballX; }

        public void Update(GameTime gameTime)
        {
            _canonballX -= _speed;
        }

        public Rectangle GetBounds()
        {
            return new Rectangle((int)_canonballX, (int)_canonballY, (int)(_canonballSprite.Width * _scale), (int)(_canonballSprite.Height * _scale));
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            //public void Draw(Texture2D texture, Vector2 position, Rectangle? sourceRectangle, Color color, float rotation, Vector2 origin, Vector2 scale, SpriteEffects effects, float layerDepth)

            spriteBatch.Draw(_canonballSprite,   //sprite
                new Vector2(_canonballX, _canonballY),  //location
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
