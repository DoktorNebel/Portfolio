using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Storage;
using The_Secret_Castle;
using System.Diagnostics;


namespace TSC_Game
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class MainGame : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        string GameState;

        Save Savegame;
        Settings Options;

        Level CurrentLevel;
        GameCharacter Player;
        GameCamera Cam;
        Interface UI;
        bool Finished;
        bool Restart;

        List<string[]> LevelNames;
        int EasyCount;
        int MediumCount;
        int HardCount;
        List<List<Vector2>> LevelOffsets;
        int ChosenLevel;
        bool KeyPressed;
        Vector2 LevelScreenScroll;
        int Trigger;
        bool TriggerActivated;
        int TriggerActivation;
        int TriggerTimer;
        int TriggerPhase;
        List<Vector2> Coins;
        int CoinTimer;
        int CoinAnim;
        int CoinAnimPhase;
        int CoinToAnim;
        int OverallCoins;
        int CollectedCoins;
        int Difficulty;
        int ScrollAnim;
        int ScrollDestination;
        int AndereDifficulty;
        float Fade;
        int FadeTimer;
        int LoadVar;
        int maxLoad;
        bool Loaded;
        bool ga;
        bool it;
        float fadeSpeed;

        List<GameButton> MainMenuButtons;
        List<List<GameButton>> LevelScreenButtons;
        List<GameButton> OptionsMenuButtons;
        List<GameButton> GraphicsMenuButtons;
        List<GameButton> SoundMenuButtons;
        List<GameButton> ControlsMenuButtons;
        List<GameButton> PauseMenuButtons;
        List<GameButton> LevelEndScreenButtons;

        List<List<Texture2D>> TriggerPics;
        List<List<Vector3>> TriggerProps;

        Texture2D GALogo;
        Texture2D InTeamLogo;

        Texture2D SwordNormal;
        Texture2D SwordPortable;

        Texture2D Cursor;

        Texture2D StartButton;
        Texture2D StartButtonMO;
        Texture2D StartButtonP;
        Texture2D LVButton;
        Texture2D LVButtonMO;
        Texture2D LVButtonP;
        Texture2D NewButton;
        Texture2D NewButtonMO;
        Texture2D NewButtonP;
        Texture2D OptionsButton;
        Texture2D OptionsButtonMO;
        Texture2D OptionsButtonP;
        Texture2D ExitButton;
        Texture2D ExitButtonMO;
        Texture2D ExitButtonP;
        Texture2D BackButton;
        Texture2D BackButtonMO;
        Texture2D BackButtonP;
        Texture2D ConfirmButton;
        Texture2D ConfirmButtonMO;
        Texture2D ConfirmButtonP;
        Texture2D LevelScreenButton;
        Texture2D LevelScreenButtonMO;
        Texture2D LevelScreenButtonP;
        Texture2D EasyButton;
        Texture2D MediumButton;
        Texture2D HardButton;
        Texture2D EasyButtonGold;
        Texture2D MediumButtonGold;
        Texture2D HardButtonGold;
        Texture2D PauseExitButton;
        Texture2D PauseExitButtonMO;
        Texture2D PauseExitButtonP;
        Texture2D ReplayButton;
        Texture2D ReplayButtonMO;
        Texture2D ReplayButtonP;
        Texture2D NextButton;
        Texture2D NextButtonMO;
        Texture2D NextButtonP;
        Texture2D PreviousButton;
        Texture2D PreviousButtonMO;
        Texture2D PreviousButtonP;

        Texture2D Background;
        Texture2D Chest;
        Texture2D Shield;
        Texture2D Coin;
        Texture2D SmallCoinEmpty;
        List<Texture2D> SmallCoins;
        Texture2D DEmpty;
        Texture2D DFull;
        Texture2D KeyboardPic;
        Texture2D MousePic;
        Texture2D ControllerPic;

        Texture2D FullScreenButton;
        Texture2D WindowedButton;

        Texture2D SoundSlider;
        Texture2D SoundDot;
        Texture2D SoundDotMO;
        Texture2D SoundDotP;

        Texture2D ControlsButton;
        Texture2D ControlsButtonMO;
        Texture2D ControlsButtonP;

        Texture2D CastleTop;
        Texture2D CastleElement;
        Texture2D LevelEndScreen;
        Texture2D Smiley;

        Texture2D Dot;

        List<List<Texture2D>> ControlsAnimations;
        List<Vector2> ControlsTimers;

        List<List<Texture2D>> LevelPics;
        List<List<int>> maxCoins;

        Effect WindowEffect;

        RenderTarget2D renderTarget;
        RenderTarget2D bgRender;
        RenderTarget2D windowRender;
        RenderTarget2D wallRender;
        EffectManager FXManager;
        SoundManager SoundManager;

        List<Texture2D> CSPics;
        Vector2 CSPosFuck;
        int CSAT;
        int CSAP;
        int CSPhase;

        int krueppelcounterj;
        int krueppelcounterk;

        public MainGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            GameState = "Intro";

            Player = null;
            Cam = new GameCamera();
            UI = null;
            Trigger = -1;
            TriggerActivated = false;
            TriggerActivation = 0;
            TriggerTimer = 0;
            TriggerPhase = 0;
            CoinTimer = 0;
            CoinAnim = 0;
            CoinAnimPhase = 0;
            CoinToAnim = -1;
            OverallCoins = 0;
            AndereDifficulty = 0;
            Restart = false;
            Fade = 1f;
            LoadVar = -1;
            maxLoad = 0;
            krueppelcounterj = 0;
            krueppelcounterk = 0;
            Loaded = false;
            FadeTimer = -1;
            ga = true;
            it = false;
            fadeSpeed = 0.025f;


            Difficulty = 0;
            ScrollAnim = 0;
            ScrollDestination = 0;

            LevelNames = new List<string[]>();
            LevelNames.Add(Directory.GetFiles(Environment.CurrentDirectory + "/Levels/AllPlayers/Leicht"));
            LevelNames.Add(Directory.GetFiles(Environment.CurrentDirectory + "/Levels/AllPlayers/Mittel"));
            LevelNames.Add(Directory.GetFiles(Environment.CurrentDirectory + "/Levels/AllPlayers/Schwer"));
            Array.Sort(LevelNames[0]);
            Array.Sort(LevelNames[1]);
            Array.Sort(LevelNames[2]);

            maxLoad = LevelNames[0].Length;
            if (LevelNames[1].Length > maxLoad)
            {
                maxLoad = LevelNames[1].Length;
            }
            if (LevelNames[2].Length > maxLoad)
            {
                maxLoad = LevelNames[2].Length;
            }

            Savegame = LoadGame();
            if (Savegame == null)
            {
                Savegame = new Save();
                Savegame.Unlocked = new List<int>();
                Savegame.Unlocked.Add(1);
                Savegame.Unlocked.Add(0);
                Savegame.Unlocked.Add(0);
                Savegame.Played = new List<List<bool[]>>();
                Savegame.Played.Add(new List<bool[]>());
                Savegame.Played.Add(new List<bool[]>());
                Savegame.Played.Add(new List<bool[]>());
                Savegame.Coins = new List<int[]>();
                for (int i = 0; i < LevelNames.Count; i++)
                {
                    foreach (string suppe in LevelNames[i])
                    {
                        Savegame.Played[i].Add(new bool[3]);
                    }
                    Savegame.Coins.Add(new int[LevelNames[i].Length]);
                }
                Savegame.Diamonds = new bool[4];
                SaveGame();
            }

            CollectedCoins = 0;
            for (int i = 0; i < Savegame.Coins.Count; i++)
            {
                for (int j = 0; j < Savegame.Coins[i].Length; j++)
                {
                    CollectedCoins += Savegame.Coins[i][j];
                }
            }


            Options = LoadOptions();
            if (Options == null)
            {
                Options = new Settings();
                Options.FullScreen = true;
                Options.Resolution = new Vector2(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width,
                                                 GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height);
                Options.Volume = 1f;
                Options.ControlType = 0;
                SaveOptions();
            }
            graphics.PreferredBackBufferWidth = (int)Options.Resolution.X;
            graphics.PreferredBackBufferHeight = (int)Options.Resolution.Y;
            graphics.IsFullScreen = Options.FullScreen;
            Window.AllowUserResizing = !graphics.IsFullScreen;


            TriggerPics = new List<List<Texture2D>>();
            TriggerProps = new List<List<Vector3>>();
            maxCoins = new List<List<int>>();
            SmallCoins = new List<Texture2D>();

            Coins = new List<Vector2>();

            ControlsAnimations = new List<List<Texture2D>>();

            LevelPics = new List<List<Texture2D>>();

            LevelOffsets = new List<List<Vector2>>();
            ChosenLevel = -1;
            CurrentLevel = null;
            KeyPressed = false;

            MainMenuButtons = new List<GameButton>();
            LevelScreenButtons = new List<List<GameButton>>();
            OptionsMenuButtons = new List<GameButton>();
            GraphicsMenuButtons = new List<GameButton>();
            SoundMenuButtons = new List<GameButton>();
            ControlsMenuButtons = new List<GameButton>();
            PauseMenuButtons = new List<GameButton>();
            LevelEndScreenButtons = new List<GameButton>();

            CSPics = new List<Texture2D>();
            CSPosFuck = new Vector2(770, 1560);
            CSPhase = 0;
            CSAT = 0;
            CSAP = 0;

        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);


            LevelScreenScroll = Vector2.Zero;

            GALogo = Content.Load<Texture2D>("Sprites/Menus/GALogo");
            InTeamLogo = Content.Load<Texture2D>("Sprites/Menus/Team-Logo");

        }





        //LOASFtgrs<e<e<e<e<e<e<e<e<e<e<e<e<e<e<e
        public bool LoadStuff()
        {
            if (LoadVar == -1)
            {
                SwordNormal = Content.Load<Texture2D>("Sprites/UI/Mauszeiger_klein_ohne");
                SwordPortable = Content.Load<Texture2D>("Sprites/UI/Mauszeiger_klein");


                Cursor = new Texture2D(graphics.GraphicsDevice, 1, 1);
                Color[] pixel = new Color[1];
                pixel[0] = Color.Black;
                Cursor.SetData<Color>(pixel);
                
                StartButton = Content.Load<Texture2D>("Sprites/Menus/StartButton");
                StartButtonMO = Content.Load<Texture2D>("Sprites/Menus/StartButtonMO");
                StartButtonP = Content.Load<Texture2D>("Sprites/Menus/StartButtonP");
                LVButton = Content.Load<Texture2D>("Sprites/Menus/LvAuswahl");
                LVButtonMO = Content.Load<Texture2D>("Sprites/Menus/LvAuswahlMO");
                LVButtonP = Content.Load<Texture2D>("Sprites/Menus/LvAuswahlP");
                NewButton = Content.Load<Texture2D>("Sprites/Menus/NewGame");
                NewButtonMO = Content.Load<Texture2D>("Sprites/Menus/NewGameMO");
                NewButtonP = Content.Load<Texture2D>("Sprites/Menus/NewGameP");
                OptionsButton = Content.Load<Texture2D>("Sprites/Menus/OptionsButton");
                OptionsButtonMO = Content.Load<Texture2D>("Sprites/Menus/OptionsButtonMO");
                OptionsButtonP = Content.Load<Texture2D>("Sprites/Menus/OptionsButtonP");
                ExitButton = Content.Load<Texture2D>("Sprites/Menus/ExitButton");
                ExitButtonMO = Content.Load<Texture2D>("Sprites/Menus/ExitButtonMO");
                ExitButtonP = Content.Load<Texture2D>("Sprites/Menus/ExitButtonP");
                BackButton = Content.Load<Texture2D>("Sprites/Menus/BackButton");
                BackButtonMO = Content.Load<Texture2D>("Sprites/Menus/BackButtonMO");
                BackButtonP = Content.Load<Texture2D>("Sprites/Menus/BackButtonP");
                ConfirmButton = Content.Load<Texture2D>("Sprites/Menus/ConfirmButton");
                ConfirmButtonMO = Content.Load<Texture2D>("Sprites/Menus/ConfirmButtonMO");
                ConfirmButtonP = Content.Load<Texture2D>("Sprites/Menus/ConfirmButtonP");
                LevelScreenButton = Content.Load<Texture2D>("Sprites/Menus/3");
                LevelScreenButtonMO = Content.Load<Texture2D>("Sprites/Menus/3");
                LevelScreenButtonP = Content.Load<Texture2D>("Sprites/Menus/3");
                EasyButton = Content.Load<Texture2D>("Sprites/Menus/Easy");
                EasyButtonGold = Content.Load<Texture2D>("Sprites/Menus/EasyGold");
                MediumButton = Content.Load<Texture2D>("Sprites/Menus/Medium");
                MediumButtonGold = Content.Load<Texture2D>("Sprites/Menus/MediumGold");
                HardButton = Content.Load<Texture2D>("Sprites/Menus/Hard");
                HardButtonGold = Content.Load<Texture2D>("Sprites/Menus/HardGold");
                PauseExitButton = Content.Load<Texture2D>("Sprites/Menus/PauseExitButton");
                PauseExitButtonMO = Content.Load<Texture2D>("Sprites/Menus/PauseExitButtonMO");
                PauseExitButtonP = Content.Load<Texture2D>("Sprites/Menus/PauseExitButtonP");
                ReplayButton = Content.Load<Texture2D>("Sprites/Menus/replay");
                ReplayButtonMO = Content.Load<Texture2D>("Sprites/Menus/replayMO");
                ReplayButtonP = Content.Load<Texture2D>("Sprites/Menus/replayP");
                NextButton = Content.Load<Texture2D>("Sprites/Menus/ButtonR");
                NextButtonMO = Content.Load<Texture2D>("Sprites/Menus/ButtonR_MO");
                NextButtonP = Content.Load<Texture2D>("Sprites/Menus/ButtonR_P");
                PreviousButton = Content.Load<Texture2D>("Sprites/Menus/ButtonL");
                PreviousButtonMO = Content.Load<Texture2D>("Sprites/Menus/ButtonL_MO");
                PreviousButtonP = Content.Load<Texture2D>("Sprites/Menus/ButtonL_P");


                Background = Content.Load<Texture2D>("Sprites/Backgrounds/Past/Background");
                Chest = Content.Load<Texture2D>("Sprites/Menus/Truhe");
                Shield = Content.Load<Texture2D>("Sprites/Menus/Logo");
                Coin = Content.Load<Texture2D>("Sprites/UI/UI_Muenze_Drehung0");
                SmallCoinEmpty = Content.Load<Texture2D>("Sprites/Menus/MuenzeKleinLeer");
                SmallCoins.Add(Content.Load<Texture2D>("Sprites/Menus/MuenzeOhneLicht"));
                SmallCoins.Add(Content.Load<Texture2D>("Sprites/Menus/MuenzeKleinDrehung1"));
                SmallCoins.Add(Content.Load<Texture2D>("Sprites/Menus/MuenzeKleinDrehung2"));
                SmallCoins.Add(Content.Load<Texture2D>("Sprites/Menus/MuenzeKleinDrehung1"));
                DFull = Content.Load<Texture2D>("Sprites/UI/UI Diamanten_2_einzeln");
                DEmpty = Content.Load<Texture2D>("Sprites/UI/Diamant");
                KeyboardPic = Content.Load<Texture2D>("Sprites/Menus/Tastatur");
                MousePic = Content.Load<Texture2D>("Sprites/Menus/Maus");
                ControllerPic = Content.Load<Texture2D>("Sprites/Menus/Controller");


                FullScreenButton = Content.Load<Texture2D>("Sprites/Menus/FullScreenButton");
                WindowedButton = Content.Load<Texture2D>("Sprites/Menus/WindowedButton");

                SoundSlider = Content.Load<Texture2D>("Sprites/Menus/SoundSlider");
                SoundDot = Content.Load<Texture2D>("Sprites/Menus/SoundDot");
                SoundDotMO = Content.Load<Texture2D>("Sprites/Menus/SoundDot");
                SoundDotP = Content.Load<Texture2D>("Sprites/Menus/SoundDot");

                ControlsButton = Content.Load<Texture2D>("Sprites/Menus/ControlsButton");
                ControlsButtonMO = Content.Load<Texture2D>("Sprites/Menus/ControlsButtonMO");
                ControlsButtonP = Content.Load<Texture2D>("Sprites/Menus/ControlsButtonP");

                CastleElement = Content.Load<Texture2D>("Sprites/Menus/1");
                CastleTop = Content.Load<Texture2D>("Sprites/Menus/2");
                LevelEndScreen = Content.Load<Texture2D>("Sprites/Menus/LevelEndScreen");
                Smiley = Content.Load<Texture2D>("Sprites/Menus/Smiley");

                Dot = Content.Load<Texture2D>("Sprites/Menus/Dot");

                WindowEffect = Content.Load<Effect>("Shaders/WindowEffect");


                TriggerPics.Add(new List<Texture2D>());
                TriggerPics.Add(new List<Texture2D>());
                TriggerPics.Add(new List<Texture2D>());
                TriggerPics.Add(new List<Texture2D>());
                TriggerPics.Add(new List<Texture2D>());
                TriggerPics.Add(new List<Texture2D>());
                TriggerPics.Add(new List<Texture2D>());
                TriggerPics.Add(new List<Texture2D>());
                TriggerPics.Add(new List<Texture2D>());
                TriggerPics.Add(new List<Texture2D>());
                TriggerPics.Add(new List<Texture2D>());
                TriggerPics.Add(new List<Texture2D>());
                TriggerPics[0].Add(Content.Load<Texture2D>("Sprites/Trigger/Pfeil"));
                TriggerPics[0].Add(Content.Load<Texture2D>("Sprites/Trigger/e"));
                TriggerPics[1].Add(Content.Load<Texture2D>("Sprites/Trigger/1"));
                TriggerPics[2].Add(Content.Load<Texture2D>("Sprites/Trigger/Pfeil"));
                TriggerPics[2].Add(Content.Load<Texture2D>("Sprites/Trigger/Maus1"));
                TriggerPics[3].Add(Content.Load<Texture2D>("Sprites/Trigger/2"));
                TriggerPics[4].Add(Content.Load<Texture2D>("Sprites/Trigger/Pfeil"));
                TriggerPics[4].Add(Content.Load<Texture2D>("Sprites/Trigger/Maus1"));
                TriggerPics[5].Add(Content.Load<Texture2D>("Sprites/Trigger/Maus2"));
                TriggerPics[6].Add(Content.Load<Texture2D>("Sprites/Trigger/2"));
                TriggerPics[7].Add(Content.Load<Texture2D>("Sprites/Trigger/Pfeil"));
                TriggerPics[7].Add(Content.Load<Texture2D>("Sprites/Trigger/Maus1"));
                TriggerPics[8].Add(Content.Load<Texture2D>("Sprites/Trigger/3"));
                TriggerPics[9].Add(Content.Load<Texture2D>("Sprites/Trigger/Pfeil"));
                TriggerPics[9].Add(Content.Load<Texture2D>("Sprites/Trigger/Maus1"));
                TriggerPics[10].Add(Content.Load<Texture2D>("Sprites/Trigger/3"));
                TriggerPics[11].Add(Content.Load<Texture2D>("Sprites/Trigger/Maus2"));

                TriggerPics.Add(new List<Texture2D>());
                TriggerPics.Add(new List<Texture2D>());
                TriggerPics.Add(new List<Texture2D>());
                TriggerPics.Add(new List<Texture2D>());
                TriggerPics.Add(new List<Texture2D>());
                TriggerPics.Add(new List<Texture2D>());
                TriggerPics.Add(new List<Texture2D>());
                TriggerPics.Add(new List<Texture2D>());
                TriggerPics.Add(new List<Texture2D>());
                TriggerPics.Add(new List<Texture2D>());
                TriggerPics.Add(new List<Texture2D>());
                TriggerPics.Add(new List<Texture2D>());
                TriggerPics[12].Add(Content.Load<Texture2D>("Sprites/Trigger/Pfeil"));
                TriggerPics[12].Add(Content.Load<Texture2D>("Sprites/Trigger/A"));
                TriggerPics[13].Add(Content.Load<Texture2D>("Sprites/Trigger/X"));
                TriggerPics[14].Add(Content.Load<Texture2D>("Sprites/Trigger/Pfeil"));
                TriggerPics[14].Add(Content.Load<Texture2D>("Sprites/Trigger/LT"));
                TriggerPics[15].Add(Content.Load<Texture2D>("Sprites/Trigger/Y"));
                TriggerPics[16].Add(Content.Load<Texture2D>("Sprites/Trigger/LT"));
                TriggerPics[17].Add(Content.Load<Texture2D>("Sprites/Trigger/LB"));
                TriggerPics[18].Add(Content.Load<Texture2D>("Sprites/Trigger/Y"));
                TriggerPics[19].Add(Content.Load<Texture2D>("Sprites/Trigger/Pfeil"));
                TriggerPics[19].Add(Content.Load<Texture2D>("Sprites/Trigger/RT"));
                TriggerPics[20].Add(Content.Load<Texture2D>("Sprites/Trigger/B"));
                TriggerPics[21].Add(Content.Load<Texture2D>("Sprites/Trigger/LT"));
                TriggerPics[22].Add(Content.Load<Texture2D>("Sprites/Trigger/B"));
                TriggerPics[23].Add(Content.Load<Texture2D>("Sprites/Trigger/LB"));

                TriggerProps.Add(new List<Vector3>());
                TriggerProps.Add(new List<Vector3>());
                TriggerProps.Add(new List<Vector3>());
                TriggerProps.Add(new List<Vector3>());
                TriggerProps.Add(new List<Vector3>());
                TriggerProps.Add(new List<Vector3>());
                TriggerProps.Add(new List<Vector3>());
                TriggerProps.Add(new List<Vector3>());
                TriggerProps.Add(new List<Vector3>());
                TriggerProps.Add(new List<Vector3>());
                TriggerProps.Add(new List<Vector3>());
                TriggerProps.Add(new List<Vector3>());
                TriggerProps[0].Add(new Vector3(758, 1200, 0));
                TriggerProps[0].Add(new Vector3(758, 1050, 0));
                TriggerProps[1].Add(new Vector3(760, 1400, 1));
                TriggerProps[2].Add(new Vector3(-1, -1, 0));
                TriggerProps[2].Add(new Vector3(-1, -1, 0));
                TriggerProps[3].Add(new Vector3(1000, 1400, 1));
                TriggerProps[4].Add(new Vector3(-1, -1, 0));
                TriggerProps[4].Add(new Vector3(-1, -1, 0));
                TriggerProps[5].Add(new Vector3(800, 1100, 0));
                TriggerProps[6].Add(new Vector3(920, 1100, 1));
                TriggerProps[7].Add(new Vector3(940, 1200, 0));
                TriggerProps[7].Add(new Vector3(940, 1120, 0));
                TriggerProps[8].Add(new Vector3(980, 1020, 1));
                TriggerProps[9].Add(new Vector3(-1, -1, 0));
                TriggerProps[9].Add(new Vector3(-1, -1, 0));
                TriggerProps[10].Add(new Vector3(950, 1100, 1));
                TriggerProps[11].Add(new Vector3(950, 1100, 0));



                ControlsAnimations.Add(new List<Texture2D>());
                ControlsAnimations.Add(new List<Texture2D>());
                ControlsAnimations.Add(new List<Texture2D>());
                ControlsAnimations.Add(new List<Texture2D>());
                ControlsAnimations.Add(new List<Texture2D>());
                ControlsAnimations.Add(new List<Texture2D>());
                ControlsAnimations.Add(new List<Texture2D>());
                ControlsAnimations.Add(new List<Texture2D>());
                ControlsAnimations[0].Add(Content.Load<Texture2D>("Sprites/Character/Laufen/laufen4"));
                ControlsAnimations[0].Add(Content.Load<Texture2D>("Sprites/Character/Laufen/laufen5"));
                ControlsAnimations[0].Add(Content.Load<Texture2D>("Sprites/Character/Laufen/laufen6"));
                ControlsAnimations[0].Add(Content.Load<Texture2D>("Sprites/Character/Laufen/laufen7"));
                ControlsAnimations[1].Add(Content.Load<Texture2D>("Sprites/Character/Schalter/schalter_wand/schalter1a"));
                ControlsAnimations[1].Add(Content.Load<Texture2D>("Sprites/Character/Schalter/schalter_wand/schalter2a"));
                ControlsAnimations[1].Add(Content.Load<Texture2D>("Sprites/Character/Schalter/schalter_wand/schalter3a"));
                ControlsAnimations[2].Add(Content.Load<Texture2D>("Sprites/UI/PastIcon"));
                ControlsAnimations[3].Add(Content.Load<Texture2D>("Sprites/UI/PresentIcon"));
                ControlsAnimations[4].Add(Content.Load<Texture2D>("Sprites/UI/FutureIcon"));
                ControlsAnimations[5].Add(Content.Load<Texture2D>("Sprites/Character/Port/port1"));
                ControlsAnimations[5].Add(Content.Load<Texture2D>("Sprites/Character/Port/port2"));
                ControlsAnimations[5].Add(Content.Load<Texture2D>("Sprites/Character/Port/port3"));
                ControlsAnimations[6].Add(Content.Load<Texture2D>("Sprites/Character/Port/1"));
                ControlsAnimations[6].Add(Content.Load<Texture2D>("Sprites/Character/Port/2"));
                ControlsAnimations[6].Add(Content.Load<Texture2D>("Sprites/Character/Port/3"));
                ControlsAnimations[7].Add(Content.Load<Texture2D>("Sprites/Trigger/Monokel"));

                ControlsTimers = new List<Vector2>();
                ControlsTimers.Add(Vector2.Zero);
                ControlsTimers.Add(Vector2.Zero);
                ControlsTimers.Add(Vector2.Zero);
                ControlsTimers.Add(Vector2.Zero);
                ControlsTimers.Add(Vector2.Zero);
                ControlsTimers.Add(Vector2.Zero);
                ControlsTimers.Add(Vector2.Zero);
                ControlsTimers.Add(Vector2.Zero);

                CSPics.Add(Content.Load<Texture2D>("Sprites/Ghost/1s"));
                CSPics.Add(Content.Load<Texture2D>("Sprites/Ghost/2s"));
                CSPics.Add(Content.Load<Texture2D>("Sprites/Ghost/3s"));
                CSPics.Add(Content.Load<Texture2D>("Sprites/Ghost/4s"));
                CSPics.Add(Content.Load<Texture2D>("Sprites/Ghost/schwert"));
                CSPics.Add(Content.Load<Texture2D>("Sprites/Ghost/1"));
                CSPics.Add(Content.Load<Texture2D>("Sprites/Ghost/2"));
                CSPics.Add(Content.Load<Texture2D>("Sprites/Ghost/3"));
                CSPics.Add(Content.Load<Texture2D>("Sprites/Ghost/4"));



                if (Savegame.Unlocked[0] == 1)
                {
                    MainMenuButtons.Add(new GameButton("Start", new Vector2(50, 50), StartButton, StartButtonMO, StartButtonP));
                }
                else
                {
                    MainMenuButtons.Add(new GameButton("New", new Vector2(50, 50), NewButton, NewButtonMO, NewButtonP));
                    MainMenuButtons.Add(new GameButton("Start", new Vector2(50, 50), LVButton, LVButtonMO, LVButtonP));
                }
                MainMenuButtons.Add(new GameButton("Options", new Vector2(50, 350), OptionsButton, OptionsButtonMO, OptionsButtonP));
                MainMenuButtons.Add(new GameButton("Exit", new Vector2(50, 650), ExitButton, ExitButtonMO, ExitButtonP));

                OptionsMenuButtons.Add(new GameButton("Graphics", new Vector2(150, 50), FullScreenButton, WindowedButton, WindowedButton));
                OptionsMenuButtons.Add(new GameButton("SoundSlider", new Vector2(150, 300), SoundSlider, SoundSlider, SoundSlider));
                OptionsMenuButtons.Add(new GameButton("Controls", new Vector2(150, 550), ControlsButton, ControlsButtonMO, ControlsButtonP));
                OptionsMenuButtons.Add(new GameButton("Back", new Vector2(150, 800), BackButton, BackButtonMO, BackButtonP));
                OptionsMenuButtons.Add(new GameButton("Confirm", new Vector2(400, 800), ConfirmButton, ConfirmButtonMO, ConfirmButtonP));
                OptionsMenuButtons.Add(new GameButton("Sound", new Vector2(380, 350), SoundDot, SoundDotMO, SoundDotP));

                ControlsMenuButtons.Add(new GameButton("Next", new Vector2(380, 350), NextButton, NextButtonMO, NextButtonP));
                ControlsMenuButtons.Add(new GameButton("Previous", new Vector2(380, 350), PreviousButton, PreviousButtonMO, PreviousButtonP));
                ControlsMenuButtons.Add(new GameButton("Confirm", new Vector2(380, 350), ConfirmButton, ConfirmButtonMO, ConfirmButtonP));

                PauseMenuButtons.Add(new GameButton("PauseResume", Vector2.Zero, StartButton, StartButtonMO, StartButtonP));
                PauseMenuButtons.Add(new GameButton("Replay", Vector2.Zero, ReplayButton, ReplayButtonMO, ReplayButtonP));
                PauseMenuButtons.Add(new GameButton("PauseExit", Vector2.Zero, PauseExitButton, PauseExitButtonMO, PauseExitButtonP));

                LevelEndScreenButtons.Add(new GameButton("Next", new Vector2(50, 850), StartButton, StartButtonMO, StartButtonP));
                LevelEndScreenButtons.Add(new GameButton("Replay", new Vector2(300, 850), ReplayButton, ReplayButtonMO, ReplayButtonP));
                LevelEndScreenButtons.Add(new GameButton("Back", new Vector2(550, 850), BackButton, BackButtonMO, BackButtonP));

                for (int m = 0; m < 3; m++)
                {
                    LevelOffsets.Add(new List<Vector2>());
                    LevelScreenButtons.Add(new List<GameButton>());
                }

                return false;
            }
            else if (LoadVar == maxLoad)
            {
                for (int m = 0; m < 3; m++)
                {
                    LevelScreenButtons[m].Add(new GameButton("Back", new Vector2(50, 800), BackButton, BackButtonMO, BackButtonP));
                    LevelScreenButtons[m].Add(new GameButton("Easy", new Vector2(-32, 0), EasyButton, EasyButton, EasyButton));
                    LevelScreenButtons[m].Add(new GameButton("Medium", new Vector2(-32, 0), MediumButton, MediumButton, MediumButton));
                    LevelScreenButtons[m].Add(new GameButton("Hard", new Vector2(-32, 0), HardButton, HardButton, HardButton));
                    LevelScreenButtons[m].Add(new GameButton("Next", new Vector2(-32, 0), NextButton, NextButtonMO, NextButtonP));
                    LevelScreenButtons[m].Add(new GameButton("Previous", new Vector2(-32, 0), PreviousButton, PreviousButtonMO, PreviousButtonP));
                }

                for (int m = 0; m < LevelScreenButtons.Count; m++)
                {
                    for (int i = 0; i < LevelScreenButtons[m].Count; i++)
                    {
                        GameButton b = LevelScreenButtons[m][i];
                        if (i < Savegame.Unlocked[m])
                        {
                            LevelScreenButtons[m][i].Disabled = false;
                        }
                        else if (b.Name != "Back" && b.Name != "Next" && b.Name != "Previous" && b.Name != "Easy" && b.Name != "Medium" && b.Name != "Hard")
                        {
                            LevelScreenButtons[m][i].Disabled = true;
                        }
                    }
                }

                GameButton deineMudda = OptionsMenuButtons.Find(b => b.Name == "SoundSlider");

                for (int i = 0; i < MainMenuButtons.Count; i++)
                {
                    MainMenuButtons[i].AdjustPosition(GraphicsDevice, i, MainMenuButtons.Count);
                }
                for (int i = 0; i < OptionsMenuButtons.Count - 2; i++)
                {
                    OptionsMenuButtons[i].AdjustPosition(GraphicsDevice, i, OptionsMenuButtons.Count - 2);
                }
                GameButton tmp = OptionsMenuButtons.Find(b => b.Name == "Confirm");
                tmp.Scale = (float)GraphicsDevice.Viewport.Height / 1024f;
                tmp.Position = new Vector2(GraphicsDevice.Viewport.Width / 2 - (tmp.Sprite.Width / 2) * tmp.Scale - 150 * tmp.Scale, GraphicsDevice.Viewport.Height / (OptionsMenuButtons.Count - 2) * (OptionsMenuButtons.FindIndex(b => b.Name == "Confirm") - 1) + 50);

                tmp = OptionsMenuButtons.Find(b => b.Name == "Back");
                tmp.Scale = (float)GraphicsDevice.Viewport.Height / 1024f;
                tmp.Position = new Vector2(GraphicsDevice.Viewport.Width / 2 + 50, GraphicsDevice.Viewport.Height / (OptionsMenuButtons.Count - 2) * (OptionsMenuButtons.FindIndex(b => b.Name == "Confirm") - 1) + 50);

                tmp = OptionsMenuButtons.Find(b => b.Name == "Sound");
                tmp.Scale = (float)GraphicsDevice.Viewport.Height / 1024f;
                tmp.Position = new Vector2(deineMudda.Position.X + 60 * deineMudda.Scale + Options.Volume * 170 * deineMudda.Scale, deineMudda.Position.Y + 50 * deineMudda.Scale);

                for (int i = 0; i < LevelOffsets.Count; i++)
                {
                    for (int j = 0; j < LevelOffsets[i].Count; j++)
                    {
                        Vector2 off = LevelOffsets[i][j];
                        LevelScreenButtons[i][j].Position = new Vector2(GraphicsDevice.Viewport.Width / 2 + 64 * tmp.Scale + off.X, GraphicsDevice.Viewport.Height / 1.25f + off.Y);
                        LevelScreenButtons[i][j].Scale = (float)GraphicsDevice.Viewport.Height / 1024f;
                    }

                    LevelScreenButtons[i].Find(b => b.Name == "Back").Position = new Vector2(50, GraphicsDevice.Viewport.Height - 200);
                    LevelScreenButtons[i].Find(b => b.Name == "Back").Scale = (float)GraphicsDevice.Viewport.Height / 1024f;
                    LevelScreenButtons[i].Find(b => b.Name == "Next").Position = new Vector2(GraphicsDevice.Viewport.Width - 128, GraphicsDevice.Viewport.Height / 2 - 64);
                    LevelScreenButtons[i].Find(b => b.Name == "Next").Scale = (float)GraphicsDevice.Viewport.Height / 1024f;
                    LevelScreenButtons[i].Find(b => b.Name == "Previous").Position = new Vector2(0, GraphicsDevice.Viewport.Height / 2 - 64);
                    LevelScreenButtons[i].Find(b => b.Name == "Previous").Scale = (float)GraphicsDevice.Viewport.Height / 1024f;
                }

                SaveOptions();

                PresentationParameters pp = GraphicsDevice.PresentationParameters;
                renderTarget = new RenderTarget2D(GraphicsDevice, 2048, 2048);
                bgRender = new RenderTarget2D(GraphicsDevice, 2048, 2048);
                windowRender = new RenderTarget2D(GraphicsDevice, 2048, 2048);
                wallRender = new RenderTarget2D(GraphicsDevice, 2048, 2048);
                FXManager = new EffectManager(Content);
                SoundManager = new SoundManager(Options.Volume, Options.Volume, Content);


                return true;
            }
            else
            {
                for (int m = 0; m < LevelNames.Count; m++)
                {
                    if (LoadVar < LevelNames[m].Length)
                    {
                        float hScale = (float)GraphicsDevice.Viewport.Width / 1280f;
                        float vScale = (float)GraphicsDevice.Viewport.Height / 1024f;
                        LevelScreenButtons[m].Add(new GameButton(Convert.ToString(LoadVar), Vector2.Zero, LevelScreenButton, LevelScreenButtonMO, LevelScreenButtonP));
                        LevelOffsets[m].Add(new Vector2(-300 + krueppelcounterj * 128 + (GraphicsDevice.Viewport.Width * m) / vScale, krueppelcounterk));


                        maxCoins.Add(new List<int>());
                        LevelPics.Add(new List<Texture2D>());

                        string s = LevelNames[m][LoadVar];

                        RenderTarget2D rT = new RenderTarget2D(GraphicsDevice, 1024, 1024);

                        maxCoins[m].Add(0);

                        Level l = LoadLevel(s);
                        Texture2D bg = l.PastBackground;
                        if (l.PlayerStart.Z == 1)
                        {
                            bg = l.PresentBackground;
                        }
                        else if (l.PlayerStart.Z == 2)
                        {
                            bg = l.FutureBackground;
                        }
                        List<GameObject> objects = l.Past;
                        if (l.PlayerStart.Z == 1)
                        {
                            objects = l.Present;
                        }
                        else if (l.PlayerStart.Z == 2)
                        {
                            objects = l.Future;
                        }
                        GameCamera c = new GameCamera();
                        Vector2 pos = new Vector2(-l.PlayerStart.X, -l.PlayerStart.Y) + new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);
                        pos.X = MathHelper.Clamp(pos.X, -bg.Width + GraphicsDevice.Viewport.Width, 0);
                        pos.Y = MathHelper.Clamp(pos.Y, -bg.Height + GraphicsDevice.Viewport.Height, 0);

                        c.Position = pos;

                        GraphicsDevice.SetRenderTarget(rT);
                        GraphicsDevice.Clear(Color.Black);

                        spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, null, null, null, null, c.GetMatrix());
                        spriteBatch.Draw(bg, Vector2.Zero, Color.White);
                        spriteBatch.End();

                        spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, null, null, null, null, c.GetMatrix());
                        foreach (GameObject o in objects)
                        {
                            o.Draw(false, spriteBatch);
                        }
                        spriteBatch.End();

                        LevelPics[m].Add(rT);

                        objects = l.Past;
                        foreach (GameObject o in objects)
                        {
                            if (o.Type == 9)
                            {
                                maxCoins[m][maxCoins[m].Count - 1]++;
                                OverallCoins++;
                            }
                        }
                        objects = l.Present;
                        foreach (GameObject o in objects)
                        {
                            if (o.Type == 9)
                            {
                                maxCoins[m][maxCoins[m].Count - 1]++;
                                OverallCoins++;
                            }
                        }
                        objects = l.Future;
                        foreach (GameObject o in objects)
                        {
                            if (o.Type == 9)
                            {
                                maxCoins[m][maxCoins[m].Count - 1]++;
                                OverallCoins++;
                            }
                        }

                        GraphicsDevice.SetRenderTarget(null);


                    }
                }

                krueppelcounterj++;
                if (krueppelcounterj > 4)
                {
                    krueppelcounterj = 0;
                    krueppelcounterk -= 256;
                }

                return false;
            }
        }






        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            MouseState mouseState = Mouse.GetState();
            KeyboardState keyState = Keyboard.GetState();


            if (GameState == "Game")
            {
                SoundManager.Update(Options.Volume);

                if (Player != null)
                {
                    List<GameObject> objects = CurrentLevel.Past;
                    if (Player.Time == 1)
                    {
                        objects = CurrentLevel.Present;
                    }
                    else if (Player.Time == 2)
                    {
                        objects = CurrentLevel.Future;
                    }
                    List<Link> links = CurrentLevel.PastLinks;
                    if (Player.Time == 1)
                    {
                        links = CurrentLevel.PresentLinks;
                    }
                    else if (Player.Time == 2)
                    {
                        links = CurrentLevel.FutureLinks;
                    }
                    Texture2D bg = CurrentLevel.PastBackground;
                    if (Player.Time == 1)
                    {
                        bg = CurrentLevel.PresentBackground;
                    }
                    else if (Player.Time == 2)
                    {
                        bg = CurrentLevel.FutureBackground;
                    }

                    Player.Update(CurrentLevel, CollisionCheck, gameTime, Cursor, new Vector2(mouseState.X - Cam.Position.X, mouseState.Y - Cam.Position.Y), UI, FXManager, Cam, GraphicsDevice, SoundManager, ref Finished, ref TriggerActivation, Options.ControlType, ref Restart, CollectedCoins, OverallCoins);
                    if (Player.State != 12 && Player.State != 13)
                    {
                        UI.Update(gameTime);


                        for (int i = 0; i < objects.Count; i++)
                        {
                            GameObject o = objects[i];
                            o.Update(Player, objects, links, CollisionCheck, gameTime, Cursor, CollisionCheck);
                            if (o.Position.Y > bg.Height)
                            {
                                objects.RemoveAt(i);
                            }
                        }
                    }

                    FXManager.Update(gameTime, CurrentLevel, Player, Cam, Options.ControlType);
                    Cam.Update(Player, GraphicsDevice, bg);

                    //Triggerstuff
                    if (TriggerActivation == 1)
                    {
                        TriggerActivation = 0;
                        Trigger++;
                        TriggerActivated = true;
                    }
                    if (TriggerActivation == 2)
                    {
                        if (Trigger > -1)
                        {
                            if (TriggerProps[Trigger][0].Z > 0)
                            {
                                TriggerActivation = 0;
                                Trigger++;
                                TriggerActivated = true;
                            }
                            else
                            {
                                TriggerActivation = 0;
                                TriggerActivated = false;
                            }
                        }
                    }



                    //Geistszenenstuff
                    if (ChosenLevel == 7 && Difficulty == 0)
                    {
                        if (CSPosFuck.X - Player.Position.X < 80 && CSPhase != 2)
                        {
                            CSPhase = 1;
                            if (CollisionCheck(Player.Sprite, Player.Position, CSPics[CSAP], CSPosFuck, 0))
                            {
                                CSPhase = 2;
                                Player.ChangeSprites(1, Content);
                            }
                        }
                        else if (CSPhase != 2)
                        {
                            CSPhase = 0;
                        }

                        if (CSPhase == 0)
                        {
                            CSAT += gameTime.ElapsedGameTime.Milliseconds;

                            if (CSAT > 100)
                            {
                                CSAT = 0;
                                CSAP++;

                                if (CSAP > 3)
                                {
                                    CSAP = 0;
                                }
                            }
                        }

                        if (CSPhase == 1)
                        {
                            CSAP = 4;
                        }

                        if (CSPhase == 2)
                        {
                            CSAT += gameTime.ElapsedGameTime.Milliseconds;

                            if (CSAT > 100)
                            {
                                CSAT = 0;
                                CSAP++;

                                if (CSAP > 8)
                                {
                                    CSAP = 5;
                                }
                            }

                            CSPosFuck += new Vector2(0, -5);
                        }
                    }
                }
                else
                {
                    int ab = 0;
                    if (ChosenLevel >= 7)
                    {
                        ab = 1;
                    }
                    if (ChosenLevel >= 10)
                    {
                        ab = 2;
                    }
                    if (ChosenLevel >= 23)
                    {
                        ab = 3;
                    }
                    if (Difficulty > 0)
                    {
                        ab = 3;
                    }

                    Player = new GameCharacter(CurrentLevel.PlayerStart, Content, ab);
                    if (ChosenLevel <= 7 && Difficulty == 0)
                    {
                        Player.ChangeSprites(0, Content);
                        CSPosFuck = new Vector2(770, 1560);
                        CSPhase = 0;
                    }

                    foreach (GameObject o in CurrentLevel.Past)
                    {
                        o.Update(Player, CurrentLevel.Past, CurrentLevel.PastLinks, CollisionCheck, gameTime, Cursor, CollisionCheck);
                    }
                    foreach (GameObject o in CurrentLevel.Present)
                    {
                        o.Update(Player, CurrentLevel.Present, CurrentLevel.PresentLinks, CollisionCheck, gameTime, Cursor, CollisionCheck);
                    }
                    foreach (GameObject o in CurrentLevel.Future)
                    {
                        o.Update(Player, CurrentLevel.Future, CurrentLevel.FutureLinks, CollisionCheck, gameTime, Cursor, CollisionCheck);
                    }
                }

                if (keyState.IsKeyDown(Keys.Escape) && !KeyPressed)
                {
                    KeyPressed = true;
                    GameState = "Pause";
                    PauseMenuButtons[0].Position = new Vector2(GraphicsDevice.Viewport.Width / 2 - 122.5f, GraphicsDevice.Viewport.Height / 2 - 320);
                    PauseMenuButtons[1].Position = new Vector2(GraphicsDevice.Viewport.Width / 2 - 122.5f, GraphicsDevice.Viewport.Height / 2 - 80);
                    PauseMenuButtons[2].Position = new Vector2(GraphicsDevice.Viewport.Width / 2 - 122.5f, GraphicsDevice.Viewport.Height / 2 + 160);
                }

                if (Restart)
                {
                    UI.Reset();
                    Player = null;
                    CurrentLevel = LoadLevel(LevelNames[Difficulty][ChosenLevel]);
                    if (ChosenLevel == 2)
                    {
                        Trigger = -1;
                    }
                    if (ChosenLevel == 7)
                    {
                        Trigger = 0;
                    }
                    if (ChosenLevel == 10)
                    {
                        Trigger = 4;
                    }
                    if (ChosenLevel == 17)
                    {
                        Trigger = 5;
                    }
                    if (ChosenLevel == 24)
                    {
                        Trigger = 7;
                    }
                    if (ChosenLevel == 25)
                    {
                        Trigger = 9;
                    }

                    Restart = false;
                }

                if (Finished)
                {
                    Savegame.Played[Difficulty][ChosenLevel][AndereDifficulty] = true;
                    Savegame.Coins[Difficulty][ChosenLevel] = UI.Coins;
                    Savegame.Diamonds = UI.Diamonds;
                    if (Savegame.Unlocked[Difficulty] < ChosenLevel + 2)
                    {
                        Savegame.Unlocked[Difficulty] = ChosenLevel + 2;
                    }
                    if (ChosenLevel == LevelNames[Difficulty].Length - 1 && Difficulty < 2 && Savegame.Unlocked[Difficulty + 1] == 0)
                    {
                        Savegame.Unlocked[Difficulty + 1] = 1;
                    }
                    SaveGame();

                    CollectedCoins = 0;
                    for (int i = 0; i < Savegame.Coins.Count; i++)
                    {
                        for (int j = 0; j < Savegame.Coins[i].Length; j++)
                        {
                            CollectedCoins += Savegame.Coins[i][j];
                        }
                    }

                    MainMenuButtons = new List<GameButton>();
                    if (Savegame.Unlocked[0] == 1)
                    {
                        MainMenuButtons.Add(new GameButton("Start", new Vector2(50, 50), StartButton, StartButtonMO, StartButtonP));
                    }
                    else
                    {
                        MainMenuButtons.Add(new GameButton("New", new Vector2(50, 50), NewButton, NewButtonMO, NewButtonP));
                        MainMenuButtons.Add(new GameButton("Start", new Vector2(50, 50), LVButton, LVButtonMO, LVButtonP));
                    }
                    MainMenuButtons.Add(new GameButton("Options", new Vector2(50, 350), OptionsButton, OptionsButtonMO, OptionsButtonP));
                    MainMenuButtons.Add(new GameButton("Exit", new Vector2(50, 650), ExitButton, ExitButtonMO, ExitButtonP));
                    for (int i = 0; i < MainMenuButtons.Count; i++)
                    {
                        MainMenuButtons[i].AdjustPosition(GraphicsDevice, i, MainMenuButtons.Count);
                    }


                    float scale = (float)GraphicsDevice.Viewport.Height / 1024f;
                    LevelEndScreenButtons[0].Position = new Vector2(50 * scale, GraphicsDevice.Viewport.Height - 174 * scale);
                    LevelEndScreenButtons[0].Scale = scale;
                    LevelEndScreenButtons[1].Position = new Vector2(300 * scale, GraphicsDevice.Viewport.Height - 174 * scale);
                    LevelEndScreenButtons[1].Scale = scale;
                    LevelEndScreenButtons[2].Position = new Vector2(550 * scale, GraphicsDevice.Viewport.Height - 174 * scale);
                    LevelEndScreenButtons[2].Scale = scale;

                    GameState = "LevelEndScreen";
                    Finished = false;
                    CoinToAnim = 0;
                }
            }
            else if (GameState == "Pause")
            {
                SoundManager.Update(Options.Volume);

                foreach (GameButton b in PauseMenuButtons)
                {
                    b.Update(SoundManager);
                }

                GameButton pressedButton = PauseMenuButtons.Find(b => b.WasPressed);

                if (pressedButton != null)
                {
                    if (pressedButton.Name == "PauseResume")
                    {
                        GameState = "Game";
                        pressedButton.MouseOver = false;
                        pressedButton.Pressed = false;
                        pressedButton.WasPressed = false;
                    }

                    if (pressedButton.Name == "Replay")
                    {
                        GameState = "Game";
                        UI.Reset();
                        Player = null;
                        CurrentLevel = LoadLevel(LevelNames[Difficulty][ChosenLevel]);
                        pressedButton.MouseOver = false;
                        pressedButton.Pressed = false;
                        pressedButton.WasPressed = false;
                        if (ChosenLevel == 2)
                        {
                            Trigger = -1;
                        }
                        if (ChosenLevel == 7)
                        {
                            Trigger = 0;
                        }
                        if (ChosenLevel == 10)
                        {
                            Trigger = 4;
                        }
                        if (ChosenLevel == 17)
                        {
                            Trigger = 5;
                        }
                        if (ChosenLevel == 24)
                        {
                            Trigger = 7;
                        }
                        if (ChosenLevel == 25)
                        {
                            Trigger = 9;
                        }
                    }

                    if (pressedButton.Name == "PauseExit")
                    {
                        GameState = "MainMenu";
                        Player = null;
                        UI = null;
                        pressedButton.MouseOver = false;
                        pressedButton.Pressed = false;
                        pressedButton.WasPressed = false;
                        ChosenLevel = -1;
                    }
                }

                if (keyState.IsKeyDown(Keys.Escape) && !KeyPressed)
                {
                    KeyPressed = true;
                    GameState = "Game";
                }
            }
            else if (GameState == "Intro")
            {
                if (!Loaded)
                {
                    if (LoadStuff())
                    {
                        Loaded = true;
                    }
                    LoadVar++;
                }


                if (ga)
                {
                    if (FadeTimer < 0)
                    {
                        Fade += fadeSpeed;

                        if (fadeSpeed > 0)
                        {
                            if (Fade >= 1f)
                            {
                                FadeTimer = 0;
                            }
                        }
                        else
                        {
                            if (Fade <= 0f)
                            {
                                ga = false;
                                it = true;
                                fadeSpeed = 0.025f;
                                FadeTimer = -1;
                            }
                        }
                    }
                    else
                    {
                        FadeTimer += gameTime.ElapsedGameTime.Milliseconds;

                        if (FadeTimer > 1000)
                        {
                            FadeTimer = -1;
                            fadeSpeed = -0.025f;
                        }
                    }
                }

                if (it)
                {
                    if (FadeTimer < 0)
                    {
                        Fade += fadeSpeed;

                        if (fadeSpeed > 0)
                        {
                            if (Fade >= 1f)
                            {
                                FadeTimer = 0;
                            }
                        }
                        else
                        {
                            if (Fade <= 0f)
                            {
                                ga = false;
                                it = false;
                                GameState = "MainMenu";
                            }
                        }
                    }
                    else
                    {
                        FadeTimer += gameTime.ElapsedGameTime.Milliseconds;

                        if (FadeTimer > 1000)
                        {
                            FadeTimer = -1;
                            fadeSpeed = -0.025f;
                        }
                    }
                }
            }
            else
            {
                SoundManager.Update(Options.Volume);

                GameButton deineMudda = OptionsMenuButtons.Find(b => b.Name == "SoundSlider");

                float Scale = GraphicsDevice.Viewport.Height / 1024f;

                if (GraphicsDevice.Viewport.Width != Options.Resolution.X || GraphicsDevice.Viewport.Height != Options.Resolution.Y)
                {
                    for (int i = 0; i < MainMenuButtons.Count; i++)
                    {
                        MainMenuButtons[i].AdjustPosition(GraphicsDevice, i, MainMenuButtons.Count);
                    }
                    for (int i = 0; i < OptionsMenuButtons.Count - 2; i++)
                    {
                        OptionsMenuButtons[i].AdjustPosition(GraphicsDevice, i, OptionsMenuButtons.Count - 2);
                    }
                    GameButton tmp = OptionsMenuButtons.Find(b => b.Name == "Confirm");
                    tmp.Scale = (float)GraphicsDevice.Viewport.Height / 1024f;
                    tmp.Position = new Vector2(GraphicsDevice.Viewport.Width / 2 - (tmp.Sprite.Width / 2) * tmp.Scale - 150 * tmp.Scale, GraphicsDevice.Viewport.Height / (OptionsMenuButtons.Count - 2) * (OptionsMenuButtons.FindIndex(b => b.Name == "Confirm") - 1) + 50);

                    tmp = OptionsMenuButtons.Find(b => b.Name == "Back");
                    tmp.Scale = (float)GraphicsDevice.Viewport.Height / 1024f;
                    tmp.Position = new Vector2(GraphicsDevice.Viewport.Width / 2 + 50, GraphicsDevice.Viewport.Height / (OptionsMenuButtons.Count - 2) * (OptionsMenuButtons.FindIndex(b => b.Name == "Confirm") - 1) + 50);

                    tmp = OptionsMenuButtons.Find(b => b.Name == "Sound");
                    tmp.Scale = (float)GraphicsDevice.Viewport.Height / 1024f;
                    tmp.Position = new Vector2(deineMudda.Position.X + 60 * deineMudda.Scale + Options.Volume * 170 * deineMudda.Scale, deineMudda.Position.Y + 50 * deineMudda.Scale);


                    LevelOffsets = new List<List<Vector2>>();
                    LevelOffsets.Add(new List<Vector2>());
                    LevelOffsets.Add(new List<Vector2>());
                    LevelOffsets.Add(new List<Vector2>());
                    float hScale = (float)GraphicsDevice.Viewport.Width / 1280f;
                    float vScale = (float)GraphicsDevice.Viewport.Height / 1024f;
                    for (int m = 0; m < LevelNames.Count; m++)
                    {
                        krueppelcounterj = 0;
                        krueppelcounterk = 0;
                        for (int n = 0; n < LevelNames[m].Length; n++)
                        {
                            LevelOffsets[m].Add(new Vector2(-300 + krueppelcounterj * 128 + (GraphicsDevice.Viewport.Width * m) / vScale, krueppelcounterk));
                            krueppelcounterj++;
                            if (krueppelcounterj > 4)
                            {
                                krueppelcounterj = 0;
                                krueppelcounterk -= 256;
                            }
                        }
                    }

                    for (int i = 0; i < LevelOffsets.Count; i++)
                    {
                        for (int j = 0; j < LevelOffsets[i].Count; j++)
                        {
                            Vector2 off = LevelOffsets[i][j];
                            LevelScreenButtons[i][j].Position = new Vector2(GraphicsDevice.Viewport.Width / 2 + 64 * tmp.Scale + off.X, GraphicsDevice.Viewport.Height / 1.25f + off.Y);
                            LevelScreenButtons[i][j].Scale = (float)GraphicsDevice.Viewport.Height / 1024f;
                        }

                        LevelScreenButtons[i].Find(b => b.Name == "Back").Position = new Vector2(50, GraphicsDevice.Viewport.Height - 200);
                        LevelScreenButtons[i].Find(b => b.Name == "Back").Scale = (float)GraphicsDevice.Viewport.Height / 1024f;
                        LevelScreenButtons[i].Find(b => b.Name == "Next").Position = new Vector2(GraphicsDevice.Viewport.Width - 128, GraphicsDevice.Viewport.Height / 2 - 64);
                        LevelScreenButtons[i].Find(b => b.Name == "Next").Scale = (float)GraphicsDevice.Viewport.Height / 1024f;
                        LevelScreenButtons[i].Find(b => b.Name == "Previous").Position = new Vector2(0, GraphicsDevice.Viewport.Height / 2 - 64);
                        LevelScreenButtons[i].Find(b => b.Name == "Previous").Scale = (float)GraphicsDevice.Viewport.Height / 1024f;
                    }

                    Options.Resolution.X = GraphicsDevice.Viewport.Width;
                    Options.Resolution.Y = GraphicsDevice.Viewport.Height;
                    LevelScreenScroll = new Vector2(0, 0);
                    Difficulty = 0;
                    ScrollDestination = 0;
                    SaveOptions();
                    SoundManager.Volume = Options.Volume;
                }

                if (GameState == "MainMenu")
                {
                    if (keyState.IsKeyDown(Keys.T) && keyState.IsKeyDown(Keys.X) && keyState.IsKeyDown(Keys.L) && keyState.IsKeyDown(Keys.M))
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            Savegame.Unlocked[i] = 50;
                        }
                    }

                    CoinTimer += gameTime.ElapsedGameTime.Milliseconds;
                    if (CoinTimer > 10)
                    {
                        Random rand = new Random();
                        CoinTimer = 0;
                        Coins.Add(new Vector2(rand.Next(-50, GraphicsDevice.Viewport.Width), -100));
                    }

                    for (int i = 0; i < Coins.Count; i++)
                    {
                        Vector2 c = Coins[i];
                        Coins[i] += new Vector2(0, (c.Y + 101) / 10f);
                        if (c.Y > GraphicsDevice.Viewport.Height)
                        {
                            Coins.RemoveAt(i);
                        }
                    }

                    foreach (GameButton b in MainMenuButtons)
                    {
                        b.Update(SoundManager);
                    }

                    GameButton pressedButton = MainMenuButtons.Find(b => b.WasPressed);

                    if (pressedButton != null)
                    {
                        if (pressedButton.Name == "New")
                        {
                            Savegame = new Save();
                            Savegame.Unlocked = new List<int>();
                            Savegame.Unlocked.Add(1);
                            Savegame.Unlocked.Add(0);
                            Savegame.Unlocked.Add(0);
                            Savegame.Played = new List<List<bool[]>>();
                            Savegame.Played.Add(new List<bool[]>());
                            Savegame.Played.Add(new List<bool[]>());
                            Savegame.Played.Add(new List<bool[]>());
                            Savegame.Coins = new List<int[]>();
                            for (int i = 0; i < LevelNames.Count; i++)
                            {
                                foreach (string suppe in LevelNames[i])
                                {
                                    Savegame.Played[i].Add(new bool[3]);
                                }
                                Savegame.Coins.Add(new int[LevelNames[i].Length]);
                            }
                            Savegame.Diamonds = new bool[4];
                            SaveGame();

                            CollectedCoins = 0;

                            MainMenuButtons = new List<GameButton>();
                            if (Savegame.Unlocked[0] == 1)
                            {
                                MainMenuButtons.Add(new GameButton("Start", new Vector2(50, 50), StartButton, StartButtonMO, StartButtonP));
                            }
                            else
                            {
                                MainMenuButtons.Add(new GameButton("New", new Vector2(50, 50), NewButton, NewButtonMO, NewButtonP));
                                MainMenuButtons.Add(new GameButton("Start", new Vector2(50, 50), LVButton, LVButtonMO, LVButtonP));
                            }
                            MainMenuButtons.Add(new GameButton("Options", new Vector2(50, 350), OptionsButton, OptionsButtonMO, OptionsButtonP));
                            MainMenuButtons.Add(new GameButton("Exit", new Vector2(50, 650), ExitButton, ExitButtonMO, ExitButtonP));
                            for (int i = 0; i < MainMenuButtons.Count; i++)
                            {
                                MainMenuButtons[i].AdjustPosition(GraphicsDevice, i, MainMenuButtons.Count);
                            }

                            for (int j = 0; j < LevelScreenButtons.Count; j++)
                            {
                                for (int i = 0; i < LevelScreenButtons[j].Count; i++)
                                {
                                    GameButton b = LevelScreenButtons[j][i];
                                    if (i < Savegame.Unlocked[j])
                                    {
                                        LevelScreenButtons[j][i].Disabled = false;
                                    }
                                    else if (b.Name != "Back" && b.Name != "Next" && b.Name != "Previous" && b.Name != "Easy" && b.Name != "Medium" && b.Name != "Hard")
                                    {
                                        LevelScreenButtons[j][i].Disabled = true;
                                    }
                                }
                            }

                            for (int i = 0; i < LevelScreenButtons.Count; i++)
                            {
                                foreach (GameButton b in LevelScreenButtons[i])
                                {
                                    if (b.Name == "Easy" || b.Name == "Medium" || b.Name == "Hard" || b.Name == "Back")
                                    {
                                        b.Position = new Vector2(-32, GraphicsDevice.Viewport.Height);
                                    }
                                }
                            }

                            LevelScreenButtons[Difficulty].Find(b => b.Name == "Back").Position = new Vector2(50, GraphicsDevice.Viewport.Height - 200);
                            LevelScreenButtons[Difficulty].Find(b => b.Name == "Back").Scale = (float)GraphicsDevice.Viewport.Height / 1024f;
                            GameState = "LevelScreen";
                            pressedButton.MouseOver = false;
                            pressedButton.Pressed = false;
                            pressedButton.WasPressed = false;
                        }

                        if (pressedButton.Name == "Start")
                        {
                            for (int j = 0; j < LevelScreenButtons.Count; j++)
                            {
                                for (int i = 0; i < LevelScreenButtons[j].Count; i++)
                                {
                                    GameButton b = LevelScreenButtons[j][i];
                                    if (i < Savegame.Unlocked[j])
                                    {
                                        LevelScreenButtons[j][i].Disabled = false;
                                    }
                                    else if (b.Name != "Back" && b.Name != "Next" && b.Name != "Previous" && b.Name != "Easy" && b.Name != "Medium" && b.Name != "Hard")
                                    {
                                        LevelScreenButtons[j][i].Disabled = true;
                                    }
                                }
                            }

                            for (int i = 0; i < LevelScreenButtons.Count; i++)
                            {
                                foreach (GameButton b in LevelScreenButtons[i])
                                {
                                    if (b.Name == "Easy" || b.Name == "Medium" || b.Name == "Hard" || b.Name == "Back")
                                    {
                                        b.Position = new Vector2(-32, GraphicsDevice.Viewport.Height);
                                    }
                                }
                            }

                            LevelScreenButtons[Difficulty].Find(b => b.Name == "Back").Position = new Vector2(50, GraphicsDevice.Viewport.Height - 200);
                            LevelScreenButtons[Difficulty].Find(b => b.Name == "Back").Scale = (float)GraphicsDevice.Viewport.Height / 1024f;
                            GameState = "LevelScreen";
                            pressedButton.MouseOver = false;
                            pressedButton.Pressed = false;
                            pressedButton.WasPressed = false;
                        }

                        if (pressedButton.Name == "Options")
                        {
                            GameState = "OptionsMenu";
                            pressedButton.MouseOver = false;
                            pressedButton.Pressed = false;
                            pressedButton.WasPressed = false;
                        }

                        if (pressedButton.Name == "Exit")
                        {
                            this.Exit();
                        }
                    }
                }
                else if (GameState == "OptionsMenu")
                {
                    foreach (GameButton b in OptionsMenuButtons)
                    {
                        b.Update(SoundManager);
                    }

                    GameButton pressedButton = OptionsMenuButtons.Find(b => b.WasPressed);

                    if (OptionsMenuButtons.Find(b => b.Name == "SoundSlider").Pressed)
                    {
                        GameButton slideDot = OptionsMenuButtons.Find(b => b.Name == "Sound");
                        float x = MathHelper.Clamp(mouseState.X - slideDot.Sprite.Width / 2, deineMudda.Position.X + 60 * deineMudda.Scale, deineMudda.Position.X + 230 * deineMudda.Scale);
                        slideDot.Position = new Vector2(x, slideDot.Position.Y);
                    }

                    if (pressedButton != null)
                    {
                        if (pressedButton.Name == "Graphics")
                        {
                            if (graphics.IsFullScreen)
                            {
                                graphics.IsFullScreen = false;
                                graphics.PreferredBackBufferWidth = (int)(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width * 0.75);
                                graphics.PreferredBackBufferHeight = (int)(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height * 0.75);
                                pressedButton.Sprite = WindowedButton;
                                pressedButton.MouseOverSprite = FullScreenButton;
                                pressedButton.PressedSprite = FullScreenButton;
                            }
                            else
                            {
                                graphics.IsFullScreen = true;
                                graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
                                graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
                                pressedButton.Sprite = FullScreenButton;
                                pressedButton.MouseOverSprite = WindowedButton;
                                pressedButton.PressedSprite = WindowedButton;
                            }
                        }

                        if (pressedButton.Name == "Controls")
                        {
                            ControlsMenuButtons[0].Position = new Vector2(GraphicsDevice.Viewport.Width - ControlsMenuButtons[0].Sprite.Width - 10, GraphicsDevice.Viewport.Height / 2 - ControlsMenuButtons[0].Sprite.Height / 2);
                            ControlsMenuButtons[1].Position = new Vector2(10, GraphicsDevice.Viewport.Height / 2 - ControlsMenuButtons[1].Sprite.Height / 2);
                            ControlsMenuButtons[2].Position = new Vector2(20, GraphicsDevice.Viewport.Height - ControlsMenuButtons[2].Sprite.Height - 20);
                            GameState = "ControlsMenu";
                            pressedButton.MouseOver = false;
                            pressedButton.Pressed = false;
                            pressedButton.WasPressed = false;
                        }

                        if (pressedButton.Name == "Back")
                        {
                            graphics.PreferredBackBufferWidth = (int)Options.Resolution.X;
                            graphics.PreferredBackBufferHeight = (int)Options.Resolution.Y;
                            graphics.IsFullScreen = Options.FullScreen;
                            GameState = "MainMenu";
                            pressedButton.MouseOver = false;
                            pressedButton.Pressed = false;
                            pressedButton.WasPressed = false;
                        }

                        if (pressedButton.Name == "Confirm")
                        {

                            Options.Resolution = new Vector2(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
                            Options.FullScreen = graphics.IsFullScreen;
                            graphics.ApplyChanges();
                            Options.Volume = MathHelper.Distance(deineMudda.Position.X + 60 * deineMudda.Scale, OptionsMenuButtons.Find(b => b.Name == "Sound").Position.X) / (170f * deineMudda.Scale);


                            for (int i = 0; i < MainMenuButtons.Count; i++)
                            {
                                MainMenuButtons[i].AdjustPosition(GraphicsDevice, i, MainMenuButtons.Count);
                            }
                            for (int i = 0; i < OptionsMenuButtons.Count - 2; i++)
                            {
                                OptionsMenuButtons[i].AdjustPosition(GraphicsDevice, i, OptionsMenuButtons.Count - 2);
                            }
                            GameButton tmp = OptionsMenuButtons.Find(b => b.Name == "Confirm");
                            tmp.Scale = (float)GraphicsDevice.Viewport.Height / 1024f;
                            tmp.Position = new Vector2(GraphicsDevice.Viewport.Width / 2 - (tmp.Sprite.Width / 2) * tmp.Scale - 150 * tmp.Scale, GraphicsDevice.Viewport.Height / (OptionsMenuButtons.Count - 2) * (OptionsMenuButtons.FindIndex(b => b.Name == "Confirm") - 1) + 50);

                            tmp = OptionsMenuButtons.Find(b => b.Name == "Back");
                            tmp.Scale = (float)GraphicsDevice.Viewport.Height / 1024f;
                            tmp.Position = new Vector2(GraphicsDevice.Viewport.Width / 2 + 50, GraphicsDevice.Viewport.Height / (OptionsMenuButtons.Count - 2) * (OptionsMenuButtons.FindIndex(b => b.Name == "Confirm") - 1) + 50);

                            tmp = OptionsMenuButtons.Find(b => b.Name == "Sound");
                            tmp.Scale = (float)GraphicsDevice.Viewport.Height / 1024f;
                            tmp.Position = new Vector2(deineMudda.Position.X + 60 * deineMudda.Scale + Options.Volume * 170 * deineMudda.Scale, deineMudda.Position.Y + 50 * deineMudda.Scale);



                            LevelOffsets = new List<List<Vector2>>();
                            LevelOffsets.Add(new List<Vector2>());
                            LevelOffsets.Add(new List<Vector2>());
                            LevelOffsets.Add(new List<Vector2>());
                            float hScale = (float)GraphicsDevice.Viewport.Width / 1280f;
                            float vScale = (float)GraphicsDevice.Viewport.Height / 1024f;
                            for (int m = 0; m < LevelNames.Count; m++)
                            {
                                krueppelcounterj = 0;
                                krueppelcounterk = 0;
                                for (int n = 0; n < LevelNames[m].Length; n++)
                                {
                                    LevelOffsets[m].Add(new Vector2(-300 + krueppelcounterj * 128 + (GraphicsDevice.Viewport.Width * m) / vScale, krueppelcounterk));
                                    krueppelcounterj++;
                                    if (krueppelcounterj > 4)
                                    {
                                        krueppelcounterj = 0;
                                        krueppelcounterk -= 256;
                                    }
                                }
                            }

                            for (int i = 0; i < LevelOffsets.Count; i++)
                            {
                                for (int j = 0; j < LevelOffsets[i].Count; j++)
                                {
                                    Vector2 off = LevelOffsets[i][j];
                                    LevelScreenButtons[i][j].Scale = (float)GraphicsDevice.Viewport.Height / 1024f;
                                    LevelScreenButtons[i][j].Position = new Vector2(GraphicsDevice.Viewport.Width / 2 + 64 * tmp.Scale + off.X, GraphicsDevice.Viewport.Height / 1.25f + off.Y);
                  
                                }

                                LevelScreenButtons[i].Find(b => b.Name == "Back").Position = new Vector2(50, GraphicsDevice.Viewport.Height - 200);
                                LevelScreenButtons[i].Find(b => b.Name == "Back").Scale = (float)GraphicsDevice.Viewport.Height / 1024f;
                                LevelScreenButtons[i].Find(b => b.Name == "Next").Position = new Vector2(GraphicsDevice.Viewport.Width - 128, GraphicsDevice.Viewport.Height / 2 - 64);
                                LevelScreenButtons[i].Find(b => b.Name == "Next").Scale = (float)GraphicsDevice.Viewport.Height / 1024f;
                                LevelScreenButtons[i].Find(b => b.Name == "Previous").Position = new Vector2(0, GraphicsDevice.Viewport.Height / 2 - 64);
                                LevelScreenButtons[i].Find(b => b.Name == "Previous").Scale = (float)GraphicsDevice.Viewport.Height / 1024f;
                            }

                            Options.Resolution.X = GraphicsDevice.Viewport.Width;
                            Options.Resolution.Y = GraphicsDevice.Viewport.Height;
                            LevelScreenScroll = new Vector2(0, 0);
                            Difficulty = 0;
                            ScrollDestination = 0;
                            SaveOptions();
                            SoundManager.Volume = Options.Volume;

                            Window.AllowUserResizing = !graphics.IsFullScreen;
                        }
                    }
                }
                else if (GameState == "ControlsMenu")
                {
                    foreach (GameButton b in ControlsMenuButtons)
                    {
                        b.Update(SoundManager);
                    }

                    for (int i = 0; i < ControlsTimers.Count; i++)
                    {
                        ControlsTimers[i] += new Vector2(gameTime.ElapsedGameTime.Milliseconds, 0);
                        if (ControlsTimers[i].X > 200)
                        {
                            ControlsTimers[i] = new Vector2(0, ControlsTimers[i].Y + 1);
                            if (ControlsTimers[i].Y >= ControlsAnimations[i].Count)
                            {
                                ControlsTimers[i] = Vector2.Zero;
                            }
                        }
                    }

                    GameButton pressedButton = ControlsMenuButtons.Find(b => b.Pressed);

                    if (pressedButton != null)
                    {
                        if (pressedButton.Name == "Next" && Options.ControlType == 0)
                        {
                            Options.ControlType = 1;
                        }

                        if (pressedButton.Name == "Previous" && Options.ControlType == 1)
                        {
                            Options.ControlType = 0;
                        }

                        if (pressedButton.Name == "Confirm")
                        {
                            GameState = "OptionsMenu";
                            SaveOptions();
                            pressedButton.MouseOver = false;
                            pressedButton.Pressed = false;
                            pressedButton.WasPressed = false;
                        }
                    }
                }
                else if (GameState == "LevelScreen")
                {

                    GameButton easy = LevelScreenButtons[Difficulty].Find(b => b.Name == "Easy");
                    GameButton medium = LevelScreenButtons[Difficulty].Find(b => b.Name == "Medium");
                    GameButton hard = LevelScreenButtons[Difficulty].Find(b => b.Name == "Hard");

                    if (mouseState.Y < 50 && LevelScreenScroll.Y < ((LevelNames[Difficulty].Length / 5) - 1) * 256)
                    {
                        LevelScreenScroll += new Vector2(0, 10);
                        easy.Position += new Vector2(0, 10);
                        medium.Position += new Vector2(0, 10);
                        hard.Position += new Vector2(0, 10);
                    }
                    if (mouseState.Y > GraphicsDevice.Viewport.Height - 50 && LevelScreenScroll.Y > 0)
                    {
                        LevelScreenScroll += new Vector2(0, -10);
                        easy.Position += new Vector2(0, -10);
                        medium.Position += new Vector2(0, -10);
                        hard.Position += new Vector2(0, -10);
                    }

                    
                    foreach (GameButton b in LevelScreenButtons[Difficulty])
                    {
                        if (b.Name != "Back" && b.Name != "Easy" && b.Name != "Medium" && b.Name != "Hard" && b.Name != "Next" && b.Name != "Previous")
                        {
                            b.Position = new Vector2(GraphicsDevice.Viewport.Width / 2 + 64 + LevelOffsets[Difficulty][Convert.ToInt32(b.Name)].X * Scale, GraphicsDevice.Viewport.Height / 1.25f + LevelOffsets[Difficulty][Convert.ToInt32(b.Name)].Y * Scale) + LevelScreenScroll;
                        }

                        if (b.Name != Convert.ToString(ChosenLevel))
                        {
                            b.Update(SoundManager);
                        }
                    }


                    if (ScrollAnim == 1)
                    {
                        if (LevelScreenScroll.X > ScrollDestination)
                        {
                            for (int i = 0; i < LevelScreenButtons.Count; i++)
                            {
                                foreach (GameButton b in LevelScreenButtons[i])
                                {
                                    if (b.Name != "Back" && b.Name != "Next" && b.Name != "Previous" && b.Name != "Easy" && b.Name != "Medium" && b.Name != "Hard")
                                    {
                                        b.Position -= new Vector2(50, 0);
                                    }
                                }
                            }
                            LevelScreenScroll -= new Vector2(50, 0);
                        }
                        else
                        {
                            ScrollAnim = 0;
                        }
                    }
                    if (ScrollAnim == 2)
                    {
                        if (LevelScreenScroll.X < ScrollDestination)
                        {
                            for (int i = 0; i < LevelScreenButtons.Count; i++)
                            {
                                foreach (GameButton b in LevelScreenButtons[i])
                                {
                                    if (b.Name != "Back" && b.Name != "Next" && b.Name != "Previous" && b.Name != "Easy" && b.Name != "Medium" && b.Name != "Hard")
                                    {
                                        b.Position += new Vector2(50, 0);
                                    }
                                }
                            }
                            LevelScreenScroll += new Vector2(50, 0);
                        }
                        else
                        {
                            ScrollAnim = 0;
                        }
                    }


                    List<GameButton> pressedButtons = LevelScreenButtons[Difficulty].FindAll(b => b.WasPressed);
                    GameButton pressedButton = pressedButtons.Find(b => b.Name != Convert.ToString(ChosenLevel));


                    if (pressedButton != null)
                    {
                        if (pressedButton.Name == "Next")
                        {
                            if (Difficulty < 2)
                            {
                                ScrollAnim = 1;
                                ScrollDestination -= GraphicsDevice.Viewport.Width;
                                Difficulty++;
                                for (int i = 0; i < LevelScreenButtons.Count; i++)
                                {
                                    foreach (GameButton b in LevelScreenButtons[i])
                                    {
                                        if (b.Name == "Easy" || b.Name == "Medium" || b.Name == "Hard" || b.Name == "Back")
                                        {
                                            b.Position = new Vector2(-32, GraphicsDevice.Viewport.Height);
                                        }
                                    }
                                }
                                LevelScreenButtons[Difficulty].Find(b => b.Name == "Back").Position = new Vector2(50, GraphicsDevice.Viewport.Height - 200);
                                LevelScreenButtons[Difficulty].Find(b => b.Name == "Back").Scale = (float)GraphicsDevice.Viewport.Height / 1024f;
                            }
                            pressedButton.MouseOver = false;
                            pressedButton.Pressed = false;
                            pressedButton.WasPressed = false;
                        }
                        if (pressedButton.Name == "Previous")
                        {
                            if (Difficulty > 0)
                            {
                                ScrollAnim = 2;
                                ScrollDestination += GraphicsDevice.Viewport.Width;
                                Difficulty--;
                                for (int i = 0; i < LevelScreenButtons.Count; i++)
                                {
                                    foreach (GameButton b in LevelScreenButtons[i])
                                    {
                                        if (b.Name == "Easy" || b.Name == "Medium" || b.Name == "Hard" || b.Name == "Back")
                                        {
                                            b.Position = new Vector2(-32, GraphicsDevice.Viewport.Height);
                                        }
                                    }
                                }
                                LevelScreenButtons[Difficulty].Find(b => b.Name == "Back").Position = new Vector2(50, GraphicsDevice.Viewport.Height - 200);
                                LevelScreenButtons[Difficulty].Find(b => b.Name == "Back").Scale = (float)GraphicsDevice.Viewport.Height / 1024f;
                            }
                            pressedButton.MouseOver = false;
                            pressedButton.Pressed = false;
                            pressedButton.WasPressed = false;
                        }
                        if (pressedButton.Name == "Back")
                        {
                            GameState = "MainMenu";
                            ChosenLevel = -1;
                            LevelScreenButtons[Difficulty].Find(b => b.Name == "Easy").Position = new Vector2(-32, 0);
                            LevelScreenButtons[Difficulty].Find(b => b.Name == "Medium").Position = new Vector2(-32, 0);
                            LevelScreenButtons[Difficulty].Find(b => b.Name == "Hard").Position = new Vector2(-32, 0);
                            pressedButton.MouseOver = false;
                            pressedButton.Pressed = false;
                            pressedButton.WasPressed = false;
                        }
                        else
                        {
                            int charges = -1;
                            int time = -10;
                            if (Difficulty == 0)
                            {
                                switch (ChosenLevel)
                                {
                                    case 0:
                                        charges = 0;
                                        time = 120;
                                        break;
                                    case 1:
                                        charges = 0;
                                        time = 120;
                                        break;
                                    case 2:
                                        charges = 0;
                                        time = 120;
                                        break;
                                    case 3:
                                        charges = 0;
                                        time = 120;
                                        break;
                                    case 4:
                                        charges = 0;
                                        time = 120;
                                        break;
                                    case 5:
                                        charges = 0;
                                        time = 120;
                                        break;
                                    case 6:
                                        charges = 0;
                                        time = 120;
                                        break;
                                    case 7:
                                        charges = 2;
                                        time = 120;
                                        break;
                                    case 8:
                                        charges = 10;
                                        time = 120;
                                        break;
                                    case 9:
                                        charges = 6;
                                        time = 120;
                                        break;
                                    case 10:
                                        charges = 4;
                                        time = 120;
                                        break;
                                    case 11:
                                        charges = 5;
                                        time = 120;
                                        break;
                                    case 12:
                                        charges = 5;
                                        time = 120;
                                        break;
                                    case 13:
                                        charges = 12;
                                        time = 120;
                                        break;
                                    case 14:
                                        charges = 2;
                                        time = 120;
                                        break;
                                    case 15:
                                        charges = 7;
                                        time = 120;
                                        break;
                                    case 16:
                                        charges = 8;
                                        time = 120;
                                        break;
                                    case 17:
                                        charges = 7;
                                        time = 120;
                                        break;
                                    case 18:
                                        charges = 9;
                                        time = 120;
                                        break;
                                    case 19:
                                        charges = 5;
                                        time = 120;
                                        break;
                                    case 20:
                                        charges = 1;
                                        time = 120;
                                        break;
                                    case 21:
                                        charges = 8;
                                        time = 120;
                                        break;
                                    case 22:
                                        charges = 6;
                                        time = 120;
                                        break;
                                    case 23:
                                        charges = 11;
                                        time = 120;
                                        break;
                                    case 24:
                                        charges = 4;
                                        time = 120;
                                        break;
                                    case 25:
                                        charges = 4;
                                        time = 120;
                                        break;
                                }
                            }

                            if (Difficulty == 1)
                            {
                                switch (ChosenLevel)
                                {
                                    case 0:
                                        charges = 4;
                                        time = 120;
                                        break;
                                    case 1:
                                        charges = 8;
                                        time = 120;
                                        break;
                                    case 2:
                                        charges = 9;
                                        time = 120;
                                        break;
                                    case 3:
                                        charges = 14;
                                        time = 120;
                                        break;
                                    case 4:
                                        charges = 16;
                                        time = 120;
                                        break;
                                    case 5:
                                        charges = 10;
                                        time = 120;
                                        break;
                                    case 6:
                                        charges = 8;
                                        time = 120;
                                        break;
                                    case 7:
                                        charges = 22;
                                        time = 120;
                                        break;
                                    case 8:
                                        charges = 20;
                                        time = 120;
                                        break;
                                    case 9:
                                        charges = 14;
                                        time = 120;
                                        break;
                                    case 10:
                                        charges = 28;
                                        time = 120;
                                        break;
                                    case 11:
                                        charges = 6;
                                        time = 120;
                                        break;
                                    case 12:
                                        charges = 6;
                                        time = 120;
                                        break;
                                    case 13:
                                        charges = 14;
                                        time = 120;
                                        break;
                                    case 14:
                                        charges = 8;
                                        time = 120;
                                        break;
                                    case 15:
                                        charges = 0;
                                        time = 120;
                                        break;
                                }
                            }

                            if (Difficulty == 2)
                            {
                                switch (ChosenLevel)
                                {
                                    case 0:
                                        charges = 11;
                                        time = 120;
                                        break;
                                    case 1:
                                        charges = 3;
                                        time = 120;
                                        break;
                                    case 2:
                                        charges = 9;
                                        time = 120;
                                        break;
                                    case 3:
                                        charges = 13;
                                        time = 120;
                                        break;
                                    case 4:
                                        charges = 23;
                                        time = 120;
                                        break;
                                    case 5:
                                        charges = 7;
                                        time = 120;
                                        break;
                                    case 6:
                                        charges = 9;
                                        time = 120;
                                        break;
                                    case 7:
                                        charges = 18;
                                        time = 120;
                                        break;
                                    case 8:
                                        charges = 21;
                                        time = 120;
                                        break;
                                    case 9:
                                        charges = 10;
                                        time = 120;
                                        break;
                                    case 10:
                                        charges = 20;
                                        time = 120;
                                        break;
                                    case 11:
                                        charges = 100;
                                        time = 120;
                                        break;
                                    case 12:
                                        charges = 0;
                                        time = 120;
                                        break;
                                    case 13:
                                        charges = 4;
                                        time = 120;
                                        break;
                                    case 14:
                                        charges = 11;
                                        time = 120;
                                        break;
                                    case 15:
                                        charges = 100;
                                        time = 120;
                                        break;
                                }
                            }


                            if (pressedButton.Name != "Easy" && pressedButton.Name != "Medium" && pressedButton.Name != "Hard" && pressedButton.Name != "Next" && pressedButton.Name != "Previous")
                            {
                                ChosenLevel = Convert.ToInt32(pressedButton.Name);

                                easy.Position = pressedButton.Position;
                                easy.Position += new Vector2(0, 128) * pressedButton.Scale;
                                easy.Sprite = EasyButton;
                                if (Savegame.Played[Difficulty][ChosenLevel][0])
                                {
                                    easy.Sprite = EasyButtonGold;
                                }
                                medium.Position = pressedButton.Position;
                                medium.Position += new Vector2(48, 128) * pressedButton.Scale;
                                medium.Sprite = MediumButton;
                                if (Savegame.Played[Difficulty][ChosenLevel][1])
                                {
                                    medium.Sprite = MediumButtonGold;
                                }
                                hard.Position = pressedButton.Position;
                                hard.Position += new Vector2(96, 128) * pressedButton.Scale;
                                hard.Sprite = HardButton;
                                if (Savegame.Played[Difficulty][ChosenLevel][2])
                                {
                                    hard.Sprite = HardButtonGold;
                                }
                            }
                            else if (easy.WasPressed)
                            {
                                CurrentLevel = LoadLevel(LevelNames[Difficulty][ChosenLevel]);
                                UI = new Interface(GraphicsDevice, -1, Savegame.Diamonds, -10, Content);
                                GameState = "Game";
                                AndereDifficulty = 0;
                                if (ChosenLevel == 2)
                                {
                                    Trigger = -1;
                                }
                                if (ChosenLevel == 7)
                                {
                                    Trigger = 0;
                                }
                                if (ChosenLevel == 10)
                                {
                                    Trigger = 4;
                                }
                                if (ChosenLevel == 17)
                                {
                                    Trigger = 5;
                                }
                                if (ChosenLevel == 24)
                                {
                                    Trigger = 7;
                                }
                                if (ChosenLevel == 25)
                                {
                                    Trigger = 9;
                                }
                                pressedButton.MouseOver = false;
                                pressedButton.Pressed = false;
                                pressedButton.WasPressed = false;
                            }
                            else if (medium.WasPressed)
                            {
                                CurrentLevel = LoadLevel(LevelNames[Difficulty][ChosenLevel]);
                                UI = new Interface(GraphicsDevice, charges, Savegame.Diamonds, -10, Content);
                                GameState = "Game";
                                AndereDifficulty = 1;
                                if (ChosenLevel == 2)
                                {
                                    Trigger = -1;
                                }
                                if (ChosenLevel == 7)
                                {
                                    Trigger = 0;
                                }
                                if (ChosenLevel == 10)
                                {
                                    Trigger = 4;
                                }
                                if (ChosenLevel == 17)
                                {
                                    Trigger = 5;
                                }
                                if (ChosenLevel == 24)
                                {
                                    Trigger = 7;
                                }
                                if (ChosenLevel == 25)
                                {
                                    Trigger = 9;
                                }
                                pressedButton.MouseOver = false;
                                pressedButton.Pressed = false;
                                pressedButton.WasPressed = false;
                            }
                            else if (hard.WasPressed)
                            {
                                AndereDifficulty = 2;
                                CurrentLevel = LoadLevel(LevelNames[Difficulty][ChosenLevel]);
                                UI = new Interface(GraphicsDevice, charges, Savegame.Diamonds, time, Content);
                                GameState = "Game";
                                if (ChosenLevel == 2)
                                {
                                    Trigger = -1;
                                }
                                if (ChosenLevel == 7)
                                {
                                    Trigger = 0;
                                }
                                if (ChosenLevel == 10)
                                {
                                    Trigger = 4;
                                }
                                if (ChosenLevel == 17)
                                {
                                    Trigger = 5;
                                }
                                if (ChosenLevel == 24)
                                {
                                    Trigger = 7;
                                }
                                if (ChosenLevel == 25)
                                {
                                    Trigger = 9;
                                }
                                pressedButton.MouseOver = false;
                                pressedButton.Pressed = false;
                                pressedButton.WasPressed = false;
                            }
                        }
                    }
                }
                else if (GameState == "LevelEndScreen")
                {
                    foreach (GameButton b in LevelEndScreenButtons)
                    {
                        b.Update(SoundManager);
                    }

                    if (CoinToAnim > -1)
                    {
                        CoinAnim += gameTime.ElapsedGameTime.Milliseconds;

                        if (CoinAnim > 100)
                        {
                            CoinAnim = 0;
                            CoinAnimPhase++;
                            if (CoinAnimPhase > 3)
                            {
                                CoinAnimPhase = 0;
                                CoinToAnim++;
                                if (CoinToAnim > Savegame.Coins[Difficulty][ChosenLevel])
                                {
                                    CoinToAnim = -1;
                                }
                            }
                        }
                    }

                    GameButton pressedButton = LevelEndScreenButtons.Find(b => b.WasPressed);

                    if (pressedButton != null)
                    {
                        if (pressedButton.Name == "Next")
                        {
                            if (ChosenLevel < LevelNames[Difficulty].Length - 1)
                            {
                                ChosenLevel++;
                                CurrentLevel = LoadLevel(LevelNames[Difficulty][ChosenLevel]);
                                if (ChosenLevel == 2)
                                {
                                    Trigger = -1;
                                }
                                if (ChosenLevel == 7)
                                {
                                    Trigger = 0;
                                }
                                if (ChosenLevel == 10)
                                {
                                    Trigger = 4;
                                }
                                if (ChosenLevel == 17)
                                {
                                    Trigger = 5;
                                }
                                if (ChosenLevel == 24)
                                {
                                    Trigger = 7;
                                }
                                if (ChosenLevel == 25)
                                {
                                    Trigger = 9;
                                }
                                int charges = -1;
                                int time = -10;
                                if (Difficulty == 0)
                                {
                                    switch (ChosenLevel)
                                    {
                                        case 0:
                                            charges = 0;
                                            time = 120;
                                            break;
                                        case 1:
                                            charges = 0;
                                            time = 120;
                                            break;
                                        case 2:
                                            charges = 0;
                                            time = 120;
                                            break;
                                        case 3:
                                            charges = 0;
                                            time = 120;
                                            break;
                                        case 4:
                                            charges = 0;
                                            time = 120;
                                            break;
                                        case 5:
                                            charges = 0;
                                            time = 120;
                                            break;
                                        case 6:
                                            charges = 0;
                                            time = 120;
                                            break;
                                        case 7:
                                            charges = 2;
                                            time = 120;
                                            break;
                                        case 8:
                                            charges = 10;
                                            time = 120;
                                            break;
                                        case 9:
                                            charges = 6;
                                            time = 120;
                                            break;
                                        case 10:
                                            charges = 4;
                                            time = 120;
                                            break;
                                        case 11:
                                            charges = 5;
                                            time = 120;
                                            break;
                                        case 12:
                                            charges = 5;
                                            time = 120;
                                            break;
                                        case 13:
                                            charges = 12;
                                            time = 120;
                                            break;
                                        case 14:
                                            charges = 2;
                                            time = 120;
                                            break;
                                        case 15:
                                            charges = 7;
                                            time = 120;
                                            break;
                                        case 16:
                                            charges = 8;
                                            time = 120;
                                            break;
                                        case 17:
                                            charges = 7;
                                            time = 120;
                                            break;
                                        case 18:
                                            charges = 9;
                                            time = 120;
                                            break;
                                        case 19:
                                            charges = 5;
                                            time = 120;
                                            break;
                                        case 20:
                                            charges = 1;
                                            time = 120;
                                            break;
                                        case 21:
                                            charges = 8;
                                            time = 120;
                                            break;
                                        case 22:
                                            charges = 6;
                                            time = 120;
                                            break;
                                        case 23:
                                            charges = 11;
                                            time = 120;
                                            break;
                                        case 24:
                                            charges = 4;
                                            time = 120;
                                            break;
                                        case 25:
                                            charges = 4;
                                            time = 120;
                                            break;
                                    }
                                }

                                if (Difficulty == 1)
                                {
                                    switch (ChosenLevel)
                                    {
                                        case 0:
                                            charges = 4;
                                            time = 120;
                                            break;
                                        case 1:
                                            charges = 8;
                                            time = 120;
                                            break;
                                        case 2:
                                            charges = 9;
                                            time = 120;
                                            break;
                                        case 3:
                                            charges = 14;
                                            time = 120;
                                            break;
                                        case 4:
                                            charges = 16;
                                            time = 120;
                                            break;
                                        case 5:
                                            charges = 10;
                                            time = 120;
                                            break;
                                        case 6:
                                            charges = 8;
                                            time = 120;
                                            break;
                                        case 7:
                                            charges = 22;
                                            time = 120;
                                            break;
                                        case 8:
                                            charges = 20;
                                            time = 120;
                                            break;
                                        case 9:
                                            charges = 14;
                                            time = 120;
                                            break;
                                        case 10:
                                            charges = 28;
                                            time = 120;
                                            break;
                                        case 11:
                                            charges = 6;
                                            time = 120;
                                            break;
                                        case 12:
                                            charges = 6;
                                            time = 120;
                                            break;
                                        case 13:
                                            charges = 14;
                                            time = 120;
                                            break;
                                        case 14:
                                            charges = 8;
                                            time = 120;
                                            break;
                                        case 15:
                                            charges = 0;
                                            time = 120;
                                            break;
                                    }
                                }

                                if (Difficulty == 2)
                                {
                                    switch (ChosenLevel)
                                    {
                                        case 0:
                                            charges = 11;
                                            time = 120;
                                            break;
                                        case 1:
                                            charges = 3;
                                            time = 120;
                                            break;
                                        case 2:
                                            charges = 9;
                                            time = 120;
                                            break;
                                        case 3:
                                            charges = 13;
                                            time = 120;
                                            break;
                                        case 4:
                                            charges = 23;
                                            time = 120;
                                            break;
                                        case 5:
                                            charges = 7;
                                            time = 120;
                                            break;
                                        case 6:
                                            charges = 9;
                                            time = 120;
                                            break;
                                        case 7:
                                            charges = 18;
                                            time = 120;
                                            break;
                                        case 8:
                                            charges = 21;
                                            time = 120;
                                            break;
                                        case 9:
                                            charges = 10;
                                            time = 120;
                                            break;
                                        case 10:
                                            charges = 20;
                                            time = 120;
                                            break;
                                        case 11:
                                            charges = 100;
                                            time = 120;
                                            break;
                                        case 12:
                                            charges = 0;
                                            time = 120;
                                            break;
                                        case 13:
                                            charges = 4;
                                            time = 120;
                                            break;
                                        case 14:
                                            charges = 11;
                                            time = 120;
                                            break;
                                        case 15:
                                            charges = 100;
                                            time = 120;
                                            break;
                                    }
                                }
                                Player = null;
                                if (AndereDifficulty == 0)
                                {
                                    UI = new Interface(GraphicsDevice, -1, Savegame.Diamonds, -10, Content);
                                }
                                if (AndereDifficulty == 1)
                                {
                                    UI = new Interface(GraphicsDevice, charges, Savegame.Diamonds, -10, Content);
                                }
                                if (AndereDifficulty == 2)
                                {
                                    UI = new Interface(GraphicsDevice, charges, Savegame.Diamonds, time, Content);
                                }
                                GameState = "Game";
                            }
                            else
                            {
                                Player = null;
                                UI = null;
                                GameState = "MainMenu";
                            }
                            pressedButton.MouseOver = false;
                            pressedButton.Pressed = false;
                            pressedButton.WasPressed = false;
                            Finished = false;
                        }

                        if (pressedButton.Name == "Replay")
                        {
                            GameState = "Game";
                            UI.Reset();
                            Player = null;
                            CurrentLevel = LoadLevel(LevelNames[Difficulty][ChosenLevel]);
                            pressedButton.MouseOver = false;
                            pressedButton.Pressed = false;
                            pressedButton.WasPressed = false;
                            Finished = false;
                            if (ChosenLevel == 2)
                            {
                                Trigger = -1;
                            }
                            if (ChosenLevel == 7)
                            {
                                Trigger = 0;
                            }
                            if (ChosenLevel == 10)
                            {
                                Trigger = 4;
                            }
                            if (ChosenLevel == 17)
                            {
                                Trigger = 5;
                            }
                            if (ChosenLevel == 23)
                            {
                                Trigger = 7;
                            }
                            if (ChosenLevel == 24)
                            {
                                Trigger = 9;
                            }
                        }

                        if (pressedButton.Name == "Back")
                        {
                            GameState = "MainMenu";
                            Player = null;
                            UI = null;
                            pressedButton.MouseOver = false;
                            pressedButton.Pressed = false;
                            pressedButton.WasPressed = false;
                            Finished = false;
                            ChosenLevel = -1;
                        }
                    }
                }
            }

            if (keyState.IsKeyUp(Keys.Escape))
            {
                KeyPressed = false;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            MouseState mouseState = Mouse.GetState();

            if (Player != null && (GameState == "Game" | GameState == "Pause"))
            {
                if (FXManager.Index == -1)
                {
                    List<GameObject> objects = CurrentLevel.Past;
                    if (Player.Time == 1)
                    {
                        objects = CurrentLevel.Present;
                    }
                    else if (Player.Time == 2)
                    {
                        objects = CurrentLevel.Future;
                    }
                    Texture2D bg = CurrentLevel.PastBackground;
                    if (Player.Time == 1)
                    {
                        bg = CurrentLevel.PresentBackground;
                    }
                    else if (Player.Time == 2)
                    {
                        bg = CurrentLevel.FutureBackground;
                    }

                    GraphicsDevice.SetRenderTarget(wallRender);
                    GraphicsDevice.Clear(Color.Transparent);
                    spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
                    foreach (GameObject o in objects)
                    {
                        if (o.Type == 0)
                        {
                            o.Draw(false, spriteBatch);
                        }
                    }
                    spriteBatch.End();

                    GraphicsDevice.SetRenderTarget(windowRender);
                    GraphicsDevice.Clear(Color.Transparent);
                    spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
                    foreach (GameObject o in objects)
                    {
                        if (o.Type == 14)
                        {
                            o.Draw(false, spriteBatch);
                        }
                    }
                    spriteBatch.End();


                    GraphicsDevice.SetRenderTarget(null);
                    GraphicsDevice.Clear(Color.Black);

                    spriteBatch.Begin(SpriteSortMode.BackToFront, null, null, null, null, null, Cam.GetMatrix());
                    spriteBatch.Draw(bg, Vector2.Zero, Color.White);
                    spriteBatch.End();

                    spriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, null, Cam.GetMatrix());
                    WindowEffect.Parameters["windows"].SetValue(windowRender);
                    WindowEffect.CurrentTechnique.Passes[0].Apply();
                    spriteBatch.Draw(wallRender, Vector2.Zero, Color.White);
                    spriteBatch.End();

                    spriteBatch.Begin(SpriteSortMode.BackToFront, null, null, null, null, null, Cam.GetMatrix());
                    foreach (GameObject o in objects)
                    {
                        if (o.Type > 0 && o.Type < 16)
                        {
                            o.Draw(false, spriteBatch);
                        }
                    }

                    Player.Draw(spriteBatch);
                    spriteBatch.End();
                }
                else if (FXManager.Index == 0)
                {
                    List<GameObject> objects = CurrentLevel.Past;
                    if (Player.Time == 1)
                    {
                        objects = CurrentLevel.Present;
                    }
                    else if (Player.Time == 2)
                    {
                        objects = CurrentLevel.Future;
                    }
                    Texture2D bg = CurrentLevel.PastBackground;
                    if (Player.Time == 1)
                    {
                        bg = CurrentLevel.PresentBackground;
                    }
                    else if (Player.Time == 2)
                    {
                        bg = CurrentLevel.FutureBackground;
                    }

                    GraphicsDevice.SetRenderTarget(wallRender);
                    GraphicsDevice.Clear(Color.Transparent);
                    spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
                    foreach (GameObject o in objects)
                    {
                        if (o.Type == 0)
                        {
                            o.Draw(false, spriteBatch);
                        }
                    }
                    spriteBatch.End();

                    GraphicsDevice.SetRenderTarget(windowRender);
                    GraphicsDevice.Clear(Color.Transparent);
                    spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
                    foreach (GameObject o in objects)
                    {
                        if (o.Type == 14)
                        {
                            o.Draw(false, spriteBatch);
                        }
                    }
                    spriteBatch.End();


                    GraphicsDevice.SetRenderTarget(null);
                    GraphicsDevice.Clear(Color.Black);

                    spriteBatch.Begin(SpriteSortMode.BackToFront, null, null, null, null, null, Cam.GetMatrix());
                    spriteBatch.Draw(bg, Vector2.Zero, Color.White);
                    spriteBatch.End();

                    spriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, null, Cam.GetMatrix());
                    WindowEffect.Parameters["windows"].SetValue(windowRender);
                    WindowEffect.CurrentTechnique.Passes[0].Apply();
                    spriteBatch.Draw(wallRender, Vector2.Zero, Color.White);
                    spriteBatch.End();

                    spriteBatch.Begin(SpriteSortMode.BackToFront, null, null, null, null, null, Cam.GetMatrix());
                    foreach (GameObject o in objects)
                    {
                        if (o.Type > 0 && o.Type < 16)
                        {
                            o.Draw(false, spriteBatch);
                        }
                    }

                    Player.Draw(spriteBatch);
                    spriteBatch.End();

                    spriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, null, Cam.GetMatrix());
                    FXManager.ApplyEffect();
                    FXManager.PortObject.Draw(false, spriteBatch);
                    spriteBatch.End();
                }
                else if (FXManager.Index == 1)
                {
                    List<GameObject> objects = CurrentLevel.Past;
                    if (Player.Time == 1)
                    {
                        objects = CurrentLevel.Present;
                    }
                    else if (Player.Time == 2)
                    {
                        objects = CurrentLevel.Future;
                    }
                    Texture2D bg = CurrentLevel.PastBackground;
                    if (Player.Time == 1)
                    {
                        bg = CurrentLevel.PresentBackground;
                    }
                    else if (Player.Time == 2)
                    {
                        bg = CurrentLevel.FutureBackground;
                    }

                    GraphicsDevice.SetRenderTarget(wallRender);
                    GraphicsDevice.Clear(Color.Transparent);
                    spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
                    foreach (GameObject o in objects)
                    {
                        if (o.Type == 0)
                        {
                            o.Draw(false, spriteBatch);
                        }
                    }
                    spriteBatch.End();

                    GraphicsDevice.SetRenderTarget(windowRender);
                    GraphicsDevice.Clear(Color.Transparent);
                    spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
                    foreach (GameObject o in objects)
                    {
                        if (o.Type == 14)
                        {
                            o.Draw(false, spriteBatch);
                        }
                    }
                    spriteBatch.End();


                    GraphicsDevice.SetRenderTarget(renderTarget);
                    GraphicsDevice.Clear(Color.Black);

                    spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
                    spriteBatch.Draw(bg, Vector2.Zero, Color.White);
                    spriteBatch.End();

                    spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
                    WindowEffect.Parameters["windows"].SetValue(windowRender);
                    WindowEffect.CurrentTechnique.Passes[0].Apply();
                    spriteBatch.Draw(wallRender, Vector2.Zero, Color.White);
                    spriteBatch.End();

                    spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
                    foreach (GameObject o in objects)
                    {
                        if (o.Type > 0 && o.Type < 16)
                        {
                            o.Draw(false, spriteBatch);
                        }
                    }

                    Player.Draw(spriteBatch);
                    spriteBatch.End();


                    if (FXManager.Speed < 0)
                    {
                        GraphicsDevice.SetRenderTarget(null);
                        GraphicsDevice.Clear(Color.Black);

                        spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, null, Cam.GetMatrix());
                        FXManager.ApplyEffect();
                        spriteBatch.Draw(renderTarget, Vector2.Zero, Color.White);
                        spriteBatch.End();
                    }

                    if (FXManager.Speed > 0)
                    {
                        objects = CurrentLevel.Past;
                        if (FXManager.NewTime == 1)
                        {
                            objects = CurrentLevel.Present;
                        }
                        else if (FXManager.NewTime == 2)
                        {
                            objects = CurrentLevel.Future;
                        }
                        bg = CurrentLevel.PastBackground;
                        if (FXManager.NewTime == 1)
                        {
                            bg = CurrentLevel.PresentBackground;
                        }
                        else if (FXManager.NewTime == 2)
                        {
                            bg = CurrentLevel.FutureBackground;
                        }

                        GraphicsDevice.SetRenderTarget(wallRender);
                        GraphicsDevice.Clear(Color.Transparent);
                        spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
                        foreach (GameObject o in objects)
                        {
                            if (o.Type == 0)
                            {
                                o.Draw(false, spriteBatch);
                            }
                        }
                        spriteBatch.End();

                        GraphicsDevice.SetRenderTarget(windowRender);
                        GraphicsDevice.Clear(Color.Transparent);
                        spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
                        foreach (GameObject o in objects)
                        {
                            if (o.Type == 14)
                            {
                                o.Draw(false, spriteBatch);
                            }
                        }
                        spriteBatch.End();


                        GraphicsDevice.SetRenderTarget(bgRender);
                        GraphicsDevice.Clear(Color.Black);

                        spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
                        spriteBatch.Draw(bg, Vector2.Zero, Color.White);
                        spriteBatch.End();

                        spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
                        WindowEffect.Parameters["windows"].SetValue(windowRender);
                        WindowEffect.CurrentTechnique.Passes[0].Apply();
                        spriteBatch.Draw(wallRender, Vector2.Zero, Color.White);
                        spriteBatch.End();

                        spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
                        foreach (GameObject o in objects)
                        {
                            if (o.Type > 0 && o.Type < 16)
                            {
                                o.Draw(false, spriteBatch);
                            }
                        }

                        Player.Draw(spriteBatch);
                        spriteBatch.End();


                        GraphicsDevice.SetRenderTarget(null);
                        GraphicsDevice.Clear(Color.Black);

                        spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, null, Cam.GetMatrix());
                        FXManager.SetBG(bgRender);
                        FXManager.ApplyEffect();
                        spriteBatch.Draw(renderTarget, Vector2.Zero, Color.White);

                        spriteBatch.End();
                    }
                }
                else if (FXManager.Index == 2)
                {
                    List<GameObject> objects = CurrentLevel.Past;
                    if (Player.Time == 1)
                    {
                        objects = CurrentLevel.Present;
                    }
                    else if (Player.Time == 2)
                    {
                        objects = CurrentLevel.Future;
                    }
                    Texture2D bg = CurrentLevel.PastBackground;
                    if (Player.Time == 1)
                    {
                        bg = CurrentLevel.PresentBackground;
                    }
                    else if (Player.Time == 2)
                    {
                        bg = CurrentLevel.FutureBackground;
                    }

                    RenderTarget2D playerRender = new RenderTarget2D(GraphicsDevice, 128, 128);
                    GraphicsDevice.SetRenderTarget(playerRender);
                    GraphicsDevice.Clear(Color.Transparent);
                    spriteBatch.Begin();
                    spriteBatch.Draw(Player.Sprite, new Vector2(64, 64), null, Color.White, 0f, new Vector2(32, 32), 1f, SpriteEffects.None, 0f);
                    spriteBatch.End();

                    GraphicsDevice.SetRenderTarget(wallRender);
                    GraphicsDevice.Clear(Color.Transparent);
                    spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
                    foreach (GameObject o in objects)
                    {
                        if (o.Type == 0)
                        {
                            o.Draw(false, spriteBatch);
                        }
                    }
                    spriteBatch.End();

                    GraphicsDevice.SetRenderTarget(windowRender);
                    GraphicsDevice.Clear(Color.Transparent);
                    spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
                    foreach (GameObject o in objects)
                    {
                        if (o.Type == 14)
                        {
                            o.Draw(false, spriteBatch);
                        }
                    }
                    spriteBatch.End();


                    GraphicsDevice.SetRenderTarget(null);
                    GraphicsDevice.Clear(Color.Black);

                    spriteBatch.Begin(SpriteSortMode.BackToFront, null, null, null, null, null, Cam.GetMatrix());
                    spriteBatch.Draw(bg, Vector2.Zero, Color.White);
                    spriteBatch.End();

                    spriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, null, Cam.GetMatrix());
                    WindowEffect.Parameters["windows"].SetValue(windowRender);
                    WindowEffect.CurrentTechnique.Passes[0].Apply();
                    spriteBatch.Draw(wallRender, Vector2.Zero, Color.White);
                    spriteBatch.End();

                    spriteBatch.Begin(SpriteSortMode.BackToFront, null, null, null, null, null, Cam.GetMatrix());
                    foreach (GameObject o in objects)
                    {
                        if (o.Type > 0 && o.Type < 16)
                        {
                            o.Draw(false, spriteBatch);
                        }
                    }

                    spriteBatch.End();

                    spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, null, Cam.GetMatrix());
                    FXManager.ApplyEffect();
                    spriteBatch.Draw(playerRender, Player.Position, null, Color.White, 0f, new Vector2(64, 64), 1f, SpriteEffects.None, 0f);
                    spriteBatch.End();
                }
                else if (FXManager.Index == 3)
                {
                    List<GameObject> objects = CurrentLevel.Past;
                    if (Player.Time == 1)
                    {
                        objects = CurrentLevel.Present;
                    }
                    else if (Player.Time == 2)
                    {
                        objects = CurrentLevel.Future;
                    }
                    Texture2D bg = CurrentLevel.PastBackground;
                    if (Player.Time == 1)
                    {
                        bg = CurrentLevel.PresentBackground;
                    }
                    else if (Player.Time == 2)
                    {
                        bg = CurrentLevel.FutureBackground;
                    }


                    GraphicsDevice.SetRenderTarget(wallRender);
                    GraphicsDevice.Clear(Color.Transparent);
                    spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
                    foreach (GameObject o in objects)
                    {
                        if (o.Type == 0)
                        {
                            o.Draw(false, spriteBatch);
                        }
                    }
                    spriteBatch.End();

                    GraphicsDevice.SetRenderTarget(windowRender);
                    GraphicsDevice.Clear(Color.Transparent);
                    spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
                    foreach (GameObject o in objects)
                    {
                        if (o.Type == 14)
                        {
                            o.Draw(false, spriteBatch);
                        }
                    }
                    spriteBatch.End();


                    GraphicsDevice.SetRenderTarget(renderTarget);
                    GraphicsDevice.Clear(Color.Black);

                    spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
                    spriteBatch.Draw(bg, Vector2.Zero, Color.White);
                    spriteBatch.End();

                    spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
                    WindowEffect.Parameters["windows"].SetValue(windowRender);
                    WindowEffect.CurrentTechnique.Passes[0].Apply();
                    spriteBatch.Draw(wallRender, Vector2.Zero, Color.White);
                    spriteBatch.End();

                    spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
                    foreach (GameObject o in objects)
                    {
                        if (o.Type > 0 && o.Type < 16)
                        {
                            o.Draw(false, spriteBatch);
                        }
                    }

                    Player.Draw(spriteBatch);
                    spriteBatch.End();


                    objects = CurrentLevel.Past;
                    if (FXManager.NewTime == 1)
                    {
                        objects = CurrentLevel.Present;
                    }
                    else if (FXManager.NewTime == 2)
                    {
                        objects = CurrentLevel.Future;
                    }
                    bg = CurrentLevel.PastBackground;
                    if (FXManager.NewTime == 1)
                    {
                        bg = CurrentLevel.PresentBackground;
                    }
                    else if (FXManager.NewTime == 2)
                    {
                        bg = CurrentLevel.FutureBackground;
                    }

                    GraphicsDevice.SetRenderTarget(wallRender);
                    GraphicsDevice.Clear(Color.Transparent);
                    spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
                    foreach (GameObject o in objects)
                    {
                        if (o.Type == 0)
                        {
                            o.Draw(false, spriteBatch);
                        }
                    }
                    spriteBatch.End();

                    GraphicsDevice.SetRenderTarget(windowRender);
                    GraphicsDevice.Clear(Color.Transparent);
                    spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
                    foreach (GameObject o in objects)
                    {
                        if (o.Type == 14)
                        {
                            o.Draw(false, spriteBatch);
                        }
                    }
                    spriteBatch.End();


                    GraphicsDevice.SetRenderTarget(bgRender);
                    GraphicsDevice.Clear(Color.Black);

                    spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
                    spriteBatch.Draw(bg, Vector2.Zero, Color.White);
                    spriteBatch.End();

                    spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
                    WindowEffect.Parameters["windows"].SetValue(windowRender);
                    WindowEffect.CurrentTechnique.Passes[0].Apply();
                    spriteBatch.Draw(wallRender, Vector2.Zero, Color.White);
                    spriteBatch.End();

                    spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
                    foreach (GameObject o in objects)
                    {
                        if (o.Type > 0 && o.Type < 16)
                        {
                            o.Draw(false, spriteBatch);
                        }
                    }

                    Player.Draw(spriteBatch);
                    spriteBatch.End();


                    GraphicsDevice.SetRenderTarget(null);
                    GraphicsDevice.Clear(Color.Black);

                    spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, null, Cam.GetMatrix());
                    FXManager.SetBG(bgRender);
                    FXManager.ApplyEffect();
                    spriteBatch.Draw(renderTarget, Vector2.Zero, Color.White);

                    spriteBatch.End();
                }

                if (ChosenLevel == 7 && Difficulty == 0)
                {
                    if (CSPosFuck.Y > 0)
                    {
                        spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, null, null, null, null, Cam.GetMatrix());
                        spriteBatch.Draw(CSPics[CSAP], CSPosFuck, null, Color.White, 0f, new Vector2(32, 32), 1f, SpriteEffects.FlipHorizontally, 0f);
                        spriteBatch.End();
                    }
                }

                if (TriggerActivated)
                {
                    spriteBatch.Begin();
                    List<Texture2D> pic = TriggerPics[Trigger + Options.ControlType * 12];
                    for (int i = 0; i < pic.Count; i++)
                    {
                        if (TriggerProps[Trigger][i].X > -1 && TriggerProps[Trigger][i].Y > -1)
                        {
                            spriteBatch.Draw(pic[i], new Vector2(TriggerProps[Trigger][i].X + Cam.Position.X, TriggerProps[Trigger][i].Y + Cam.Position.Y), null, Color.White, 0f, new Vector2(pic[i].Width / 2, pic[i].Height / 2), 1f, SpriteEffects.None, 0f);
                        }
                        else
                        {
                            spriteBatch.Draw(pic[i], new Vector2(Player.Position.X + Cam.Position.X + 40 * i, Player.Position.Y + Cam.Position.Y - pic[i].Height - 20), null, Color.White, 0f, new Vector2(pic[i].Width / 2, pic[i].Height / 2), 1f, SpriteEffects.None, 0f);
                        }
                    }
                    spriteBatch.End();
                }

                UI.Draw(spriteBatch, Player, Options.ControlType, Cam, GraphicsDevice);

                if (GameState == "Pause")
                {
                    spriteBatch.Begin();
                    foreach (GameButton b in PauseMenuButtons)
                    {
                        b.Draw(spriteBatch);
                    }
                    spriteBatch.End();
                    spriteBatch.Begin();
                    spriteBatch.Draw(SwordNormal, new Vector2(mouseState.X, mouseState.Y), null, Color.White, MathHelper.ToRadians(-45), new Vector2(16, 0), 1f, SpriteEffects.None, 0f);
                    spriteBatch.End();
                }
            }
            else if (GameState == "Intro")
            {
                float Scale = (float)GraphicsDevice.Viewport.Height / 1024f;
                spriteBatch.Begin();
                if (ga)
                {
                    spriteBatch.Draw(GALogo, new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2), null, Color.White * Fade, 0f, new Vector2(GALogo.Width / 2, GALogo.Height / 2), Scale, SpriteEffects.None, 0f);
                }
                if (it)
                {
                    GraphicsDevice.Clear(Color.Gray);
                    spriteBatch.Draw(InTeamLogo, new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2), null, Color.White * Fade, 0f, new Vector2(InTeamLogo.Width / 2, InTeamLogo.Height / 2), Scale, SpriteEffects.None, 0f);
                }
                spriteBatch.End();
            }
            else if (GameState == "MainMenu")
            {
                spriteBatch.Begin();
                spriteBatch.Draw(Background, new Vector2(((2048 - GraphicsDevice.Viewport.Width) / 3) * (LevelScreenScroll.X / GraphicsDevice.Viewport.Width), -GraphicsDevice.Viewport.Height + ((2048 - GraphicsDevice.Viewport.Height) / 3) * (LevelScreenScroll.Y / GraphicsDevice.Viewport.Height)), Color.White);
                spriteBatch.End();

                spriteBatch.Begin();
                spriteBatch.Draw(Shield, new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2), null, Color.White, 0f, new Vector2(Shield.Width / 2, Shield.Height / 2), 0.5f, SpriteEffects.None, 0f);
                spriteBatch.End();

                spriteBatch.Begin();
                foreach (Vector2 c in Coins)
                {
                    spriteBatch.Draw(Coin, c, null, Color.White, 0f, Vector2.Zero, 0.5f, SpriteEffects.None, 0f);
                }
                spriteBatch.End();

                spriteBatch.Begin();
                foreach (GameButton b in MainMenuButtons)
                {
                    b.Draw(spriteBatch);
                }
                spriteBatch.End();

                spriteBatch.Begin();
                spriteBatch.Draw(SwordNormal, new Vector2(mouseState.X, mouseState.Y), null, Color.White, MathHelper.ToRadians(-45), new Vector2(16, 0), 1f, SpriteEffects.None, 0f);
                spriteBatch.End();
            }
            else if (GameState == "OptionsMenu")
            {
                spriteBatch.Begin();
                spriteBatch.Draw(Background, new Vector2(((2048 - GraphicsDevice.Viewport.Width) / 3) * (LevelScreenScroll.X / GraphicsDevice.Viewport.Width), -GraphicsDevice.Viewport.Height + ((2048 - GraphicsDevice.Viewport.Height) / 3) * (LevelScreenScroll.Y / GraphicsDevice.Viewport.Height)), Color.White);
                spriteBatch.End();

                spriteBatch.Begin();
                foreach (GameButton b in OptionsMenuButtons)
                {
                    b.Draw(spriteBatch);
                }
                spriteBatch.End();
                spriteBatch.Begin();
                spriteBatch.Draw(SwordNormal, new Vector2(mouseState.X, mouseState.Y), null, Color.White, MathHelper.ToRadians(-45), new Vector2(16, 0), 1f, SpriteEffects.None, 0f);
                spriteBatch.End();
            }
            else if (GameState == "ControlsMenu")
            {
                spriteBatch.Begin();
                spriteBatch.Draw(Background, new Vector2(((2048 - GraphicsDevice.Viewport.Width) / 3) * (LevelScreenScroll.X / GraphicsDevice.Viewport.Width), -GraphicsDevice.Viewport.Height + ((2048 - GraphicsDevice.Viewport.Height) / 3) * (LevelScreenScroll.Y / GraphicsDevice.Viewport.Height)), Color.White);
                spriteBatch.End();

                spriteBatch.Begin();
                foreach (GameButton b in ControlsMenuButtons)
                {
                    b.Draw(spriteBatch);
                }

                float ScaleMult = GraphicsDevice.Viewport.Height / 1024f;
                if (Options.ControlType == 0)
                {
                    spriteBatch.Draw(KeyboardPic, new Vector2(GraphicsDevice.Viewport.Width / 2 - 100 * ScaleMult, GraphicsDevice.Viewport.Height / 2), null, Color.White, 0f, new Vector2(KeyboardPic.Width / 2, KeyboardPic.Height / 2), 0.4f * ScaleMult, SpriteEffects.None, 0f);
                    spriteBatch.Draw(MousePic, new Vector2(GraphicsDevice.Viewport.Width / 2 + 400 * ScaleMult, GraphicsDevice.Viewport.Height / 2), null, Color.White, 0f, new Vector2(MousePic.Width / 2, MousePic.Height / 2), 0.5f * ScaleMult, SpriteEffects.None, 0f);
                }
                else
                {
                    spriteBatch.Draw(ControllerPic, new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2), null, Color.White, 0f, new Vector2(ControllerPic.Width / 2, ControllerPic.Height / 2), 0.5f * ScaleMult, SpriteEffects.None, 0f);
                }

                spriteBatch.End();

                spriteBatch.Begin();
                for (int i = 0; i < ControlsAnimations.Count; i++)
                {
                    int klischeebanditenterritoriumsexpansionseinheitenschwarmintelligenztoaster = 0;
                    int wanderbienenextraktorenkatalysatorhaus = 0;
                    if (Options.ControlType == 0)
                    {
                        if (i == 0)
                        {
                            klischeebanditenterritoriumsexpansionseinheitenschwarmintelligenztoaster = 13;
                            wanderbienenextraktorenkatalysatorhaus = 320;
                        }
                        if (i == 1)
                        {
                            klischeebanditenterritoriumsexpansionseinheitenschwarmintelligenztoaster = 40;
                            wanderbienenextraktorenkatalysatorhaus = 280;
                        }
                        if (i == 2)
                        {
                            klischeebanditenterritoriumsexpansionseinheitenschwarmintelligenztoaster = 110;
                            wanderbienenextraktorenkatalysatorhaus = 420;
                        }
                        if (i == 3)
                        {
                            klischeebanditenterritoriumsexpansionseinheitenschwarmintelligenztoaster = 100;
                            wanderbienenextraktorenkatalysatorhaus = 360;
                        }
                        if (i == 4)
                        {
                            klischeebanditenterritoriumsexpansionseinheitenschwarmintelligenztoaster = 90;
                            wanderbienenextraktorenkatalysatorhaus = 300;
                        }
                        if (i == 5)
                        {
                            klischeebanditenterritoriumsexpansionseinheitenschwarmintelligenztoaster = 70;
                            wanderbienenextraktorenkatalysatorhaus = -375;
                        }
                        if (i == 6)
                        {
                            klischeebanditenterritoriumsexpansionseinheitenschwarmintelligenztoaster = 70;
                            wanderbienenextraktorenkatalysatorhaus = -375;
                        }
                        if (i == 7)
                        {
                            klischeebanditenterritoriumsexpansionseinheitenschwarmintelligenztoaster = 70;
                            wanderbienenextraktorenkatalysatorhaus = -420;
                        }
                    }
                    else
                    {
                        if (i == 0)
                        {
                            klischeebanditenterritoriumsexpansionseinheitenschwarmintelligenztoaster = -25;
                            wanderbienenextraktorenkatalysatorhaus = 176;
                        }
                        if (i == 1)
                        {
                            klischeebanditenterritoriumsexpansionseinheitenschwarmintelligenztoaster = -38;
                            wanderbienenextraktorenkatalysatorhaus = -175;
                        }
                        if (i == 2)
                        {
                            klischeebanditenterritoriumsexpansionseinheitenschwarmintelligenztoaster = 0;
                            wanderbienenextraktorenkatalysatorhaus = -128;
                        }
                        if (i == 3)
                        {
                            klischeebanditenterritoriumsexpansionseinheitenschwarmintelligenztoaster = 40;
                            wanderbienenextraktorenkatalysatorhaus = -180;
                        }
                        if (i == 4)
                        {
                            klischeebanditenterritoriumsexpansionseinheitenschwarmintelligenztoaster = 6;
                            wanderbienenextraktorenkatalysatorhaus = -224;
                        }
                        if (i == 5)
                        {
                            klischeebanditenterritoriumsexpansionseinheitenschwarmintelligenztoaster = 160;
                            wanderbienenextraktorenkatalysatorhaus = 160;
                        }
                        if (i == 6)
                        {
                            klischeebanditenterritoriumsexpansionseinheitenschwarmintelligenztoaster = 150;
                            wanderbienenextraktorenkatalysatorhaus = -160;
                        }
                        if (i == 7)
                        {
                            klischeebanditenterritoriumsexpansionseinheitenschwarmintelligenztoaster = 106;
                            wanderbienenextraktorenkatalysatorhaus = 192;
                        }
                    }
                    Vector2 rindfleischextasenposition = new Vector2((GraphicsDevice.Viewport.Width / ControlsAnimations.Count) * i + (GraphicsDevice.Viewport.Width / ControlsAnimations.Count) / 2, 100);
                    spriteBatch.Draw(ControlsAnimations[i][(int)ControlsTimers[i].Y], rindfleischextasenposition, null, Color.White, 0f, new Vector2(ControlsAnimations[i][(int)ControlsTimers[i].Y].Width / 2, ControlsAnimations[i][(int)ControlsTimers[i].Y].Height / 2), 1f, SpriteEffects.None, 0f);
                    int kavierkraftwerk = 32;
                    while (kavierkraftwerk < ((GraphicsDevice.Viewport.Height / 2 - klischeebanditenterritoriumsexpansionseinheitenschwarmintelligenztoaster * ScaleMult) - rindfleischextasenposition.Y) / 2)
                    {
                        spriteBatch.Draw(Dot, rindfleischextasenposition + new Vector2(0, kavierkraftwerk), Color.White);
                        kavierkraftwerk++;
                    }

                    int pyromantenbrot = 1;
                    if ((GraphicsDevice.Viewport.Width / 2 - wanderbienenextraktorenkatalysatorhaus * ScaleMult) - rindfleischextasenposition.X < 0)
                    {
                        pyromantenbrot = -1;
                    }
                    rindfleischextasenposition += new Vector2(0, kavierkraftwerk);
                    kavierkraftwerk = 0;
                    while (kavierkraftwerk < Math.Abs((GraphicsDevice.Viewport.Width / 2 - wanderbienenextraktorenkatalysatorhaus * ScaleMult) - rindfleischextasenposition.X))
                    {
                        spriteBatch.Draw(Dot, rindfleischextasenposition + new Vector2(kavierkraftwerk, 0) * pyromantenbrot, Color.White);
                        kavierkraftwerk++;
                    }

                    rindfleischextasenposition += new Vector2(kavierkraftwerk, 0) * pyromantenbrot;
                    kavierkraftwerk = 0;
                    while (kavierkraftwerk < (GraphicsDevice.Viewport.Height / 2 - klischeebanditenterritoriumsexpansionseinheitenschwarmintelligenztoaster * ScaleMult) - rindfleischextasenposition.Y)
                    {
                        spriteBatch.Draw(Dot, rindfleischextasenposition + new Vector2(0, kavierkraftwerk), Color.White);
                        kavierkraftwerk++;
                    }
                }
                spriteBatch.End();

                spriteBatch.Begin();
                spriteBatch.Draw(SwordNormal, new Vector2(mouseState.X, mouseState.Y), null, Color.White, MathHelper.ToRadians(-45), new Vector2(16, 0), 1f, SpriteEffects.None, 0f);
                spriteBatch.End();
            }
            else if (GameState == "LevelScreen")
            {
                spriteBatch.Begin();
                spriteBatch.Draw(Background, new Vector2(((2048 - GraphicsDevice.Viewport.Width) / 3) * (LevelScreenScroll.X / GraphicsDevice.Viewport.Width), -GraphicsDevice.Viewport.Height + ((2048 - GraphicsDevice.Viewport.Height) / 3) * (LevelScreenScroll.Y / GraphicsDevice.Viewport.Height)), Color.White);
                spriteBatch.End();

                spriteBatch.Begin();
                float scale = (float)GraphicsDevice.Viewport.Height / 1024f;
                for (int j = 0; j < LevelNames.Count; j++)
                {
                    int off = j * GraphicsDevice.Viewport.Width;
                    for (int i = 0; i < LevelNames[j].Length; i += 5)
                    {
                        spriteBatch.Draw(CastleElement, new Vector2(GraphicsDevice.Viewport.Width / 2 + 85 + off, GraphicsDevice.Viewport.Height - 128 * scale - (i / 5) * 256 * scale) + LevelScreenScroll, null, Color.White, 0f, new Vector2(CastleElement.Width / 2, CastleElement.Height / 2), scale, SpriteEffects.None, 1f);
                    }
                    spriteBatch.Draw(CastleTop, new Vector2(GraphicsDevice.Viewport.Width / 2 + 85 + off, GraphicsDevice.Viewport.Height - 384 * scale - ((LevelNames[j].Length - 1) / 5) * 256 * scale) + LevelScreenScroll, null, Color.White, 0f, new Vector2(CastleTop.Width / 2, CastleTop.Height / 2), scale, SpriteEffects.None, 1f);

                    foreach (GameButton b in LevelScreenButtons[j])
                    {
                        b.Draw(spriteBatch);
                        if (b.Name != "Back" && b.Name != "Easy" && b.Name != "Medium" && b.Name != "Hard" && b.Name != "Next" && b.Name != "Previous")
                        {
                            if (Convert.ToInt32(b.Name) < Savegame.Unlocked[j])
                            {
                                spriteBatch.Draw(LevelPics[j][Convert.ToInt32(b.Name)], b.Position + new Vector2(10 * scale, 10 * scale), null, Color.White, 0f, Vector2.Zero, 0.1f * b.Scale, SpriteEffects.None, 0f);
                                float correct = 0;
                                if (maxCoins[j][Convert.ToInt32(b.Name)] % 2 == 0)
                                {
                                    correct = (((b.Sprite.Width - SmallCoinEmpty.Width) * b.Scale) / (float)maxCoins[j][Convert.ToInt32(b.Name)]) / 2f;
                                }
                                for (int i = -(int)Math.Floor((double)maxCoins[j][Convert.ToInt32(b.Name)] / 2.0); i <= (int)Math.Floor((double)maxCoins[j][Convert.ToInt32(b.Name)] / 2.0); i++)
                                {
                                    float correctMult = correct;
                                    if (i > 0)
                                    {
                                        correctMult *= -1;
                                    }
                                    if ((maxCoins[j][Convert.ToInt32(b.Name)] % 2 == 0 && i != 0) || (maxCoins[j][Convert.ToInt32(b.Name)] % 2 != 0))
                                    {
                                        Vector2 pos = new Vector2(b.Position.X + ((b.Sprite.Width / 2 - SmallCoinEmpty.Width / 2) * b.Scale) + (((b.Sprite.Width - SmallCoinEmpty.Width) * b.Scale) / (float)maxCoins[j][Convert.ToInt32(b.Name)]) * i + correctMult, b.Position.Y - 20 * b.Scale);
                                        Texture2D sprite = SmallCoinEmpty;
                                        int plus = 0;
                                        if (Savegame.Coins[j][Convert.ToInt32(b.Name)] > 0 && maxCoins[j][Convert.ToInt32(b.Name)] % 2 == 0)
                                        {
                                            plus = 1;
                                        }
                                        if (i + (int)Math.Floor((double)maxCoins[j][Convert.ToInt32(b.Name)] / 2.0) < Savegame.Coins[j][Convert.ToInt32(b.Name)] + plus)
                                        {
                                            sprite = SmallCoins[0];
                                        }
                                        spriteBatch.Draw(sprite, pos, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.5f + 0.01f * i);
                                    }
                                }
                            }
                        }
                    }
                }


                for (int i = OverallCoins; i > 0; i--)
                {
                        Vector2 pos = new Vector2(130 * scale, GraphicsDevice.Viewport.Height / 2 - 500 * scale + scale * 1.25f * i);
                        Texture2D sprite = SmallCoinEmpty;
                        if (OverallCoins - i < CollectedCoins)
                        {
                            sprite = SmallCoins[0];
                        }
                        spriteBatch.Draw(sprite, pos, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, (1f / (float)OverallCoins) * i);
                }


                for (int i = 0; i < 4; i++)
                {
                    Vector2 pos = new Vector2(180 * scale, GraphicsDevice.Viewport.Height / 2 + (i - 2) * 50 * scale);
                    Texture2D sprite = DEmpty;
                    if (Savegame.Diamonds[3 - i])
                    {
                        sprite = DFull;
                    }
                    spriteBatch.Draw(sprite, pos, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.5f + 0.01f * i);
                }
                spriteBatch.End();

                spriteBatch.Begin();
                spriteBatch.Draw(SwordNormal, new Vector2(mouseState.X, mouseState.Y), null, Color.White, MathHelper.ToRadians(-45), new Vector2(16, 0), 1f, SpriteEffects.None, 0f);
                spriteBatch.End();
            }
            else if (GameState == "LevelEndScreen")
            {
                float Scale = GraphicsDevice.Viewport.Height / 1024f;

                spriteBatch.Begin();
                spriteBatch.Draw(Background, Vector2.Zero, Color.White);
                spriteBatch.End();

                spriteBatch.Begin();
                spriteBatch.Draw(Smiley, new Vector2(GraphicsDevice.Viewport.Width - Smiley.Width * 0.7f - 150 * Scale, 50 * Scale), null, Color.White, 0f, Vector2.Zero, 0.7f * Scale, SpriteEffects.None, 0f);
                spriteBatch.Draw(LevelPics[Difficulty][ChosenLevel], new Vector2(GraphicsDevice.Viewport.Width - 500 * Scale, GraphicsDevice.Viewport.Height - 550 * Scale), null, Color.White, 0f, Vector2.Zero, 0.3f * Scale, SpriteEffects.None, 0f);
                spriteBatch.End();

                spriteBatch.Begin();


                float correct = 0;
                if (maxCoins[Difficulty][Convert.ToInt32(ChosenLevel)] % 2 == 0)
                {
                    correct = (((500 - SmallCoinEmpty.Width) * (GraphicsDevice.Viewport.Height / 1024f)) / (float)maxCoins[Difficulty][ChosenLevel]) / 2f;
                }
                for (int i = -(int)Math.Floor((double)maxCoins[Difficulty][ChosenLevel] / 2.0); i <= (int)Math.Floor((double)maxCoins[Difficulty][ChosenLevel] / 2.0); i++)
                {
                    float correctMult = correct;
                    if (i > 0)
                    {
                        correctMult *= -1;
                    }
                    if ((maxCoins[Difficulty][ChosenLevel] % 2 == 0 && i != 0) || (maxCoins[Difficulty][ChosenLevel] % 2 != 0))
                    {
                        Vector2 pos = new Vector2(50 + ((500 / 2 - SmallCoinEmpty.Width / 2) * (GraphicsDevice.Viewport.Height / 1024f)) + (((500 - SmallCoinEmpty.Width) * (GraphicsDevice.Viewport.Height / 1024f)) / (float)maxCoins[Difficulty][ChosenLevel]) * i + correctMult, 150 * (GraphicsDevice.Viewport.Height / 1024f));
                        Texture2D sprite = SmallCoinEmpty;
                        int plus = 0;
                        if (Savegame.Coins[Difficulty][ChosenLevel] > 0 && maxCoins[Difficulty][ChosenLevel] % 2 == 0)
                        {
                            plus = 1;
                        }
                        if (CoinToAnim == -1)
                        {
                            if (i + (int)Math.Floor((double)maxCoins[Difficulty][ChosenLevel] / 2.0) < Savegame.Coins[Difficulty][ChosenLevel] + plus)
                            {
                                sprite = SmallCoins[0];
                            }
                        }
                        else
                        {
                            if (i + (int)Math.Floor((double)maxCoins[Difficulty][ChosenLevel] / 2.0) < CoinToAnim + plus - 1)
                            {
                                sprite = SmallCoins[0];
                            }
                            if (i + (int)Math.Floor((double)maxCoins[Difficulty][ChosenLevel] / 2.0) == CoinToAnim + plus - 1)
                            {
                                sprite = SmallCoins[CoinAnimPhase];
                            }
                        }
                        spriteBatch.Draw(sprite, pos, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.5f + 0.01f * i);
                    }
                }

                bool fuck = false;
                foreach (GameObject o in CurrentLevel.Past)
                {
                    if (o.Type == 13)
                    {
                        fuck = true;
                        break;
                    }
                }
                foreach (GameObject o in CurrentLevel.Present)
                {
                    if (o.Type == 13)
                    {
                        fuck = true;
                        break;
                    }
                }
                foreach (GameObject o in CurrentLevel.Future)
                {
                    if (o.Type == 13)
                    {
                        fuck = true;
                        break;
                    }
                }

                if (fuck)
                {
                    Texture2D fuckSprite = DEmpty;
                    if (UI.fuckcollected)
                    {
                        fuckSprite = DFull;
                    }
                    spriteBatch.Draw(fuckSprite, new Vector2(100, 150 * (GraphicsDevice.Viewport.Height / 1024f)), Color.White);
                }

                foreach (GameButton b in LevelEndScreenButtons)
                {
                    b.Draw(spriteBatch);
                }
                spriteBatch.End();

                spriteBatch.Begin();
                spriteBatch.Draw(SwordNormal, new Vector2(mouseState.X, mouseState.Y), null, Color.White, MathHelper.ToRadians(-45), new Vector2(16, 0), 1f, SpriteEffects.None, 0f);
                spriteBatch.End();
            }

            base.Draw(gameTime);
        }



        public bool CollisionCheck(Rectangle fr, Rectangle sr)
        {
            bool value = false;
            if (Rectangle.Intersect(fr, sr) != Rectangle.Empty)
            {
                value = true;
            }
            return value;
        }


        public bool CollisionCheck(Texture2D fs, Vector2 fp, Texture2D ss, Vector2 sp, int threshold)
        {
            Rectangle first = new Rectangle((int)fp.X - (int)fs.Width / 2, (int)fp.Y - (int)fs.Height / 2, (int)fs.Width, (int)fs.Height);
            Rectangle second = new Rectangle((int)sp.X - (int)ss.Width / 2, (int)sp.Y - (int)ss.Height / 2, (int)ss.Width, (int)ss.Height);

            bool value = false;
            Rectangle intersect = Rectangle.Intersect(first, second);

            if (intersect != Rectangle.Empty)
            {
                Color[] firstPixels = new Color[fs.Width * fs.Height];
                fs.GetData<Color>(firstPixels);
                Color[] secondPixels = new Color[ss.Width * ss.Height];
                ss.GetData<Color>(secondPixels);
                Vector2 colPos = Vector2.Zero;

                for (int i = intersect.Left; i < intersect.Right; i++)
                {
                    if (value)
                    {
                        break;
                    }
                    for (int j = intersect.Top; j < intersect.Bottom; j++)
                    {
                        int fx = i - first.Left;
                        int fy = j - first.Top;
                        int sx = i - second.Left;
                        int sy = j - second.Top;

                        if (firstPixels[fx + fy * fs.Width].A > threshold & secondPixels[sx + sy * ss.Width].A > threshold)
                        {
                            value = true;
                            colPos = new Vector2(i, j);
                            break;
                        }
                    }
                }
            }

            return value;
        }





        public void SaveGame()
        {
            StorageDevice device = StorageDevice.EndShowSelector(StorageDevice.BeginShowSelector(null, null));
            // Open a storage container.
            IAsyncResult result =
                device.BeginOpenContainer("SaveGame", null, null);

            // Wait for the WaitHandle to become signaled.
            result.AsyncWaitHandle.WaitOne();

            StorageContainer container = device.EndOpenContainer(result);

            // Close the wait handle.
            result.AsyncWaitHandle.Close();

            string filename = "save.sav";

            // Check to see whether the save exists.
            if (container.FileExists(filename))
                // Delete it so that we can create one fresh.
                container.DeleteFile(filename);

            // Create the file.
            Stream stream = container.CreateFile(filename);

            // Convert the object to XML data and put it in the stream.
            XmlSerializer serializer = new XmlSerializer(typeof(Save));

            serializer.Serialize(stream, Savegame);

            // Close the file.
            stream.Close();

            // Dispose the container, to commit changes.
            container.Dispose();
        }


        public Save LoadGame()
        {
            StorageDevice device = StorageDevice.EndShowSelector(StorageDevice.BeginShowSelector(null, null));
            // Open a storage container.
            IAsyncResult result =
                device.BeginOpenContainer("SaveGame", null, null);

            // Wait for the WaitHandle to become signaled.
            result.AsyncWaitHandle.WaitOne();

            StorageContainer container = device.EndOpenContainer(result);

            // Close the wait handle.
            result.AsyncWaitHandle.Close();

            // Check to see whether the save exists.
            if (!container.FileExists("save.sav"))
            {
                // If not, dispose of the container and return.
                container.Dispose();
                return null;
            }

            // Open the file.
            Stream stream = container.OpenFile("save.sav", FileMode.Open);

            XmlSerializer serializer = new XmlSerializer(typeof(Save));

            Save data = (Save)serializer.Deserialize(stream);

            // Close the file.
            stream.Close();

            // Dispose the container.
            container.Dispose();

            return data;
        }


        public void SaveOptions()
        {
            StorageDevice device = StorageDevice.EndShowSelector(StorageDevice.BeginShowSelector(null, null));
            // Open a storage container.
            IAsyncResult result =
                device.BeginOpenContainer("SaveGame", null, null);

            // Wait for the WaitHandle to become signaled.
            result.AsyncWaitHandle.WaitOne();

            StorageContainer container = device.EndOpenContainer(result);

            // Close the wait handle.
            result.AsyncWaitHandle.Close();

            string filename = "settings";

            // Check to see whether the save exists.
            if (container.FileExists(filename))
                // Delete it so that we can create one fresh.
                container.DeleteFile(filename);

            // Create the file.
            Stream stream = container.CreateFile(filename);

            // Convert the object to XML data and put it in the stream.
            XmlSerializer serializer = new XmlSerializer(typeof(Settings));

            serializer.Serialize(stream, Options);

            // Close the file.
            stream.Close();

            // Dispose the container, to commit changes.
            container.Dispose();
        }


        public Settings LoadOptions()
        {
            StorageDevice device = StorageDevice.EndShowSelector(StorageDevice.BeginShowSelector(null, null));
            // Open a storage container.
            IAsyncResult result =
                device.BeginOpenContainer("SaveGame", null, null);

            // Wait for the WaitHandle to become signaled.
            result.AsyncWaitHandle.WaitOne();

            StorageContainer container = device.EndOpenContainer(result);

            // Close the wait handle.
            result.AsyncWaitHandle.Close();

            // Check to see whether the save exists.
            if (!container.FileExists("settings"))
            {
                // If not, dispose of the container and return.
                container.Dispose();
                return null;
            }

            // Open the file.
            Stream stream = container.OpenFile("settings", FileMode.Open);

            XmlSerializer serializer = new XmlSerializer(typeof(Settings));

            Settings data = (Settings)serializer.Deserialize(stream);

            // Close the file.
            stream.Close();

            // Dispose the container.
            container.Dispose();

            return data;
        }


        public Level LoadLevel(string levelName)
        {
            StorageDevice device = StorageDevice.EndShowSelector(StorageDevice.BeginShowSelector(null, null));
            // Open a storage container.
            IAsyncResult result =
                device.BeginOpenContainer(Environment.CurrentDirectory + "/Levels", null, null);

            // Wait for the WaitHandle to become signaled.
            result.AsyncWaitHandle.WaitOne();

            StorageContainer container = device.EndOpenContainer(result);

            // Close the wait handle.
            result.AsyncWaitHandle.Close();

            // Open the file.
            Stream stream = container.OpenFile(levelName, FileMode.Open);

            XmlSerializer serializer = new XmlSerializer(typeof(Level));

            Level data = (Level)serializer.Deserialize(stream);

            // Close the file.
            stream.Close();

            // Dispose the container.
            container.Dispose();



            foreach (GameObject o in data.Past)
            {
                if (o.SpritePaths.Count == 0)
                {
                    o.Sprites = null;
                    o.SpritePaths = null;
                    o.Sprite = Content.Load<Texture2D>(o.SpritePath);
                }
                else
                {
                    o.Sprite = Content.Load<Texture2D>(o.SpritePath);
                    o.Sprites = new List<Texture2D>();
                    foreach (string s in o.SpritePaths)
                    {
                        o.Sprites.Add(Content.Load<Texture2D>(s));
                    }
                }
            }

            foreach (GameObject o in data.Present)
            {
                if (o.SpritePaths.Count == 0)
                {
                    o.Sprites = null;
                    o.SpritePaths = null;
                    o.Sprite = Content.Load<Texture2D>(o.SpritePath);
                }
                else
                {
                    o.Sprite = Content.Load<Texture2D>(o.SpritePath);
                    o.Sprites = new List<Texture2D>();
                    foreach (string s in o.SpritePaths)
                    {
                        o.Sprites.Add(Content.Load<Texture2D>(s));
                    }
                }
            }

            foreach (GameObject o in data.Future)
            {
                if (o.SpritePaths.Count == 0)
                {
                    o.Sprite = Content.Load<Texture2D>(o.SpritePath);
                    o.Sprites = null;
                    o.SpritePaths = null;
                }
                else
                {
                    o.Sprite = Content.Load<Texture2D>(o.SpritePath);
                    o.Sprites = new List<Texture2D>();
                    foreach (string s in o.SpritePaths)
                    {
                        o.Sprites.Add(Content.Load<Texture2D>(s));
                    }
                }
            }

            foreach (Link l in data.PastLinks)
            {
                l.FirstObject = data.Past.Find(o => o.Position == l.FirstObject.Position);
                l.SecondObject = data.Past.Find(o => o.Position == l.SecondObject.Position);
            }
            foreach (Link l in data.PresentLinks)
            {
                l.FirstObject = data.Present.Find(o => o.Position == l.FirstObject.Position);
                l.SecondObject = data.Present.Find(o => o.Position == l.SecondObject.Position);
            }
            foreach (Link l in data.FutureLinks)
            {
                l.FirstObject = data.Future.Find(o => o.Position == l.FirstObject.Position);
                l.SecondObject = data.Future.Find(o => o.Position == l.SecondObject.Position);
            }

            data.PastBackground = Content.Load<Texture2D>(data.PastBgPath);
            data.PresentBackground = Content.Load<Texture2D>(data.PresentBgPath);
            data.FutureBackground = Content.Load<Texture2D>(data.FutureBgPath);

            return data;
        }
    }
}
