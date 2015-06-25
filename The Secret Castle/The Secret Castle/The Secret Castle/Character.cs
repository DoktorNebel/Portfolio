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
using System.Diagnostics;

namespace The_Secret_Castle
{
    public class Character
    {
        public Vector2 Position { get; private set; }
        public Vector2 Velocity { get; private set; }
        public List<List<Texture2D>> Sprites { get; private set; }
        public Texture2D Sprite { get; private set; }
        public int Time { get; private set; }
        public int Coins { get; private set; }
        public int Diamonds { get; private set; }
        public int Keyparts { get; private set; }
        private int State;
        private int AnimPhase;
        private bool FacingRight;
        private int AnimTimer;
        private bool KeyPressed;
        private bool Clicked;
        private int AnimSpeed;

        public Character(Vector3 position, ContentManager content)
        {
            Position = new Vector2(position.X, position.Y);
            Time = (int)position.Z;
            Velocity = Vector2.Zero;
            Keyparts = 0;
            State = 0;
            AnimPhase = 0;
            AnimTimer = 0;
            AnimSpeed = 200;
            KeyPressed = false;
            FacingRight = true;
            Clicked = false;

            List<List<string>> sprites = new List<List<string>>();
            sprites.Add(new List<string>());
            sprites.Add(new List<string>());
            sprites.Add(new List<string>());
            sprites.Add(new List<string>());
            sprites.Add(new List<string>());
            sprites.Add(new List<string>());
            sprites.Add(new List<string>());
            sprites.Add(new List<string>());
            sprites.Add(new List<string>());
            sprites.Add(new List<string>());
            sprites.Add(new List<string>());
            sprites.Add(new List<string>());

            sprites[0].Add("Sprites/Character/Laufen/char");
            sprites[1].Add("Sprites/Character/Laufen/char3");
            sprites[1].Add("Sprites/Character/Laufen/char4");
            sprites[1].Add("Sprites/Character/Laufen/char5");
            sprites[1].Add("Sprites/Character/Laufen/char6");
            sprites[2].Add("Sprites/Character/Leiter/b2");
            sprites[2].Add("Sprites/Character/Leiter/b3");
            sprites[3].Add("Sprites/Character/Treppe/a1");
            sprites[3].Add("Sprites/Character/Treppe/a2");
            sprites[4].Add("Sprites/Character/sprung/sprung");
            sprites[4].Add("Sprites/Character/sprung/sprung1");
            sprites[4].Add("Sprites/Character/sprung/sprung2");
            sprites[4].Add("Sprites/Character/sprung/sprung3");
            sprites[4].Add("Sprites/Character/sprung/sprung4");
            sprites[4].Add("Sprites/Character/sprung/sprung5");
            sprites[5].Add("Sprites/Character/Schalter/schalter_boden/d1");
            sprites[5].Add("Sprites/Character/Schalter/schalter_boden/d2");
            sprites[5].Add("Sprites/Character/Schalter/schalter_boden/d3");
            sprites[6].Add("Sprites/Character/Schalter/schalter_wand/c1");
            sprites[6].Add("Sprites/Character/Schalter/schalter_wand/c2");
            sprites[6].Add("Sprites/Character/Schalter/schalter_wand/c3");
            sprites[7].Add("Sprites/Character/Schalter/Zeitschalter/e1");
            sprites[7].Add("Sprites/Character/Schalter/Zeitschalter/e2");
            sprites[7].Add("Sprites/Character/Schalter/Zeitschalter/e3");
            sprites[7].Add("Sprites/Character/Schalter/Zeitschalter/e4");
            sprites[8].Add("Sprites/Character/Leiter/b2");
            sprites[9].Add("Sprites/Character/Treppe/a1");
            sprites[9].Add("Sprites/Character/Treppe/a2");
            sprites[10].Add("Sprites/Character/Treppe/a1");
            sprites[10].Add("Sprites/Character/Treppe/a2");
            sprites[11].Add("Sprites/Character/Treppe/a1");


            Sprites = new List<List<Texture2D>>();
            Sprites.Add(new List<Texture2D>());
            Sprites.Add(new List<Texture2D>());
            Sprites.Add(new List<Texture2D>());
            Sprites.Add(new List<Texture2D>());
            Sprites.Add(new List<Texture2D>());
            Sprites.Add(new List<Texture2D>());
            Sprites.Add(new List<Texture2D>());
            Sprites.Add(new List<Texture2D>());
            Sprites.Add(new List<Texture2D>());
            Sprites.Add(new List<Texture2D>());
            Sprites.Add(new List<Texture2D>());
            Sprites.Add(new List<Texture2D>());

            for (int i = 0; i < sprites.Count; i++)
            {
                for (int j = 0; j < sprites[i].Count; j++)
                {
                    Sprites[i].Add(content.Load<Texture2D>(sprites[i][j]));
                }
            }
            Sprite = Sprites[0][0];
        }

