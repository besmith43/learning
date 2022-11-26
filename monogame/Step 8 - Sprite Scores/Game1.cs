using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;

namespace Step_8___Sprite_Scores
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public static Random Random;

        public static int ScreenWidth;
        public static int ScreenHeight;

        private List<Sprite> _sprites;

        private SpriteFont _font;

        private float _timer;

        private Texture2D _appleTexture;

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

            var playerTexture = Content.Load<Texture2D>("Player");

            _sprites = new List<Sprite>()
            {
                new Player(playerTexture)
                {
                    Input = new Input()
                    {
                        Left = Keys.A,
                        Right = Keys.D,
                        Up = Keys.W,
                        Down = Keys.S
                    },
                    Position = new Vector2(100,100),
                    Colour = Color.Blue,
                    Speed = 5f,
                },
                new Player(playerTexture)
                {
                    Input = new Input()
                    {
                        Left = Keys.Left,
                        Right = Keys.Right,
                        Up = Keys.Up,
                        Down = Keys.Down
                    },
                    Position = new Vector2(ScreenWidth - 100 - playerTexture.Width,100),
                    Colour = Color.Green,
                    Speed = 5f,
                }
            };

            _font = Content.Load<SpriteFont>("Font");
            _appleTexture = Content.Load<Texture2D>("Apple");
        }

        protected override void Update(GameTime gameTime)
        {
            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            foreach (var sprite in _sprites)
            {
                sprite.Update(gameTime, _sprites);
            }

            PostUpdate();

            SpawnApple();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        private void SpawnApple()
        {
            if (_timer > 1)
            {
                _timer = 0;

                var xPos = Random.Next(0, ScreenWidth - _appleTexture.Width);
                var yPos = Random.Next(0, ScreenHeight - _appleTexture.Height);

                _sprites.Add(new Sprite(_appleTexture)
                {
                    Position = new Vector2(xPos, yPos),
                });
            }
        }

        private void PostUpdate()
        {
            for (int i = 0; i < _sprites.Count; i++)
            {
                if (_sprites[i].IsRemoved)
                {
                    _sprites.RemoveAt(i);
                    i--;
                }
            }
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

            var fontY = 10;
            var i = 0;

            foreach (var sprite in _sprites)
            {
                if (sprite is Player)
                {
                    spriteBatch.DrawString(_font, string.Format("Player {0}: {1}", ++i, ((Player)sprite).Score), new Vector2(10, fontY += 20), Color.Black);
                }
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
