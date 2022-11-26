using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Step_14___Camera_Following_Sprite
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private Camera _camera;

        private List<Component> _components;

        private Player _player;

        public static int ScreenHeight;

        public static int ScreenWidth;

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

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            _camera = new Camera();

            _player = new Player(Content.Load<Texture2D>("Player"));

            _components = new List<Component>()
            {
                new Sprite(Content.Load<Texture2D>("Background")),
                _player,
                new Sprite(Content.Load<Texture2D>("NPC")),
            };
        }

        protected override void Update(GameTime gameTime)
        {
            foreach (var component in _components)
                component.Update(gameTime);

            _camera.Follow(_player);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            spriteBatch.Begin(transformMatrix: _camera.Transform);

            foreach (var component in _components)
                component.Draw(gameTime, spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
