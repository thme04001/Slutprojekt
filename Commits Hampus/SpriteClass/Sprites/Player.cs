using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpriteClass.Sprites;
using SpriteClass.Models;

namespace SpriteClass.Sprites
{
    public class Player : Sprite
    {
        public bool HasDied = false;

        public Player(Texture2D texture)
          : base(texture)
        {

        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            Move();

            foreach (var sprite in sprites)
            {
                if (sprite is Player)
                    continue;

                if (sprite.Rectangle.Intersects(this.Rectangle))
                {
                    this.HasDied = true;
                }
            }

            Position += Velocity;

            // Keep the sprite on the screen
            Position.X = MathHelper.Clamp(Position.X, 0, Game1.ScreenWidth - 50);
            Position.Y = MathHelper.Clamp(Position.Y,0,Game1.ScreenHeight-100); 
            // Resest the velocity for when the user isn't holding down a key
            Velocity = Vector2.Zero;
        }

        private void Move()
        {
            if (Input == null)
                throw new Exception("Please assign a value to 'Input'");

            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                Position.Y -= 3;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                Position.Y += 3;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                Position.X -= 3;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                Position.X += 3;
            }
        }
    }
}