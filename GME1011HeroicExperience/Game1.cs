using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace GME1011HeroicExperience
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        //Our attributes
        private SpriteFont _gamefont;
        private Hero _myHero;
        private Canonball _canonball;
        private Texture2D _heroSprite;
        private List<Canonball> _canonballList;
        private List<Hero> _heroList;
        private List<Health> _healthList;

        //for sound and music you need the Media and Audio "usings"
        //above.
        private SoundEffect _oofSound, _yaySound;
        private Song _musicSong;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _canonballList = new List<Canonball>();
            _heroList = new List<Hero>();
            _healthList = new List<Health>();


            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _gamefont = Content.Load<SpriteFont>("GameFont");



            _oofSound = Content.Load<SoundEffect>("oof");
            _yaySound = Content.Load<SoundEffect>("yay");
            _musicSong = Content.Load<Song>("music");



            _heroSprite = Content.Load<Texture2D>("hero-small");
            _myHero = new Hero(5, "Aaron", _heroSprite, Content.Load<Texture2D>("health"), _gamefont);
            _heroList.Add(_myHero);

            _healthList.Add(new Health(Content.Load<Texture2D>("health")));

            _canonballList.Add(new Canonball(Content.Load<Texture2D>("Canonball_Sprite")));

            _canonballList.Add(new Canonball(Content.Load<Texture2D>("Canonball_Sprite")));


            MediaPlayer.Play(_musicSong);
            // TODO: use this.Content to load your game content here
        }



        protected override void Update(GameTime gameTime)
        {
            //Only run this code if there is at least one hero in the 
            //list.
            if (_heroList.Count > 0)
            {
                //cheating and assuming there is only one hero
                //Update hero's activities
                _heroList[0].Update(gameTime); 
               
                //If the hero dies, remove them from the list.
                //Again, assume only one hero.
                if (_heroList[0].GetHealth() <= 0)
                    _heroList.RemoveAt(0);
                
            }

            //For each canonball, call their update method.
            if (_canonballList.Count > 0)
            {
                foreach (Canonball ball in _canonballList)
                {
                    ball.Update(gameTime);
                }
            }

            //For each heart, call the update method.
            if (_healthList.Count > 0)
            {
                //update all the hearts in the list.
                foreach(Health health in _healthList)
                {
                    health.Update(gameTime);
                }

                //check for heart collision with hero
                for (int i = 0; i < _healthList.Count; i++)
                {
                    //there is a collision!!!
                    if (_heroList.Count > 0 && _heroList[0].CollidesWithHealth(_healthList[i]))
                    {
                        //hero takes damage
                        _myHero.Heal(1);
                        _yaySound.Play();

                        //health is removed from the list
                        _healthList.RemoveAt(i);

                        //let's add a new health to the list
                        _healthList.Add(new Health(Content.Load<Texture2D>("health")));
                    }


                    //if the health exits the screen, remove it.
                    if (_healthList[i].GetX() < -250)
                    {
                        _healthList.RemoveAt(i);

                        //..and add a new one.
                        _healthList.Add(new Health(Content.Load<Texture2D>("health")));
                    }
                }


            }






            //If there are canonballs....
            if (_canonballList.Count > 0)
            {
                //for every canonball in the list...
                //check for collisions with each canonball and the hero
                for (int i = 0; i < _canonballList.Count; i++)
                {
                    //there is a collision!!!
                    if (_heroList.Count > 0 && _heroList[0].CollidesWithCanonball(_canonballList[i]))
                    {
                        //hero takes damage
                        _myHero.TakeDamage(1);

                        //canonball is removed from the list
                        _canonballList.RemoveAt(i);
                        _oofSound.Play();

                        //let's add a new canonball to the list
                        _canonballList.Add(new Canonball(Content.Load<Texture2D>("Canonball_Sprite")));
                    }


                    //if the canonball exits the screen, remove it.
                    if (_canonballList[i].GetX() < -250)
                    {
                        _canonballList.RemoveAt(i);

                        //..and add a new one.
                        _canonballList.Add(new Canonball(Content.Load<Texture2D>("Canonball_Sprite")));
                    }
                }
            }







            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            
            if(_heroList.Count > 0)
                _heroList[0].Draw(_spriteBatch);

            if (_healthList.Count > 0)
            {
                foreach (Health health in _healthList)
                {
                    health.Draw(_spriteBatch);
                }
            }


            if (_canonballList.Count > 0)
                for(int i = 0; i < _canonballList.Count; i++)
                    _canonballList[i].Draw(_spriteBatch);


            _spriteBatch.Begin();
            if (_heroList.Count <= 0)
                _spriteBatch.DrawString(_gamefont, "Game Over!!", new Vector2(10, 10), Color.Yellow);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
