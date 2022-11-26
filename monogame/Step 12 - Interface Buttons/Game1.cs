using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Step_12___Interface_Buttons
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private Color _backgroundColour = Color.CornflowerBlue;
        private List<Component> _gameComponents;

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

            var randomButton = new Button(Content.Load<Texture2D>("Controls/Button"), Content.Load<SpriteFont>("Fonts/Font"))
            {
                Position = new Vector2(350, 200),
                Text = "Random",
            };

            randomButton.Click += RandomButton_Click;

            var quitButton = new Button(Content.Load<Texture2D>("Controls/Button"), Content.Load<SpriteFont>("Fonts/Font"))
            {
                Position = new Vector2(350, 250),
                Text = "Quit",
            };

            quitButton.Click += QuitButton_Click;

            _gameComponents = new List<Component>()
            {
                randomButton,
                quitButton,
            };
        }

        private void QuitButton_Click(object sender, System.EventArgs e)
        {
            Exit();
        }

        private void RandomButton_Click(object sender, System.EventArgs e)
        {
            var random = new Random();

            _backgroundColour = new Color(random.Next(0, 255), random.Next(0, 255), random.Next(0, 255));
        }

        protected override void Update(GameTime gameTime)
        {
            foreach (var component in _gameComponents)
                component.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(_backgroundColour);

            spriteBatch.Begin();

            foreach (var component in _gameComponents)
                component.Draw(gameTime, spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