        public void Update(Level level, Operation colCheck, GameTime gameTime, Texture2D Cursor, Vector2 Pos)
        {
            List<GameObject> objects = level.Past;
            if (Time == 1)
            {
                objects = level.Present;
            }
            else if (Time == 2)
            {
                objects = level.Future;
            }
            List<Link> links = level.PastLinks;
            if (Time == 1)
            {
                links = level.PresentLinks;
            }
            else if (Time == 2)
            {
                links = level.FutureLinks;
            }

            MouseState mouseState = Mouse.GetState();
            KeyboardState keyState = Keyboard.GetState();

            //Check for Collisions
            List<GameObject> ColObjects = new List<GameObject>();
            if (State != 4)
            {
                ColObjects.Concat(Collision(level, colCheck, Cursor));

                foreach (GameObject o in objects)
                {
                    if (colCheck(Sprite, Position, o.Sprite, o.Position, 0))
                    {
                        ColObjects.Add(o);
                    }
                }

                Position += Velocity * gameTime.ElapsedGameTime.Milliseconds / 10;

                //Gravity
                if (State != 2 && State < 8)
                {
                    Velocity += new Vector2(0, 0.1f);
                }
            }



            if (State < 2 || State > 7)
            {
                //Keyboard Input
                if (keyState.IsKeyDown(Keys.A))
                {
                    if (State == 9 || State == 11)
                    {
                        State = 9;
                        Position += new Vector2(-2, 2);
                    }
                    else if (State == 10 || State == 11)
                    {
                        State = 10;
                        Position += new Vector2(-2, -2);
                    }
                    else
                    {
                        FacingRight = false;
                        if (Velocity.X > -2f)
                        {
                            Velocity += new Vector2(-0.25f, 0);
                        }
                    }
                }


                if (keyState.IsKeyDown(Keys.D))
                {
                    if (State == 9 || State == 11)
                    {
                        State = 9;
                        Position += new Vector2(2, -2);
                    }
                    else if (State == 10 || State == 11)
                    {
                        State = 10;
                        Position += new Vector2(2, 2);
                    }
                    else
                    {
                        FacingRight = true;
                        if (Velocity.X < 2f)
                        {
                            Velocity += new Vector2(0.25f, 0);
                        }
                    }
                }

                if (State > 8)
                {
                    if (!keyState.IsKeyDown(Keys.A) && !keyState.IsKeyDown(Keys.D))
                    {
                        State = 11;
                    }
                    if (!ColObjects.Exists(o => o.Type == 7))
                    {
                        State = 0;
                    }
                }


                //Climbing
                if (State < 2 && keyState.IsKeyDown(Keys.W) && Math.Abs(Velocity.Y) < 0.2f)
                {
                    int j = 0;
                    int multiplier = 1;
                    if (!FacingRight)
                    {
                        multiplier = -1;
                    }
                    Vector2 pos = new Vector2(Position.X + 32 * multiplier, Position.Y + Sprite.Height / 2);
                    Vector2 newPos = Vector2.Zero;
                    for (int i = 8; i <= 72; i += 16)
                    {
                        foreach (GameObject o in objects)
                        {
                            if (o.Layer == 1 && (o.Type < 3 || o.Type == 8))
                            {
                                for (int k = -8; k < 9; k++)
                                {
                                    if (colCheck(Cursor, pos - new Vector2(-k, i), o.Sprite, o.Position, 0))
                                    {
                                        j = i;
                                        newPos = o.Position + new Vector2((o.Sprite.Width / 4) * -multiplier, -o.Sprite.Height / 2 - 32);
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    if (j > 0 && j < 72)
                    {
                        bool free = true;
                        foreach (GameObject o in objects)
                        {
                            if (o.Layer == 1 && o.Type != 9 && o.Type != 10 && o.Type != 13)
                            {
                                if (colCheck(Sprite, newPos, o.Sprite, o.Position, 0))
                                {
                                    free = false;
                                    break;
                                }
                            }
                        }

                        if (free)
                        {
                            State = 4;
                            if (j == 8)
                            {
                                AnimPhase = 4;
                            }
                            else if (j == 24)
                            {
                                AnimPhase = 3;
                            }
                            else
                            {
                                AnimPhase = 6 - (j / 8 - 1);
                            }
                        }
                    }
                }
            }


            if (Velocity.X < -0.5f && !keyState.IsKeyDown(Keys.A))
            {
                Velocity += new Vector2(0.25f, 0);
            }
            else if (Velocity.X > 0.5f && !keyState.IsKeyDown(Keys.D))
            {
                Velocity += new Vector2(-0.25f, 0);
            }
            else if (!keyState.IsKeyDown(Keys.A) && !keyState.IsKeyDown(Keys.D))
            {
                Velocity = new Vector2(0, Velocity.Y);
            }



            if (State < 3 || State > 7)
            {
                //Object Interaction
                foreach (GameObject o in ColObjects)
                {
                    if (o != null)
                    {
                        if (State > 8 && o.Type < 2 && o.Layer == 1 && !ColObjects.Exists(ob => ob.Type == 7))
                        {
                            State = 0;
                            Position += new Vector2(0, -16);
                        }
                        if (o.Type == 3 | o.Type == 4 | o.Type == 12)
                        {
                            if (keyState.IsKeyDown(Keys.E) && !KeyPressed)
                            {
                                KeyPressed = true;
                                Velocity = new Vector2(0, Velocity.Y);
                                if (o.Type != 12 | (o.Type == 12 && !o.Activated))
                                {
                                    foreach (Link l in links)
                                    {
                                        if (l.FirstObject == o)
                                        {
                                            l.SecondObject.Switch();
                                        }
                                        if (l.SecondObject == o)
                                        {
                                            l.FirstObject.Switch();
                                        }
                                    }
                                }
                                o.Switch();

                                if (o.Type == 3)
                                {
                                    if (Time == 0)
                                    {
                                        if (o.Activated)
                                        {
                                            FacingRight = false;
                                            Position = new Vector2(o.Position.X + 20, Position.Y);
                                        }
                                        else
                                        {
                                            FacingRight = true;
                                            Position = new Vector2(o.Position.X - 20, Position.Y);
                                        }
                                        State = 5;
                                    }
                                    else
                                    {
                                        State = 6;
                                    }
                                }

                                if (o.Type == 4)
                                {
                                    State = 7;
                                    AnimSpeed = o.AnimSpeed;
                                }

                                if (o.Type == 12)
                                {
                                    State = 6;
                                }
                            }
                        }

                        if (o.Type == 6 && (keyState.IsKeyDown(Keys.W) | keyState.IsKeyDown(Keys.S)))
                        {
                            State = 2;
                            FacingRight = true;
                            Position = new Vector2(o.Position.X + 3, Position.Y);
                        }


                        if (o.Type == 8)
                        {
                            if (keyState.IsKeyDown(Keys.E) && !KeyPressed && o.Activated)
                            {
                                KeyPressed = true;
                                foreach (Link l in links)
                                {
                                    if (l.Direction == 0)
                                    {
                                        if (l.FirstObject == o && l.SecondObject.Type == 8 && l.SecondObject.Activated)
                                        {
                                            Position = l.SecondObject.Position;
                                        }
                                        if (l.SecondObject == o && l.FirstObject.Type == 8 && l.FirstObject.Activated)
                                        {
                                            Position = l.FirstObject.Position;
                                        }
                                    }
                                    else
                                    {
                                        if (l.FirstObject == o && l.SecondObject.Type == 8 && l.SecondObject.Activated)
                                        {
                                            Position = l.SecondObject.Position;
                                        }
                                    }
                                }
                            }
                        }

                        if (o.Type == 9)
                        {
                            objects.Remove(o);
                            Coins++;
                        }

                        if (o.Type == 10)
                        {
                            objects.Remove(o);
                            Keyparts++;
                        }

                        if (o.Type == 13)
                        {
                            objects.Remove(o);
                            Diamonds++;
                        }
                    }
                }

                if (State == 2)
                {
                    if (keyState.IsKeyDown(Keys.W) && Velocity.Y > -0.75f)
                    {
                        Velocity += new Vector2(0, -0.25f);
                    }
                    else if (keyState.IsKeyDown(Keys.S) && Velocity.Y < 0.75f)
                    {
                        Velocity += new Vector2(0, 0.25f);
                    }
                    else if (!keyState.IsKeyDown(Keys.W) && !keyState.IsKeyDown(Keys.S))
                    {
                        Velocity = new Vector2(Velocity.X, 0f);
                    }

                    if (!ColObjects.Exists(o => o.Type == 6))
                    {
                        State = 0;
                    }
                }
            }


            if (State < 2)
            {
                //Time Stuff
                foreach (GameObject o in objects)
                {
                    if (o.Portable)
                    {
                        if (!Clicked & colCheck(Cursor, Pos, o.Sprite, o.Position, 0))
                        {
                            if (mouseState.LeftButton == ButtonState.Pressed && Vector2.Distance(Position, o.Position) < 75)
                            {
                                if (PortCheck(colCheck, o.Sprite, o.Position, level.Past))
                                {
                                    if (Time == 1)
                                    {
                                        level.Present.Remove(o);
                                        level.Past.Add(o);
                                        Clicked = true;
                                    }
                                    if (Time == 2)
                                    {
                                        level.Future.Remove(o);
                                        level.Past.Add(o);
                                        Clicked = true;
                                    }
                                }
                                break;
                            }
                            if (mouseState.MiddleButton == ButtonState.Pressed && Vector2.Distance(Position, o.Position) < 75)
                            {
                                if (PortCheck(colCheck, o.Sprite, o.Position, level.Present))
                                {
                                    if (Time == 0)
                                    {
                                        level.Past.Remove(o);
                                        level.Present.Add(o);
                                        Clicked = true;
                                    }
                                    if (Time == 2)
                                    {
                                        level.Future.Remove(o);
                                        level.Present.Add(o);
                                        Clicked = true;
                                    }
                                }
                                break;
                            }
                            if (mouseState.RightButton == ButtonState.Pressed && Vector2.Distance(Position, o.Position) < 75)
                            {
                                if (PortCheck(colCheck, o.Sprite, o.Position, level.Future))
                                {
                                    if (Time == 0)
                                    {
                                        level.Past.Remove(o);
                                        level.Future.Add(o);
                                        Clicked = true;
                                    }
                                    if (Time == 1)
                                    {
                                        level.Present.Remove(o);
                                        level.Future.Add(o);
                                        Clicked = true;
                                    }
                                }
                                break;
                            }
                            break;
                        }
                    }
                }

                if (!Clicked & colCheck(Cursor, Pos, Sprite, Position, 0))
                {
                    if (mouseState.LeftButton == ButtonState.Pressed & Time != 0)
                    {
                        if (Math.Abs(Velocity.Y) < 0.2f && PortCheck(colCheck, Sprite, Position, level.Past))
                        {
                            Time = 0;
                            Clicked = true;
                        }
                    }
                    if (mouseState.MiddleButton == ButtonState.Pressed & Time != 1)
                    {
                        if (Math.Abs(Velocity.Y) < 0.2f && PortCheck(colCheck, Sprite, Position, level.Present))
                        {
                            Time = 1;
                            Clicked = true;
                        }
                    }
                    if (mouseState.RightButton == ButtonState.Pressed & Time != 2)
                    {
                        if (Math.Abs(Velocity.Y) < 0.2f && PortCheck(colCheck, Sprite, Position, level.Future))
                        {
                            Time = 2;
                            Clicked = true;
                        }
                    }
                }

                if (mouseState.LeftButton == ButtonState.Released &
                    mouseState.RightButton == ButtonState.Released &
                    mouseState.MiddleButton == ButtonState.Released)
                {
                    Clicked = false;
                }
            }


            //Animation
            if ((State < 2 || State == 8) && (keyState.IsKeyDown(Keys.A) | keyState.IsKeyDown(Keys.D)))
            {
                State = 1;
            }
            else if (State < 2)
            {
                State = 0;
            }
            else if (State == 2 && Velocity.Y == 0)
            {
                State = 8;
            }

            if (State == 4)
            {
                if (AnimPhase > 2)
                {
                    int multiplier = 1;
                    if (!FacingRight)
                    {
                        multiplier = -1;
                    }
                    Position += new Vector2(((6 - AnimPhase) * multiplier) / 1.7f, -(float)Math.Pow(6 - AnimPhase, 4) / 32);
                }
            }


            if (AnimPhase >= Sprites[State].Count)
            {
                AnimPhase = 0;
            }

            AnimTimer += gameTime.ElapsedGameTime.Milliseconds;
            if (AnimTimer > AnimSpeed)
            {
                AnimTimer = 0;
                if (AnimPhase < Sprites[State].Count - 1)
                {
                    AnimPhase++;
                }
                else
                {
                    AnimPhase = 0;
                    if (State > 3 && State < 8)
                    {
                        State = 0;
                        AnimSpeed = 200;
                    }
                }
            }

            Sprite = Sprites[State][AnimPhase];


            if (keyState.IsKeyUp(Keys.E) && KeyPressed)
            {
                KeyPressed = false;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            SpriteEffects effect = SpriteEffects.None;
            if (!FacingRight)
            {
                effect = SpriteEffects.FlipHorizontally;
            }
            spriteBatch.Draw(Sprite, Position, null, Color.White, 0f, new Vector2(32, 32), 1f, effect, 0f);
        }

        public bool PortCheck(Operation colCheck, Texture2D sprite, Vector2 position, List<GameObject> objects)
        {
            bool falsePos = true;
            foreach (GameObject o in objects)
            {
                if (falsePos && o.Layer == 2)
                {
                    if (colCheck(sprite, position, o.Sprite, o.Position, 250))
                    {
                        falsePos = false;
                    }
                }
                if (o.Layer == 1)
                {
                    if (colCheck(sprite, position, o.Sprite, o.Position, 250))
                    {
                        falsePos = true;
                        break;
                    }
                }
            }

            return !falsePos;
        }

        public List<GameObject> Collision(Level level, Operation colCheck, Texture2D colPoint)
        {
            List<GameObject> result = new List<GameObject>();

            if (State != 2 && State < 8)
            {
                Position += new Vector2(0, -0.05f);
            }

            List<GameObject> objects = level.Past;
            if (Time == 1)
            {
                objects = level.Present;
            }
            else if (Time == 2)
            {
                objects = level.Future;
            }

            Vector2 pos = Position;
            int multiplier = 1;
            if (Velocity.X > 0)
            {
                pos += new Vector2(24, 16);
            }
            else if (Velocity.X < 0)
            {
                pos += new Vector2(-24, 16);
                multiplier = -1;
            }

            int i = 0;
            GameObject collision = null;
            while (i <= Math.Abs(Velocity.X))
            {
                if (collision == null)
                {
                    foreach (GameObject o in objects)
                    {
                        if (o.Layer == 1)
                        {
                            if (colCheck(colPoint, pos + new Vector2(i * multiplier, 0), o.Sprite, o.Position, 250))
                            {
                                collision = o;
                                result.Add(o);
                                break;
                            }
                        }
                    }
                }

                if (collision != null)
                {
                    if (State < 9 && (collision.Type == 1 | collision.Type == 2 | collision.Type == 5 | collision.Type == 8 | (collision.Type == 11 && !collision.Activated)))
                    {
                        if (i == 0)
                        {
                            while (colCheck(colPoint, pos, collision.Sprite, collision.Position, 250))
                            {
                                pos += new Vector2(-1 * multiplier, 0);
                            }
                            pos += new Vector2(1 * multiplier, 0);
                            Position = new Vector2(pos.X - 24 * multiplier, Position.Y);
                        }
                        Velocity = new Vector2(i * multiplier, Velocity.Y);
                    }
                    break;
                }

                i++;
            }

            if (collision == null)
            {
                pos = Position;
                multiplier = 1;
                if (Velocity.X > 0)
                {
                    pos += new Vector2(24, -16);
                }
                else if (Velocity.X < 0)
                {
                    pos += new Vector2(-24, -16);
                    multiplier = -1;
                }

                i = 0;
                collision = null;
                while (i <= Math.Abs(Velocity.X))
                {
                    if (collision == null)
                    {
                        foreach (GameObject o in objects)
                        {
                            if (o.Layer == 1)
                            {
                                if (colCheck(colPoint, pos + new Vector2(i * multiplier, 0), o.Sprite, o.Position, 250))
                                {
                                    collision = o;
                                    result.Add(o);
                                    break;
                                }
                            }
                        }
                    }

                    if (collision != null)
                    {
                        if (State < 9 && (collision.Type == 1 | collision.Type == 2 | collision.Type == 5 | collision.Type == 8 | (collision.Type == 11 && !collision.Activated)))
                        {
                            if (i == 0)
                            {
                                while (colCheck(colPoint, pos, collision.Sprite, collision.Position, 250))
                                {
                                    pos += new Vector2(-1 * multiplier, 0);
                                }
                                pos += new Vector2(1 * multiplier, 0);
                                Position = new Vector2(pos.X - 24 * multiplier, Position.Y);
                            }
                            Velocity = new Vector2(i * multiplier, Velocity.Y);
                        }
                        break;
                    }

                    i++;
                }
            }



            pos = Position;
            multiplier = 1;
            if (Velocity.Y >= 0)
            {
                pos += new Vector2(-8, 32);
            }
            else if (Velocity.Y < 0)
            {
                pos += new Vector2(-8, -32);
                multiplier = -1;
            }
            i = 0;
            collision = null;
            while (i <= Math.Abs(Velocity.Y))
            {
                if (collision == null)
                {
                    foreach (GameObject o in objects)
                    {
                        if (o.Layer == 1 | ((o.Layer == 0 | o.Layer == 2) && o.Type == 1))
                        {
                            if (colCheck(colPoint, pos + new Vector2(0, i * multiplier), o.Sprite, o.Position, 250))
                            {
                                collision = o;
                                result.Add(o);
                                break;
                            }
                        }
                    }
                }

                if (collision != null)
                {
                    if (State < 9 && ((collision.Type == 1 && (collision.Layer == 0 || collision.Layer == 1 || (collision.Layer == 2 && !Keyboard.GetState().IsKeyDown(Keys.S) && multiplier == 1))) | collision.Type == 2 | collision.Type == 5 | collision.Type == 7 | collision.Type == 8 | (collision.Type == 11 && !collision.Activated)))
                    {
                        if ((collision.Layer == 2 && collision.Type == 1) | (collision.Type == 6 && (State == 2 || State == 8)) | collision.Type == 7)
                        {
                            //Position += new Vector2(0, 0.05f);
                        }
                        else if (i == 0)
                        {
                            while (colCheck(colPoint, pos, collision.Sprite, collision.Position, 250))
                            {
                                pos += new Vector2(0, -1 * multiplier);
                            }
                            pos += new Vector2(0, 1 * multiplier);
                            Position = new Vector2(Position.X, pos.Y - 32 * multiplier);
                        }
                        Velocity = new Vector2(Velocity.X, i * multiplier);
                    }
                    if (collision.Type == 7 && Keyboard.GetState().IsKeyDown(Keys.W))
                    {
                        Velocity = Vector2.Zero;
                        if (FacingRight)
                        {
                            State = 9;
                        }
                        else
                        {
                            State = 10;
                        }
                    }
                    break;
                }

                i++;
            }




            pos = Position;
            multiplier = 1;
            if (Velocity.Y >= 0)
            {
                pos += new Vector2(8, 32);
            }
            else if (Velocity.Y < 0)
            {
                pos += new Vector2(8, -32);
                multiplier = -1;
            }
            i = 0;
            collision = null;
            while (i <= Math.Abs(Velocity.Y))
            {
                if (collision == null)
                {
                    foreach (GameObject o in objects)
                    {
                        if (o.Layer == 1 | ((o.Layer == 0 | o.Layer == 2) && o.Type == 1))
                        {
                            if (colCheck(colPoint, pos + new Vector2(0, i * multiplier), o.Sprite, o.Position, 250))
                            {
                                collision = o;
                                result.Add(o);
                                break;
                            }
                        }
                    }
                }

                if (collision != null)
                {
                    if (State < 9 && ((collision.Type == 1 && (collision.Layer == 0 || collision.Layer == 1 || (collision.Layer == 2 && !Keyboard.GetState().IsKeyDown(Keys.S) && multiplier == 1))) | collision.Type == 2 | collision.Type == 5 | collision.Type == 7 | collision.Type == 8 | (collision.Type == 11 && !collision.Activated)))
                    {
                        if ((collision.Layer == 2 && collision.Type == 1) | (collision.Type == 6 && (State == 2 || State == 8)) | collision.Type == 7)
                        {
                            //Position += new Vector2(0, 0.05f);
                        }
                        else if (i == 0)
                        {
                            while (colCheck(colPoint, pos, collision.Sprite, collision.Position, 250))
                            {
                                pos += new Vector2(0, -1 * multiplier);
                            }
                            pos += new Vector2(0, 1 * multiplier);
                            Position = new Vector2(Position.X, pos.Y - 32 * multiplier);
                        }
                        Velocity = new Vector2(Velocity.X, i * multiplier);
                    }
                    if (collision.Type == 7 && Keyboard.GetState().IsKeyDown(Keys.W))
                    {
                        Velocity = Vector2.Zero;
                        if (FacingRight)
                        {
                            State = 9;
                        }
                        else
                        {
                            State = 10;
                        }
                    }
                    break;
                }

                i++;
            }

            return result;
        }
    }
}
