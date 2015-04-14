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
using The_Secret_Castle;

namespace TSC_Game
{
    public class GameCharacter
    {
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; private set; }
        public List<List<Texture2D>> Sprites { get; private set; }
        public Texture2D Sprite { get; private set; }
        public int Time { get; set; }
        public int TargetTime { get; set; }
        public bool[] Keyparts { get; private set; }
        private int LastScrollValue;
        public int State { get; set; }
        private int AnimPhase;
        private int AnimSpeed;
        public bool FacingRight;
        private int AnimTimer;
        private bool KeyPressed;
        private bool Clicked;
        private Texture2D PastKey;
        private Texture2D PresentKey;
        private Texture2D FutureKey;
        private GameObject PortObject;
        public bool CanPast;
        private bool CanFuture;
        private bool CanMag;
        private int teleports;
        private int EggTime;

        public GameCharacter(Vector3 position, ContentManager content, int abilities)
        {
            Position = new Vector2(position.X, position.Y);
            Time = (int)position.Z;
            Velocity = Vector2.Zero;
            Keyparts = new bool[3];
            State = 0;
            AnimPhase = 0;
            AnimTimer = 0;
            AnimSpeed = 200;
            KeyPressed = false;
            FacingRight = true;
            Clicked = false;
            LastScrollValue = Mouse.GetState().ScrollWheelValue;
            TargetTime = 0;
            PortObject = null;
            teleports = 0;
            EggTime = 0;

            if (abilities == 0)
            {
                CanPast = false;
                CanFuture = false;
                CanMag = false;
            }
            if (abilities == 1)
            {
                CanPast = true;
                CanFuture = false;
                CanMag = false;
            }
            if (abilities == 2)
            {
                CanPast = true;
                CanFuture = false;
                CanMag = true;
            }
            if (abilities == 3)
            {
                CanPast = true;
                CanFuture = true;
                CanMag = true;
            }

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
            sprites.Add(new List<string>());
            sprites.Add(new List<string>());
            sprites.Add(new List<string>());
            sprites.Add(new List<string>());
            sprites.Add(new List<string>());

            sprites[0].Add("Sprites/Character/Laufen/laufen1");
            sprites[1].Add("Sprites/Character/Laufen/laufen3");
            sprites[1].Add("Sprites/Character/Laufen/laufen4");
            sprites[1].Add("Sprites/Character/Laufen/laufen5");
            sprites[1].Add("Sprites/Character/Laufen/laufen6");
            sprites[2].Add("Sprites/Character/Leiter/leiter2");
            sprites[2].Add("Sprites/Character/Leiter/leiter3");
            sprites[3].Add("Sprites/Character/Treppe/treppe1");
            sprites[3].Add("Sprites/Character/Treppe/treppe2");
            sprites[4].Add("Sprites/Character/sprung/klettern1");
            sprites[4].Add("Sprites/Character/sprung/klettern2");
            sprites[4].Add("Sprites/Character/sprung/klettern3");
            sprites[4].Add("Sprites/Character/sprung/klettern4");
            sprites[4].Add("Sprites/Character/sprung/klettern5");
            sprites[4].Add("Sprites/Character/sprung/klettern6");
            sprites[5].Add("Sprites/Character/Schalter/schalter_boden/schalter1");
            sprites[5].Add("Sprites/Character/Schalter/schalter_boden/schalter2");
            sprites[5].Add("Sprites/Character/Schalter/schalter_boden/schalter3");
            sprites[6].Add("Sprites/Character/Schalter/schalter_wand/schalter1a");
            sprites[6].Add("Sprites/Character/Schalter/schalter_wand/schalter2a");
            sprites[6].Add("Sprites/Character/Schalter/schalter_wand/schalter3a");
            sprites[7].Add("Sprites/Character/Schalter/Zeitschalter/schalter1b");
            sprites[7].Add("Sprites/Character/Schalter/Zeitschalter/schalter2b");
            sprites[7].Add("Sprites/Character/Schalter/Zeitschalter/schalter3b");
            sprites[7].Add("Sprites/Character/Schalter/Zeitschalter/schalter4b");
            sprites[8].Add("Sprites/Character/Leiter/leiter2");
            sprites[9].Add("Sprites/Character/Treppe/treppe1");
            sprites[9].Add("Sprites/Character/Treppe/treppe2");
            sprites[10].Add("Sprites/Character/Treppe/treppe1");
            sprites[10].Add("Sprites/Character/Treppe/treppe2");
            sprites[11].Add("Sprites/Character/Treppe/treppe1");
            sprites[12].Add("Sprites/Character/Laufen/laufen1");
            sprites[13].Add("Sprites/Character/Port/port1");
            sprites[13].Add("Sprites/Character/Port/port2");
            sprites[13].Add("Sprites/Character/Port/port3");
            sprites[14].Add("Sprites/Character/Port/1");
            sprites[14].Add("Sprites/Character/Port/2");
            sprites[14].Add("Sprites/Character/Port/3");
            sprites[15].Add("Sprites/Character/Port/port1");
            sprites[15].Add("Sprites/Character/Port/port2");
            sprites[15].Add("Sprites/Character/Port/port3");
            sprites[15].Add("Sprites/Character/Port/port3");
            sprites[15].Add("Sprites/Character/Port/port3");
            sprites[16].Add("Sprites/Character/Geheim/1");
            sprites[16].Add("Sprites/Character/Geheim/2");



            Sprites = new List<List<Texture2D>>();

            for (int i = 0; i < sprites.Count; i++)
            {
                Sprites.Add(new List<Texture2D>());
                for (int j = 0; j < sprites[i].Count; j++)
                {
                    Sprites[i].Add(content.Load<Texture2D>(sprites[i][j]));
                }
            }
            Sprite = Sprites[0][0];
        }

