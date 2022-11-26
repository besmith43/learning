using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Step_7___Sprite_Death_and_Respawn
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public static Random Random;

        public static int ScreenHeight;
        public static int ScreenWidth;

        private List<Sprite> _sprites;

        private float _timer;

        private bool _hasStarted = false;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            Random = new Random();

            ScreenWidth = graphics.PreferredBackBufferWidth;
            ScreenHeight = graphics.PreferredBackBufferHeight;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            Restart();
        }

        private void Restart()
        {
            var playerTexture = Content.Load<Texture2D>("Player");

            _sprites = new List<Sprite>()
            {
                new Player(playerTexture)
                {
                    Position = new Vector2((ScreenWidth / 2) - (playerTexture.Width / 2), ScreenHeight - playerTexture.Height),
                    Input = new Input()
                    {
                        Left = Keys.A,
                        Right = Keys.D,
                    },
                    Speed = 10f,
                }
            };

            _hasStarted = false;
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                _hasStarted = true;
            }

            if (!_hasStarted)
            {
                return;
            }

            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            foreach (var sprite in _sprites)
            {
                sprite.Update(gameTime, _sprites);
            }

            if (_timer > 0.25f)
            {
                _timer = 0;
                _sprites.Add(new Bomb(Content.Load<Texture2D>("Bomb")));
            }

            for (int i = 0; i < _sprites.Count; i++)
            {
                var sprite = _sprites[i];

                if (sprite.IsRemoved)
                {
                    _sprites.RemoveAt(i);
                    i--;
                }

                if (sprite is  Player)
                {
                    var player = sprite as Player;
                    
                    if (player.HasDied)
                    {
                        Restart();
                    }
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            spriteBatch.Begin();

            foreach (var sprite in _sprites)
            {
                sprite.Draw(spriteBatch);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
