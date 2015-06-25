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
    [Serializable]
    public class Settings
    {
        public bool FullScreen;
        public Vector2 Resolution;
        public float Volume;
        public int ControlType;

        public Settings()
        {

        }
    }
}
