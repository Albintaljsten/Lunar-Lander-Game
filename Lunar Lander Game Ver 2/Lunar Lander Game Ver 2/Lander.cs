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
        //private Vector2 dirSum;
        private Vector2 dir;
        private float timer;
        private float speed;
        private float acceleration;

        private Vector2 gravityDir;
        private float gravityTimer;
        private float gravitySpeed;
        private float gravity;
        private float resetTimer;

        public Lander(Texture2D texture, Vector2 pos, Color color, float angle/*, Vector2 dirSum*/) : base(texture, pos, color)
        {
            this.angle = MathHelper.ToRadians(angle);
            //this.dirSum = dirSum;
            acceleration = 3f;
            gravity = 4f;
            dir.X = (float)Math.Cos(angle);
            dir.Y = (float)Math.Sin(angle);
            dir.Normalize();

            gravityDir.X = 0;
            gravityDir.Y = pos.Y * 2;
            gravityDir.Normalize();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            //Gravity and direction sum is "dirSum = dir + gravity + (more vectors if there is)"

            //dirSum = dir + gravity;


            //Rotate lander

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

            //dir.X = (float)Math.Cos(angle);
            //dir.Y = (float)Math.Sin(angle);
            //dir.Normalize();


            //Movement

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

            if (!InputHelper.KeyPressed(Keys.Space))
            {
                gravityTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else
            {
                gravityTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (gravityTimer <= 2)
                    gravityTimer = 2;
            }

            speed = timer * acceleration;
            pos += dir * speed * timer * (float)gameTime.ElapsedGameTime.TotalSeconds;

            gravitySpeed = gravityTimer * gravity;
            pos += gravityDir * gravitySpeed * gravityTimer * (float)gameTime.ElapsedGameTime.TotalSeconds;


            //Respawn

            if (color == Color.Transparent)
            {
                resetTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            if (resetTimer > 2)
            {
                pos = new Vector2(100, 100);
                color = Color.White;
                gravityTimer = 0;
                timer = 0;
                resetTimer = 0;
            }
        }

        public void Die()
        {
            color = Color.Transparent;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(hitBox, new Color(Color.Red, 0.5f));
            spriteBatch.Draw(texture, pos, null, color, angle + MathHelper.PiOver2, origin, 1f, SpriteEffects.None, 0);
        }
    }
}
