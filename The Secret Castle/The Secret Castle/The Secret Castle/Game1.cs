using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using System.IO;
using System.Text;
using Microsoft.Xna.Framework.Storage;
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
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Level CurrentLevel;

        Texture2D Cursor;
        Texture2D CursorNormal;
        Texture2D CursorSelect;
        Texture2D CursorCreate;
        Texture2D CursorHand;
        Texture2D CursorPen;
        Texture2D CursorRubber;
        Texture2D SwordNormal;
        Texture2D SwordPortable;
        Texture2D UI;
        Texture2D UIBox;
        Texture2D ControlBar;
        Texture2D StartWindow;
        Texture2D NewWindow;
        Texture2D LoadWindow;
        Texture2D StaticButtonFalse;
        Texture2D StaticButtonTrue;
        Texture2D PortableButtonFalse;
        Texture2D PortableButtonTrue;
        Texture2D ActivatedButtonFalse;
        Texture2D ActivatedButtonTrue;
        Texture2D LineDot;
        Texture2D RadioButtonT;
        Texture2D RadioButtonF;
        Texture2D PlayerStart;

        SpriteFont Font;

        GameObject Held;
        GameObject Selected;
        List<GameObject> Selection;

        List<Button> UITimeButtons;
        List<Button> ObjectButtonsPast;
        List<Button> ObjectButtonsPresent;
        List<Button> ObjectButtonsFuture;
        List<Button> DropDownButtons;
        List<Button> ControlBarButtons;

        Button StartMenuNewButton;
        Button StartMenuLoadButton;
        Button NewMenuCreateButton;
        Button LoadMenuLoadButton;
        Button CloseButton;
        Button PastRadioButton;
        Button PresentRadioButton;
        Button FutureRadioButton;

        List<Vector2> DotPositions;

        Vector2 Pos;
        int Mode;
        int lastScrollValue;
        bool DropMenu;
        bool StartMenu;
        bool NewMenu;
        bool LoadMenu;
        bool LClicked;
        bool RClicked;
        bool MClicked;
        bool LinkMode;
        Vector2 ClickPos;
        string NameString;
        Vector2 PaClickPos;
        Vector2 PrClickPos;
        Vector2 FuClickPos;
        bool TypeMode;
        bool KeyPressed;
        int Timer;
        bool SetPlayerStart;
        bool RedGhost;
        Vector2 PrevPos;
        string ContentDirectory;
        Vector2 SelectStart;
        Vector2 LastMousePos;
        int UpdatingLink;

        Character Player;

        Camera Cam;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 1024;
            graphics.PreferredBackBufferHeight = 768;
            Window.AllowUserResizing = true;
            Window.Title = "The Secret Castle Editor";
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

            Cursor = new Texture2D(graphics.GraphicsDevice, 1, 1);
            Color[] pixel = new Color[1];
            pixel[0] = Color.Black;
            Cursor.SetData<Color>(pixel);
            CursorNormal = Content.Load<Texture2D>("Sprites/UI/Cursor_Normal");
            CursorSelect = Content.Load<Texture2D>("Sprites/UI/Cursor_Selection");
            CursorCreate = Content.Load<Texture2D>("Sprites/UI/Cursor_Create");
            CursorHand = Content.Load<Texture2D>("Sprites/UI/Cursor_Hand");
            CursorPen = Content.Load<Texture2D>("Sprites/UI/Cursor_Stift");
            CursorRubber = Content.Load<Texture2D>("Sprites/UI/Cursor_Gummi");
            SwordNormal = Content.Load<Texture2D>("Sprites/UI/SwordNormal");
            SwordPortable = Content.Load<Texture2D>("Sprites/UI/SwordPortable");
            UI = Content.Load<Texture2D>("Sprites/UI/UI");
            UIBox = Content.Load<Texture2D>("Sprites/UI/Box");
            ControlBar = Content.Load<Texture2D>("Sprites/UI/ControlBar");
            StartWindow = Content.Load<Texture2D>("Sprites/UI/StartWindow");
            NewWindow = Content.Load<Texture2D>("Sprites/UI/NewWindow");
            LoadWindow = Content.Load<Texture2D>("Sprites/UI/LoadWindow");
            StaticButtonFalse = Content.Load<Texture2D>("Sprites/UI/StaticButtonFalse");
            StaticButtonTrue = Content.Load<Texture2D>("Sprites/UI/StaticButtonTrue");
            PortableButtonFalse = Content.Load<Texture2D>("Sprites/UI/PortableButtonFalse");
            PortableButtonTrue = Content.Load<Texture2D>("Sprites/UI/PortableButtonTrue");
            ActivatedButtonFalse = Content.Load<Texture2D>("Sprites/UI/ActivatedButtonFalse");
            ActivatedButtonTrue = Content.Load<Texture2D>("Sprites/UI/ActivatedButtonTrue");
            LineDot = Content.Load<Texture2D>("Sprites/UI/LineDot");
            RadioButtonF = Content.Load<Texture2D>("Sprites/UI/RadioButton");
            RadioButtonT = Content.Load<Texture2D>("Sprites/UI/CloseButton");
            PlayerStart = Content.Load<Texture2D>("Sprites/UI/PlayerStart");

            Font = Content.Load<SpriteFont>("SpriteFont1");


            UITimeButtons = new List<Button>();
            UITimeButtons.Add(new Button(new Vector2(1120, 20), "Sprites/UI/PastButton", 0, Content));
            UITimeButtons.Add(new Button(new Vector2(1120, 60), "Sprites/UI/PresentButton", 0, Content));
            UITimeButtons.Add(new Button(new Vector2(1120, 100), "Sprites/UI/FutureButton", 0, Content));
            UITimeButtons.ElementAt<Button>(0).Pressed = true;

            ObjectButtonsPast = new List<Button>();
            ObjectButtonsPast.Add(new Button(new Vector2(1150, 150), "Sprites/Walls/BodVergB", 1, Content));
            ObjectButtonsPast.Add(new Button(new Vector2(1150, 150), "Sprites/Walls/BodVergEL", 1, Content));
            ObjectButtonsPast.Add(new Button(new Vector2(1150, 150), "Sprites/Walls/BodVergEO", 1, Content));
            ObjectButtonsPast.Add(new Button(new Vector2(1150, 150), "Sprites/Walls/BodVergER", 1, Content));
            ObjectButtonsPast.Add(new Button(new Vector2(1150, 150), "Sprites/Walls/BodVergEU", 1, Content));
            ObjectButtonsPast.Add(new Button(new Vector2(1150, 150), "Sprites/Walls/BodVerg", 1, Content));
            ObjectButtonsPast.Add(new Button(new Vector2(1150, 150), "Sprites/Walls/BodVergUp", 1, Content));
            ObjectButtonsPast.Add(new Button(new Vector2(1150, 150), "Sprites/Walls/BodVergULeck", 1, Content));
            ObjectButtonsPast.Add(new Button(new Vector2(1150, 150), "Sprites/Walls/BodVergUReck", 1, Content));
            ObjectButtonsPast.Add(new Button(new Vector2(1150, 150), "Sprites/Walls/BodVergOLeck", 1, Content));
            ObjectButtonsPast.Add(new Button(new Vector2(1150, 150), "Sprites/Walls/BodVergOReck", 1, Content));
            ObjectButtonsPast.Add(new Button(new Vector2(1150, 150), "Sprites/Walls/BodVergTl", 1, Content));
            ObjectButtonsPast.Add(new Button(new Vector2(1150, 150), "Sprites/Walls/BodVergTo", 1, Content));
            ObjectButtonsPast.Add(new Button(new Vector2(1150, 150), "Sprites/Walls/BodVergTr", 1, Content));
            ObjectButtonsPast.Add(new Button(new Vector2(1150, 150), "Sprites/Walls/BodVergTu", 1, Content));
            ObjectButtonsPast.Add(new Button(new Vector2(1150, 150), "Sprites/Walls/BodVergKr", 1, Content));
            ObjectButtonsPast.Add(new Button(new Vector2(1150, ObjectButtonsPast.ElementAt<Button>(ObjectButtonsPast.Count - 1).Position.Y + ObjectButtonsPast.ElementAt<Button>(ObjectButtonsPast.Count - 1).Sprite.Height + 10), "Sprites/Walls/HintergrundVergKlein", 0, Content));
            ObjectButtonsPast.Add(new Button(new Vector2(1150, ObjectButtonsPast.ElementAt<Button>(ObjectButtonsPast.Count - 1).Position.Y + ObjectButtonsPast.ElementAt<Button>(ObjectButtonsPast.Count - 1).Sprite.Height + 10), "Sprites/Walls/HintergrundVerg", 0, Content));
            ObjectButtonsPast.Add(new Button(new Vector2(1150, ObjectButtonsPast.ElementAt<Button>(ObjectButtonsPast.Count - 1).Position.Y + ObjectButtonsPast.ElementAt<Button>(ObjectButtonsPast.Count - 1).Sprite.Height + 10), "Sprites/Objects/Crates/Kiste_Verg", 2, Content));
            List<string> lp = new List<string>();
            lp.Add("Sprites/Objects/Switches/SchVer_off");
            lp.Add("Sprites/Objects/Switches/SchVer_on");
            lp.Add("Sprites/Objects/Switches/SchVer_mitte");
            ObjectButtonsPast.Add(new Button(new Vector2(1150, ObjectButtonsPast.ElementAt<Button>(ObjectButtonsPast.Count - 1).Position.Y + ObjectButtonsPast.ElementAt<Button>(ObjectButtonsPast.Count - 1).Sprite.Height + 10), lp, 100, 0, 3, Content));
            ObjectButtonsPast.Add(new Button(new Vector2(1150, ObjectButtonsPast.ElementAt<Button>(ObjectButtonsPast.Count - 1).Position.Y + ObjectButtonsPast.ElementAt<Button>(ObjectButtonsPast.Count - 1).Sprite.Height + 10), "Sprites/Objects/Other/Leiter", 6, Content));
            ObjectButtonsPast.Add(new Button(new Vector2(1150, ObjectButtonsPast.ElementAt<Button>(ObjectButtonsPast.Count - 1).Position.Y + ObjectButtonsPast.ElementAt<Button>(ObjectButtonsPast.Count - 1).Sprite.Height + 10), "Sprites/Objects/Pillars/PfoVerg2", 2, Content));
            List<string> tsp = new List<string>();
            tsp.Add("Sprites/Objects/Switches/SchZV0");
            tsp.Add("Sprites/Objects/Switches/SchZV0");
            tsp.Add("Sprites/Objects/Switches/SchZV1");
            tsp.Add("Sprites/Objects/Switches/SchZV2");
            tsp.Add("Sprites/Objects/Switches/SchZV3");
            tsp.Add("Sprites/Objects/Switches/SchZV0");
            tsp.Add("Sprites/Objects/Switches/SchZV1");
            tsp.Add("Sprites/Objects/Switches/SchZV2");
            tsp.Add("Sprites/Objects/Switches/SchZV3");
            tsp.Add("Sprites/Objects/Switches/SchZV0");
            tsp.Add("Sprites/Objects/Switches/SchZV1");
            tsp.Add("Sprites/Objects/Switches/SchZV2");
            tsp.Add("Sprites/Objects/Switches/SchZV3");
            tsp.Add("Sprites/Objects/Switches/SchZV0");
            tsp.Add("Sprites/Objects/Switches/SchZV1");
            tsp.Add("Sprites/Objects/Switches/SchZV2");
            tsp.Add("Sprites/Objects/Switches/SchZV3");
            ObjectButtonsPast.Add(new Button(new Vector2(1150, ObjectButtonsPast.ElementAt<Button>(ObjectButtonsPast.Count - 1).Position.Y + ObjectButtonsPast.ElementAt<Button>(ObjectButtonsPast.Count - 1).Sprite.Height + 10), tsp, 100, 0, 4, Content));
            List<string> ppp = new List<string>();
            ppp.Add("Sprites/Objects/Switches/SchVergTast_off");
            ppp.Add("Sprites/Objects/Switches/SchVergTast_on");
            ppp.Add("Sprites/Objects/Switches/SchVergTast_1");
            ppp.Add("Sprites/Objects/Switches/SchVergTast_2");
            ppp.Add("Sprites/Objects/Switches/SchVergTast_3");
            ppp.Add("Sprites/Objects/Switches/SchVergTast_4");
            ppp.Add("Sprites/Objects/Switches/SchVergTast_5");
            ppp.Add("Sprites/Objects/Switches/SchVergTast_6");
            ObjectButtonsPast.Add(new Button(new Vector2(1150, ObjectButtonsPast.ElementAt<Button>(ObjectButtonsPast.Count - 1).Position.Y + ObjectButtonsPast.ElementAt<Button>(ObjectButtonsPast.Count - 1).Sprite.Height + 10), ppp, 200, 0, 5, Content));
            List<string> pppr = new List<string>();
            pppr.Add("Sprites/Objects/Switches/SeilVergTast_off");
            pppr.Add("Sprites/Objects/Switches/SeilVergTast_on");
            pppr.Add("Sprites/Objects/Switches/SeilVergTast_1");
            pppr.Add("Sprites/Objects/Switches/SeilVergTast_2");
            pppr.Add("Sprites/Objects/Switches/SeilVergTast_3");
            pppr.Add("Sprites/Objects/Switches/SeilVergTast_4");
            pppr.Add("Sprites/Objects/Switches/SeilVergTast_5");
            pppr.Add("Sprites/Objects/Switches/SeilVergTast_6");
            ObjectButtonsPast.Add(new Button(new Vector2(1150, ObjectButtonsPast.ElementAt<Button>(ObjectButtonsPast.Count - 1).Position.Y + ObjectButtonsPast.ElementAt<Button>(ObjectButtonsPast.Count - 1).Sprite.Height + 10), pppr, 200, 0, 2, Content));
            List<string> dp = new List<string>();
            dp.Add("Sprites/Objects/Doors/TuerVerg^close");
            dp.Add("Sprites/Objects/Doors/TuerVerg^open");
            dp.Add("Sprites/Objects/Doors/TuerVerg^01");
            dp.Add("Sprites/Objects/Doors/TuerVerg^02");
            dp.Add("Sprites/Objects/Doors/TuerVerg^03");
            dp.Add("Sprites/Objects/Doors/TuerVerg^04");
            dp.Add("Sprites/Objects/Doors/TuerVerg^05");
            dp.Add("Sprites/Objects/Doors/TuerVerg^06");
            dp.Add("Sprites/Objects/Doors/TuerVerg^07");
            dp.Add("Sprites/Objects/Doors/TuerVerg^08");
            dp.Add("Sprites/Objects/Doors/TuerVerg^09");
            dp.Add("Sprites/Objects/Doors/TuerVerg^10");
            dp.Add("Sprites/Objects/Doors/TuerVerg^11");
            dp.Add("Sprites/Objects/Doors/TuerVerg^12");
            dp.Add("Sprites/Objects/Doors/TuerVerg^13");
            dp.Add("Sprites/Objects/Doors/TuerVerg^14");
            dp.Add("Sprites/Objects/Doors/TuerVerg^15");
            dp.Add("Sprites/Objects/Doors/TuerVerg^16");
            dp.Add("Sprites/Objects/Doors/TuerVerg^17");
            dp.Add("Sprites/Objects/Doors/TuerVerg^18");
            dp.Add("Sprites/Objects/Doors/TuerVerg^19");
            ObjectButtonsPast.Add(new Button(new Vector2(1150, ObjectButtonsPast.ElementAt<Button>(ObjectButtonsPast.Count - 1).Position.Y + ObjectButtonsPast.ElementAt<Button>(ObjectButtonsPast.Count - 1).Sprite.Height + 10), dp, 100, 0, 2, Content));
            ObjectButtonsPast.Add(new Button(new Vector2(1150, ObjectButtonsPast.ElementAt<Button>(ObjectButtonsPast.Count - 1).Position.Y + ObjectButtonsPast.ElementAt<Button>(ObjectButtonsPast.Count - 1).Sprite.Height + 10), "Sprites/Objects/Background/fenster", 14, Content));
            ObjectButtonsPast.Add(new Button(new Vector2(1150, ObjectButtonsPast.ElementAt<Button>(ObjectButtonsPast.Count - 1).Position.Y + ObjectButtonsPast.ElementAt<Button>(ObjectButtonsPast.Count - 1).Sprite.Height + 10), "Sprites/Objects/Background/Kerze", 2, Content));
            ObjectButtonsPast.Add(new Button(new Vector2(1150, ObjectButtonsPast.ElementAt<Button>(ObjectButtonsPast.Count - 1).Position.Y + ObjectButtonsPast.ElementAt<Button>(ObjectButtonsPast.Count - 1).Sprite.Height + 10), "Sprites/Objects/Background/Stuhl_Verg_L", 2, Content));
            ObjectButtonsPast.Add(new Button(new Vector2(1150, ObjectButtonsPast.ElementAt<Button>(ObjectButtonsPast.Count - 1).Position.Y + ObjectButtonsPast.ElementAt<Button>(ObjectButtonsPast.Count - 1).Sprite.Height + 10), "Sprites/Objects/Background/Stuhl_Verg_R", 2, Content));
            ObjectButtonsPast.Add(new Button(new Vector2(1150, ObjectButtonsPast.ElementAt<Button>(ObjectButtonsPast.Count - 1).Position.Y + ObjectButtonsPast.ElementAt<Button>(ObjectButtonsPast.Count - 1).Sprite.Height + 10), "Sprites/Objects/Background/Tisch_Verg", 2, Content));
            ObjectButtonsPast.Add(new Button(new Vector2(1150, ObjectButtonsPast.ElementAt<Button>(ObjectButtonsPast.Count - 1).Position.Y + ObjectButtonsPast.ElementAt<Button>(ObjectButtonsPast.Count - 1).Sprite.Height + 10), "Sprites/Objects/Background/Statue", 2, Content));
            List<string> c = new List<string>();
            c.Add("Sprites/Objects/Gameobjects/Muenze1");
            c.Add("Sprites/Objects/Gameobjects/Muenze1");
            c.Add("Sprites/Objects/Gameobjects/Muenze1");
            c.Add("Sprites/Objects/Gameobjects/Muenze2");
            c.Add("Sprites/Objects/Gameobjects/Muenze3");
            c.Add("Sprites/Objects/Gameobjects/Muenze4");
            c.Add("Sprites/Objects/Gameobjects/Muenze5");
            c.Add("Sprites/Objects/Gameobjects/Muenze6");
            c.Add("Sprites/Objects/Gameobjects/Muenze7");
            c.Add("Sprites/Objects/Gameobjects/Muenze8");
            c.Add("Sprites/Objects/Gameobjects/Muenze9");
            c.Add("Sprites/Objects/Gameobjects/Muenze10");
            c.Add("Sprites/Objects/Gameobjects/Muenze11");
            c.Add("Sprites/Objects/Gameobjects/Muenze12");
            c.Add("Sprites/Objects/Gameobjects/Muenze13");
            c.Add("Sprites/Objects/Gameobjects/Muenze14");
            c.Add("Sprites/Objects/Gameobjects/Muenze15");
            c.Add("Sprites/Objects/Gameobjects/Muenze16");
            c.Add("Sprites/Objects/Gameobjects/Muenze17");
            c.Add("Sprites/Objects/Gameobjects/Muenze18");
            c.Add("Sprites/Objects/Gameobjects/Muenze19");
            c.Add("Sprites/Objects/Gameobjects/Muenze20");
            c.Add("Sprites/Objects/Gameobjects/Muenze21");
            ObjectButtonsPast.Add(new Button(new Vector2(1150, ObjectButtonsPast.ElementAt<Button>(ObjectButtonsPast.Count - 1).Position.Y + ObjectButtonsPast.ElementAt<Button>(ObjectButtonsPast.Count - 1).Sprite.Height + 10), c, 25, 21, 9, Content));
            List<string> ceg = new List<string>();
            ceg.Add("Sprites/Objects/Gameobjects/egg1");
            ceg.Add("Sprites/Objects/Gameobjects/egg1");
            ceg.Add("Sprites/Objects/Gameobjects/egg1");
            ceg.Add("Sprites/Objects/Gameobjects/egg2");
            ceg.Add("Sprites/Objects/Gameobjects/egg3");
            ceg.Add("Sprites/Objects/Gameobjects/egg4");
            ceg.Add("Sprites/Objects/Gameobjects/egg5");
            ceg.Add("Sprites/Objects/Gameobjects/egg6");
            ceg.Add("Sprites/Objects/Gameobjects/egg7");
            ceg.Add("Sprites/Objects/Gameobjects/egg8");
            ceg.Add("Sprites/Objects/Gameobjects/egg9");
            ceg.Add("Sprites/Objects/Gameobjects/egg10");
            ceg.Add("Sprites/Objects/Gameobjects/egg11");
            ceg.Add("Sprites/Objects/Gameobjects/egg12");
            ceg.Add("Sprites/Objects/Gameobjects/egg13");
            ceg.Add("Sprites/Objects/Gameobjects/egg14");
            ceg.Add("Sprites/Objects/Gameobjects/egg15");
            ceg.Add("Sprites/Objects/Gameobjects/egg16");
            ceg.Add("Sprites/Objects/Gameobjects/egg17");
            ceg.Add("Sprites/Objects/Gameobjects/egg18");
            ceg.Add("Sprites/Objects/Gameobjects/egg19");
            ceg.Add("Sprites/Objects/Gameobjects/egg20");
            ceg.Add("Sprites/Objects/Gameobjects/egg21");
            ObjectButtonsPast.Add(new Button(new Vector2(1150, ObjectButtonsPast.ElementAt<Button>(ObjectButtonsPast.Count - 1).Position.Y + ObjectButtonsPast.ElementAt<Button>(ObjectButtonsPast.Count - 1).Sprite.Height + 10), ceg, 25, 21, 9, Content));
            List<string> d = new List<string>();
            d.Add("Sprites/Objects/Gameobjects/DiamantL");
            d.Add("Sprites/Objects/Gameobjects/DiamantL");
            d.Add("Sprites/Objects/Gameobjects/DiamantL");
            d.Add("Sprites/Objects/Gameobjects/Diamant1");
            d.Add("Sprites/Objects/Gameobjects/Diamant2");
            d.Add("Sprites/Objects/Gameobjects/Diamant3");
            d.Add("Sprites/Objects/Gameobjects/Diamant4");
            d.Add("Sprites/Objects/Gameobjects/Diamant5");
            d.Add("Sprites/Objects/Gameobjects/Diamant6");
            d.Add("Sprites/Objects/Gameobjects/Diamant7");
            d.Add("Sprites/Objects/Gameobjects/Diamant8");
            d.Add("Sprites/Objects/Gameobjects/Diamant9");
            d.Add("Sprites/Objects/Gameobjects/Diamant10");
            d.Add("Sprites/Objects/Gameobjects/Diamant11");
            d.Add("Sprites/Objects/Gameobjects/Diamant12");
            d.Add("Sprites/Objects/Gameobjects/Diamant13");
            d.Add("Sprites/Objects/Gameobjects/Diamant14");
            d.Add("Sprites/Objects/Gameobjects/Diamant15");
            d.Add("Sprites/Objects/Gameobjects/Diamant16");
            ObjectButtonsPast.Add(new Button(new Vector2(1150, ObjectButtonsPast.ElementAt<Button>(ObjectButtonsPast.Count - 1).Position.Y + ObjectButtonsPast.ElementAt<Button>(ObjectButtonsPast.Count - 1).Sprite.Height + 10), d, 25, 16, 13, Content));
            ObjectButtonsPast.Add(new Button(new Vector2(1150, ObjectButtonsPast.ElementAt<Button>(ObjectButtonsPast.Count - 1).Position.Y + ObjectButtonsPast.ElementAt<Button>(ObjectButtonsPast.Count - 1).Sprite.Height + 10), "Sprites/Objects/Gameobjects/schluessel 1", 10, Content));
            ObjectButtonsPast.Add(new Button(new Vector2(1150, ObjectButtonsPast.ElementAt<Button>(ObjectButtonsPast.Count - 1).Position.Y + ObjectButtonsPast.ElementAt<Button>(ObjectButtonsPast.Count - 1).Sprite.Height + 10), "Sprites/Objects/Gameobjects/schluessel 2", 10, Content));
            ObjectButtonsPast.Add(new Button(new Vector2(1150, ObjectButtonsPast.ElementAt<Button>(ObjectButtonsPast.Count - 1).Position.Y + ObjectButtonsPast.ElementAt<Button>(ObjectButtonsPast.Count - 1).Sprite.Height + 10), "Sprites/Objects/Gameobjects/schluessel 3", 10, Content));
            List<string> ed = new List<string>();
            ed.Add("Sprites/Objects/Doors/Endtür");
            ed.Add("Sprites/Objects/Doors/Endtür11");
            ed.Add("Sprites/Objects/Doors/Endtür1");
            ed.Add("Sprites/Objects/Doors/Endtür2");
            ed.Add("Sprites/Objects/Doors/Endtür3");
            ed.Add("Sprites/Objects/Doors/Endtür4");
            ed.Add("Sprites/Objects/Doors/Endtür5");
            ed.Add("Sprites/Objects/Doors/Endtür6");
            ed.Add("Sprites/Objects/Doors/Endtür7");
            ed.Add("Sprites/Objects/Doors/Endtür8");
            ed.Add("Sprites/Objects/Doors/Endtür9");
            ed.Add("Sprites/Objects/Doors/Endtür10");
            ed.Add("Sprites/Objects/Doors/Endtür11");
            ObjectButtonsPast.Add(new Button(new Vector2(1150, ObjectButtonsPast.ElementAt<Button>(ObjectButtonsPast.Count - 1).Position.Y + ObjectButtonsPast.ElementAt<Button>(ObjectButtonsPast.Count - 1).Sprite.Height + 10), ed, 100, 0, 15, Content));
            ObjectButtonsPast.Add(new Button(new Vector2(1150, ObjectButtonsPast.ElementAt<Button>(ObjectButtonsPast.Count - 1).Position.Y + ObjectButtonsPast.ElementAt<Button>(ObjectButtonsPast.Count - 1).Sprite.Height + 10), "Sprites/Objects/Gameobjects/Trigger", 16, Content));
            ObjectButtonsPast.Add(new Button(new Vector2(1150, ObjectButtonsPast.ElementAt<Button>(ObjectButtonsPast.Count - 1).Position.Y + ObjectButtonsPast.ElementAt<Button>(ObjectButtonsPast.Count - 1).Sprite.Height + 10), "Sprites/Objects/Gameobjects/monokel", 17, Content));

            ObjectButtonsPresent = new List<Button>();
            ObjectButtonsPresent.Add(new Button(new Vector2(1150, 150), "Sprites/Walls/BodGegB", 1, Content));
            ObjectButtonsPresent.Add(new Button(new Vector2(1150, 150), "Sprites/Walls/BodGegEL", 1, Content));
            ObjectButtonsPresent.Add(new Button(new Vector2(1150, 150), "Sprites/Walls/BodGegEO", 1, Content));
            ObjectButtonsPresent.Add(new Button(new Vector2(1150, 150), "Sprites/Walls/BodGegER", 1, Content));
            ObjectButtonsPresent.Add(new Button(new Vector2(1150, 150), "Sprites/Walls/BodGegEU", 1, Content));
            ObjectButtonsPresent.Add(new Button(new Vector2(1150, 150), "Sprites/Walls/BodGeg", 1, Content));
            ObjectButtonsPresent.Add(new Button(new Vector2(1150, 150), "Sprites/Walls/BodGegUp", 1, Content));
            ObjectButtonsPresent.Add(new Button(new Vector2(1150, 150), "Sprites/Walls/BodGegLUeck", 1, Content));
            ObjectButtonsPresent.Add(new Button(new Vector2(1150, 150), "Sprites/Walls/BodGegRUeck", 1, Content));
            ObjectButtonsPresent.Add(new Button(new Vector2(1150, 150), "Sprites/Walls/BodGegLOeck", 1, Content));
            ObjectButtonsPresent.Add(new Button(new Vector2(1150, 150), "Sprites/Walls/BodGegROeck", 1, Content));
            ObjectButtonsPresent.Add(new Button(new Vector2(1150, 150), "Sprites/Walls/BodGegTl", 1, Content));
            ObjectButtonsPresent.Add(new Button(new Vector2(1150, 150), "Sprites/Walls/BodGegTo", 1, Content));
            ObjectButtonsPresent.Add(new Button(new Vector2(1150, 150), "Sprites/Walls/BodGegTr", 1, Content));
            ObjectButtonsPresent.Add(new Button(new Vector2(1150, 150), "Sprites/Walls/BodGegTu", 1, Content));
            ObjectButtonsPresent.Add(new Button(new Vector2(1150, 150), "Sprites/Walls/BodGegKr", 1, Content));
            ObjectButtonsPresent.Add(new Button(new Vector2(1150, ObjectButtonsPresent.ElementAt<Button>(ObjectButtonsPresent.Count - 1).Position.Y + ObjectButtonsPresent.ElementAt<Button>(ObjectButtonsPresent.Count - 1).Sprite.Height + 10), "Sprites/Walls/HintergrundGeg", 0, Content));
            ObjectButtonsPresent.Add(new Button(new Vector2(1150, ObjectButtonsPresent.ElementAt<Button>(ObjectButtonsPresent.Count - 1).Position.Y + ObjectButtonsPresent.ElementAt<Button>(ObjectButtonsPresent.Count - 1).Sprite.Height + 10), "Sprites/Objects/Crates/Kiste_Gegenwart", 2, Content));
            List<string> tspr = new List<string>();
            tspr.Add("Sprites/Objects/Switches/SchGegZ_Off");
            tspr.Add("Sprites/Objects/Switches/SchGegZ_On");
            tspr.Add("Sprites/Objects/Switches/SchGegZ1");
            tspr.Add("Sprites/Objects/Switches/SchGegZ2");
            tspr.Add("Sprites/Objects/Switches/SchGegZ3");
            tspr.Add("Sprites/Objects/Switches/SchGegZ4");
            tspr.Add("Sprites/Objects/Switches/SchGegZ5");
            tspr.Add("Sprites/Objects/Switches/SchGegZ_On");
            tspr.Add("Sprites/Objects/Switches/SchGegZ_On");
            ObjectButtonsPresent.Add(new Button(new Vector2(1150, ObjectButtonsPresent.ElementAt<Button>(ObjectButtonsPresent.Count - 1).Position.Y + ObjectButtonsPresent.ElementAt<Button>(ObjectButtonsPresent.Count - 1).Sprite.Height + 10), tspr, 100, 0, 12, Content));
            List<string> pp = new List<string>();
            pp.Add("Sprites/Objects/Switches/SchGegTast_Off");
            pp.Add("Sprites/Objects/Switches/SchGegTast_On");
            ObjectButtonsPresent.Add(new Button(new Vector2(1150, ObjectButtonsPresent.ElementAt<Button>(ObjectButtonsPresent.Count - 1).Position.Y + ObjectButtonsPresent.ElementAt<Button>(ObjectButtonsPresent.Count - 1).Sprite.Height + 10), pp, 100, 0, 5, Content));
            List<string> sp = new List<string>();
            sp.Add("Sprites/Objects/Switches/WandhebelOff");
            sp.Add("Sprites/Objects/Switches/WandhebelOn");
            ObjectButtonsPresent.Add(new Button(new Vector2(1150, ObjectButtonsPresent.ElementAt<Button>(ObjectButtonsPresent.Count - 1).Position.Y + ObjectButtonsPresent.ElementAt<Button>(ObjectButtonsPresent.Count - 1).Sprite.Height + 10), sp, 100, 0, 3, Content));
            ObjectButtonsPresent.Add(new Button(new Vector2(1150, ObjectButtonsPresent.ElementAt<Button>(ObjectButtonsPresent.Count - 1).Position.Y + ObjectButtonsPresent.ElementAt<Button>(ObjectButtonsPresent.Count - 1).Sprite.Height + 10), "Sprites/Objects/Other/TreppeL", 7, Content));
            ObjectButtonsPresent.Add(new Button(new Vector2(1150, ObjectButtonsPresent.ElementAt<Button>(ObjectButtonsPresent.Count - 1).Position.Y + ObjectButtonsPresent.ElementAt<Button>(ObjectButtonsPresent.Count - 1).Sprite.Height + 10), "Sprites/Objects/Other/TreppeR", 7, Content));
            ObjectButtonsPresent.Add(new Button(new Vector2(1150, ObjectButtonsPresent.ElementAt<Button>(ObjectButtonsPresent.Count - 1).Position.Y + ObjectButtonsPresent.ElementAt<Button>(ObjectButtonsPresent.Count - 1).Sprite.Height + 10), "Sprites/Objects/Pillars/PfoGeg", 2, Content));
            List<string> dpr = new List<string>();
            dpr.Add("Sprites/Objects/Doors/TgegClose");
            dpr.Add("Sprites/Objects/Doors/TgegOpen");
            ObjectButtonsPresent.Add(new Button(new Vector2(1150, ObjectButtonsPresent.ElementAt<Button>(ObjectButtonsPresent.Count - 1).Position.Y + ObjectButtonsPresent.ElementAt<Button>(ObjectButtonsPresent.Count - 1).Sprite.Height + 10), dpr, 100, 0, 11, Content));
            ObjectButtonsPresent.Add(new Button(new Vector2(1150, ObjectButtonsPresent.ElementAt<Button>(ObjectButtonsPresent.Count - 1).Position.Y + ObjectButtonsPresent.ElementAt<Button>(ObjectButtonsPresent.Count - 1).Sprite.Height + 10), "Sprites/Objects/Background/Stuhl_geg_L", 2, Content));
            ObjectButtonsPresent.Add(new Button(new Vector2(1150, ObjectButtonsPresent.ElementAt<Button>(ObjectButtonsPresent.Count - 1).Position.Y + ObjectButtonsPresent.ElementAt<Button>(ObjectButtonsPresent.Count - 1).Sprite.Height + 10), "Sprites/Objects/Background/Stuhl_geg_R", 2, Content));
            ObjectButtonsPresent.Add(new Button(new Vector2(1150, ObjectButtonsPresent.ElementAt<Button>(ObjectButtonsPresent.Count - 1).Position.Y + ObjectButtonsPresent.ElementAt<Button>(ObjectButtonsPresent.Count - 1).Sprite.Height + 10), "Sprites/Objects/Background/Tisch_geg", 2, Content));
            ObjectButtonsPresent.Add(new Button(new Vector2(1150, ObjectButtonsPresent.ElementAt<Button>(ObjectButtonsPresent.Count - 1).Position.Y + ObjectButtonsPresent.ElementAt<Button>(ObjectButtonsPresent.Count - 1).Sprite.Height + 10), "Sprites/Objects/Background/Bild_Geg1Groß", 2, Content));
            ObjectButtonsPresent.Add(new Button(new Vector2(1150, ObjectButtonsPresent.ElementAt<Button>(ObjectButtonsPresent.Count - 1).Position.Y + ObjectButtonsPresent.ElementAt<Button>(ObjectButtonsPresent.Count - 1).Sprite.Height + 10), "Sprites/Objects/Background/Bild_Geg2Groß", 2, Content));
            ObjectButtonsPresent.Add(new Button(new Vector2(1150, ObjectButtonsPresent.ElementAt<Button>(ObjectButtonsPresent.Count - 1).Position.Y + ObjectButtonsPresent.ElementAt<Button>(ObjectButtonsPresent.Count - 1).Sprite.Height + 10), "Sprites/Objects/Background/Bild_Geg3Groß", 2, Content));
            ObjectButtonsPresent.Add(new Button(new Vector2(1150, ObjectButtonsPresent.ElementAt<Button>(ObjectButtonsPresent.Count - 1).Position.Y + ObjectButtonsPresent.ElementAt<Button>(ObjectButtonsPresent.Count - 1).Sprite.Height + 10), "Sprites/Objects/Background/Fenster_Geg", 14, Content));
            ObjectButtonsPresent.Add(new Button(new Vector2(1150, ObjectButtonsPresent.ElementAt<Button>(ObjectButtonsPresent.Count - 1).Position.Y + ObjectButtonsPresent.ElementAt<Button>(ObjectButtonsPresent.Count - 1).Sprite.Height + 10), "Sprites/Objects/Background/Lampe_Geg", 2, Content));
            ObjectButtonsPresent.Add(new Button(new Vector2(1150, ObjectButtonsPresent.ElementAt<Button>(ObjectButtonsPresent.Count - 1).Position.Y + ObjectButtonsPresent.ElementAt<Button>(ObjectButtonsPresent.Count - 1).Sprite.Height + 10), c, 25, 21, 9, Content));
            ObjectButtonsPresent.Add(new Button(new Vector2(1150, ObjectButtonsPresent.ElementAt<Button>(ObjectButtonsPresent.Count - 1).Position.Y + ObjectButtonsPresent.ElementAt<Button>(ObjectButtonsPresent.Count - 1).Sprite.Height + 10), ceg, 25, 21, 9, Content));
            ObjectButtonsPresent.Add(new Button(new Vector2(1150, ObjectButtonsPresent.ElementAt<Button>(ObjectButtonsPresent.Count - 1).Position.Y + ObjectButtonsPresent.ElementAt<Button>(ObjectButtonsPresent.Count - 1).Sprite.Height + 10), d, 25, 16, 13, Content));
            ObjectButtonsPresent.Add(new Button(new Vector2(1150, ObjectButtonsPresent.ElementAt<Button>(ObjectButtonsPresent.Count - 1).Position.Y + ObjectButtonsPresent.ElementAt<Button>(ObjectButtonsPresent.Count - 1).Sprite.Height + 10), "Sprites/Objects/Gameobjects/schluessel 1", 10, Content));
            ObjectButtonsPresent.Add(new Button(new Vector2(1150, ObjectButtonsPresent.ElementAt<Button>(ObjectButtonsPresent.Count - 1).Position.Y + ObjectButtonsPresent.ElementAt<Button>(ObjectButtonsPresent.Count - 1).Sprite.Height + 10), "Sprites/Objects/Gameobjects/schluessel 2", 10, Content));
            ObjectButtonsPresent.Add(new Button(new Vector2(1150, ObjectButtonsPresent.ElementAt<Button>(ObjectButtonsPresent.Count - 1).Position.Y + ObjectButtonsPresent.ElementAt<Button>(ObjectButtonsPresent.Count - 1).Sprite.Height + 10), "Sprites/Objects/Gameobjects/schluessel 3", 10, Content));
            ObjectButtonsPresent.Add(new Button(new Vector2(1150, ObjectButtonsPresent.ElementAt<Button>(ObjectButtonsPresent.Count - 1).Position.Y + ObjectButtonsPresent.ElementAt<Button>(ObjectButtonsPresent.Count - 1).Sprite.Height + 10), ed, 100, 0, 15, Content));
            ObjectButtonsPresent.Add(new Button(new Vector2(1150, ObjectButtonsPresent.ElementAt<Button>(ObjectButtonsPresent.Count - 1).Position.Y + ObjectButtonsPresent.ElementAt<Button>(ObjectButtonsPresent.Count - 1).Sprite.Height + 10), "Sprites/Objects/Gameobjects/Trigger", 16, Content));
            ObjectButtonsPresent.Add(new Button(new Vector2(1150, ObjectButtonsPresent.ElementAt<Button>(ObjectButtonsPresent.Count - 1).Position.Y + ObjectButtonsPresent.ElementAt<Button>(ObjectButtonsPresent.Count - 1).Sprite.Height + 10), "Sprites/Objects/Gameobjects/monokel", 17, Content));

            ObjectButtonsFuture = new List<Button>();
            ObjectButtonsFuture.Add(new Button(new Vector2(1150, 150), "Sprites/Walls/BodenZukBlock", 1, Content));
            ObjectButtonsFuture.Add(new Button(new Vector2(1150, 150), "Sprites/Walls/BodenZukEndL", 1, Content));
            ObjectButtonsFuture.Add(new Button(new Vector2(1150, 150), "Sprites/Walls/BodenZukEndO", 1, Content));
            ObjectButtonsFuture.Add(new Button(new Vector2(1150, 150), "Sprites/Walls/BodenZukEndR", 1, Content));
            ObjectButtonsFuture.Add(new Button(new Vector2(1150, 150), "Sprites/Walls/BodenZukEndU", 1, Content));
            ObjectButtonsFuture.Add(new Button(new Vector2(1150, 150), "Sprites/Walls/BodenZuk", 1, Content));
            ObjectButtonsFuture.Add(new Button(new Vector2(1150, 150), "Sprites/Walls/BodenZukUp", 1, Content));
            ObjectButtonsFuture.Add(new Button(new Vector2(1150, 150), "Sprites/Walls/BodenZukUL", 1, Content));
            ObjectButtonsFuture.Add(new Button(new Vector2(1150, 150), "Sprites/Walls/BodenZukUR", 1, Content));
            ObjectButtonsFuture.Add(new Button(new Vector2(1150, 150), "Sprites/Walls/BodenZukOL", 1, Content));
            ObjectButtonsFuture.Add(new Button(new Vector2(1150, 150), "Sprites/Walls/BodenZukOR", 1, Content));
            ObjectButtonsFuture.Add(new Button(new Vector2(1150, 150), "Sprites/Walls/BodenZukTl", 1, Content));
            ObjectButtonsFuture.Add(new Button(new Vector2(1150, 150), "Sprites/Walls/BodenZukTo", 1, Content));
            ObjectButtonsFuture.Add(new Button(new Vector2(1150, 150), "Sprites/Walls/BodenZukTr", 1, Content));
            ObjectButtonsFuture.Add(new Button(new Vector2(1150, 150), "Sprites/Walls/BodenZukTu", 1, Content));
            ObjectButtonsFuture.Add(new Button(new Vector2(1150, 150), "Sprites/Walls/BodenZukKreuz", 1, Content));
            ObjectButtonsFuture.Add(new Button(new Vector2(1150, 170), "Sprites/Objects/Background/Windows/Fenster_zuk1", 14, Content));
            ObjectButtonsFuture.Add(new Button(new Vector2(1150, 170), "Sprites/Objects/Background/Windows/Fenster_zuk2", 14, Content));
            ObjectButtonsFuture.Add(new Button(new Vector2(1150, 170), "Sprites/Objects/Background/Windows/Fenster_zuk5", 14, Content));
            ObjectButtonsFuture.Add(new Button(new Vector2(1150, 170), "Sprites/Objects/Background/Windows/Fenster_zuk3", 14, Content));
            ObjectButtonsFuture.Add(new Button(new Vector2(1150, 170), "Sprites/Objects/Background/Windows/Fenster_zuk4", 14, Content));
            ObjectButtonsFuture.Add(new Button(new Vector2(1150, 170), "Sprites/Objects/Background/Windows/Fenster_zuk16", 14, Content));
            ObjectButtonsFuture.Add(new Button(new Vector2(1150, 170), "Sprites/Objects/Background/Windows/Fenster_zuk15", 14, Content));
            ObjectButtonsFuture.Add(new Button(new Vector2(1150, 170), "Sprites/Objects/Background/Windows/Fenster_zuk9", 14, Content));
            ObjectButtonsFuture.Add(new Button(new Vector2(1150, 170), "Sprites/Objects/Background/Windows/Fenster_zuk8", 14, Content));
            ObjectButtonsFuture.Add(new Button(new Vector2(1150, 170), "Sprites/Objects/Background/Windows/Fenster_zuk10", 14, Content));
            ObjectButtonsFuture.Add(new Button(new Vector2(1150, 170), "Sprites/Objects/Background/Windows/Fenster_zuk7", 14, Content));
            ObjectButtonsFuture.Add(new Button(new Vector2(1150, 170), "Sprites/Objects/Background/Windows/Fenster_zuk12", 14, Content));
            ObjectButtonsFuture.Add(new Button(new Vector2(1150, 170), "Sprites/Objects/Background/Windows/Fenster_zuk13", 14, Content));
            ObjectButtonsFuture.Add(new Button(new Vector2(1150, 170), "Sprites/Objects/Background/Windows/Fenster_zuk11", 14, Content));
            ObjectButtonsFuture.Add(new Button(new Vector2(1150, 170), "Sprites/Objects/Background/Windows/Fenster_zuk14", 14, Content));
            ObjectButtonsFuture.Add(new Button(new Vector2(1150, 170), "Sprites/Objects/Background/Windows/Fenster_zuk", 14, Content));
            ObjectButtonsFuture.Add(new Button(new Vector2(1150, ObjectButtonsFuture.ElementAt<Button>(ObjectButtonsFuture.Count - 1).Position.Y + ObjectButtonsFuture.ElementAt<Button>(ObjectButtonsFuture.Count - 1).Sprite.Height + 10), "Sprites/Walls/HintergrundZuk10", 0, Content));
            ObjectButtonsFuture.Add(new Button(new Vector2(1150, ObjectButtonsFuture.ElementAt<Button>(ObjectButtonsFuture.Count - 1).Position.Y + ObjectButtonsFuture.ElementAt<Button>(ObjectButtonsFuture.Count - 1).Sprite.Height + 10), "Sprites/Objects/Crates/Kiste_Zuk", 2, Content));
            List<string> tp = new List<string>();
            tp.Add("Sprites/Objects/Other/TeleporterAn");
            tp.Add("Sprites/Objects/Other/TeleporterAn28");
            tp.Add("Sprites/Objects/Other/TeleporterAn28");
            tp.Add("Sprites/Objects/Other/TeleporterAn28");
            tp.Add("Sprites/Objects/Other/TeleporterAn28");
            tp.Add("Sprites/Objects/Other/TeleporterAn28");
            tp.Add("Sprites/Objects/Other/TeleporterAn29");
            tp.Add("Sprites/Objects/Other/TeleporterAn29");
            tp.Add("Sprites/Objects/Other/TeleporterAn29");
            tp.Add("Sprites/Objects/Other/TeleporterAn29");
            tp.Add("Sprites/Objects/Other/TeleporterAn29");
            tp.Add("Sprites/Objects/Other/TeleporterAn30");
            tp.Add("Sprites/Objects/Other/TeleporterAn30");
            tp.Add("Sprites/Objects/Other/TeleporterAn30");
            tp.Add("Sprites/Objects/Other/TeleporterAn30");
            tp.Add("Sprites/Objects/Other/TeleporterAn30");
            tp.Add("Sprites/Objects/Other/TeleporterAn01");
            tp.Add("Sprites/Objects/Other/TeleporterAn01");
            tp.Add("Sprites/Objects/Other/TeleporterAn02");
            tp.Add("Sprites/Objects/Other/TeleporterAn03");
            tp.Add("Sprites/Objects/Other/TeleporterAn04");
            tp.Add("Sprites/Objects/Other/TeleporterAn05");
            tp.Add("Sprites/Objects/Other/TeleporterAn06");
            tp.Add("Sprites/Objects/Other/TeleporterAn07");
            tp.Add("Sprites/Objects/Other/TeleporterAn08");
            tp.Add("Sprites/Objects/Other/TeleporterAn09");
            tp.Add("Sprites/Objects/Other/TeleporterAn10");
            tp.Add("Sprites/Objects/Other/TeleporterAn11");
            tp.Add("Sprites/Objects/Other/TeleporterAn12");
            tp.Add("Sprites/Objects/Other/TeleporterAn13");
            tp.Add("Sprites/Objects/Other/TeleporterAn14");
            tp.Add("Sprites/Objects/Other/TeleporterAn15");
            tp.Add("Sprites/Objects/Other/TeleporterAn16");
            tp.Add("Sprites/Objects/Other/TeleporterAn17");
            tp.Add("Sprites/Objects/Other/TeleporterAn18");
            tp.Add("Sprites/Objects/Other/TeleporterAn19");
            tp.Add("Sprites/Objects/Other/TeleporterAn20");
            tp.Add("Sprites/Objects/Other/TeleporterAn21");
            tp.Add("Sprites/Objects/Other/TeleporterAn22");
            tp.Add("Sprites/Objects/Other/TeleporterAn23");
            tp.Add("Sprites/Objects/Other/TeleporterAn24");
            tp.Add("Sprites/Objects/Other/TeleporterAn25");
            tp.Add("Sprites/Objects/Other/TeleporterAn26");
            tp.Add("Sprites/Objects/Other/TeleporterAn27");
            ObjectButtonsFuture.Add(new Button(new Vector2(1150, ObjectButtonsFuture.ElementAt<Button>(ObjectButtonsFuture.Count - 1).Position.Y + ObjectButtonsFuture.ElementAt<Button>(ObjectButtonsFuture.Count - 1).Sprite.Height + 10), tp, 25, 14, 8, Content));
            List<string> nonhtp = new List<string>();
            nonhtp.Add("Sprites/Objects/Other/TeleporterAn");
            nonhtp.Add("Sprites/Objects/Other/TeleporterAn30");
            nonhtp.Add("Sprites/Objects/Other/TeleporterAn01");
            nonhtp.Add("Sprites/Objects/Other/TeleporterAn01");
            nonhtp.Add("Sprites/Objects/Other/TeleporterAn02");
            nonhtp.Add("Sprites/Objects/Other/TeleporterAn03");
            nonhtp.Add("Sprites/Objects/Other/TeleporterAn04");
            nonhtp.Add("Sprites/Objects/Other/TeleporterAn05");
            nonhtp.Add("Sprites/Objects/Other/TeleporterAn06");
            nonhtp.Add("Sprites/Objects/Other/TeleporterAn07");
            nonhtp.Add("Sprites/Objects/Other/TeleporterAn08");
            nonhtp.Add("Sprites/Objects/Other/TeleporterAn09");
            nonhtp.Add("Sprites/Objects/Other/TeleporterAn10");
            nonhtp.Add("Sprites/Objects/Other/TeleporterAn11");
            nonhtp.Add("Sprites/Objects/Other/TeleporterAn12");
            nonhtp.Add("Sprites/Objects/Other/TeleporterAn13");
            nonhtp.Add("Sprites/Objects/Other/TeleporterAn14");
            nonhtp.Add("Sprites/Objects/Other/TeleporterAn15");
            nonhtp.Add("Sprites/Objects/Other/TeleporterAn16");
            nonhtp.Add("Sprites/Objects/Other/TeleporterAn17");
            nonhtp.Add("Sprites/Objects/Other/TeleporterAn18");
            nonhtp.Add("Sprites/Objects/Other/TeleporterAn19");
            nonhtp.Add("Sprites/Objects/Other/TeleporterAn20");
            nonhtp.Add("Sprites/Objects/Other/TeleporterAn21");
            nonhtp.Add("Sprites/Objects/Other/TeleporterAn22");
            nonhtp.Add("Sprites/Objects/Other/TeleporterAn23");
            nonhtp.Add("Sprites/Objects/Other/TeleporterAn24");
            nonhtp.Add("Sprites/Objects/Other/TeleporterAn25");
            nonhtp.Add("Sprites/Objects/Other/TeleporterAn26");
            nonhtp.Add("Sprites/Objects/Other/TeleporterAn27");
            ObjectButtonsFuture.Add(new Button(new Vector2(1150, ObjectButtonsFuture.ElementAt<Button>(ObjectButtonsFuture.Count - 1).Position.Y + ObjectButtonsFuture.ElementAt<Button>(ObjectButtonsFuture.Count - 1).Sprite.Height + 10), nonhtp, 25, 0, 8, Content));
            List<string> sf = new List<string>();
            sf.Add("Sprites/Objects/Switches/SchZukOff");
            sf.Add("Sprites/Objects/Switches/SchZukOn");
            ObjectButtonsFuture.Add(new Button(new Vector2(1150, ObjectButtonsFuture.ElementAt<Button>(ObjectButtonsFuture.Count - 1).Position.Y + ObjectButtonsFuture.ElementAt<Button>(ObjectButtonsFuture.Count - 1).Sprite.Height + 10), sf, 100, 0, 3, Content));
            ObjectButtonsFuture.Add(new Button(new Vector2(1150, ObjectButtonsFuture.ElementAt<Button>(ObjectButtonsFuture.Count - 1).Position.Y + ObjectButtonsFuture.ElementAt<Button>(ObjectButtonsFuture.Count - 1).Sprite.Height + 10), "Sprites/Objects/Pillars/PfoZuk", 2, Content));
            List<string> df = new List<string>();
            df.Add("Sprites/Objects/Doors/TuerZukunft");
            df.Add("Sprites/Objects/Doors/TuerZukunftOffen");
            ObjectButtonsFuture.Add(new Button(new Vector2(1150, ObjectButtonsFuture.ElementAt<Button>(ObjectButtonsFuture.Count - 1).Position.Y + ObjectButtonsFuture.ElementAt<Button>(ObjectButtonsFuture.Count - 1).Sprite.Height + 10), df, 100, 0, 2, Content));
            List<string> lb = new List<string>();
            lb.Add("Sprites/Objects/Actors/LbOff");
            lb.Add("Sprites/Objects/Actors/LbOn");
            lb.Add("Sprites/Objects/Actors/LbOn1");
            lb.Add("Sprites/Objects/Actors/LbOn2");
            lb.Add("Sprites/Objects/Actors/LbOn3");
            lb.Add("Sprites/Objects/Actors/LbOn4");
            lb.Add("Sprites/Objects/Actors/LbOn5");
            lb.Add("Sprites/Objects/Actors/LbOn6");
            lb.Add("Sprites/Objects/Actors/LbOn7");
            ObjectButtonsFuture.Add(new Button(new Vector2(1150, ObjectButtonsFuture.ElementAt<Button>(ObjectButtonsFuture.Count - 1).Position.Y + ObjectButtonsFuture.ElementAt<Button>(ObjectButtonsFuture.Count - 1).Sprite.Height + 10), lb, 100, 0, 2, Content));
            List<string> ls = new List<string>();
            ls.Add("Sprites/Objects/Switches/SchZukTast_Off");
            ls.Add("Sprites/Objects/Switches/SchZukTast_On");
            ObjectButtonsFuture.Add(new Button(new Vector2(1150, ObjectButtonsFuture.ElementAt<Button>(ObjectButtonsFuture.Count - 1).Position.Y + ObjectButtonsFuture.ElementAt<Button>(ObjectButtonsFuture.Count - 1).Sprite.Height + 10), ls, 100, 0, 5, Content));
            List<string> tsf = new List<string>();
            tsf.Add("Sprites/Objects/Switches/SchZukZ_Off");
            tsf.Add("Sprites/Objects/Switches/SchZukZ_On");
            tsf.Add("Sprites/Objects/Switches/SchZukZ_On1");
            tsf.Add("Sprites/Objects/Switches/SchZukZ_On2");
            tsf.Add("Sprites/Objects/Switches/SchZukZ_On3");
            tsf.Add("Sprites/Objects/Switches/SchZukZ_On4");
            tsf.Add("Sprites/Objects/Switches/SchZukZ_On5");
            tsf.Add("Sprites/Objects/Switches/SchZukZ_On6");
            tsf.Add("Sprites/Objects/Switches/SchZukZ_On7");
            tsf.Add("Sprites/Objects/Switches/SchZukZ_On8");
            tsf.Add("Sprites/Objects/Switches/SchZukZ_On9");
            tsf.Add("Sprites/Objects/Switches/SchZukZ_On10");
            tsf.Add("Sprites/Objects/Switches/SchZukZ_On11");
            tsf.Add("Sprites/Objects/Switches/SchZukZ_On12");
            tsf.Add("Sprites/Objects/Switches/SchZukZ_On13");
            tsf.Add("Sprites/Objects/Switches/SchZukZ_On14");
            tsf.Add("Sprites/Objects/Switches/SchZukZ_On15");
            tsf.Add("Sprites/Objects/Switches/SchZukZ_On16");
            tsf.Add("Sprites/Objects/Switches/SchZukZ_On");
            tsf.Add("Sprites/Objects/Switches/SchZukZ_On");
            ObjectButtonsFuture.Add(new Button(new Vector2(1150, ObjectButtonsFuture.ElementAt<Button>(ObjectButtonsFuture.Count - 1).Position.Y + ObjectButtonsFuture.ElementAt<Button>(ObjectButtonsFuture.Count - 1).Sprite.Height + 10), tsf, 100, 0, 12, Content));
            ObjectButtonsFuture.Add(new Button(new Vector2(1150, ObjectButtonsFuture.ElementAt<Button>(ObjectButtonsFuture.Count - 1).Position.Y + ObjectButtonsFuture.ElementAt<Button>(ObjectButtonsFuture.Count - 1).Sprite.Height + 10), "Sprites/Objects/Background/Lampe", 2, Content));
            List<string> cfl = new List<string>();
            cfl.Add("Sprites/Objects/Background/stuhl_l");
            cfl.Add("Sprites/Objects/Background/stuhl_l");
            cfl.Add("Sprites/Objects/Background/stuhl_l1");
            cfl.Add("Sprites/Objects/Background/stuhl_l2");
            ObjectButtonsFuture.Add(new Button(new Vector2(1150, ObjectButtonsFuture.ElementAt<Button>(ObjectButtonsFuture.Count - 1).Position.Y + ObjectButtonsFuture.ElementAt<Button>(ObjectButtonsFuture.Count - 1).Sprite.Height + 10), cfl, 500, 2, 2, Content));
            List<string> cfr = new List<string>();
            cfr.Add("Sprites/Objects/Background/stuhl_r");
            cfr.Add("Sprites/Objects/Background/stuhl_r");
            cfr.Add("Sprites/Objects/Background/stuhl_r1");
            cfr.Add("Sprites/Objects/Background/stuhl_r2");
            ObjectButtonsFuture.Add(new Button(new Vector2(1150, ObjectButtonsFuture.ElementAt<Button>(ObjectButtonsFuture.Count - 1).Position.Y + ObjectButtonsFuture.ElementAt<Button>(ObjectButtonsFuture.Count - 1).Sprite.Height + 10), cfr, 500, 2, 2, Content));
            List<string> taf = new List<string>();
            taf.Add("Sprites/Objects/Background/1");
            taf.Add("Sprites/Objects/Background/1");
            taf.Add("Sprites/Objects/Background/2");
            taf.Add("Sprites/Objects/Background/3");
            ObjectButtonsFuture.Add(new Button(new Vector2(1150, ObjectButtonsFuture.ElementAt<Button>(ObjectButtonsFuture.Count - 1).Position.Y + ObjectButtonsFuture.ElementAt<Button>(ObjectButtonsFuture.Count - 1).Sprite.Height + 10), taf, 500, 2, 2, Content));
            List<string> h1 = new List<string>();
            h1.Add("Sprites/Objects/Background/Kunst_ZuK1");
            h1.Add("Sprites/Objects/Background/Kunst_ZuK_egg1");
            h1.Add("Sprites/Objects/Background/Kunst_ZuK_egg2");
            ObjectButtonsFuture.Add(new Button(new Vector2(1150, ObjectButtonsFuture.ElementAt<Button>(ObjectButtonsFuture.Count - 1).Position.Y + ObjectButtonsFuture.ElementAt<Button>(ObjectButtonsFuture.Count - 1).Sprite.Height + 10), h1, 1000, 1, 3, Content));
            List<string> h2 = new List<string>();
            h2.Add("Sprites/Objects/Background/Kunst_ZuK2");
            h2.Add("Sprites/Objects/Background/Kunst_ZuK_egg1");
            h2.Add("Sprites/Objects/Background/Kunst_ZuK_egg2");
            ObjectButtonsFuture.Add(new Button(new Vector2(1150, ObjectButtonsFuture.ElementAt<Button>(ObjectButtonsFuture.Count - 1).Position.Y + ObjectButtonsFuture.ElementAt<Button>(ObjectButtonsFuture.Count - 1).Sprite.Height + 10), h2, 1000, 1, 3, Content));
            ObjectButtonsFuture.Add(new Button(new Vector2(1150, ObjectButtonsFuture.ElementAt<Button>(ObjectButtonsFuture.Count - 1).Position.Y + ObjectButtonsFuture.ElementAt<Button>(ObjectButtonsFuture.Count - 1).Sprite.Height + 10), c, 25, 21, 9, Content));
            ObjectButtonsFuture.Add(new Button(new Vector2(1150, ObjectButtonsFuture.ElementAt<Button>(ObjectButtonsFuture.Count - 1).Position.Y + ObjectButtonsFuture.ElementAt<Button>(ObjectButtonsFuture.Count - 1).Sprite.Height + 10), ceg, 25, 21, 9, Content));
            ObjectButtonsFuture.Add(new Button(new Vector2(1150, ObjectButtonsFuture.ElementAt<Button>(ObjectButtonsFuture.Count - 1).Position.Y + ObjectButtonsFuture.ElementAt<Button>(ObjectButtonsFuture.Count - 1).Sprite.Height + 10), d, 25, 16, 13, Content));
            ObjectButtonsFuture.Add(new Button(new Vector2(1150, ObjectButtonsFuture.ElementAt<Button>(ObjectButtonsFuture.Count - 1).Position.Y + ObjectButtonsFuture.ElementAt<Button>(ObjectButtonsFuture.Count - 1).Sprite.Height + 10), "Sprites/Objects/Gameobjects/schluessel 1", 10, Content));
            ObjectButtonsFuture.Add(new Button(new Vector2(1150, ObjectButtonsFuture.ElementAt<Button>(ObjectButtonsFuture.Count - 1).Position.Y + ObjectButtonsFuture.ElementAt<Button>(ObjectButtonsFuture.Count - 1).Sprite.Height + 10), "Sprites/Objects/Gameobjects/schluessel 2", 10, Content));
            ObjectButtonsFuture.Add(new Button(new Vector2(1150, ObjectButtonsFuture.ElementAt<Button>(ObjectButtonsFuture.Count - 1).Position.Y + ObjectButtonsFuture.ElementAt<Button>(ObjectButtonsFuture.Count - 1).Sprite.Height + 10), "Sprites/Objects/Gameobjects/schluessel 3", 10, Content));
            ObjectButtonsFuture.Add(new Button(new Vector2(1150, ObjectButtonsFuture.ElementAt<Button>(ObjectButtonsFuture.Count - 1).Position.Y + ObjectButtonsFuture.ElementAt<Button>(ObjectButtonsFuture.Count - 1).Sprite.Height + 10), ed, 100, 0, 15, Content));
            ObjectButtonsFuture.Add(new Button(new Vector2(1150, ObjectButtonsFuture.ElementAt<Button>(ObjectButtonsFuture.Count - 1).Position.Y + ObjectButtonsFuture.ElementAt<Button>(ObjectButtonsFuture.Count - 1).Sprite.Height + 10), "Sprites/Objects/Gameobjects/Trigger", 16, Content));
            ObjectButtonsFuture.Add(new Button(new Vector2(1150, ObjectButtonsFuture.ElementAt<Button>(ObjectButtonsFuture.Count - 1).Position.Y + ObjectButtonsFuture.ElementAt<Button>(ObjectButtonsFuture.Count - 1).Sprite.Height + 10), "Sprites/Objects/Gameobjects/monokel", 17, Content));

            ObjectButtonsPast[0].Pressed = true;
            ObjectButtonsPresent[0].Pressed = true;
            ObjectButtonsFuture[0].Pressed = true;

            DropDownButtons = new List<Button>();
            DropDownButtons.Add(new Button(Vector2.Zero, "Sprites/UI/StaticButtonFalse", 0, Content));
            DropDownButtons.Add(new Button(Vector2.Zero, "Sprites/UI/PortableButtonFalse", 0, Content));
            DropDownButtons.Add(new Button(Vector2.Zero, "Sprites/UI/ActivatedButtonFalse", 0, Content));
            DropDownButtons.Add(new Button(Vector2.Zero, "Sprites/UI/TimeButton", 0, Content));

            ControlBarButtons = new List<Button>();
            ControlBarButtons.Add(new Button(Vector2.Zero, "Sprites/UI/NewButton", 0, Content));
            ControlBarButtons.Add(new Button(new Vector2(49, 0), "Sprites/UI/LoadButton", 0, Content));
            ControlBarButtons.Add(new Button(new Vector2(98, 0), "Sprites/UI/SaveButton", 0, Content));
            ControlBarButtons.Add(new Button(new Vector2(147, 0), "Sprites/UI/PlayerStartButton", 0, Content));
            ControlBarButtons.Add(new Button(new Vector2(196, 0), "Sprites/UI/TestButton", 0, Content));

            StartMenuNewButton = new Button(new Vector2(graphics.GraphicsDevice.Viewport.Width / 2 - 32, graphics.GraphicsDevice.Viewport.Height / 2 - 30), "Sprites/UI/NewButton2", 0, Content);
            StartMenuLoadButton = new Button(new Vector2(graphics.GraphicsDevice.Viewport.Width / 2 - 32, graphics.GraphicsDevice.Viewport.Height / 2 + 10), "Sprites/UI/LoadingButton", 0, Content);
            NewMenuCreateButton = new Button(new Vector2(graphics.GraphicsDevice.Viewport.Width / 2 + 155, graphics.GraphicsDevice.Viewport.Height / 2 + 222), "Sprites/UI/CreateButton", 0, Content);
            LoadMenuLoadButton = new Button(new Vector2(graphics.GraphicsDevice.Viewport.Width / 2 + 155, graphics.GraphicsDevice.Viewport.Height / 2 + 228), "Sprites/UI/LoadingButton", 0, Content);
            CloseButton = new Button(new Vector2(graphics.GraphicsDevice.Viewport.Width / 2 + 222, graphics.GraphicsDevice.Viewport.Height / 2 - 250), "Sprites/UI/CloseButton", 0, Content);
            PastRadioButton = new Button(new Vector2(graphics.GraphicsDevice.Viewport.Width - 160, 20), "Sprites/UI/RadioButton", 0, Content);
            PresentRadioButton = new Button(new Vector2(graphics.GraphicsDevice.Viewport.Width - 160, 60), "Sprites/UI/RadioButton", 0, Content);
            FutureRadioButton = new Button(new Vector2(graphics.GraphicsDevice.Viewport.Width - 160, 100), "Sprites/UI/RadioButton", 0, Content);


            DotPositions = new List<Vector2>();

            Selection = new List<GameObject>();

            Pos = Vector2.Zero;
            Selected = null;
            Held = null;
            LClicked = false;
            RClicked = false;
            MClicked = false;
            LinkMode = false;
            DropMenu = false;
            NewMenu = false;
            LoadMenu = false;
            StartMenu = true;
            ClickPos = Vector2.Zero;
            NameString = "";
            PaClickPos = Vector2.Zero;
            PrClickPos = Vector2.Zero;
            FuClickPos = Vector2.Zero;
            TypeMode = false;
            KeyPressed = false;
            Timer = 0;
            SetPlayerStart = false;
            RedGhost = false;
            PrevPos = Vector2.Zero;
            SelectStart = Vector2.Zero;
            LastMousePos = Vector2.Zero;
            UpdatingLink = 0;

            ContentDirectory = Environment.CurrentDirectory;
            ContentDirectory = Directory.GetParent(ContentDirectory).FullName;
            ContentDirectory = Directory.GetParent(ContentDirectory).FullName;
            ContentDirectory = Directory.GetParent(ContentDirectory).FullName;
            ContentDirectory = Directory.GetParent(ContentDirectory).FullName;
            ContentDirectory += "/The Secret CastleContent";

            Player = null;


            CurrentLevel = new Level("new", "Sprites/Backgrounds/Past/Background", "Sprites/Backgrounds/Present/Background_Present", "Sprites/Backgrounds/Future/Background_Future", Content);




            Mode = 0;
            lastScrollValue = 0;

            Cam = new Camera();
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
            if (this.IsActive)
            {
                // Allows the game to exit
                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                    this.Exit();

                MouseState mouseState = Mouse.GetState();
                KeyboardState keyState = Keyboard.GetState();


                //Updating Buttons
                foreach (Button b in UITimeButtons)
                {
                    b.Position = new Vector2(graphics.GraphicsDevice.Viewport.Width - 130, b.Position.Y);
                    b.Update(UITimeButtons);
                }
                foreach (Button b in ControlBarButtons)
                {
                    b.Pressed = false;
                    b.Update(new List<Button>());
                }
                foreach (Button b in ObjectButtonsPast)
                {
                    b.Position = new Vector2(graphics.GraphicsDevice.Viewport.Width - 85 - b.Sprite.Width / 2, b.Position.Y);
                    b.Update(ObjectButtonsPast);
                }
                foreach (Button b in ObjectButtonsPresent)
                {
                    b.Position = new Vector2(graphics.GraphicsDevice.Viewport.Width - 85 - b.Sprite.Width / 2, b.Position.Y);
                    b.Update(ObjectButtonsPresent);
                }
                foreach (Button b in ObjectButtonsFuture)
                {
                    b.Position = new Vector2(graphics.GraphicsDevice.Viewport.Width - 85 - b.Sprite.Width / 2, b.Position.Y);
                    b.Update(ObjectButtonsFuture);
                }
                if (DropMenu)
                {
                    foreach (Button b in DropDownButtons)
                    {
                        b.Pressed = false;
                        if (b.Sprite == DropDownButtons.ElementAt<Button>(3).Sprite)
                        {
                            if (Selected.Sprites != null)
                            {
                                b.Update(new List<Button>());
                            }
                        }
                        else
                        {
                            b.Update(new List<Button>());
                        }
                    }
                }
                PastRadioButton.Pressed = false;
                PastRadioButton.Position = new Vector2(graphics.GraphicsDevice.Viewport.Width - 160, 25);
                PastRadioButton.Update(new List<Button>());
                if (PastRadioButton.Pressed & !LClicked)
                {
                    LClicked = true;
                    if (PastRadioButton.Sprite == RadioButtonF)
                    {
                        PastRadioButton.Sprite = RadioButtonT;
                    }
                    else
                    {
                        PastRadioButton.Sprite = RadioButtonF;
                    }
                }

                PresentRadioButton.Pressed = false;
                PresentRadioButton.Position = new Vector2(graphics.GraphicsDevice.Viewport.Width - 160, 65);
                PresentRadioButton.Update(new List<Button>());
                if (PresentRadioButton.Pressed & !LClicked)
                {
                    LClicked = true;
                    if (PresentRadioButton.Sprite == RadioButtonF)
                    {
                        PresentRadioButton.Sprite = RadioButtonT;
                    }
                    else
                    {
                        PresentRadioButton.Sprite = RadioButtonF;
                    }
                }

                FutureRadioButton.Pressed = false;
                FutureRadioButton.Position = new Vector2(graphics.GraphicsDevice.Viewport.Width - 160, 105);
                FutureRadioButton.Update(new List<Button>());
                if (FutureRadioButton.Pressed & !LClicked)
                {
                    LClicked = true;
                    if (FutureRadioButton.Sprite == RadioButtonF)
                    {
                        FutureRadioButton.Sprite = RadioButtonT;
                    }
                    else
                    {
                        FutureRadioButton.Sprite = RadioButtonF;
                    }
                }



                if (ControlBarButtons.ElementAt<Button>(0).Pressed & !LClicked)
                {
                    LClicked = true;
                    NewMenu = true;
                }
                if (ControlBarButtons.ElementAt<Button>(1).Pressed & !LClicked)
                {
                    LClicked = true;
                    LoadMenu = true;
                }
                if (ControlBarButtons.ElementAt<Button>(2).Pressed & !LClicked)
                {
                    LClicked = true;
                    SaveLevel();
                }
                if (ControlBarButtons.ElementAt<Button>(3).Pressed & !LClicked)
                {
                    SetPlayerStart = true;
                }
                if (ControlBarButtons.ElementAt<Button>(4).Pressed & !LClicked)
                {
                    SaveLevel();

                    Player = new Character(CurrentLevel.PlayerStart, Content);
                }

                if (SetPlayerStart & !ControlBarButtons.ElementAt<Button>(3).MouseOver)
                {
                    if (mouseState.LeftButton == ButtonState.Pressed)
                    {
                        if (UITimeButtons.ElementAt<Button>(0).Pressed)
                        {
                            CurrentLevel.PlayerStart = new Vector3(new Vector2(mouseState.X, mouseState.Y) - Cam.Position, 0);
                        }
                        if (UITimeButtons.ElementAt<Button>(1).Pressed)
                        {
                            CurrentLevel.PlayerStart = new Vector3(new Vector2(mouseState.X, mouseState.Y) - Cam.Position, 1);
                        }
                        if (UITimeButtons.ElementAt<Button>(2).Pressed)
                        {
                            CurrentLevel.PlayerStart = new Vector3(new Vector2(mouseState.X, mouseState.Y) - Cam.Position, 2);
                        }
                    }
                }



                //--------------------------New Menu Screen----------------------------
                else if (NewMenu)
                {
                    CloseButton.Pressed = false;
                    CloseButton.Update(new List<Button>());
                    CloseButton.Position = new Vector2(graphics.GraphicsDevice.Viewport.Width / 2 + 222, graphics.GraphicsDevice.Viewport.Height / 2 - 250);
                    NewMenuCreateButton.Pressed = false;
                    NewMenuCreateButton.Update(new List<Button>());
                    NewMenuCreateButton.Position = new Vector2(graphics.GraphicsDevice.Viewport.Width / 2 + 155, graphics.GraphicsDevice.Viewport.Height / 2 + 222);

                    if (TypeMode)
                    {
                        Timer += gameTime.ElapsedGameTime.Milliseconds;
                        if (keyState.GetPressedKeys().Length > 0)
                        {
                            string[] chars = new string[keyState.GetPressedKeys().Length];
                            int j = -1;
                            for (int i = 0; i < keyState.GetPressedKeys().Length; i++)
                            {
                                if (keyState.GetPressedKeys()[i].ToString().Length == 1 && (int)Convert.ToChar(keyState.GetPressedKeys()[i].ToString()) > 48 & (int)Convert.ToChar(keyState.GetPressedKeys()[i].ToString()) < 122)
                                {
                                    chars[i] = keyState.GetPressedKeys()[i].ToString();
                                    j = i;
                                }
                            }
                            if ((keyState.IsKeyDown(Keys.LeftShift) | keyState.IsKeyDown(Keys.RightShift)) & Timer > 100 & j != -1 & NameString.Length < 30)
                            {
                                Timer = 0;
                                NameString += chars[j].ToUpper();
                            }
                            else if (Timer > 100 & j != -1 & NameString.Length < 30)
                            {
                                Timer = 0;
                                NameString += chars[j].ToLower();
                            }

                            if (keyState.IsKeyDown(Keys.Back) & Timer > 100 & NameString.Length > 0)
                            {
                                Timer = 0;
                                NameString = NameString.Remove(NameString.Length - 1);
                            }

                            if (keyState.IsKeyDown(Keys.Space) & Timer > 100 & NameString.Length < 30)
                            {
                                Timer = 0;
                                NameString += " ";
                            }
                        }
                        if (keyState.GetPressedKeys().Length == 0)
                        {
                            Timer = 101;
                        }
                    }

                    if (mouseState.LeftButton == ButtonState.Pressed)
                    {
                        if (mouseState.Y > graphics.GraphicsDevice.Viewport.Height / 2 - 222 & mouseState.Y < graphics.GraphicsDevice.Viewport.Height / 2 - 194)
                        {
                            TypeMode = true;
                        }
                        else
                        {
                            TypeMode = false;
                        }

                        if (mouseState.Y > graphics.GraphicsDevice.Viewport.Height / 2 - 168 & mouseState.Y < graphics.GraphicsDevice.Viewport.Height / 2 - 58)
                        {
                            PaClickPos = new Vector2(mouseState.X, mouseState.Y);
                        }
                        if (mouseState.Y > graphics.GraphicsDevice.Viewport.Height / 2 - 36 & mouseState.Y < graphics.GraphicsDevice.Viewport.Height / 2 + 76)
                        {
                            PrClickPos = new Vector2(mouseState.X, mouseState.Y);
                        }
                        if (mouseState.Y > graphics.GraphicsDevice.Viewport.Height / 2 + 98 & mouseState.Y < graphics.GraphicsDevice.Viewport.Height / 2 + 210)
                        {
                            FuClickPos = new Vector2(mouseState.X, mouseState.Y);
                        }
                    }

                    if (NewMenuCreateButton.Pressed)
                    {
                        string[] paths = Directory.GetFiles(Content.RootDirectory + "/Sprites/Backgrounds/Past");
                        string PaPath = "";
                        string PrPath = "";
                        string FuPath = "";

                        for (int i = 0; i < paths.Length; i++)
                        {
                            if (PaClickPos.Y > graphics.GraphicsDevice.Viewport.Height / 2 - 168 + i * 30 & PaClickPos.Y < graphics.GraphicsDevice.Viewport.Height / 2 - 138 + i * 30)
                            {
                                PaPath = "Sprites/Backgrounds/Past/" + Path.GetFileNameWithoutExtension(paths[i]);
                                break;
                            }
                        }


                        paths = Directory.GetFiles(Content.RootDirectory + "/Sprites/Backgrounds/Present");

                        for (int i = 0; i < paths.Length; i++)
                        {
                            if (PrClickPos.Y > graphics.GraphicsDevice.Viewport.Height / 2 - 34 + i * 30 & PrClickPos.Y < graphics.GraphicsDevice.Viewport.Height / 2 - 4 + i * 30)
                            {
                                PrPath = "Sprites/Backgrounds/Present/" + Path.GetFileNameWithoutExtension(paths[i]);
                                break;
                            }
                        }


                        paths = Directory.GetFiles(Content.RootDirectory + "/Sprites/Backgrounds/Future");

                        for (int i = 0; i < paths.Length; i++)
                        {
                            if (FuClickPos.Y > graphics.GraphicsDevice.Viewport.Height / 2 + 98 + i * 30 & FuClickPos.Y < graphics.GraphicsDevice.Viewport.Height / 2 + 128 + i * 30)
                            {
                                FuPath = "Sprites/Backgrounds/Future/" + Path.GetFileNameWithoutExtension(paths[i]);
                                break;
                            }
                        }


                        CurrentLevel = new Level(NameString, PaPath, PrPath, FuPath, Content);

                        StartMenu = false;
                        NewMenu = false;
                        Window.Title = "The Secret Castle Editor - " + CurrentLevel.Name;
                    }

                    if (CloseButton.Pressed)
                    {
                        NewMenu = false;
                    }
                }



                //--------------------------Load Menu Screen----------------------------
                if (LoadMenu)
                {
                    CloseButton.Pressed = false;
                    CloseButton.Update(new List<Button>());
                    CloseButton.Position = new Vector2(graphics.GraphicsDevice.Viewport.Width / 2 + 222, graphics.GraphicsDevice.Viewport.Height / 2 - 250);
                    LoadMenuLoadButton.Pressed = false;
                    LoadMenuLoadButton.Update(new List<Button>());
                    LoadMenuLoadButton.Position = new Vector2(graphics.GraphicsDevice.Viewport.Width / 2 + 155, graphics.GraphicsDevice.Viewport.Height / 2 + 228);
                    if (mouseState.LeftButton == ButtonState.Pressed & mouseState.Y > graphics.GraphicsDevice.Viewport.Height / 2 - 225 & mouseState.Y < graphics.GraphicsDevice.Viewport.Height / 2 + 220)
                    {
                        ClickPos = new Vector2(mouseState.X, mouseState.Y);
                    }

                    if (LoadMenuLoadButton.Pressed)
                    {
                        string[] paths = Directory.GetFiles(Environment.CurrentDirectory + "/Levels/AllPlayers");

                        for (int i = 0; i < paths.Length; i++)
                        {
                            if (ClickPos.Y > graphics.GraphicsDevice.Viewport.Height / 2 - 223 + i * 30 & ClickPos.Y < graphics.GraphicsDevice.Viewport.Height / 2 - 193 + i * 30)
                            {
                                CurrentLevel = LoadLevel(Path.GetFileName(paths[i]));
                                break;
                            }
                        }

                        StartMenu = false;
                        LoadMenu = false;
                        Window.Title = "The Secret Castle Editor - " + CurrentLevel.Name;
                    }

                    if (CloseButton.Pressed)
                    {
                        LoadMenu = false;
                    }
                }



                //--------------------------Start Menu Screen----------------------------
                if (StartMenu)
                {
                    if (!NewMenu & !LoadMenu)
                    {
                        StartMenuNewButton.Pressed = false;
                        StartMenuLoadButton.Pressed = false;
                        StartMenuNewButton.Update(new List<Button>());
                        StartMenuLoadButton.Update(new List<Button>());
                        StartMenuNewButton.Position = new Vector2(graphics.GraphicsDevice.Viewport.Width / 2 - 32, graphics.GraphicsDevice.Viewport.Height / 2 - 30);
                        StartMenuLoadButton.Position = new Vector2(graphics.GraphicsDevice.Viewport.Width / 2 - 32, graphics.GraphicsDevice.Viewport.Height / 2 + 10);

                        if (StartMenuNewButton.Pressed)
                        {
                            NewMenu = true;
                        }

                        if (StartMenuLoadButton.Pressed)
                        {
                            LoadMenu = true;
                        }
                    }
                }
                else if (Player == null)
                {


                    //Updating Objects
                    if (UITimeButtons.ElementAt<Button>(0).Pressed)
                    {
                        foreach (GameObject o in CurrentLevel.Past)
                        {
                            o.Update(Player, CurrentLevel.Past, CurrentLevel.PastLinks, CollisionCheck, gameTime, Cursor, CollisionCheck);
                        }
                    }
                    if (UITimeButtons.ElementAt<Button>(1).Pressed)
                    {
                        foreach (GameObject o in CurrentLevel.Present)
                        {
                            o.Update(Player, CurrentLevel.Present, CurrentLevel.PresentLinks, CollisionCheck, gameTime, Cursor, CollisionCheck);
                        }
                    }
                    if (UITimeButtons.ElementAt<Button>(2).Pressed)
                    {
                        foreach (GameObject o in CurrentLevel.Future)
                        {
                            o.Update(Player, CurrentLevel.Future, CurrentLevel.FutureLinks, CollisionCheck, gameTime, Cursor, CollisionCheck);
                        }
                    }



                    Pos = new Vector2(mouseState.X - Cam.Position.X, mouseState.Y - Cam.Position.Y);
                    bool fblocked = false;
                    bool mblocked = false;
                    bool bblocked = false;
                    GameObject temp = null;



                    //Check if an Object is at Mouse Position
                    List<GameObject> tList = null;
                    if (UITimeButtons.ElementAt<Button>(0).Pressed)
                    {
                        tList = CurrentLevel.Past;
                    }
                    if (UITimeButtons.ElementAt<Button>(1).Pressed)
                    {
                        tList = CurrentLevel.Present;
                    }
                    if (UITimeButtons.ElementAt<Button>(2).Pressed)
                    {
                        tList = CurrentLevel.Future;
                    }
                    foreach (GameObject o in tList)
                    {
                        if (CollisionCheck(Cursor, Pos, o.Sprite, o.Position, 0))
                        {
                            if (o.Layer == 0)
                            {
                                fblocked = true;
                            }
                            if (o.Layer == 1)
                            {
                                mblocked = true;
                            }
                            if (o.Layer == 2)
                            {
                                bblocked = true;
                            }
                            temp = o;
                            if (o.Type != 0)
                            {
                                break;
                            }
                        }
                    }


                    if (Mode == 2)
                    {
                        RedGhost = false;

                        List<GameObject> list = null;
                        Texture2D sprite = null;
                        if (UITimeButtons.ElementAt<Button>(0).Pressed)
                        {
                            list = CurrentLevel.Past;
                            sprite = ObjectButtonsPast.Find(b => b.Pressed).Sprite;
                        }
                        if (UITimeButtons.ElementAt<Button>(1).Pressed)
                        {
                            list = CurrentLevel.Present;
                            sprite = ObjectButtonsPresent.Find(b => b.Pressed).Sprite;
                        }
                        if (UITimeButtons.ElementAt<Button>(2).Pressed)
                        {
                            list = CurrentLevel.Future;
                            sprite = ObjectButtonsFuture.Find(b => b.Pressed).Sprite;
                        }
                        foreach (GameObject o in list)
                        {
                            if (CollisionCheck(sprite, Pos, o.Sprite, o.Position, 0) && ((o.Layer != 2 && o.Type != 0) | (o.Layer != 2)))
                            {
                                RedGhost = true;
                                break;
                            }
                        }
                    }


                    if (Mode == 3)
                    {
                        Button tmp = null;
                        if (UITimeButtons.FindIndex(b => b.Pressed) == 0)
                        {
                            tmp = ObjectButtonsPast.Find(b => b.Pressed);
                        }
                        if (UITimeButtons.FindIndex(b => b.Pressed) == 1)
                        {
                            tmp = ObjectButtonsPresent.Find(b => b.Pressed);
                        }
                        if (UITimeButtons.FindIndex(b => b.Pressed) == 2)
                        {
                            tmp = ObjectButtonsFuture.Find(b => b.Pressed);
                        }
                        //Adjusting Position to Grid
                        if (!LClicked && !MClicked && !RClicked)
                        {
                            if (Pos.X % 16 != 0)
                            {
                                if (Pos.X % 16 <= 8)
                                {
                                    while (Pos.X % 16 != 0)
                                    {
                                        Pos.X--;
                                    }
                                }
                                if (Pos.X % 16 > 8)
                                {
                                    while (Pos.X % 16 != 0)
                                    {
                                        Pos.X++;
                                    }
                                }
                            }
                            if (Pos.Y % 16 != 0)
                            {
                                if (Pos.Y % 16 <= 8)
                                {
                                    while (Pos.Y % 16 != 0)
                                    {
                                        Pos.Y--;
                                    }
                                }
                                if (Pos.Y % 16 > 8)
                                {
                                    while (Pos.Y % 16 != 0)
                                    {
                                        Pos.Y++;
                                    }
                                }
                            }
                            PrevPos = Pos;
                        }
                        else
                        {
                            int dir = 0;
                            float angle = MathHelper.ToDegrees((float)Math.Atan2(-(PrevPos.X - Pos.X), -(PrevPos.Y - Pos.Y))) + 180f;
                            if (angle >= 45 & angle < 135)
                            {
                                dir = 1;
                            }
                            else if (angle >= 135 & angle < 225)
                            {
                                dir = 2;
                            }
                            else if (angle >= 225 & angle < 315)
                            {
                                dir = 3;
                            }

                            if (dir == 0)
                            {
                                Pos = PrevPos + new Vector2(0, -tmp.Sprite.Height);
                            }
                            else if (dir == 1)
                            {
                                Pos = PrevPos + new Vector2(-tmp.Sprite.Width, 0);
                            }
                            else if (dir == 2)
                            {
                                Pos = PrevPos + new Vector2(0, tmp.Sprite.Height);
                            }
                            else if (dir == 3)
                            {
                                Pos = PrevPos + new Vector2(tmp.Sprite.Width, 0);
                            }
                            PrevPos = Pos;
                        }
                    }




                    //--------------------------Left Click operations----------------------------
                    if (mouseState.LeftButton == ButtonState.Pressed & mouseState.X < graphics.GraphicsDevice.Viewport.Width - 192)
                    {
                        //Selection Mode
                        if (Mode == 0)
                        {
                            if (Selected != null && Selected.Type != 0)
                            {
                                LinkMode = true;
                                DotPositions = new List<Vector2>();
                                for (int i = 0; i < (int)Math.Sqrt(Math.Pow(Pos.X - Selected.Position.X, 2) + Math.Pow(Pos.Y - Selected.Position.Y, 2)); i++)
                                {
                                    double rot = Math.Atan2(Pos.Y - Selected.Position.Y, Pos.X - Selected.Position.X);
                                    Vector2 p = new Vector2(Selected.Position.X + (float)Math.Cos(rot) * i, Selected.Position.Y + (float)Math.Sin(rot) * i);
                                    DotPositions.Add(p);
                                }
                            }
                            if (!DropDownButtons.ElementAt<Button>(0).MouseOver & !DropDownButtons.ElementAt<Button>(1).MouseOver & !DropDownButtons.ElementAt<Button>(2).MouseOver & !DropDownButtons.ElementAt<Button>(3).MouseOver)
                            {
                                if (!LinkMode)
                                {
                                    Selected = temp;
                                }
                                DropMenu = false;
                            }
                            if (DropMenu & !LClicked)
                            {
                                if (DropDownButtons.ElementAt<Button>(0).Pressed)
                                {
                                    Selected.Static = !Selected.Static;
                                }
                                if (DropDownButtons.ElementAt<Button>(1).Pressed)
                                {
                                    Selected.Portable = !Selected.Portable;
                                }
                                if (DropDownButtons.ElementAt<Button>(2).Pressed)
                                {
                                    Selected.Switch();
                                }
                                if (DropDownButtons.ElementAt<Button>(3).Pressed)
                                {
                                    if (Selected.AnimSpeed < 2000)
                                    {
                                        Selected.AnimSpeed += 25;
                                    }
                                    else
                                    {
                                        Selected.AnimSpeed = 25;
                                    }
                                }

                                if (Selected.Static)
                                {
                                    DropDownButtons.ElementAt<Button>(0).Sprite = StaticButtonTrue;
                                }
                                else
                                {
                                    DropDownButtons.ElementAt<Button>(0).Sprite = StaticButtonFalse;
                                }
                                if (Selected.Portable)
                                {
                                    DropDownButtons.ElementAt<Button>(1).Sprite = PortableButtonTrue;
                                }
                                else
                                {
                                    DropDownButtons.ElementAt<Button>(1).Sprite = PortableButtonFalse;
                                }
                                if (Selected.Activated)
                                {
                                    DropDownButtons.ElementAt<Button>(2).Sprite = ActivatedButtonTrue;
                                }
                                else
                                {
                                    DropDownButtons.ElementAt<Button>(2).Sprite = ActivatedButtonFalse;
                                }
                            }

                            List<Link> links = CurrentLevel.PastLinks;
                            if (UITimeButtons[1].Pressed)
                            {
                                links = CurrentLevel.PresentLinks;
                            }
                            if (UITimeButtons[2].Pressed)
                            {
                                links = CurrentLevel.FutureLinks;
                            }
                            foreach (Link l in links)
                            {
                                if (l.MouseOver && !LClicked)
                                {
                                    LClicked = true;
                                    if (l.Direction == 0)
                                    {
                                        l.Direction = 1;
                                        GameObject tmp = l.FirstObject;
                                        l.FirstObject = l.SecondObject;
                                        l.SecondObject = tmp;
                                    }
                                    else if (l.Direction == 1)
                                    {
                                        l.Direction = 2;
                                        GameObject tmp = l.FirstObject;
                                        l.FirstObject = l.SecondObject;
                                        l.SecondObject = tmp;
                                    }
                                    else
                                    {
                                        l.Direction = 0;
                                    }
                                }
                            }
                        }

                        //Mass Move Mode (Deine Mudda)
                        if (Mode == 1)
                        {
                            if (!fblocked && !mblocked && !bblocked && !LClicked)
                            {
                                SelectStart = new Vector2(mouseState.X, mouseState.Y);
                                Selection = new List<GameObject>();
                                LClicked = true;
                            }
                            else if (!LClicked)
                            {
                                LastMousePos = new Vector2(mouseState.X, mouseState.Y);
                                LClicked = true;
                            }

                            if (Selection.Count > 0)
                            {
                                if (Selection.Contains(temp))
                                {
                                    int x = (int)mouseState.X;
                                    int y = (int)mouseState.Y;


                                    if (x % 16 != 0)
                                    {
                                        if (x % 16 <= 8)
                                        {
                                            while (x % 16 != 0)
                                            {
                                                x--;
                                            }
                                        }
                                        if (x % 16 > 8)
                                        {
                                            while (x % 16 != 0)
                                            {
                                                x++;
                                            }
                                        }
                                    }
                                    if (y % 16 != 0)
                                    {
                                        if (y % 16 <= 8)
                                        {
                                            while (y % 16 != 0)
                                            {
                                                y--;
                                            }
                                        }
                                        if (y % 16 > 8)
                                        {
                                            while (y % 16 != 0)
                                            {
                                                y++;
                                            }
                                        }
                                    }

                                    int ox = x - (int)LastMousePos.X;
                                    int oy = y - (int)LastMousePos.Y;

                                    if (ox % 16 != 0)
                                    {
                                        if (ox % 16 <= 8)
                                        {
                                            while (ox % 16 != 0)
                                            {
                                                ox--;
                                            }
                                        }
                                        if (ox % 16 > 8)
                                        {
                                            while (ox % 16 != 0)
                                            {
                                                ox++;
                                            }
                                        }
                                    }
                                    if (oy % 16 != 0)
                                    {
                                        if (oy % 16 <= 8)
                                        {
                                            while (oy % 16 != 0)
                                            {
                                                oy--;
                                            }
                                        }
                                        if (oy % 16 > 8)
                                        {
                                            while (oy % 16 != 0)
                                            {
                                                oy++;
                                            }
                                        }
                                    }


                                    foreach (GameObject o in Selection)
                                    {
                                        o.Position += new Vector2(ox, oy);
                                    }

                                    LastMousePos = new Vector2(x, y);
                                }
                            }
                            else
                            {
                                DotPositions = new List<Vector2>();

                                int multiplier = 1;
                                if (mouseState.X - SelectStart.X < 0)
                                {
                                    multiplier = -1;
                                }
                                for (int i = 0; i < Math.Abs(SelectStart.X - mouseState.X); i++)
                                {
                                    DotPositions.Add(new Vector2(SelectStart.X - Cam.Position.X + i * multiplier, SelectStart.Y - Cam.Position.Y));
                                    DotPositions.Add(new Vector2(SelectStart.X - Cam.Position.X + i * multiplier, mouseState.Y - Cam.Position.Y));
                                }
                                multiplier = 1;
                                if (mouseState.Y - SelectStart.Y < 0)
                                {
                                    multiplier = -1;
                                }
                                for (int i = 0; i < Math.Abs(SelectStart.Y - mouseState.Y); i++)
                                {
                                    DotPositions.Add(new Vector2(SelectStart.X - Cam.Position.X, SelectStart.Y - Cam.Position.Y + i * multiplier));
                                    DotPositions.Add(new Vector2(mouseState.X - Cam.Position.X, SelectStart.Y - Cam.Position.Y + i * multiplier));
                                }
                            }
                        }

                        //Creation Mode
                        if (Mode == 2)
                        {
                            if (mblocked && temp.Type != 0)
                            {
                                Held = temp;
                                if (UITimeButtons.ElementAt<Button>(0).Pressed)
                                {
                                    CurrentLevel.Past.Remove(Held);
                                }
                                if (UITimeButtons.ElementAt<Button>(1).Pressed)
                                {
                                    CurrentLevel.Present.Remove(Held);
                                }
                                if (UITimeButtons.ElementAt<Button>(2).Pressed)
                                {
                                    CurrentLevel.Future.Remove(Held);
                                }
                                Mode = 5;
                            }
                            else if (!LClicked)
                            {
                                if (UITimeButtons.ElementAt<Button>(0).Pressed)
                                {
                                    foreach (Button b in ObjectButtonsPast)
                                    {
                                        if (b.Pressed)
                                        {
                                            if (b.Sprites == null)
                                            {
                                                Held = new GameObject(new Vector2(Pos.X - b.Sprite.Width / 2, Pos.Y - b.Sprite.Height / 2), b.SpritePath, 1, b.Type, Content);
                                            }
                                            else
                                            {
                                                Held = new GameObject(new Vector2(Pos.X - b.Sprite.Width / 2, Pos.Y - b.Sprite.Height / 2), b.SpritePaths, b.AnimSpeed, b.Loop, 1, b.Type, Content);
                                            }
                                            Mode = 5;
                                        }
                                    }
                                }
                                if (UITimeButtons.ElementAt<Button>(1).Pressed)
                                {
                                    foreach (Button b in ObjectButtonsPresent)
                                    {
                                        if (b.Pressed)
                                        {
                                            if (b.Sprites == null)
                                            {
                                                Held = new GameObject(new Vector2(Pos.X - b.Sprite.Width / 2, Pos.Y - b.Sprite.Height / 2), b.SpritePath, 1, b.Type, Content);
                                            }
                                            else
                                            {
                                                Held = new GameObject(new Vector2(Pos.X - b.Sprite.Width / 2, Pos.Y - b.Sprite.Height / 2), b.SpritePaths, b.AnimSpeed, b.Loop, 1, b.Type, Content);
                                            }
                                            Mode = 5;
                                        }
                                    }
                                }
                                if (UITimeButtons.ElementAt<Button>(2).Pressed)
                                {
                                    foreach (Button b in ObjectButtonsFuture)
                                    {
                                        if (b.Pressed)
                                        {
                                            if (b.Sprites == null)
                                            {
                                                Held = new GameObject(new Vector2(Pos.X - b.Sprite.Width / 2, Pos.Y - b.Sprite.Height / 2), b.SpritePath, 1, b.Type, Content);
                                            }
                                            else
                                            {
                                                Held = new GameObject(new Vector2(Pos.X - b.Sprite.Width / 2, Pos.Y - b.Sprite.Height / 2), b.SpritePaths, b.AnimSpeed, b.Loop, 1, b.Type, Content);
                                            }
                                            Mode = 5;
                                        }
                                    }
                                }
                            }
                        }

                        //Draw Mode
                        if (Mode == 3)
                        {
                            if (!mblocked)
                            {
                                if (UITimeButtons.ElementAt<Button>(0).Pressed)
                                {
                                    Button button = ObjectButtonsPast.Find(b => b.Pressed);
                                    GameObject tmp = new GameObject(Pos, button.SpritePath, 1, button.Type, Content);
                                    foreach (GameObject o in CurrentLevel.Past)
                                    {
                                        if (o.Type == tmp.Type && MathHelper.Distance(o.Position.X, tmp.Position.X) < 1f && MathHelper.Distance(o.Position.Y, tmp.Position.Y) < 1f)
                                        {
                                            tmp = null;
                                            break;
                                        }
                                    }
                                    if (tmp != null)
                                    {
                                        CurrentLevel.Past.Add(tmp);
                                    }
                                }
                                if (UITimeButtons.ElementAt<Button>(1).Pressed)
                                {
                                    Button button = ObjectButtonsPresent.Find(b => b.Pressed);
                                    GameObject tmp = new GameObject(Pos, button.SpritePath, 1, button.Type, Content);
                                    foreach (GameObject o in CurrentLevel.Present)
                                    {
                                        if (o.Type == tmp.Type && MathHelper.Distance(o.Position.X, tmp.Position.X) < 1f && MathHelper.Distance(o.Position.Y, tmp.Position.Y) < 1f)
                                        {
                                            tmp = null;
                                            break;
                                        }
                                    }
                                    if (tmp != null)
                                    {
                                        CurrentLevel.Present.Add(tmp);
                                    }
                                }
                                if (UITimeButtons.ElementAt<Button>(2).Pressed)
                                {
                                    Button button = ObjectButtonsFuture.Find(b => b.Pressed);
                                    GameObject tmp = new GameObject(Pos, button.SpritePath, 1, button.Type, Content);
                                    foreach (GameObject o in CurrentLevel.Future)
                                    {
                                        if (o.Type == tmp.Type && MathHelper.Distance(o.Position.X, tmp.Position.X) < 1f && MathHelper.Distance(o.Position.Y, tmp.Position.Y) < 1f)
                                        {
                                            tmp = null;
                                            break;
                                        }
                                    }
                                    if (tmp != null)
                                    {
                                        CurrentLevel.Future.Add(tmp);
                                    }
                                }
                            }
                        }

                        //Removal Mode
                        if (Mode == 4)
                        {
                            List<GameObject> list = null;
                            if (UITimeButtons.ElementAt<Button>(0).Pressed)
                            {
                                list = CurrentLevel.Past;
                            }
                            if (UITimeButtons.ElementAt<Button>(1).Pressed)
                            {
                                list = CurrentLevel.Present;
                            }
                            if (UITimeButtons.ElementAt<Button>(2).Pressed)
                            {
                                list = CurrentLevel.Future;
                            }
                            if ((fblocked || mblocked || bblocked) && temp.Layer == 1)
                            {
                                list.Remove(temp);
                            }
                        }

                        //Drag Mode
                        if (Mode == 5)
                        {
                            RedGhost = false;
                            Held.Position = new Vector2(Pos.X + Cam.Position.X, Pos.Y + Cam.Position.Y);
                            List<GameObject> list = null;
                            if (UITimeButtons.ElementAt<Button>(0).Pressed)
                            {
                                list = CurrentLevel.Past;
                            }
                            if (UITimeButtons.ElementAt<Button>(1).Pressed)
                            {
                                list = CurrentLevel.Present;
                            }
                            if (UITimeButtons.ElementAt<Button>(2).Pressed)
                            {
                                list = CurrentLevel.Future;
                            }
                            foreach (GameObject o in list)
                            {
                                if (CollisionCheck(Held.Sprite, Held.Position, o.Sprite, o.Position + Cam.Position, 0) && o.Layer == 1)
                                {
                                    RedGhost = true;
                                }
                            }
                        }
                        LClicked = true;
                    }



                    //--------------------------Right Click operations----------------------------
                    else if (mouseState.RightButton == ButtonState.Pressed & mouseState.X < graphics.GraphicsDevice.Viewport.Width - 192)
                    {
                        //Selection Mode
                        if (Mode == 0)
                        {
                            Selected = temp;
                            if (Selected != null && Selected.Type != 0)
                            {
                                DropMenu = true;
                                if (Selected.Static)
                                {
                                    DropDownButtons.ElementAt<Button>(0).Sprite = StaticButtonTrue;
                                }
                                else
                                {
                                    DropDownButtons.ElementAt<Button>(0).Sprite = StaticButtonFalse;
                                }
                                if (Selected.Portable)
                                {
                                    DropDownButtons.ElementAt<Button>(1).Sprite = PortableButtonTrue;
                                }
                                else
                                {
                                    DropDownButtons.ElementAt<Button>(1).Sprite = PortableButtonFalse;
                                }
                                if (Selected.Activated)
                                {
                                    DropDownButtons.ElementAt<Button>(2).Sprite = ActivatedButtonTrue;
                                }
                                else
                                {
                                    DropDownButtons.ElementAt<Button>(2).Sprite = ActivatedButtonFalse;
                                }
                                DropDownButtons.ElementAt<Button>(0).Position = new Vector2(mouseState.X, mouseState.Y);
                                DropDownButtons.ElementAt<Button>(1).Position = new Vector2(mouseState.X, mouseState.Y + 32);
                                DropDownButtons.ElementAt<Button>(2).Position = new Vector2(mouseState.X, mouseState.Y + 64);
                                DropDownButtons.ElementAt<Button>(3).Position = new Vector2(mouseState.X, mouseState.Y + 96);
                            }
                            else
                            {
                                DropMenu = false;
                            }

                            if (UITimeButtons.ElementAt<Button>(0).Pressed)
                            {
                                foreach (Link l in CurrentLevel.PastLinks)
                                {
                                    if (l.MouseOver & !fblocked & !mblocked & !bblocked)
                                    {
                                        CurrentLevel.PastLinks.Remove(l);
                                        break;
                                    }
                                }
                            }
                            if (UITimeButtons.ElementAt<Button>(1).Pressed)
                            {
                                foreach (Link l in CurrentLevel.PresentLinks)
                                {
                                    if (l.MouseOver & !fblocked & !mblocked & !bblocked)
                                    {
                                        CurrentLevel.PresentLinks.Remove(l);
                                        break;
                                    }
                                }
                            }
                            if (UITimeButtons.ElementAt<Button>(2).Pressed)
                            {
                                foreach (Link l in CurrentLevel.FutureLinks)
                                {
                                    if (l.MouseOver & !fblocked & !mblocked & !bblocked)
                                    {
                                        CurrentLevel.FutureLinks.Remove(l);
                                        break;
                                    }
                                }
                            }
                        }

                        //Creation Mode
                        if (Mode == 2)
                        {
                            if (bblocked && temp.Type != 0)
                            {
                                Held = temp;
                                if (UITimeButtons.ElementAt<Button>(0).Pressed)
                                {
                                    CurrentLevel.Past.Remove(Held);
                                }
                                if (UITimeButtons.ElementAt<Button>(1).Pressed)
                                {
                                    CurrentLevel.Present.Remove(Held);
                                }
                                if (UITimeButtons.ElementAt<Button>(2).Pressed)
                                {
                                    CurrentLevel.Future.Remove(Held);
                                }
                                Mode = 5;
                            }
                            else if (!RClicked)
                            {
                                if (UITimeButtons.ElementAt<Button>(0).Pressed)
                                {
                                    foreach (Button b in ObjectButtonsPast)
                                    {
                                        if (b.Pressed)
                                        {
                                            if (b.Sprites == null)
                                            {
                                                Held = new GameObject(new Vector2(Pos.X - b.Sprite.Width / 2, Pos.Y - b.Sprite.Height / 2), b.SpritePath, 2, b.Type, Content);
                                            }
                                            else
                                            {
                                                Held = new GameObject(new Vector2(Pos.X - b.Sprite.Width / 2, Pos.Y - b.Sprite.Height / 2), b.SpritePaths, b.AnimSpeed, b.Loop, 2, b.Type, Content);
                                            }
                                            Mode = 5;
                                        }
                                    }
                                }
                                if (UITimeButtons.ElementAt<Button>(1).Pressed)
                                {
                                    foreach (Button b in ObjectButtonsPresent)
                                    {
                                        if (b.Pressed)
                                        {
                                            if (b.Sprites == null)
                                            {
                                                Held = new GameObject(new Vector2(Pos.X - b.Sprite.Width / 2, Pos.Y - b.Sprite.Height / 2), b.SpritePath, 2, b.Type, Content);
                                            }
                                            else
                                            {
                                                Held = new GameObject(new Vector2(Pos.X - b.Sprite.Width / 2, Pos.Y - b.Sprite.Height / 2), b.SpritePaths, b.AnimSpeed, b.Loop, 2, b.Type, Content);
                                            }
                                            Mode = 5;
                                        }
                                    }
                                }
                                if (UITimeButtons.ElementAt<Button>(2).Pressed)
                                {
                                    foreach (Button b in ObjectButtonsFuture)
                                    {
                                        if (b.Pressed)
                                        {
                                            if (b.Sprites == null)
                                            {
                                                Held = new GameObject(new Vector2(Pos.X - b.Sprite.Width / 2, Pos.Y - b.Sprite.Height / 2), b.SpritePath, 2, b.Type, Content);
                                            }
                                            else
                                            {
                                                Held = new GameObject(new Vector2(Pos.X - b.Sprite.Width / 2, Pos.Y - b.Sprite.Height / 2), b.SpritePaths, b.AnimSpeed, b.Loop, 2, b.Type, Content);
                                            }
                                            Mode = 5;
                                        }
                                    }
                                }
                            }
                        }

                        //Draw Mode
                        if (Mode == 3)
                        {
                            Button btmp = null;
                            if (UITimeButtons.FindIndex(b => b.Pressed) == 0)
                            {
                                btmp = ObjectButtonsPast.Find(b => b.Pressed);
                            }
                            if (UITimeButtons.FindIndex(b => b.Pressed) == 1)
                            {
                                btmp = ObjectButtonsPresent.Find(b => b.Pressed);
                            }
                            if (UITimeButtons.FindIndex(b => b.Pressed) == 2)
                            {
                                btmp = ObjectButtonsFuture.Find(b => b.Pressed);
                            }

                            if (!bblocked || (temp.Type == 0 && btmp.Type != 0))
                            {
                                if (UITimeButtons.ElementAt<Button>(0).Pressed)
                                {
                                    Button button = ObjectButtonsPast.Find(b => b.Pressed);
                                    GameObject tmp = new GameObject(Pos, button.SpritePath, 2, button.Type, Content);
                                    foreach (GameObject o in CurrentLevel.Past)
                                    {
                                        if (o.Type == tmp.Type && MathHelper.Distance(o.Position.X, tmp.Position.X) < 1f && MathHelper.Distance(o.Position.Y, tmp.Position.Y) < 1f)
                                        {
                                            tmp = null;
                                            break;
                                        }
                                    }
                                    if (tmp != null)
                                    {
                                        CurrentLevel.Past.Add(tmp);
                                    }
                                }
                                if (UITimeButtons.ElementAt<Button>(1).Pressed)
                                {
                                    Button button = ObjectButtonsPresent.Find(b => b.Pressed);
                                    GameObject tmp = new GameObject(Pos, button.SpritePath, 2, button.Type, Content);
                                    foreach (GameObject o in CurrentLevel.Present)
                                    {
                                        if (o.Type == tmp.Type && MathHelper.Distance(o.Position.X, tmp.Position.X) < 1f && MathHelper.Distance(o.Position.Y, tmp.Position.Y) < 1f)
                                        {
                                            tmp = null;
                                            break;
                                        }
                                    }
                                    if (tmp != null)
                                    {
                                        CurrentLevel.Present.Add(tmp);
                                    }
                                }
                                if (UITimeButtons.ElementAt<Button>(2).Pressed)
                                {
                                    Button button = ObjectButtonsFuture.Find(b => b.Pressed);
                                    GameObject tmp = new GameObject(Pos, button.SpritePath, 2, button.Type, Content);
                                    foreach (GameObject o in CurrentLevel.Future)
                                    {
                                        if (o.Type == tmp.Type && MathHelper.Distance(o.Position.X, tmp.Position.X) < 1f && MathHelper.Distance(o.Position.Y, tmp.Position.Y) < 1f)
                                        {
                                            tmp = null;
                                            break;
                                        }
                                    }
                                    if (tmp != null)
                                    {
                                        CurrentLevel.Future.Add(tmp);
                                    }
                                }
                            }
                        }

                        //Removal Mode
                        if (Mode == 4)
                        {
                            List<GameObject> list = null;
                            if (UITimeButtons.ElementAt<Button>(0).Pressed)
                            {
                                list = CurrentLevel.Past;
                            }
                            if (UITimeButtons.ElementAt<Button>(1).Pressed)
                            {
                                list = CurrentLevel.Present;
                            }
                            if (UITimeButtons.ElementAt<Button>(2).Pressed)
                            {
                                list = CurrentLevel.Future;
                            }
                            if ((fblocked || mblocked || bblocked) && temp.Layer == 2)
                            {
                                list.Remove(temp);
                            }
                        }

                        //Drag Mode
                        if (Mode == 5)
                        {
                            RedGhost = false;
                            Held.Position = new Vector2(Pos.X + Cam.Position.X, Pos.Y + Cam.Position.Y);
                            List<GameObject> list = null;
                            if (UITimeButtons.ElementAt<Button>(0).Pressed)
                            {
                                list = CurrentLevel.Past;
                            }
                            if (UITimeButtons.ElementAt<Button>(1).Pressed)
                            {
                                list = CurrentLevel.Present;
                            }
                            if (UITimeButtons.ElementAt<Button>(2).Pressed)
                            {
                                list = CurrentLevel.Future;
                            }
                            foreach (GameObject o in list)
                            {
                                if (CollisionCheck(Held.Sprite, Held.Position, o.Sprite, o.Position + Cam.Position, 0) && o.Layer == 2 && o.Type != 0)
                                {
                                    RedGhost = true;
                                }
                            }
                        }
                        RClicked = true;
                    }



                    //--------------------------Middle Click operations----------------------------
                    else if (mouseState.MiddleButton == ButtonState.Pressed & mouseState.X < graphics.GraphicsDevice.Viewport.Width - 192)
                    {
                        //Selection Mode
                        if (Mode == 0)
                        {
                            if (!DropDownButtons.ElementAt<Button>(0).MouseOver & !DropDownButtons.ElementAt<Button>(1).MouseOver & !DropDownButtons.ElementAt<Button>(2).MouseOver)
                            {
                                if (!LinkMode)
                                {
                                    Selected = temp;
                                }
                                DropMenu = false;
                            }
                            if (DropMenu & !MClicked)
                            {
                                if (DropDownButtons.ElementAt<Button>(0).Pressed)
                                {
                                    Selected.Static = !Selected.Static;
                                }
                                if (DropDownButtons.ElementAt<Button>(1).Pressed)
                                {
                                    Selected.Portable = !Selected.Portable;
                                }
                                if (DropDownButtons.ElementAt<Button>(2).Pressed)
                                {
                                    Selected.Activated = !Selected.Activated;
                                    Selected.AnimPhase = 1 * Convert.ToInt16(Selected.Activated);
                                }
                                if (Selected.Static)
                                {
                                    DropDownButtons.ElementAt<Button>(0).Sprite = StaticButtonTrue;
                                }
                                else
                                {
                                    DropDownButtons.ElementAt<Button>(0).Sprite = StaticButtonFalse;
                                }
                                if (Selected.Portable)
                                {
                                    DropDownButtons.ElementAt<Button>(1).Sprite = PortableButtonTrue;
                                }
                                else
                                {
                                    DropDownButtons.ElementAt<Button>(1).Sprite = PortableButtonFalse;
                                }
                                if (Selected.Activated)
                                {
                                    DropDownButtons.ElementAt<Button>(2).Sprite = ActivatedButtonTrue;
                                }
                                else
                                {
                                    DropDownButtons.ElementAt<Button>(2).Sprite = ActivatedButtonFalse;
                                }
                            }
                        }

                        //Creation Mode
                        if (Mode == 2)
                        {
                            if (fblocked && temp.Type != 0)
                            {
                                Held = temp;
                                if (UITimeButtons.ElementAt<Button>(0).Pressed)
                                {
                                    CurrentLevel.Past.Remove(Held);
                                }
                                if (UITimeButtons.ElementAt<Button>(1).Pressed)
                                {
                                    CurrentLevel.Present.Remove(Held);
                                }
                                if (UITimeButtons.ElementAt<Button>(2).Pressed)
                                {
                                    CurrentLevel.Future.Remove(Held);
                                }
                                Mode = 5;
                            }
                            else if (!MClicked)
                            {
                                if (UITimeButtons.ElementAt<Button>(0).Pressed)
                                {
                                    foreach (Button b in ObjectButtonsPast)
                                    {
                                        if (b.Pressed)
                                        {
                                            if (b.Sprites == null)
                                            {
                                                Held = new GameObject(new Vector2(Pos.X - b.Sprite.Width / 2, Pos.Y - b.Sprite.Height / 2), b.SpritePath, 0, b.Type, Content);
                                            }
                                            else
                                            {
                                                Held = new GameObject(new Vector2(Pos.X - b.Sprite.Width / 2, Pos.Y - b.Sprite.Height / 2), b.SpritePaths, b.AnimSpeed, b.Loop, 0, b.Type, Content);
                                            }
                                            Mode = 5;
                                        }
                                    }
                                }
                                if (UITimeButtons.ElementAt<Button>(1).Pressed)
                                {
                                    foreach (Button b in ObjectButtonsPresent)
                                    {
                                        if (b.Pressed)
                                        {
                                            if (b.Sprites == null)
                                            {
                                                Held = new GameObject(new Vector2(Pos.X - b.Sprite.Width / 2, Pos.Y - b.Sprite.Height / 2), b.SpritePath, 0, b.Type, Content);
                                            }
                                            else
                                            {
                                                Held = new GameObject(new Vector2(Pos.X - b.Sprite.Width / 2, Pos.Y - b.Sprite.Height / 2), b.SpritePaths, b.AnimSpeed, b.Loop, 0, b.Type, Content);
                                            }
                                            Mode = 5;
                                        }
                                    }
                                }
                                if (UITimeButtons.ElementAt<Button>(2).Pressed)
                                {
                                    foreach (Button b in ObjectButtonsFuture)
                                    {
                                        if (b.Pressed)
                                        {
                                            if (b.Sprites == null)
                                            {
                                                Held = new GameObject(new Vector2(Pos.X - b.Sprite.Width / 2, Pos.Y - b.Sprite.Height / 2), b.SpritePath, 0, b.Type, Content);
                                            }
                                            else
                                            {
                                                Held = new GameObject(new Vector2(Pos.X - b.Sprite.Width / 2, Pos.Y - b.Sprite.Height / 2), b.SpritePaths, b.AnimSpeed, b.Loop, 0, b.Type, Content);
                                            }
                                            Mode = 5;
                                        }
                                    }
                                }
                            }
                        }

                        //Draw Mode
                        if (Mode == 3)
                        {
                            if (!fblocked)
                            {
                                Button btmp = null;
                                if (UITimeButtons.FindIndex(b => b.Pressed) == 0)
                                {
                                    btmp = ObjectButtonsPast.Find(b => b.Pressed);
                                }
                                if (UITimeButtons.FindIndex(b => b.Pressed) == 1)
                                {
                                    btmp = ObjectButtonsPresent.Find(b => b.Pressed);
                                }
                                if (UITimeButtons.FindIndex(b => b.Pressed) == 2)
                                {
                                    btmp = ObjectButtonsFuture.Find(b => b.Pressed);
                                }

                                if (UITimeButtons.ElementAt<Button>(0).Pressed)
                                {
                                    Button button = ObjectButtonsPast.Find(b => b.Pressed);
                                    GameObject tmp = new GameObject(Pos, button.SpritePath, 0, button.Type, Content);
                                    foreach (GameObject o in CurrentLevel.Past)
                                    {
                                        if (o.Type == tmp.Type && MathHelper.Distance(o.Position.X, tmp.Position.X) < 1f && MathHelper.Distance(o.Position.Y, tmp.Position.Y) < 1f)
                                        {
                                            tmp = null;
                                            break;
                                        }
                                    }
                                    if (tmp != null)
                                    {
                                        CurrentLevel.Past.Add(tmp);
                                    }
                                }
                                if (UITimeButtons.ElementAt<Button>(1).Pressed)
                                {
                                    Button button = ObjectButtonsPresent.Find(b => b.Pressed);
                                    GameObject tmp = new GameObject(Pos, button.SpritePath, 0, button.Type, Content);
                                    foreach (GameObject o in CurrentLevel.Present)
                                    {
                                        if (o.Type == tmp.Type && MathHelper.Distance(o.Position.X, tmp.Position.X) < 1f && MathHelper.Distance(o.Position.Y, tmp.Position.Y) < 1f)
                                        {
                                            tmp = null;
                                            break;
                                        }
                                    }
                                    if (tmp != null)
                                    {
                                        CurrentLevel.Present.Add(tmp);
                                    }
                                }
                                if (UITimeButtons.ElementAt<Button>(2).Pressed)
                                {
                                    Button button = ObjectButtonsFuture.Find(b => b.Pressed);
                                    GameObject tmp = new GameObject(Pos, button.SpritePath, 0, button.Type, Content);
                                    foreach (GameObject o in CurrentLevel.Future)
                                    {
                                        if (o.Type == tmp.Type && MathHelper.Distance(o.Position.X, tmp.Position.X) < 1f && MathHelper.Distance(o.Position.Y, tmp.Position.Y) < 1f)
                                        {
                                            tmp = null;
                                            break;
                                        }
                                    }
                                    if (tmp != null)
                                    {
                                        CurrentLevel.Future.Add(tmp);
                                    }
                                }
                            }
                        }

                        //Removal Mode
                        if (Mode == 4)
                        {
                            List<GameObject> list = null;
                            if (UITimeButtons.ElementAt<Button>(0).Pressed)
                            {
                                list = CurrentLevel.Past;
                            }
                            if (UITimeButtons.ElementAt<Button>(1).Pressed)
                            {
                                list = CurrentLevel.Present;
                            }
                            if (UITimeButtons.ElementAt<Button>(2).Pressed)
                            {
                                list = CurrentLevel.Future;
                            }
                            if ((fblocked || mblocked || bblocked) && temp.Layer == 0)
                            {
                                list.Remove(temp);
                            }
                        }

                        //Drag Mode
                        if (Mode == 5)
                        {
                            RedGhost = false;
                            Held.Position = new Vector2(Pos.X + Cam.Position.X, Pos.Y + Cam.Position.Y);
                            List<GameObject> list = null;
                            if (UITimeButtons.ElementAt<Button>(0).Pressed)
                            {
                                list = CurrentLevel.Past;
                            }
                            if (UITimeButtons.ElementAt<Button>(1).Pressed)
                            {
                                list = CurrentLevel.Present;
                            }
                            if (UITimeButtons.ElementAt<Button>(2).Pressed)
                            {
                                list = CurrentLevel.Future;
                            }
                            foreach (GameObject o in list)
                            {
                                if (CollisionCheck(Held.Sprite, Held.Position, o.Sprite, o.Position + Cam.Position, 0) && o.Layer == 0)
                                {
                                    RedGhost = true;
                                }
                            }
                        }
                        MClicked = true;
                    }



                    //--------------------------Left Button Release operations----------------------------
                    if (mouseState.LeftButton == ButtonState.Released & LClicked)
                    {
                        PrevPos = Vector2.Zero;
                        if (SetPlayerStart & !ControlBarButtons.ElementAt<Button>(3).MouseOver)
                        {
                            SetPlayerStart = false;
                        }
                        if (LinkMode & !DropMenu)
                        {
                            if (fblocked | mblocked | bblocked)
                            {
                                if (Selected != temp)
                                {
                                    if (UITimeButtons.ElementAt<Button>(0).Pressed)
                                    {
                                        CurrentLevel.PastLinks.Add(new Link(Selected, temp, 1));
                                    }
                                    if (UITimeButtons.ElementAt<Button>(1).Pressed)
                                    {
                                        CurrentLevel.PresentLinks.Add(new Link(Selected, temp, 1));
                                    }
                                    if (UITimeButtons.ElementAt<Button>(2).Pressed)
                                    {
                                        CurrentLevel.FutureLinks.Add(new Link(Selected, temp, 1));
                                    }
                                }
                            }
                            LinkMode = false;
                            Selected = null;
                        }

                        if (Mode == 1)
                        {
                            List<GameObject> objects = CurrentLevel.Past;
                            if (UITimeButtons[1].Pressed)
                            {
                                objects = CurrentLevel.Present;
                            }
                            if (UITimeButtons[2].Pressed)
                            {
                                objects = CurrentLevel.Future;
                            }

                            int x = (int)SelectStart.X;
                            if (x > mouseState.X)
                            {
                                x = mouseState.X;
                            }

                            int y = (int)SelectStart.Y;
                            if (y > mouseState.Y)
                            {
                                y = mouseState.Y;
                            }

                            x -= (int)Cam.Position.X;
                            y -= (int)Cam.Position.Y;

                            foreach (GameObject o in objects)
                            {
                                if (CollisionCheck(new Rectangle(x, y, (int)MathHelper.Distance(SelectStart.X, mouseState.X), (int)MathHelper.Distance(SelectStart.Y, mouseState.Y)),
                                    new Rectangle((int)o.Position.X - (int)o.Sprite.Width / 2, (int)o.Position.Y - (int)o.Sprite.Height / 2, (int)o.Sprite.Width, (int)o.Sprite.Height)))
                                {
                                    Selection.Add(o);
                                }
                            }
                        }

                        if (Mode == 1 | Mode == 3 | Mode == 4)
                        {
                            List<GameObject> list = null;
                            List<Button> bList = null;
                            if (UITimeButtons.ElementAt<Button>(0).Pressed)
                            {
                                list = CurrentLevel.Past;
                                bList = ObjectButtonsPast;
                            }
                            if (UITimeButtons.ElementAt<Button>(1).Pressed)
                            {
                                list = CurrentLevel.Present;
                                bList = ObjectButtonsPresent;
                            }
                            if (UITimeButtons.ElementAt<Button>(2).Pressed)
                            {
                                list = CurrentLevel.Future;
                                bList = ObjectButtonsFuture;
                            }
                            foreach (GameObject o in list)
                            {
                                if (o.Type == 1 | (o.Type == 14 && UITimeButtons[2].Pressed))
                                {
                                    int dif = 0;
                                    if (bList.FindIndex(b => b.Sprite == o.Sprite) > 15)
                                    {
                                        dif = 16;
                                    }
                                    bool left = false;
                                    bool up = false;
                                    bool right = false;
                                    bool down = false;
                                    foreach (GameObject ob in list)
                                    {
                                        double dist = Math.Sqrt(Math.Pow(o.Position.X - ob.Position.X, 2) + Math.Pow(o.Position.Y - ob.Position.Y, 2));
                                        if (o != ob & (dist < 17 & (o.Type == 1 && ob.Type == 1)) | (dist < 65 & (o.Type == 14 && ob.Type == 14)))
                                        {
                                            if (o.Position.X > ob.Position.X & MathHelper.Distance(o.Position.Y, ob.Position.Y) < 1)
                                            {
                                                left = true;
                                            }
                                            if (o.Position.Y > ob.Position.Y & MathHelper.Distance(o.Position.X, ob.Position.X) < 1)
                                            {
                                                up = true;
                                            }
                                            if (o.Position.X < ob.Position.X & MathHelper.Distance(o.Position.Y, ob.Position.Y) < 1)
                                            {
                                                right = true;
                                            }
                                            if (o.Position.Y < ob.Position.Y & MathHelper.Distance(o.Position.X, ob.Position.X) < 1)
                                            {
                                                down = true;
                                            }
                                        }

                                    }
                                    o.SpritePath = bList.ElementAt<Button>(0 + dif).SpritePath;
                                    o.Sprite = bList.ElementAt<Button>(0 + dif).Sprite;
                                    if (right)
                                    {
                                        o.SpritePath = bList.ElementAt<Button>(1 + dif).SpritePath;
                                        o.Sprite = bList.ElementAt<Button>(1 + dif).Sprite;
                                    }
                                    if (down)
                                    {
                                        o.SpritePath = bList.ElementAt<Button>(2 + dif).SpritePath;
                                        o.Sprite = bList.ElementAt<Button>(2 + dif).Sprite;
                                    }
                                    if (left)
                                    {
                                        o.SpritePath = bList.ElementAt<Button>(3 + dif).SpritePath;
                                        o.Sprite = bList.ElementAt<Button>(3 + dif).Sprite;
                                    }
                                    if (up)
                                    {
                                        o.SpritePath = bList.ElementAt<Button>(4 + dif).SpritePath;
                                        o.Sprite = bList.ElementAt<Button>(4 + dif).Sprite;
                                    }
                                    if (left & right)
                                    {
                                        o.SpritePath = bList.ElementAt<Button>(5 + dif).SpritePath;
                                        o.Sprite = bList.ElementAt<Button>(5 + dif).Sprite;
                                    }
                                    if (up & down)
                                    {
                                        o.SpritePath = bList.ElementAt<Button>(6 + dif).SpritePath;
                                        o.Sprite = bList.ElementAt<Button>(6 + dif).Sprite;
                                    }
                                    if (up & right)
                                    {
                                        o.SpritePath = bList.ElementAt<Button>(7 + dif).SpritePath;
                                        o.Sprite = bList.ElementAt<Button>(7 + dif).Sprite;
                                    }
                                    if (up & left)
                                    {
                                        o.SpritePath = bList.ElementAt<Button>(8 + dif).SpritePath;
                                        o.Sprite = bList.ElementAt<Button>(8 + dif).Sprite;
                                    }
                                    if (down & right)
                                    {
                                        o.SpritePath = bList.ElementAt<Button>(9 + dif).SpritePath;
                                        o.Sprite = bList.ElementAt<Button>(9 + dif).Sprite;
                                    }
                                    if (down & left)
                                    {
                                        o.SpritePath = bList.ElementAt<Button>(10 + dif).SpritePath;
                                        o.Sprite = bList.ElementAt<Button>(10 + dif).Sprite;
                                    }
                                    if (up & down & right)
                                    {
                                        o.SpritePath = bList.ElementAt<Button>(11 + dif).SpritePath;
                                        o.Sprite = bList.ElementAt<Button>(11 + dif).Sprite;
                                    }
                                    if (left & right & down)
                                    {
                                        o.SpritePath = bList.ElementAt<Button>(12 + dif).SpritePath;
                                        o.Sprite = bList.ElementAt<Button>(12 + dif).Sprite;
                                    }
                                    if (up & down & left)
                                    {
                                        o.SpritePath = bList.ElementAt<Button>(13 + dif).SpritePath;
                                        o.Sprite = bList.ElementAt<Button>(13 + dif).Sprite;
                                    }
                                    if (left & right & up)
                                    {
                                        o.SpritePath = bList.ElementAt<Button>(14 + dif).SpritePath;
                                        o.Sprite = bList.ElementAt<Button>(14 + dif).Sprite;
                                    }
                                    if (left & up & right & down)
                                    {
                                        o.SpritePath = bList.ElementAt<Button>(15 + dif).SpritePath;
                                        o.Sprite = bList.ElementAt<Button>(15 + dif).Sprite;
                                    }

                                }
                            }
                        }

                        if (Mode == 5)
                        {
                            if (!RedGhost)
                            {
                                Held.Position = new Vector2(Held.Position.X - Cam.Position.X, Held.Position.Y - Cam.Position.Y);
                                if (UITimeButtons.ElementAt<Button>(0).Pressed)
                                {
                                    CurrentLevel.Past.Add(Held);
                                }
                                if (UITimeButtons.ElementAt<Button>(1).Pressed)
                                {
                                    CurrentLevel.Present.Add(Held);
                                }
                                if (UITimeButtons.ElementAt<Button>(2).Pressed)
                                {
                                    CurrentLevel.Future.Add(Held);
                                }
                                Mode = 2;
                            }
                        }

                        LClicked = false;

                        DotPositions = new List<Vector2>();
                    }


                    //--------------------------Right Button Release operations----------------------------
                    if (mouseState.RightButton == ButtonState.Released & RClicked)
                    {
                        PrevPos = Vector2.Zero;
                        if ((Mode == 3 | Mode == 4))
                        {
                            List<GameObject> list = null;
                            List<Button> bList = null;
                            if (UITimeButtons.ElementAt<Button>(0).Pressed)
                            {
                                list = CurrentLevel.Past;
                                bList = ObjectButtonsPast;
                            }
                            if (UITimeButtons.ElementAt<Button>(1).Pressed)
                            {
                                list = CurrentLevel.Present;
                                bList = ObjectButtonsPresent;
                            }
                            if (UITimeButtons.ElementAt<Button>(2).Pressed)
                            {
                                list = CurrentLevel.Future;
                                bList = ObjectButtonsFuture;
                            }
                            foreach (GameObject o in list)
                            {
                                if (o.Type == 1 | (o.Type == 14 && UITimeButtons[2].Pressed))
                                {
                                    int dif = 0;
                                    if (bList.FindIndex(b => b.Sprite == o.Sprite) > 15)
                                    {
                                        dif = 16;
                                    }
                                    bool left = false;
                                    bool up = false;
                                    bool right = false;
                                    bool down = false;
                                    foreach (GameObject ob in list)
                                    {
                                        double dist = Math.Sqrt(Math.Pow(o.Position.X - ob.Position.X, 2) + Math.Pow(o.Position.Y - ob.Position.Y, 2));
                                        if (o != ob & (dist < 17 & (o.Type == 1 && ob.Type == 1)) | (dist < 65 & (o.Type == 14 && ob.Type == 14)))
                                        {
                                            if (o.Position.X > ob.Position.X & MathHelper.Distance(o.Position.Y, ob.Position.Y) < 1)
                                            {
                                                left = true;
                                            }
                                            if (o.Position.Y > ob.Position.Y & MathHelper.Distance(o.Position.X, ob.Position.X) < 1)
                                            {
                                                up = true;
                                            }
                                            if (o.Position.X < ob.Position.X & MathHelper.Distance(o.Position.Y, ob.Position.Y) < 1)
                                            {
                                                right = true;
                                            }
                                            if (o.Position.Y < ob.Position.Y & MathHelper.Distance(o.Position.X, ob.Position.X) < 1)
                                            {
                                                down = true;
                                            }
                                        }

                                    }
                                    o.SpritePath = bList.ElementAt<Button>(0 + dif).SpritePath;
                                    o.Sprite = bList.ElementAt<Button>(0 + dif).Sprite;
                                    if (right)
                                    {
                                        o.SpritePath = bList.ElementAt<Button>(1 + dif).SpritePath;
                                        o.Sprite = bList.ElementAt<Button>(1 + dif).Sprite;
                                    }
                                    if (down)
                                    {
                                        o.SpritePath = bList.ElementAt<Button>(2 + dif).SpritePath;
                                        o.Sprite = bList.ElementAt<Button>(2 + dif).Sprite;
                                    }
                                    if (left)
                                    {
                                        o.SpritePath = bList.ElementAt<Button>(3 + dif).SpritePath;
                                        o.Sprite = bList.ElementAt<Button>(3 + dif).Sprite;
                                    }
                                    if (up)
                                    {
                                        o.SpritePath = bList.ElementAt<Button>(4 + dif).SpritePath;
                                        o.Sprite = bList.ElementAt<Button>(4 + dif).Sprite;
                                    }
                                    if (left & right)
                                    {
                                        o.SpritePath = bList.ElementAt<Button>(5 + dif).SpritePath;
                                        o.Sprite = bList.ElementAt<Button>(5 + dif).Sprite;
                                    }
                                    if (up & down)
                                    {
                                        o.SpritePath = bList.ElementAt<Button>(6 + dif).SpritePath;
                                        o.Sprite = bList.ElementAt<Button>(6 + dif).Sprite;
                                    }
                                    if (up & right)
                                    {
                                        o.SpritePath = bList.ElementAt<Button>(7 + dif).SpritePath;
                                        o.Sprite = bList.ElementAt<Button>(7 + dif).Sprite;
                                    }
                                    if (up & left)
                                    {
                                        o.SpritePath = bList.ElementAt<Button>(8 + dif).SpritePath;
                                        o.Sprite = bList.ElementAt<Button>(8 + dif).Sprite;
                                    }
                                    if (down & right)
                                    {
                                        o.SpritePath = bList.ElementAt<Button>(9 + dif).SpritePath;
                                        o.Sprite = bList.ElementAt<Button>(9 + dif).Sprite;
                                    }
                                    if (down & left)
                                    {
                                        o.SpritePath = bList.ElementAt<Button>(10 + dif).SpritePath;
                                        o.Sprite = bList.ElementAt<Button>(10 + dif).Sprite;
                                    }
                                    if (up & down & right)
                                    {
                                        o.SpritePath = bList.ElementAt<Button>(11 + dif).SpritePath;
                                        o.Sprite = bList.ElementAt<Button>(11 + dif).Sprite;
                                    }
                                    if (left & right & down)
                                    {
                                        o.SpritePath = bList.ElementAt<Button>(12 + dif).SpritePath;
                                        o.Sprite = bList.ElementAt<Button>(12 + dif).Sprite;
                                    }
                                    if (up & down & left)
                                    {
                                        o.SpritePath = bList.ElementAt<Button>(13 + dif).SpritePath;
                                        o.Sprite = bList.ElementAt<Button>(13 + dif).Sprite;
                                    }
                                    if (left & right & up)
                                    {
                                        o.SpritePath = bList.ElementAt<Button>(14 + dif).SpritePath;
                                        o.Sprite = bList.ElementAt<Button>(14 + dif).Sprite;
                                    }
                                    if (left & up & right & down)
                                    {
                                        o.SpritePath = bList.ElementAt<Button>(15 + dif).SpritePath;
                                        o.Sprite = bList.ElementAt<Button>(15 + dif).Sprite;
                                    }
                                }
                            }
                        }

                        if (Mode == 5)
                        {
                            if (!RedGhost)
                            {
                                Held.Position = new Vector2(Held.Position.X - Cam.Position.X, Held.Position.Y - Cam.Position.Y);
                                if (UITimeButtons.ElementAt<Button>(0).Pressed)
                                {
                                    CurrentLevel.Past.Add(Held);
                                }
                                if (UITimeButtons.ElementAt<Button>(1).Pressed)
                                {
                                    CurrentLevel.Present.Add(Held);
                                }
                                if (UITimeButtons.ElementAt<Button>(2).Pressed)
                                {
                                    CurrentLevel.Future.Add(Held);
                                }
                                Mode = 2;
                            }
                        }

                        RClicked = false;
                    }



                    //--------------------------Middle Button Release operations----------------------------
                    if (mouseState.MiddleButton == ButtonState.Released & MClicked)
                    {
                        PrevPos = Vector2.Zero;
                        if ((Mode == 3 | Mode == 4))
                        {
                            List<GameObject> list = null;
                            List<Button> bList = null;
                            if (UITimeButtons.ElementAt<Button>(0).Pressed)
                            {
                                list = CurrentLevel.Past;
                                bList = ObjectButtonsPast;
                            }
                            if (UITimeButtons.ElementAt<Button>(1).Pressed)
                            {
                                list = CurrentLevel.Present;
                                bList = ObjectButtonsPresent;
                            }
                            if (UITimeButtons.ElementAt<Button>(2).Pressed)
                            {
                                list = CurrentLevel.Future;
                                bList = ObjectButtonsFuture;
                            }
                            foreach (GameObject o in list)
                            {
                                if (o.Type == 1 | (o.Type == 14 && UITimeButtons[2].Pressed))
                                {
                                    int dif = 0;
                                    if (bList.FindIndex(b => b.Sprite == o.Sprite) > 15)
                                    {
                                        dif = 16;
                                    }
                                    bool left = false;
                                    bool up = false;
                                    bool right = false;
                                    bool down = false;
                                    foreach (GameObject ob in list)
                                    {
                                        double dist = Math.Sqrt(Math.Pow(o.Position.X - ob.Position.X, 2) + Math.Pow(o.Position.Y - ob.Position.Y, 2));
                                        if (o != ob & (dist < 17 & (o.Type == 1 && ob.Type == 1)) | (dist < 65 & (o.Type == 14 && ob.Type == 14)))
                                        {
                                            if (o.Position.X > ob.Position.X & MathHelper.Distance(o.Position.Y, ob.Position.Y) < 1)
                                            {
                                                left = true;
                                            }
                                            if (o.Position.Y > ob.Position.Y & MathHelper.Distance(o.Position.X, ob.Position.X) < 1)
                                            {
                                                up = true;
                                            }
                                            if (o.Position.X < ob.Position.X & MathHelper.Distance(o.Position.Y, ob.Position.Y) < 1)
                                            {
                                                right = true;
                                            }
                                            if (o.Position.Y < ob.Position.Y & MathHelper.Distance(o.Position.X, ob.Position.X) < 1)
                                            {
                                                down = true;
                                            }
                                        }

                                    }
                                    o.SpritePath = bList.ElementAt<Button>(0 + dif).SpritePath;
                                    o.Sprite = bList.ElementAt<Button>(0 + dif).Sprite;
                                    if (right)
                                    {
                                        o.SpritePath = bList.ElementAt<Button>(1 + dif).SpritePath;
                                        o.Sprite = bList.ElementAt<Button>(1 + dif).Sprite;
                                    }
                                    if (down)
                                    {
                                        o.SpritePath = bList.ElementAt<Button>(2 + dif).SpritePath;
                                        o.Sprite = bList.ElementAt<Button>(2 + dif).Sprite;
                                    }
                                    if (left)
                                    {
                                        o.SpritePath = bList.ElementAt<Button>(3 + dif).SpritePath;
                                        o.Sprite = bList.ElementAt<Button>(3 + dif).Sprite;
                                    }
                                    if (up)
                                    {
                                        o.SpritePath = bList.ElementAt<Button>(4 + dif).SpritePath;
                                        o.Sprite = bList.ElementAt<Button>(4 + dif).Sprite;
                                    }
                                    if (left & right)
                                    {
                                        o.SpritePath = bList.ElementAt<Button>(5 + dif).SpritePath;
                                        o.Sprite = bList.ElementAt<Button>(5 + dif).Sprite;
                                    }
                                    if (up & down)
                                    {
                                        o.SpritePath = bList.ElementAt<Button>(6 + dif).SpritePath;
                                        o.Sprite = bList.ElementAt<Button>(6 + dif).Sprite;
                                    }
                                    if (up & right)
                                    {
                                        o.SpritePath = bList.ElementAt<Button>(7 + dif).SpritePath;
                                        o.Sprite = bList.ElementAt<Button>(7 + dif).Sprite;
                                    }
                                    if (up & left)
                                    {
                                        o.SpritePath = bList.ElementAt<Button>(8 + dif).SpritePath;
                                        o.Sprite = bList.ElementAt<Button>(8 + dif).Sprite;
                                    }
                                    if (down & right)
                                    {
                                        o.SpritePath = bList.ElementAt<Button>(9 + dif).SpritePath;
                                        o.Sprite = bList.ElementAt<Button>(9 + dif).Sprite;
                                    }
                                    if (down & left)
                                    {
                                        o.SpritePath = bList.ElementAt<Button>(10 + dif).SpritePath;
                                        o.Sprite = bList.ElementAt<Button>(10 + dif).Sprite;
                                    }
                                    if (up & down & right)
                                    {
                                        o.SpritePath = bList.ElementAt<Button>(11 + dif).SpritePath;
                                        o.Sprite = bList.ElementAt<Button>(11 + dif).Sprite;
                                    }
                                    if (left & right & down)
                                    {
                                        o.SpritePath = bList.ElementAt<Button>(12 + dif).SpritePath;
                                        o.Sprite = bList.ElementAt<Button>(12 + dif).Sprite;
                                    }
                                    if (up & down & left)
                                    {
                                        o.SpritePath = bList.ElementAt<Button>(13 + dif).SpritePath;
                                        o.Sprite = bList.ElementAt<Button>(13 + dif).Sprite;
                                    }
                                    if (left & right & up)
                                    {
                                        o.SpritePath = bList.ElementAt<Button>(14 + dif).SpritePath;
                                        o.Sprite = bList.ElementAt<Button>(14 + dif).Sprite;
                                    }
                                    if (left & up & right & down)
                                    {
                                        o.SpritePath = bList.ElementAt<Button>(15 + dif).SpritePath;
                                        o.Sprite = bList.ElementAt<Button>(15 + dif).Sprite;
                                    }
                                }
                            }
                        }

                        if (Mode == 5)
                        {
                            if (!RedGhost)
                            {
                                Held.Position = new Vector2(Held.Position.X - Cam.Position.X, Held.Position.Y - Cam.Position.Y);
                                if (UITimeButtons.ElementAt<Button>(0).Pressed)
                                {
                                    CurrentLevel.Past.Add(Held);
                                }
                                if (UITimeButtons.ElementAt<Button>(1).Pressed)
                                {
                                    CurrentLevel.Present.Add(Held);
                                }
                                if (UITimeButtons.ElementAt<Button>(2).Pressed)
                                {
                                    CurrentLevel.Future.Add(Held);
                                }
                                Mode = 2;
                            }
                        }

                        MClicked = false;
                    }




                    //Mode Cycle
                    if (mouseState.X < graphics.GraphicsDevice.Viewport.Width - 160)
                    {
                        if (mouseState.ScrollWheelValue < lastScrollValue & Mode < 4)
                        {
                            Mode++;
                        }
                        if (mouseState.ScrollWheelValue > lastScrollValue & Mode > 0)
                        {
                            Mode--;
                        }
                    }
                    //Object Bar Scroll
                    else
                    {
                        List<Button> list = null;
                        if (UITimeButtons.ElementAt<Button>(0).Pressed)
                        {
                            list = ObjectButtonsPast;
                        }
                        if (UITimeButtons.ElementAt<Button>(1).Pressed)
                        {
                            list = ObjectButtonsPresent;
                        }
                        if (UITimeButtons.ElementAt<Button>(2).Pressed)
                        {
                            list = ObjectButtonsFuture;
                        }
                        if (mouseState.ScrollWheelValue > lastScrollValue & list.ElementAt<Button>(0).Position.Y < 142)
                        {
                            foreach (Button b in list)
                            {
                                b.Position = new Vector2(b.Position.X, b.Position.Y + 100);
                            }
                        }
                        if (mouseState.ScrollWheelValue < lastScrollValue & list.ElementAt<Button>(list.Count - 1).Position.Y > graphics.GraphicsDevice.Viewport.Height - list.ElementAt<Button>(list.Count - 1).Sprite.Height)
                        {
                            foreach (Button b in list)
                            {
                                b.Position = new Vector2(b.Position.X, b.Position.Y - 100);
                            }
                        }
                    }
                    lastScrollValue = mouseState.ScrollWheelValue;


                    //Updating Camera
                    if (UITimeButtons.ElementAt<Button>(0).Pressed)
                    {
                        Cam.Update(gameTime, graphics, CurrentLevel.PastBackground);
                    }
                    if (UITimeButtons.ElementAt<Button>(1).Pressed)
                    {
                        Cam.Update(gameTime, graphics, CurrentLevel.PresentBackground);
                    }
                    if (UITimeButtons.ElementAt<Button>(2).Pressed)
                    {
                        Cam.Update(gameTime, graphics, CurrentLevel.FutureBackground);
                    }


                    //Updating Links
                    if (UITimeButtons[0].Pressed)
                    {
                        if (UpdatingLink >= CurrentLevel.PastLinks.Count)
                        {
                            UpdatingLink = 0;
                        }
                        if (CurrentLevel.PastLinks.Count > 0)
                        {
                            Link l = CurrentLevel.PastLinks[UpdatingLink];
                            l.Update(Cam);
                            if (Mode == 4 & (!CurrentLevel.Past.Contains(l.FirstObject) | !CurrentLevel.Past.Contains(l.SecondObject)))
                            {
                                CurrentLevel.PastLinks.Remove(l);
                            }
                        }
                        UpdatingLink++;
                    }
                    if (UITimeButtons[1].Pressed)
                    {
                        if (UpdatingLink >= CurrentLevel.PresentLinks.Count)
                        {
                            UpdatingLink = 0;
                        }
                        if (CurrentLevel.PresentLinks.Count > 0)
                        {
                            Link l = CurrentLevel.PresentLinks[UpdatingLink];
                            l.Update(Cam);
                            if (Mode == 4 & (!CurrentLevel.Present.Contains(l.FirstObject) | !CurrentLevel.Present.Contains(l.SecondObject)))
                            {
                                CurrentLevel.PresentLinks.Remove(l);
                            }
                        }
                        UpdatingLink++;
                    }
                    if (UITimeButtons[2].Pressed)
                    {
                        if (UpdatingLink >= CurrentLevel.FutureLinks.Count)
                        {
                            UpdatingLink = 0;
                        }
                        if (CurrentLevel.FutureLinks.Count > 0)
                        {
                            Link l = CurrentLevel.FutureLinks[UpdatingLink];
                            l.Update(Cam);
                            if (Mode == 4 & (!CurrentLevel.Future.Contains(l.FirstObject) | !CurrentLevel.Future.Contains(l.SecondObject)))
                            {
                                CurrentLevel.FutureLinks.Remove(l);
                            }
                        }
                        UpdatingLink++;
                    }
                }


                //Test Mode
                else
                {
                    //Updating Objects
                    if (Player.Time == 0)
                    {
                        foreach (GameObject o in CurrentLevel.Past)
                        {
                            o.Update(Player, CurrentLevel.Past, CurrentLevel.PastLinks, CollisionCheck, gameTime, Cursor, CollisionCheck);
                        }
                        Cam.Update(Player, GraphicsDevice, CurrentLevel.PastBackground);
                    }
                    if (Player.Time == 1)
                    {
                        foreach (GameObject o in CurrentLevel.Present)
                        {
                            o.Update(Player, CurrentLevel.Present, CurrentLevel.PresentLinks, CollisionCheck, gameTime, Cursor, CollisionCheck);
                        }
                        Cam.Update(Player, GraphicsDevice, CurrentLevel.PresentBackground);
                    }
                    if (Player.Time == 2)
                    {
                        foreach (GameObject o in CurrentLevel.Future)
                        {
                            o.Update(Player, CurrentLevel.Future, CurrentLevel.FutureLinks, CollisionCheck, gameTime, Cursor, CollisionCheck);
                        }
                        Cam.Update(Player, GraphicsDevice, CurrentLevel.FutureBackground);
                    }

                    Player.Update(CurrentLevel, CollisionCheck, gameTime, Cursor, new Vector2(mouseState.X - Cam.Position.X, mouseState.Y - Cam.Position.Y));

                    if (keyState.IsKeyDown(Keys.Escape))
                    {
                        Player = null;
                        CurrentLevel = LoadLevel(CurrentLevel.Name + ".sav");
                    }
                }

                base.Update(gameTime);
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            MouseState mouseState = Mouse.GetState();
            KeyboardState keyState = Keyboard.GetState();

            if (StartMenu)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(StartWindow, new Vector2(graphics.GraphicsDevice.Viewport.Width / 2, graphics.GraphicsDevice.Viewport.Height / 2), null, Color.White, 0f, new Vector2(1024, 1024), 1f, SpriteEffects.None, 0f);
                StartMenuNewButton.Draw(spriteBatch);
                StartMenuLoadButton.Draw(spriteBatch);
                spriteBatch.Draw(CursorNormal, new Vector2(mouseState.X, mouseState.Y), Color.White);
                spriteBatch.End();
            }
            else
            {
                //Drawing Background
                spriteBatch.Begin(SpriteSortMode.BackToFront, null, null, null, null, null, Cam.GetMatrix());
                if (Player == null)
                {
                    if (UITimeButtons.ElementAt<Button>(0).Pressed)
                    {
                        spriteBatch.Draw(CurrentLevel.PastBackground, Vector2.Zero, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
                    }
                    if (UITimeButtons.ElementAt<Button>(1).Pressed)
                    {
                        spriteBatch.Draw(CurrentLevel.PresentBackground, Vector2.Zero, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
                    }
                    if (UITimeButtons.ElementAt<Button>(2).Pressed)
                    {
                        spriteBatch.Draw(CurrentLevel.FutureBackground, Vector2.Zero, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
                    }
                }
                else
                {
                    if (Player.Time == 0)
                    {
                        spriteBatch.Draw(CurrentLevel.PastBackground, Vector2.Zero, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
                    }
                    if (Player.Time == 1)
                    {
                        spriteBatch.Draw(CurrentLevel.PresentBackground, Vector2.Zero, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
                    }
                    if (Player.Time == 2)
                    {
                        spriteBatch.Draw(CurrentLevel.FutureBackground, Vector2.Zero, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
                    }
                }
                spriteBatch.End();

                //Drawing Objects
                spriteBatch.Begin(SpriteSortMode.BackToFront, null, null, null, null, null, Cam.GetMatrix());
                if (Player == null)
                {
                    if (UITimeButtons.ElementAt<Button>(0).Pressed)
                    {
                        foreach (GameObject o in CurrentLevel.Past)
                        {
                            o.Draw(false, spriteBatch);
                        }
                    }
                    if (UITimeButtons.ElementAt<Button>(1).Pressed)
                    {
                        foreach (GameObject o in CurrentLevel.Present)
                        {
                            o.Draw(false, spriteBatch);
                        }
                    }
                    if (UITimeButtons.ElementAt<Button>(2).Pressed)
                    {
                        foreach (GameObject o in CurrentLevel.Future)
                        {
                            o.Draw(false, spriteBatch);
                        }
                    }
                    foreach (GameObject o in Selection)
                    {
                        spriteBatch.Draw(o.Sprite, o.Position, null, Color.LightBlue, 0f, new Vector2(o.Sprite.Width / 2, o.Sprite.Height / 2), 1f, SpriteEffects.None, 0.3f * o.Layer);
                    }
                }
                else
                {
                    if (Player.Time == 0)
                    {
                        foreach (GameObject o in CurrentLevel.Past)
                        {
                            o.Draw(false, spriteBatch);
                        }
                    }
                    if (Player.Time == 1)
                    {
                        foreach (GameObject o in CurrentLevel.Present)
                        {
                            o.Draw(false, spriteBatch);
                        }
                    }

                    Player.Draw(spriteBatch);

                    if (Player.Time == 2)
                    {
                        foreach (GameObject o in CurrentLevel.Future)
                        {
                            o.Draw(false, spriteBatch);
                        }
                    }
                }
                spriteBatch.End();

                spriteBatch.Begin(SpriteSortMode.BackToFront, null, null, null, null, null, Cam.GetMatrix());
                if (PastRadioButton.Sprite == RadioButtonT)
                {
                    foreach (GameObject o in CurrentLevel.Past)
                    {
                        o.Draw(true, spriteBatch);
                    }
                }
                if (PresentRadioButton.Sprite == RadioButtonT)
                {
                    foreach (GameObject o in CurrentLevel.Present)
                    {
                        o.Draw(true, spriteBatch);
                    }
                }
                if (FutureRadioButton.Sprite == RadioButtonT)
                {
                    foreach (GameObject o in CurrentLevel.Future)
                    {
                        o.Draw(true, spriteBatch);
                    }
                }
                spriteBatch.End();


                //Drawing Links
                if (Player == null)
                {
                    spriteBatch.Begin(SpriteSortMode.BackToFront, null, null, null, null, null, Cam.GetMatrix());
                    if (Selected != null)
                    {
                        Texture2D drawSprite = Selected.Sprite;
                        if (Selected.Sprites != null)
                        {
                            drawSprite = Selected.Sprites.ElementAt<Texture2D>(1 * Convert.ToInt16(Selected.Activated));
                        }
                        spriteBatch.Draw(drawSprite, Selected.Position, null, Color.LightBlue, 0f, new Vector2(Selected.Sprite.Width / 2, Selected.Sprite.Height / 2), 1f, SpriteEffects.None, 0f);
                    }
                    if (UITimeButtons.ElementAt<Button>(0).Pressed)
                    {
                        foreach (Link l in CurrentLevel.PastLinks)
                        {
                            foreach (Vector2 v in l.DotPositions)
                            {
                                spriteBatch.Draw(LineDot, v, null, Color.Gray * 0.5f, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                            }
                        }
                    }
                    if (UITimeButtons.ElementAt<Button>(1).Pressed)
                    {
                        foreach (Link l in CurrentLevel.PresentLinks)
                        {
                            foreach (Vector2 v in l.DotPositions)
                            {
                                spriteBatch.Draw(LineDot, v, null, Color.Gray * 0.5f, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                            }
                        }
                    }
                    if (UITimeButtons.ElementAt<Button>(2).Pressed)
                    {
                        foreach (Link l in CurrentLevel.FutureLinks)
                        {
                            foreach (Vector2 v in l.DotPositions)
                            {
                                spriteBatch.Draw(LineDot, v, null, Color.Gray * 0.5f, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                            }
                        }
                    }
                    foreach (Vector2 v in DotPositions)
                    {
                        spriteBatch.Draw(LineDot, v, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                    }
                    spriteBatch.End();
                }


                //Drawing UI
                spriteBatch.Begin();
                spriteBatch.Draw(ControlBar, Vector2.Zero, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
                spriteBatch.Draw(UI, new Vector2(graphics.GraphicsDevice.Viewport.Width - 1000, 0), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
                spriteBatch.End();

                if (UITimeButtons.ElementAt<Button>(0).Pressed)
                {
                    foreach (Button b in ObjectButtonsPast)
                    {
                        spriteBatch.Begin();
                        b.Draw(spriteBatch);
                        spriteBatch.End();
                    }
                }
                if (UITimeButtons.ElementAt<Button>(1).Pressed)
                {
                    foreach (Button b in ObjectButtonsPresent)
                    {
                        spriteBatch.Begin();
                        b.Draw(spriteBatch);
                        spriteBatch.End();
                    }
                }
                if (UITimeButtons.ElementAt<Button>(2).Pressed)
                {
                    foreach (Button b in ObjectButtonsFuture)
                    {
                        spriteBatch.Begin();
                        b.Draw(spriteBatch);
                        spriteBatch.End();
                    }
                }

                foreach (Button b in ControlBarButtons)
                {
                    spriteBatch.Begin();
                    b.Draw(spriteBatch);
                    spriteBatch.End();
                }

                spriteBatch.Begin();
                spriteBatch.Draw(UIBox, new Vector2(graphics.GraphicsDevice.Viewport.Width - 150, -50), Color.White);
                PastRadioButton.Draw(spriteBatch);
                PresentRadioButton.Draw(spriteBatch);
                FutureRadioButton.Draw(spriteBatch);
                spriteBatch.End();

                foreach (Button b in UITimeButtons)
                {
                    spriteBatch.Begin();
                    b.Draw(spriteBatch);
                    spriteBatch.End();
                }

                spriteBatch.Begin(SpriteSortMode.BackToFront, null, null, null, null, null, Cam.GetMatrix());
                if (UITimeButtons.ElementAt<Button>((int)CurrentLevel.PlayerStart.Z).Pressed && Player == null)
                {
                    spriteBatch.Draw(PlayerStart, new Vector2(CurrentLevel.PlayerStart.X, CurrentLevel.PlayerStart.Y), null, Color.White, 0f, new Vector2(32, 32), 1f, SpriteEffects.None, 1f);
                }
                spriteBatch.End();

                spriteBatch.Begin();
                if (keyState.IsKeyDown(Keys.LeftAlt))
                {
                    spriteBatch.Draw(CursorHand, new Vector2(mouseState.X, mouseState.Y), Color.White);
                }
                else if (Player == null)
                {
                    if (Mode == 5)
                    {
                        if (Held.Layer == 0)
                        {
                            Color color = Color.White;
                            if (RedGhost)
                            {
                                color = Color.Red;
                            }
                            spriteBatch.Draw(Held.Sprite, Held.Position, null, color, 0f, new Vector2(Held.Sprite.Width / 2, Held.Sprite.Height / 2), 1f, SpriteEffects.None, 0.5f * Held.Layer);
                        }
                        else if (Held.Layer == 1)
                        {
                            Color color = Color.LightGray;
                            if (RedGhost)
                            {
                                color = Color.Red;
                            }
                            spriteBatch.Draw(Held.Sprite, Held.Position, null, color, 0f, new Vector2(Held.Sprite.Width / 2, Held.Sprite.Height / 2), 1f, SpriteEffects.None, 0.5f * Held.Layer);
                        }
                        else
                        {
                            Color color = Color.Gray;
                            if (RedGhost)
                            {
                                color = Color.Red;
                            }
                            spriteBatch.Draw(Held.Sprite, Held.Position, null, color, 0f, new Vector2(Held.Sprite.Width / 2, Held.Sprite.Height / 2), 1f, SpriteEffects.None, 0.5f * Held.Layer);
                        }
                        spriteBatch.Draw(CursorCreate, new Vector2(mouseState.X, mouseState.Y), Color.White);
                    }
                    if (DropMenu)
                    {
                        foreach (Button b in DropDownButtons)
                        {
                            if (b == DropDownButtons.ElementAt<Button>(3))
                            {
                                if (Selected.Sprites != null)
                                {
                                    b.Draw(spriteBatch);
                                    spriteBatch.DrawString(Font, Convert.ToString((Selected.Sprites.Count - 2 - Selected.Loop) * (double)Selected.AnimSpeed / 1000) + "s", new Vector2(b.Position.X + 50, b.Position.Y + 3), Color.Black);
                                }
                            }
                            else
                            {
                                b.Draw(spriteBatch);
                            }
                        }
                    }
                    if (Mode == 0)
                    {
                        spriteBatch.Draw(CursorNormal, new Vector2(mouseState.X, mouseState.Y), Color.White);
                    }
                    if (Mode == 1)
                    {
                        spriteBatch.Draw(CursorSelect, new Vector2(mouseState.X, mouseState.Y), Color.White);
                    }
                    if (Mode == 2)
                    {
                        if (mouseState.X < graphics.GraphicsDevice.Viewport.Width - 192 & mouseState.Y > 22)
                        {
                            List<Button> list = ObjectButtonsPast;
                            if (UITimeButtons.ElementAt<Button>(1).Pressed)
                            {
                                list = ObjectButtonsPresent;
                            }
                            if (UITimeButtons.ElementAt<Button>(2).Pressed)
                            {
                                list = ObjectButtonsFuture;
                            }
                            Texture2D sprite = list.Find(b => b.Pressed).Sprite;
                            Color color = Color.White;
                            if (RedGhost)
                            {
                                color = Color.Red;
                            }
                            spriteBatch.Draw(sprite, Pos + Cam.Position, null, color * 0.25f, 0f, new Vector2(sprite.Width / 2, sprite.Height / 2), 1f, SpriteEffects.None, 1f);
                        }
                        spriteBatch.Draw(CursorCreate, new Vector2(mouseState.X, mouseState.Y), Color.White);
                    }
                    if (Mode == 3)
                    {
                        if (mouseState.X < graphics.GraphicsDevice.Viewport.Width - 192 & mouseState.Y > 22)
                        {
                            List<Button> list = ObjectButtonsPast;
                            if (UITimeButtons.ElementAt<Button>(1).Pressed)
                            {
                                list = ObjectButtonsPresent;
                            }
                            if (UITimeButtons.ElementAt<Button>(2).Pressed)
                            {
                                list = ObjectButtonsFuture;
                            }
                            Texture2D sprite = list.Find(b => b.Pressed).Sprite;
                            spriteBatch.Draw(sprite, Pos + Cam.Position, null, Color.White * 0.25f, 0f, new Vector2(sprite.Width / 2, sprite.Height / 2), 1f, SpriteEffects.None, 1f);
                        }
                        spriteBatch.Draw(CursorPen, new Vector2(mouseState.X, mouseState.Y), Color.White);
                    }
                    if (Mode == 4)
                    {
                        spriteBatch.Draw(CursorRubber, new Vector2(mouseState.X, mouseState.Y), Color.White);
                    }
                }
                else
                {
                    bool onObject = false;
                    List<GameObject> list = CurrentLevel.Past;
                    if (Player.Time == 1)
                    {
                        list = CurrentLevel.Present;
                    }
                    else if (Player.Time == 2)
                    {
                        list = CurrentLevel.Future;
                    }
                    foreach (GameObject o in list)
                    {
                        if (o.Portable && CollisionCheck(Cursor, new Vector2(mouseState.X - Cam.Position.X, mouseState.Y - Cam.Position.Y), o.Sprite, o.Position, 0))
                        {
                            onObject = true;
                            break;
                        }
                    }
                    if (CollisionCheck(Cursor, new Vector2(mouseState.X - Cam.Position.X, mouseState.Y - Cam.Position.Y), Player.Sprite, Player.Position, 0))
                    {
                        onObject = true;
                    }

                    if (onObject)
                    {
                        spriteBatch.Draw(SwordPortable, new Vector2(mouseState.X, mouseState.Y), Color.White);
                    }
                    else
                    {
                        spriteBatch.Draw(SwordNormal, new Vector2(mouseState.X, mouseState.Y), Color.White);
                    }

                    spriteBatch.DrawString(Font, Convert.ToString(Player.Coins), new Vector2(50, 50), Color.Black);
                    spriteBatch.DrawString(Font, Convert.ToString(Player.Diamonds), new Vector2(50, 70), Color.Black);
                    if (Player.Keyparts == 3)
                    {
                        Random rand = new Random();
                        spriteBatch.DrawString(Font, "YOU WON", new Vector2(rand.Next(50, graphics.GraphicsDevice.Viewport.Width - 150), rand.Next(70, graphics.GraphicsDevice.Viewport.Height - 50)), new Color(rand.Next(0, 255), rand.Next(0, 255), rand.Next(0, 255)), 0f, Vector2.Zero, (float)rand.Next(5, 15), SpriteEffects.None, 0f);
                    }
                }
                spriteBatch.End();
            }

            if (NewMenu)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(NewWindow, new Vector2(graphics.GraphicsDevice.Viewport.Width / 2 - NewWindow.Width / 2, graphics.GraphicsDevice.Viewport.Height / 2 - NewWindow.Height / 2), Color.White);
                NewMenuCreateButton.Draw(spriteBatch);
                CloseButton.Draw(spriteBatch);
                spriteBatch.DrawString(Font, NameString, new Vector2(graphics.GraphicsDevice.Viewport.Width / 2 - 110, graphics.GraphicsDevice.Viewport.Height / 2 - 220), Color.Black);

                string[] paths = Directory.GetFiles(ContentDirectory + "/Sprites/Backgrounds/Past");
                for (int i = 0; i < paths.Length; i++)
                {
                    Color drawColor = Color.Black;
                    if (PaClickPos.Y > graphics.GraphicsDevice.Viewport.Height / 2 - 168 + i * 30 & PaClickPos.Y < graphics.GraphicsDevice.Viewport.Height / 2 - 138 + i * 30)
                    {
                        drawColor = Color.White;
                    }
                    paths[i] = Path.GetFileName(paths[i]);
                    spriteBatch.DrawString(Font, paths[i], new Vector2(graphics.GraphicsDevice.Viewport.Width / 2 - 114, graphics.GraphicsDevice.Viewport.Height / 2 - 168 + i * 30), drawColor);
                }

                paths = Directory.GetFiles(ContentDirectory + "/Sprites/Backgrounds/Present");
                for (int i = 0; i < paths.Length; i++)
                {
                    Color drawColor = Color.Black;
                    if (PrClickPos.Y > graphics.GraphicsDevice.Viewport.Height / 2 - 34 + i * 30 & PrClickPos.Y < graphics.GraphicsDevice.Viewport.Height / 2 - 4 + i * 30)
                    {
                        drawColor = Color.White;
                    }
                    paths[i] = Path.GetFileName(paths[i]);
                    spriteBatch.DrawString(Font, paths[i], new Vector2(graphics.GraphicsDevice.Viewport.Width / 2 - 114, graphics.GraphicsDevice.Viewport.Height / 2 - 34 + i * 30), drawColor);
                }

                paths = Directory.GetFiles(ContentDirectory + "/Sprites/Backgrounds/Future");
                for (int i = 0; i < paths.Length; i++)
                {
                    Color drawColor = Color.Black;
                    if (FuClickPos.Y > graphics.GraphicsDevice.Viewport.Height / 2 + 98 + i * 30 & FuClickPos.Y < graphics.GraphicsDevice.Viewport.Height / 2 + 128 + i * 30)
                    {
                        drawColor = Color.White;
                    }
                    paths[i] = Path.GetFileName(paths[i]);
                    spriteBatch.DrawString(Font, paths[i], new Vector2(graphics.GraphicsDevice.Viewport.Width / 2 - 114, graphics.GraphicsDevice.Viewport.Height / 2 + 98 + i * 30), drawColor);
                }

                spriteBatch.Draw(CursorNormal, new Vector2(mouseState.X, mouseState.Y), Color.White);
                spriteBatch.End();
            }
            if (LoadMenu)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(LoadWindow, new Vector2(graphics.GraphicsDevice.Viewport.Width / 2 - LoadWindow.Width / 2, graphics.GraphicsDevice.Viewport.Height / 2 - LoadWindow.Height / 2), Color.White);
                LoadMenuLoadButton.Draw(spriteBatch);
                CloseButton.Draw(spriteBatch);

                string[] paths = Directory.GetFiles(Environment.CurrentDirectory + "/Levels/AllPlayers");
                for (int i = 0; i < paths.Length; i++)
                {
                    Color drawColor = Color.Black;
                    if (ClickPos.Y > graphics.GraphicsDevice.Viewport.Height / 2 - 223 + i * 30 & ClickPos.Y < graphics.GraphicsDevice.Viewport.Height / 2 - 193 + i * 30)
                    {
                        drawColor = Color.White;
                    }
                    paths[i] = Path.GetFileName(paths[i]);
                    spriteBatch.DrawString(Font, paths[i], new Vector2(graphics.GraphicsDevice.Viewport.Width / 2 - 228, graphics.GraphicsDevice.Viewport.Height / 2 - 223 + i * 30), drawColor);
                }
                spriteBatch.Draw(CursorNormal, new Vector2(mouseState.X, mouseState.Y), Color.White);
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





        public void SaveLevel()
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

            string filename = CurrentLevel.Name + ".sav";

            // Check to see whether the save exists.
            if (container.FileExists(filename))
                // Delete it so that we can create one fresh.
                container.DeleteFile(filename);

            // Create the file.
            Stream stream = container.CreateFile(filename);

            // Convert the object to XML data and put it in the stream.
            XmlSerializer serializer = new XmlSerializer(typeof(Level));

            serializer.Serialize(stream, CurrentLevel);

            // Close the file.
            stream.Close();

            // Dispose the container, to commit changes.
            container.Dispose();
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
