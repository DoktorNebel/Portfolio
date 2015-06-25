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
    public class Button
    {
        public Vector2 Position { get; set; }
        public bool Pressed { get; set; }
        public bool MouseOver { get; set; }
        public int Type { get; private set; }
        public Texture2D Sprite { get; set; }
        public List<Texture2D> Sprites { get; set; }
        public string SpritePath { get; set; }
        public List<string> SpritePaths { get; set; }
        public int Loop { get; set; }
        public int AnimSpeed { get; set; }

        public Button(Vector2 position, string spritePath, int type, ContentManager content)
        {
            Position = position;
            Pressed = false;
            SpritePath = spritePath;
            Sprite = content.Load<Texture2D>(spritePath);
            SpritePaths = null;
            Sprites = null;
            Type = type;
        }

        public Button(Vector2 position, List<string> spritePaths, int animSpeed, int loop, int type, ContentManager content)
        {
            Position = position;
            Pressed = false;
            SpritePath = spritePaths.ElementAt<string>(0);
            SpritePaths = spritePaths;
            Sprite = content.Load<Texture2D>(spritePaths.ElementAt<string>(0));
            Sprites = new List<Texture2D>();
            for (int i = 0; i < SpritePaths.Count; i++)
            {
                Sprites.Add(content.Load<Texture2D>(SpritePaths.ElementAt<string>(i)));
            }
            Loop = loop;
            Type = type;
            AnimSpeed = animSpeed;
        }

        public void Update(List<Button> buttons)
        {
            MouseState mouseState = Mouse.GetState();

            if (mouseState.X > Position.X &
                mouseState.X < Position.X + Sprite.Width &
                mouseState.Y > Position.Y &
                mouseState.Y < Position.Y + Sprite.Height)
            {
                foreach (Button b in buttons)
                {
                    if (mouseState.LeftButton == ButtonState.Pressed)
                    {
                        if (b.Pressed)
                        {
                            b.Pressed = false;
                        }
                    }
                    if (b.MouseOver)
                    {
                        b.MouseOver = false;
                    }
                }
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    Pressed = true;
                }
                MouseOver = true;
            }
            else
            {
                MouseOver = false;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Texture2D drawSprite = Sprite;
            if (Sprites != null)
            {
                drawSprite = Sprites.ElementAt<Texture2D>(0);
            }
            if (Pressed)
            {
                spriteBatch.Draw(drawSprite, Position, Color.Gray);
            }
            else if (MouseOver)
            {
                spriteBatch.Draw(drawSprite, Position, Color.LightGray);
            }
            else
            {
                spriteBatch.Draw(drawSprite, Position, Color.White);
            }
        }
    }
}
