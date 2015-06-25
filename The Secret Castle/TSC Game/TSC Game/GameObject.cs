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
using The_Secret_Castle;

namespace TSC_Game
{
    public class GameObject
    {
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public bool Static { get; set; }
        public bool Portable { get; set; }
        public int Layer { get; set; }
        public int Type { get; set; }
        public bool Activated { get; set; }
        public bool Activating { get; set; }
        public Texture2D Sprite { get; set; }
        public List<Texture2D> Sprites { get; set; }
        public string SpritePath { get; set; }
        public List<string> SpritePaths { get; set; }
        public int AnimTimer { get; set; }
        public int AnimPhase { get; set; }
        public int AnimSpeed { get; set; }
        public int Loop { get; set; }
        public int LoopDir { get; set; }

        public GameObject()
        {

        }

        public GameObject(Vector2 position, string spritePath, int layer, int type, ContentManager content)
        {
            Position = position;
            Velocity = Vector2.Zero;
            SpritePath = spritePath;
            Sprite = content.Load<Texture2D>(spritePath);
            SpritePaths = null;
            Sprites = null;
            Layer = layer;
            Static = true;
            Portable = false;
            Type = type;
            Activated = false;
            Activating = false;
            AnimTimer = 0;
            AnimPhase = 0;
            Loop = 0;
        }

        public GameObject(Vector2 position, List<string> spritePaths, int animSpeed, int loop, int layer, int type, ContentManager content)
        {
            Position = position;
            Velocity = Vector2.Zero;
            SpritePath = spritePaths.ElementAt<string>(0);
            SpritePaths = spritePaths;
            Sprite = content.Load<Texture2D>(spritePaths.ElementAt<string>(0));
            Sprites = new List<Texture2D>();
            for (int i = 0; i < SpritePaths.Count; i++)
            {
                Sprites.Add(content.Load<Texture2D>(SpritePaths.ElementAt<string>(i)));
            }
            Layer = layer;
            Static = true;
            Portable = false;
            Type = type;
            Activated = false;
            Activating = false;
            AnimTimer = 0;
            AnimPhase = 0;
            AnimSpeed = animSpeed;
            Loop = loop;
            LoopDir = 0;
        }

