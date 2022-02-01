using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lunar_Lander_Game_Ver_2
{
    class GameObject
    {
        protected Texture2D texture;
        protected Vector2 pos, origin;
        protected Color color;
        protected float angle;
        protected Rectangle hitBox;
        public GameObject(Texture2D texture, Vector2 pos, Color color)
        {
            this.texture = texture;
            this.pos = pos;
            this.color = color;
            origin = new Vector2(texture.Width, texture.Height) / 2;
            angle = 0.0f;
        }

        public virtual void Update(GameTime gameTime)
        {
            hitBox.Location = pos.ToPoint() - origin.ToPoint();
            hitBox.Width = texture.Width;
            hitBox.Height = texture.Height;
        }

        public bool Intersects(GameObject other)
        {
            return hitBox.Intersects(other.hitBox);
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, pos, null, color, angle, origin, 1f, SpriteEffects.None, 0);
        }
    }
}
