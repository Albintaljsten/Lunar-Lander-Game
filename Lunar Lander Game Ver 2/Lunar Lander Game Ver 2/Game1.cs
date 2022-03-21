using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace Lunar_Lander_Game_Ver_2
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        Lander lander;
        Ground ground;
        Texture2D groundRect;
        SpriteFont font;
        Vector2 pointsVec;
        Vector2 fuelVec;
        string text;
        float offset;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            if (GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width == 1920 && GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height == 1080)
            {
                graphics.PreferredBackBufferWidth = 1920;
                graphics.PreferredBackBufferHeight = 1080;
                graphics.IsFullScreen = true;
                graphics.ApplyChanges();
            }
            else if (GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width == 3840 && GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height == 2160)
            {
                graphics.PreferredBackBufferWidth = 3840;
                graphics.PreferredBackBufferHeight = 2160;
                graphics.IsFullScreen = true;
                graphics.ApplyChanges();
            }
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            if (graphics.PreferredBackBufferWidth == 1920 && graphics.PreferredBackBufferHeight == 1080)
            {
                spriteBatch = new SpriteBatch(GraphicsDevice);
                lander = new Lander(Content.Load<Texture2D>("Silver_fresh"), new Vector2(100), Color.White, -0.0f);

                groundRect = new Texture2D(GraphicsDevice, 1, 1);
                groundRect.SetData(new Color[1] { Color.DarkGray });

                ground = new Ground(groundRect, new Vector2(0, 980), Color.White, new Vector2(1920, 100));
                font = Content.Load<SpriteFont>("font");

                pointsVec = new Vector2(10);
                fuelVec = new Vector2(1520, 10);
                offset = 50f;
            }


            else if (graphics.PreferredBackBufferWidth == 3840 && graphics.PreferredBackBufferHeight == 2160)
            {
                spriteBatch = new SpriteBatch(GraphicsDevice);
                lander = new Lander(Content.Load<Texture2D>("Silver_fresh_Big"), new Vector2(200), Color.White, -0.0f);

                groundRect = new Texture2D(GraphicsDevice, 1, 1);
                groundRect.SetData(new Color[1] { Color.DarkGray });

                ground = new Ground(groundRect, new Vector2(0, 1960), Color.White, new Vector2(3840, 200));
                font = Content.Load<SpriteFont>("fontBig");

                pointsVec = new Vector2(20);
                fuelVec = new Vector2(3440, 10);
                offset = 100f;
            }
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            InputHelper.Update();
            lander.Update(gameTime);

            if (lander.Intersects(ground) /*&& lander.GravitySpeed > lander.MaxSpeed / 2*/)
            {
                lander.SetGrounded();

                if (lander.CorrectLanding)
                {
                    lander.Reset();
                    lander.AddScore();
                }
                else
                {
                    lander.Dead = true;
                    lander.Reset();
                    lander.LoseFuel(gameTime);
                }
            }
            text = "Game Over!\nYour score was: " + lander.Score;
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            lander.Draw(gameTime, spriteBatch);
            if (lander.Fuel! >= 0)
            {
                spriteBatch.DrawString(font, "Score: " + lander.Score, pointsVec, Color.White);
                spriteBatch.DrawString(font, "Fuel: " + (int)lander.Fuel, fuelVec, Color.White);
            }
            else if (lander.Fuel <= 0)
            {
                spriteBatch.DrawString(font, text,
                    new Vector2(graphics.PreferredBackBufferWidth / 2 - font.MeasureString(text).Length() / 2, graphics.PreferredBackBufferHeight / 2 - offset),
                    Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            }
            ground.Draw(gameTime, spriteBatch);
            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
