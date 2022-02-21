using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Lunar_Lander_Game_Ver_2
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        Lander lander;
        Ground ground;
        Texture2D groundRect;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            lander = new Lander(Content.Load<Texture2D>("Silver_fresh"), new Vector2(100), Color.White, -0.0f);

            groundRect = new Texture2D(GraphicsDevice, 1, 1);
            groundRect.SetData(new Color[1] { Color.DarkGray });

            ground = new Ground(groundRect, new Vector2(0, 400), Color.White, new Vector2(800, 100));
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            InputHelper.Update();
            lander.Update(gameTime);

            if (lander.Intersects(ground) && lander.GravitySpeed > lander.MaxSpeed/2)
            {
                lander.Die();
            }
            else if(lander.Intersects(ground) && lander.GravitySpeed < lander.MaxSpeed/2)
            {
                lander.pos.Y = ground.pos.Y - lander.texture.Height;
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            lander.Draw(gameTime, spriteBatch);
            ground.Draw(gameTime, spriteBatch);
            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
