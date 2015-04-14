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

namespace TSC_Game
{
    class GameButton
    {
        public string Name { get; set; }
        public Vector2 Position { get; set; }
        public float Scale { get; set; }
        public bool MouseOver { get; set; }
        public bool Pressed { get; set; }
        public bool WasPressed { get; set; }
        public Texture2D Sprite { get; set; }
        public Texture2D MouseOverSprite { get; set; }
        public Texture2D PressedSprite { get; set; }
        public bool Disabled { get; set; }

        public GameButton(string name, Vector2 position, Texture2D sprite, Texture2D mouseOverSprite, Texture2D pressedSprite)
        {
            Name = name;
            Position = position;
            Scale = 1f;
            Sprite = sprite;
            MouseOverSprite = mouseOverSprite;
            PressedSprite = pressedSprite;
            MouseOver = false;
            Pressed = false;
            WasPressed = false;
            Disabled = false;
        }

        public void Update(SoundManager sound)
        {
            if (!Disabled)
            {
                MouseState mouseState = Mouse.GetState();

                if (mouseState.X > Position.X && mouseState.X < Position.X + Sprite.Width * Scale &&
                    mouseState.Y > Position.Y && mouseState.Y < Position.Y + Sprite.Height * Scale)
                {
                    MouseOver = true;

                    if (mouseState.LeftButton == ButtonState.Pressed)
                    {
                        Pressed = true;
                        //sound.PlaySound("MenuB");
                    }
                    else if (Pressed == true)
                    {
                        Pressed = false;
                        WasPressed = true;
                    }
                    else
                    {
                        WasPressed = false;
                    }
                }
                else
                {
                    MouseOver = false;
                    Pressed = false;
                    WasPressed = false;
                }
            }
        }

        public void AdjustPosition(GraphicsDevice graphics, int index, int count)
        {
            Scale = (float)graphics.Viewport.Height / 1024f;
            Position = new Vector2(graphics.Viewport.Width / 2 - (Sprite.Width / 2) * Scale, graphics.Viewport.Height / count * index + 50);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Texture2D drawSprite = Sprite;
            if (MouseOver)
            {
                drawSprite = MouseOverSprite;
            }
            if (Pressed)
            {
                drawSprite = PressedSprite;
            }
            Color drawColor = Color.White;
            if (Disabled)
            {
                drawColor = Color.Gray;
            }
            spriteBatch.Draw(drawSprite, Position, null, Color.White, 0f, Vector2.Zero, Scale, SpriteEffects.None, 0f);
        }
    }
}
