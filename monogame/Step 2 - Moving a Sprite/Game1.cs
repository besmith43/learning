using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Step_2___Moving_a_Sprite
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private Texture2D _texture;
        private Vector2 _position;

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

            _texture = Content.Load<Texture2D>("Box");
            _position = new Vector2(0, 0);
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                // up
                _position.Y -= 1;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                // left
                _position.X -= 1;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                // right
                _position.X += 1;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                // down
                _position.Y += 1;
            }

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            spriteBatch.Begin();
            spriteBatch.Draw(_texture, _position, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
