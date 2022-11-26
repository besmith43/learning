using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Step_19___Per_Pixel_Collision_Detection_on_Rotated_Sprites
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

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

            var shipTexture = Content.Load<Texture2D>("Player");

            var bulletPrefab = new Bullet(Content.Load<Texture2D>("Bullet"));

            _sprites = new List<Sprite>()
            {
                new Ship(shipTexture)
                {
                    Bullet = bulletPrefab,
                    Position = new Vector2(100, 100),
                    Colour = Color.Green,
                },
                new Sprite(shipTexture)
                {
                    Position = new Vector2(200, 200),
                    Colour = Color.Red,
                },
                new Sprite(Content.Load<Texture2D>("Enemy_1"))
                {
                    Position = new Vector2(300, 100),
                    Colour = Color.Red,
                }
            };
        }

        protected override void Update(GameTime gameTime)
        {
            foreach (var sprite in _sprites)
                sprite.Update(gameTime);

            PostUpdate(gameTime);

            base.Update(gameTime);
        }

        protected void PostUpdate(GameTime gameTime)
        {
            // 1. Check collision between all current "Sprites"
            // 2. Add "Children" to the list of "_sprites" and clear
            // 3. Remove all "IsRemoved" sprites

            foreach (var spriteA in _sprites)
            {
                foreach (var spriteB in _sprites)
                {
                    if (spriteA == spriteB)
                        continue;
                }
            }

            int count = _sprites.Count;
            for (int i = 0; i < count; i++)
            {
                foreach (var child in _sprites[i].Children)
                    _sprites.Add(child);

                _sprites[i].Children.Clear();
            }

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
                sprite.Draw(gameTime, spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
