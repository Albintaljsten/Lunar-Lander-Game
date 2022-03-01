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
        private float maxSpeed;

        public Lander(Texture2D texture, Vector2 pos, Color color, float angle) : base(texture, pos, color)
        {
            this.angle = MathHelper.ToRadians(angle);


            acceleration = 3f;
            gravity = 4f;

            dir.X = (float)Math.Cos(angle);
            dir.Y = (float)Math.Sin(angle);
            dir.Normalize();
            maxSpeed = 30;

            gravityDir.X = 0;
            gravityDir.Y = pos.Y * 2;
            gravityDir.Normalize();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (!isGrounded)
            {
                //Rotate lander

                UpdateRotation();

                ////Movement

                UpdateMovement(gameTime);

                speed = acceleration * timer;
                pos += dir * speed * timer * (float)gameTime.ElapsedGameTime.TotalSeconds;

                gravitySpeed = gravity * gravityTimer;
                pos += gravityDir * gravitySpeed * gravityTimer * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            //Respawn

            //if (dead == true)
            //{
            //    resetTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            //    if (resetTimer >= 4)
            //    {
            //        Reset();
            //    }
            //}
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
            }
            else
            {
                timer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (timer < 0)
                    timer = 0;
            }

            if (!InputHelper.KeyPressed(Keys.Space) && speed != maxSpeed)
            {
                gravityTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else
            {
                gravityTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (gravityTimer <= 2)
                    gravityTimer = 2;
            }
        }

        public void SetGrounded()
        {

            isGrounded = true;
            if (gravitySpeed > 12)
            {
                correctLanding = false;
            }
            else
            {
                correctLanding = true;
            }
            //Kolla hastighet och rotation och sätt state "korrekt landning" till true/false

        }

        public bool CorrectLanding
        {
            get { return correctLanding; }
        }

        public void Die()
        {
            dead = true;
        }
        public void Reset()
        {
            pos = new Vector2(100, 100);
            angle = -0.0f;
            gravityTimer = 0;
            timer = 0;
            resetTimer = 0;
            dead = false;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (dead == false)
            {
                spriteBatch.Draw(hitBox, new Color(Color.Red, 0.5f));
                spriteBatch.Draw(texture, pos, null, color, angle + MathHelper.PiOver2, origin, 1f, SpriteEffects.None, 0);
            }
        }
    }
}
