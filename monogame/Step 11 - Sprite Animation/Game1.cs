using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Step_11___Sprite_Animation
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

            var animations = new Dictionary<string, Animation>()
            {
                { "WalkUp", new Animation(Content.Load<Texture2D>("Player/WalkingUp"),3) },
                { "WalkDown", new Animation(Content.Load<Texture2D>("Player/WalkingDown"),3) },
                { "WalkLeft", new Animation(Content.Load<Texture2D>("Player/WalkingLeft"),3) },
                { "WalkRight", new Animation(Content.Load<Texture2D>("Player/WalkingRight"),3) }
            };

            _sprites = new List<Sprite>()
            {
                new Sprite(animations)
                {
                    Position = new Vector2(100, 100),
                    Input = new Input()
                    {
                        Up = Keys.W,
                        Down = Keys.S,
                        Left = Keys.A,
                        Right = Keys.D
                    }
                },
                new Sprite(animations)
                {
                    Position = new Vector2(150, 100),
                    Input = new Input()
                    {
                        Up = Keys.Up,
                        Down = Keys.Down,
                        Left = Keys.Left,
                        Right = Keys.Right
                    }
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

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