        public void Update(Level level, Operation colCheck, GameTime gameTime, Texture2D Cursor, Vector2 Pos, Interface ui, EffectManager fx, GameCamera cam, GraphicsDevice graphics, SoundManager sound, ref bool finished, ref int ta, int controlType, ref bool restart, int Coins, int allCoins)
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
            GamePadState padState = GamePad.GetState(PlayerIndex.One);


            //Getting Input Stuff
            bool left = false;
            bool right = false;
            bool up = false;
            bool down = false;
            bool timeone = false;
            bool timetwo = false;
            bool timethree = false;
            bool timewatch = false;
            bool use = false;
            bool switchObject = false;
            bool switchSelf = false;

            if (controlType == 0)
            {
                if (keyState.IsKeyDown(Keys.A))
                {
                    left = true;
                }
                if (keyState.IsKeyDown(Keys.D))
                {
                    right = true;
                }
                if (keyState.IsKeyDown(Keys.W))
                {
                    up = true;
                }
                if (keyState.IsKeyDown(Keys.S))
                {
                    down = true;
                }
                if (keyState.IsKeyDown(Keys.D1))
                {
                    timeone = true;
                }
                if (keyState.IsKeyDown(Keys.D2))
                {
                    timetwo = true;
                }
                if (keyState.IsKeyDown(Keys.D3))
                {
                    timethree = true;
                }
                if (mouseState.RightButton == ButtonState.Pressed)
                {
                    timewatch = true;
                }
                if (keyState.IsKeyDown(Keys.E))
                {
                    use = true;
                }
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    switchObject = true;
                    switchSelf = true;
                }
            }

            if (controlType == 1)
            {
                if (padState.ThumbSticks.Left.X < 0)
                {
                    left = true;
                }
                if (padState.ThumbSticks.Left.X > 0)
                {
                    right = true;
                }
                if (padState.ThumbSticks.Left.Y > 0)
                {
                    up = true;
                }
                if (padState.ThumbSticks.Left.Y < 0)
                {
                    down = true;
                }
                if (padState.Buttons.X == ButtonState.Pressed)
                {
                    timeone = true;
                }
                if (padState.Buttons.Y == ButtonState.Pressed)
                {
                    timetwo = true;
                }
                if (padState.Buttons.B == ButtonState.Pressed)
                {
                    timethree = true;
                }
                if (padState.Buttons.LeftShoulder == ButtonState.Pressed)
                {
                    timewatch = true;
                }
                if (padState.Buttons.A == ButtonState.Pressed)
                {
                    use = true;
                }
                if (padState.Triggers.Left == 1)
                {
                    switchSelf = true;
                }
                if (padState.Triggers.Right == 1)
                {
                    switchObject = true;
                }
            }



            if (State < 12)
            {
                //Check for Collisions
                List<GameObject> ColObjects = new List<GameObject>();
                if (State != 4)
                {
                    ColObjects.Concat(Collision(level, colCheck, Cursor, controlType));

                    foreach (GameObject o in objects)
                    {
                        if (colCheck(Sprite, Position, o.Sprite, o.Position, 0))
                        {
                            ColObjects.Add(o);
                        }

                        if (o.Type == 15)
                        {
                            bool eseoikh = true;
                            for (int i = 0; i < 3; i++)
                            {
                                if (!Keyparts[i])
                                {
                                    eseoikh = false;
                                }
                            }
                            if (eseoikh)
                            {
                                double dist = Math.Sqrt(Math.Pow(Position.X - o.Position.X, 2) + Math.Pow(Position.Y - o.Position.Y, 2));
                                if (dist < 100)
                                {
                                    if (!o.Activated && !o.Activating)
                                    {
                                        o.Activate();
                                        sound.PlaySound("EndDoorAct");
                                    }
                                }
                                if (dist < 15)
                                {
                                    sound.PlaySound("winner");
                                    finished = true;
                                }
                            }

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
                    //Input
                    if (left)
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


                    if (right)
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
                        if (!left && !right)
                        {
                            State = 11;
                        }
                        if (!ColObjects.Exists(o => o.Type == 7) || ColObjects.Exists(o => o.Type == 1 && o.Layer == 1))
                        {
                            State = 0;
                        }
                    }


                    //Climbing
                    if (State < 2 && up && Math.Abs(Velocity.Y) < 0.2f)
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
                                if (o.Layer == 1 && o.Type != 9 && o.Type != 10 && o.Type != 13 && o.Type != 16)
                                {
                                    if (colCheck(Sprite, newPos, o.Sprite, o.Position, 250))
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


                if (mouseState.ScrollWheelValue > LastScrollValue && TargetTime < 2)
                {
                    TargetTime++;
                    ui.SetTargetTime(TargetTime);
                    if (fx.Index == 3)
                    {
                        if (TargetTime != Time)
                        {
                            fx.ActivateEffect(TargetTime);
                        }
                        else
                        {
                            fx.ActivateEffect(-1);
                        }
                    }
                    LastScrollValue = mouseState.ScrollWheelValue;
                }
                else if (mouseState.ScrollWheelValue < LastScrollValue && TargetTime > 0)
                {
                    TargetTime--;
                    ui.SetTargetTime(TargetTime);
                    if (fx.Index == 3)
                    {
                        if (TargetTime != Time)
                        {
                            fx.ActivateEffect(TargetTime);
                        }
                        else
                        {
                            fx.ActivateEffect(-1);
                        }
                    }
                    LastScrollValue = mouseState.ScrollWheelValue;
                }

                if (timeone && !KeyPressed && CanPast)
                {
                    KeyPressed = true;
                    ta = 2;
                    TargetTime = 0;
                    ui.SetTargetTime(TargetTime);
                    if (fx.Index == 3)
                    {
                        if (TargetTime != Time)
                        {
                            fx.ActivateEffect(TargetTime);
                        }
                        else
                        {
                            fx.ActivateEffect(-1);
                        }
                    }
                }
                if (timetwo && !KeyPressed)
                {
                    KeyPressed = true;
                    ta = 2;
                    TargetTime = 1;
                    ui.SetTargetTime(TargetTime);
                    if (fx.Index == 3)
                    {
                        if (TargetTime != Time)
                        {
                            fx.ActivateEffect(TargetTime);
                        }
                        else
                        {
                            fx.ActivateEffect(-1);
                        }
                    }
                }
                if (timethree && !KeyPressed && CanFuture)
                {
                    KeyPressed = true;
                    ta = 2;
                    TargetTime = 2;
                    ui.SetTargetTime(TargetTime);
                    if (fx.Index == 3)
                    {
                        if (TargetTime != Time)
                        {
                            fx.ActivateEffect(TargetTime);
                        }
                        else
                        {
                            fx.ActivateEffect(-1);
                        }
                    }
                }


                if (timewatch && !Clicked && CanMag)
                {
                    Clicked = true;
                    if (TargetTime != Time)
                    {
                        fx.ActivateEffect(TargetTime);
                        ta = 2;
                    }
                    else
                    {
                        fx.ActivateEffect(-1);
                    }
                }

                if (Velocity.X < -0.5f && !left)
                {
                    Velocity += new Vector2(0.25f, 0);
                }
                else if (Velocity.X > 0.5f && !right)
                {
                    Velocity += new Vector2(-0.25f, 0);
                }
                else if (!left && !right)
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
                                Position += new Vector2(0, -32);
                            }
                            if (o.Type == 3 | o.Type == 4 | o.Type == 12)
                            {
                                if (use && !KeyPressed)
                                {
                                    ta = 2;
                                    if (Time == 0)
                                    {
                                        sound.PlaySound("PastSwitch");
                                    }
                                    else if (Time == 1)
                                    {
                                        sound.PlaySound("PresentSwitch");
                                    }
                                    else if (Time == 2)
                                    {
                                        sound.PlaySound("FutureSwitch");
                                    }
                                    KeyPressed = true;
                                    Velocity = new Vector2(0, Velocity.Y);
                                    if (o.Type != 12 | (o.Type == 12 && !o.Activated))
                                    {
                                        foreach (Link l in links)
                                        {
                                            if (l.FirstObject == o)
                                            {
                                                l.SecondObject.Switch();
                                                if (l.SecondObject.Type == 2 || l.SecondObject.Type == 11)
                                                {
                                                    if (l.SecondObject.Sprites.Count > 5 && Time == 2)
                                                    {
                                                        if (!l.SecondObject.Activated)
                                                        {
                                                            sound.PlaySound("LaserbridgeOn");
                                                        }
                                                        else
                                                        {
                                                            sound.PlaySound("LaserbridgeOff");
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (Time == 0)
                                                        {
                                                            if (!l.SecondObject.Activated)
                                                            {
                                                                sound.PlaySound("OpenPastDoor");
                                                            }
                                                            else
                                                            {
                                                                sound.PlaySound("ClosePastDoor");
                                                            }
                                                        }
                                                        else if (Time == 1)
                                                        {
                                                            if (!l.SecondObject.Activated)
                                                            {
                                                                sound.PlaySound("OpenPresentDoor");
                                                            }
                                                            else
                                                            {
                                                                sound.PlaySound("ClosePresentDoor");
                                                            }
                                                        }
                                                        else if (Time == 2)
                                                        {
                                                            if (!l.SecondObject.Activated)
                                                            {
                                                                sound.PlaySound("OpenFutureDoor");
                                                            }
                                                            else
                                                            {
                                                                sound.PlaySound("CloseFutureDoor");
                                                            }
                                                        }
                                                    }
                                                }

                                                if (l.SecondObject.Type == 8)
                                                {
                                                    if (!l.SecondObject.Activated)
                                                    {
                                                        sound.PlaySound("TeleporterOn");
                                                    }
                                                    else
                                                    {
                                                        sound.PlaySound("TeleporterOff");
                                                    }
                                                }
                                            }
                                            if (l.SecondObject == o)
                                            {
                                                l.FirstObject.Switch();
                                                if (l.FirstObject.Type == 2 || l.FirstObject.Type == 11)
                                                {
                                                    if (l.FirstObject.Sprites.Count > 5 && Time == 2)
                                                    {
                                                        if (!l.FirstObject.Activated)
                                                        {
                                                            sound.PlaySound("LaserbridgeOn");
                                                        }
                                                        else
                                                        {
                                                            sound.PlaySound("LaserbridgeOff");
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (Time == 0)
                                                        {
                                                            if (!l.FirstObject.Activated)
                                                            {
                                                                sound.PlaySound("OpenPastDoor");
                                                            }
                                                            else
                                                            {
                                                                sound.PlaySound("ClosePastDoor");
                                                            }
                                                        }
                                                        else if (Time == 1)
                                                        {
                                                            if (!l.FirstObject.Activated)
                                                            {
                                                                sound.PlaySound("OpenPresentDoor");
                                                            }
                                                            else
                                                            {
                                                                sound.PlaySound("ClosePresentDoor");
                                                            }
                                                        }
                                                        else if (Time == 2)
                                                        {
                                                            if (!l.FirstObject.Activated)
                                                            {
                                                                sound.PlaySound("OpenFutureDoor");
                                                            }
                                                            else
                                                            {
                                                                sound.PlaySound("CloseFutureDoor");
                                                            }
                                                        }
                                                    }
                                                }

                                                if (l.FirstObject.Type == 8)
                                                {
                                                    if (!l.FirstObject.Activated)
                                                    {
                                                        sound.PlaySound("TeleporterOn");
                                                    }
                                                    else
                                                    {
                                                        sound.PlaySound("TeleporterOff");
                                                    }
                                                }
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
                                                FacingRight = true;
                                                Position = new Vector2(o.Position.X - 20, Position.Y);
                                            }
                                            else
                                            {
                                                FacingRight = false;
                                                Position = new Vector2(o.Position.X + 20, Position.Y);
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

                            if (o.Type == 6 && (up | down))
                            {
                                State = 2;
                                FacingRight = true;
                                Position = new Vector2(o.Position.X + 3, Position.Y);
                            }

                            if (o.Type == 8)
                            {
                                if (use && !KeyPressed && o.Activated)
                                {
                                    KeyPressed = true;
                                    foreach (Link l in links)
                                    {
                                        if (l.Direction == 0)
                                        {
                                            if (l.FirstObject == o && l.SecondObject.Type == 8 && l.SecondObject.Activated)
                                            {
                                                teleports++;
                                                EggTime = 5000;
                                                sound.PlaySound("Teleport");
                                                fx.ActivateEffect(l.SecondObject.Position);
                                            }
                                            if (l.SecondObject == o && l.FirstObject.Type == 8 && l.FirstObject.Activated)
                                            {
                                                teleports++;
                                                EggTime = 5000;
                                                sound.PlaySound("Teleport");
                                                fx.ActivateEffect(l.FirstObject.Position);
                                            }
                                        }
                                        else
                                        {
                                            if (l.FirstObject == o && l.SecondObject.Type == 8 && l.SecondObject.Activated)
                                            {
                                                teleports++;
                                                EggTime = 5000;
                                                sound.PlaySound("Teleport");
                                                fx.ActivateEffect(l.SecondObject.Position);
                                            }
                                        }
                                    }
                                }
                            }

                            if (o.Type == 9)
                            {
                                objects.Remove(o);
                                ui.AddCoin();
                                sound.PlaySound("ColC");
                            }

                            if (o.Type == 10)
                            {
                                objects.Remove(o);
                                if (o.SpritePath == "Sprites/Objects/Gameobjects/schluessel 1")
                                {
                                    Keyparts[0] = true;
                                }
                                if (o.SpritePath == "Sprites/Objects/Gameobjects/schluessel 2")
                                {
                                    Keyparts[1] = true;
                                }
                                if (o.SpritePath == "Sprites/Objects/Gameobjects/schluessel 3")
                                {
                                    Keyparts[2] = true;
                                }
                                sound.PlaySound("ColK");
                            }

                            if (o.Type == 13)
                            {
                                objects.Remove(o);
                                ui.AddDiamond(0);
                                sound.PlaySound("ColD");
                            }

                            if (o.Type == 15)
                            {
                                bool kot = false;
                                for (int i = 0; i < 3; i++)
                                {
                                    if (!Keyparts[i])
                                    {
                                        kot = true;
                                    }
                                }
                                if (!kot && !o.Activated)
                                {
                                    o.Activate();
                                }
                            }

                            if (o.Type == 16)
                            {
                                ta = 1;
                                objects.Remove(o);
                            }


                            if (o.Type == 17)
                            {
                                CanMag = true;
                                objects.Remove(o);
                            }
                        }
                    }

                    if (State == 2)
                    {
                        if (up && Velocity.Y > -0.75f)
                        {
                            Velocity += new Vector2(0, -0.25f);
                        }
                        else if (down && Velocity.Y < 0.75f)
                        {
                            Velocity += new Vector2(0, 0.25f);
                        }
                        else if (!up && !down)
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
                    if (!ui.OutOfCharges())
                    {
                        bool krueppeldoener = false;
                        foreach (GameObject o in objects)
                        {
                            if (o.Portable)
                            {
                                bool todeskarpfen = false;
                                if (controlType == 0)
                                {
                                    todeskarpfen = colCheck(Cursor, Pos, o.Sprite, o.Position, 0);
                                }
                                else
                                {
                                    int mult = 1;
                                    if (!FacingRight)
                                    {
                                        mult = -1;
                                    }
                                    for (int i = 0; i < 11; i++)
                                    {
                                        if (todeskarpfen)
                                        {
                                            break;
                                        }
                                        else
                                        {
                                            todeskarpfen = colCheck(Cursor, Position + new Vector2((32 + i) * mult, 0), o.Sprite, o.Position, 0);
                                        }
                                    }
                                }
                                if (!Clicked & todeskarpfen)
                                {
                                    krueppeldoener = true;
                                    ui.Portable(1);
                                    if (switchObject && TargetTime == 0 && Vector2.Distance(Position, o.Position) < 75)
                                    {
                                        if (PortCheck(colCheck, o.Sprite, o.Position, level.Past))
                                        {
                                            State = 14;
                                            Clicked = true;
                                            PortObject = o;
                                            //Vector2 pos = new Vector2((o.Position.X + cam.Position.X) / (float)graphics.Viewport.Width, (o.Position.Y + cam.Position.Y) / (float)graphics.Viewport.Height);
                                        }
                                        else
                                        {
                                            ui.Portable(2);
                                        }
                                        break;
                                    }
                                    else if (switchObject && TargetTime == 1 && Vector2.Distance(Position, o.Position) < 75)
                                    {
                                        if (PortCheck(colCheck, o.Sprite, o.Position, level.Present))
                                        {
                                            State = 14;
                                            Clicked = true;
                                            PortObject = o;
                                        }
                                        else
                                        {
                                            ui.Portable(2);
                                        }
                                        break;
                                    }
                                    else if (switchObject && TargetTime == 2 && Vector2.Distance(Position, o.Position) < 75)
                                    {
                                        if (PortCheck(colCheck, o.Sprite, o.Position, level.Future))
                                        {
                                            State = 14;
                                            Clicked = true;
                                            PortObject = o;
                                        }
                                        else
                                        {
                                            ui.Portable(2);
                                        }
                                        break;
                                    }
                                    break;
                                }
                                else
                                {
                                    ui.Portable(0);
                                }
                            }
                        }

                        bool karpfenderverdammnis = false;
                        if (controlType == 0)
                        {
                            if (colCheck(Cursor, Pos, Sprite, Position, 0))
                            {
                                karpfenderverdammnis = true;
                            }
                        }
                        if (controlType == 1)
                        {
                            karpfenderverdammnis = true;
                        }
                        if (!Clicked & karpfenderverdammnis)
                        {
                            ui.Portable(1);
                            if (switchSelf && TargetTime == 0 && Time != 0)
                            {
                                if (Math.Abs(Velocity.Y) < 0.2f && PortCheck(colCheck, Sprite, Position, level.Past))
                                {
                                    Clicked = true;
                                    State = 13;
                                }
                                else
                                {
                                    ui.Portable(2);
                                }
                            }
                            if (switchSelf && TargetTime == 1 && Time != 1)
                            {
                                if (Math.Abs(Velocity.Y) < 0.2f && PortCheck(colCheck, Sprite, Position, level.Present))
                                {
                                    Clicked = true;
                                    State = 13;
                                }
                                else
                                {
                                    ui.Portable(2);
                                }
                            }
                            if (switchSelf && TargetTime == 2 && Time != 2)
                            {
                                if (Math.Abs(Velocity.Y) < 0.2f && PortCheck(colCheck, Sprite, Position, level.Future))
                                {
                                    Clicked = true;
                                    State = 13;
                                }
                                else
                                {
                                    ui.Portable(2);
                                }
                            }
                        }
                        else if (!krueppeldoener)
                        {
                            ui.Portable(0);
                        }
                    }
                    else
                    {
                        if (switchObject || switchSelf)
                        {
                            State = 15;
                        }
                        ui.Portable(0);
                    }

                }

                if (ui.TimeIsUp())
                {
                    State = 15;
                }

                if (Coins == allCoins && teleports >= 20)
                {
                    State = 16;
                }

            }

            //UUUUUUUUULLLLLLLLLLLLLLLTTTTTTTTTTTTTTTTRRRRRRRRRRRRRRAAAAAAAAAAAAAAAA GEHEIM
            if (State == 16)
            {
                EggTime += gameTime.ElapsedGameTime.Milliseconds;

                if (EggTime > 5000)
                {
                    State = 0;
                }
            }
            else
            {
                if (EggTime > 0)
                {
                    EggTime -= gameTime.ElapsedGameTime.Milliseconds;
                }
                else
                {
                    teleports = 0;
                }
            }


            //Animation
            if ((State < 2 || State == 8) && (left | right))
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
                    if (State == 15)
                    {
                        restart = true;
                    }
                    if (State != 13)
                    {
                        AnimPhase = 0;
                    }
                    else if (fx.Index != 1)
                    {
                        sound.PlaySound("Timeswitch");
                        ui.RemoveCharge();
                        fx.ActivateEffect(new Vector2(Position.X / 2048, Position.Y / 2048), TargetTime);
                        ta = 2;
                    }
                    if (State > 3 && State < 8)
                    {
                        State = 0;
                        AnimSpeed = 200;
                        Velocity = new Vector2(0, Velocity.Y);
                    }
                    if (State == 14 && fx.Index != 0)
                    {
                        if (Time == 0)
                        {
                            sound.PlaySound("Timeswitch");
                            level.Past.Remove(PortObject);
                            fx.ActivateEffect(PortObject, TargetTime);
                            ui.RemoveCharge();
                            ta = 2;
                        }
                        if (Time == 1)
                        {
                            sound.PlaySound("Timeswitch");
                            level.Present.Remove(PortObject);
                            fx.ActivateEffect(PortObject, TargetTime);
                            ui.RemoveCharge();
                            ta = 2;
                        }
                        if (Time == 2)
                        {
                            sound.PlaySound("Timeswitch");
                            level.Future.Remove(PortObject);
                            fx.ActivateEffect(PortObject, TargetTime);
                            ui.RemoveCharge();
                            ta = 2;
                        }
                        PortObject = null;
                        State = 0;
                    }
                }
            }

            Sprite = Sprites[State][AnimPhase];

            if (!switchSelf && !switchObject && !timewatch)
            {
                Clicked = false;
            }

            if (!left && !right && !up && !down && !timeone && !timetwo && !timethree && !use && KeyPressed)
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
                if (o.Layer == 1 && ((o.Type != 6 && o.Type != 9 && o.Type != 10 && o.Type != 13 && o.Type != 16) || (o.Type == 11 && !o.Activated)))
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

        public List<GameObject> Collision(Level level, Operation colCheck, Texture2D colPoint, int controlType)
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

            bool darmwindkannibalismusunten = false;
            bool darmwindkannibalismusoben = false;
            if (controlType == 0)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.S))
                {
                    darmwindkannibalismusunten = true;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.W))
                {
                    darmwindkannibalismusoben = true;
                }
            }

            if (controlType == 1)
            {
                if (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y < 0)
                {
                    darmwindkannibalismusunten = true;
                }
                if (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y > 0)
                {
                    darmwindkannibalismusoben = true;
                }
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
                    if (State < 9 && (collision.Type == 1 | collision.Type == 2 | collision.Type == 5 | collision.Type == 8 | (collision.Type == 11 && !collision.Activated) | collision.Type == 15))
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
                        if (State < 9 && (collision.Type == 1 | collision.Type == 2 | collision.Type == 5 | collision.Type == 8 | (collision.Type == 11 && !collision.Activated) | collision.Type == 15))
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
                int thresh = 250;
                if (collision == null)
                {
                    foreach (GameObject o in objects)
                    {
                        if (o.Layer == 1 | ((o.Layer == 0 | o.Layer == 2) && o.Type == 1))
                        {
                            if (colCheck(colPoint, pos + new Vector2(0, i * multiplier), o.Sprite, o.Position, thresh))
                            {
                                if (o.Type == 8)
                                {
                                    thresh = 250;
                                }
                                else
                                {
                                    thresh = 0;
                                }
                                collision = o;
                                result.Add(o);
                                break;
                            }
                        }
                    }
                }

                if (collision != null)
                {
                    if (State < 9 && ((collision.Type == 1 && (collision.Layer == 0 || collision.Layer == 1 || (collision.Layer == 2 && !darmwindkannibalismusunten && multiplier == 1))) | collision.Type == 2 | collision.Type == 5 | collision.Type == 7 | collision.Type == 8 | (collision.Type == 11 && !collision.Activated) | collision.Type == 15))
                    {
                        if ((collision.Layer == 2 && collision.Type == 1) | (collision.Type == 6 && (State == 2 || State == 8)) | collision.Type == 7)
                        {
                            //Position += new Vector2(0, 0.05f);
                        }
                        else if (i == 0)
                        {
                            while (colCheck(colPoint, pos, collision.Sprite, collision.Position, thresh))
                            {
                                pos += new Vector2(0, -1 * multiplier);
                            }
                            pos += new Vector2(0, 1 * multiplier);
                            Position = new Vector2(Position.X, pos.Y - 32 * multiplier);
                        }
                        Velocity = new Vector2(Velocity.X, i * multiplier);
                    }
                    if (collision.Type == 7 && darmwindkannibalismusoben)
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
                int thresh = 0;
                if (collision == null)
                {
                    foreach (GameObject o in objects)
                    {
                        if (o.Layer == 1 | ((o.Layer == 0 | o.Layer == 2) && o.Type == 1))
                        {
                            if (colCheck(colPoint, pos + new Vector2(0, i * multiplier), o.Sprite, o.Position, thresh))
                            {
                                if (o.Type == 8)
                                {
                                    thresh = 250;
                                }
                                else
                                {
                                    thresh = 0;
                                }
                                collision = o;
                                result.Add(o);
                                break;
                            }
                        }
                    }
                }

                if (collision != null)
                {
                    if (State < 9 && ((collision.Type == 1 && (collision.Layer == 0 || collision.Layer == 1 || (collision.Layer == 2 && !darmwindkannibalismusunten && multiplier == 1))) | collision.Type == 2 | collision.Type == 5 | collision.Type == 7 | collision.Type == 8 | (collision.Type == 11 && !collision.Activated) | collision.Type == 15))
                    {
                        if ((collision.Layer == 2 && collision.Type == 1) | (collision.Type == 6 && (State == 2 || State == 8)) | collision.Type == 7)
                        {
                            //Position += new Vector2(0, 0.05f);
                        }
                        else if (i == 0)
                        {
                            while (colCheck(colPoint, pos, collision.Sprite, collision.Position, thresh))
                            {
                                pos += new Vector2(0, -1 * multiplier);
                            }
                            pos += new Vector2(0, 1 * multiplier);
                            Position = new Vector2(Position.X, pos.Y - 32 * multiplier);
                        }
                        Velocity = new Vector2(Velocity.X, i * multiplier);
                    }
                    if (collision.Type == 7 && darmwindkannibalismusoben)
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


        public void ChangeSprites(int change, ContentManager content)
        {
            if (change == 1)
            {
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
                sprites.Add(new List<string>());
                sprites.Add(new List<string>());
                sprites.Add(new List<string>());
                sprites.Add(new List<string>());
                sprites.Add(new List<string>());

                sprites[0].Add("Sprites/Character/Laufen/laufen1");
                sprites[1].Add("Sprites/Character/Laufen/laufen3");
                sprites[1].Add("Sprites/Character/Laufen/laufen4");
                sprites[1].Add("Sprites/Character/Laufen/laufen5");
                sprites[1].Add("Sprites/Character/Laufen/laufen6");
                sprites[2].Add("Sprites/Character/Leiter/leiter2");
                sprites[2].Add("Sprites/Character/Leiter/leiter3");
                sprites[3].Add("Sprites/Character/Treppe/treppe1");
                sprites[3].Add("Sprites/Character/Treppe/treppe2");
                sprites[4].Add("Sprites/Character/sprung/klettern1");
                sprites[4].Add("Sprites/Character/sprung/klettern2");
                sprites[4].Add("Sprites/Character/sprung/klettern3");
                sprites[4].Add("Sprites/Character/sprung/klettern4");
                sprites[4].Add("Sprites/Character/sprung/klettern5");
                sprites[4].Add("Sprites/Character/sprung/klettern6");
                sprites[5].Add("Sprites/Character/Schalter/schalter_boden/schalter1");
                sprites[5].Add("Sprites/Character/Schalter/schalter_boden/schalter2");
                sprites[5].Add("Sprites/Character/Schalter/schalter_boden/schalter3");
                sprites[6].Add("Sprites/Character/Schalter/schalter_wand/schalter1a");
                sprites[6].Add("Sprites/Character/Schalter/schalter_wand/schalter2a");
                sprites[6].Add("Sprites/Character/Schalter/schalter_wand/schalter3a");
                sprites[7].Add("Sprites/Character/Schalter/Zeitschalter/schalter1b");
                sprites[7].Add("Sprites/Character/Schalter/Zeitschalter/schalter2b");
                sprites[7].Add("Sprites/Character/Schalter/Zeitschalter/schalter3b");
                sprites[7].Add("Sprites/Character/Schalter/Zeitschalter/schalter4b");
                sprites[8].Add("Sprites/Character/Leiter/leiter2");
                sprites[9].Add("Sprites/Character/Treppe/treppe1");
                sprites[9].Add("Sprites/Character/Treppe/treppe2");
                sprites[10].Add("Sprites/Character/Treppe/treppe1");
                sprites[10].Add("Sprites/Character/Treppe/treppe2");
                sprites[11].Add("Sprites/Character/Treppe/treppe1");
                sprites[12].Add("Sprites/Character/Laufen/laufen1");
                sprites[13].Add("Sprites/Character/Port/port1");
                sprites[13].Add("Sprites/Character/Port/port2");
                sprites[13].Add("Sprites/Character/Port/port3");
                sprites[14].Add("Sprites/Character/Port/1");
                sprites[14].Add("Sprites/Character/Port/2");
                sprites[14].Add("Sprites/Character/Port/3");
                sprites[15].Add("Sprites/Character/Port/port1");
                sprites[15].Add("Sprites/Character/Port/port2");
                sprites[15].Add("Sprites/Character/Port/port3");
                sprites[15].Add("Sprites/Character/Port/port3");
                sprites[15].Add("Sprites/Character/Port/port3");
                sprites[16].Add("Sprites/Character/Geheim/1");
                sprites[16].Add("Sprites/Character/Geheim/2");



                Sprites = new List<List<Texture2D>>();

                for (int i = 0; i < sprites.Count; i++)
                {
                    Sprites.Add(new List<Texture2D>());
                    for (int j = 0; j < sprites[i].Count; j++)
                    {
                        Sprites[i].Add(content.Load<Texture2D>(sprites[i][j]));
                    }
                }
                Sprite = Sprites[0][0];
            }
            else
            {
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
                sprites.Add(new List<string>());
                sprites.Add(new List<string>());
                sprites.Add(new List<string>());
                sprites.Add(new List<string>());
                sprites.Add(new List<string>());

                sprites[0].Add("Sprites/Character/Laufen/char1");
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
                sprites[12].Add("Sprites/Character/Laufen/char1");
                sprites[13].Add("Sprites/Character/Port/port1");
                sprites[13].Add("Sprites/Character/Port/port2");
                sprites[13].Add("Sprites/Character/Port/port3");
                sprites[14].Add("Sprites/Character/Port/1");
                sprites[14].Add("Sprites/Character/Port/2");
                sprites[14].Add("Sprites/Character/Port/3");
                sprites[15].Add("Sprites/Character/sprung/sprung5");
                sprites[15].Add("Sprites/Character/sprung/sprung5");
                sprites[15].Add("Sprites/Character/sprung/sprung5");
                sprites[15].Add("Sprites/Character/sprung/sprung5");
                sprites[15].Add("Sprites/Character/sprung/sprung5");
                sprites[16].Add("Sprites/Character/Geheim/1");
                sprites[16].Add("Sprites/Character/Geheim/2");



                Sprites = new List<List<Texture2D>>();

                for (int i = 0; i < sprites.Count; i++)
                {
                    Sprites.Add(new List<Texture2D>());
                    for (int j = 0; j < sprites[i].Count; j++)
                    {
                        Sprites[i].Add(content.Load<Texture2D>(sprites[i][j]));
                    }
                }
                Sprite = Sprites[0][0];
            }
        }
    }
}
