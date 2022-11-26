using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Step_6___Sprite_Shooting_Bullets
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        List<Sprite> _sprites;

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

            var shipTexture = Content.Load<Texture2D>("Ship");

            _sprites = new List<Sprite>()
            {
                new Ship(shipTexture)
                {
                    Position = new Vector2(100, 100),
                    Bullet = new Bullet(Content.Load<Texture2D>("Bullet")),
                }
            };
        }

        protected override void Update(GameTime gameTime)
        {
            // TODO: Add your update logic here

            foreach(var sprite in _sprites.ToArray())
            {
                sprite.Update(gameTime, _sprites);
            }

            PostUpdate();

            base.Update(gameTime);
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

            foreach(var sprite in _sprites)
            {
                sprite.Draw(spriteBatch);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
