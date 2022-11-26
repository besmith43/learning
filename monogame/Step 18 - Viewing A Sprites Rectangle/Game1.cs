using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Step_18___Viewing_A_Sprites_Rectangle
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private KeyboardState _currentKey;
        private KeyboardState _previousKey;

        private bool _showBorders = false;

        private List<Sprite> _sprites;

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

            _sprites = new List<Sprite>
            {
                new Sprite(graphics.GraphicsDevice, Content.Load<Texture2D>("Square"))
                {
                    Position = new Vector2(100, 100),
                },
                new Player(graphics.GraphicsDevice, Content.Load<Texture2D>("Apple"))
                {
                    Position = new Vector2(200, 100),
                },
                new Sprite(graphics.GraphicsDevice, Content.Load<Texture2D>("Bomb"))
                {
                    Position = new Vector2(200, 200),
                }
             };
        }

        protected override void Update(GameTime gameTime)
        {
            _previousKey = _currentKey;
            _currentKey = Keyboard.GetState();

            if (_currentKey.IsKeyUp(Keys.F1) && _previousKey.IsKeyDown(Keys.F1))
                _showBorders = !_showBorders;

            foreach (var sprite in _sprites)
                sprite.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            spriteBatch.Begin();

            foreach (var sprite in _sprites)
            {
                sprite.ShowRectangle = _showBorders;
                sprite.Draw(gameTime, spriteBatch);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
