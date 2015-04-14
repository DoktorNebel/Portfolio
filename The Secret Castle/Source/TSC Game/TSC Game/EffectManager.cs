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
    public class EffectManager
    {
        private List<Effect> Effects;
        public int Index { get; private set; }
        public float Speed { get; private set; }
        public float Intensity;
        private int Value;
        private Vector2 Position;
        public GameObject PortObject { get; private set; }
        private Texture2D BG;
        public int NewTime { get; private set; }
        private bool stickmoved;

        public EffectManager(ContentManager content)
        {
            Effects = new List<Effect>();
            Effects.Add(content.Load<Effect>("Shaders/TimeEffect"));
            Effects.Add(content.Load<Effect>("Shaders/TimeEffect2"));
            Effects.Add(content.Load<Effect>("Shaders/TeleportEffect"));
            Effects.Add(content.Load<Effect>("Shaders/TimeEffect2"));
            Index = -1;
            PortObject = null;
            Position = Vector2.Zero;
            Speed = 0;
            Intensity = 20;
            Value = 2;
            NewTime = -1;
            stickmoved = false;
        }

        public void Update(GameTime gameTime, Level level, GameCharacter player, GameCamera cam, int controlType)
        {
            if (Index == 0)
            {
                if (Intensity > Value)
                {
                    Intensity += Speed;
                }
                else
                {
                    if (NewTime == 0)
                    {
                        level.Past.Add(PortObject);
                    }
                    else if (NewTime == 1)
                    {
                        level.Present.Add(PortObject);
                    }
                    else if (NewTime == 2)
                    {
                        level.Future.Add(PortObject);
                    }
                    Index = -1;
                    PortObject = null;
                    Position = Vector2.Zero;
                    Speed = 0;
                    Intensity = 20;
                    Value = 2;
                    NewTime = 0;
                }
            }

            if (Index == 1)
            {
                if (Speed < 0)
                {
                    if (Intensity > Value)
                    {
                        Intensity += Speed;
                    }
                    else
                    {
                        Intensity = 0;
                        Speed = 5f;
                        Value = 1000;
                    }
                }
                else
                {
                    if (Intensity < Value)
                    {
                        Intensity += Speed;
                        Speed += 0.5f;
                    }
                    else
                    {
                        player.Time = NewTime;
                        player.State = 0;
                        Index = -1;
                        PortObject = null;
                        Position = Vector2.Zero;
                        Speed = 0;
                        Intensity = 20;
                        Value = 2;
                        NewTime = 0;
                    }
                }
            }

            if (Index == 2)
            {
                if (Speed > 0)
                {
                    if (Intensity < Value)
                    {
                        Intensity += Speed;
                    }
                    else
                    {
                        player.Position = Position;
                        Speed = -0.03f;
                        Value = 0;
                    }
                }
                else
                {
                    if (Intensity > Value)
                    {
                        Intensity += Speed;
                    }
                    else
                    {
                        Index = -1;
                        Position = Vector2.Zero;
                        Speed = 0;
                        Intensity = 20;
                        Value = 2;
                    }
                }
            }

            if (Index == 3)
            {
                if (controlType == 0)
                {
                    MouseState mouseState = Mouse.GetState();
                    Position = new Vector2(((float)mouseState.X - cam.Position.X) / 2048f, ((float)mouseState.Y - cam.Position.Y) / 2048f);
                }
                if (controlType == 1)
                {
                    GamePadState padState = GamePad.GetState(PlayerIndex.One);
                    if (!stickmoved)
                    {
                        Position = player.Position / 2048f;
                    }
                    else
                    {
                        Position += new Vector2(padState.ThumbSticks.Right.X * 0.01f, padState.ThumbSticks.Right.Y * -0.01f);
                    }

                    if (padState.ThumbSticks.Right.X != 0f || padState.ThumbSticks.Right.Y != 0f)
                    {
                        stickmoved = true;
                    }
                    //Position = new Vector2((player.Position.X + padState.ThumbSticks.Right.X * 500) / 2048f, (player.Position.Y + padState.ThumbSticks.Right.Y * 500 * -1) / 2048f);
                    //Position += new Vector2(padState.ThumbSticks.Right.X * 10, padState.ThumbSticks.Right.Y * -10);
                }
                if (Intensity < Value)
                {
                    Intensity += Speed;
                }
            }
        }

        public void ApplyEffect()
        {
            if (Index == 0 || (Index == 1 && Speed < 0))
            {
                Effects[0].Parameters["pos"].SetValue(Position);
                Effects[0].Parameters["intensity"].SetValue(Intensity);
                Effects[0].CurrentTechnique.Passes[0].Apply();
            }
            if (Index == 1 && Speed > 0)
            {
                Effects[Index].Parameters["pos"].SetValue(Position);
                Effects[Index].Parameters["intensity"].SetValue(-5);
                Effects[Index].Parameters["radius"].SetValue(Intensity / 1000f);
                Effects[Index].Parameters["bg"].SetValue(BG);
                Effects[Index].CurrentTechnique.Passes[0].Apply();
            }
            if (Index == 2)
            {
                Effects[Index].Parameters["seed"].SetValue(Position);
                Effects[Index].Parameters["intensity"].SetValue(Intensity / 10f);
                Effects[Index].CurrentTechnique.Passes[0].Apply();
            }
            if (Index == 3)
            {
                Effects[Index].Parameters["pos"].SetValue(Position);
                Effects[Index].Parameters["intensity"].SetValue(-7.5f);
                Effects[Index].Parameters["radius"].SetValue(Intensity / 1000f);
                Effects[Index].Parameters["bg"].SetValue(BG);
                Effects[Index].CurrentTechnique.Passes[0].Apply();
            }
        }

        public void ActivateEffect(GameObject portObject, int newTime)
        {
            Index = 0;
            PortObject = portObject;
            Intensity = 20f;
            Value = 2;
            Speed = -0.5f;
            NewTime = newTime;
            Position = new Vector2(0.5f, 0.5f);
        }

        public void ActivateEffect(Vector2 position, int newTime)
        {
            Index = 1;
            Position = position;
            Intensity = 20f;
            Value = 2;
            Speed = -0.5f;
            NewTime = newTime;
        }

        public void ActivateEffect(int newTime)
        {
            if (newTime == -1 || NewTime == newTime)
            {
                Index = -1;
                PortObject = null;
                Position = Vector2.Zero;
                Speed = 0;
                Intensity = 20;
                Value = 2;
                NewTime = -1;
                stickmoved = false;
            }
            else
            {
                Index = 3;
                Position = Vector2.Zero;
                Intensity = 0f;
                Value = 50;
                Speed = 1f;
                NewTime = newTime;
            }
        }

        public void ActivateEffect(Vector2 newPos)
        {
            Index = 2;
            Position = newPos;
            Intensity = 0f;
            Value = 1;
            Speed = 0.03f;
        }


        public void SetBG(Texture2D bg)
        {
            BG = bg;
        }

        public void SetNewTime(int time)
        {
            NewTime = time;
        }
    }
}
