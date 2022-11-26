using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Step_17___Static_Background
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private Camera _camera;

        private Texture2D _playerTexture;

        private Texture2D _backgroundTexture;

        private Vector2 _playerPosition;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
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

            _camera = new Camera();

            _playerTexture = Content.Load<Texture2D>("Square");
            _backgroundTexture = Content.Load<Texture2D>("Background");
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.W))
                _playerPosition.Y -= 3f;
            if (Keyboard.GetState().IsKeyDown(Keys.S))
                _playerPosition.Y += 3f;

            if (Keyboard.GetState().IsKeyDown(Keys.A))
                _playerPosition.X -= 3f;
            if (Keyboard.GetState().IsKeyDown(Keys.D))
                _playerPosition.X += 3f;

            _camera.Follow(_playerPosition);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            spriteBatch.Begin();

            spriteBatch.Draw(_backgroundTexture, new Vector2(0, 0), Color.White);

            spriteBatch.End();

            spriteBatch.Begin(transformMatrix: _camera.Transform);

            spriteBatch.Draw(_playerTexture, _playerPosition, Color.Green);
            spriteBatch.Draw(_playerTexture, new Vector2(0, 0), Color.Red);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
