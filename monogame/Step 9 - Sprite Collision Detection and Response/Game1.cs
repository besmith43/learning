using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Step_9___Sprite_Collision_Detection_and_Response
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

            var playerTexture = Content.Load<Texture2D>("Block");

            _sprites = new List<Sprite>()
            { 
                new Player(playerTexture)
                {
                    Input = new Input()
                    {
                        Left = Keys.A,
                        Right = Keys.D,
                        Up = Keys.W,
                        Down = Keys.S
                    },
                    Position = new Vector2(100, 100),
                    Colour = Color.Blue,
                    Speed = 5f
                },
                new Player(playerTexture)
                {
                    Input = new Input()
                    {
                        Left = Keys.Left,
                        Right = Keys.Right,
                        Up = Keys.Up,
                        Down = Keys.Down
                    },
                    Position = new Vector2(300, 100),
                    Colour = Color.Red,
                    Speed = 5f
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
