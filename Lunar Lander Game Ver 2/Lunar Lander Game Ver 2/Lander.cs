using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Lunar_Lander_Game_Ver_2
{
    class Lander : GameObject
    {
        private float angle;

        private Vector2 dir;
        private float timer;
        private float speed;
        private float acceleration;

        private Vector2 gravityDir;
        private float gravityTimer;
        private float gravitySpeed;
        private float gravity;

        private bool isGrounded = false;
        private bool correctLanding = false;

        private float resetTimer;
        private bool dead;

        private Vector2 lastPos;
        private float deltaPos;

        private float fuel;
        private int score;

        public Lander(Texture2D texture, Vector2 pos, Color color, float angle) : base(texture, pos, color)
        {
            this.angle = MathHelper.ToRadians(angle);


            acceleration = 3f;
            gravity = 4f;

            dir.X = (float)Math.Cos(angle);
            dir.Y = (float)Math.Sin(angle);
            dir.Normalize();

            gravityDir.X = 0;
            gravityDir.Y = pos.Y * 2;
            gravityDir.Normalize();

            fuel = 1000;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (fuel !>= 0)
            {
                if (!isGrounded)
                {
                    //Rotate lander

                    UpdateRotation();

                    //Movement

                    UpdateMovement(gameTime);

                    //Fuel

                    LoseFuel(gameTime);


                    lastPos = pos;


                    speed = acceleration * timer;
                    gravitySpeed = gravity * gravityTimer;
                    pos += ((dir * speed * timer) + (gravityDir * gravitySpeed * gravityTimer)) * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    //pos.X = dir.X * speed * timer * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    //speed = acceleration * timer;
                    //pos.Y += dir.Y * speed * timer * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    //pos.X += dir.X * speed * timer * (float)gameTime.ElapsedGameTime.TotalSeconds;

                    //gravitySpeed = gravity * gravityTimer;
                    //pos.Y += gravityDir.Y * gravitySpeed * gravityTimer * (float)gameTime.ElapsedGameTime.TotalSeconds;


                    deltaPos = pos.Y - lastPos.Y;
                }

                //Respawn

                if (isGrounded)
                {
                    resetTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
            }
            Debug.WriteLine(gravitySpeed);

        }

        private void UpdateRotation()
        {
            if (InputHelper.KeyPressed(Keys.A))
            {
                angle -= 0.025f;
            }
            else if (InputHelper.KeyPressed(Keys.D))
            {
                angle += 0.025f;
            }

            if (angle > MathHelper.ToRadians(0.0f))
                angle = MathHelper.ToRadians(0.0f);

            else if (angle < MathHelper.ToRadians(-180.0f))
                angle = MathHelper.ToRadians(-180.0f);
        }

        private void UpdateMovement(GameTime gameTime)
        {
            if (InputHelper.KeyPressed(Keys.Space))
            {
                timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                dir.X = (float)Math.Cos(angle);
                dir.Y = (float)Math.Sin(angle);
                dir.Normalize();

                gravityTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else
            {

                timer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (timer <= 0)
                    timer = 0;

                gravityTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
        }

        public void SetGrounded()
        {

            isGrounded = true;
            if (deltaPos > 0.5 || (angle > MathHelper.ToRadians(-60) || angle < MathHelper.ToRadians(-120)))
            {
                correctLanding = false;
            }
            else
            {
                correctLanding = true;
            }
            //Kolla hastighet och rotation och sätt state "korrekt landning" till true/false

        }

        public void AddScore()
        {
            if (correctLanding == true && resetTimer == 0)
            {
                score += 100;
            }
            else if (correctLanding == false)
            {
                score += 0;
            }
        }

        public void LoseFuel(GameTime gameTime)
        {
            if (fuel !>= 0)
            {
                if (InputHelper.KeyPressed(Keys.Space) && isGrounded == false)
                {
                    fuel -= 10f * (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
                if (dead == true && resetTimer == 0)
                {
                    fuel -= 150f;
                } 
            }
        }

        public bool CorrectLanding
        {
            get { return correctLanding; }
        }

        public int Score
        {
            get { return score; }
        }

        public bool Dead
        {
            set { dead = value; }
        }

        public float Fuel
        {
            get { return fuel; }
        }

        public void Reset()
        {
            //Dead
            if (resetTimer >= 2 && correctLanding == false)
            {
                if (GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width == 1920 && GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height == 1080)
                {
                    pos = new Vector2(100); 
                }
                else
                {
                    pos = new Vector2(200);
                }
                angle = -0.0f;
                gravityTimer = 0;
                timer = 0;
                resetTimer = 0;
                dead = false;
                isGrounded = false;
            }

            //Alive
            else if (resetTimer >= 4 && correctLanding == true)
            {
                if (GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width == 1920 && GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height == 1080)
                {
                    pos = new Vector2(100);
                }
                else
                {
                    pos = new Vector2(200);
                }

                angle = -0.0f;
                gravityTimer = 0;
                timer = 0;
                resetTimer = 0;
                dead = false;
                isGrounded = false;
                correctLanding = false;
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (dead == false)
            {
                spriteBatch.Draw(hitBox, new Color(Color.Red, 0.5f));
                spriteBatch.Draw(texture, pos, null, color, angle + MathHelper.PiOver2, origin, 1f, SpriteEffects.None, 0);
            }
            //else if (dead == true)
            //{
            //    spriteBatch.Draw();
            //}
        }
    }
}
