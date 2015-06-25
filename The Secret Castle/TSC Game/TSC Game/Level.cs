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
    [Serializable]
    public class Level
    {
        public string Name { get; set; }
        public List<GameObject> Past { get; set; }
        public List<GameObject> Present { get; set; }
        public List<GameObject> Future { get; set; }
        public List<Link> PastLinks { get; set; }
        public List<Link> PresentLinks { get; set; }
        public List<Link> FutureLinks { get; set; }
        public Texture2D PastBackground { get; set; }
        public Texture2D PresentBackground { get; set; }
        public Texture2D FutureBackground { get; set; }
        public string PastBgPath { get; set; }
        public string PresentBgPath { get; set; }
        public string FutureBgPath { get; set; }
        public Vector3 PlayerStart { get; set; }

        public Level()
        {

        }

        public Level(string name, string pastBackground, string presentBackground, string futureBackground, ContentManager content)
        {
            Name = name;
            PastBgPath = pastBackground;
            PresentBgPath = presentBackground;
            FutureBgPath = futureBackground;
            PastBackground = content.Load<Texture2D>(PastBgPath);
            PresentBackground = content.Load<Texture2D>(PresentBgPath);
            FutureBackground = content.Load<Texture2D>(FutureBgPath);
            Past = new List<GameObject>();
            Present = new List<GameObject>();
            Future = new List<GameObject>();
            PastLinks = new List<Link>();
            PresentLinks = new List<Link>();
            FutureLinks = new List<Link>();
            PlayerStart = new Vector3(50, 60, 0);
        }
    }
}
