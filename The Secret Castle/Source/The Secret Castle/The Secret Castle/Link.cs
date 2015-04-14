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
    public class Link
    {
        public GameObject FirstObject { get; set; }
        public GameObject SecondObject { get; set; }
        public int LinkType { get; set; }
        public List<Vector2> DotPositions { get; set; }
        public bool MouseOver { get; set; }
        public int Direction { get; set; }

        public Link()
        {

        }

        public Link(GameObject firstObject, GameObject secondObject, int linkType)
        {
            FirstObject = firstObject;
            SecondObject = secondObject;
            LinkType = linkType;
            MouseOver = false;
            Direction = 0;

            DotPositions = new List<Vector2>();
            for (int i = 0; i < (int)Math.Sqrt(Math.Pow(SecondObject.Position.X - FirstObject.Position.X, 2) + Math.Pow(SecondObject.Position.Y - FirstObject.Position.Y, 2)); i++)
            {
                double rot = Math.Atan2(SecondObject.Position.Y - FirstObject.Position.Y, SecondObject.Position.X - FirstObject.Position.X);
                Vector2 p = new Vector2(FirstObject.Position.X + (float)Math.Cos(rot) * i, FirstObject.Position.Y + (float)Math.Sin(rot) * i);
                DotPositions.Add(p);
            }
        }

        public void Update(Camera cam)
        {
            MouseState mouseState = Mouse.GetState();

            DotPositions = new List<Vector2>();
            for (int i = 0; i < (int)Math.Sqrt(Math.Pow(SecondObject.Position.X - FirstObject.Position.X, 2) + Math.Pow(SecondObject.Position.Y - FirstObject.Position.Y, 2)); i++)
            {
                double rot = Math.Atan2(SecondObject.Position.Y - FirstObject.Position.Y, SecondObject.Position.X - FirstObject.Position.X);
                Vector2 p = new Vector2(FirstObject.Position.X + (float)Math.Cos(rot) * i, FirstObject.Position.Y + (float)Math.Sin(rot) * i);
                DotPositions.Add(p);
                if (i % 20 == 0 && Direction != 0)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        double rott = rot + MathHelper.ToRadians(-135);
                        Vector2 pp = new Vector2(p.X + (float)Math.Cos(rott) * j, p.Y + (float)Math.Sin(rott) * j);
                        DotPositions.Add(pp);
                    }
                    for (int j = 0; j < 10; j++)
                    {
                        double rott = rot + MathHelper.ToRadians(135);
                        Vector2 pp = new Vector2(p.X + (float)Math.Cos(rott) * j, p.Y + (float)Math.Sin(rott) * j);
                        DotPositions.Add(pp);
                    }
                }
            }

            foreach (Vector2 v in DotPositions)
            {
                if (MathHelper.Distance(v.X, mouseState.X - cam.Position.X) <= 5 & MathHelper.Distance(v.Y, mouseState.Y - cam.Position.Y) <= 5)
                {
                    MouseOver = true;
                    break;
                }
                else
                {
                    MouseOver = false;
                }
            }
        }
    }
}
