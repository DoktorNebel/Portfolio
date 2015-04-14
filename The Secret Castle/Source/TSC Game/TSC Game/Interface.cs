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
    public class Interface
    {
        private Vector2 CoinPos;
        private Vector2 SwordPos;
        private Vector2 DiamondsPos;
        private Vector2 TimelinePos;
        private Texture2D Cursor;
        private Texture2D CursorNormal;
        private Texture2D CursorPort;
        private Texture2D CursorFalse;
        private Texture2D PastIcon;
        private Texture2D PresentIcon;
        private Texture2D FutureIcon;
        private int TargetTime;
        private List<Texture2D> CoinSprites;
        private Texture2D FullSword;
        private Texture2D FullAllDs;
        private List<Texture2D> DSprites;
        private Texture2D TimelineHandle;
        private Texture2D TimelineLine;
        private Texture2D TimelineSpitze;
        private Effect UIShader;
        private float Scale;
        private bool Animation;
        private int AnimPhase;
        private int AnimTimer;
        public int Coins { get; set; }
        public int StartCharges { get; set; }
        public int Charges { get; set; }
        public bool[] StartDiamonds { get; set; }
        public bool[] Diamonds { get; set; }
        public int SD { get; set; }
        public int D { get; set; }
        public int StartTime { get; set; }
        public int Time { get; set; }
        private int Milliseconds;
        public bool fuckcollected;
        private Texture2D firstkey;
        private Texture2D secondkey;
        private Texture2D thirdkey;

        public Interface(GraphicsDevice graphicsDevice, int charges, bool[] diamonds, int time, ContentManager content)
        {
            Scale = (float)graphicsDevice.Viewport.Height / 1024f;
            CoinPos = new Vector2(80, 50);
            SwordPos = new Vector2(graphicsDevice.Viewport.Width / 2 - 512 * Scale, graphicsDevice.Viewport.Height - 150 * Scale);
            DiamondsPos = new Vector2(graphicsDevice.Viewport.Width - 150 * Scale, graphicsDevice.Viewport.Height - 200 * Scale);
            TimelinePos = new Vector2(0, 0);

            CursorNormal = content.Load<Texture2D>("Sprites/UI/Mauszeiger_klein_ohne");
            Cursor = CursorNormal;
            CursorPort = content.Load<Texture2D>("Sprites/UI/Mauszeiger_klein");
            CursorFalse = content.Load<Texture2D>("Sprites/UI/Mauszeiger_klein_falsch");
            PastIcon = content.Load<Texture2D>("Sprites/UI/PastIcon");
            PresentIcon = content.Load<Texture2D>("Sprites/UI/PresentIcon");
            FutureIcon = content.Load<Texture2D>("Sprites/UI/FutureIcon");

            firstkey = content.Load<Texture2D>("Sprites/Objects/GameObjects/schluessel 1");
            secondkey = content.Load<Texture2D>("Sprites/Objects/GameObjects/schluessel 2");
            thirdkey = content.Load<Texture2D>("Sprites/Objects/GameObjects/schluessel 3");


            CoinSprites = new List<Texture2D>();
            CoinSprites.Add(content.Load<Texture2D>("Sprites/UI/UI_muenzen_1_NA"));
            CoinSprites.Add(content.Load<Texture2D>("Sprites/UI/UI_Muenze_Drehung0"));
            CoinSprites.Add(content.Load<Texture2D>("Sprites/UI/UI_muenzen_1_NA"));
            CoinSprites.Add(content.Load<Texture2D>("Sprites/UI/UI_Muenze_Drehung1"));
            CoinSprites.Add(content.Load<Texture2D>("Sprites/UI/UI_muenzen_1_NA"));
            CoinSprites.Add(content.Load<Texture2D>("Sprites/UI/UI_Muenze_Drehung2"));
            CoinSprites.Add(content.Load<Texture2D>("Sprites/UI/UI_muenzen_1_NA"));
            CoinSprites.Add(content.Load<Texture2D>("Sprites/UI/UI_Muenze_Drehung1"));
            CoinSprites.Add(content.Load<Texture2D>("Sprites/UI/UI_muenzen_1_NA"));
            CoinSprites.Add(content.Load<Texture2D>("Sprites/UI/UI_Muenze_Drehung0"));
            CoinSprites.Add(content.Load<Texture2D>("Sprites/UI/UI_muenzen_1_NA"));
            CoinSprites.Add(content.Load<Texture2D>("Sprites/UI/UI_Muenze_Drehung1"));
            CoinSprites.Add(content.Load<Texture2D>("Sprites/UI/UI_muenzen_1_NA"));
            CoinSprites.Add(content.Load<Texture2D>("Sprites/UI/UI_Muenze_Drehung2"));
            CoinSprites.Add(content.Load<Texture2D>("Sprites/UI/UI_muenzen_1_NA"));
            CoinSprites.Add(content.Load<Texture2D>("Sprites/UI/UI_Muenze_Drehung1"));
            CoinSprites.Add(content.Load<Texture2D>("Sprites/UI/UI_muenzen_1_NA"));
            CoinSprites.Add(content.Load<Texture2D>("Sprites/UI/UI_Muenze_Drehung0"));

            FullSword = content.Load<Texture2D>("Sprites/UI/gui1");
            FullAllDs = content.Load<Texture2D>("Sprites/UI/gui9");
            DSprites = new List<Texture2D>();
            DSprites.Add(content.Load<Texture2D>("Sprites/UI/gui2"));
            DSprites.Add(content.Load<Texture2D>("Sprites/UI/gui3"));
            DSprites.Add(content.Load<Texture2D>("Sprites/UI/gui4"));
            DSprites.Add(content.Load<Texture2D>("Sprites/UI/gui5"));
            DSprites.Add(content.Load<Texture2D>("Sprites/UI/gui6"));
            DSprites.Add(content.Load<Texture2D>("Sprites/UI/gui7"));
            DSprites.Add(content.Load<Texture2D>("Sprites/UI/gui8"));
            TimelineHandle = content.Load<Texture2D>("Sprites/UI/Griff");
            TimelineLine = content.Load<Texture2D>("Sprites/UI/Zeitleiste");
            TimelineSpitze = content.Load<Texture2D>("Sprites/UI/spitze");

            Animation = false;
            AnimPhase = 0;
            AnimTimer = 0;

            UIShader = content.Load<Effect>("Shaders/UIShader");

            Coins = 0;
            StartCharges = charges;
            Charges = charges;
            StartDiamonds = diamonds;
            Diamonds = diamonds;
            StartTime = time;
            Time = time;
            Milliseconds = 0;

            fuckcollected = false;

            SD = 0;
            D = 0;
            foreach (bool d in StartDiamonds)
            {
                if (d)
                {
                    SD++;
                    D++;
                }
            }
        }

        public void Update(GameTime gameTime)
        {
            if (Time > 0)
            {
                Milliseconds += gameTime.ElapsedGameTime.Milliseconds;
                if (Milliseconds >= 1000)
                {
                    Milliseconds -= 1000;
                    Time--;
                }
            }

            if (Animation)
            {
                AnimTimer += gameTime.ElapsedGameTime.Milliseconds;

                if (AnimTimer > 50)
                {
                    AnimTimer = 0;
                    AnimPhase += 2;
                }

                if (AnimPhase >= CoinSprites.Count)
                {
                    AnimTimer = 0;
                    Animation = false;
                    AnimPhase = 0;
                }
            }
        }

        public void AddCoin()
        {
            Coins++;
            if (Coins % 10 == 0)
            {
                Animation = true;
            }
        }

        public void AddDiamond(int i)
        {
            Diamonds[i] = true;
            fuckcollected = true;
        }

        public void RemoveCharge()
        {
            bool bla = false;
            for (int i = 3; i >= 0; i--)
            {
                if (Diamonds[i])
                {
                    Diamonds[i] = false;
                    D--;
                    bla = true;
                    break;
                }
            }
            if (!bla)
            {
                Charges--;
            }
        }

        public bool OutOfCharges()
        {
            if (Charges == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool TimeIsUp()
        {
            if (Time <= 0 && Time > -10)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Portable(int port)
        {
            if (port == 0)
            {
                Cursor = CursorNormal;
            }
            else if (port == 1)
            {
                Cursor = CursorPort;
            }
            else
            {
                Cursor = CursorFalse;
            }
        }

        public void SetTargetTime(int time)
        {
            TargetTime = time;
        }

        public void Draw(SpriteBatch spriteBatch, GameCharacter player, int controlType, GameCamera cam, GraphicsDevice graphics)
        {
            MouseState mouseState = Mouse.GetState();
            Vector2 playerPos = player.Position;

            if (StartCharges > -1)
            {
                if (SD > 0)
                {
                    if (SD == 4)
                    {
                        if (D == 4)
                        {
                            spriteBatch.Begin();
                            spriteBatch.Draw(DSprites[3], SwordPos, null, Color.White, 0f, Vector2.Zero, Scale, SpriteEffects.None, 0f);
                            spriteBatch.End();
                        }
                        else if (D == 3)
                        {
                            spriteBatch.Begin();
                            spriteBatch.Draw(DSprites[4], SwordPos, null, Color.White, 0f, Vector2.Zero, Scale, SpriteEffects.None, 0f);
                            spriteBatch.End();
                        }
                        else if (D == 2)
                        {
                            spriteBatch.Begin();
                            spriteBatch.Draw(DSprites[5], SwordPos, null, Color.White, 0f, Vector2.Zero, Scale, SpriteEffects.None, 0f);
                            spriteBatch.End();
                        }
                        else if (D == 1)
                        {
                            spriteBatch.Begin();
                            spriteBatch.Draw(DSprites[6], SwordPos, null, Color.White, 0f, Vector2.Zero, Scale, SpriteEffects.None, 0f);
                            spriteBatch.End();
                        }
                        else
                        {
                            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
                            UIShader.Parameters["vertical"].SetValue(true);
                            UIShader.Parameters["value"].SetValue(Charges);
                            UIShader.Parameters["maxValue"].SetValue(StartCharges);
                            UIShader.Parameters["lines"].SetValue(true);
                            UIShader.Parameters["start"].SetValue(0);
                            UIShader.Parameters["end"].SetValue(64);
                            UIShader.Parameters["up"].SetValue(0);
                            UIShader.Parameters["low"].SetValue(64);
                            UIShader.CurrentTechnique.Passes[0].Apply();
                            spriteBatch.Draw(FullAllDs, SwordPos, null, Color.White, 0f, Vector2.Zero, Scale, SpriteEffects.None, 0f);
                            spriteBatch.End();
                        }
                    }
                    else
                    {
                        if (D == 3)
                        {
                            spriteBatch.Begin();
                            spriteBatch.Draw(DSprites[2], SwordPos, null, Color.White, 0f, Vector2.Zero, Scale, SpriteEffects.None, 0f);
                            spriteBatch.End();
                        }
                        else if (D == 2)
                        {
                            spriteBatch.Begin();
                            spriteBatch.Draw(DSprites[1], SwordPos, null, Color.White, 0f, Vector2.Zero, Scale, SpriteEffects.None, 0f);
                            spriteBatch.End();
                        }
                        else if (D == 1)
                        {
                            spriteBatch.Begin();
                            spriteBatch.Draw(DSprites[0], SwordPos, null, Color.White, 0f, Vector2.Zero, Scale, SpriteEffects.None, 0f);
                            spriteBatch.End();
                        }
                        else
                        {
                            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
                            UIShader.Parameters["vertical"].SetValue(true);
                            UIShader.Parameters["value"].SetValue(Charges);
                            UIShader.Parameters["maxValue"].SetValue(StartCharges);
                            UIShader.Parameters["lines"].SetValue(true);
                            UIShader.Parameters["start"].SetValue(0);
                            UIShader.Parameters["end"].SetValue(64);
                            UIShader.Parameters["up"].SetValue(0);
                            UIShader.Parameters["low"].SetValue(64);
                            UIShader.CurrentTechnique.Passes[0].Apply();
                            spriteBatch.Draw(FullSword, SwordPos, null, Color.White, 0f, Vector2.Zero, Scale, SpriteEffects.None, 0f);
                            spriteBatch.End();
                        }
                    }
                }
                else
                {
                    spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
                    UIShader.Parameters["vertical"].SetValue(true);
                    UIShader.Parameters["value"].SetValue(Charges);
                    UIShader.Parameters["maxValue"].SetValue(StartCharges);
                    UIShader.Parameters["lines"].SetValue(true);
                    UIShader.Parameters["start"].SetValue(0);
                    UIShader.Parameters["end"].SetValue(64);
                    UIShader.Parameters["up"].SetValue(0);
                    UIShader.Parameters["low"].SetValue(64);
                    UIShader.CurrentTechnique.Passes[0].Apply();
                    spriteBatch.Draw(FullSword, SwordPos, null, Color.White, 0f, Vector2.Zero, Scale, SpriteEffects.None, 0f);
                    spriteBatch.End();
                }
            }

            if (Time >= 0)
            {
                spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
                spriteBatch.Draw(TimelineHandle, TimelinePos, null, Color.White, 0f, Vector2.Zero, Scale, SpriteEffects.None, 0f);
                spriteBatch.Draw(TimelineSpitze, TimelinePos - new Vector2((1 - ((float)Time / (float)StartTime)) * Scale * 796 + 5, 0), null, Color.White, 0f, Vector2.Zero, Scale, SpriteEffects.None, 0f);
                UIShader.Parameters["vertical"].SetValue(false);
                UIShader.Parameters["value"].SetValue(Time);
                UIShader.Parameters["maxValue"].SetValue(StartTime);
                UIShader.Parameters["lines"].SetValue(false);
                UIShader.Parameters["start"].SetValue(178);
                UIShader.Parameters["end"].SetValue(974);
                UIShader.Parameters["up"].SetValue(0);
                UIShader.Parameters["low"].SetValue(128);
                UIShader.CurrentTechnique.Passes[0].Apply();
                spriteBatch.Draw(TimelineLine, TimelinePos, null, Color.White, 0f, Vector2.Zero, Scale, SpriteEffects.None, 0f);
                spriteBatch.End();
            }


            spriteBatch.Begin();
            Vector2 pos = Vector2.Zero;
            if (controlType == 0)
            {
                spriteBatch.Draw(Cursor, new Vector2(mouseState.X, mouseState.Y), null, Color.White, MathHelper.ToRadians(-45), new Vector2(16, 0), 1f, SpriteEffects.None, 0f);
                pos = new Vector2(mouseState.X + 32, mouseState.Y);
            }
            else
            {
                float mult = 1f;
                if (player.FacingRight)
                {
                    mult = -1.5f;
                }
                pos = playerPos + new Vector2(32 * mult, 16) + cam.Position;
            }

            if (player.CanPast)
            {
                if (TargetTime == 0)
                {
                    spriteBatch.Draw(PastIcon, pos, Color.White);
                }
                else if (TargetTime == 1)
                {
                    spriteBatch.Draw(PresentIcon, pos, Color.White);
                }
                else if (TargetTime == 2)
                {
                    spriteBatch.Draw(FutureIcon, pos, Color.White);
                }
            }

            for (int i = 0; i < 3; i++)
            {
                Texture2D kolben = firstkey;
                if (i == 1)
                {
                    kolben = secondkey;
                }
                if (i == 2)
                {
                    kolben = thirdkey;
                }

                if (player.Keyparts[i])
                {
                    spriteBatch.Draw(kolben, new Vector2(graphics.Viewport.Width - (3 - i) * 64 - 10, 10), Color.White);
                }
            }
            spriteBatch.End();
        }

        public void Reset()
        {
            Coins = 0;
            Charges = StartCharges;
            Diamonds = StartDiamonds;
            Time = StartTime;
            D = SD;
            fuckcollected = false;
        }
    }
}
