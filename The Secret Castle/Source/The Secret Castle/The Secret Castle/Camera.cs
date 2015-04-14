using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace The_Secret_Castle
{
    public class Camera
    {
        private Vector2 position;
        public Vector2 Position { get { return position; } set { position = value; } }
        public float Zoom { get; private set; }
        private MouseState LastMouseState;

        public Camera()
        {
            position = Vector2.Zero;
            Zoom = 1f;
            LastMouseState = Mouse.GetState();
        }

        public void Update(GameTime gameTime, GraphicsDeviceManager graphics, Texture2D backGround)
        {
            MouseState mouseState = Mouse.GetState();
            KeyboardState keyState = Keyboard.GetState();

            if (keyState.IsKeyDown(Keys.LeftAlt))
            {
                position.X -= LastMouseState.X - mouseState.X;
                position.Y -= LastMouseState.Y - mouseState.Y;
            }
            if (keyState.IsKeyDown(Keys.LeftControl))
            {
                Zoom = 0.5f;
            }
            else
            {
                Zoom = 1f;
            }
            position.X = MathHelper.Clamp(position.X, -backGround.Width + graphics.GraphicsDevice.Viewport.Width - 168, 0);
            position.Y = MathHelper.Clamp(position.Y, -backGround.Height + graphics.GraphicsDevice.Viewport.Height, 0);

            LastMouseState = mouseState;
        }

        public void Update(Character player, GraphicsDevice graphics, Texture2D backGround)
        {
            Vector2 pos = -player.Position + new Vector2(graphics.Viewport.Width / 2 - 89, graphics.Viewport.Height / 2);
            if (Vector2.Distance(position, pos) > 1)
            {
                position = pos;

                position.X = MathHelper.Clamp(position.X, -backGround.Width + graphics.Viewport.Width, 0);
                position.Y = MathHelper.Clamp(position.Y, -backGround.Height + graphics.Viewport.Height, 0);
            }
        }

        public Matrix GetMatrix()
        {
            return Matrix.CreateTranslation(new Vector3(position, 0)) * Matrix.CreateScale(Zoom);
        }
    }
}