        public void Update(GameCharacter player, List<GameObject> objects, List<Link> links, Operation colCheck, GameTime gameTime, Texture2D cursor, OperationTwo colCheckTwo)
        {
            if (Type == 12)
            {
                if (!Activated && !Activating)
                {
                    foreach (Link l in links)
                    {
                        if (l.FirstObject == this)
                        {
                            Activating = true;
                            if (l.SecondObject.Activated)
                            {
                                l.SecondObject.Deactivate();
                            }
                            else
                            {
                                l.SecondObject.Activate();
                            }
                        }
                        if (l.SecondObject == this)
                        {
                            Activating = true;
                            if (l.FirstObject.Activated)
                            {
                                l.FirstObject.Deactivate();
                            }
                            else
                            {
                                l.FirstObject.Activate();
                            }
                        }
                    }
                }
            }

            if (Type == 5)
            {
                bool obOn = false;
                foreach (GameObject o in objects)
                {
                    if (o.Layer == 1 && o != this &&
                        colCheckTwo(new Rectangle((int)Position.X - (int)Sprite.Width / 4, (int)Position.Y - (int)Sprite.Height / 4, (int)Sprite.Width / 2, (int)Sprite.Height / 2), new Rectangle((int)o.Position.X - (int)o.Sprite.Width / 4, (int)o.Position.Y - (int)o.Sprite.Height / 4, (int)o.Sprite.Width / 2, (int)o.Sprite.Height / 2)))
                    {
                        if (!Activated && !Activating)
                        {
                            foreach (Link l in links)
                            {
                                if (l.FirstObject == this)
                                {
                                    l.SecondObject.Switch();
                                }
                                if (l.SecondObject == this)
                                {
                                    l.FirstObject.Switch();
                                }
                            }
                            Activate();
                        }

                        obOn = true;
                        break;
                    }
                    else
                    {
                        obOn = false;
                    }
                }

                if (player != null && !Activating && !obOn)
                {
                    if (!colCheckTwo(new Rectangle((int)Position.X - (int)Sprite.Width / 4, (int)Position.Y - (int)Sprite.Height / 4, (int)Sprite.Width / 2, (int)Sprite.Height / 2), new Rectangle((int)player.Position.X - (int)player.Sprite.Width / 4, (int)player.Position.Y - (int)player.Sprite.Height / 4, (int)player.Sprite.Width / 2, (int)player.Sprite.Height / 2)))
                    {
                        if (Activated)
                        {
                            foreach (Link l in links)
                            {
                                if (l.FirstObject == this)
                                {
                                    l.SecondObject.Switch();
                                }
                                if (l.SecondObject == this)
                                {
                                    l.FirstObject.Switch();
                                }
                            }
                        }
                        Deactivate();
                    }
                    else
                    {
                        if (!Activated)
                        {
                            foreach (Link l in links)
                            {
                                if (l.FirstObject == this)
                                {
                                    l.SecondObject.Switch();
                                }
                                if (l.SecondObject == this)
                                {
                                    l.FirstObject.Switch();
                                }
                            }
                        }
                        Activate();
                    }
                }
            }

            //Animation
            if (Sprites != null)
            {
                AnimTimer += gameTime.ElapsedGameTime.Milliseconds;
                if (AnimTimer > AnimSpeed)
                {
                    if (LoopDir > 0)
                    {
                        if (LoopDir == 1)
                        {
                            if (AnimPhase <= Loop)
                            {
                                AnimPhase++;
                                AnimTimer = 0;
                            }
                            else
                            {
                                if (Type == 9 | Type == 13)
                                {
                                    AnimPhase = 1;
                                }
                                else
                                {
                                    LoopDir = 2;
                                }
                            }
                        }
                        if (LoopDir == 2)
                        {
                            if (AnimPhase > 1)
                            {
                                AnimPhase--;
                                AnimTimer = 0;
                            }
                            else
                            {
                                LoopDir = 1;
                            }
                        }
                    }
                    else
                    {
                        if (AnimPhase >= 2 + Loop)
                        {
                            if (Activated)
                            {
                                if (AnimPhase > 2 + Loop)
                                {
                                    AnimPhase--;
                                    AnimTimer = 0;
                                }
                                else
                                {
                                    AnimPhase = 0;
                                    Activated = false;
                                    LoopDir = 0;
                                    Activating = false;
                                }
                            }
                            else
                            {
                                if (AnimPhase < Sprites.Count - 1)
                                {
                                    AnimPhase++;
                                    AnimTimer = 0;
                                }
                                else
                                {
                                    AnimPhase = 1;
                                    Activated = true;
                                    Activating = false;
                                    LoopDir = 1;
                                    if (Type == 4)
                                    {
                                        Deactivate();
                                        foreach (Link l in links)
                                        {
                                            if (l.FirstObject == this)
                                            {
                                                l.SecondObject.Switch();
                                            }
                                            if (l.SecondObject == this)
                                            {
                                                l.FirstObject.Switch();
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    Sprite = Sprites[AnimPhase];
                }
            }

            //Physics
            if (!Static && player != null)
            {
                //Check for Collisions
                GameObject ColObject = Collision(objects, colCheck, cursor);

                Position += Velocity * gameTime.ElapsedGameTime.Milliseconds / 10;

                Velocity += new Vector2(0f, 0.1f);

                /*if (ColObject != null && ColObject.Type == 5)
                {
                    if (!ColObject.Activated)
                    {
                        ColObject.Switch();
                        foreach (Link l in links)
                        {
                            if (l.FirstObject == ColObject)
                            {
                                l.SecondObject.Switch();
                            }
                            if (l.SecondObject == ColObject)
                            {
                                l.FirstObject.Switch();
                            }
                        }
                    }
                }*/
            }
        }

        public void Activate()
        {
            if (Sprites != null)
            {
                if (Type == 12)
                {
                    AnimPhase = Sprites.Count - 1;
                    Activated = true;
                    Deactivate();
                    Activating = true;
                }
                else
                {
                    LoopDir = 0;
                    if (Sprites.Count > 2)
                    {
                        if (!Activating)
                        {
                            if (!Activated)
                            {
                                Activating = true;
                                AnimPhase = 2 + Loop;
                            }
                        }
                        else if (Type != 12)
                        {
                            Activated = !Activated;
                        }
                    }
                    else
                    {
                        if (!Activated)
                        {
                            AnimPhase = 1;
                            Activated = true;
                        }
                    }
                }
            }
        }

        public void Deactivate()
        {
            if (Sprites != null)
            {
                LoopDir = 0;
                if (Sprites.Count > 2)
                {
                    if (!Activating)
                    {
                        if (Activated)
                        {
                            AnimPhase = Sprites.Count - 1;
                            Activating = true;
                        }
                    }
                    else if (Type != 12)
                    {
                        Activated = !Activated;
                    }
                }
                else
                {
                    if (Activated)
                    {
                        AnimPhase = 0;
                        Activated = false;
                    }
                }
            }
        }

        public void Switch()
        {
            if (Activated && Type != 12)
            {
                Deactivate();
            }
            else
            {
                Activate();
            }
        }

        public void Draw(bool transparent, SpriteBatch spriteBatch)
        {
            Color drawColor = Color.White;
            if (Layer == 1)
            {
                drawColor = Color.WhiteSmoke;
            }
            if (Layer == 2)
            {
                drawColor = Color.Gray;
            }
            if (transparent)
            {
                drawColor *= 0.2f;
            }

            float layer = 0.4f * Layer;
            if (Type == 0)
            {
                layer = 0.5f * Layer;
            }

            spriteBatch.Draw(Sprite, Position, null, drawColor, 0f, new Vector2(Sprite.Width / 2, Sprite.Height / 2), 1f, SpriteEffects.None, layer);
        }

        public GameObject Collision(List<GameObject> objects, Operation colCheck, Texture2D colPoint)
        {
            Position += new Vector2(0, -0.05f);

            Vector2 pos = Position;
            int multiplier = 1;
            if (Velocity.Y > 0)
            {
                pos += new Vector2(0, Sprite.Height / 2);
            }
            else if (Velocity.Y < 0)
            {
                pos += new Vector2(0, -Sprite.Height / 2);
                multiplier = -1;
            }
            int i = 0;
            GameObject collision = null;
            while (i <= Math.Abs(Velocity.Y))
            {
                if (collision == null)
                {
                    foreach (GameObject o in objects)
                    {
                        if (o != this && (o.Layer == 1 | (o.Layer == 2 && o.Type == 1)))
                        {
                            if (colCheck(colPoint, pos + new Vector2(0, i * multiplier), o.Sprite, o.Position, 250))
                            {
                                collision = o;
                                break;
                            }
                        }
                    }
                }

                if (collision != null)
                {
                    if (collision.Type == 1 | collision.Type == 2 | collision.Type == 5 | collision.Type == 8 | collision.Type == 9 | collision.Type == 10 | (collision.Type == 11 && !collision.Activated))
                    {
                        if (i == 0)
                        {
                            while (colCheck(colPoint, pos, collision.Sprite, collision.Position, 250))
                            {
                                pos += new Vector2(0, -1 * multiplier);
                            }
                            pos += new Vector2(0, 1 * multiplier);
                            Position = new Vector2(Position.X, pos.Y - Sprite.Height / 2 * multiplier);
                        }
                        Velocity = new Vector2(Velocity.X, i * multiplier);
                    }
                    break;
                }

                i++;
            }

            return collision;
        }
    }
}
