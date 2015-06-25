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
    public class SoundManager
    {
        private List<SoundEffect> Sounds;
        private SoundEffectInstance Music;
        private SoundEffectInstance MusicLoop;
        public float Volume;
        public float MusicVolume;

        public SoundManager(float volume, float musicVolume, ContentManager content)
        {
            Volume = volume;
            MusicVolume = musicVolume;

            Sounds = new List<SoundEffect>();

            Sounds.Add(content.Load<SoundEffect>("Sounds/Schritte auf Stein"));
            Sounds.Add(content.Load<SoundEffect>("Sounds/Schritte Holz"));
            Sounds.Add(content.Load<SoundEffect>("Sounds/Schritte auf Metall"));
            Sounds.Add(content.Load<SoundEffect>("Sounds/Schalter Vergangenheit"));
            Sounds.Add(content.Load<SoundEffect>("Sounds/Schalter Gegenwart"));
            Sounds.Add(content.Load<SoundEffect>("Sounds/Schalter Zukunft"));
            Sounds.Add(content.Load<SoundEffect>("Sounds/Zeitschalter Vergangenheit"));
            Sounds.Add(content.Load<SoundEffect>("Sounds/Zeitschalter Gegenwart"));
            Sounds.Add(content.Load<SoundEffect>("Sounds/Zeitschalter Zukunft"));
            Sounds.Add(content.Load<SoundEffect>("Sounds/Tür Vergangenheit auf"));
            Sounds.Add(content.Load<SoundEffect>("Sounds/Tür Vergangenheit zu"));
            Sounds.Add(content.Load<SoundEffect>("Sounds/Tür Gegenwart auf"));
            Sounds.Add(content.Load<SoundEffect>("Sounds/Tür Gegenwart zu"));
            Sounds.Add(content.Load<SoundEffect>("Sounds/Tür Zukunft auf"));
            Sounds.Add(content.Load<SoundEffect>("Sounds/Tür Zukunft zu"));
            Sounds.Add(content.Load<SoundEffect>("Sounds/Laserbrücke an"));
            Sounds.Add(content.Load<SoundEffect>("Sounds/Laserbrücke aus"));
            Sounds.Add(content.Load<SoundEffect>("Sounds/teleport on"));
            Sounds.Add(content.Load<SoundEffect>("Sounds/teleport off"));
            Sounds.Add(content.Load<SoundEffect>("Sounds/teleport"));
            Sounds.Add(content.Load<SoundEffect>("Sounds/Zeitsprung"));
            Sounds.Add(content.Load<SoundEffect>("Sounds/winner"));
            Sounds.Add(content.Load<SoundEffect>("Sounds/looser"));
            Sounds.Add(content.Load<SoundEffect>("Sounds/Einsammeln Diamanten"));
            Sounds.Add(content.Load<SoundEffect>("Sounds/Einsammeln Schlüssel"));
            Sounds.Add(content.Load<SoundEffect>("Sounds/Endtür aktiviert"));
            Sounds.Add(content.Load<SoundEffect>("Sounds/Endtür durchgehen"));
            Sounds.Add(content.Load<SoundEffect>("Sounds/Menübutton"));
            Sounds.Add(content.Load<SoundEffect>("Sounds/Sammeln Münze_2"));

            SoundEffect tmp = content.Load<SoundEffect>("Sounds/Sound");
            Music = tmp.CreateInstance();
            tmp = content.Load<SoundEffect>("Sounds/loop");
            MusicLoop = tmp.CreateInstance();

            Music.Volume = MusicVolume;
            MusicLoop.Volume = MusicVolume;

            Music.Play();
        }

        public void Update(float vol)
        {
            if (Music.State == SoundState.Stopped)
            {
                MusicLoop.Play();
            }
            Music.Volume = vol;
            MusicLoop.Volume = vol;
        }

        public void PlaySound(string sound)
        {
            switch (sound)
            {
                case "StepsStone":
                    Sounds[0].Play(Volume, 0f, 0f);
                    break;
                case "StepsWood":
                    Sounds[1].Play(Volume, 0f, 0f);
                    break;
                case "StepsMetal":
                    Sounds[2].Play(Volume, 0f, 0f);
                    break;
                case "PastSwitch":
                    Sounds[3].Play(Volume, 0f, 0f);
                    break;
                case "PresentSwitch":
                    Sounds[4].Play(Volume, 0f, 0f);
                    break;
                case "FutureSwitch":
                    Sounds[5].Play(Volume, 0f, 0f);
                    break;
                case "PastTimeswitch":
                    Sounds[6].Play(Volume, 0f, 0f);
                    break;
                case "PresentTimeswitch":
                    Sounds[7].Play(Volume, 0f, 0f);
                    break;
                case "FutureTimeswitch":
                    Sounds[8].Play(Volume, 0f, 0f);
                    break;
                case "OpenPastDoor":
                    Sounds[9].Play(Volume, 0f, 0f);
                    break;
                case "ClosePastDoor":
                    Sounds[10].Play(Volume, 0f, 0f);
                    break;
                case "OpenPresentDoor":
                    Sounds[11].Play(Volume, 0f, 0f);
                    break;
                case "ClosePresentDoor":
                    Sounds[12].Play(Volume, 0f, 0f);
                    break;
                case "OpenFutureDoor":
                    Sounds[13].Play(Volume, 0f, 0f);
                    break;
                case "CloseFutureDoor":
                    Sounds[14].Play(Volume, 0f, 0f);
                    break;
                case "LaserbridgeOn":
                    Sounds[15].Play(Volume, 0f, 0f);
                    break;
                case "LaserbridgeOff":
                    Sounds[16].Play(Volume, 0f, 0f);
                    break;
                case "TeleporterOn":
                    Sounds[17].Play(Volume, 0f, 0f);
                    break;
                case "TeleporterOff":
                    Sounds[18].Play(Volume, 0f, 0f);
                    break;
                case "Teleport":
                    Sounds[19].Play(Volume, 0f, 0f);
                    break;
                case "Timeswitch":
                    Sounds[20].Play(Volume, 0f, 0f);
                    break;
                case "Win":
                    Sounds[21].Play(Volume, 0f, 0f);
                    break;
                case "Loose":
                    Sounds[22].Play(Volume, 0f, 0f);
                    break;
                case "ColD":
                    Sounds[23].Play(Volume, 0f, 0f);
                    break;
                case "ColK":
                    Sounds[24].Play(Volume, 0f, 0f);
                    break;
                case "EndDoorAct":
                    Sounds[25].Play(Volume, 0f, 0f);
                    break;
                case "EndDoorWalk":
                    Sounds[26].Play(Volume, 0f, 0f);
                    break;
                case "MenuB":
                    Sounds[27].Play(Volume, 0f, 0f);
                    break;
                case "ColC":
                    Sounds[28].Play(Volume, 0f, 0f);
                    break;
            }
        }

        
    }
}
