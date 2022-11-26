using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Step_10___2_Player_Pong_Game
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public static int ScreenHeight;
        public static int ScreenWidth;
        public static Random Random;

        private List<Sprite> _sprites;
        private Score _score;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            ScreenHeight = graphics.PreferredBackBufferHeight;
            ScreenWidth = graphics.PreferredBackBufferWidth;

            Random = new Random();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            var batTexture = Content.Load<Texture2D>("Bat");
            var ballTexture = Content.Load<Texture2D>("Ball");

            _score = new Score(Content.Load<SpriteFont>("Font"));

            _sprites = new List<Sprite>()
            {
                new Sprite(Content.Load<Texture2D>("Background")),
                new Bat(batTexture)
                {
                    Position = new Vector2(20, (ScreenHeight / 2) - (batTexture.Height/2)),
                    Input = new Input()
                    {
                        Up = Keys.W,
                        Down = Keys.S
                    }
                },
                new Bat(batTexture)
                {
                    Position = new Vector2((ScreenWidth - 20 - batTexture.Width), (ScreenHeight / 2) - (batTexture.Height/2)),
                    Input = new Input()
                    {
                        Up = Keys.Up,
                        Down = Keys.Down
                    }
                },
                new Ball(ballTexture)
                {
                    Position = new Vector2((ScreenWidth / 2) - (ballTexture.Width / 2), (ScreenHeight / 2) - (ballTexture.Height / 2)),
                    Score = _score
                }
            };
        }

        protected override void Update(GameTime gameTime)
        {
            foreach (var sprite in _sprites)
            {
                sprite.Update(gameTime, _sprites);
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

            _score.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
