using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Step_16___Sprite_Layers
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private Vector2 _position1;
        private Vector2 _position2;

        private Texture2D _texture;

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

            _position1 = new Vector2(100, 100);
            _position2 = new Vector2(125, 100);

            _texture = Content.Load<Texture2D>("Square");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            spriteBatch.Begin(SpriteSortMode.BackToFront);

            spriteBatch.Draw(_texture, _position2, null, Color.Blue, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0.1f);
            spriteBatch.Draw(_texture, _position1, null, Color.Red, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0.2f);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
