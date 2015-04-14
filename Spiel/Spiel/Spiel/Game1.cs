using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Storage;
using Classes;

namespace Game
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        StorageDevice device;
        SpriteBatch spriteBatch;
        Random rand;
        bool InvOpen;
        bool Paused;
        bool Menu;
        bool StartScreen;
        bool HelpScreen;
        bool GameOver;
        bool Credits;
        int FadeCounter;
        bool TabPressed;
        bool EscPressed;
        MouseState lastMouseState;
        Item GrabbedItem;
        WeaponPickup EquippedLeft;
        WeaponPickup EquippedRight;
        WeaponPickup ItemOnHold;
        int HoldOnCounter;
        bool QPressed;
        bool WPressed;
        bool TestPhase;
        bool NameChanging;
        bool KeyPressed;
        bool SoundsOn;
        bool LowParticles;
        bool DamageNumbers;
        bool Clicked;

        Player Player;
        float multiplier;
        int BeastModeTime;
        int Score;
        int LevelUnlocked;
        int Level;
        string Name;
        List<HighScore> HighScoresL1;
        List<HighScore> HighScoresL2;
        List<HighScore> HighScoresL3;
        List<HighScore> HighScoresL4;
        List<HighScore> HighScoresL5;
        List<ProgressFile> ProgressList;

        Texture2D StartScreenPic;
        Texture2D MenuPic;
        Texture2D HelpScreenPic;

        Texture2D Ship_1;
        Texture2D Ship_2;
        Texture2D Ship_3;
        Texture2D Ship_BW_1;
        Texture2D Ship_BW_2;
        Texture2D Ship_BW_3;

        Texture2D LaserProjectile;
        Texture2D LaserProjectile_BW;
        Texture2D Rocket;
        Texture2D Rocket_BW;
        Texture2D Laser;
        Texture2D Laser_BW;
        Texture2D HomingProjectile;
        Texture2D HomingProjectile_BW;

        Texture2D Enemy_Small_1;
        Texture2D Enemy_Small_2;
        Texture2D Enemy_Small_3;
        Texture2D Enemy_Medium_1;
        Texture2D Enemy_Medium_2;
        Texture2D Enemy_Medium_3;
        Texture2D Enemy_Large_1;
        Texture2D Enemy_Large_2;
        Texture2D Enemy_Large_3;
        Texture2D Enemy_Small2_1;
        Texture2D Enemy_Small2_2;
        Texture2D Enemy_Small2_3;
        Texture2D Enemy_Medium2_1;
        Texture2D Enemy_Medium2_2;
        Texture2D Enemy_Medium2_3;
        Texture2D Enemy_Large2_1;
        Texture2D Enemy_Large2_2;
        Texture2D Enemy_Large2_3;
        Texture2D Enemy_Small3_1;
        Texture2D Enemy_Small3_2;
        Texture2D Enemy_Small3_3;
        Texture2D Enemy_Medium3_1;
        Texture2D Enemy_Medium3_2;
        Texture2D Enemy_Medium3_3;
        Texture2D Enemy_Large3_1;
        Texture2D Enemy_Large3_2;
        Texture2D Enemy_Large3_3;
        Texture2D Enemy_Small4_1;
        Texture2D Enemy_Small4_2;
        Texture2D Enemy_Small4_3;
        Texture2D Enemy_Medium4_1;
        Texture2D Enemy_Medium4_2;
        Texture2D Enemy_Medium4_3;
        Texture2D Enemy_Large4_1;
        Texture2D Enemy_Large4_2;
        Texture2D Enemy_Large4_3;
        Texture2D Enemy_Small5_1;
        Texture2D Enemy_Small5_2;
        Texture2D Enemy_Small5_3;
        Texture2D Enemy_Medium5_1;
        Texture2D Enemy_Medium5_2;
        Texture2D Enemy_Medium5_3;
        Texture2D Enemy_Large5_1;
        Texture2D Enemy_Large5_2;
        Texture2D Enemy_Large5_3;

        Texture2D Enemy_Small_BW_1;
        Texture2D Enemy_Small_BW_2;
        Texture2D Enemy_Small_BW_3;
        Texture2D Enemy_Medium_BW_1;
        Texture2D Enemy_Medium_BW_2;
        Texture2D Enemy_Medium_BW_3;
        Texture2D Enemy_Large_BW_1;
        Texture2D Enemy_Large_BW_2;
        Texture2D Enemy_Large_BW_3;
        Texture2D Enemy_Small2_BW_1;
        Texture2D Enemy_Small2_BW_2;
        Texture2D Enemy_Small2_BW_3;
        Texture2D Enemy_Medium2_BW_1;
        Texture2D Enemy_Medium2_BW_2;
        Texture2D Enemy_Medium2_BW_3;
        Texture2D Enemy_Large2_BW_1;
        Texture2D Enemy_Large2_BW_2;
        Texture2D Enemy_Large2_BW_3;
        Texture2D Enemy_Small3_BW_1;
        Texture2D Enemy_Small3_BW_2;
        Texture2D Enemy_Small3_BW_3;
        Texture2D Enemy_Medium3_BW_1;
        Texture2D Enemy_Medium3_BW_2;
        Texture2D Enemy_Medium3_BW_3;
        Texture2D Enemy_Large3_BW_1;
        Texture2D Enemy_Large3_BW_2;
        Texture2D Enemy_Large3_BW_3;
        Texture2D Enemy_Small4_BW_1;
        Texture2D Enemy_Small4_BW_2;
        Texture2D Enemy_Small4_BW_3;
        Texture2D Enemy_Medium4_BW_1;
        Texture2D Enemy_Medium4_BW_2;
        Texture2D Enemy_Medium4_BW_3;
        Texture2D Enemy_Large4_BW_1;
        Texture2D Enemy_Large4_BW_2;
        Texture2D Enemy_Large4_BW_3;
        Texture2D Enemy_Small5_BW_1;
        Texture2D Enemy_Small5_BW_2;
        Texture2D Enemy_Small5_BW_3;
        Texture2D Enemy_Medium5_BW_1;
        Texture2D Enemy_Medium5_BW_2;
        Texture2D Enemy_Medium5_BW_3;
        Texture2D Enemy_Large5_BW_1;
        Texture2D Enemy_Large5_BW_2;
        Texture2D Enemy_Large5_BW_3;

        Texture2D EnemyLaserProjectile_Medium;
        Texture2D EnemyLaserProjectile_Large;
        Texture2D EnemyLaserProjectile_Medium_BW;
        Texture2D EnemyLaserProjectile_Large_BW;

        Texture2D Background1;
        Texture2D Background2;
        Texture2D CreditsPic;

        Texture2D Effect_1;
        Texture2D Effect_2;
        Texture2D Effect_3;
        Texture2D Effect_BW_1;
        Texture2D Effect_BW_2;
        Texture2D Effect_BW_3;
        Texture2D Effect_B_1;
        Texture2D Effect_B_2;
        Texture2D Effect_B_3;
        Texture2D Effect_LB_1;
        Texture2D Effect_LB_2;
        Texture2D Effect_LB_3;
        Texture2D Effect_White_1;
        Texture2D Effect_White_2;
        Texture2D Effect_White_3;
        Texture2D ProjectileEffect_1;
        Texture2D ProjectileEffect_2;
        Texture2D ProjectileEffect_3;

        Texture2D null_1;
        Texture2D null_2;
        Texture2D null_3;
        Texture2D eins_1;
        Texture2D eins_2;
        Texture2D eins_3;
        Texture2D zwei_1;
        Texture2D zwei_2;
        Texture2D zwei_3;
        Texture2D drei_1;
        Texture2D drei_2;
        Texture2D drei_3;
        Texture2D vier_1;
        Texture2D vier_2;
        Texture2D vier_3;
        Texture2D fünf_1;
        Texture2D fünf_2;
        Texture2D fünf_3;
        Texture2D sechs_1;
        Texture2D sechs_2;
        Texture2D sechs_3;
        Texture2D sieben_1;
        Texture2D sieben_2;
        Texture2D sieben_3;
        Texture2D acht_1;
        Texture2D acht_2;
        Texture2D acht_3;
        Texture2D neun_1;
        Texture2D neun_2;
        Texture2D neun_3;
        Texture2D null_BW_1;
        Texture2D null_BW_2;
        Texture2D null_BW_3;
        Texture2D eins_BW_1;
        Texture2D eins_BW_2;
        Texture2D eins_BW_3;
        Texture2D zwei_BW_1;
        Texture2D zwei_BW_2;
        Texture2D zwei_BW_3;
        Texture2D drei_BW_1;
        Texture2D drei_BW_2;
        Texture2D drei_BW_3;
        Texture2D vier_BW_1;
        Texture2D vier_BW_2;
        Texture2D vier_BW_3;
        Texture2D fünf_BW_1;
        Texture2D fünf_BW_2;
        Texture2D fünf_BW_3;
        Texture2D sechs_BW_1;
        Texture2D sechs_BW_2;
        Texture2D sechs_BW_3;
        Texture2D sieben_BW_1;
        Texture2D sieben_BW_2;
        Texture2D sieben_BW_3;
        Texture2D acht_BW_1;
        Texture2D acht_BW_2;
        Texture2D acht_BW_3;
        Texture2D neun_BW_1;
        Texture2D neun_BW_2;
        Texture2D neun_BW_3;

        Texture2D Pickup_LP;
        Texture2D Pickup_R;
        Texture2D Pickup_L;
        Texture2D Pickup_HV;
        Texture2D Pickup_Li;
        Texture2D Pickup_S;
        Texture2D Pickup_LP_BW;
        Texture2D Pickup_R_BW;
        Texture2D Pickup_L_BW;
        Texture2D Pickup_HV_BW;
        Texture2D Pickup_Li_BW;
        Texture2D Pickup_S_BW;

        Texture2D UPickup_LP;
        Texture2D UPickup_R;
        Texture2D UPickup_L;
        Texture2D UPickup_HV;
        Texture2D UPickup_Li;
        Texture2D UPickup_S;
        Texture2D UPickup_LP_BW;
        Texture2D UPickup_R_BW;
        Texture2D UPickup_L_BW;
        Texture2D UPickup_HV_BW;
        Texture2D UPickup_Li_BW;
        Texture2D UPickup_S_BW;

        Texture2D Boss1_1;
        Texture2D Boss1_2;
        Texture2D Boss1_3;
        Texture2D Boss2_1;
        Texture2D Boss2_2;
        Texture2D Boss2_3;
        Texture2D Boss3_1;
        Texture2D Boss3_2;
        Texture2D Boss3_3;
        Texture2D Boss4_1;
        Texture2D Boss4_2;
        Texture2D Boss4_3;
        Texture2D Boss5_1;
        Texture2D Boss5_2;
        Texture2D Boss5_3;

        Texture2D Boss1_BW_1;
        Texture2D Boss1_BW_2;
        Texture2D Boss1_BW_3;
        Texture2D Boss2_BW_1;
        Texture2D Boss2_BW_2;
        Texture2D Boss2_BW_3;
        Texture2D Boss3_BW_1;
        Texture2D Boss3_BW_2;
        Texture2D Boss3_BW_3;
        Texture2D Boss4_BW_1;
        Texture2D Boss4_BW_2;
        Texture2D Boss4_BW_3;
        Texture2D Boss5_BW_1;
        Texture2D Boss5_BW_2;
        Texture2D Boss5_BW_3;

        Texture2D Inv;
        Texture2D Cursor;
        Texture2D Cursor_White;
        Texture2D Box;
        Texture2D BlackScreen;

        SoundEffect Hit;
        SoundEffect Click;
        SoundEffect SmallExp_1;
        SoundEffect SmallExp_2;
        SoundEffect SmallExp_3;
        SoundEffect MediumExp_1;
        SoundEffect MediumExp_2;
        SoundEffect MediumExp_3;
        SoundEffect BigExp_1;
        SoundEffect BigExp_2;
        SoundEffect BigExp_3;
        SoundEffect LPSound_1;
        SoundEffect LPSound_2;
        SoundEffect LPSound_3;
        SoundEffect RocketSound_1;
        SoundEffect RocketSound_2;
        SoundEffect RocketSound_3;
        SoundEffect HVSound_1;
        SoundEffect HVSound_2;
        SoundEffect HVSound_3;
        SoundEffect LightningSound_1;
        SoundEffect LightningSound_2;
        SoundEffect LightningSound_3;
        SoundEffect SWSound_1;
        SoundEffect SWSound_2;
        SoundEffect SWSound_3;
        SoundEffectInstance LaserSound_1;
        SoundEffectInstance LaserSound_2;
        SoundEffectInstance LaserSound_3;

        SpriteFont Font;

        int BG1Position;
        int BG2Position;
        int CreditsPosition;

        List<Attack> Attacks;
        List<Enemy> Enemies;
        List<Attack> EnemyAttacks;
        List<Classes.Effect> Effects;
        List<Item> Items;
        List<Item> Inventory;
        List<Button> ButtonList;
        Boss Boss;

        EnemySpawner enemySpawner;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = 900;
            graphics.PreferredBackBufferWidth = 750;
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {

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
            rand = new Random();
            InvOpen = false;
            Paused = false;
            Menu = false;
            StartScreen = true;
            HelpScreen = false;
            GameOver = false;
            Credits = false;
            FadeCounter = 2000;
            TabPressed = false;
            EscPressed = false;
            lastMouseState = Mouse.GetState();
            GrabbedItem = null;
            EquippedLeft = null;
            EquippedRight = null;
            ItemOnHold = null;
            HoldOnCounter = 0;
            QPressed = false;
            WPressed = false;
            TestPhase = false;
            NameChanging = false;
            KeyPressed = false;
            SoundsOn = true;
            LowParticles = false;
            DamageNumbers = true;
            Clicked = false;

            StartScreenPic = Content.Load<Texture2D>("Sprites/Misc/StartScreen");
            MenuPic = Content.Load<Texture2D>("Sprites/Misc/Menu");
            HelpScreenPic = Content.Load<Texture2D>("Sprites/Misc/HelpScreen");

            Ship_1 = Content.Load<Texture2D>("Sprites/Ships/Ship_1");
            Ship_2 = Content.Load<Texture2D>("Sprites/Ships/Ship_2");
            Ship_3 = Content.Load<Texture2D>("Sprites/Ships/Ship_3");
            Ship_BW_1 = Content.Load<Texture2D>("Sprites/Ships/Ship_BW_1");
            Ship_BW_2 = Content.Load<Texture2D>("Sprites/Ships/Ship_BW_2");
            Ship_BW_3 = Content.Load<Texture2D>("Sprites/Ships/Ship_BW_3");

            LaserProjectile = Content.Load<Texture2D>("Sprites/Projectiles and Effects/LaserProjectile");
            LaserProjectile_BW = Content.Load<Texture2D>("Sprites/Projectiles and Effects/LaserProjectile_BW");
            Rocket = Content.Load<Texture2D>("Sprites/Projectiles and Effects/Rocket");
            Rocket_BW = Content.Load<Texture2D>("Sprites/Projectiles and Effects/Rocket_BW");
            Laser = Content.Load<Texture2D>("Sprites/Projectiles and Effects/Laser");
            Laser_BW = Content.Load<Texture2D>("Sprites/Projectiles and Effects/Laser_BW");
            HomingProjectile = Content.Load<Texture2D>("Sprites/Projectiles and Effects/HomingProjectile");
            HomingProjectile_BW = Content.Load<Texture2D>("Sprites/Projectiles and Effects/HomingProjectile_BW");

            Enemy_Small_1 = Content.Load<Texture2D>("Sprites/Ships/Enemy_Small_1");
            Enemy_Small_2 = Content.Load<Texture2D>("Sprites/Ships/Enemy_Small_2");
            Enemy_Small_3 = Content.Load<Texture2D>("Sprites/Ships/Enemy_Small_3");
            Enemy_Medium_1 = Content.Load<Texture2D>("Sprites/Ships/Enemy_Medium_1");
            Enemy_Medium_2 = Content.Load<Texture2D>("Sprites/Ships/Enemy_Medium_2");
            Enemy_Medium_3 = Content.Load<Texture2D>("Sprites/Ships/Enemy_Medium_3");
            Enemy_Large_1 = Content.Load<Texture2D>("Sprites/Ships/Enemy_Large_1");
            Enemy_Large_2 = Content.Load<Texture2D>("Sprites/Ships/Enemy_Large_2");
            Enemy_Large_3 = Content.Load<Texture2D>("Sprites/Ships/Enemy_Large_3");
            Enemy_Small2_1 = Content.Load<Texture2D>("Sprites/Ships/Enemy_Small2_1");
            Enemy_Small2_2 = Content.Load<Texture2D>("Sprites/Ships/Enemy_Small2_2");
            Enemy_Small2_3 = Content.Load<Texture2D>("Sprites/Ships/Enemy_Small2_3");
            Enemy_Medium2_1 = Content.Load<Texture2D>("Sprites/Ships/Enemy_Medium2_1");
            Enemy_Medium2_2 = Content.Load<Texture2D>("Sprites/Ships/Enemy_Medium2_2");
            Enemy_Medium2_3 = Content.Load<Texture2D>("Sprites/Ships/Enemy_Medium2_3");
            Enemy_Large2_1 = Content.Load<Texture2D>("Sprites/Ships/Enemy_Large2_1");
            Enemy_Large2_2 = Content.Load<Texture2D>("Sprites/Ships/Enemy_Large2_2");
            Enemy_Large2_3 = Content.Load<Texture2D>("Sprites/Ships/Enemy_Large2_3");
            Enemy_Small3_1 = Content.Load<Texture2D>("Sprites/Ships/Enemy_Small3_1");
            Enemy_Small3_2 = Content.Load<Texture2D>("Sprites/Ships/Enemy_Small3_2");
            Enemy_Small3_3 = Content.Load<Texture2D>("Sprites/Ships/Enemy_Small3_3");
            Enemy_Medium3_1 = Content.Load<Texture2D>("Sprites/Ships/Enemy_Medium3_1");
            Enemy_Medium3_2 = Content.Load<Texture2D>("Sprites/Ships/Enemy_Medium3_2");
            Enemy_Medium3_3 = Content.Load<Texture2D>("Sprites/Ships/Enemy_Medium3_3");
            Enemy_Large3_1 = Content.Load<Texture2D>("Sprites/Ships/Enemy_Large3_1");
            Enemy_Large3_2 = Content.Load<Texture2D>("Sprites/Ships/Enemy_Large3_2");
            Enemy_Large3_3 = Content.Load<Texture2D>("Sprites/Ships/Enemy_Large3_3");
            Enemy_Small4_1 = Content.Load<Texture2D>("Sprites/Ships/Enemy_Small4_1");
            Enemy_Small4_2 = Content.Load<Texture2D>("Sprites/Ships/Enemy_Small4_2");
            Enemy_Small4_3 = Content.Load<Texture2D>("Sprites/Ships/Enemy_Small4_3");
            Enemy_Medium4_1 = Content.Load<Texture2D>("Sprites/Ships/Enemy_Medium4_1");
            Enemy_Medium4_2 = Content.Load<Texture2D>("Sprites/Ships/Enemy_Medium4_2");
            Enemy_Medium4_3 = Content.Load<Texture2D>("Sprites/Ships/Enemy_Medium4_3");
            Enemy_Large4_1 = Content.Load<Texture2D>("Sprites/Ships/Enemy_Large4_1");
            Enemy_Large4_2 = Content.Load<Texture2D>("Sprites/Ships/Enemy_Large4_2");
            Enemy_Large4_3 = Content.Load<Texture2D>("Sprites/Ships/Enemy_Large4_3");
            Enemy_Small5_1 = Content.Load<Texture2D>("Sprites/Ships/Enemy_Small5_1");
            Enemy_Small5_2 = Content.Load<Texture2D>("Sprites/Ships/Enemy_Small5_2");
            Enemy_Small5_3 = Content.Load<Texture2D>("Sprites/Ships/Enemy_Small5_3");
            Enemy_Medium5_1 = Content.Load<Texture2D>("Sprites/Ships/Enemy_Medium5_1");
            Enemy_Medium5_2 = Content.Load<Texture2D>("Sprites/Ships/Enemy_Medium5_2");
            Enemy_Medium5_3 = Content.Load<Texture2D>("Sprites/Ships/Enemy_Medium5_3");
            Enemy_Large5_1 = Content.Load<Texture2D>("Sprites/Ships/Enemy_Large5_1");
            Enemy_Large5_2 = Content.Load<Texture2D>("Sprites/Ships/Enemy_Large5_2");
            Enemy_Large5_3 = Content.Load<Texture2D>("Sprites/Ships/Enemy_Large5_3");

            Enemy_Small_BW_1 = Content.Load<Texture2D>("Sprites/Ships/Enemy_Small_BW_1");
            Enemy_Small_BW_2 = Content.Load<Texture2D>("Sprites/Ships/Enemy_Small_BW_2");
            Enemy_Small_BW_3 = Content.Load<Texture2D>("Sprites/Ships/Enemy_Small_BW_3");
            Enemy_Medium_BW_1 = Content.Load<Texture2D>("Sprites/Ships/Enemy_Medium_1_BW");
            Enemy_Medium_BW_2 = Content.Load<Texture2D>("Sprites/Ships/Enemy_Medium_2_BW");
            Enemy_Medium_BW_3 = Content.Load<Texture2D>("Sprites/Ships/Enemy_Medium_3_BW");
            Enemy_Large_BW_1 = Content.Load<Texture2D>("Sprites/Ships/Enemy_Large_1_BW");
            Enemy_Large_BW_2 = Content.Load<Texture2D>("Sprites/Ships/Enemy_Large_2_BW");
            Enemy_Large_BW_3 = Content.Load<Texture2D>("Sprites/Ships/Enemy_Large_3_BW");
            Enemy_Small2_BW_1 = Content.Load<Texture2D>("Sprites/Ships/Enemy_Small2_BW_1");
            Enemy_Small2_BW_2 = Content.Load<Texture2D>("Sprites/Ships/Enemy_Small2_BW_2");
            Enemy_Small2_BW_3 = Content.Load<Texture2D>("Sprites/Ships/Enemy_Small2_BW_3");
            Enemy_Medium2_BW_1 = Content.Load<Texture2D>("Sprites/Ships/Enemy_Medium2_1_BW");
            Enemy_Medium2_BW_2 = Content.Load<Texture2D>("Sprites/Ships/Enemy_Medium2_2_BW");
            Enemy_Medium2_BW_3 = Content.Load<Texture2D>("Sprites/Ships/Enemy_Medium2_3_BW");
            Enemy_Large2_BW_1 = Content.Load<Texture2D>("Sprites/Ships/Enemy_Large2_1_BW");
            Enemy_Large2_BW_2 = Content.Load<Texture2D>("Sprites/Ships/Enemy_Large2_2_BW");
            Enemy_Large2_BW_3 = Content.Load<Texture2D>("Sprites/Ships/Enemy_Large2_3_BW");
            Enemy_Small3_BW_1 = Content.Load<Texture2D>("Sprites/Ships/Enemy_Small3_1_BW");
            Enemy_Small3_BW_2 = Content.Load<Texture2D>("Sprites/Ships/Enemy_Small3_2_BW");
            Enemy_Small3_BW_3 = Content.Load<Texture2D>("Sprites/Ships/Enemy_Small3_3_BW");
            Enemy_Medium3_BW_1 = Content.Load<Texture2D>("Sprites/Ships/Enemy_Medium3_1_BW");
            Enemy_Medium3_BW_2 = Content.Load<Texture2D>("Sprites/Ships/Enemy_Medium3_2_BW");
            Enemy_Medium3_BW_3 = Content.Load<Texture2D>("Sprites/Ships/Enemy_Medium3_3_BW");
            Enemy_Large3_BW_1 = Content.Load<Texture2D>("Sprites/Ships/Enemy_Large3_1_BW");
            Enemy_Large3_BW_2 = Content.Load<Texture2D>("Sprites/Ships/Enemy_Large3_2_BW");
            Enemy_Large3_BW_3 = Content.Load<Texture2D>("Sprites/Ships/Enemy_Large3_3_BW");
            Enemy_Small4_BW_1 = Content.Load<Texture2D>("Sprites/Ships/Enemy_Small4_1_BW");
            Enemy_Small4_BW_2 = Content.Load<Texture2D>("Sprites/Ships/Enemy_Small4_2_BW");
            Enemy_Small4_BW_3 = Content.Load<Texture2D>("Sprites/Ships/Enemy_Small4_3_BW");
            Enemy_Medium4_BW_1 = Content.Load<Texture2D>("Sprites/Ships/Enemy_Medium4_1_BW");
            Enemy_Medium4_BW_2 = Content.Load<Texture2D>("Sprites/Ships/Enemy_Medium4_2_BW");
            Enemy_Medium4_BW_3 = Content.Load<Texture2D>("Sprites/Ships/Enemy_Medium4_3_BW");
            Enemy_Large4_BW_1 = Content.Load<Texture2D>("Sprites/Ships/Enemy_Large4_1_BW");
            Enemy_Large4_BW_2 = Content.Load<Texture2D>("Sprites/Ships/Enemy_Large4_2_BW");
            Enemy_Large4_BW_3 = Content.Load<Texture2D>("Sprites/Ships/Enemy_Large4_3_BW");
            Enemy_Small5_BW_1 = Content.Load<Texture2D>("Sprites/Ships/Enemy_Small5_1_BW");
            Enemy_Small5_BW_2 = Content.Load<Texture2D>("Sprites/Ships/Enemy_Small5_2_BW");
            Enemy_Small5_BW_3 = Content.Load<Texture2D>("Sprites/Ships/Enemy_Small5_3_BW");
            Enemy_Medium5_BW_1 = Content.Load<Texture2D>("Sprites/Ships/Enemy_Medium5_1_BW");
            Enemy_Medium5_BW_2 = Content.Load<Texture2D>("Sprites/Ships/Enemy_Medium5_2_BW");
            Enemy_Medium5_BW_3 = Content.Load<Texture2D>("Sprites/Ships/Enemy_Medium5_3_BW");
            Enemy_Large5_BW_1 = Content.Load<Texture2D>("Sprites/Ships/Enemy_Large5_1_BW");
            Enemy_Large5_BW_2 = Content.Load<Texture2D>("Sprites/Ships/Enemy_Large5_2_BW");
            Enemy_Large5_BW_3 = Content.Load<Texture2D>("Sprites/Ships/Enemy_Large5_3_BW");

            EnemyLaserProjectile_Medium = Content.Load<Texture2D>("Sprites/Projectiles and Effects/EnemyLaserProjectile_Medium");
            EnemyLaserProjectile_Large = Content.Load<Texture2D>("Sprites/Projectiles and Effects/EnemyLaserProjectile_Large");
            EnemyLaserProjectile_Medium_BW = Content.Load<Texture2D>("Sprites/Projectiles and Effects/EnemyLaserProjectile_Medium_BW");
            EnemyLaserProjectile_Large_BW = Content.Load<Texture2D>("Sprites/Projectiles and Effects/EnemyLaserProjectile_Large_BW");

            Background1 = Content.Load<Texture2D>("Sprites/Misc/background_1");
            Background2 = Content.Load<Texture2D>("Sprites/Misc/background_2");
            CreditsPic = Content.Load<Texture2D>("Sprites/Misc/Credits");

            Effect_1 = Content.Load<Texture2D>("Sprites/Projectiles and Effects/Effect_1");
            Effect_2 = Content.Load<Texture2D>("Sprites/Projectiles and Effects/Effect_2");
            Effect_3 = Content.Load<Texture2D>("Sprites/Projectiles and Effects/Effect_3");
            Effect_BW_1 = Content.Load<Texture2D>("Sprites/Projectiles and Effects/Effect_BW_1");
            Effect_BW_2 = Content.Load<Texture2D>("Sprites/Projectiles and Effects/Effect_BW_2");
            Effect_BW_3 = Content.Load<Texture2D>("Sprites/Projectiles and Effects/Effect_BW_3");
            Effect_B_1 = Content.Load<Texture2D>("Sprites/Projectiles and Effects/Effect_B_1");
            Effect_B_2 = Content.Load<Texture2D>("Sprites/Projectiles and Effects/Effect_B_2");
            Effect_B_3 = Content.Load<Texture2D>("Sprites/Projectiles and Effects/Effect_B_3");
            Effect_LB_1 = Content.Load<Texture2D>("Sprites/Projectiles and Effects/Effect_LB_1");
            Effect_LB_2 = Content.Load<Texture2D>("Sprites/Projectiles and Effects/Effect_LB_2");
            Effect_LB_3 = Content.Load<Texture2D>("Sprites/Projectiles and Effects/Effect_LB_3");
            Effect_White_1 = Content.Load<Texture2D>("Sprites/Projectiles and Effects/Effect_White_1");
            Effect_White_2 = Content.Load<Texture2D>("Sprites/Projectiles and Effects/Effect_White_2");
            Effect_White_3 = Content.Load<Texture2D>("Sprites/Projectiles and Effects/Effect_White_3");
            ProjectileEffect_1 = Content.Load<Texture2D>("Sprites/Projectiles and Effects/ProjectileEffect_1");
            ProjectileEffect_2 = Content.Load<Texture2D>("Sprites/Projectiles and Effects/ProjectileEffect_2");
            ProjectileEffect_3 = Content.Load<Texture2D>("Sprites/Projectiles and Effects/ProjectileEffect_3");

            null_1 = Content.Load<Texture2D>("Sprites/Projectiles and Effects/0_1");
            null_2 = Content.Load<Texture2D>("Sprites/Projectiles and Effects/0_2");
            null_3 = Content.Load<Texture2D>("Sprites/Projectiles and Effects/0_3");
            eins_1 = Content.Load<Texture2D>("Sprites/Projectiles and Effects/1_1");
            eins_2 = Content.Load<Texture2D>("Sprites/Projectiles and Effects/1_2");
            eins_3 = Content.Load<Texture2D>("Sprites/Projectiles and Effects/1_3");
            zwei_1 = Content.Load<Texture2D>("Sprites/Projectiles and Effects/2_1");
            zwei_2 = Content.Load<Texture2D>("Sprites/Projectiles and Effects/2_2");
            zwei_3 = Content.Load<Texture2D>("Sprites/Projectiles and Effects/2_3");
            drei_1 = Content.Load<Texture2D>("Sprites/Projectiles and Effects/3_1");
            drei_2 = Content.Load<Texture2D>("Sprites/Projectiles and Effects/3_2");
            drei_3 = Content.Load<Texture2D>("Sprites/Projectiles and Effects/3_3");
            vier_1 = Content.Load<Texture2D>("Sprites/Projectiles and Effects/4_1");
            vier_2 = Content.Load<Texture2D>("Sprites/Projectiles and Effects/4_2");
            vier_3 = Content.Load<Texture2D>("Sprites/Projectiles and Effects/4_3");
            fünf_1 = Content.Load<Texture2D>("Sprites/Projectiles and Effects/5_1");
            fünf_2 = Content.Load<Texture2D>("Sprites/Projectiles and Effects/5_2");
            fünf_3 = Content.Load<Texture2D>("Sprites/Projectiles and Effects/5_3");
            sechs_1 = Content.Load<Texture2D>("Sprites/Projectiles and Effects/6_1");
            sechs_2 = Content.Load<Texture2D>("Sprites/Projectiles and Effects/6_2");
            sechs_3 = Content.Load<Texture2D>("Sprites/Projectiles and Effects/6_3");
            sieben_1 = Content.Load<Texture2D>("Sprites/Projectiles and Effects/7_1");
            sieben_2 = Content.Load<Texture2D>("Sprites/Projectiles and Effects/7_2");
            sieben_3 = Content.Load<Texture2D>("Sprites/Projectiles and Effects/7_3");
            acht_1 = Content.Load<Texture2D>("Sprites/Projectiles and Effects/8_1");
            acht_2 = Content.Load<Texture2D>("Sprites/Projectiles and Effects/8_2");
            acht_3 = Content.Load<Texture2D>("Sprites/Projectiles and Effects/8_3");
            neun_1 = Content.Load<Texture2D>("Sprites/Projectiles and Effects/9_1");
            neun_2 = Content.Load<Texture2D>("Sprites/Projectiles and Effects/9_2");
            neun_3 = Content.Load<Texture2D>("Sprites/Projectiles and Effects/9_3");
            null_BW_1 = Content.Load<Texture2D>("Sprites/Projectiles and Effects/0_BW_1");
            null_BW_2 = Content.Load<Texture2D>("Sprites/Projectiles and Effects/0_BW_2");
            null_BW_3 = Content.Load<Texture2D>("Sprites/Projectiles and Effects/0_BW_3");
            eins_BW_1 = Content.Load<Texture2D>("Sprites/Projectiles and Effects/1_BW_1");
            eins_BW_2 = Content.Load<Texture2D>("Sprites/Projectiles and Effects/1_BW_2");
            eins_BW_3 = Content.Load<Texture2D>("Sprites/Projectiles and Effects/1_BW_3");
            zwei_BW_1 = Content.Load<Texture2D>("Sprites/Projectiles and Effects/2_BW_1");
            zwei_BW_2 = Content.Load<Texture2D>("Sprites/Projectiles and Effects/2_BW_2");
            zwei_BW_3 = Content.Load<Texture2D>("Sprites/Projectiles and Effects/2_BW_3");
            drei_BW_1 = Content.Load<Texture2D>("Sprites/Projectiles and Effects/3_BW_1");
            drei_BW_2 = Content.Load<Texture2D>("Sprites/Projectiles and Effects/3_BW_2");
            drei_BW_3 = Content.Load<Texture2D>("Sprites/Projectiles and Effects/3_BW_3");
            vier_BW_1 = Content.Load<Texture2D>("Sprites/Projectiles and Effects/4_BW_1");
            vier_BW_2 = Content.Load<Texture2D>("Sprites/Projectiles and Effects/4_BW_2");
            vier_BW_3 = Content.Load<Texture2D>("Sprites/Projectiles and Effects/4_BW_3");
            fünf_BW_1 = Content.Load<Texture2D>("Sprites/Projectiles and Effects/5_BW_1");
            fünf_BW_2 = Content.Load<Texture2D>("Sprites/Projectiles and Effects/5_BW_2");
            fünf_BW_3 = Content.Load<Texture2D>("Sprites/Projectiles and Effects/5_BW_3");
            sechs_BW_1 = Content.Load<Texture2D>("Sprites/Projectiles and Effects/6_BW_1");
            sechs_BW_2 = Content.Load<Texture2D>("Sprites/Projectiles and Effects/6_BW_2");
            sechs_BW_3 = Content.Load<Texture2D>("Sprites/Projectiles and Effects/6_BW_3");
            sieben_BW_1 = Content.Load<Texture2D>("Sprites/Projectiles and Effects/7_BW_1");
            sieben_BW_2 = Content.Load<Texture2D>("Sprites/Projectiles and Effects/7_BW_2");
            sieben_BW_3 = Content.Load<Texture2D>("Sprites/Projectiles and Effects/7_BW_3");
            acht_BW_1 = Content.Load<Texture2D>("Sprites/Projectiles and Effects/8_BW_1");
            acht_BW_2 = Content.Load<Texture2D>("Sprites/Projectiles and Effects/8_BW_2");
            acht_BW_3 = Content.Load<Texture2D>("Sprites/Projectiles and Effects/8_BW_3");
            neun_BW_1 = Content.Load<Texture2D>("Sprites/Projectiles and Effects/9_BW_1");
            neun_BW_2 = Content.Load<Texture2D>("Sprites/Projectiles and Effects/9_BW_2");
            neun_BW_3 = Content.Load<Texture2D>("Sprites/Projectiles and Effects/9_BW_3");

            Pickup_LP = Content.Load<Texture2D>("Sprites/Items/Pickup_LP");
            Pickup_R = Content.Load<Texture2D>("Sprites/Items/Pickup_R");
            Pickup_L = Content.Load<Texture2D>("Sprites/Items/Pickup_L");
            Pickup_HV = Content.Load<Texture2D>("Sprites/Items/Pickup_HV");
            Pickup_Li = Content.Load<Texture2D>("Sprites/Items/Pickup_Li");
            Pickup_S = Content.Load<Texture2D>("Sprites/Items/Pickup_S");
            Pickup_LP_BW = Content.Load<Texture2D>("Sprites/Items/Pickup_LP_BW");
            Pickup_R_BW = Content.Load<Texture2D>("Sprites/Items/Pickup_R_BW");
            Pickup_L_BW = Content.Load<Texture2D>("Sprites/Items/Pickup_L_BW");
            Pickup_HV_BW = Content.Load<Texture2D>("Sprites/Items/Pickup_HV_BW");
            Pickup_Li_BW = Content.Load<Texture2D>("Sprites/Items/Pickup_Li_BW");
            Pickup_S_BW = Content.Load<Texture2D>("Sprites/Items/Pickup_S_BW");

            UPickup_LP = Content.Load<Texture2D>("Sprites/Items/UPickup_LP");
            UPickup_R = Content.Load<Texture2D>("Sprites/Items/UPickup_R");
            UPickup_L = Content.Load<Texture2D>("Sprites/Items/UPickup_L");
            UPickup_HV = Content.Load<Texture2D>("Sprites/Items/UPickup_HV");
            UPickup_Li = Content.Load<Texture2D>("Sprites/Items/UPickup_Li");
            UPickup_S = Content.Load<Texture2D>("Sprites/Items/UPickup_S");
            UPickup_LP_BW = Content.Load<Texture2D>("Sprites/Items/UPickup_LP_BW");
            UPickup_R_BW = Content.Load<Texture2D>("Sprites/Items/UPickup_R_BW");
            UPickup_L_BW = Content.Load<Texture2D>("Sprites/Items/UPickup_L_BW");
            UPickup_HV_BW = Content.Load<Texture2D>("Sprites/Items/UPickup_HV_BW");
            UPickup_Li_BW = Content.Load<Texture2D>("Sprites/Items/UPickup_Li_BW");
            UPickup_S_BW = Content.Load<Texture2D>("Sprites/Items/UPickup_S_BW");

            Boss1_1 = Content.Load<Texture2D>("Sprites/Ships/Boss1_1");
            Boss1_2 = Content.Load<Texture2D>("Sprites/Ships/Boss1_2");
            Boss1_3 = Content.Load<Texture2D>("Sprites/Ships/Boss1_3");
            Boss2_1 = Content.Load<Texture2D>("Sprites/Ships/Boss2_1");
            Boss2_2 = Content.Load<Texture2D>("Sprites/Ships/Boss2_2");
            Boss2_3 = Content.Load<Texture2D>("Sprites/Ships/Boss2_3");
            Boss3_1 = Content.Load<Texture2D>("Sprites/Ships/Boss3_1");
            Boss3_2 = Content.Load<Texture2D>("Sprites/Ships/Boss3_2");
            Boss3_3 = Content.Load<Texture2D>("Sprites/Ships/Boss3_3");
            Boss4_1 = Content.Load<Texture2D>("Sprites/Ships/Boss4_1");
            Boss4_2 = Content.Load<Texture2D>("Sprites/Ships/Boss4_2");
            Boss4_3 = Content.Load<Texture2D>("Sprites/Ships/Boss4_3");
            Boss5_1 = Content.Load<Texture2D>("Sprites/Ships/Boss5_1");
            Boss5_2 = Content.Load<Texture2D>("Sprites/Ships/Boss5_2");
            Boss5_3 = Content.Load<Texture2D>("Sprites/Ships/Boss5_3");

            Boss1_BW_1 = Content.Load<Texture2D>("Sprites/Ships/Boss1_1_BW");
            Boss1_BW_2 = Content.Load<Texture2D>("Sprites/Ships/Boss1_2_BW");
            Boss1_BW_3 = Content.Load<Texture2D>("Sprites/Ships/Boss1_3_BW");
            Boss2_BW_1 = Content.Load<Texture2D>("Sprites/Ships/Boss2_1_BW");
            Boss2_BW_2 = Content.Load<Texture2D>("Sprites/Ships/Boss2_2_BW");
            Boss2_BW_3 = Content.Load<Texture2D>("Sprites/Ships/Boss2_3_BW");
            Boss3_BW_1 = Content.Load<Texture2D>("Sprites/Ships/Boss3_1_BW");
            Boss3_BW_2 = Content.Load<Texture2D>("Sprites/Ships/Boss3_2_BW");
            Boss3_BW_3 = Content.Load<Texture2D>("Sprites/Ships/Boss3_3_BW");
            Boss4_BW_1 = Content.Load<Texture2D>("Sprites/Ships/Boss4_1_BW");
            Boss4_BW_2 = Content.Load<Texture2D>("Sprites/Ships/Boss4_2_BW");
            Boss4_BW_3 = Content.Load<Texture2D>("Sprites/Ships/Boss4_3_BW");
            Boss5_BW_1 = Content.Load<Texture2D>("Sprites/Ships/Boss5_1_BW");
            Boss5_BW_2 = Content.Load<Texture2D>("Sprites/Ships/Boss5_2_BW");
            Boss5_BW_3 = Content.Load<Texture2D>("Sprites/Ships/Boss5_3_BW");

            Inv = Content.Load<Texture2D>("Sprites/Misc/Inventory");
            Cursor = Content.Load<Texture2D>("Sprites/Misc/Cursor");
            Cursor_White = Content.Load<Texture2D>("Sprites/Misc/Cursor_White");
            Box = Content.Load<Texture2D>("Sprites/Misc/Box");
            BlackScreen = Content.Load<Texture2D>("Sprites/Misc/BlackScreen");

            Hit = Content.Load<SoundEffect>("Sounds/Hit");
            Click = Content.Load<SoundEffect>("Sounds/Click");
            SmallExp_1 = Content.Load<SoundEffect>("Sounds/SmallExp_1");
            SmallExp_2 = Content.Load<SoundEffect>("Sounds/SmallExp_2");
            SmallExp_3 = Content.Load<SoundEffect>("Sounds/SmallExp_3");
            MediumExp_1 = Content.Load<SoundEffect>("Sounds/MediumExp_1");
            MediumExp_2 = Content.Load<SoundEffect>("Sounds/MediumExp_2");
            MediumExp_3 = Content.Load<SoundEffect>("Sounds/MediumExp_3");
            BigExp_1 = Content.Load<SoundEffect>("Sounds/BigExp_1");
            BigExp_2 = Content.Load<SoundEffect>("Sounds/BigExp_2");
            BigExp_3 = Content.Load<SoundEffect>("Sounds/BigExp_3");
            LPSound_1 = Content.Load<SoundEffect>("Sounds/LPSound_1");
            LPSound_2 = Content.Load<SoundEffect>("Sounds/LPSound_2");
            LPSound_3 = Content.Load<SoundEffect>("Sounds/LPSound_3");
            RocketSound_1 = Content.Load<SoundEffect>("Sounds/RocketSound_1");
            RocketSound_2 = Content.Load<SoundEffect>("Sounds/RocketSound_2");
            RocketSound_3 = Content.Load<SoundEffect>("Sounds/RocketSound_3");
            HVSound_1 = Content.Load<SoundEffect>("Sounds/HVSound_1");
            HVSound_2 = Content.Load<SoundEffect>("Sounds/HVSound_2");
            HVSound_3 = Content.Load<SoundEffect>("Sounds/HVSound_3");
            LightningSound_1 = Content.Load<SoundEffect>("Sounds/LightningSound_1");
            LightningSound_2 = Content.Load<SoundEffect>("Sounds/LightningSound_2");
            LightningSound_3 = Content.Load<SoundEffect>("Sounds/LightningSound_3");
            SWSound_1 = Content.Load<SoundEffect>("Sounds/SWSound_1");
            SWSound_2 = Content.Load<SoundEffect>("Sounds/SWSound_2");
            SWSound_3 = Content.Load<SoundEffect>("Sounds/SWSound_3");
            SoundEffect LS_1 = Content.Load<SoundEffect>("Sounds/LaserSound_1");
            SoundEffect LS_2 = Content.Load<SoundEffect>("Sounds/LaserSound_2");
            SoundEffect LS_3 = Content.Load<SoundEffect>("Sounds/LaserSound_3");
            LaserSound_1 = LS_1.CreateInstance();
            LaserSound_2 = LS_2.CreateInstance();
            LaserSound_3 = LS_3.CreateInstance();
            LaserSound_1.Volume = 0.1f;
            LaserSound_2.Volume = 0.1f;
            LaserSound_3.Volume = 0.1f;

            Font = Content.Load<SpriteFont>("SpriteFont1");

            BG1Position = 0;
            BG2Position = 0;
            CreditsPosition = -150;

            Attacks = new List<Attack>();
            Enemies = new List<Enemy>();
            EnemyAttacks = new List<Attack>();
            Effects = new List<Classes.Effect>();
            Items = new List<Item>();
            Inventory = new List<Item>();
            HighScoresL1 = new List<HighScore>();
            HighScoresL2 = new List<HighScore>();
            HighScoresL3 = new List<HighScore>();
            HighScoresL4 = new List<HighScore>();
            HighScoresL5 = new List<HighScore>();
            ProgressList = new List<ProgressFile>();

            ButtonList = new List<Button>();
            ButtonList.Add(new Button(new Vector2(500, 190), "Start", 1f, false));
            ButtonList.Add(new Button(new Vector2(200, 290), "Start Game", 0.05f, false));
            ButtonList.Add(new Button(new Vector2(200, 490), "End Game", 0.05f, false));
            ButtonList.Add(new Button(new Vector2(315, 390), "Resume", 0f, false));
            ButtonList.Add(new Button(new Vector2(315, 440), "Menu", 0f, false));
            ButtonList.Add(new Button(new Vector2(315, 490), "End", 0f, false));
            ButtonList.Add(new Button(new Vector2(350, 800), "Retry", 0f, false));
            ButtonList.Add(new Button(new Vector2(350, 850), "Return to Menu", 0f, false));
            ButtonList.Add(new Button(new Vector2(400, 290), "Level 1", 0f, false));
            ButtonList.Add(new Button(new Vector2(400, 320), "Level 2", 0f, false));
            ButtonList.Add(new Button(new Vector2(400, 350), "Level 3", 0f, false));
            ButtonList.Add(new Button(new Vector2(400, 380), "Level 4", 0f, false));
            ButtonList.Add(new Button(new Vector2(400, 410), "Level 5", 0f, false));
            ButtonList.Add(new Button(new Vector2(100, 100), "Change", 0f, false));
            ButtonList.Add(new Button(new Vector2(350, 750), "Proceed", 0f, false));
            ButtonList.Add(new Button(new Vector2(200, 357), "Help", 0.05f, false));
            ButtonList.Add(new Button(new Vector2(200, 424), "Options", 0.05f, false));
            ButtonList.Add(new Button(new Vector2(400, 424), "Sounds: On", 0f, false));
            ButtonList.Add(new Button(new Vector2(400, 424), "Sounds: Off", 0f, false));
            ButtonList.Add(new Button(new Vector2(400, 449), "Effects: Normal", 0f, false));
            ButtonList.Add(new Button(new Vector2(400, 449), "Effects: Low", 0f, false));
            ButtonList.Add(new Button(new Vector2(400, 474), "Damage numbers: On", 0f, false));
            ButtonList.Add(new Button(new Vector2(400, 474), "Damage numbers: Off", 0f, false));

            Player = new Player(new Vector2(300, 800), 10, 100, 100, 50, Ship_1, Ship_2, Ship_3);
            Player.Weapons.Add(new Weapon(new Vector2(Player.Position.X + 20, Player.Position.Y + 15), 0, 300, 20, 10, 1, 1, LaserProjectile, LaserProjectile_BW));
            Player.Weapons.Add(new Weapon(new Vector2(Player.Position.X + 130, Player.Position.Y + 15), 0, 300, 20, 10, 1, 1, LaserProjectile, LaserProjectile_BW));
            multiplier = 0f;
            BeastModeTime = 0;
            Score = 0;
            LevelUnlocked = 1;
            Level = 1;
            Name = "Player";

            IAsyncResult result2 = StorageDevice.BeginShowSelector(PlayerIndex.One, null, null);
            device = StorageDevice.EndShowSelector(result2);
            IAsyncResult result = device.BeginOpenContainer("Storage", null, null);

            result.AsyncWaitHandle.WaitOne();

            StorageContainer container = device.EndOpenContainer(result);

            result.AsyncWaitHandle.Close();

            string filename = "savegame.sav";

            if (!container.FileExists(filename))
            {
                container.Dispose();
            }
            else
            {
                Stream stream = container.OpenFile(filename, FileMode.Open);

                XmlSerializer serializer = new XmlSerializer(typeof(SaveGame));

                SaveGame data = (SaveGame)serializer.Deserialize(stream);

                stream.Close();

                container.Dispose();

                Name = data.CurrentName;
                HighScoresL1 = data.HighScoresL1;
                HighScoresL2 = data.HighScoresL2;
                HighScoresL3 = data.HighScoresL3;
                HighScoresL4 = data.HighScoresL4;
                HighScoresL5 = data.HighScoresL5;
                ProgressList = data.ProgressList;
                LevelUnlocked = ProgressList.Find(P => P.Name == Name).UnlockedLevel;
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
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            MouseState mouseState = Mouse.GetState();
            KeyboardState keyState = Keyboard.GetState();

            //________________________Start Screen____________________________________________________________________________________________________________
            if (StartScreen)
            {
                Button StartButton = ButtonList.Find(B => B.Text == "Start");

                //:::::::::::::::::Start Button:::::::::::::::::::::::::::::::::::::::::::::
                if (mouseState.X > StartButton.Position.X & mouseState.X < StartButton.Position.X + StartButton.Width & mouseState.Y > StartButton.Position.Y & mouseState.Y < StartButton.Position.Y + 25)
                {
                    Effects.Add(new Classes.Effect(new Vector2(StartButton.Position.X + rand.Next(0, StartButton.Width), StartButton.Position.Y + rand.Next(0, 26)), MathHelper.ToRadians(rand.Next(0, 360)), rand.Next(1, 3), rand.Next(5, 11), Effect_White_1, Effect_White_2, Effect_White_3, Effect_White_1, Effect_White_2, Effect_White_3));
                    if (mouseState.LeftButton == ButtonState.Pressed)
                    {
                        Menu = true;
                        StartScreen = false;
                        ButtonList.Find(B => B.Text == "Start Game").Visibility = 1f;
                        ButtonList.Find(B => B.Text == "Change").Visibility = 1f;
                        ButtonList.Find(B => B.Text == "Help").Visibility = 1f;
                        ButtonList.Find(B => B.Text == "Options").Visibility = 1f;
                        ButtonList.Find(B => B.Text == "End Game").Visibility = 1f;
                        ButtonList.Find(B => B.Text == "Start").Visibility = 0f;
                    }
                }

                if (mouseState.X != lastMouseState.X | mouseState.Y != lastMouseState.Y)
                {
                    Effects.Add(new Classes.Effect(new Vector2(mouseState.X, mouseState.Y), MathHelper.ToDegrees((float)Math.Atan2(lastMouseState.Y + mouseState.Y, lastMouseState.X + mouseState.X)) + rand.Next(-2, 3), rand.Next(1, 3), rand.Next(5, 11), Effect_White_1, Effect_White_2, Effect_White_3, Effect_White_1, Effect_White_2, Effect_White_3));
                }
                for (int i = 0; i <= Effects.Count - 1; i++)
                {
                    if (Effects.ElementAt<Classes.Effect>(i).Life > 0)
                    {
                        Effects.ElementAt<Classes.Effect>(i).Move();
                    }
                    else
                    {
                        Effects.RemoveAt(i);
                    }
                }

                lastMouseState = mouseState;
            }



            //________________________Menu____________________________________________________________________________________________________________
            else if (Menu)
            {
                Button StartButton = ButtonList.Find(B => B.Text == "Start Game");
                Button ChangeNameButton = ButtonList.Find(B => B.Text == "Change");
                Button HelpButton = ButtonList.Find(B => B.Text == "Help");
                Button OptionsButton = ButtonList.Find(B => B.Text == "Options");
                Button SoundsOnButton = ButtonList.Find(B => B.Text == "Sounds: On");
                Button SoundsOffButton = ButtonList.Find(B => B.Text == "Sounds: Off");
                Button NormalEffectsButton = ButtonList.Find(B => B.Text == "Effects: Normal");
                Button LowEffectsButton = ButtonList.Find(B => B.Text == "Effects: Low");
                Button DNOnButton = ButtonList.Find(B => B.Text == "Damage numbers: On");
                Button DNOffButton = ButtonList.Find(B => B.Text == "Damage numbers: Off");
                Button Level1Button = ButtonList.Find(B => B.Text == "Level 1");
                Button Level2Button = ButtonList.Find(B => B.Text == "Level 2");
                Button Level3Button = ButtonList.Find(B => B.Text == "Level 3");
                Button Level4Button = ButtonList.Find(B => B.Text == "Level 4");
                Button Level5Button = ButtonList.Find(B => B.Text == "Level 5");
                Button EndButton = ButtonList.Find(B => B.Text == "End Game");

                //:::::::::::::::::Change Name Button:::::::::::::::::::::::::::::::::::::::::::::
                if (mouseState.X > ChangeNameButton.Position.X & mouseState.X < ChangeNameButton.Position.X + ChangeNameButton.Width & mouseState.Y > ChangeNameButton.Position.Y & mouseState.Y < ChangeNameButton.Position.Y + 25)
                {
                    Effects.Add(new Classes.Effect(new Vector2(ChangeNameButton.Position.X + rand.Next(0, ChangeNameButton.Width), ChangeNameButton.Position.Y + rand.Next(0, 26)), MathHelper.ToRadians(rand.Next(0, 360)), rand.Next(1, 3), rand.Next(5, 11), Effect_White_1, Effect_White_2, Effect_White_3, Effect_White_1, Effect_White_2, Effect_White_3));
                    if (mouseState.LeftButton == ButtonState.Pressed)
                    {
                        NameChanging = true;
                        Name = "";
                    }
                }
                else
                {
                    if (mouseState.LeftButton == ButtonState.Pressed)
                    {
                        NameChanging = false;
                    }
                }


                //:::::::::::::::::Level Buttons:::::::::::::::::::::::::::::::::::::::::::::
                //.................Level 1...................................................
                if (Level1Button.Visibility == 1f && mouseState.X > Level1Button.Position.X & mouseState.X < Level1Button.Position.X + Level1Button.Width & mouseState.Y > Level1Button.Position.Y & mouseState.Y < Level1Button.Position.Y + 25)
                {
                    Effects.Add(new Classes.Effect(new Vector2(Level1Button.Position.X + rand.Next(0, Level1Button.Width), Level1Button.Position.Y + rand.Next(0, 26)), MathHelper.ToRadians(rand.Next(0, 360)), rand.Next(1, 3), rand.Next(5, 11), Effect_White_1, Effect_White_2, Effect_White_3, Effect_White_1, Effect_White_2, Effect_White_3));
                    if (mouseState.LeftButton == ButtonState.Pressed)
                    {
                        Level = 1;
                        this.ChangeLevel(1);
                        Level1Button.Visibility = 0f;
                        Level2Button.Visibility = 0f;
                        Level3Button.Visibility = 0f;
                        Level4Button.Visibility = 0f;
                        Level5Button.Visibility = 0f;
                        ChangeNameButton.Visibility = 0f;
                        StartButton.Visibility = 0f;
                        HelpButton.Visibility = 0f;
                        OptionsButton.Visibility = 0f;
                        EndButton.Visibility = 0f;
                        FadeCounter = -300;
                        Menu = false;
                    }
                }

                //.................Level 2...................................................
                if (Level2Button.Visibility == 1f && mouseState.X > Level2Button.Position.X & mouseState.X < Level2Button.Position.X + Level2Button.Width & mouseState.Y > Level2Button.Position.Y & mouseState.Y < Level2Button.Position.Y + 25)
                {
                    Effects.Add(new Classes.Effect(new Vector2(Level2Button.Position.X + rand.Next(0, Level2Button.Width), Level2Button.Position.Y + rand.Next(0, 26)), MathHelper.ToRadians(rand.Next(0, 360)), rand.Next(1, 3), rand.Next(5, 11), Effect_White_1, Effect_White_2, Effect_White_3, Effect_White_1, Effect_White_2, Effect_White_3));
                    if (mouseState.LeftButton == ButtonState.Pressed)
                    {
                        Level = 2;
                        this.ChangeLevel(2);
                        Level1Button.Visibility = 0f;
                        Level2Button.Visibility = 0f;
                        Level3Button.Visibility = 0f;
                        Level4Button.Visibility = 0f;
                        Level5Button.Visibility = 0f;
                        ChangeNameButton.Visibility = 0f;
                        StartButton.Visibility = 0f;
                        HelpButton.Visibility = 0f;
                        OptionsButton.Visibility = 0f;
                        EndButton.Visibility = 0f;
                        FadeCounter = -300;
                        Menu = false;
                    }
                }

                //.................Level 3...................................................
                if (Level3Button.Visibility == 1f && mouseState.X > Level3Button.Position.X & mouseState.X < Level3Button.Position.X + Level3Button.Width & mouseState.Y > Level3Button.Position.Y & mouseState.Y < Level3Button.Position.Y + 25)
                {
                    Effects.Add(new Classes.Effect(new Vector2(Level3Button.Position.X + rand.Next(0, Level3Button.Width), Level3Button.Position.Y + rand.Next(0, 26)), MathHelper.ToRadians(rand.Next(0, 360)), rand.Next(1, 3), rand.Next(5, 11), Effect_White_1, Effect_White_2, Effect_White_3, Effect_White_1, Effect_White_2, Effect_White_3));
                    if (mouseState.LeftButton == ButtonState.Pressed)
                    {
                        Level = 3;
                        this.ChangeLevel(3);
                        Level1Button.Visibility = 0f;
                        Level2Button.Visibility = 0f;
                        Level3Button.Visibility = 0f;
                        Level4Button.Visibility = 0f;
                        Level5Button.Visibility = 0f;
                        ChangeNameButton.Visibility = 0f;
                        StartButton.Visibility = 0f;
                        HelpButton.Visibility = 0f;
                        OptionsButton.Visibility = 0f;
                        EndButton.Visibility = 0f;
                        FadeCounter = -300;
                        Menu = false;
                    }
                }

                //.................Level 4...................................................
                if (Level4Button.Visibility == 1f && mouseState.X > Level4Button.Position.X & mouseState.X < Level4Button.Position.X + Level4Button.Width & mouseState.Y > Level4Button.Position.Y & mouseState.Y < Level4Button.Position.Y + 25)
                {
                    Effects.Add(new Classes.Effect(new Vector2(Level4Button.Position.X + rand.Next(0, Level4Button.Width), Level4Button.Position.Y + rand.Next(0, 26)), MathHelper.ToRadians(rand.Next(0, 360)), rand.Next(1, 3), rand.Next(5, 11), Effect_White_1, Effect_White_2, Effect_White_3, Effect_White_1, Effect_White_2, Effect_White_3));
                    if (mouseState.LeftButton == ButtonState.Pressed)
                    {
                        Level = 4;
                        this.ChangeLevel(4);
                        Level1Button.Visibility = 0f;
                        Level2Button.Visibility = 0f;
                        Level3Button.Visibility = 0f;
                        Level4Button.Visibility = 0f;
                        Level5Button.Visibility = 0f;
                        ChangeNameButton.Visibility = 0f;
                        StartButton.Visibility = 0f;
                        HelpButton.Visibility = 0f;
                        OptionsButton.Visibility = 0f;
                        EndButton.Visibility = 0f;
                        FadeCounter = -300;
                        Menu = false;
                    }
                }

                //.................Level 5...................................................
                if (Level5Button.Visibility == 1f && mouseState.X > Level5Button.Position.X & mouseState.X < Level5Button.Position.X + Level5Button.Width & mouseState.Y > Level5Button.Position.Y & mouseState.Y < Level5Button.Position.Y + 25)
                {
                    Effects.Add(new Classes.Effect(new Vector2(Level5Button.Position.X + rand.Next(0, Level5Button.Width), Level5Button.Position.Y + rand.Next(0, 26)), MathHelper.ToRadians(rand.Next(0, 360)), rand.Next(1, 3), rand.Next(5, 11), Effect_White_1, Effect_White_2, Effect_White_3, Effect_White_1, Effect_White_2, Effect_White_3));
                    if (mouseState.LeftButton == ButtonState.Pressed)
                    {
                        Level = 5;
                        this.ChangeLevel(5);
                        Level1Button.Visibility = 0f;
                        Level2Button.Visibility = 0f;
                        Level3Button.Visibility = 0f;
                        Level4Button.Visibility = 0f;
                        Level5Button.Visibility = 0f;
                        ChangeNameButton.Visibility = 0f;
                        StartButton.Visibility = 0f;
                        HelpButton.Visibility = 0f;
                        OptionsButton.Visibility = 0f;
                        EndButton.Visibility = 0f;
                        FadeCounter = -300;
                        Menu = false;
                    }
                }


                //:::::::::::::::::Start Button:::::::::::::::::::::::::::::::::::::::::::::
                if (mouseState.X > StartButton.Position.X & mouseState.X < StartButton.Position.X + StartButton.Width & mouseState.Y > StartButton.Position.Y & mouseState.Y < StartButton.Position.Y + 25)
                {
                    Effects.Add(new Classes.Effect(new Vector2(StartButton.Position.X + rand.Next(0, StartButton.Width), StartButton.Position.Y + rand.Next(0, 26)), MathHelper.ToRadians(rand.Next(0, 360)), rand.Next(1, 3), rand.Next(5, 11), Effect_White_1, Effect_White_2, Effect_White_3, Effect_White_1, Effect_White_2, Effect_White_3));
                    if (mouseState.LeftButton == ButtonState.Pressed)
                    {
                        if (ProgressList.Exists(P => P.Name == Name))
                            LevelUnlocked = ProgressList.Find(P => P.Name == Name).UnlockedLevel;
                        else
                        {
                            ProgressList.Add(new ProgressFile(Name, 1));
                            LevelUnlocked = 1;
                        }
                        Level1Button.Visibility = 1f;
                        if (LevelUnlocked >= 2)
                            Level2Button.Visibility = 1f;
                        else
                            Level2Button.Visibility = 0.25f;
                        if (LevelUnlocked >= 3)
                            Level3Button.Visibility = 1f;
                        else
                            Level3Button.Visibility = 0.25f;
                        if (LevelUnlocked >= 4)
                            Level4Button.Visibility = 1f;
                        else
                            Level4Button.Visibility = 0.25f;
                        if (LevelUnlocked >= 5)
                            Level5Button.Visibility = 1f;
                        else
                            Level5Button.Visibility = 0.25f;
                    }
                }
                else
                {
                    if (mouseState.LeftButton == ButtonState.Pressed)
                    {
                        Level1Button.Visibility = 0f;
                        Level2Button.Visibility = 0f;
                        Level3Button.Visibility = 0f;
                        Level4Button.Visibility = 0f;
                        Level5Button.Visibility = 0f;
                    }
                }


                //:::::::::::::::::Help Button:::::::::::::::::::::::::::::::::::::::::::::
                if (mouseState.X > HelpButton.Position.X & mouseState.X < HelpButton.Position.X + HelpButton.Width & mouseState.Y > HelpButton.Position.Y & mouseState.Y < HelpButton.Position.Y + 25)
                {
                    Effects.Add(new Classes.Effect(new Vector2(HelpButton.Position.X + rand.Next(0, HelpButton.Width), HelpButton.Position.Y + rand.Next(0, 26)), MathHelper.ToRadians(rand.Next(0, 360)), rand.Next(1, 3), rand.Next(5, 11), Effect_White_1, Effect_White_2, Effect_White_3, Effect_White_1, Effect_White_2, Effect_White_3));
                    if (mouseState.LeftButton == ButtonState.Pressed)
                    {
                        Menu = false;
                        HelpScreen = true;
                        Clicked = true;
                    }
                }



                //:::::::::::::::::Options Button:::::::::::::::::::::::::::::::::::::::::::::
                if (mouseState.X > OptionsButton.Position.X & mouseState.X < OptionsButton.Position.X + OptionsButton.Width & mouseState.Y > OptionsButton.Position.Y & mouseState.Y < OptionsButton.Position.Y + 25)
                {
                    Effects.Add(new Classes.Effect(new Vector2(OptionsButton.Position.X + rand.Next(0, OptionsButton.Width), OptionsButton.Position.Y + rand.Next(0, 26)), MathHelper.ToRadians(rand.Next(0, 360)), rand.Next(1, 3), rand.Next(5, 11), Effect_White_1, Effect_White_2, Effect_White_3, Effect_White_1, Effect_White_2, Effect_White_3));
                    if (mouseState.LeftButton == ButtonState.Pressed)
                    {
                        if (SoundsOn)
                            SoundsOnButton.Visibility = 1f;
                        else
                            SoundsOffButton.Visibility = 1f;
                        if (LowParticles)
                            LowEffectsButton.Visibility = 1f;
                        else
                            NormalEffectsButton.Visibility = 1f;
                        if (DamageNumbers)
                            DNOnButton.Visibility = 1f;
                        else
                            DNOffButton.Visibility = 1f;
                    }
                }
                else
                {
                    //.................Sounds Button...................................................
                    if (SoundsOnButton.Visibility == 1f && mouseState.X > SoundsOnButton.Position.X & mouseState.X < SoundsOnButton.Position.X + SoundsOnButton.Width & mouseState.Y > SoundsOnButton.Position.Y & mouseState.Y < SoundsOnButton.Position.Y + 25)
                    {
                        Effects.Add(new Classes.Effect(new Vector2(SoundsOnButton.Position.X + rand.Next(0, SoundsOnButton.Width), SoundsOnButton.Position.Y + rand.Next(0, 26)), MathHelper.ToRadians(rand.Next(0, 360)), rand.Next(1, 3), rand.Next(5, 11), Effect_White_1, Effect_White_2, Effect_White_3, Effect_White_1, Effect_White_2, Effect_White_3));
                        if (mouseState.LeftButton == ButtonState.Pressed & !Clicked)
                        {
                            SoundsOn = false;
                            Clicked = true;
                            SoundsOffButton.Visibility = 1f;
                            SoundsOnButton.Visibility = 0f;
                        }
                    }
                    else if (SoundsOffButton.Visibility == 1f && mouseState.X > SoundsOffButton.Position.X & mouseState.X < SoundsOffButton.Position.X + SoundsOffButton.Width & mouseState.Y > SoundsOffButton.Position.Y & mouseState.Y < SoundsOffButton.Position.Y + 25)
                    {
                        Effects.Add(new Classes.Effect(new Vector2(SoundsOffButton.Position.X + rand.Next(0, SoundsOffButton.Width), SoundsOffButton.Position.Y + rand.Next(0, 26)), MathHelper.ToRadians(rand.Next(0, 360)), rand.Next(1, 3), rand.Next(5, 11), Effect_White_1, Effect_White_2, Effect_White_3, Effect_White_1, Effect_White_2, Effect_White_3));
                        if (mouseState.LeftButton == ButtonState.Pressed & !Clicked)
                        {
                            SoundsOn = true;
                            Clicked = true;
                            SoundsOffButton.Visibility = 0f;
                            SoundsOnButton.Visibility = 1f;
                        }
                    }

                    //.................Effects Button...................................................
                    else if (NormalEffectsButton.Visibility == 1f && mouseState.X > NormalEffectsButton.Position.X & mouseState.X < NormalEffectsButton.Position.X + NormalEffectsButton.Width & mouseState.Y > NormalEffectsButton.Position.Y & mouseState.Y < NormalEffectsButton.Position.Y + 25)
                    {
                        Effects.Add(new Classes.Effect(new Vector2(NormalEffectsButton.Position.X + rand.Next(0, NormalEffectsButton.Width), NormalEffectsButton.Position.Y + rand.Next(0, 26)), MathHelper.ToRadians(rand.Next(0, 360)), rand.Next(1, 3), rand.Next(5, 11), Effect_White_1, Effect_White_2, Effect_White_3, Effect_White_1, Effect_White_2, Effect_White_3));
                        if (mouseState.LeftButton == ButtonState.Pressed & !Clicked)
                        {
                            LowParticles = true;
                            Clicked = true;
                            NormalEffectsButton.Visibility = 0f;
                            LowEffectsButton.Visibility = 1f;
                        }
                    }
                    else if (LowEffectsButton.Visibility == 1f && mouseState.X > LowEffectsButton.Position.X & mouseState.X < LowEffectsButton.Position.X + LowEffectsButton.Width & mouseState.Y > LowEffectsButton.Position.Y & mouseState.Y < LowEffectsButton.Position.Y + 25)
                    {
                        Effects.Add(new Classes.Effect(new Vector2(LowEffectsButton.Position.X + rand.Next(0, LowEffectsButton.Width), LowEffectsButton.Position.Y + rand.Next(0, 26)), MathHelper.ToRadians(rand.Next(0, 360)), rand.Next(1, 3), rand.Next(5, 11), Effect_White_1, Effect_White_2, Effect_White_3, Effect_White_1, Effect_White_2, Effect_White_3));
                        if (mouseState.LeftButton == ButtonState.Pressed & !Clicked)
                        {
                            LowParticles = false;
                            Clicked = true;
                            LowEffectsButton.Visibility = 0f;
                            NormalEffectsButton.Visibility = 1f;
                        }
                    }

                    //.................Damage numbers Button...................................................
                    else if (DNOnButton.Visibility == 1f && mouseState.X > DNOnButton.Position.X & mouseState.X < DNOnButton.Position.X + DNOnButton.Width & mouseState.Y > DNOnButton.Position.Y & mouseState.Y < DNOnButton.Position.Y + 25)
                    {
                        Effects.Add(new Classes.Effect(new Vector2(DNOnButton.Position.X + rand.Next(0, DNOnButton.Width), DNOnButton.Position.Y + rand.Next(0, 26)), MathHelper.ToRadians(rand.Next(0, 360)), rand.Next(1, 3), rand.Next(5, 11), Effect_White_1, Effect_White_2, Effect_White_3, Effect_White_1, Effect_White_2, Effect_White_3));
                        if (mouseState.LeftButton == ButtonState.Pressed & !Clicked)
                        {
                            DamageNumbers = false;
                            Clicked = true;
                            DNOnButton.Visibility = 0f;
                            DNOffButton.Visibility = 1f;
                        }
                    }
                    else if (DNOffButton.Visibility == 1f && mouseState.X > DNOffButton.Position.X & mouseState.X < DNOffButton.Position.X + DNOffButton.Width & mouseState.Y > DNOffButton.Position.Y & mouseState.Y < DNOffButton.Position.Y + 25)
                    {
                        Effects.Add(new Classes.Effect(new Vector2(DNOffButton.Position.X + rand.Next(0, DNOffButton.Width), DNOffButton.Position.Y + rand.Next(0, 26)), MathHelper.ToRadians(rand.Next(0, 360)), rand.Next(1, 3), rand.Next(5, 11), Effect_White_1, Effect_White_2, Effect_White_3, Effect_White_1, Effect_White_2, Effect_White_3));
                        if (mouseState.LeftButton == ButtonState.Pressed & !Clicked)
                        {
                            DamageNumbers = true;
                            Clicked = true;
                            DNOffButton.Visibility = 0f;
                            DNOnButton.Visibility = 1f;
                        }
                    }
                    else if (mouseState.LeftButton == ButtonState.Pressed)
                    {
                        SoundsOnButton.Visibility = 0f;
                        SoundsOffButton.Visibility = 0f;
                        NormalEffectsButton.Visibility = 0f;
                        LowEffectsButton.Visibility = 0f;
                        DNOnButton.Visibility = 0f;
                        DNOffButton.Visibility = 0f;
                    }
                }

                if (mouseState.LeftButton == ButtonState.Released)
                {
                    Clicked = false;
                }



                //:::::::::::::::::End Button:::::::::::::::::::::::::::::::::::::::::::::
                if (mouseState.X > EndButton.Position.X & mouseState.X < EndButton.Position.X + EndButton.Width & mouseState.Y > EndButton.Position.Y & mouseState.Y < EndButton.Position.Y + 25)
                {
                    Effects.Add(new Classes.Effect(new Vector2(EndButton.Position.X + rand.Next(0, EndButton.Width), EndButton.Position.Y + rand.Next(0, 26)), MathHelper.ToRadians(rand.Next(0, 360)), rand.Next(1, 3), rand.Next(5, 11), Effect_White_1, Effect_White_2, Effect_White_3, Effect_White_1, Effect_White_2, Effect_White_3));
                    if (mouseState.LeftButton == ButtonState.Pressed)
                    {
                        this.Exit();
                    }
                }



                //:::::::::::::::::Name Changing:::::::::::::::::::::::::::::::::::::::::::::
                if (NameChanging)
                {
                    Effects.Add(new Classes.Effect(new Vector2(100 + rand.Next(0, 50 + Name.Length * 13), 50 + rand.Next(0, 26)), MathHelper.ToRadians(rand.Next(0, 360)), rand.Next(1, 3), rand.Next(5, 11), Effect_White_1, Effect_White_2, Effect_White_3, Effect_White_1, Effect_White_2, Effect_White_3));
                    Keys[] PressedKeys = keyState.GetPressedKeys();
                    if (PressedKeys.Length > 0)
                    {
                        if (!KeyPressed & PressedKeys[0] == Keys.Back & Name.Length > 0)
                        {
                            Name = Name.Substring(0, Name.Length - 1);
                            KeyPressed = true;
                        }
                        if (!KeyPressed & PressedKeys[0] == Keys.Enter & Name.Length > 0)
                        {
                            NameChanging = false;
                        }
                        if (!KeyPressed & (int)PressedKeys[0] >= 65 & (int)PressedKeys[0] <= 90 & Name.Length < 10)
                        {
                            Name = Name + PressedKeys[0];
                            KeyPressed = true;
                        }
                    }
                    else
                    {
                        KeyPressed = false;
                    }
                }


                if (mouseState.X != lastMouseState.X | mouseState.Y != lastMouseState.Y)
                {
                    Effects.Add(new Classes.Effect(new Vector2(mouseState.X, mouseState.Y), MathHelper.ToDegrees((float)Math.Atan2(lastMouseState.Y + mouseState.Y, lastMouseState.X + mouseState.X)) + rand.Next(-2, 3), rand.Next(1, 3), rand.Next(5, 11), Effect_White_1, Effect_White_2, Effect_White_3, Effect_White_1, Effect_White_2, Effect_White_3));
                }
                for (int i = 0; i <= Effects.Count - 1; i++)
                {
                    if (Effects.ElementAt<Classes.Effect>(i).Life > 0)
                    {
                        Effects.ElementAt<Classes.Effect>(i).Move();
                    }
                    else
                    {
                        Effects.RemoveAt(i);
                    }
                }

                if (FadeCounter > 0)
                    FadeCounter--;
                else if (FadeCounter == 0)
                    FadeCounter = 2000;

                lastMouseState = mouseState;
            }



            //________________________Help Screen____________________________________________________________________________________________________________
            else if (HelpScreen)
            {
                if (mouseState.LeftButton == ButtonState.Pressed & !Clicked)
                {
                    HelpScreen = false;
                    Menu = true;
                }
                if (mouseState.LeftButton == ButtonState.Released)
                {
                    Clicked = false;
                }
            }



            //________________________Pause Menu____________________________________________________________________________________________________________
            else if (Paused)
            {
                if (keyState.IsKeyDown(Keys.Escape) & !EscPressed)
                {
                    EscPressed = true;
                    Paused = false;
                    ButtonList.Find(B => B.Text == "Resume").Visibility = 0f;
                    ButtonList.Find(B => B.Text == "Menu").Visibility = 0f;
                    ButtonList.Find(B => B.Text == "End").Visibility = 0f;
                }
                if (keyState.IsKeyUp(Keys.Escape))
                {
                    EscPressed = false;
                }

                Button ResumeButton = ButtonList.Find(B => B.Text == "Resume");
                Button MenuButton = ButtonList.Find(B => B.Text == "Menu");
                Button EndButton = ButtonList.Find(B => B.Text == "End");

                //:::::::::::::::::Resume Button:::::::::::::::::::::::::::::::::::::::::::::
                if (mouseState.X > ResumeButton.Position.X & mouseState.X < ResumeButton.Position.X + ResumeButton.Width & mouseState.Y > ResumeButton.Position.Y & mouseState.Y < ResumeButton.Position.Y + 25)
                {
                    Effects.Add(new Classes.Effect(new Vector2(ResumeButton.Position.X + rand.Next(0, ResumeButton.Width), ResumeButton.Position.Y + rand.Next(0, 26)), MathHelper.ToRadians(rand.Next(0, 360)), rand.Next(1, 3), rand.Next(5, 11), Effect_White_1, Effect_White_2, Effect_White_3, Effect_White_1, Effect_White_2, Effect_White_3));
                    if (mouseState.LeftButton == ButtonState.Pressed)
                    {
                        Paused = false;
                        ButtonList.Find(B => B.Text == "Resume").Visibility = 0f;
                        ButtonList.Find(B => B.Text == "Menu").Visibility = 0f;
                        ButtonList.Find(B => B.Text == "End").Visibility = 0f;
                    }
                }


                //:::::::::::::::::Menu Button:::::::::::::::::::::::::::::::::::::::::::::
                if (mouseState.X > MenuButton.Position.X & mouseState.X < MenuButton.Position.X + MenuButton.Width & mouseState.Y > MenuButton.Position.Y & mouseState.Y < MenuButton.Position.Y + 25)
                {
                    Effects.Add(new Classes.Effect(new Vector2(MenuButton.Position.X + rand.Next(0, MenuButton.Width), MenuButton.Position.Y + rand.Next(0, 26)), MathHelper.ToRadians(rand.Next(0, 360)), rand.Next(1, 3), rand.Next(5, 11), Effect_White_1, Effect_White_2, Effect_White_3, Effect_White_1, Effect_White_2, Effect_White_3));
                    if (mouseState.LeftButton == ButtonState.Pressed)
                    {
                        Menu = true;
                        Paused = false;
                        ItemOnHold = null;
                        ButtonList.Find(B => B.Text == "Resume").Visibility = 0f;
                        ButtonList.Find(B => B.Text == "Menu").Visibility = 0f;
                        ButtonList.Find(B => B.Text == "End").Visibility = 0f;
                        ButtonList.Find(B => B.Text == "Start Game").Visibility = 1f;
                        ButtonList.Find(B => B.Text == "Help").Visibility = 1f;
                        ButtonList.Find(B => B.Text == "Options").Visibility = 1f;
                        ButtonList.Find(B => B.Text == "End Game").Visibility = 1f;
                    }
                }


                //:::::::::::::::::End Button:::::::::::::::::::::::::::::::::::::::::::::
                if (mouseState.X > EndButton.Position.X & mouseState.X < EndButton.Position.X + EndButton.Width & mouseState.Y > EndButton.Position.Y & mouseState.Y < EndButton.Position.Y + 25)
                {
                    Effects.Add(new Classes.Effect(new Vector2(EndButton.Position.X + rand.Next(0, EndButton.Width), EndButton.Position.Y + rand.Next(0, 26)), MathHelper.ToRadians(rand.Next(0, 360)), rand.Next(1, 3), rand.Next(5, 11), Effect_White_1, Effect_White_2, Effect_White_3, Effect_White_1, Effect_White_2, Effect_White_3));
                    if (mouseState.LeftButton == ButtonState.Pressed)
                    {
                        this.Exit();
                    }
                }


                if (mouseState.X != lastMouseState.X | mouseState.Y != lastMouseState.Y)
                {
                    Effects.Add(new Classes.Effect(new Vector2(mouseState.X, mouseState.Y), MathHelper.ToDegrees((float)Math.Atan2(lastMouseState.Y + mouseState.Y, lastMouseState.X + mouseState.X)) + rand.Next(-2, 3), rand.Next(1, 3), rand.Next(5, 11), Effect_White_1, Effect_White_2, Effect_White_3, Effect_White_1, Effect_White_2, Effect_White_3));
                }

                for (int i = 0; i <= Effects.Count - 1; i++)
                {
                    if (Effects.ElementAt<Classes.Effect>(i).Life > 0)
                    {
                        Effects.ElementAt<Classes.Effect>(i).Move();
                    }
                    else
                    {
                        Effects.RemoveAt(i);
                    }
                }

                lastMouseState = mouseState;
            }



            //________________________Inventory____________________________________________________________________________________________________________
            else if (InvOpen)
            {
                if (keyState.IsKeyDown(Keys.Escape) & !EscPressed)
                {
                    EscPressed = true;
                    Paused = true;
                    ButtonList.Find(B => B.Text == "Resume").Visibility = 1f;
                    ButtonList.Find(B => B.Text == "Menu").Visibility = 1f;
                    ButtonList.Find(B => B.Text == "End").Visibility = 1f;
                }
                if (keyState.IsKeyUp(Keys.Escape))
                {
                    EscPressed = false;
                }


                //:::::::::::::::::Exiting:::::::::::::::::::::::::::::::::::::::::::::
                if (keyState.IsKeyDown(Keys.Tab) & !TabPressed)
                {
                    if (EquippedLeft != null)
                    {
                        switch (EquippedLeft.Type)
                        {
                            case 1:
                                Player.Weapons[0] = new Weapon(new Vector2(Player.Position.X + 37, Player.Position.Y + 60), 0, EquippedLeft.FireRate, EquippedLeft.ProjectileSpeed, EquippedLeft.Damage, EquippedLeft.Projectiles, EquippedLeft.Type, LaserProjectile, LaserProjectile_BW);
                                break;
                            case 2:
                                Player.Weapons[0] = new Weapon(new Vector2(Player.Position.X + 37, Player.Position.Y + 60), 0, EquippedLeft.FireRate, EquippedLeft.ProjectileSpeed, EquippedLeft.Damage, EquippedLeft.Projectiles, EquippedLeft.Type, Rocket, Rocket_BW);
                                break;
                            case 3:
                                Player.Weapons[0] = new Weapon(new Vector2(Player.Position.X + 37, Player.Position.Y + 60), 0, EquippedLeft.FireRate, EquippedLeft.ProjectileSpeed, EquippedLeft.Damage, EquippedLeft.Projectiles, EquippedLeft.Type, Laser, Laser_BW);
                                break;
                            case 4:
                                Player.Weapons[0] = new Weapon(new Vector2(Player.Position.X + 37, Player.Position.Y + 60), 0, EquippedLeft.FireRate, EquippedLeft.ProjectileSpeed, EquippedLeft.Damage, EquippedLeft.Projectiles, EquippedLeft.Type, HomingProjectile, HomingProjectile_BW);
                                break;
                            case 5:
                                Player.Weapons[0] = new Weapon(new Vector2(Player.Position.X + 37, Player.Position.Y + 60), 0, EquippedLeft.FireRate, EquippedLeft.ProjectileSpeed, EquippedLeft.Damage, EquippedLeft.Projectiles, EquippedLeft.Type, null, null);
                                break;
                            case 6:
                                Player.Weapons[0] = new Weapon(new Vector2(Player.Position.X + 37, Player.Position.Y + 60), 0, EquippedLeft.FireRate, EquippedLeft.ProjectileSpeed, EquippedLeft.Damage, EquippedLeft.Projectiles, EquippedLeft.Type, null, null);
                                break;
                        }
                    }
                    else
                    {
                        Player.Weapons[0] = (new Weapon(new Vector2(Player.Position.X + 35, Player.Position.Y + 50), 0, 300, 20, 10, 1, 1, LaserProjectile, LaserProjectile_BW));
                    }
                    if (EquippedRight != null)
                    {
                        switch (EquippedRight.Type)
                        {
                            case 1:
                                Player.Weapons[1] = new Weapon(new Vector2(Player.Position.X + 113, Player.Position.Y + 60), 0, EquippedRight.FireRate, EquippedRight.ProjectileSpeed, EquippedRight.Damage, EquippedRight.Projectiles, EquippedRight.Type, LaserProjectile, LaserProjectile_BW);
                                break;
                            case 2:
                                Player.Weapons[1] = new Weapon(new Vector2(Player.Position.X + 113, Player.Position.Y + 60), 0, EquippedRight.FireRate, EquippedRight.ProjectileSpeed, EquippedRight.Damage, EquippedRight.Projectiles, EquippedRight.Type, Rocket, Rocket_BW);
                                break;
                            case 3:
                                Player.Weapons[1] = new Weapon(new Vector2(Player.Position.X + 113, Player.Position.Y + 60), 0, EquippedRight.FireRate, EquippedRight.ProjectileSpeed, EquippedRight.Damage, EquippedRight.Projectiles, EquippedRight.Type, Laser, Laser_BW);
                                break;
                            case 4:
                                Player.Weapons[1] = new Weapon(new Vector2(Player.Position.X + 113, Player.Position.Y + 60), 0, EquippedRight.FireRate, EquippedRight.ProjectileSpeed, EquippedRight.Damage, EquippedRight.Projectiles, EquippedRight.Type, HomingProjectile, HomingProjectile_BW);
                                break;
                            case 5:
                                Player.Weapons[1] = new Weapon(new Vector2(Player.Position.X + 113, Player.Position.Y + 60), 0, EquippedRight.FireRate, EquippedRight.ProjectileSpeed, EquippedRight.Damage, EquippedRight.Projectiles, EquippedRight.Type, null, null);
                                break;
                            case 6:
                                Player.Weapons[1] = new Weapon(new Vector2(Player.Position.X + 113, Player.Position.Y + 60), 0, EquippedRight.FireRate, EquippedRight.ProjectileSpeed, EquippedRight.Damage, EquippedRight.Projectiles, EquippedRight.Type, null, null);
                                break;
                        }
                    }
                    else
                    {
                        Player.Weapons[1] = (new Weapon(new Vector2(Player.Position.X + 115, Player.Position.Y + 50), 0, 300, 20, 10, 1, 1, LaserProjectile, LaserProjectile_BW));
                    }
                    InvOpen = false;
                    TabPressed = true;
                }
                if (keyState.IsKeyUp(Keys.Tab))
                {
                    TabPressed = false;
                }


                if (mouseState.X != lastMouseState.X | mouseState.Y != lastMouseState.Y)
                {
                    Effects.Add(new Classes.Effect(new Vector2(mouseState.X, mouseState.Y), MathHelper.ToDegrees((float)Math.Atan2(lastMouseState.Y + mouseState.Y, lastMouseState.X + mouseState.X)) + rand.Next(-2, 3), rand.Next(1, 3), rand.Next(5, 11), Effect_White_1, Effect_White_2, Effect_White_3, Effect_White_1, Effect_White_2, Effect_White_3));
                }


                //:::::::::::::::::Setting Item Positions:::::::::::::::::::::::::::::::::::::::::::::
                int X = 360;
                int Y = 310;
                foreach (Item Item in Inventory)
                {
                    if (mouseState.X > Item.Position.X & mouseState.X < Item.Position.X + Item.Sprite.Width & mouseState.Y > Item.Position.Y & mouseState.Y < Item.Position.Y + Item.Sprite.Height & mouseState.LeftButton == ButtonState.Pressed & GrabbedItem == null)
                    {
                        GrabbedItem = Item;
                    }
                    else if (Item != EquippedLeft & Item != EquippedRight)
                    {
                        Item.Position = new Vector2(X, Y);
                    }
                    X += 55;
                    if (X > 700)
                    {
                        X = 360;
                        Y += 55;
                    }
                }
                if (GrabbedItem != null)
                {
                    GrabbedItem.Position = new Vector2(mouseState.X - GrabbedItem.Sprite.Width / 2, mouseState.Y - GrabbedItem.Sprite.Height / 2);
                }



                //:::::::::::::::::Releasing the grabbed Item:::::::::::::::::::::::::::::::::::::::::::::
                if (mouseState.LeftButton == ButtonState.Released & GrabbedItem != null)
                {
                    if (GrabbedItem is WeaponPickup && GrabbedItem.Position.X + GrabbedItem.Sprite.Width > 437 & GrabbedItem.Position.X < 481 & GrabbedItem.Position.Y + GrabbedItem.Sprite.Height > 72 & GrabbedItem.Position.Y < 109)
                    {
                        if (SoundsOn)
                            Click.Play();
                        EquippedLeft = GrabbedItem as WeaponPickup;
                        EquippedLeft.Position = new Vector2(437, 72);
                        if (EquippedRight == GrabbedItem)
                        {
                            EquippedRight = null;
                        }
                    }
                    else if (GrabbedItem is WeaponPickup && GrabbedItem.Position.X + GrabbedItem.Sprite.Width > 633 & GrabbedItem.Position.X < 680 & GrabbedItem.Position.Y + GrabbedItem.Sprite.Height > 72 & GrabbedItem.Position.Y < 109)
                    {
                        if (SoundsOn)
                            Click.Play();
                        EquippedRight = GrabbedItem as WeaponPickup;
                        EquippedRight.Position = new Vector2(633, 72);
                        if (EquippedLeft == GrabbedItem)
                        {
                            EquippedLeft = null;
                        }
                    }
                    else if (GrabbedItem == EquippedLeft)
                    {
                        EquippedLeft = null;
                    }
                    else if (GrabbedItem == EquippedRight)
                    {
                        EquippedRight = null;
                    }
                    if (GrabbedItem.Position.X < 300)
                    {
                        Inventory.Remove(GrabbedItem);
                    }
                    GrabbedItem = null;
                }


                for (int i = 0; i <= Effects.Count - 1; i++)
                {
                    if (Effects.ElementAt<Classes.Effect>(i).Life > 0)
                    {
                        Effects.ElementAt<Classes.Effect>(i).Move();
                    }
                    else
                    {
                        Effects.RemoveAt(i);
                    }
                }

                lastMouseState = mouseState;
            }



            //________________________Game Over Screen____________________________________________________________________________________________________________
            else if (GameOver)
            {
                if (FadeCounter < 0)
                {
                    FadeCounter++;
                }

                Button ProceedButton = ButtonList.Find(B => B.Text == "Proceed");
                Button RetryButton = ButtonList.Find(B => B.Text == "Retry");
                Button ReturnButton = ButtonList.Find(B => B.Text == "Return to Menu");
                //:::::::::::::::::Proceed Button:::::::::::::::::::::::::::::::::::::::::::::
                if (ProceedButton.Visibility > 0f && mouseState.X > ProceedButton.Position.X & mouseState.X < ProceedButton.Position.X + ProceedButton.Width & mouseState.Y > ProceedButton.Position.Y & mouseState.Y < ProceedButton.Position.Y + 25)
                {
                    Effects.Add(new Classes.Effect(new Vector2(ProceedButton.Position.X + rand.Next(0, ProceedButton.Width), ProceedButton.Position.Y + rand.Next(0, 26)), MathHelper.ToRadians(rand.Next(0, 360)), rand.Next(1, 3), rand.Next(5, 11), Effect_White_1, Effect_White_2, Effect_White_3, Effect_White_1, Effect_White_2, Effect_White_3));
                    if (mouseState.LeftButton == ButtonState.Pressed)
                    {
                        GameOver = false;
                        if (Level == 5)
                        {
                            Credits = true;
                        }
                        else
                        {
                            Level++;
                            this.ChangeLevel(Level);
                            ItemOnHold = null;
                            HoldOnCounter = 0;
                            multiplier = 0f;
                            BeastModeTime = 0;
                            FadeCounter = -300;
                        }
                        ProceedButton.Visibility = 0f;
                        RetryButton.Visibility = 0f;
                        ReturnButton.Visibility = 0f;
                    }
                }


                //:::::::::::::::::Return Button:::::::::::::::::::::::::::::::::::::::::::::
                if (mouseState.X > ReturnButton.Position.X & mouseState.X < ReturnButton.Position.X + ReturnButton.Width & mouseState.Y > ReturnButton.Position.Y & mouseState.Y < ReturnButton.Position.Y + 25)
                {
                    Effects.Add(new Classes.Effect(new Vector2(ReturnButton.Position.X + rand.Next(0, ReturnButton.Width), ReturnButton.Position.Y + rand.Next(0, 26)), MathHelper.ToRadians(rand.Next(0, 360)), rand.Next(1, 3), rand.Next(5, 11), Effect_White_1, Effect_White_2, Effect_White_3, Effect_White_1, Effect_White_2, Effect_White_3));
                    if (mouseState.LeftButton == ButtonState.Pressed)
                    {
                        GameOver = false;
                        ItemOnHold = null;
                        HoldOnCounter = 0;
                        multiplier = 0f;
                        BeastModeTime = 0;
                        FadeCounter = 2000;
                        Menu = true;
                        ButtonList.Find(B => B.Text == "Start Game").Visibility = 1f;
                        ButtonList.Find(B => B.Text == "Help").Visibility = 1f;
                        ButtonList.Find(B => B.Text == "Options").Visibility = 1f;
                        ButtonList.Find(B => B.Text == "End Game").Visibility = 1f;
                        ButtonList.Find(B => B.Text == "Change").Visibility = 1f;
                        ProceedButton.Visibility = 0f;
                        RetryButton.Visibility = 0f;
                        ReturnButton.Visibility = 0f;
                    }
                }


                //:::::::::::::::::Retry Button:::::::::::::::::::::::::::::::::::::::::::::
                if (mouseState.X > RetryButton.Position.X & mouseState.X < RetryButton.Position.X + RetryButton.Width & mouseState.Y > RetryButton.Position.Y & mouseState.Y < RetryButton.Position.Y + 25)
                {
                    Effects.Add(new Classes.Effect(new Vector2(RetryButton.Position.X + rand.Next(0, RetryButton.Width), RetryButton.Position.Y + rand.Next(0, 26)), MathHelper.ToRadians(rand.Next(0, 360)), rand.Next(1, 3), rand.Next(5, 11), Effect_White_1, Effect_White_2, Effect_White_3, Effect_White_1, Effect_White_2, Effect_White_3));
                    if (mouseState.LeftButton == ButtonState.Pressed)
                    {
                        GameOver = false;
                        this.ChangeLevel(Level);
                        ItemOnHold = null;
                        HoldOnCounter = 0;
                        multiplier = 0f;
                        BeastModeTime = 0;
                        FadeCounter = -300;
                        ProceedButton.Visibility = 0f;
                        RetryButton.Visibility = 0f;
                        ReturnButton.Visibility = 0f;
                    }
                }


                if (mouseState.X != lastMouseState.X | mouseState.Y != lastMouseState.Y)
                {
                    Effects.Add(new Classes.Effect(new Vector2(mouseState.X, mouseState.Y), MathHelper.ToDegrees((float)Math.Atan2(lastMouseState.Y + mouseState.Y, lastMouseState.X + mouseState.X)) + rand.Next(-2, 3), rand.Next(1, 3), rand.Next(5, 11), Effect_White_1, Effect_White_2, Effect_White_3, Effect_White_1, Effect_White_2, Effect_White_3));
                }

                for (int i = 0; i <= Effects.Count - 1; i++)
                {
                    if (Effects.ElementAt<Classes.Effect>(i).Life > 0)
                    {
                        Effects.ElementAt<Classes.Effect>(i).Move();
                    }
                    else
                    {
                        Effects.RemoveAt(i);
                    }
                }

                lastMouseState = mouseState;
            }



            //________________________Credits____________________________________________________________________________________________________________
            else if (Credits)
            {
                CreditsPosition++;
                if (keyState.IsKeyDown(Keys.Enter) | CreditsPosition == 3300)
                {
                    CreditsPosition = -150;
                    Credits = false;
                    FadeCounter = 2000;
                    Menu = true;
                    ButtonList.Find(B => B.Text == "Start Game").Visibility = 1f;
                    ButtonList.Find(B => B.Text == "Help").Visibility = 1f;
                    ButtonList.Find(B => B.Text == "Options").Visibility = 1f;
                    ButtonList.Find(B => B.Text == "End Game").Visibility = 1f;
                    ButtonList.Find(B => B.Text == "Change").Visibility = 1f;
                }
            }



            //________________________Main Game____________________________________________________________________________________________________________
            else
            {
                enemySpawner.update(Enemies, gameTime);

                int minX = 0;
                int maxX = graphics.GraphicsDevice.Viewport.Width;
                int minY = 0;
                int maxY = graphics.GraphicsDevice.Viewport.Height;

                foreach (Enemy Enemy in Enemies)
                {
                    Enemy.ShotTime += gameTime.ElapsedGameTime.Milliseconds;
                }



                //--------------------------------------------Updating Player--------------------------------------------
                if (Player != null)
                {
                    //::::::::::::::::::::::::::::::::::::::::::::Beastmode and multiplier::::::::::::::::::::::::::::::::::::::::::::
                    if (BeastModeTime <= 0)
                    {
                        if (multiplier > 0f)
                        {
                            if (multiplier > 10f)
                            {
                                if (BeastModeTime == 0)
                                {
                                    for (int i = 0; i <= 2000 / (1 + Convert.ToInt16(LowParticles)); i++)
                                    {
                                        Effects.Add(new Classes.Effect(new Vector2(Player.Position.X + rand.Next(0, Player.Sprite100.Width), Player.Position.Y + rand.Next(0, Player.Sprite100.Height)), MathHelper.ToRadians(rand.Next(360)), rand.Next(1, 20), rand.Next(10, 50), Effect_White_1, Effect_White_2, Effect_White_3, Effect_White_1, Effect_White_2, Effect_White_3));
                                    }
                                    BeastModeTime = 50;
                                }
                                else
                                {
                                    BeastModeTime++;
                                }
                                multiplier = MathHelper.Clamp(multiplier, 0, 10);
                            }
                            else if (Boss != null && !Boss.Spawned)
                            {
                                if (BeastModeTime < 0)
                                {
                                    BeastModeTime++;
                                }
                                if (multiplier < 10f)
                                {
                                    multiplier -= 0.001f;
                                }
                            }
                        }
                        else
                        {
                            multiplier = 0f;
                        }
                    }
                    else if (BeastModeTime == 1)
                    {
                        BeastModeTime = -1000;
                    }
                    else if (BeastModeTime > 1)
                    {
                        if (Enemies.Count > 0)
                        {
                            BeastModeTime--;
                        }
                        Effects.Add(new Classes.Effect(new Vector2(Player.Position.X + rand.Next(0, Player.Sprite100.Width), Player.Position.Y + rand.Next(0, Player.Sprite100.Height)), MathHelper.ToRadians(rand.Next(360)), rand.Next(1, 10), rand.Next(10, 30), Effect_White_1, Effect_White_2, Effect_White_3, Effect_White_1, Effect_White_2, Effect_White_3));
                        if (!LowParticles)
                            Effects.Add(new Classes.Effect(new Vector2(Player.Position.X + rand.Next(0, Player.Sprite100.Width), Player.Position.Y + rand.Next(0, Player.Sprite100.Height)), MathHelper.ToRadians(rand.Next(360)), rand.Next(1, 10), rand.Next(10, 30), Effect_White_1, Effect_White_2, Effect_White_3, Effect_White_1, Effect_White_2, Effect_White_3));
                        multiplier = 10f;
                    }


                    foreach (Weapon Weapon in Player.Weapons)
                    {
                        Weapon.ShotTime += gameTime.ElapsedGameTime.Milliseconds;
                    }


                    //::::::::::::::::::::::::::::::::::::::::::::Keyboard Input::::::::::::::::::::::::::::::::::::::::::::
                    //............................................Movement..................................................
                    if (keyState.IsKeyDown(Keys.Up))
                    {
                        if (Player.Position.Y > minY)
                        {
                            Player.Move(1);
                            Effects.Add(new Classes.Effect(new Vector2(Player.Position.X + Player.Sprite100.Width / 2, Player.Position.Y + Player.Sprite100.Height / 2), MathHelper.ToRadians(rand.Next(30, 150)), rand.Next(1, 5), rand.Next(20, 30), Effect_1, Effect_2, Effect_3, Effect_BW_1, Effect_BW_2, Effect_BW_3));
                        }
                    }
                    if (keyState.IsKeyDown(Keys.Right))
                    {
                        if (Player.Position.X + Player.Sprite100.Width < maxX)
                        {
                            Player.Move(2);
                            Effects.Add(new Classes.Effect(new Vector2(Player.Position.X + Player.Sprite100.Width / 2, Player.Position.Y + Player.Sprite100.Height / 2), MathHelper.ToRadians(rand.Next(120, 240)), rand.Next(1, 5), rand.Next(20, 30), Effect_1, Effect_2, Effect_3, Effect_BW_1, Effect_BW_2, Effect_BW_3));
                        }
                    }
                    if (keyState.IsKeyDown(Keys.Down))
                    {
                        if (Player.Position.Y + Player.Sprite100.Height < maxY)
                        {
                            Player.Move(3);
                            Effects.Add(new Classes.Effect(new Vector2(Player.Position.X + Player.Sprite100.Width / 2, Player.Position.Y + Player.Sprite100.Height / 2), MathHelper.ToRadians(rand.Next(210, 330)), rand.Next(1, 5), rand.Next(20, 30), Effect_1, Effect_2, Effect_3, Effect_BW_1, Effect_BW_2, Effect_BW_3));
                        }
                    }
                    if (keyState.IsKeyDown(Keys.Left))
                    {
                        if (Player.Position.X > minX)
                        {
                            Player.Move(4);
                            Effects.Add(new Classes.Effect(new Vector2(Player.Position.X + Player.Sprite100.Width / 2, Player.Position.Y + Player.Sprite100.Height / 2), MathHelper.ToRadians(rand.Next(-60, 60)), rand.Next(1, 5), rand.Next(20, 30), Effect_1, Effect_2, Effect_3, Effect_BW_1, Effect_BW_2, Effect_BW_3));
                        }
                    }



                    //............................................Weapon switching..................................................
                    if (ItemOnHold != null)
                    {
                        WeaponPickup tempItem;

                        //Left Weapon
                        if (keyState.IsKeyDown(Keys.Q) & !QPressed)
                        {
                            QPressed = true;
                            switch (ItemOnHold.Type)
                            {
                                case 1:
                                    Player.Weapons[0] = new Weapon(new Vector2(Player.Position.X + 37, Player.Position.Y + 60), 0, ItemOnHold.FireRate, ItemOnHold.ProjectileSpeed, ItemOnHold.Damage, ItemOnHold.Projectiles, ItemOnHold.Type, LaserProjectile, LaserProjectile_BW);
                                    break;
                                case 2:
                                    Player.Weapons[0] = new Weapon(new Vector2(Player.Position.X + 37, Player.Position.Y + 60), 0, ItemOnHold.FireRate, ItemOnHold.ProjectileSpeed, ItemOnHold.Damage, ItemOnHold.Projectiles, ItemOnHold.Type, Rocket, Rocket_BW);
                                    break;
                                case 3:
                                    Player.Weapons[0] = new Weapon(new Vector2(Player.Position.X + 37, Player.Position.Y + 60), 0, ItemOnHold.FireRate, ItemOnHold.ProjectileSpeed, ItemOnHold.Damage, ItemOnHold.Projectiles, ItemOnHold.Type, Laser, Laser_BW);
                                    break;
                                case 4:
                                    Player.Weapons[0] = new Weapon(new Vector2(Player.Position.X + 37, Player.Position.Y + 60), 0, ItemOnHold.FireRate, ItemOnHold.ProjectileSpeed, ItemOnHold.Damage, ItemOnHold.Projectiles, ItemOnHold.Type, HomingProjectile, HomingProjectile_BW);
                                    break;
                                case 5:
                                    Player.Weapons[0] = new Weapon(new Vector2(Player.Position.X + 37, Player.Position.Y + 60), 0, ItemOnHold.FireRate, ItemOnHold.ProjectileSpeed, ItemOnHold.Damage, ItemOnHold.Projectiles, ItemOnHold.Type, null, null);
                                    break;
                                case 6:
                                    Player.Weapons[0] = new Weapon(new Vector2(Player.Position.X + 37, Player.Position.Y + 60), 0, ItemOnHold.FireRate, ItemOnHold.ProjectileSpeed, ItemOnHold.Damage, ItemOnHold.Projectiles, ItemOnHold.Type, null, null);
                                    break;
                            }

                            tempItem = EquippedLeft;
                            EquippedLeft = ItemOnHold;
                            EquippedLeft.Position = new Vector2(437, 72);
                            ItemOnHold = tempItem;
                            TestPhase = !TestPhase;
                            HoldOnCounter = Convert.ToInt16(TestPhase) * 500;
                            LaserSound_1.Stop();
                            LaserSound_2.Stop();
                            LaserSound_3.Stop();
                        }

                        //Right Weapon
                        if (keyState.IsKeyDown(Keys.W) & !WPressed)
                        {
                            WPressed = true;
                            switch (ItemOnHold.Type)
                            {
                                case 1:
                                    Player.Weapons[1] = new Weapon(new Vector2(Player.Position.X + 113, Player.Position.Y + 60), 0, ItemOnHold.FireRate, ItemOnHold.ProjectileSpeed, ItemOnHold.Damage, ItemOnHold.Projectiles, ItemOnHold.Type, LaserProjectile, LaserProjectile_BW);
                                    break;
                                case 2:
                                    Player.Weapons[1] = new Weapon(new Vector2(Player.Position.X + 113, Player.Position.Y + 60), 0, ItemOnHold.FireRate, ItemOnHold.ProjectileSpeed, ItemOnHold.Damage, ItemOnHold.Projectiles, ItemOnHold.Type, Rocket, Rocket_BW);
                                    break;
                                case 3:
                                    Player.Weapons[1] = new Weapon(new Vector2(Player.Position.X + 113, Player.Position.Y + 60), 0, ItemOnHold.FireRate, ItemOnHold.ProjectileSpeed, ItemOnHold.Damage, ItemOnHold.Projectiles, ItemOnHold.Type, Laser, Laser_BW);
                                    break;
                                case 4:
                                    Player.Weapons[1] = new Weapon(new Vector2(Player.Position.X + 113, Player.Position.Y + 60), 0, ItemOnHold.FireRate, ItemOnHold.ProjectileSpeed, ItemOnHold.Damage, ItemOnHold.Projectiles, ItemOnHold.Type, HomingProjectile, HomingProjectile_BW);
                                    break;
                                case 5:
                                    Player.Weapons[1] = new Weapon(new Vector2(Player.Position.X + 113, Player.Position.Y + 60), 0, ItemOnHold.FireRate, ItemOnHold.ProjectileSpeed, ItemOnHold.Damage, ItemOnHold.Projectiles, ItemOnHold.Type, null, null);
                                    break;
                                case 6:
                                    Player.Weapons[1] = new Weapon(new Vector2(Player.Position.X + 113, Player.Position.Y + 60), 0, ItemOnHold.FireRate, ItemOnHold.ProjectileSpeed, ItemOnHold.Damage, ItemOnHold.Projectiles, ItemOnHold.Type, null, null);
                                    break;
                            }

                            tempItem = EquippedRight;
                            EquippedRight = ItemOnHold;
                            EquippedRight.Position = new Vector2(633, 72);
                            ItemOnHold = tempItem;
                            TestPhase = !TestPhase;
                            HoldOnCounter = Convert.ToInt16(TestPhase) * 500;
                            LaserSound_1.Stop();
                            LaserSound_2.Stop();
                            LaserSound_3.Stop();
                        }
                    }

                    if (keyState.IsKeyUp(Keys.Q))
                    {
                        QPressed = false;
                    }
                    if (keyState.IsKeyUp(Keys.W))
                    {
                        WPressed = false;
                    }

                    if (keyState.IsKeyDown(Keys.Tab) & !TabPressed)
                    {
                        InvOpen = true;
                        TabPressed = true;
                    }
                    if (keyState.IsKeyUp(Keys.Tab))
                    {
                        TabPressed = false;
                    }

                    if (keyState.IsKeyDown(Keys.Escape) & !EscPressed)
                    {
                        Paused = true;
                        EscPressed = true;
                        ButtonList.Find(B => B.Text == "Resume").Visibility = 1f;
                        ButtonList.Find(B => B.Text == "Menu").Visibility = 1f;
                        ButtonList.Find(B => B.Text == "End").Visibility = 1f;
                    }
                    if (keyState.IsKeyUp(Keys.Escape))
                    {
                        EscPressed = false;
                    }

                    //::::::::::::::::::::::::::::::::::::::::::::Attacking::::::::::::::::::::::::::::::::::::::::::::
                    if (keyState.IsKeyDown(Keys.E))
                    {
                        foreach (Weapon Weapon in Player.Weapons)
                        {
                            if (Weapon.ShotTime >= Weapon.FireRate)
                            {
                                Weapon.ShotTime = 0;
                                int Rest;
                                Math.DivRem(Weapon.Projectiles, 2, out Rest);
                                switch (Weapon.Type)
                                {
                                    case 1:
                                        if (SoundsOn)
                                        {
                                            if (Player.Health > 66)
                                                LPSound_1.Play();
                                            else if (Player.Health > 33)
                                                LPSound_2.Play();
                                            else
                                                LPSound_3.Play();
                                        }
                                        if (Rest == 1)
                                        {
                                            for (int i = -(int)Math.Floor((double)Weapon.Projectiles / 2); i <= (int)Math.Floor((double)Weapon.Projectiles / 2); i++)
                                            {
                                                Attacks.Add(new LaserProjectile(new Vector2(Weapon.Position.X - Weapon.AttackSprite.Width / 2, Weapon.Position.Y), Weapon.Rotation + MathHelper.ToRadians(i * 10) - MathHelper.ToRadians(90), Weapon.Damage, Weapon.ProjectileSpeed, Weapon.AttackSprite, Weapon.BWAttackSprite));
                                            }
                                        }
                                        else
                                        {
                                            for (int i = -Weapon.Projectiles / 2; i <= Weapon.Projectiles / 2; i++)
                                            {
                                                if (i != 0)
                                                {
                                                    Attacks.Add(new LaserProjectile(new Vector2(Weapon.Position.X - Weapon.AttackSprite.Width / 2, Weapon.Position.Y), Weapon.Rotation + MathHelper.ToRadians((i * i * i) / MathHelper.Clamp(Weapon.Projectiles / 3, 1, 10)) - MathHelper.ToRadians(90), Weapon.Damage, Weapon.ProjectileSpeed, Weapon.AttackSprite, Weapon.BWAttackSprite));
                                                }
                                            }
                                        }
                                        break;
                                    case 2:
                                        if (SoundsOn)
                                        {
                                            if (Player.Health > 66)
                                                RocketSound_1.Play();
                                            else if (Player.Health > 33)
                                                RocketSound_2.Play();
                                            else
                                                RocketSound_3.Play();
                                        }
                                        if (Rest == 1)
                                        {
                                            for (int i = -(int)Math.Floor((double)Weapon.Projectiles / 2); i <= (int)Math.Floor((double)Weapon.Projectiles / 2); i++)
                                            {
                                                Attacks.Add(new HomingProjectile(new Vector2(Weapon.Position.X - Weapon.AttackSprite.Width / 2, Weapon.Position.Y), Weapon.Rotation + MathHelper.ToRadians(i * 10) - MathHelper.ToRadians(90), Enemies, Boss, Weapon.Damage, Weapon.ProjectileSpeed, Weapon.AttackSprite, Weapon.BWAttackSprite));
                                            }
                                        }
                                        else
                                        {
                                            for (int i = -Weapon.Projectiles / 2; i <= Weapon.Projectiles / 2; i++)
                                            {
                                                if (i != 0)
                                                {
                                                    Attacks.Add(new HomingProjectile(new Vector2(Weapon.Position.X - Weapon.AttackSprite.Width / 2, Weapon.Position.Y), Weapon.Rotation + MathHelper.ToRadians((i * i * i) / MathHelper.Clamp(Weapon.Projectiles / 3, 1, 10)) - MathHelper.ToRadians(90), Enemies, Boss, Weapon.Damage, Weapon.ProjectileSpeed, Weapon.AttackSprite, Weapon.BWAttackSprite));
                                                }
                                            }
                                        }
                                        break;
                                    case 3:
                                        if (SoundsOn)
                                        {
                                            if (Player.Health > 66)
                                            {
                                                LaserSound_1.Play();
                                                LaserSound_2.Stop();
                                                LaserSound_3.Stop();
                                            }
                                            else if (Player.Health > 33)
                                            {
                                                LaserSound_1.Stop();
                                                LaserSound_2.Play();
                                                LaserSound_3.Stop();
                                            }
                                            else
                                            {
                                                LaserSound_1.Stop();
                                                LaserSound_2.Stop();
                                                LaserSound_3.Play();
                                            }
                                        }
                                        if (Rest == 1)
                                        {
                                            for (int i = -(int)Math.Floor((double)Weapon.Projectiles / 2); i <= (int)Math.Floor((double)Weapon.Projectiles / 2); i++)
                                            {
                                                Attacks.Add(new Laser(new Vector2(Weapon.Position.X - Weapon.AttackSprite.Width / 2, Weapon.Position.Y), Weapon.Rotation + MathHelper.ToRadians(i * 10) - MathHelper.ToRadians(90), Weapon, Weapon.Damage, Weapon.AttackSprite, Weapon.BWAttackSprite));
                                            }
                                        }
                                        else
                                        {
                                            for (int i = -Weapon.Projectiles / 2; i <= Weapon.Projectiles / 2; i++)
                                            {
                                                if (i != 0)
                                                {
                                                    Attacks.Add(new Laser(new Vector2(Weapon.Position.X - Weapon.AttackSprite.Width / 2, Weapon.Position.Y), Weapon.Rotation + MathHelper.ToRadians((i * i * i) / MathHelper.Clamp(Weapon.Projectiles / 3, 1, 10)) - MathHelper.ToRadians(90), Weapon, Weapon.Damage, Weapon.AttackSprite, Weapon.BWAttackSprite));
                                                }
                                            }
                                        }
                                        break;
                                    case 4:
                                        if (SoundsOn)
                                        {
                                            if (Player.Health > 66)
                                                HVSound_1.Play();
                                            else if (Player.Health > 33)
                                                HVSound_2.Play();
                                            else
                                                HVSound_3.Play();
                                        }
                                        for (int i = 1; i <= Weapon.Projectiles; i++)
                                        {
                                            Attacks.Add(new DelayedHomingProjectile(new Vector2(Weapon.Position.X - Weapon.AttackSprite.Width / 2, Weapon.Position.Y), Weapon.Rotation + MathHelper.ToRadians(rand.Next(-140, -40)), Enemies, Boss, Weapon.Damage, Weapon.ProjectileSpeed, rand.Next(10, 20), Weapon.AttackSprite, Weapon.BWAttackSprite));
                                        }
                                        break;
                                    case 5:
                                        if (SoundsOn)
                                        {
                                            if (Player.Health > 66)
                                                LightningSound_1.Play();
                                            else if (Player.Health > 33)
                                                LightningSound_2.Play();
                                            else
                                                LightningSound_3.Play();
                                        }
                                        if (Rest == 1)
                                        {
                                            for (int i = -(int)Math.Floor((double)Weapon.Projectiles / 2); i <= (int)Math.Floor((double)Weapon.Projectiles / 2); i++)
                                            {
                                                Attacks.Add(new Lightning(Weapon.Position, Weapon.Rotation + MathHelper.ToRadians(i * 10) - MathHelper.ToRadians(90), Weapon.Damage, Weapon.ProjectileSpeed, rand.Next(50)));
                                            }
                                        }
                                        else
                                        {
                                            for (int i = -Weapon.Projectiles / 2; i <= Weapon.Projectiles / 2; i++)
                                            {
                                                if (i != 0)
                                                {
                                                    Attacks.Add(new Lightning(Weapon.Position, Weapon.Rotation + MathHelper.ToRadians((i * i * i) / MathHelper.Clamp(Weapon.Projectiles / 3, 1, 10)) - MathHelper.ToRadians(90), Weapon.Damage, Weapon.ProjectileSpeed, rand.Next(50)));
                                                }
                                            }
                                        }
                                        break;
                                    case 6:
                                        if (SoundsOn)
                                        {
                                            if (Player.Health > 66)
                                                SWSound_1.Play();
                                            else if (Player.Health > 33)
                                                SWSound_2.Play();
                                            else
                                                SWSound_3.Play();
                                        }
                                        Attacks.Add(new Shockwave(new Vector2(Weapon.Position.X, Weapon.Position.Y), Weapon.Rotation - MathHelper.ToRadians(90), Weapon.Damage, Weapon.ProjectileSpeed, Weapon.Projectiles));
                                        break;
                                }
                            }
                        }
                    }
                    else
                    {
                        LaserSound_1.Pause();
                        LaserSound_2.Pause();
                        LaserSound_3.Pause();
                    }


                    //::::::::::::::::::::::::::::::::::::::::::::Death::::::::::::::::::::::::::::::::::::::::::::
                    if (Player.Health <= 0)
                    {
                        LaserSound_1.Pause();
                        LaserSound_2.Pause();
                        LaserSound_3.Pause();

                        for (int i = 0; i <= 2000; i++)
                        {
                            Effects.Add(new Classes.Effect(new Vector2(Player.Position.X + Player.Sprite33.Width / 2, Player.Position.Y + Player.Sprite33.Height), MathHelper.ToRadians(rand.Next(360)), rand.Next(1, 20), rand.Next(10, 50), Effect_1, Effect_2, Effect_3, Effect_BW_1, Effect_BW_2, Effect_BW_3));
                        }
                        Player = null;


                        //............................................Saving............................................
                        switch (Level)
                        {
                            case 1:
                                HighScoresL1.Add(new HighScore(Name, Score));
                                HighScoresL1 = HighScoresL1.OrderByDescending(H => H.Score).ToList();
                                if (HighScoresL1.Count > 8)
                                    HighScoresL1.RemoveAt(HighScoresL1.Count - 1);
                                break;
                            case 2:
                                HighScoresL2.Add(new HighScore(Name, Score));
                                HighScoresL2 = HighScoresL2.OrderByDescending(H => H.Score).ToList();
                                if (HighScoresL2.Count > 8)
                                    HighScoresL2.RemoveAt(HighScoresL2.Count - 1);
                                break;
                            case 3:
                                HighScoresL3.Add(new HighScore(Name, Score));
                                HighScoresL3 = HighScoresL3.OrderByDescending(H => H.Score).ToList();
                                if (HighScoresL3.Count > 8)
                                    HighScoresL3.RemoveAt(HighScoresL3.Count - 1);
                                break;
                            case 4:
                                HighScoresL4.Add(new HighScore(Name, Score));
                                HighScoresL4 = HighScoresL4.OrderByDescending(H => H.Score).ToList();
                                if (HighScoresL4.Count > 8)
                                    HighScoresL4.RemoveAt(HighScoresL4.Count - 1);
                                break;
                            case 5:
                                HighScoresL5.Add(new HighScore(Name, Score));
                                HighScoresL5 = HighScoresL5.OrderByDescending(H => H.Score).ToList();
                                if (HighScoresL5.Count > 8)
                                    HighScoresL5.RemoveAt(HighScoresL5.Count - 1);
                                break;
                        }
                        ProgressList.Find(P => P.Name == Name).UnlockedLevel = LevelUnlocked;

                        IAsyncResult result = device.BeginOpenContainer("Storage", null, null);

                        result.AsyncWaitHandle.WaitOne();

                        StorageContainer container = device.EndOpenContainer(result);

                        result.AsyncWaitHandle.Close();

                        string filename = "savegame.sav";

                        if (container.FileExists(filename))
                            container.DeleteFile(filename);

                        Stream stream = container.CreateFile(filename);

                        XmlSerializer serializer = new XmlSerializer(typeof(SaveGame));

                        SaveGame data = new SaveGame(HighScoresL1, HighScoresL2, HighScoresL3, HighScoresL4, HighScoresL5, Name, ProgressList);
                        serializer.Serialize(stream, data);

                        stream.Close();

                        container.Dispose();



                        GameOver = true;
                        ButtonList.Find(B => B.Text == "Return to Menu").Visibility = 1f;
                        ButtonList.Find(B => B.Text == "Retry").Visibility = 1f;
                    }
                }



                //--------------------------------------------Updating Boss--------------------------------------------
                if (Boss != null)
                {
                    //::::::::::::::::::::::::::::::::::::::::::::Death::::::::::::::::::::::::::::::::::::::::::::
                    if (Boss.Health <= 0 & Boss.Spawned)
                    {
                        if (SoundsOn)
                            BigExp_1.Play();
                        if (LevelUnlocked < 5)
                            LevelUnlocked++;
                        Score += (int)(Boss.MaxHealth * multiplier);
                        for (int i = 0; i <= 10000 / (1 + Convert.ToInt16(LowParticles)); i++)
                        {
                            Effects.Add(new Classes.Effect(new Vector2(Boss.Position.X + rand.Next(0, Boss.Sprite33.Width), Boss.Position.Y + rand.Next(0, Boss.Sprite33.Height)), MathHelper.ToRadians(rand.Next(360)), rand.Next(1, 20), rand.Next(10, 50), Effect_1, Effect_2, Effect_3, Effect_BW_1, Effect_BW_2, Effect_BW_3));
                        }
                        Attacks.RemoveAll(A => A is HomingProjectile);


                        //............................................Item drops............................................
                        for (int i = 0; i <= rand.Next(4, 8); i++)
                        {
                            //Uniques
                            if (rand.Next(1, 101) <= 5)
                            {
                                switch (rand.Next(1, 12))
                                {
                                    case 1:
                                        Items.Add(new WeaponPickup("Bullet Stream", new Vector2(Boss.Position.X + rand.Next(0, Boss.Sprite100.Width), Boss.Position.Y + rand.Next(0, Boss.Sprite100.Height)), UPickup_LP, UPickup_LP_BW, 1, 25, rand.Next(20, 31), rand.Next(18, 23), 1));
                                        break;
                                    case 2:
                                        Items.Add(new WeaponPickup("Brutal Rocket", new Vector2(Boss.Position.X + rand.Next(0, Boss.Sprite100.Width), Boss.Position.Y + rand.Next(0, Boss.Sprite100.Height)), UPickup_R, UPickup_R_BW, 2, 500, rand.Next(400, 501), rand.Next(7, 14), 1));
                                        break;
                                    case 3:
                                        Items.Add(new WeaponPickup("Unstable Laser", new Vector2(Boss.Position.X + rand.Next(0, Boss.Sprite100.Width), Boss.Position.Y + rand.Next(0, Boss.Sprite100.Height)), UPickup_L, UPickup_L_BW, 3, rand.Next(45, 56), rand.Next(30, 36), 20, 1));
                                        break;
                                    case 4:
                                        Items.Add(new WeaponPickup("Homing Cloud", new Vector2(Boss.Position.X + rand.Next(0, Boss.Sprite100.Width), Boss.Position.Y + rand.Next(0, Boss.Sprite100.Height)), UPickup_HV, UPickup_HV_BW, 4, 100, 1, rand.Next(15, 26), rand.Next(90, 101)));
                                        break;
                                    case 5:
                                        Items.Add(new WeaponPickup("The Storm", new Vector2(Boss.Position.X + rand.Next(0, Boss.Sprite100.Width), Boss.Position.Y + rand.Next(0, Boss.Sprite100.Height)), UPickup_Li, UPickup_Li_BW, 5, rand.Next(100, 151), rand.Next(2, 5), 3, 29));
                                        break;
                                    case 6:
                                        Items.Add(new WeaponPickup("The Flood", new Vector2(Boss.Position.X + rand.Next(0, Boss.Sprite100.Width), Boss.Position.Y + rand.Next(0, Boss.Sprite100.Height)), UPickup_S, UPickup_S_BW, 6, rand.Next(180, 221), rand.Next(1, 3), rand.Next(15, 21), rand.Next(4, 7)));
                                        break;
                                    case 7:
                                        Items.Add(new WeaponPickup("Laser Cannon", new Vector2(Boss.Position.X + rand.Next(0, Boss.Sprite100.Width)), UPickup_LP, UPickup_LP_BW, 1, rand.Next(400, 601), rand.Next(800, 1001), 60, 1));
                                        break;
                                    case 8:
                                        Items.Add(new WeaponPickup("Quite A Large Amount Of Homing Missiles", new Vector2(Boss.Position.X + rand.Next(0, Boss.Sprite100.Width)), UPickup_R, UPickup_R_BW, 2, rand.Next(50, 101), rand.Next(3, 7), rand.Next(15, 26), 19));
                                        break;
                                    case 9:
                                        Items.Add(new WeaponPickup("Homing Projectile Of Mass Destruction", new Vector2(Boss.Position.X + rand.Next(0, Boss.Sprite100.Width)), UPickup_HV, UPickup_HV_BW, 4, rand.Next(400, 601), rand.Next(700, 901), rand.Next(15, 26), 1));
                                        break;
                                    case 10:
                                        Items.Add(new WeaponPickup("Close-Up Protection", new Vector2(Boss.Position.X + rand.Next(0, Boss.Sprite100.Width)), UPickup_Li, UPickup_Li_BW, 5, rand.Next(50, 101), rand.Next(30, 51), 1, 10));
                                        break;
                                    case 11:
                                        Items.Add(new WeaponPickup("Devastating Wave", new Vector2(Boss.Position.X + rand.Next(0, Boss.Sprite100.Width)), UPickup_S, UPickup_S_BW, 6, 5000, rand.Next(20, 31), rand.Next(5, 11), 10));
                                        break;
                                }
                            }

                            //Normal Items
                            else
                            {
                                Items.Add(new WeaponPickup(new Vector2(Boss.Position.X + rand.Next(0, Boss.Sprite100.Width), Boss.Position.Y + rand.Next(0, Boss.Sprite100.Height)), Pickup_LP, Pickup_LP_BW, rand.Next()));
                                switch ((Items.ElementAt<Item>(Items.Count - 1) as WeaponPickup).Type)
                                {
                                    case 1:
                                        Items.ElementAt<Item>(Items.Count - 1).Sprite = Pickup_LP;
                                        Items.ElementAt<Item>(Items.Count - 1).BWSprite = Pickup_LP_BW;
                                        break;
                                    case 2:
                                        Items.ElementAt<Item>(Items.Count - 1).Sprite = Pickup_R;
                                        Items.ElementAt<Item>(Items.Count - 1).BWSprite = Pickup_R_BW;
                                        break;
                                    case 3:
                                        Items.ElementAt<Item>(Items.Count - 1).Sprite = Pickup_L;
                                        Items.ElementAt<Item>(Items.Count - 1).BWSprite = Pickup_L_BW;
                                        break;
                                    case 4:
                                        Items.ElementAt<Item>(Items.Count - 1).Sprite = Pickup_HV;
                                        Items.ElementAt<Item>(Items.Count - 1).BWSprite = Pickup_HV_BW;
                                        break;
                                    case 5:
                                        Items.ElementAt<Item>(Items.Count - 1).Sprite = Pickup_Li;
                                        Items.ElementAt<Item>(Items.Count - 1).BWSprite = Pickup_Li_BW;
                                        break;
                                    case 6:
                                        Items.ElementAt<Item>(Items.Count - 1).Sprite = Pickup_S;
                                        Items.ElementAt<Item>(Items.Count - 1).BWSprite = Pickup_S_BW;
                                        break;
                                }
                            }
                        }
                        Boss = null;
                    }


                    else
                    {
                        if (Player != null && Boss.Spawned && (Boss.Position.X + Boss.Sprite100.Width > Player.Position.X + Player.Sprite100.Width / 2 & Boss.Position.X < Player.Position.X + Player.Sprite100.Width / 2 & Boss.Position.Y + Boss.Sprite100.Height > Player.Position.Y + Player.Sprite100.Height / 2 & Boss.Position.Y < Player.Position.Y + Player.Sprite100.Height / 2))
                        {
                            int health = Boss.Health;
                            Boss.Health -= Player.Health;
                            Player.Health -= health;
                        }

                        foreach (Weapon Weapon in Boss.Weapons)
                        {
                            Weapon.ShotTime += gameTime.ElapsedGameTime.Milliseconds;
                        }

                        Boss.Update(gameTime);



                        if (Boss.Spawned)
                        {
                            //::::::::::::::::::::::::::::::::::::::::::::Attacking::::::::::::::::::::::::::::::::::::::::::::
                            foreach (Weapon Weapon in Boss.Weapons)
                            {
                                if (Weapon.ShotTime >= Weapon.FireRate & Weapon.Activated)
                                {
                                    Weapon.ShotTime = 0;
                                    int Rest;
                                    Math.DivRem(Weapon.Projectiles, 2, out Rest);
                                    switch (Weapon.Type)
                                    {
                                        case 1:
                                            if (Rest == 1)
                                            {
                                                for (int i = -(int)Math.Floor((double)Weapon.Projectiles / 2); i <= (int)Math.Floor((double)Weapon.Projectiles / 2); i++)
                                                {
                                                    EnemyAttacks.Add(new LaserProjectile(Weapon.Position, Weapon.Rotation + MathHelper.ToRadians(i * 10) - MathHelper.ToRadians(90), Weapon.Damage, Weapon.ProjectileSpeed, Weapon.AttackSprite, Weapon.BWAttackSprite));
                                                }
                                            }
                                            else
                                            {
                                                for (int i = -Weapon.Projectiles / 2; i <= Weapon.Projectiles / 2; i++)
                                                {
                                                    if (i != 0)
                                                    {
                                                        EnemyAttacks.Add(new LaserProjectile(Weapon.Position, Weapon.Rotation + MathHelper.ToRadians((i * i * i) / MathHelper.Clamp(Weapon.Projectiles / 3, 1, 10)) - MathHelper.ToRadians(90), Weapon.Damage, Weapon.ProjectileSpeed, Weapon.AttackSprite, Weapon.BWAttackSprite));
                                                    }
                                                }
                                            }
                                            break;
                                        case 2:
                                            if (Rest == 1)
                                            {
                                                for (int i = -(int)Math.Floor((double)Weapon.Projectiles / 2); i <= (int)Math.Floor((double)Weapon.Projectiles / 2); i++)
                                                {
                                                    EnemyAttacks.Add(new HomingProjectile(Weapon.Position, Weapon.Rotation + MathHelper.ToRadians(i * 10) - MathHelper.ToRadians(90), Enemies, Boss, Weapon.Damage, Weapon.ProjectileSpeed, Weapon.AttackSprite, Weapon.BWAttackSprite));
                                                }
                                            }
                                            else
                                            {
                                                for (int i = -Weapon.Projectiles / 2; i <= Weapon.Projectiles / 2; i++)
                                                {
                                                    if (i != 0)
                                                    {
                                                        EnemyAttacks.Add(new HomingProjectile(Weapon.Position, Weapon.Rotation + MathHelper.ToRadians((i * i * i) / MathHelper.Clamp(Weapon.Projectiles / 3, 1, 10)) - MathHelper.ToRadians(90), Enemies, Boss, Weapon.Damage, Weapon.ProjectileSpeed, Weapon.AttackSprite, Weapon.BWAttackSprite));
                                                    }
                                                }
                                            }
                                            break;
                                        case 3:
                                            if (Rest == 1)
                                            {
                                                for (int i = -(int)Math.Floor((double)Weapon.Projectiles / 2); i <= (int)Math.Floor((double)Weapon.Projectiles / 2); i++)
                                                {
                                                    EnemyAttacks.Add(new Laser(Weapon.Position, Weapon.Rotation + MathHelper.ToRadians(i * 10) - MathHelper.ToRadians(90), Weapon, Weapon.Damage, Weapon.AttackSprite, Weapon.BWAttackSprite));
                                                }
                                            }
                                            else
                                            {
                                                for (int i = -Weapon.Projectiles / 2; i <= Weapon.Projectiles / 2; i++)
                                                {
                                                    if (i != 0)
                                                    {
                                                        EnemyAttacks.Add(new Laser(Weapon.Position, Weapon.Rotation + MathHelper.ToRadians((i * i * i) / MathHelper.Clamp(Weapon.Projectiles / 3, 1, 10)) - MathHelper.ToRadians(90), Weapon, Weapon.Damage, Weapon.AttackSprite, Weapon.BWAttackSprite));
                                                    }
                                                }
                                            }
                                            break;
                                        case 4:
                                            for (int i = 1; i <= Weapon.Projectiles; i++)
                                            {
                                                EnemyAttacks.Add(new DelayedHomingProjectile(Weapon.Position, Weapon.Rotation + MathHelper.ToRadians(rand.Next(-140, -40)), Player, Weapon.Damage, Weapon.ProjectileSpeed, rand.Next(10, 20), Weapon.AttackSprite, Weapon.BWAttackSprite));
                                            }
                                            break;
                                        case 5:
                                            if (Rest == 1)
                                            {
                                                for (int i = -(int)Math.Floor((double)Weapon.Projectiles / 2); i <= (int)Math.Floor((double)Weapon.Projectiles / 2); i++)
                                                {
                                                    EnemyAttacks.Add(new Lightning(Weapon.Position, Weapon.Rotation + MathHelper.ToRadians(i * 10) - MathHelper.ToRadians(90), Weapon.Damage, Weapon.ProjectileSpeed, rand.Next(50)));
                                                }
                                            }
                                            else
                                            {
                                                for (int i = -Weapon.Projectiles / 2; i <= Weapon.Projectiles / 2; i++)
                                                {
                                                    if (i != 0)
                                                    {
                                                        EnemyAttacks.Add(new Lightning(Weapon.Position, Weapon.Rotation + MathHelper.ToRadians((i * i * i) / MathHelper.Clamp(Weapon.Projectiles / 3, 1, 10)) - MathHelper.ToRadians(90), Weapon.Damage, Weapon.ProjectileSpeed, rand.Next(50)));
                                                    }
                                                }
                                            }
                                            break;
                                        case 6:
                                            EnemyAttacks.Add(new Shockwave(Weapon.Position, Weapon.Rotation - MathHelper.ToRadians(90), Weapon.Damage, Weapon.ProjectileSpeed, Weapon.Projectiles));
                                            break;
                                    }
                                }
                            }
                        }
                    }
                }


                //::::::::::::::::::::::::::::::::::::::::::::Level completion::::::::::::::::::::::::::::::::::::::::::::
                else if (Items.Count == 0)
                {
                    //............................................Saving............................................
                    switch (Level)
                    {
                        case 1:
                            HighScoresL1.Add(new HighScore(Name, Score));
                            HighScoresL1 = HighScoresL1.OrderByDescending(H => H.Score).ToList();
                            if (HighScoresL1.Count > 8)
                                HighScoresL1.RemoveAt(HighScoresL1.Count - 1);
                            break;
                        case 2:
                            HighScoresL2.Add(new HighScore(Name, Score));
                            HighScoresL2 = HighScoresL2.OrderByDescending(H => H.Score).ToList();
                            if (HighScoresL2.Count > 8)
                                HighScoresL2.RemoveAt(HighScoresL2.Count - 1);
                            break;
                        case 3:
                            HighScoresL3.Add(new HighScore(Name, Score));
                            HighScoresL3 = HighScoresL3.OrderByDescending(H => H.Score).ToList();
                            if (HighScoresL3.Count > 8)
                                HighScoresL3.RemoveAt(HighScoresL3.Count - 1);
                            break;
                        case 4:
                            HighScoresL4.Add(new HighScore(Name, Score));
                            HighScoresL4 = HighScoresL4.OrderByDescending(H => H.Score).ToList();
                            if (HighScoresL4.Count > 8)
                                HighScoresL4.RemoveAt(HighScoresL4.Count - 1);
                            break;
                        case 5:
                            HighScoresL5.Add(new HighScore(Name, Score));
                            HighScoresL5 = HighScoresL5.OrderByDescending(H => H.Score).ToList();
                            if (HighScoresL5.Count > 8)
                                HighScoresL5.RemoveAt(HighScoresL5.Count - 1);
                            break;
                    }
                    ProgressList.Find(P => P.Name == Name).UnlockedLevel = LevelUnlocked;

                    IAsyncResult result = device.BeginOpenContainer("Storage", null, null);

                    result.AsyncWaitHandle.WaitOne();

                    StorageContainer container = device.EndOpenContainer(result);

                    result.AsyncWaitHandle.Close();

                    string filename = "savegame.sav";

                    if (container.FileExists(filename))
                        container.DeleteFile(filename);

                    Stream stream = container.CreateFile(filename);

                    XmlSerializer serializer = new XmlSerializer(typeof(SaveGame));

                    SaveGame data = new SaveGame(HighScoresL1, HighScoresL2, HighScoresL3, HighScoresL4, HighScoresL5, Name, ProgressList);
                    serializer.Serialize(stream, data);

                    stream.Close();

                    container.Dispose();



                    GameOver = true;
                    ButtonList.Find(B => B.Text == "Proceed").Visibility = 1f;
                    ButtonList.Find(B => B.Text == "Return to Menu").Visibility = 1f;
                    ButtonList.Find(B => B.Text == "Retry").Visibility = 1f;
                }

                //--------------------------------------------updating player attacks--------------------------------------------
                for (int i = 0; i <= Attacks.Count - 1; i++)
                {
                    Attack Attack = Attacks.ElementAt<Attack>(i);
                    Attack.Move();
                    if ((!(Attack is DelayedHomingProjectile) || (Attack as DelayedHomingProjectile).speed > 0) & !(Attack is Lightning) & !(Attack is Shockwave))
                    {
                        Effects.Add(new Classes.Effect(new Vector2(Attack.Position.X + Attack.Sprite.Width / 2, Attack.Position.Y), MathHelper.ToRadians(rand.Next((int)MathHelper.ToDegrees(Attack.Rotation) - 20, (int)MathHelper.ToDegrees(Attack.Rotation) + 20) + 180), rand.Next(1, 5), rand.Next(5, 10), Effect_BW_1, Effect_BW_2, Effect_BW_3, Effect_BW_1, Effect_BW_2, Effect_BW_3));
                    }
                    if (Attack is Lightning)
                    {
                        for (int j = 0; j <= (Attack as Lightning).Dots.Count - 1; j++)
                        {
                            if ((Attack as Lightning).Dots.ElementAt<Dot>(j).Life < 0)
                            {
                                (Attack as Lightning).Dots.RemoveAt(j);
                            }
                        }
                        if ((Attack as Lightning).Dots.Count == 0)
                        {
                            Attacks.RemoveAt(i);
                        }
                    }
                    if (Attack is Shockwave)
                    {
                        for (int j = 0; j <= (Attack as Shockwave).Dots.Count - 1; j++)
                        {
                            if ((Attack as Shockwave).Dots.ElementAt<Dot>(j).Life < 0)
                            {
                                (Attack as Shockwave).Dots.RemoveAt(j);
                            }
                        }
                    }
                    if (!(Attack is Lightning) & !(Attack is Shockwave) && (Attack.Position.Y < -100 | Attack.Position.Y > 1000 | Attack.Position.X < -250 | Attack.Position.X > 1000))
                    {
                        Attacks.RemoveAt(i);
                    }
                    else if (Attack is Shockwave & Attack.Position.Y < -100)
                    {
                        Attacks.RemoveAt(i);
                    }
                    else
                    {
                        //::::::::::::::::::::::::::::::::::::::::::::Hitting the Boss::::::::::::::::::::::::::::::::::::::::::::
                        if (Boss != null && Boss.Spawned && Attack.HitTest(Boss.Position, new Vector2(Boss.Position.X + Boss.Sprite100.Width, Boss.Position.Y + Boss.Sprite100.Height - 50)))
                        {
                            string dmgString;

                            if (BeastModeTime > 0)
                            {
                                Boss.Health -= Attack.Damage * 2;
                                BeastModeTime = (int)MathHelper.Clamp(BeastModeTime + Attack.Damage, 1, 50);
                                dmgString = Convert.ToString(Attack.Damage * 2);
                            }
                            else
                            {
                                Boss.Health -= Attack.Damage;
                                dmgString = Convert.ToString(Attack.Damage);
                            }


                            //............................................Damage numbers............................................
                            if (DamageNumbers)
                            {
                                Vector2 Position;
                                if ((Attack is Lightning) | (Attack is Shockwave))
                                {
                                    Position = new Vector2(Boss.Position.X + Boss.Sprite100.Width / 2, Boss.Position.Y + Boss.Sprite100.Height / 2);
                                }
                                else
                                {
                                    Position = Attack.Position;
                                }

                                float Rotation = MathHelper.ToRadians(rand.Next(361));
                                int Speed = rand.Next(5, 7);
                                int Duration = rand.Next(10, 16);
                                if (BeastModeTime > 0)
                                {
                                    for (int k = 0; k <= dmgString.Count<Char>() - 1; k++)
                                    {
                                        switch (Convert.ToInt32(dmgString.Substring(k, 1)))
                                        {
                                            case 0:
                                                Effects.Add(new Classes.Effect(new Vector2(Position.X + k * 15, Position.Y), Rotation, Speed, Duration, null_BW_1, null_BW_2, null_BW_3, null_BW_1, null_BW_2, null_BW_3));
                                                break;
                                            case 1:
                                                Effects.Add(new Classes.Effect(new Vector2(Position.X + k * 15, Position.Y), Rotation, Speed, Duration, eins_BW_1, eins_BW_2, eins_BW_3, eins_BW_1, eins_BW_2, eins_BW_3));
                                                break;
                                            case 2:
                                                Effects.Add(new Classes.Effect(new Vector2(Position.X + k * 15, Position.Y), Rotation, Speed, Duration, zwei_BW_1, zwei_BW_2, zwei_BW_3, zwei_BW_1, zwei_BW_2, zwei_BW_3));
                                                break;
                                            case 3:
                                                Effects.Add(new Classes.Effect(new Vector2(Position.X + k * 15, Position.Y), Rotation, Speed, Duration, drei_BW_1, drei_BW_2, drei_BW_3, drei_BW_1, drei_BW_2, drei_BW_3));
                                                break;
                                            case 4:
                                                Effects.Add(new Classes.Effect(new Vector2(Position.X + k * 15, Position.Y), Rotation, Speed, Duration, vier_BW_1, vier_BW_2, vier_BW_3, vier_BW_1, vier_BW_2, vier_BW_3));
                                                break;
                                            case 5:
                                                Effects.Add(new Classes.Effect(new Vector2(Position.X + k * 15, Position.Y), Rotation, Speed, Duration, fünf_BW_1, fünf_BW_2, fünf_BW_3, fünf_BW_1, fünf_BW_2, fünf_BW_3));
                                                break;
                                            case 6:
                                                Effects.Add(new Classes.Effect(new Vector2(Position.X + k * 15, Position.Y), Rotation, Speed, Duration, sechs_BW_1, sechs_BW_2, sechs_BW_3, sechs_BW_1, sechs_BW_2, sechs_BW_3));
                                                break;
                                            case 7:
                                                Effects.Add(new Classes.Effect(new Vector2(Position.X + k * 15, Position.Y), Rotation, Speed, Duration, sieben_BW_1, sieben_BW_2, sieben_BW_3, sieben_BW_1, sieben_BW_2, sieben_BW_3));
                                                break;
                                            case 8:
                                                Effects.Add(new Classes.Effect(new Vector2(Position.X + k * 15, Position.Y), Rotation, Speed, Duration, acht_BW_1, acht_BW_2, acht_BW_3, acht_BW_1, acht_BW_2, acht_BW_3));
                                                break;
                                            case 9:
                                                Effects.Add(new Classes.Effect(new Vector2(Position.X + k * 15, Position.Y), Rotation, Speed, Duration, neun_BW_1, neun_BW_2, neun_BW_3, neun_BW_1, neun_BW_2, neun_BW_3));
                                                break;
                                        }
                                    }
                                }
                                else
                                {
                                    for (int k = 0; k <= dmgString.Count<Char>() - 1; k++)
                                    {
                                        switch (Convert.ToInt32(dmgString.Substring(k, 1)))
                                        {
                                            case 0:
                                                Effects.Add(new Classes.Effect(new Vector2(Position.X + k * 15, Position.Y), Rotation, Speed, Duration, null_1, null_2, null_3, null_1, null_2, null_3));
                                                break;
                                            case 1:
                                                Effects.Add(new Classes.Effect(new Vector2(Position.X + k * 15, Position.Y), Rotation, Speed, Duration, eins_1, eins_2, eins_3, eins_1, eins_2, eins_3));
                                                break;
                                            case 2:
                                                Effects.Add(new Classes.Effect(new Vector2(Position.X + k * 15, Position.Y), Rotation, Speed, Duration, zwei_1, zwei_2, zwei_3, zwei_1, zwei_2, zwei_3));
                                                break;
                                            case 3:
                                                Effects.Add(new Classes.Effect(new Vector2(Position.X + k * 15, Position.Y), Rotation, Speed, Duration, drei_1, drei_2, drei_3, drei_1, drei_2, drei_3));
                                                break;
                                            case 4:
                                                Effects.Add(new Classes.Effect(new Vector2(Position.X + k * 15, Position.Y), Rotation, Speed, Duration, vier_1, vier_2, vier_3, vier_1, vier_2, vier_3));
                                                break;
                                            case 5:
                                                Effects.Add(new Classes.Effect(new Vector2(Position.X + k * 15, Position.Y), Rotation, Speed, Duration, fünf_1, fünf_2, fünf_3, fünf_1, fünf_2, fünf_3));
                                                break;
                                            case 6:
                                                Effects.Add(new Classes.Effect(new Vector2(Position.X + k * 15, Position.Y), Rotation, Speed, Duration, sechs_1, sechs_2, sechs_3, sechs_1, sechs_2, sechs_3));
                                                break;
                                            case 7:
                                                Effects.Add(new Classes.Effect(new Vector2(Position.X + k * 15, Position.Y), Rotation, Speed, Duration, sieben_1, sieben_2, sieben_3, sieben_1, sieben_2, sieben_3));
                                                break;
                                            case 8:
                                                Effects.Add(new Classes.Effect(new Vector2(Position.X + k * 15, Position.Y), Rotation, Speed, Duration, acht_1, acht_2, acht_3, acht_1, acht_2, acht_3));
                                                break;
                                            case 9:
                                                Effects.Add(new Classes.Effect(new Vector2(Position.X + k * 15, Position.Y), Rotation, Speed, Duration, neun_1, neun_2, neun_3, neun_1, neun_2, neun_3));
                                                break;
                                        }
                                    }
                                }
                            }


                            if (!(Attack is Lightning) & !(Attack is Shockwave))
                            {
                                for (int j = 1; j <= Attack.Damage / 10; j++)
                                {
                                    Effects.Add(new Classes.Effect(Attack.Position, MathHelper.ToRadians(rand.Next((int)MathHelper.ToDegrees(Attack.Rotation) - 30, (int)MathHelper.ToDegrees(Attack.Rotation) + 30) + 180), rand.Next(1, 21), rand.Next(1 + Attack.Damage / 100, 10 + Attack.Damage / 100), Effect_1, Effect_2, Effect_3, Effect_BW_1, Effect_BW_2, Effect_BW_3));
                                }
                                Attacks.RemoveAt(i);
                            }
                        }



                        //::::::::::::::::::::::::::::::::::::::::::::Hitting enemies::::::::::::::::::::::::::::::::::::::::::::
                        foreach (Enemy Enemy in Enemies)
                        {
                            if (Attack.HitTest(Enemy.Position, new Vector2(Enemy.Position.X + Enemy.Sprite100.Width, Enemy.Position.Y + Enemy.Sprite100.Height)))
                            {
                                string dmgString;

                                if (BeastModeTime > 0)
                                {
                                    Enemy.Health -= Attack.Damage * 2;
                                    BeastModeTime = (int)MathHelper.Clamp(BeastModeTime + Attack.Damage, 1, 50);
                                    dmgString = Convert.ToString(Attack.Damage * 2);
                                }
                                else
                                {
                                    Enemy.Health -= Attack.Damage;
                                    dmgString = Convert.ToString(Attack.Damage);
                                }


                                //............................................Damage numbers............................................
                                if (DamageNumbers)
                                {
                                    Vector2 Position;
                                    if ((Attack is Lightning) | (Attack is Shockwave))
                                    {
                                        Position = new Vector2(Enemy.Position.X + Enemy.Sprite100.Width / 2, Enemy.Position.Y + Enemy.Sprite100.Height / 2);
                                    }
                                    else
                                    {
                                        Position = Attack.Position;
                                    }
                                    float Rotation = MathHelper.ToRadians(rand.Next(361));
                                    int Speed = rand.Next(5, 7);
                                    int Duration = rand.Next(10, 16);
                                    if (BeastModeTime > 0)
                                    {
                                        for (int k = 0; k <= dmgString.Count<Char>() - 1; k++)
                                        {
                                            switch (Convert.ToInt32(dmgString.Substring(k, 1)))
                                            {
                                                case 0:
                                                    Effects.Add(new Classes.Effect(new Vector2(Position.X + k * 15, Position.Y), Rotation, Speed, Duration, null_BW_1, null_BW_2, null_BW_3, null_BW_1, null_BW_2, null_BW_3));
                                                    break;
                                                case 1:
                                                    Effects.Add(new Classes.Effect(new Vector2(Position.X + k * 15, Position.Y), Rotation, Speed, Duration, eins_BW_1, eins_BW_2, eins_BW_3, eins_BW_1, eins_BW_2, eins_BW_3));
                                                    break;
                                                case 2:
                                                    Effects.Add(new Classes.Effect(new Vector2(Position.X + k * 15, Position.Y), Rotation, Speed, Duration, zwei_BW_1, zwei_BW_2, zwei_BW_3, zwei_BW_1, zwei_BW_2, zwei_BW_3));
                                                    break;
                                                case 3:
                                                    Effects.Add(new Classes.Effect(new Vector2(Position.X + k * 15, Position.Y), Rotation, Speed, Duration, drei_BW_1, drei_BW_2, drei_BW_3, drei_BW_1, drei_BW_2, drei_BW_3));
                                                    break;
                                                case 4:
                                                    Effects.Add(new Classes.Effect(new Vector2(Position.X + k * 15, Position.Y), Rotation, Speed, Duration, vier_BW_1, vier_BW_2, vier_BW_3, vier_BW_1, vier_BW_2, vier_BW_3));
                                                    break;
                                                case 5:
                                                    Effects.Add(new Classes.Effect(new Vector2(Position.X + k * 15, Position.Y), Rotation, Speed, Duration, fünf_BW_1, fünf_BW_2, fünf_BW_3, fünf_BW_1, fünf_BW_2, fünf_BW_3));
                                                    break;
                                                case 6:
                                                    Effects.Add(new Classes.Effect(new Vector2(Position.X + k * 15, Position.Y), Rotation, Speed, Duration, sechs_BW_1, sechs_BW_2, sechs_BW_3, sechs_BW_1, sechs_BW_2, sechs_BW_3));
                                                    break;
                                                case 7:
                                                    Effects.Add(new Classes.Effect(new Vector2(Position.X + k * 15, Position.Y), Rotation, Speed, Duration, sieben_BW_1, sieben_BW_2, sieben_BW_3, sieben_BW_1, sieben_BW_2, sieben_BW_3));
                                                    break;
                                                case 8:
                                                    Effects.Add(new Classes.Effect(new Vector2(Position.X + k * 15, Position.Y), Rotation, Speed, Duration, acht_BW_1, acht_BW_2, acht_BW_3, acht_BW_1, acht_BW_2, acht_BW_3));
                                                    break;
                                                case 9:
                                                    Effects.Add(new Classes.Effect(new Vector2(Position.X + k * 15, Position.Y), Rotation, Speed, Duration, neun_BW_1, neun_BW_2, neun_BW_3, neun_BW_1, neun_BW_2, neun_BW_3));
                                                    break;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        for (int k = 0; k <= dmgString.Count<Char>() - 1; k++)
                                        {
                                            switch (Convert.ToInt32(dmgString.Substring(k, 1)))
                                            {
                                                case 0:
                                                    Effects.Add(new Classes.Effect(new Vector2(Position.X + k * 15, Position.Y), Rotation, Speed, Duration, null_1, null_2, null_3, null_1, null_2, null_3));
                                                    break;
                                                case 1:
                                                    Effects.Add(new Classes.Effect(new Vector2(Position.X + k * 15, Position.Y), Rotation, Speed, Duration, eins_1, eins_2, eins_3, eins_1, eins_2, eins_3));
                                                    break;
                                                case 2:
                                                    Effects.Add(new Classes.Effect(new Vector2(Position.X + k * 15, Position.Y), Rotation, Speed, Duration, zwei_1, zwei_2, zwei_3, zwei_1, zwei_2, zwei_3));
                                                    break;
                                                case 3:
                                                    Effects.Add(new Classes.Effect(new Vector2(Position.X + k * 15, Position.Y), Rotation, Speed, Duration, drei_1, drei_2, drei_3, drei_1, drei_2, drei_3));
                                                    break;
                                                case 4:
                                                    Effects.Add(new Classes.Effect(new Vector2(Position.X + k * 15, Position.Y), Rotation, Speed, Duration, vier_1, vier_2, vier_3, vier_1, vier_2, vier_3));
                                                    break;
                                                case 5:
                                                    Effects.Add(new Classes.Effect(new Vector2(Position.X + k * 15, Position.Y), Rotation, Speed, Duration, fünf_1, fünf_2, fünf_3, fünf_1, fünf_2, fünf_3));
                                                    break;
                                                case 6:
                                                    Effects.Add(new Classes.Effect(new Vector2(Position.X + k * 15, Position.Y), Rotation, Speed, Duration, sechs_1, sechs_2, sechs_3, sechs_1, sechs_2, sechs_3));
                                                    break;
                                                case 7:
                                                    Effects.Add(new Classes.Effect(new Vector2(Position.X + k * 15, Position.Y), Rotation, Speed, Duration, sieben_1, sieben_2, sieben_3, sieben_1, sieben_2, sieben_3));
                                                    break;
                                                case 8:
                                                    Effects.Add(new Classes.Effect(new Vector2(Position.X + k * 15, Position.Y), Rotation, Speed, Duration, acht_1, acht_2, acht_3, acht_1, acht_2, acht_3));
                                                    break;
                                                case 9:
                                                    Effects.Add(new Classes.Effect(new Vector2(Position.X + k * 15, Position.Y), Rotation, Speed, Duration, neun_1, neun_2, neun_3, neun_1, neun_2, neun_3));
                                                    break;
                                            }
                                        }
                                    }
                                }


                                if (!(Attack is Lightning) & !(Attack is Shockwave))
                                {
                                    for (int j = 1; j <= Attack.Damage / 10; j++)
                                    {
                                        Effects.Add(new Classes.Effect(Attack.Position, MathHelper.ToRadians(rand.Next((int)MathHelper.ToDegrees(Attack.Rotation) - 30, (int)MathHelper.ToDegrees(Attack.Rotation) + 30) + 180), rand.Next(1, 21), rand.Next(1 + Attack.Damage / 100, 10 + Attack.Damage / 100), Effect_1, Effect_2, Effect_3, Effect_BW_1, Effect_BW_2, Effect_BW_3));
                                    }
                                    if (i < Attacks.Count)
                                    {
                                        Attacks.RemoveAt(i);
                                    }
                                }
                            }
                        }
                    }
                }



                //--------------------------------------------updating enemies--------------------------------------------
                for (int i = 0; i <= Enemies.Count - 1; i++)
                {
                    Enemy Enemy = Enemies.ElementAt<Enemy>(i);
                    //::::::::::::::::::::::::::::::::::::::::::::Life::::::::::::::::::::::::::::::::::::::::::::
                    if (Enemy.Health > 0)
                    {
                        Enemy.Move();
                        if (Enemy.ShotTime >= Enemy.AttackSpeed)
                        {
                            Enemy.ShotTime = 0;
                            EnemyAttacks.Add(new LaserProjectile(new Vector2(Enemy.Position.X + Enemy.Sprite100.Width / 2 - Enemy.AttackSprite.Width / 2, Enemy.Position.Y + Enemy.Sprite100.Height), MathHelper.ToRadians(90), (int)MathHelper.Clamp(Enemy.MaxHealth / 100, 1, 100), 10, Enemy.AttackSprite, Enemy.BWAttackSprite));
                        }
                        if ((Player != null) && (Enemy.Position.X + Enemy.Sprite100.Width > Player.Position.X + Player.Sprite100.Width / 2 & Enemy.Position.X < Player.Position.X + Player.Sprite100.Width / 2 & Enemy.Position.Y + Enemy.Sprite100.Height > Player.Position.Y + Player.Sprite100.Height / 2 & Enemy.Position.Y < Player.Position.Y + Player.Sprite100.Height / 2))
                        {
                            if (SoundsOn)
                                Hit.Play();
                            int health = Enemy.Health;
                            Enemy.Health -= Player.Health;
                            Player.Health -= health;
                            BeastModeTime = 0;
                            multiplier -= 0.01f * Enemy.MaxHealth;
                        }
                        if (Enemy.Position.Y > 910 | Enemy.Position.Y < -Enemy.Sprite100.Height - 20 | Enemy.Position.X < -Enemy.Sprite100.Width - 10 | Enemy.Position.X > 760)
                        {
                            Enemies.RemoveAt(i);
                        }
                    }
                    //::::::::::::::::::::::::::::::::::::::::::::Death::::::::::::::::::::::::::::::::::::::::::::
                    else
                    {
                        Score += (int)(Enemy.MaxHealth * multiplier);
                        multiplier += 0.001f * Enemy.MaxHealth;
                        //............................................Item Drops............................................
                        if (rand.Next(1, 1001) <= Enemy.DropChance)
                        {
                            //Uniques
                            if (rand.Next(1, 101) == 1)
                            {
                                switch (rand.Next(1, 12))
                                {
                                    case 1:
                                        Items.Add(new WeaponPickup("Bullet Stream", Enemy.Position, UPickup_LP, UPickup_LP_BW, 1, 25, rand.Next(20, 31), rand.Next(18, 23), 1));
                                        break;
                                    case 2:
                                        Items.Add(new WeaponPickup("Brutal Rocket", Enemy.Position, UPickup_R, UPickup_R_BW, 2, 500, rand.Next(400, 501), rand.Next(7, 14), 1));
                                        break;
                                    case 3:
                                        Items.Add(new WeaponPickup("Unstable Laser", Enemy.Position, UPickup_L, UPickup_L_BW, 3, rand.Next(45, 56), rand.Next(30, 36), 20, 1));
                                        break;
                                    case 4:
                                        Items.Add(new WeaponPickup("Homing Cloud", Enemy.Position, UPickup_HV, UPickup_HV_BW, 4, 100, 1, rand.Next(15, 26), rand.Next(90, 101)));
                                        break;
                                    case 5:
                                        Items.Add(new WeaponPickup("The Storm", Enemy.Position, UPickup_Li, UPickup_Li_BW, 5, rand.Next(100, 151), rand.Next(2, 5), 3, 29));
                                        break;
                                    case 6:
                                        Items.Add(new WeaponPickup("The Flood", Enemy.Position, UPickup_S, UPickup_S_BW, 6, rand.Next(180, 221), rand.Next(1, 3), rand.Next(15, 21), rand.Next(4, 7)));
                                        break;
                                    case 7:
                                        Items.Add(new WeaponPickup("Laser Cannon", Enemy.Position, UPickup_LP, UPickup_LP_BW, 1, rand.Next(400, 601), rand.Next(800, 1001), 60, 1));
                                        break;
                                    case 8:
                                        Items.Add(new WeaponPickup("Quite A Large Amount Of Homing Missiles", Enemy.Position, UPickup_R, UPickup_R_BW, 2, rand.Next(50, 101), rand.Next(3, 7), rand.Next(15, 26), 19));
                                        break;
                                    case 9:
                                        Items.Add(new WeaponPickup("Homing Projectile Of Mass Destruction", Enemy.Position, UPickup_HV, UPickup_HV_BW, 4, rand.Next(400, 601), rand.Next(700, 901), rand.Next(15, 26), 1));
                                        break;
                                    case 10:
                                        Items.Add(new WeaponPickup("Close-Up Protection", Enemy.Position, UPickup_Li, UPickup_Li_BW, 5, rand.Next(50, 101), rand.Next(30, 51), 1, 10));
                                        break;
                                    case 11:
                                        Items.Add(new WeaponPickup("Devastating Wave", Enemy.Position, UPickup_S, UPickup_S_BW, 6, 5000, rand.Next(20, 31), rand.Next(5, 11), 10));
                                        break;
                                }
                            }
                            //Normal Items
                            else
                            {
                                Items.Add(new WeaponPickup(Enemy.Position, Pickup_LP, Pickup_LP_BW, rand.Next()));
                                switch ((Items.ElementAt<Item>(Items.Count - 1) as WeaponPickup).Type)
                                {
                                    case 1:
                                        Items.ElementAt<Item>(Items.Count - 1).Sprite = Pickup_LP;
                                        Items.ElementAt<Item>(Items.Count - 1).BWSprite = Pickup_LP_BW;
                                        break;
                                    case 2:
                                        Items.ElementAt<Item>(Items.Count - 1).Sprite = Pickup_R;
                                        Items.ElementAt<Item>(Items.Count - 1).BWSprite = Pickup_R_BW;
                                        break;
                                    case 3:
                                        Items.ElementAt<Item>(Items.Count - 1).Sprite = Pickup_L;
                                        Items.ElementAt<Item>(Items.Count - 1).BWSprite = Pickup_L_BW;
                                        break;
                                    case 4:
                                        Items.ElementAt<Item>(Items.Count - 1).Sprite = Pickup_HV;
                                        Items.ElementAt<Item>(Items.Count - 1).BWSprite = Pickup_HV_BW;
                                        break;
                                    case 5:
                                        Items.ElementAt<Item>(Items.Count - 1).Sprite = Pickup_Li;
                                        Items.ElementAt<Item>(Items.Count - 1).BWSprite = Pickup_Li_BW;
                                        break;
                                    case 6:
                                        Items.ElementAt<Item>(Items.Count - 1).Sprite = Pickup_S;
                                        Items.ElementAt<Item>(Items.Count - 1).BWSprite = Pickup_S_BW;
                                        break;
                                }
                            }
                        }
                        for (int j = 1; j <= Enemy.MaxHealth / 4 / (1 + Convert.ToInt16(LowParticles)); j++)
                        {
                            Effects.Add(new Classes.Effect(new Vector2(Enemy.Position.X + rand.Next(0, Enemy.Sprite100.Width), Enemy.Position.Y + rand.Next(Enemy.Sprite100.Height)), MathHelper.ToRadians(rand.Next(360)), rand.Next(1, 10), rand.Next(40, 76), Effect_1, Effect_2, Effect_3, Effect_BW_1, Effect_BW_2, Effect_BW_3));
                            Effects.Add(new Classes.Effect(new Vector2(Enemy.Position.X + rand.Next(0, Enemy.Sprite100.Width), Enemy.Position.Y + rand.Next(Enemy.Sprite100.Height)), MathHelper.ToRadians(rand.Next(360)), rand.Next(1, 10), rand.Next(40, 76), Effect_1, Effect_2, Effect_3, Effect_BW_1, Effect_BW_2, Effect_BW_3));
                            Effects.Add(new Classes.Effect(new Vector2(Enemy.Position.X + rand.Next(0, Enemy.Sprite100.Width), Enemy.Position.Y + rand.Next(Enemy.Sprite100.Height)), MathHelper.ToRadians(rand.Next(360)), rand.Next(1, 10), rand.Next(40, 76), Effect_1, Effect_2, Effect_3, Effect_BW_1, Effect_BW_2, Effect_BW_3));
                            Effects.Add(new Classes.Effect(new Vector2(Enemy.Position.X + rand.Next(0, Enemy.Sprite100.Width), Enemy.Position.Y + rand.Next(Enemy.Sprite100.Height)), MathHelper.ToRadians(rand.Next(360)), rand.Next(1, 10), rand.Next(40, 76), Effect_1, Effect_2, Effect_3, Effect_BW_1, Effect_BW_2, Effect_BW_3));
                            Effects.Add(new Classes.Effect(new Vector2(Enemy.Position.X + Enemy.Sprite100.Width / 2 + (float)Math.Cos(MathHelper.ToRadians(rand.Next(-360, 360))) * rand.Next(1, Enemy.Sprite100.Width / 2), Enemy.Position.Y + Enemy.Sprite100.Height / 2 + (float)Math.Sin(MathHelper.ToRadians(rand.Next(-360, 360))) * rand.Next(1, Enemy.Sprite100.Height / 2)), 0f, 0, rand.Next(50, 101), Effect_1, Effect_2, Effect_3, Effect_BW_1, Effect_BW_2, Effect_BW_3));
                        }

                        //............................................Explosion sound............................................
                        if (SoundsOn & Player != null)
                        {
                            if (Player.Health > 66)
                            {
                                if (Enemy.MaxHealth >= 1000)
                                    BigExp_1.Play();
                                else if (Enemy.MaxHealth >= 250)
                                    MediumExp_1.Play();
                                else
                                    SmallExp_1.Play();
                            }
                            else if (Player.Health > 33)
                            {
                                if (Enemy.MaxHealth >= 1000)
                                    BigExp_2.Play();
                                else if (Enemy.MaxHealth >= 250)
                                    MediumExp_2.Play();
                                else
                                    SmallExp_2.Play();
                            }
                            else
                            {
                                if (Enemy.MaxHealth >= 1000)
                                    BigExp_3.Play();
                                else if (Enemy.MaxHealth >= 250)
                                    MediumExp_3.Play();
                                else
                                    SmallExp_3.Play();
                            }
                        }
                        Enemies.RemoveAt(i);
                    }
                }



                //--------------------------------------------Updating enemy Attacks--------------------------------------------
                for (int i = 0; i <= EnemyAttacks.Count - 1; i++)
                {
                    Attack Attack = EnemyAttacks.ElementAt<Attack>(i);
                    Attack.Move();
                    if ((!(Attack is DelayedHomingProjectile) || (Attack as DelayedHomingProjectile).speed > 0) & !(Attack is Lightning) & !(Attack is Shockwave))
                    {
                        Effects.Add(new Classes.Effect(new Vector2(Attack.Position.X + Attack.Sprite.Width / 2, Attack.Position.Y), MathHelper.ToRadians(rand.Next((int)MathHelper.ToDegrees(Attack.Rotation) - 20, (int)MathHelper.ToDegrees(Attack.Rotation) + 20) + 180), rand.Next(1, 5), rand.Next(5, 10), Effect_BW_1, Effect_BW_2, Effect_BW_3, Effect_BW_1, Effect_BW_2, Effect_BW_3));
                    }
                    if (Attack is Lightning)
                    {
                        for (int j = 0; j <= (Attack as Lightning).Dots.Count - 1; j++)
                        {
                            if ((Attack as Lightning).Dots.ElementAt<Dot>(j).Life < 0)
                            {
                                (Attack as Lightning).Dots.RemoveAt(j);
                            }
                        }
                        if ((Attack as Lightning).Dots.Count == 0)
                        {
                            EnemyAttacks.RemoveAt(i);
                        }
                    }
                    if (Attack is Shockwave)
                    {
                        for (int j = 0; j <= (Attack as Shockwave).Dots.Count - 1; j++)
                        {
                            if ((Attack as Shockwave).Dots.ElementAt<Dot>(j).Life < 0)
                            {
                                (Attack as Shockwave).Dots.RemoveAt(j);
                            }
                        }
                    }
                    if (!(Attack is Lightning) & !(Attack is Shockwave) && (Attack.Position.Y < -100 | Attack.Position.Y > 1000 | Attack.Position.X < -250 | Attack.Position.X > 1000))
                    {
                        EnemyAttacks.RemoveAt(i);
                    }
                    else if (Attack is Shockwave & Attack.Position.Y > 1000)
                    {
                        EnemyAttacks.RemoveAt(i);
                    }
                    else
                    {
                        if (Player != null && Attack.HitTest(new Vector2(Player.Position.X + Player.Sprite100.Width / 2 - 2, Player.Position.Y + Player.Sprite100.Height / 2 - 2), new Vector2(Player.Position.X + Player.Sprite100.Width / 2 + 2, Player.Position.Y + Player.Sprite100.Height / 2 + 2)))
                        {
                            if (SoundsOn)
                                Hit.Play();
                            Player.Health -= Attack.Damage;
                            if (BeastModeTime == 0)
                            {
                                multiplier -= 0.1f * Attack.Damage;
                            }
                            else
                            {
                                BeastModeTime = -1000;
                                multiplier -= 0.1f * Attack.Damage;
                            }

                            if (!(Attack is Lightning) & !(Attack is Shockwave))
                            {
                                for (int j = 1; j <= Attack.Damage; j++)
                                {
                                    Effects.Add(new Classes.Effect(Attack.Position, MathHelper.ToRadians(rand.Next((int)MathHelper.ToDegrees(Attack.Rotation) - 60, (int)MathHelper.ToDegrees(Attack.Rotation) + 60) + 180), rand.Next(1, 5), rand.Next(5 + Attack.Damage / 100, 10 + Attack.Damage / 100), Effect_1, Effect_2, Effect_3, Effect_BW_1, Effect_BW_2, Effect_BW_3));
                                }
                                EnemyAttacks.RemoveAt(i);
                            }
                        }
                    }
                }



                //--------------------------------------------updating effects--------------------------------------------
                for (int i = 0; i <= Effects.Count - 1; i++)
                {
                    if (Effects.ElementAt<Classes.Effect>(i).Life > 0)
                    {
                        Effects.ElementAt<Classes.Effect>(i).Move();
                    }
                    else
                    {
                        Effects.RemoveAt(i);
                    }
                }



                //--------------------------------------------updating items--------------------------------------------
                for (int i = 0; i <= Items.Count - 1; i++)
                {
                    Item Item = Items.ElementAt<Item>(i);
                    Item.Move();
                    if (!(Player == null) && Inventory.Count < 70 && Item.HitTest(Player.Position, new Vector2(Player.Position.X + Player.Sprite100.Width, Player.Position.Y + Player.Sprite100.Height)))
                    {
                        ItemOnHold = Item as WeaponPickup;
                        HoldOnCounter = 500;
                        TestPhase = false;
                        Inventory.Add(Item);
                        Items.RemoveAt(i);
                    }
                    if (Item.Position.Y > 900)
                    {
                        Items.RemoveAt(i);
                    }
                }
                if (HoldOnCounter > 0)
                {
                    HoldOnCounter--;
                }
                else
                {
                    ItemOnHold = null;
                    TestPhase = false;
                }
                if (!Inventory.Contains(ItemOnHold) | EquippedLeft == ItemOnHold | EquippedRight == ItemOnHold)
                {
                    ItemOnHold = null;
                    TestPhase = false;
                }



                //--------------------------------------------updating background--------------------------------------------
                if (BG1Position == 900)
                {
                    BG1Position = 0;
                }
                else
                {
                    BG1Position++;
                }
                if (BG2Position == 900)
                {
                    BG2Position = 0;
                }
                else
                {
                    BG2Position += 2;
                }



                if (FadeCounter < -100)
                    FadeCounter++;

                base.Update(gameTime);
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Gray);

            //-----------------------------Drawing background------------------------------------------------------
            spriteBatch.Begin();
            spriteBatch.Draw(Background1, new Vector2(-50, BG1Position), null, Color.White, 0f, Vector2.Zero, 1.2f, SpriteEffects.None, 1f);
            spriteBatch.Draw(Background1, new Vector2(-50, BG1Position - 900), null, Color.White, 0f, Vector2.Zero, 1.2f, SpriteEffects.None, 1f);
            spriteBatch.Draw(Background2, new Vector2(-50, BG2Position), null, Color.White, 0f, Vector2.Zero, 1.2f, SpriteEffects.None, 0f);
            spriteBatch.Draw(Background2, new Vector2(-50, BG2Position - 900), null, Color.White, 0f, Vector2.Zero, 1.2f, SpriteEffects.None, 0f);
            spriteBatch.End();



            //-----------------------------Drawing start screen------------------------------------------------------
            if (StartScreen)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(StartScreenPic, Vector2.Zero, Color.White);
                spriteBatch.End();

                spriteBatch.Begin();
                spriteBatch.Draw(Cursor_White, new Vector2(Mouse.GetState().X, Mouse.GetState().Y), Color.White);

                foreach (Classes.Effect Effect in Effects)
                {
                    int lifePerc = (int)(Effect.Life * (100 / Effect.Duration));
                    if (lifePerc > 66)
                    {
                        spriteBatch.Draw(Effect.Sprite100, Effect.Position, Color.White);
                    }
                    else if (lifePerc > 33)
                    {
                        spriteBatch.Draw(Effect.Sprite66, Effect.Position, Color.White);
                    }
                    else
                    {
                        spriteBatch.Draw(Effect.Sprite33, Effect.Position, Color.White);
                    }
                }
                spriteBatch.End();
            }


            //-----------------------------Drawing menu------------------------------------------------------
            else if (Menu)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(MenuPic, Vector2.Zero, Color.White);
                spriteBatch.End();

                spriteBatch.Begin();
                spriteBatch.Draw(Cursor_White, new Vector2(Mouse.GetState().X, Mouse.GetState().Y), Color.White);
                spriteBatch.DrawString(Font, "Name: " + Name, new Vector2(100, 50), Color.White);
                spriteBatch.DrawString(Font, "Highscores", new Vector2(300, 600), Color.White, 0f, Vector2.Zero, 1.5f, SpriteEffects.None, 0f);

                int number = 1;
                if (FadeCounter > 1600)
                {
                    spriteBatch.DrawString(Font, "Level 1", new Vector2(350, 625), Color.White * 0.8f);
                    foreach (HighScore HighScore in HighScoresL1)
                    {
                        string score = Convert.ToString(HighScore.Score);
                        for (int i = score.Length; i >= 1; i--)
                        {
                            int Rest;
                            Math.DivRem(score.Length - i, 4, out Rest);
                            if (Rest == 0)
                                score = score.Insert(i, " ");
                        }
                        spriteBatch.DrawString(Font, number + ".   " + HighScore.Name, new Vector2(240, 630 + number * 30), Color.White);
                        spriteBatch.DrawString(Font, score, new Vector2(440, 630 + number * 30), Color.White);
                        number++;
                    }
                }
                else if (FadeCounter > 1200)
                {
                    spriteBatch.DrawString(Font, "Level 2", new Vector2(350, 625), Color.White * 0.8f);
                    foreach (HighScore HighScore in HighScoresL2)
                    {
                        string score = Convert.ToString(HighScore.Score);
                        for (int i = score.Length; i >= 1; i--)
                        {
                            int Rest;
                            Math.DivRem(score.Length - i, 4, out Rest);
                            if (Rest == 0)
                                score = score.Insert(i, " ");
                        }
                        spriteBatch.DrawString(Font, number + ".   " + HighScore.Name, new Vector2(240, 630 + number * 30), Color.White);
                        spriteBatch.DrawString(Font, score, new Vector2(440, 630 + number * 30), Color.White);
                        number++;
                    }
                }
                else if (FadeCounter > 800)
                {
                    spriteBatch.DrawString(Font, "Level 3", new Vector2(350, 625), Color.White * 0.8f);
                    foreach (HighScore HighScore in HighScoresL3)
                    {
                        string score = Convert.ToString(HighScore.Score);
                        for (int i = score.Length; i >= 1; i--)
                        {
                            int Rest;
                            Math.DivRem(score.Length - i, 4, out Rest);
                            if (Rest == 0)
                                score = score.Insert(i, " ");
                        }
                        spriteBatch.DrawString(Font, number + ".   " + HighScore.Name, new Vector2(240, 630 + number * 30), Color.White);
                        spriteBatch.DrawString(Font, score, new Vector2(440, 630 + number * 30), Color.White);
                        number++;
                    }
                }
                else if (FadeCounter > 400)
                {
                    spriteBatch.DrawString(Font, "Level 4", new Vector2(350, 625), Color.White * 0.8f);
                    foreach (HighScore HighScore in HighScoresL4)
                    {
                        string score = Convert.ToString(HighScore.Score);
                        for (int i = score.Length; i >= 1; i--)
                        {
                            int Rest;
                            Math.DivRem(score.Length - i, 4, out Rest);
                            if (Rest == 0)
                                score = score.Insert(i, " ");
                        }
                        spriteBatch.DrawString(Font, number + ".   " + HighScore.Name, new Vector2(240, 630 + number * 30), Color.White);
                        spriteBatch.DrawString(Font, score, new Vector2(440, 630 + number * 30), Color.White);
                        number++;
                    }
                }
                else if (FadeCounter > 0)
                {
                    spriteBatch.DrawString(Font, "Level 5", new Vector2(350, 625), Color.White * 0.8f);
                    foreach (HighScore HighScore in HighScoresL5)
                    {
                        string score = Convert.ToString(HighScore.Score);
                        for (int i = score.Length; i >= 1; i--)
                        {
                            int Rest;
                            Math.DivRem(score.Length - i, 4, out Rest);
                            if (Rest == 0)
                                score = score.Insert(i, " ");
                        }
                        spriteBatch.DrawString(Font, number + ".   " + HighScore.Name, new Vector2(240, 630 + number * 30), Color.White);
                        spriteBatch.DrawString(Font, score, new Vector2(440, 630 + number * 30), Color.White);
                        number++;
                    }
                }

                foreach (Classes.Effect Effect in Effects)
                {
                    int lifePerc = (int)(Effect.Life * (100 / Effect.Duration));
                    if (lifePerc > 66)
                    {
                        spriteBatch.Draw(Effect.Sprite100, Effect.Position, Color.White);
                    }
                    else if (lifePerc > 33)
                    {
                        spriteBatch.Draw(Effect.Sprite66, Effect.Position, Color.White);
                    }
                    else
                    {
                        spriteBatch.Draw(Effect.Sprite33, Effect.Position, Color.White);
                    }
                }
                spriteBatch.End();
            }


            //-----------------------------Drawing main game------------------------------------------------------
            else
            {
                spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
                if (FadeCounter < -100)
                {
                    float value;
                    if (FadeCounter < -250)
                        value = (float)rand.Next(90, 101) / 100f;
                    else if (FadeCounter < -200)
                        value = (float)rand.Next(50, 76) / 100f;
                    else if (FadeCounter < -150)
                        value = (float)rand.Next(25, 51) / 100f;
                    else
                        value = (float)rand.Next(1, 26) / 100f;
                    spriteBatch.DrawString(Font, "Level " + Level, new Vector2(250, 200), Color.White * value, 0f, Vector2.Zero, 4f, SpriteEffects.None, 0f);
                }


                //::::::::::::::::::::::::::::::::Drawing player::::::::::::::::::::::::::::::::::::::::::
                if (Player != null)
                {
                    int healthPerc = (int)(Player.Health * ((double)100 / (double)Player.MaxHealth));
                    if (healthPerc > 66)
                    {
                        spriteBatch.Draw(Ship_BW_1, Player.Position, null, Color.White * (0.6f - 0.06f * multiplier), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.2f);
                        spriteBatch.Draw(Player.Sprite100, Player.Position, null, Color.White * (0.4f + 0.06f * multiplier), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.5f);
                        if (rand.Next(1, 101) == 1)
                        {
                            int X = rand.Next(-5, 6);
                            int Y = rand.Next(-5, 6);
                            spriteBatch.Draw(Ship_BW_1, new Vector2(Player.Position.X + X, Player.Position.Y + Y), null, Color.White * (0.6f - 0.06f * multiplier), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.2f);
                            spriteBatch.Draw(Player.Sprite100, new Vector2(Player.Position.X + X, Player.Position.Y + Y), null, Color.White * (0.4f + 0.06f * multiplier), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.5f);
                        }
                    }
                    else if (healthPerc > 33)
                    {
                        spriteBatch.Draw(Ship_BW_2, Player.Position, null, Color.White * (0.6f - 0.06f * multiplier), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.2f);
                        spriteBatch.Draw(Player.Sprite66, Player.Position, null, Color.White * (0.4f + 0.06f * multiplier), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.5f);
                        if (rand.Next(1, 101) <= 20)
                        {
                            int X = rand.Next(-10, 11);
                            int Y = rand.Next(-10, 11);
                            spriteBatch.Draw(Ship_BW_2, new Vector2(Player.Position.X + X, Player.Position.Y + Y), null, Color.White * (0.6f - 0.06f * multiplier), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.2f);
                            spriteBatch.Draw(Player.Sprite66, new Vector2(Player.Position.X + X, Player.Position.Y + Y), null, Color.White * (0.4f + 0.06f * multiplier), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.5f);
                        }
                    }
                    else
                    {
                        if (rand.Next(1, 101) <= 95)
                        {
                            spriteBatch.Draw(Ship_BW_3, Player.Position, null, Color.White * (0.6f - 0.06f * multiplier), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.2f);
                            spriteBatch.Draw(Player.Sprite33, Player.Position, null, Color.White * (0.4f + 0.06f * multiplier), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.5f);
                        }
                        if (rand.Next(1, 101) <= 40)
                        {
                            int X = rand.Next(-15, 16);
                            int Y = rand.Next(-15, 16);
                            spriteBatch.Draw(Ship_BW_3, new Vector2(Player.Position.X + X, Player.Position.Y + Y), null, Color.White * (0.6f - 0.06f * multiplier), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.2f);
                            spriteBatch.Draw(Player.Sprite33, new Vector2(Player.Position.X + X, Player.Position.Y + Y), null, Color.White * (0.4f + 0.06f * multiplier), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.5f);
                        }
                        if (rand.Next(1, 101) <= 40)
                        {
                            int X = rand.Next(-15, 16);
                            int Y = rand.Next(-15, 16);
                            spriteBatch.Draw(Ship_BW_3, new Vector2(Player.Position.X + X, Player.Position.Y + Y), null, Color.White * (0.6f - 0.06f * multiplier), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.2f);
                            spriteBatch.Draw(Player.Sprite33, new Vector2(Player.Position.X + X, Player.Position.Y + Y), null, Color.White * (0.4f + 0.06f * multiplier), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.5f);
                        }
                    }
                }


                //::::::::::::::::::::::::::::::::Drawing boss::::::::::::::::::::::::::::::::::::::::::
                if (Boss != null && Boss.Spawned)
                {
                    int healthPerc = (int)(Boss.Health * ((double)100 / (double)Boss.MaxHealth));
                    if (healthPerc > 66)
                    {
                        spriteBatch.Draw(Boss.BWSprite100, Boss.Position, null, Color.White * (0.6f - 0.06f * multiplier), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                        spriteBatch.Draw(Boss.Sprite100, Boss.Position, null, Color.White * (0.4f + 0.06f * multiplier), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.1f);
                        if (rand.Next(1, 101) == 1)
                        {
                            int X = rand.Next(-5, 6);
                            int Y = rand.Next(-5, 6);
                            spriteBatch.Draw(Boss.BWSprite100, new Vector2(Boss.Position.X + X, Boss.Position.Y + Y), null, Color.White * (0.6f - 0.06f * multiplier), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                            spriteBatch.Draw(Boss.Sprite100, new Vector2(Boss.Position.X + X, Boss.Position.Y + Y), null, Color.White * (0.4f + 0.06f * multiplier), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.1f);
                        }
                    }
                    else if (healthPerc > 33)
                    {
                        spriteBatch.Draw(Boss.BWSprite66, Boss.Position, null, Color.White * (0.6f - 0.06f * multiplier), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                        spriteBatch.Draw(Boss.Sprite66, Boss.Position, null, Color.White * (0.4f + 0.06f * multiplier), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.1f);
                        if (rand.Next(1, 101) <= 20)
                        {
                            int X = rand.Next(-10, 11);
                            int Y = rand.Next(-10, 11);
                            spriteBatch.Draw(Boss.BWSprite66, new Vector2(Boss.Position.X + X, Boss.Position.Y + Y), null, Color.White * (0.6f - 0.06f * multiplier), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                            spriteBatch.Draw(Boss.Sprite66, new Vector2(Boss.Position.X + X, Boss.Position.Y + Y), null, Color.White * (0.4f + 0.06f * multiplier), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.1f);
                        }
                    }
                    else
                    {
                        if (rand.Next(1, 101) <= 40)
                        {
                            int X = rand.Next(-25, 26);
                            int Y = rand.Next(-25, 26);
                            spriteBatch.Draw(Boss.BWSprite33, new Vector2(Boss.Position.X + X, Boss.Position.Y + Y), null, Color.White * (0.6f - 0.06f * multiplier), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                            spriteBatch.Draw(Boss.Sprite33, new Vector2(Boss.Position.X + X, Boss.Position.Y + Y), null, Color.White * (0.4f + 0.06f * multiplier), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.1f);
                        }
                        if (rand.Next(1, 101) <= 40)
                        {
                            int X = rand.Next(-25, 26);
                            int Y = rand.Next(-25, 26);
                            spriteBatch.Draw(Boss.BWSprite33, new Vector2(Boss.Position.X + X, Boss.Position.Y + Y), null, Color.White * (0.6f - 0.06f * multiplier), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                            spriteBatch.Draw(Boss.Sprite33, new Vector2(Boss.Position.X + X, Boss.Position.Y + Y), null, Color.White * (0.4f + 0.06f * multiplier), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.1f);
                        }
                        if (rand.Next(1, 101) <= 40)
                        {
                            int X = rand.Next(-25, 26);
                            int Y = rand.Next(-25, 26);
                            spriteBatch.Draw(Boss.BWSprite33, new Vector2(Boss.Position.X + X, Boss.Position.Y + Y), null, Color.White * (0.6f - 0.06f * multiplier), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                            spriteBatch.Draw(Boss.Sprite33, new Vector2(Boss.Position.X + X, Boss.Position.Y + Y), null, Color.White * (0.4f + 0.06f * multiplier), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.1f);
                        }
                    }
                }


                //::::::::::::::::::::::::::::::::Drawing enemies::::::::::::::::::::::::::::::::::::::::::
                foreach (Enemy Enemy in Enemies)
                {
                    int healthPerc = (int)(Enemy.Health * ((double)100 / (double)Enemy.MaxHealth));
                    if (healthPerc > 66)
                    {
                        spriteBatch.Draw(Enemy.BWSprite100, Enemy.Position, null, Color.White * (0.6f - 0.06f * multiplier), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.3f);
                        spriteBatch.Draw(Enemy.Sprite100, Enemy.Position, null, Color.White * (0.4f + 0.06f * multiplier), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.5f);
                        if (rand.Next(1, 101) == 1)
                        {
                            int X = rand.Next(-5, 6);
                            int Y = rand.Next(-5, 6);
                            spriteBatch.Draw(Enemy.BWSprite100, new Vector2(Enemy.Position.X + X, Enemy.Position.Y + Y), null, Color.White * (0.6f - 0.06f * multiplier), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.3f);
                            spriteBatch.Draw(Enemy.Sprite100, new Vector2(Enemy.Position.X + X, Enemy.Position.Y + Y), null, Color.White * (0.4f + 0.06f * multiplier), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.5f);
                        }
                    }
                    else if (healthPerc > 33)
                    {
                        spriteBatch.Draw(Enemy.BWSprite66, Enemy.Position, null, Color.White * (0.6f - 0.06f * multiplier), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.3f);
                        spriteBatch.Draw(Enemy.Sprite66, Enemy.Position, null, Color.White * (0.4f + 0.06f * multiplier), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.5f);
                        if (rand.Next(1, 101) <= 10)
                        {
                            int X = rand.Next(-10, 11);
                            int Y = rand.Next(-10, 11);
                            spriteBatch.Draw(Enemy.BWSprite66, new Vector2(Enemy.Position.X + X, Enemy.Position.Y + Y), null, Color.White * (0.6f - 0.06f * multiplier), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.3f);
                            spriteBatch.Draw(Enemy.Sprite66, new Vector2(Enemy.Position.X + X, Enemy.Position.Y + Y), null, Color.White * (0.4f + 0.06f * multiplier), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.5f);
                        }
                    }
                    else
                    {
                        spriteBatch.Draw(Enemy.BWSprite33, Enemy.Position, null, Color.White * (0.6f - 0.06f * multiplier), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.3f);
                        spriteBatch.Draw(Enemy.Sprite33, Enemy.Position, null, Color.White * (0.4f + 0.06f * multiplier), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.5f);
                        if (rand.Next(1, 101) <= 25)
                        {
                            int X = rand.Next(-15, 16);
                            int Y = rand.Next(-15, 16);
                            spriteBatch.Draw(Enemy.BWSprite33, new Vector2(Enemy.Position.X + X, Enemy.Position.Y + Y), null, Color.White * (0.6f - 0.06f * multiplier), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.3f);
                            spriteBatch.Draw(Enemy.Sprite33, new Vector2(Enemy.Position.X + X, Enemy.Position.Y + Y), null, Color.White * (0.4f + 0.06f * multiplier), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.5f);
                        }
                        if (rand.Next(1, 101) <= 25)
                        {
                            int X = rand.Next(-15, 16);
                            int Y = rand.Next(-15, 16);
                            spriteBatch.Draw(Enemy.BWSprite33, new Vector2(Enemy.Position.X + X, Enemy.Position.Y + Y), null, Color.White * (0.6f - 0.06f * multiplier), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.3f);
                            spriteBatch.Draw(Enemy.Sprite33, new Vector2(Enemy.Position.X + X, Enemy.Position.Y + Y), null, Color.White * (0.4f + 0.06f * multiplier), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.5f);
                        }
                        if (rand.Next(1, 101) <= 25)
                        {
                            int X = rand.Next(-15, 16);
                            int Y = rand.Next(-15, 16);
                            spriteBatch.Draw(Enemy.BWSprite33, new Vector2(Enemy.Position.X + X, Enemy.Position.Y + Y), null, Color.White * (0.6f - 0.06f * multiplier), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.3f);
                            spriteBatch.Draw(Enemy.Sprite33, new Vector2(Enemy.Position.X + X, Enemy.Position.Y + Y), null, Color.White * (0.4f + 0.06f * multiplier), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.5f);
                        }
                    }
                }


                //::::::::::::::::::::::::::::::::Drawing attacks::::::::::::::::::::::::::::::::::::::::::
                foreach (Attack Attack in Attacks)
                {
                    if (Attack is Lightning)
                    {
                        foreach (Dot Dot in (Attack as Lightning).Dots)
                        {
                            if (Dot.Life >= 2)
                            {
                                spriteBatch.Draw(Effect_B_1, Dot.Position, null, Color.White * 0.025f * multiplier, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.1f);
                                spriteBatch.Draw(Effect_BW_1, Dot.Position, null, Color.White * (0.25f - 0.025f * multiplier), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                            }
                            else if (Dot.Life == 1)
                            {
                                spriteBatch.Draw(Effect_B_2, Dot.Position, null, Color.White * 0.025f * multiplier, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.1f);
                                spriteBatch.Draw(Effect_BW_2, Dot.Position, null, Color.White * (0.25f - 0.025f * multiplier), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                            }
                            else
                            {
                                spriteBatch.Draw(Effect_B_3, Dot.Position, null, Color.White * 0.025f * multiplier, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.1f);
                                spriteBatch.Draw(Effect_BW_3, Dot.Position, null, Color.White * (0.25f - 0.025f * multiplier), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                            }
                        }
                    }
                    else if (Attack is Shockwave)
                    {
                        foreach (Dot Dot in (Attack as Shockwave).Dots)
                        {
                            if (Dot.Life >= 2)
                            {
                                spriteBatch.Draw(Effect_LB_1, Dot.Position, null, Color.White * 0.05f * multiplier, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.1f);
                                spriteBatch.Draw(Effect_BW_1, Dot.Position, null, Color.White * (0.5f - 0.05f * multiplier), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                            }
                            else if (Dot.Life == 1)
                            {
                                spriteBatch.Draw(Effect_LB_2, Dot.Position, null, Color.White * 0.05f * multiplier, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.1f);
                                spriteBatch.Draw(Effect_BW_2, Dot.Position, null, Color.White * (0.5f - 0.05f * multiplier), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                            }
                            else
                            {
                                spriteBatch.Draw(Effect_LB_3, Dot.Position, null, Color.White * 0.05f * multiplier, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.1f);
                                spriteBatch.Draw(Effect_BW_3, Dot.Position, null, Color.White * (0.5f - 0.05f * multiplier), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                            }
                        }
                    }
                    else
                    {
                        spriteBatch.Draw(Attack.Sprite, Attack.Position, null, Color.White * 0.05f * multiplier, Attack.Rotation + MathHelper.ToRadians(90), Vector2.Zero, 1.0f, SpriteEffects.None, 0.1f);
                        spriteBatch.Draw(Attack.BWSprite, Attack.Position, null, Color.White * (0.5f - 0.05f * multiplier), Attack.Rotation + MathHelper.ToRadians(90), Vector2.Zero, 1.0f, SpriteEffects.None, 0f);
                    }
                }


                //::::::::::::::::::::::::::::::::Drawing enemy attacks::::::::::::::::::::::::::::::::::::::::::
                foreach (Attack Attack in EnemyAttacks)
                {
                    if (Attack is Lightning)
                    {
                        foreach (Dot Dot in (Attack as Lightning).Dots)
                        {
                            if (Dot.Life >= 2)
                            {
                                spriteBatch.Draw(Effect_B_1, Dot.Position, null, Color.White * 0.1f * multiplier, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.1f);
                                spriteBatch.Draw(Effect_BW_1, Dot.Position, null, Color.White * (1f - 0.1f * multiplier), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                            }
                            else if (Dot.Life == 1)
                            {
                                spriteBatch.Draw(Effect_B_2, Dot.Position, null, Color.White * 0.1f * multiplier, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.1f);
                                spriteBatch.Draw(Effect_BW_2, Dot.Position, null, Color.White * (1f - 0.1f * multiplier), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                            }
                            else
                            {
                                spriteBatch.Draw(Effect_B_3, Dot.Position, null, Color.White * 0.1f * multiplier, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.1f);
                                spriteBatch.Draw(Effect_BW_3, Dot.Position, null, Color.White * (1f - 0.1f * multiplier), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                            }
                        }
                    }
                    else if (Attack is Shockwave)
                    {
                        foreach (Dot Dot in (Attack as Shockwave).Dots)
                        {
                            if (Dot.Life >= 2)
                            {
                                spriteBatch.Draw(Effect_LB_1, Dot.Position, null, Color.White * 0.1f * multiplier, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.1f);
                                spriteBatch.Draw(Effect_BW_1, Dot.Position, null, Color.White * (1f - 0.1f * multiplier), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                            }
                            else if (Dot.Life == 1)
                            {
                                spriteBatch.Draw(Effect_LB_2, Dot.Position, null, Color.White * 0.1f * multiplier, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.1f);
                                spriteBatch.Draw(Effect_BW_2, Dot.Position, null, Color.White * (1f - 0.1f * multiplier), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                            }
                            else
                            {
                                spriteBatch.Draw(Effect_LB_3, Dot.Position, null, Color.White * 0.1f * multiplier, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.1f);
                                spriteBatch.Draw(Effect_BW_3, Dot.Position, null, Color.White * (1f - 0.1f * multiplier), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                            }
                        }
                    }
                    else
                    {
                        spriteBatch.Draw(Attack.Sprite, Attack.Position, null, Color.White * 0.1f * multiplier, Attack.Rotation + MathHelper.ToRadians(90), new Vector2(Attack.Sprite.Width, Attack.Sprite.Height), 1.0f, SpriteEffects.None, 0.1f);
                        spriteBatch.Draw(Attack.BWSprite, Attack.Position, null, Color.White * (1f - 0.1f * multiplier), Attack.Rotation + MathHelper.ToRadians(90), new Vector2(Attack.Sprite.Width, Attack.Sprite.Height), 1.0f, SpriteEffects.None, 0f);
                    }
                }


                //::::::::::::::::::::::::::::::::Drawing effects::::::::::::::::::::::::::::::::::::::::::
                if (!InvOpen & !Paused)
                {
                    foreach (Classes.Effect Effect in Effects)
                    {
                        int lifePerc = (int)(Effect.Life * (100 / Effect.Duration));
                        if (lifePerc > 66)
                        {
                            spriteBatch.Draw(Effect.Sprite100, Effect.Position, null, Color.White * 0.075f * multiplier, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.9f);
                            spriteBatch.Draw(Effect.BWSprite100, Effect.Position, null, Color.White * (0.75f - 0.075f * multiplier), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.8f);
                        }
                        else if (lifePerc > 33)
                        {
                            spriteBatch.Draw(Effect.Sprite66, Effect.Position, null, Color.White * 0.075f * multiplier, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.9f);
                            spriteBatch.Draw(Effect.BWSprite66, Effect.Position, null, Color.White * (0.75f - 0.075f * multiplier), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.8f);
                        }
                        else
                        {
                            spriteBatch.Draw(Effect.Sprite33, Effect.Position, null, Color.White * 0.075f * multiplier, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.9f);
                            spriteBatch.Draw(Effect.BWSprite33, Effect.Position, null, Color.White * (0.75f - 0.075f * multiplier), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.8f);
                        }
                    }
                }


                //::::::::::::::::::::::::::::::::Drawing items::::::::::::::::::::::::::::::::::::::::::
                foreach (Item Item in Items)
                {
                    spriteBatch.Draw(Item.Sprite, Item.Position, null, Color.White * (0.4f + 0.06f * multiplier), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.7f);
                    spriteBatch.Draw(Item.BWSprite, Item.Position, null, Color.White * (0.6f - 0.06f * multiplier), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.6f);
                }


                //::::::::::::::::::::::::::::::::Drawing item comparison::::::::::::::::::::::::::::::::::::::::::
                if (ItemOnHold != null & Player != null)
                {
                    spriteBatch.Draw(ItemOnHold.Sprite, new Vector2(Player.Position.X + Player.Sprite100.Width / 2 - ItemOnHold.Sprite.Width / 2, Player.Position.Y + Player.Sprite100.Height), null, Color.White * (0.4f + 0.06f * multiplier), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.7f);
                    spriteBatch.Draw(ItemOnHold.BWSprite, new Vector2(Player.Position.X + Player.Sprite100.Width / 2 - ItemOnHold.Sprite.Width / 2, Player.Position.Y + Player.Sprite100.Height), null, Color.White * (0.6f - 0.06f * multiplier), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.6f);
                    spriteBatch.DrawString(Font, "Q", new Vector2(Player.Position.X + 5, Player.Position.Y + 10), Color.White);
                    spriteBatch.DrawString(Font, "W", new Vector2(Player.Position.X + Player.Sprite100.Width - 25, Player.Position.Y + 10), Color.White);



                    //................................Left weapon color..........................................
                    if (ItemOnHold.Damage > Player.Weapons[0].Damage)
                        spriteBatch.DrawString(Font, "DMG +", new Vector2(Player.Position.X - 40, Player.Position.Y + 10), Color.Green * (0.3f + 0.07f * multiplier), 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0f);
                    else if (ItemOnHold.Damage < Player.Weapons[0].Damage)
                        spriteBatch.DrawString(Font, "DMG -", new Vector2(Player.Position.X - 40, Player.Position.Y + 10), Color.Red * (0.3f + 0.07f * multiplier), 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0f);
                    else
                        spriteBatch.DrawString(Font, "DMG =", new Vector2(Player.Position.X - 40, Player.Position.Y + 10), Color.White, 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0f);

                    if (ItemOnHold.Type == 6)
                    {
                        if (Player.Weapons[0].Type == 6)
                        {
                            if (ItemOnHold.Projectiles > Player.Weapons[0].Projectiles)
                                spriteBatch.DrawString(Font, "WI +", new Vector2(Player.Position.X - 30, Player.Position.Y + 30), Color.Green * (0.3f + 0.07f * multiplier), 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0f);
                            else if (ItemOnHold.Projectiles < Player.Weapons[0].Projectiles)
                                spriteBatch.DrawString(Font, "WI -", new Vector2(Player.Position.X - 30, Player.Position.Y + 30), Color.Red * (0.3f + 0.07f * multiplier), 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0f);
                            else
                                spriteBatch.DrawString(Font, "WI =", new Vector2(Player.Position.X - 30, Player.Position.Y + 30), Color.White, 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0f);
                        }
                        else
                            spriteBatch.DrawString(Font, "----", new Vector2(Player.Position.X - 30, Player.Position.Y + 30), Color.White, 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0f);
                    }
                    else
                    {
                        if (Player.Weapons[0].Type == 6)
                            spriteBatch.DrawString(Font, "----", new Vector2(Player.Position.X - 30, Player.Position.Y + 30), Color.White, 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0f);
                        else
                        {
                            if (ItemOnHold.Projectiles > Player.Weapons[0].Projectiles)
                                spriteBatch.DrawString(Font, "PR +", new Vector2(Player.Position.X - 30, Player.Position.Y + 30), Color.Green * (0.3f + 0.07f * multiplier), 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0f);
                            else if (ItemOnHold.Projectiles < Player.Weapons[0].Projectiles)
                                spriteBatch.DrawString(Font, "PR -", new Vector2(Player.Position.X - 30, Player.Position.Y + 30), Color.Red * (0.3f + 0.07f * multiplier), 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0f);
                            else
                                spriteBatch.DrawString(Font, "PR =", new Vector2(Player.Position.X - 30, Player.Position.Y + 30), Color.White, 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0f);
                        }
                    }

                    if (ItemOnHold.FireRate < Player.Weapons[0].FireRate)
                        spriteBatch.DrawString(Font, "FR +", new Vector2(Player.Position.X - 30, Player.Position.Y + 50), Color.Green * (0.3f + 0.07f * multiplier), 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0f);
                    else if (ItemOnHold.FireRate > Player.Weapons[0].FireRate)
                        spriteBatch.DrawString(Font, "FR -", new Vector2(Player.Position.X - 30, Player.Position.Y + 50), Color.Red * (0.3f + 0.07f * multiplier), 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0f);
                    else
                        spriteBatch.DrawString(Font, "FR =", new Vector2(Player.Position.X - 30, Player.Position.Y + 50), Color.White, 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0f);

                    if (ItemOnHold.Type == 5)
                    {
                        if (Player.Weapons[0].Type == 5)
                        {
                            if (ItemOnHold.ProjectileSpeed > Player.Weapons[0].ProjectileSpeed)
                                spriteBatch.DrawString(Font, "LE +", new Vector2(Player.Position.X - 30, Player.Position.Y + 70), Color.Green * (0.3f + 0.07f * multiplier), 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0f);
                            else if (ItemOnHold.ProjectileSpeed < Player.Weapons[0].ProjectileSpeed)
                                spriteBatch.DrawString(Font, "LE -", new Vector2(Player.Position.X - 30, Player.Position.Y + 70), Color.Red * (0.3f + 0.07f * multiplier), 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0f);
                            else
                                spriteBatch.DrawString(Font, "LE =", new Vector2(Player.Position.X - 30, Player.Position.Y + 70), Color.White, 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0f);
                        }
                        else
                            spriteBatch.DrawString(Font, "----", new Vector2(Player.Position.X - 30, Player.Position.Y + 70), Color.White, 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0f);
                    }
                    else if (ItemOnHold.Type != 3 & Player.Weapons[0].Type != 5)
                    {
                        if (ItemOnHold.ProjectileSpeed > Player.Weapons[0].ProjectileSpeed)
                            spriteBatch.DrawString(Font, "PS +", new Vector2(Player.Position.X - 30, Player.Position.Y + 70), Color.Green * (0.3f + 0.07f * multiplier), 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0f);
                        else if (ItemOnHold.ProjectileSpeed < Player.Weapons[0].ProjectileSpeed)
                            spriteBatch.DrawString(Font, "PS -", new Vector2(Player.Position.X - 30, Player.Position.Y + 70), Color.Red * (0.3f + 0.07f * multiplier), 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0f);
                        else
                            spriteBatch.DrawString(Font, "PS =", new Vector2(Player.Position.X - 30, Player.Position.Y + 70), Color.White, 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0f);
                    }
                    else
                        spriteBatch.DrawString(Font, "----", new Vector2(Player.Position.X - 30, Player.Position.Y + 70), Color.White, 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0f);


                    //................................Right weapon color..........................................
                    if (ItemOnHold.Damage > Player.Weapons[1].Damage)
                        spriteBatch.DrawString(Font, "+ DMG", new Vector2(Player.Position.X + Player.Sprite100.Width, Player.Position.Y + 10), Color.Green * (0.3f + 0.07f * multiplier), 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0f);
                    else if (ItemOnHold.Damage < Player.Weapons[1].Damage)
                        spriteBatch.DrawString(Font, "- DMG", new Vector2(Player.Position.X + Player.Sprite100.Width, Player.Position.Y + 10), Color.Red * (0.3f + 0.07f * multiplier), 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0f);
                    else
                        spriteBatch.DrawString(Font, "= DMG", new Vector2(Player.Position.X + Player.Sprite100.Width, Player.Position.Y + 10), Color.White, 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0f);

                    if (ItemOnHold.Type == 6)
                    {
                        if (Player.Weapons[1].Type == 6)
                        {
                            if (ItemOnHold.Projectiles > Player.Weapons[1].Projectiles)
                                spriteBatch.DrawString(Font, "+ WI", new Vector2(Player.Position.X + Player.Sprite100.Width, Player.Position.Y + 30), Color.Green * (0.3f + 0.07f * multiplier), 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0f);
                            else if (ItemOnHold.Projectiles < Player.Weapons[1].Projectiles)
                                spriteBatch.DrawString(Font, "- WI", new Vector2(Player.Position.X + Player.Sprite100.Width, Player.Position.Y + 30), Color.Red * (0.3f + 0.07f * multiplier), 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0f);
                            else
                                spriteBatch.DrawString(Font, "= WI", new Vector2(Player.Position.X + Player.Sprite100.Width, Player.Position.Y + 30), Color.White, 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0f);
                        }
                        else
                            spriteBatch.DrawString(Font, "----", new Vector2(Player.Position.X + Player.Sprite100.Width, Player.Position.Y + 30), Color.White, 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0f);
                    }
                    else
                    {
                        if (Player.Weapons[1].Type == 6)
                            spriteBatch.DrawString(Font, "----", new Vector2(Player.Position.X + Player.Sprite100.Width, Player.Position.Y + 30), Color.White, 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0f);
                        else
                        {
                            if (ItemOnHold.Projectiles > Player.Weapons[1].Projectiles)
                                spriteBatch.DrawString(Font, "+ PR", new Vector2(Player.Position.X + Player.Sprite100.Width, Player.Position.Y + 30), Color.Green * (0.3f + 0.07f * multiplier), 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0f);
                            else if (ItemOnHold.Projectiles < Player.Weapons[1].Projectiles)
                                spriteBatch.DrawString(Font, "- PR", new Vector2(Player.Position.X + Player.Sprite100.Width, Player.Position.Y + 30), Color.Red * (0.3f + 0.07f * multiplier), 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0f);
                            else
                                spriteBatch.DrawString(Font, "= PR", new Vector2(Player.Position.X + Player.Sprite100.Width, Player.Position.Y + 30), Color.White, 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0f);
                        }
                    }

                    if (ItemOnHold.FireRate < Player.Weapons[1].FireRate)
                        spriteBatch.DrawString(Font, "+ FR", new Vector2(Player.Position.X + Player.Sprite100.Width, Player.Position.Y + 50), Color.Green * (0.3f + 0.07f * multiplier), 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0f);
                    else if (ItemOnHold.FireRate > Player.Weapons[1].FireRate)
                        spriteBatch.DrawString(Font, "- FR", new Vector2(Player.Position.X + Player.Sprite100.Width, Player.Position.Y + 50), Color.Red * (0.3f + 0.07f * multiplier), 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0f);
                    else
                        spriteBatch.DrawString(Font, "= FR", new Vector2(Player.Position.X + Player.Sprite100.Width, Player.Position.Y + 50), Color.White, 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0f);

                    if (ItemOnHold.Type == 5)
                    {
                        if (Player.Weapons[1].Type == 5)
                        {
                            if (ItemOnHold.ProjectileSpeed > Player.Weapons[1].ProjectileSpeed)
                                spriteBatch.DrawString(Font, "+ LE", new Vector2(Player.Position.X + Player.Sprite100.Width, Player.Position.Y + 70), Color.Green * (0.3f + 0.07f * multiplier), 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0f);
                            else if (ItemOnHold.ProjectileSpeed < Player.Weapons[1].ProjectileSpeed)
                                spriteBatch.DrawString(Font, "- LE", new Vector2(Player.Position.X + Player.Sprite100.Width, Player.Position.Y + 70), Color.Red * (0.3f + 0.07f * multiplier), 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0f);
                            else
                                spriteBatch.DrawString(Font, "= LE", new Vector2(Player.Position.X + Player.Sprite100.Width, Player.Position.Y + 70), Color.White, 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0f);
                        }
                        else
                            spriteBatch.DrawString(Font, "----", new Vector2(Player.Position.X + Player.Sprite100.Width, Player.Position.Y + 70), Color.White, 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0f);
                    }
                    else if (ItemOnHold.Type != 3 & Player.Weapons[1].Type != 5)
                    {
                        if (ItemOnHold.ProjectileSpeed > Player.Weapons[1].ProjectileSpeed)
                            spriteBatch.DrawString(Font, "+ PS", new Vector2(Player.Position.X + Player.Sprite100.Width, Player.Position.Y + 70), Color.Green * (0.3f + 0.07f * multiplier), 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0f);
                        else if (ItemOnHold.ProjectileSpeed < Player.Weapons[1].ProjectileSpeed)
                            spriteBatch.DrawString(Font, "- PS", new Vector2(Player.Position.X + Player.Sprite100.Width, Player.Position.Y + 70), Color.Red * (0.3f + 0.07f * multiplier), 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0f);
                        else
                            spriteBatch.DrawString(Font, "= PS", new Vector2(Player.Position.X + Player.Sprite100.Width, Player.Position.Y + 70), Color.White, 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0f);
                    }
                    else
                        spriteBatch.DrawString(Font, "----", new Vector2(Player.Position.X + Player.Sprite100.Width, Player.Position.Y + 70), Color.White, 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0f);




                    //................................Left Weapon black and white..........................................
                    if (ItemOnHold.Damage > Player.Weapons[0].Damage)
                        spriteBatch.DrawString(Font, "DMG +", new Vector2(Player.Position.X - 40, Player.Position.Y + 10), Color.White * (0.7f - 0.07f * multiplier), 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0.1f);
                    else if (ItemOnHold.Damage < Player.Weapons[0].Damage)
                        spriteBatch.DrawString(Font, "DMG -", new Vector2(Player.Position.X - 40, Player.Position.Y + 10), Color.White * (0.7f - 0.07f * multiplier), 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0.1f);
                    else
                        spriteBatch.DrawString(Font, "DMG =", new Vector2(Player.Position.X - 40, Player.Position.Y + 10), Color.White * (0.7f - 0.07f * multiplier), 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0.1f);

                    if (ItemOnHold.Type == 6)
                    {
                        if (Player.Weapons[0].Type == 6)
                        {
                            if (ItemOnHold.Projectiles > Player.Weapons[0].Projectiles)
                                spriteBatch.DrawString(Font, "WI +", new Vector2(Player.Position.X - 30, Player.Position.Y + 30), Color.White * (0.7f - 0.07f * multiplier), 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0.1f);
                            else if (ItemOnHold.Projectiles < Player.Weapons[0].Projectiles)
                                spriteBatch.DrawString(Font, "WI -", new Vector2(Player.Position.X - 30, Player.Position.Y + 30), Color.White * (0.7f - 0.07f * multiplier), 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0.1f);
                            else
                                spriteBatch.DrawString(Font, "WI =", new Vector2(Player.Position.X - 30, Player.Position.Y + 30), Color.White * (0.7f - 0.07f * multiplier), 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0.1f);
                        }
                        else
                            spriteBatch.DrawString(Font, "----", new Vector2(Player.Position.X - 30, Player.Position.Y + 30), Color.White * (0.7f - 0.07f * multiplier), 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0.1f);
                    }
                    else
                    {
                        if (Player.Weapons[0].Type == 6)
                            spriteBatch.DrawString(Font, "----", new Vector2(Player.Position.X - 30, Player.Position.Y + 30), Color.White * (0.7f - 0.07f * multiplier), 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0.1f);
                        else
                        {
                            if (ItemOnHold.Projectiles > Player.Weapons[0].Projectiles)
                                spriteBatch.DrawString(Font, "PR +", new Vector2(Player.Position.X - 30, Player.Position.Y + 30), Color.White * (0.7f - 0.07f * multiplier), 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0.1f);
                            else if (ItemOnHold.Projectiles < Player.Weapons[0].Projectiles)
                                spriteBatch.DrawString(Font, "PR -", new Vector2(Player.Position.X - 30, Player.Position.Y + 30), Color.White * (0.7f - 0.07f * multiplier), 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0.1f);
                            else
                                spriteBatch.DrawString(Font, "PR =", new Vector2(Player.Position.X - 30, Player.Position.Y + 30), Color.White * (0.7f - 0.07f * multiplier), 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0.1f);
                        }
                    }

                    if (ItemOnHold.FireRate < Player.Weapons[0].FireRate)
                        spriteBatch.DrawString(Font, "FR +", new Vector2(Player.Position.X - 30, Player.Position.Y + 50), Color.White * (0.7f - 0.07f * multiplier), 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0.1f);
                    else if (ItemOnHold.FireRate > Player.Weapons[0].FireRate)
                        spriteBatch.DrawString(Font, "FR -", new Vector2(Player.Position.X - 30, Player.Position.Y + 50), Color.White * (0.7f - 0.07f * multiplier), 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0.1f);
                    else
                        spriteBatch.DrawString(Font, "FR =", new Vector2(Player.Position.X - 30, Player.Position.Y + 50), Color.White * (0.7f - 0.07f * multiplier), 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0.1f);

                    if (ItemOnHold.Type == 5)
                    {
                        if (Player.Weapons[0].Type == 5)
                        {
                            if (ItemOnHold.ProjectileSpeed > Player.Weapons[0].ProjectileSpeed)
                                spriteBatch.DrawString(Font, "LE +", new Vector2(Player.Position.X - 30, Player.Position.Y + 70), Color.White * (0.7f - 0.07f * multiplier), 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0.1f);
                            else if (ItemOnHold.ProjectileSpeed < Player.Weapons[0].ProjectileSpeed)
                                spriteBatch.DrawString(Font, "LE -", new Vector2(Player.Position.X - 30, Player.Position.Y + 70), Color.White * (0.7f - 0.07f * multiplier), 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0.1f);
                            else
                                spriteBatch.DrawString(Font, "LE =", new Vector2(Player.Position.X - 30, Player.Position.Y + 70), Color.White * (0.7f - 0.07f * multiplier), 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0.1f);
                        }
                        else
                            spriteBatch.DrawString(Font, "----", new Vector2(Player.Position.X - 30, Player.Position.Y + 70), Color.White * (0.7f - 0.07f * multiplier), 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0.1f);
                    }
                    else if (ItemOnHold.Type != 3 & Player.Weapons[0].Type != 5)
                    {
                        if (ItemOnHold.ProjectileSpeed > Player.Weapons[0].ProjectileSpeed)
                            spriteBatch.DrawString(Font, "PS +", new Vector2(Player.Position.X - 30, Player.Position.Y + 70), Color.White * (0.7f - 0.07f * multiplier), 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0.1f);
                        else if (ItemOnHold.ProjectileSpeed < Player.Weapons[0].ProjectileSpeed)
                            spriteBatch.DrawString(Font, "PS -", new Vector2(Player.Position.X - 30, Player.Position.Y + 70), Color.White * (0.7f - 0.07f * multiplier), 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0.1f);
                        else
                            spriteBatch.DrawString(Font, "PS =", new Vector2(Player.Position.X - 30, Player.Position.Y + 70), Color.White * (0.7f - 0.07f * multiplier), 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0.1f);
                    }
                    else
                        spriteBatch.DrawString(Font, "----", new Vector2(Player.Position.X - 30, Player.Position.Y + 70), Color.White * (0.7f - 0.07f * multiplier), 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0.1f);


                    //................................Right weapon black and white..........................................
                    if (ItemOnHold.Damage > Player.Weapons[1].Damage)
                        spriteBatch.DrawString(Font, "+ DMG", new Vector2(Player.Position.X + Player.Sprite100.Width, Player.Position.Y + 10), Color.White * (0.7f - 0.07f * multiplier), 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0.1f);
                    else if (ItemOnHold.Damage < Player.Weapons[1].Damage)
                        spriteBatch.DrawString(Font, "- DMG", new Vector2(Player.Position.X + Player.Sprite100.Width, Player.Position.Y + 10), Color.White * (0.7f - 0.07f * multiplier), 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0.1f);
                    else
                        spriteBatch.DrawString(Font, "= DMG", new Vector2(Player.Position.X + Player.Sprite100.Width, Player.Position.Y + 10), Color.White * (0.7f - 0.07f * multiplier), 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0.1f);

                    if (ItemOnHold.Type == 6)
                    {
                        if (Player.Weapons[1].Type == 6)
                        {
                            if (ItemOnHold.Projectiles > Player.Weapons[1].Projectiles)
                                spriteBatch.DrawString(Font, "+ WI", new Vector2(Player.Position.X + Player.Sprite100.Width, Player.Position.Y + 30), Color.White * (0.7f - 0.07f * multiplier), 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0.1f);
                            else if (ItemOnHold.Projectiles < Player.Weapons[1].Projectiles)
                                spriteBatch.DrawString(Font, "- WI", new Vector2(Player.Position.X + Player.Sprite100.Width, Player.Position.Y + 30), Color.White * (0.7f - 0.07f * multiplier), 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0.1f);
                            else
                                spriteBatch.DrawString(Font, "= WI", new Vector2(Player.Position.X + Player.Sprite100.Width, Player.Position.Y + 30), Color.White * (0.7f - 0.07f * multiplier), 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0.1f);
                        }
                        else
                            spriteBatch.DrawString(Font, "----", new Vector2(Player.Position.X + Player.Sprite100.Width, Player.Position.Y + 30), Color.White * (0.7f - 0.07f * multiplier), 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0.1f);
                    }
                    else
                    {
                        if (Player.Weapons[1].Type == 6)
                            spriteBatch.DrawString(Font, "----", new Vector2(Player.Position.X + Player.Sprite100.Width, Player.Position.Y + 30), Color.White * (0.7f - 0.07f * multiplier), 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0.1f);
                        else
                        {
                            if (ItemOnHold.Projectiles > Player.Weapons[1].Projectiles)
                                spriteBatch.DrawString(Font, "+ PR", new Vector2(Player.Position.X + Player.Sprite100.Width, Player.Position.Y + 30), Color.White * (0.7f - 0.07f * multiplier), 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0.1f);
                            else if (ItemOnHold.Projectiles < Player.Weapons[1].Projectiles)
                                spriteBatch.DrawString(Font, "- PR", new Vector2(Player.Position.X + Player.Sprite100.Width, Player.Position.Y + 30), Color.White * (0.7f - 0.07f * multiplier), 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0.1f);
                            else
                                spriteBatch.DrawString(Font, "= PR", new Vector2(Player.Position.X + Player.Sprite100.Width, Player.Position.Y + 30), Color.White * (0.7f - 0.07f * multiplier), 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0.1f);
                        }
                    }

                    if (ItemOnHold.FireRate < Player.Weapons[1].FireRate)
                        spriteBatch.DrawString(Font, "+ FR", new Vector2(Player.Position.X + Player.Sprite100.Width, Player.Position.Y + 50), Color.White * (0.7f - 0.07f * multiplier), 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0.1f);
                    else if (ItemOnHold.FireRate > Player.Weapons[1].FireRate)
                        spriteBatch.DrawString(Font, "- FR", new Vector2(Player.Position.X + Player.Sprite100.Width, Player.Position.Y + 50), Color.White * (0.7f - 0.07f * multiplier), 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0.1f);
                    else
                        spriteBatch.DrawString(Font, "= FR", new Vector2(Player.Position.X + Player.Sprite100.Width, Player.Position.Y + 50), Color.White * (0.7f - 0.07f * multiplier), 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0.1f);

                    if (ItemOnHold.Type == 5)
                    {
                        if (Player.Weapons[1].Type == 5)
                        {
                            if (ItemOnHold.ProjectileSpeed > Player.Weapons[1].ProjectileSpeed)
                                spriteBatch.DrawString(Font, "+ LE", new Vector2(Player.Position.X + Player.Sprite100.Width, Player.Position.Y + 70), Color.White * (0.7f - 0.07f * multiplier), 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0.1f);
                            else if (ItemOnHold.ProjectileSpeed < Player.Weapons[1].ProjectileSpeed)
                                spriteBatch.DrawString(Font, "- LE", new Vector2(Player.Position.X + Player.Sprite100.Width, Player.Position.Y + 70), Color.White * (0.7f - 0.07f * multiplier), 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0.1f);
                            else
                                spriteBatch.DrawString(Font, "= LE", new Vector2(Player.Position.X + Player.Sprite100.Width, Player.Position.Y + 70), Color.White * (0.7f - 0.07f * multiplier), 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0.1f);
                        }
                        else
                            spriteBatch.DrawString(Font, "----", new Vector2(Player.Position.X + Player.Sprite100.Width, Player.Position.Y + 70), Color.White * (0.7f - 0.07f * multiplier), 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0.1f);
                    }
                    else if (ItemOnHold.Type != 3 & Player.Weapons[1].Type != 5)
                    {
                        if (ItemOnHold.ProjectileSpeed > Player.Weapons[1].ProjectileSpeed)
                            spriteBatch.DrawString(Font, "+ PS", new Vector2(Player.Position.X + Player.Sprite100.Width, Player.Position.Y + 70), Color.White * (0.7f - 0.07f * multiplier), 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0.1f);
                        else if (ItemOnHold.ProjectileSpeed < Player.Weapons[1].ProjectileSpeed)
                            spriteBatch.DrawString(Font, "- PS", new Vector2(Player.Position.X + Player.Sprite100.Width, Player.Position.Y + 70), Color.White * (0.7f - 0.07f * multiplier), 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0.1f);
                        else
                            spriteBatch.DrawString(Font, "= PS", new Vector2(Player.Position.X + Player.Sprite100.Width, Player.Position.Y + 70), Color.White * (0.7f - 0.07f * multiplier), 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0.1f);
                    }
                    else
                        spriteBatch.DrawString(Font, "----", new Vector2(Player.Position.X + Player.Sprite100.Width, Player.Position.Y + 70), Color.White * (0.7f - 0.07f * multiplier), 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0.1f);
                }

                spriteBatch.End();



                //-----------------------------Drawing inventory------------------------------------------------------
                if (InvOpen)
                {
                    spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
                    spriteBatch.Draw(Inv, Vector2.Zero, Color.White);
                    if (rand.Next(1, 101) == 1)
                    {
                        spriteBatch.Draw(Inv, new Vector2(rand.Next(-10, 11), rand.Next(-10, 11)), Color.White);
                    }
                    spriteBatch.End();



                    //::::::::::::::::::::::::::::::::Drawing item stats::::::::::::::::::::::::::::::::::::::::::
                    spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
                    spriteBatch.DrawString(Font, "Health: " + Convert.ToString(Player.Health), new Vector2(500, 170), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.3f);
                    spriteBatch.DrawString(Font, "Score: " + Convert.ToString(Score), new Vector2(500, 200), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.3f);
                    spriteBatch.DrawString(Font, "Multiplier: " + Convert.ToString(Math.Round(multiplier, 2)), new Vector2(500, 230), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.3f);
                    foreach (Item Item in Inventory)
                    {
                        spriteBatch.Draw(Item.Sprite, Item.Position, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.2f);
                        if (lastMouseState.X > Item.Position.X & lastMouseState.X < Item.Position.X + Item.Sprite.Width & lastMouseState.Y > Item.Position.Y & lastMouseState.Y < Item.Position.Y + Item.Sprite.Height & lastMouseState.LeftButton == ButtonState.Released & !Paused)
                        {
                            string Name1 = Item.Name;
                            string Name2 = "";
                            string Name3 = "";
                            int X = 0;
                            int Y = 0;
                            for (int i = 0; i <= Item.Name.Length - 1; i++)
                            {
                                if (Item.Name[i] == Convert.ToChar(" "))
                                {
                                    if (i > 8 & i < 19)
                                    {
                                        Name1 = Item.Name.Substring(0, i);
                                        Name2 = Item.Name.Substring(i, Item.Name.Length - i);
                                        X = i;
                                        if (Y == 0)
                                            Y++;
                                    }
                                    if (i > 18 & i < 28)
                                    {
                                        Name2 = Item.Name.Substring(X, i - X);
                                        Name3 = Item.Name.Substring(i, Item.Name.Length - i);
                                    }
                                }
                            }
                            if (Item.Name.Length > 27)
                                Y++;
                            float yPos = MathHelper.Clamp(lastMouseState.Y, 1, 700);
                            spriteBatch.Draw(Box, new Vector2(lastMouseState.X - 200, yPos), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.1f);
                            spriteBatch.DrawString(Font, String.Format(Name1 + "\n" + Name2 + "\n" + Name3), new Vector2(lastMouseState.X - 165, yPos + 30), Color.White, 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0f);
                            spriteBatch.DrawString(Font, "Damage    " + Convert.ToString((Item as WeaponPickup).Damage), new Vector2(lastMouseState.X - 165, yPos + 50 + Y * 20), Color.White, 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0f);
                            if ((Item as WeaponPickup).Type == 6)
                                spriteBatch.DrawString(Font, "Width      " + Convert.ToString((Item as WeaponPickup).Projectiles), new Vector2(lastMouseState.X - 165, yPos + 70 + Y * 20), Color.White, 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0f);
                            else
                                spriteBatch.DrawString(Font, "Projectiles " + Convert.ToString((Item as WeaponPickup).Projectiles), new Vector2(lastMouseState.X - 165, yPos + 70 + Y * 20), Color.White, 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0f);
                            spriteBatch.DrawString(Font, "Firerate    " + Convert.ToString((Item as WeaponPickup).FireRate), new Vector2(lastMouseState.X - 165, yPos + 90 + Y * 20), Color.White, 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0f);
                            if ((Item as WeaponPickup).Type == 5)
                                spriteBatch.DrawString(Font, "Length     " + Convert.ToString((Item as WeaponPickup).ProjectileSpeed), new Vector2(lastMouseState.X - 165, yPos + 110 + Y * 20), Color.White, 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0f);
                            else if ((Item as WeaponPickup).Type != 3)
                                spriteBatch.DrawString(Font, String.Format("Projectile\n Speed     ") + Convert.ToString((Item as WeaponPickup).ProjectileSpeed), new Vector2(lastMouseState.X - 165, yPos + 110 + Y * 20), Color.White, 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0f);


                            //................................Drawing left weapon stats..........................................
                            if (Keyboard.GetState().IsKeyDown(Keys.Q) & Keyboard.GetState().IsKeyUp(Keys.W) & EquippedLeft != null)
                            {
                                Name1 = EquippedLeft.Name;
                                Name2 = "";
                                Name3 = "";
                                X = 0;
                                Y = 0;
                                for (int i = 0; i <= EquippedLeft.Name.Length - 1; i++)
                                {
                                    if (EquippedLeft.Name[i] == Convert.ToChar(" "))
                                    {
                                        if (i > 8 & i < 19)
                                        {
                                            Name1 = EquippedLeft.Name.Substring(0, i);
                                            Name2 = EquippedLeft.Name.Substring(i, EquippedLeft.Name.Length - i);
                                            X = i;
                                            if (Y == 0)
                                                Y++;
                                        }
                                        if (i > 18 & i < 28)
                                        {
                                            Name2 = EquippedLeft.Name.Substring(X, i - X);
                                            Name3 = EquippedLeft.Name.Substring(i, EquippedLeft.Name.Length - i);
                                        }
                                    }
                                }
                                if (EquippedLeft.Name.Length > 27)
                                    Y++;
                                yPos = MathHelper.Clamp(lastMouseState.Y, 1, 700);
                                spriteBatch.Draw(Box, new Vector2(lastMouseState.X - 400, yPos), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.1f);
                                spriteBatch.DrawString(Font, String.Format(Name1 + "\n" + Name2 + "\n" + Name3), new Vector2(lastMouseState.X - 365, yPos + 30), Color.White, 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0f);
                                spriteBatch.DrawString(Font, "Damage    " + Convert.ToString((EquippedLeft as WeaponPickup).Damage), new Vector2(lastMouseState.X - 365, yPos + 50 + Y * 20), Color.White, 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0f);
                                if ((EquippedLeft as WeaponPickup).Type == 6)
                                    spriteBatch.DrawString(Font, "Width      " + Convert.ToString((EquippedLeft as WeaponPickup).Projectiles), new Vector2(lastMouseState.X - 365, yPos + 70 + Y * 20), Color.White, 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0f);
                                else
                                    spriteBatch.DrawString(Font, "Projectiles " + Convert.ToString((EquippedLeft as WeaponPickup).Projectiles), new Vector2(lastMouseState.X - 365, yPos + 70 + Y * 20), Color.White, 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0f);
                                spriteBatch.DrawString(Font, "Firerate    " + Convert.ToString((EquippedLeft as WeaponPickup).FireRate), new Vector2(lastMouseState.X - 365, yPos + 90 + Y * 20), Color.White, 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0f);
                                if ((EquippedLeft as WeaponPickup).Type == 5)
                                    spriteBatch.DrawString(Font, "Length     " + Convert.ToString((EquippedLeft as WeaponPickup).ProjectileSpeed), new Vector2(lastMouseState.X - 365, yPos + 110 + Y * 20), Color.White, 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0f);
                                else if ((EquippedLeft as WeaponPickup).Type != 3)
                                    spriteBatch.DrawString(Font, String.Format("Projectile\n Speed     ") + Convert.ToString((EquippedLeft as WeaponPickup).ProjectileSpeed), new Vector2(lastMouseState.X - 365, yPos + 110 + Y * 20), Color.White, 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0f);
                            }


                            //::::::::::::::::::::::::::::::::Drawing right weapon stats::::::::::::::::::::::::::::::::::::::::::
                            if (Keyboard.GetState().IsKeyDown(Keys.W) & Keyboard.GetState().IsKeyUp(Keys.Q) & EquippedRight != null)
                            {
                                Name1 = EquippedRight.Name;
                                Name2 = "";
                                Name3 = "";
                                X = 0;
                                Y = 0;
                                for (int i = 0; i <= EquippedRight.Name.Length - 1; i++)
                                {
                                    if (EquippedRight.Name[i] == Convert.ToChar(" "))
                                    {
                                        if (i > 8 & i < 19)
                                        {
                                            Name1 = EquippedRight.Name.Substring(0, i);
                                            Name2 = EquippedRight.Name.Substring(i, EquippedRight.Name.Length - i);
                                            X = i;
                                            if (Y == 0)
                                                Y++;
                                        }
                                        if (i > 18 & i < 28)
                                        {
                                            Name2 = EquippedRight.Name.Substring(X, i - X);
                                            Name3 = EquippedRight.Name.Substring(i, EquippedRight.Name.Length - i);
                                        }
                                    }
                                }
                                if (EquippedRight.Name.Length > 27)
                                    Y++;
                                yPos = MathHelper.Clamp(lastMouseState.Y, 1, 700);
                                spriteBatch.Draw(Box, new Vector2(lastMouseState.X - 400, yPos), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.1f);
                                spriteBatch.DrawString(Font, String.Format(Name1 + "\n" + Name2 + "\n" + Name3), new Vector2(lastMouseState.X - 365, yPos + 30), Color.White, 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0f);
                                spriteBatch.DrawString(Font, "Damage    " + Convert.ToString((EquippedRight as WeaponPickup).Damage), new Vector2(lastMouseState.X - 365, yPos + 50 + Y * 20), Color.White, 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0f);
                                if ((EquippedRight as WeaponPickup).Type == 6)
                                    spriteBatch.DrawString(Font, "Width      " + Convert.ToString((EquippedRight as WeaponPickup).Projectiles), new Vector2(lastMouseState.X - 365, yPos + 70 + Y * 20), Color.White, 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0f);
                                else
                                    spriteBatch.DrawString(Font, "Projectiles " + Convert.ToString((EquippedRight as WeaponPickup).Projectiles), new Vector2(lastMouseState.X - 365, yPos + 70 + Y * 20), Color.White, 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0f);
                                spriteBatch.DrawString(Font, "Firerate    " + Convert.ToString((EquippedRight as WeaponPickup).FireRate), new Vector2(lastMouseState.X - 365, yPos + 90 + Y * 20), Color.White, 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0f);
                                if ((EquippedRight as WeaponPickup).Type == 5)
                                    spriteBatch.DrawString(Font, "Length     " + Convert.ToString((EquippedRight as WeaponPickup).ProjectileSpeed), new Vector2(lastMouseState.X - 365, yPos + 110 + Y * 20), Color.White, 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0f);
                                else if ((EquippedRight as WeaponPickup).Type != 3)
                                    spriteBatch.DrawString(Font, String.Format("Projectile\n Speed     ") + Convert.ToString((EquippedRight as WeaponPickup).ProjectileSpeed), new Vector2(lastMouseState.X - 365, yPos + 110 + Y * 20), Color.White, 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0f);
                            }
                        }
                    }
                    spriteBatch.End();

                    spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
                    spriteBatch.Draw(Cursor_White, new Vector2(Mouse.GetState().X, Mouse.GetState().Y), Color.White);

                    //::::::::::::::::::::::::::::::::Drawing effects::::::::::::::::::::::::::::::::::::::::::
                    foreach (Classes.Effect Effect in Effects)
                    {
                        int lifePerc = (int)(Effect.Life * (100 / Effect.Duration));
                        if (lifePerc > 66)
                        {
                            spriteBatch.Draw(Effect.Sprite100, Effect.Position, null, Color.White * 0.075f * multiplier, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.9f);
                            spriteBatch.Draw(Effect.BWSprite100, Effect.Position, null, Color.White * (0.75f - 0.075f * multiplier), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.8f);
                        }
                        else if (lifePerc > 33)
                        {
                            spriteBatch.Draw(Effect.Sprite66, Effect.Position, null, Color.White * 0.075f * multiplier, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.9f);
                            spriteBatch.Draw(Effect.BWSprite66, Effect.Position, null, Color.White * (0.75f - 0.075f * multiplier), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.8f);
                        }
                        else
                        {
                            spriteBatch.Draw(Effect.Sprite33, Effect.Position, null, Color.White * 0.075f * multiplier, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.9f);
                            spriteBatch.Draw(Effect.BWSprite33, Effect.Position, null, Color.White * (0.75f - 0.075f * multiplier), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.8f);
                        }
                    }
                    spriteBatch.End();
                }



                //-----------------------------Drawing pause menu------------------------------------------------------
                if (Paused)
                {
                    spriteBatch.Begin();
                    spriteBatch.Draw(Box, new Vector2(275, 350), Color.White);
                    spriteBatch.End();

                    spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
                    spriteBatch.Draw(Cursor_White, new Vector2(Mouse.GetState().X, Mouse.GetState().Y), Color.White);

                    //::::::::::::::::::::::::::::::::Drawing effects::::::::::::::::::::::::::::::::::::::::::
                    foreach (Classes.Effect Effect in Effects)
                    {
                        int lifePerc = (int)(Effect.Life * (100 / Effect.Duration));
                        if (lifePerc > 66)
                        {
                            spriteBatch.Draw(Effect.Sprite100, Effect.Position, null, Color.White * 0.075f * multiplier, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.9f);
                            spriteBatch.Draw(Effect.BWSprite100, Effect.Position, null, Color.White * (0.75f - 0.075f * multiplier), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.8f);
                        }
                        else if (lifePerc > 33)
                        {
                            spriteBatch.Draw(Effect.Sprite66, Effect.Position, null, Color.White * 0.075f * multiplier, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.9f);
                            spriteBatch.Draw(Effect.BWSprite66, Effect.Position, null, Color.White * (0.75f - 0.075f * multiplier), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.8f);
                        }
                        else
                        {
                            spriteBatch.Draw(Effect.Sprite33, Effect.Position, null, Color.White * 0.075f * multiplier, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.9f);
                            spriteBatch.Draw(Effect.BWSprite33, Effect.Position, null, Color.White * (0.75f - 0.075f * multiplier), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.8f);
                        }
                    }
                    spriteBatch.End();
                }



                //-----------------------------Drawing game over screen------------------------------------------------------
                if (GameOver)
                {
                    spriteBatch.Begin();
                    spriteBatch.Draw(BlackScreen, Vector2.Zero, Color.White * (1f - 0.01f * -FadeCounter));
                    spriteBatch.End();

                    spriteBatch.Begin();
                    if (Player == null)
                        spriteBatch.DrawString(Font, "Game Over", new Vector2(160 + rand.Next((int)-Math.Pow(FadeCounter, 2), (int)Math.Pow(FadeCounter, 2)) + rand.Next(-1, 2), 100 + rand.Next((int)-Math.Pow(FadeCounter, 2), (int)Math.Pow(FadeCounter, 2)) + rand.Next(-1, 2)), Color.White, 0f, Vector2.Zero, 4.9f, SpriteEffects.None, 0f);
                    else
                        spriteBatch.DrawString(Font, "Level " + Convert.ToString(Level) + " Completed", new Vector2(90 + rand.Next((int)-Math.Pow(FadeCounter, 2), (int)Math.Pow(FadeCounter, 2)) + rand.Next(-1, 2), 100 + rand.Next((int)-Math.Pow(FadeCounter, 2), (int)Math.Pow(FadeCounter, 2)) + rand.Next(-1, 2)), Color.White, 0f, Vector2.Zero, 3.5f, SpriteEffects.None, 0f);
                    if (FadeCounter == 0)
                    {
                        spriteBatch.DrawString(Font, "Score " + Score, new Vector2(340, 200), Color.White);
                        spriteBatch.DrawString(Font, "Highscores", new Vector2(300, 280), Color.White, 0f, Vector2.Zero, 1.5f, SpriteEffects.None, 0f);
                        spriteBatch.Draw(Cursor_White, new Vector2(lastMouseState.X, lastMouseState.Y), Color.White);


                        //::::::::::::::::::::::::::::::::Drawing highscores::::::::::::::::::::::::::::::::::::::::::
                        int number = 1;
                        switch (Level)
                        {
                            case 1:
                                foreach (HighScore HighScore in HighScoresL1)
                                {
                                    string score = Convert.ToString(HighScore.Score);
                                    for (int i = score.Length; i >= 1; i--)
                                    {
                                        int Rest;
                                        Math.DivRem(score.Length - i, 4, out Rest);
                                        if (Rest == 0)
                                            score = score.Insert(i, " ");
                                    }
                                    spriteBatch.DrawString(Font, number + ".   " + HighScore.Name, new Vector2(250, 300 + number * 30), Color.White);
                                    spriteBatch.DrawString(Font, score, new Vector2(440, 300 + number * 30), Color.White);
                                    number++;
                                }
                                break;
                            case 2:
                                foreach (HighScore HighScore in HighScoresL2)
                                {
                                    string score = Convert.ToString(HighScore.Score);
                                    for (int i = score.Length; i >= 1; i--)
                                    {
                                        int Rest;
                                        Math.DivRem(score.Length - i, 4, out Rest);
                                        if (Rest == 0)
                                            score = score.Insert(i, " ");
                                    }
                                    spriteBatch.DrawString(Font, number + ".   " + HighScore.Name, new Vector2(250, 300 + number * 30), Color.White);
                                    spriteBatch.DrawString(Font, score, new Vector2(440, 300 + number * 30), Color.White);
                                    number++;
                                }
                                break;
                            case 3:
                                foreach (HighScore HighScore in HighScoresL3)
                                {
                                    string score = Convert.ToString(HighScore.Score);
                                    for (int i = score.Length; i >= 1; i--)
                                    {
                                        int Rest;
                                        Math.DivRem(score.Length - i, 4, out Rest);
                                        if (Rest == 0)
                                            score = score.Insert(i, " ");
                                    }
                                    spriteBatch.DrawString(Font, number + ".   " + HighScore.Name, new Vector2(250, 300 + number * 30), Color.White);
                                    spriteBatch.DrawString(Font, score, new Vector2(440, 300 + number * 30), Color.White);
                                    number++;
                                }
                                break;
                            case 4:
                                foreach (HighScore HighScore in HighScoresL4)
                                {
                                    string score = Convert.ToString(HighScore.Score);
                                    for (int i = score.Length; i >= 1; i--)
                                    {
                                        int Rest;
                                        Math.DivRem(score.Length - i, 4, out Rest);
                                        if (Rest == 0)
                                            score = score.Insert(i, " ");
                                    }
                                    spriteBatch.DrawString(Font, number + ".   " + HighScore.Name, new Vector2(250, 300 + number * 30), Color.White);
                                    spriteBatch.DrawString(Font, score, new Vector2(440, 300 + number * 30), Color.White);
                                    number++;
                                }
                                break;
                            case 5:
                                foreach (HighScore HighScore in HighScoresL5)
                                {
                                    string score = Convert.ToString(HighScore.Score);
                                    for (int i = score.Length; i >= 1; i--)
                                    {
                                        int Rest;
                                        Math.DivRem(score.Length - i, 4, out Rest);
                                        if (Rest == 0)
                                            score = score.Insert(i, " ");
                                    }
                                    spriteBatch.DrawString(Font, number + ".   " + HighScore.Name, new Vector2(250, 300 + number * 30), Color.White);
                                    spriteBatch.DrawString(Font, score, new Vector2(440, 300 + number * 30), Color.White);
                                    number++;
                                }
                                break;
                        }
                    }


                    //::::::::::::::::::::::::::::::::Drawing effects::::::::::::::::::::::::::::::::::::::::::
                    foreach (Classes.Effect Effect in Effects)
                    {
                        int lifePerc = (int)(Effect.Life * (100 / Effect.Duration));
                        if (lifePerc > 66)
                        {
                            spriteBatch.Draw(Effect.Sprite100, Effect.Position, null, Color.White * 0.075f * multiplier, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.9f);
                            spriteBatch.Draw(Effect.BWSprite100, Effect.Position, null, Color.White * (0.75f - 0.075f * multiplier), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.8f);
                        }
                        else if (lifePerc > 33)
                        {
                            spriteBatch.Draw(Effect.Sprite66, Effect.Position, null, Color.White * 0.075f * multiplier, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.9f);
                            spriteBatch.Draw(Effect.BWSprite66, Effect.Position, null, Color.White * (0.75f - 0.075f * multiplier), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.8f);
                        }
                        else
                        {
                            spriteBatch.Draw(Effect.Sprite33, Effect.Position, null, Color.White * 0.075f * multiplier, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.9f);
                            spriteBatch.Draw(Effect.BWSprite33, Effect.Position, null, Color.White * (0.75f - 0.075f * multiplier), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.8f);
                        }
                    }
                    spriteBatch.End();
                }
            }



            //-----------------------------Drawing buttons------------------------------------------------------
            spriteBatch.Begin();
            foreach (Button Button in ButtonList)
            {
                if (Button.Black)
                    spriteBatch.DrawString(Font, Button.Text, Button.Position, Color.Black * Button.Visibility);
                else
                    spriteBatch.DrawString(Font, Button.Text, Button.Position, Color.White * Button.Visibility);
            }
            spriteBatch.End();



            //-----------------------------Drawing credits------------------------------------------------------
            if (Credits)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(CreditsPic, new Vector2(0, MathHelper.Clamp(-CreditsPosition, -3196, 0)), Color.White);
                if (rand.Next(1, 100) == 1)
                    spriteBatch.Draw(CreditsPic, new Vector2(0 + rand.Next(-1, 2), MathHelper.Clamp(-CreditsPosition, -3196, 0) + rand.Next(-1, 2)), Color.White);
                spriteBatch.End();
            }



            //-----------------------------Drawing help screen------------------------------------------------------
            if (HelpScreen)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(HelpScreenPic, Vector2.Zero, Color.White);
                spriteBatch.End();
            }

            base.Draw(gameTime);
        }


        /// <summary>
        /// Sets up enemy spawns for each level and resets important
        /// numeric values.
        /// </summary>
        /// <param name="level"></param>
        protected void ChangeLevel(int level)
        {
            if (Player == null)
            {
                Player = new Player(new Vector2(300, 800), 10, 100, 100, 50, Ship_1, Ship_2, Ship_3);
                Player.Weapons.Add(new Weapon(new Vector2(Player.Position.X + 20, Player.Position.Y + 15), 0, 300, 20, 10, 1, 1, LaserProjectile, LaserProjectile_BW));
                Player.Weapons.Add(new Weapon(new Vector2(Player.Position.X + 130, Player.Position.Y + 15), 0, 300, 20, 10, 1, 1, LaserProjectile, LaserProjectile_BW));
                if (EquippedLeft != null)
                {
                    switch (EquippedLeft.Type)
                    {
                        case 1:
                            Player.Weapons[0] = new Weapon(new Vector2(Player.Position.X + 37, Player.Position.Y + 60), 0, EquippedLeft.FireRate, EquippedLeft.ProjectileSpeed, EquippedLeft.Damage, EquippedLeft.Projectiles, EquippedLeft.Type, LaserProjectile, LaserProjectile_BW);
                            break;
                        case 2:
                            Player.Weapons[0] = new Weapon(new Vector2(Player.Position.X + 37, Player.Position.Y + 60), 0, EquippedLeft.FireRate, EquippedLeft.ProjectileSpeed, EquippedLeft.Damage, EquippedLeft.Projectiles, EquippedLeft.Type, Rocket, Rocket_BW);
                            break;
                        case 3:
                            Player.Weapons[0] = new Weapon(new Vector2(Player.Position.X + 37, Player.Position.Y + 60), 0, EquippedLeft.FireRate, EquippedLeft.ProjectileSpeed, EquippedLeft.Damage, EquippedLeft.Projectiles, EquippedLeft.Type, Laser, Laser_BW);
                            break;
                        case 4:
                            Player.Weapons[0] = new Weapon(new Vector2(Player.Position.X + 37, Player.Position.Y + 60), 0, EquippedLeft.FireRate, EquippedLeft.ProjectileSpeed, EquippedLeft.Damage, EquippedLeft.Projectiles, EquippedLeft.Type, HomingProjectile, HomingProjectile_BW);
                            break;
                        case 5:
                            Player.Weapons[0] = new Weapon(new Vector2(Player.Position.X + 37, Player.Position.Y + 60), 0, EquippedLeft.FireRate, EquippedLeft.ProjectileSpeed, EquippedLeft.Damage, EquippedLeft.Projectiles, EquippedLeft.Type, null, null);
                            break;
                        case 6:
                            Player.Weapons[0] = new Weapon(new Vector2(Player.Position.X + 37, Player.Position.Y + 60), 0, EquippedLeft.FireRate, EquippedLeft.ProjectileSpeed, EquippedLeft.Damage, EquippedLeft.Projectiles, EquippedLeft.Type, null, null);
                            break;
                    }
                }
                else
                {
                    Player.Weapons[0] = (new Weapon(new Vector2(Player.Position.X + 20, Player.Position.Y + 15), 0, 300, 20, 10, 1, 1, LaserProjectile, LaserProjectile_BW));
                }
                if (EquippedRight != null)
                {
                    switch (EquippedRight.Type)
                    {
                        case 1:
                            Player.Weapons[1] = new Weapon(new Vector2(Player.Position.X + 113, Player.Position.Y + 60), 0, EquippedRight.FireRate, EquippedRight.ProjectileSpeed, EquippedRight.Damage, EquippedRight.Projectiles, EquippedRight.Type, LaserProjectile, LaserProjectile_BW);
                            break;
                        case 2:
                            Player.Weapons[1] = new Weapon(new Vector2(Player.Position.X + 113, Player.Position.Y + 60), 0, EquippedRight.FireRate, EquippedRight.ProjectileSpeed, EquippedRight.Damage, EquippedRight.Projectiles, EquippedRight.Type, Rocket, Rocket_BW);
                            break;
                        case 3:
                            Player.Weapons[1] = new Weapon(new Vector2(Player.Position.X + 113, Player.Position.Y + 60), 0, EquippedRight.FireRate, EquippedRight.ProjectileSpeed, EquippedRight.Damage, EquippedRight.Projectiles, EquippedRight.Type, Laser, Laser_BW);
                            break;
                        case 4:
                            Player.Weapons[1] = new Weapon(new Vector2(Player.Position.X + 113, Player.Position.Y + 60), 0, EquippedRight.FireRate, EquippedRight.ProjectileSpeed, EquippedRight.Damage, EquippedRight.Projectiles, EquippedRight.Type, HomingProjectile, HomingProjectile_BW);
                            break;
                        case 5:
                            Player.Weapons[1] = new Weapon(new Vector2(Player.Position.X + 113, Player.Position.Y + 60), 0, EquippedRight.FireRate, EquippedRight.ProjectileSpeed, EquippedRight.Damage, EquippedRight.Projectiles, EquippedRight.Type, null, null);
                            break;
                        case 6:
                            Player.Weapons[1] = new Weapon(new Vector2(Player.Position.X + 113, Player.Position.Y + 60), 0, EquippedRight.FireRate, EquippedRight.ProjectileSpeed, EquippedRight.Damage, EquippedRight.Projectiles, EquippedRight.Type, null, null);
                            break;
                    }
                }
                else
                {
                    Player.Weapons[1] = (new Weapon(new Vector2(Player.Position.X + 130, Player.Position.Y + 15), 0, 300, 20, 10, 1, 1, LaserProjectile, LaserProjectile_BW));
                }
            }
            Player.Health = 100;
            Player.Position = new Vector2(300, 800);
            Player.Weapons[0].Position = new Vector2(Player.Position.X + 37, Player.Position.Y + 60);
            Player.Weapons[1].Position = new Vector2(Player.Position.X + 113, Player.Position.Y + 60);
            Score = 0;
            multiplier = 0f;
            BeastModeTime = 0;

            Enemies = new List<Enemy>();
            EnemyAttacks = new List<Attack>();
            Attacks = new List<Attack>();
            Items = new List<Item>();

            switch (level)
            {
                case 1:
                    enemySpawner = new EnemySpawner();
                    enemySpawner.addSpawn(new Vector2(50, -50), 10, 3, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small_1, Enemy_Small_2, Enemy_Small_3, Enemy_Small_BW_1, Enemy_Small_BW_2, Enemy_Small_BW_3, 100, new TimeSpan(0, 0, 3), new TimeSpan(0, 0, 0, 0, 500), 4);
                    enemySpawner.addSpawn(new Vector2(150, -50), 10, 3, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small_1, Enemy_Small_2, Enemy_Small_3, Enemy_Small_BW_1, Enemy_Small_BW_2, Enemy_Small_BW_3, 100, new TimeSpan(0, 0, 6), new TimeSpan(0, 0, 0, 0, 500), 4);
                    enemySpawner.addSpawn(new Vector2(250, -50), 10, 3, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small_1, Enemy_Small_2, Enemy_Small_3, Enemy_Small_BW_1, Enemy_Small_BW_2, Enemy_Small_BW_3, 100, new TimeSpan(0, 0, 9), new TimeSpan(0, 0, 0, 0, 500), 4);
                    enemySpawner.addSpawn(new Vector2(350, -50), 10, 3, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small_1, Enemy_Small_2, Enemy_Small_3, Enemy_Small_BW_1, Enemy_Small_BW_2, Enemy_Small_BW_3, 100, new TimeSpan(0, 0, 12), new TimeSpan(0, 0, 0, 0, 500), 4);
                    enemySpawner.addSpawn(new Vector2(450, -50), 10, 3, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small_1, Enemy_Small_2, Enemy_Small_3, Enemy_Small_BW_1, Enemy_Small_BW_2, Enemy_Small_BW_3, 100, new TimeSpan(0, 0, 15), new TimeSpan(0, 0, 0, 0, 500), 4);
                    enemySpawner.addSpawn(new Vector2(550, -50), 10, 3, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small_1, Enemy_Small_2, Enemy_Small_3, Enemy_Small_BW_1, Enemy_Small_BW_2, Enemy_Small_BW_3, 100, new TimeSpan(0, 0, 18), new TimeSpan(0, 0, 0, 0, 500), 4);
                    enemySpawner.addSpawn(new Vector2(650, -50), 10, 3, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small_1, Enemy_Small_2, Enemy_Small_3, Enemy_Small_BW_1, Enemy_Small_BW_2, Enemy_Small_BW_3, 100, new TimeSpan(0, 0, 21), new TimeSpan(0, 0, 0, 0, 500), 4);

                    enemySpawner.addSpawn(new Vector2(-50, 250), 20, 2, 4, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small_1, Enemy_Small_2, Enemy_Small_3, Enemy_Small_BW_1, Enemy_Small_BW_2, Enemy_Small_BW_3, 5, new TimeSpan(0, 0, 25), new TimeSpan(0, 0, 0, 1, 0), 4);
                    enemySpawner.addSpawn(new Vector2(750, 150), 20, 2, 5, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small_1, Enemy_Small_2, Enemy_Small_3, Enemy_Small_BW_1, Enemy_Small_BW_2, Enemy_Small_BW_3, 5, new TimeSpan(0, 0, 29), new TimeSpan(0, 0, 0, 1, 0), 4);
                    enemySpawner.addSpawn(new Vector2(350, -50), 20, 3, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small_1, Enemy_Small_2, Enemy_Small_3, Enemy_Small_BW_1, Enemy_Small_BW_2, Enemy_Small_BW_3, 5, new TimeSpan(0, 0, 33), new TimeSpan(0, 0, 0, 0, 500), 9);
                    enemySpawner.addSpawn(new Vector2(-50, -50), 20, 3, 2, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small_1, Enemy_Small_2, Enemy_Small_3, Enemy_Small_BW_1, Enemy_Small_BW_2, Enemy_Small_BW_3, 5, new TimeSpan(0, 0, 35), new TimeSpan(0, 0, 0, 0, 500), 9);
                    enemySpawner.addSpawn(new Vector2(750, -50), 20, 3, 3, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small_1, Enemy_Small_2, Enemy_Small_3, Enemy_Small_BW_1, Enemy_Small_BW_2, Enemy_Small_BW_3, 5, new TimeSpan(0, 0, 35), new TimeSpan(0, 0, 0, 0, 500), 9);

                    enemySpawner.addSpawn(new Vector2(200, -75), 250, 2, 6, 1500, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium_BW, Enemy_Medium_1, Enemy_Medium_2, Enemy_Medium_3, Enemy_Medium_BW_1, Enemy_Medium_BW_2, Enemy_Medium_BW_3, 25, new TimeSpan(0, 0, 41), new TimeSpan(0, 0, 0, 0, 500), 0);
                    enemySpawner.addSpawn(new Vector2(500, -75), 250, 2, 6, 1500, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium_BW, Enemy_Medium_1, Enemy_Medium_2, Enemy_Medium_3, Enemy_Medium_BW_1, Enemy_Medium_BW_2, Enemy_Medium_BW_3, 25, new TimeSpan(0, 0, 43), new TimeSpan(0, 0, 0, 0, 500), 0);
                    enemySpawner.addSpawn(new Vector2(-75, 100), 150, 3, 2, 1000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium_BW, Enemy_Medium_1, Enemy_Medium_2, Enemy_Medium_3, Enemy_Medium_BW_1, Enemy_Medium_BW_2, Enemy_Medium_BW_3, 25, new TimeSpan(0, 0, 46), new TimeSpan(0, 0, 0, 1, 0), 4);
                    enemySpawner.addSpawn(new Vector2(750, 100), 150, 3, 3, 1000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium_BW, Enemy_Medium_1, Enemy_Medium_2, Enemy_Medium_3, Enemy_Medium_BW_1, Enemy_Medium_BW_2, Enemy_Medium_BW_3, 25, new TimeSpan(0, 0, 51), new TimeSpan(0, 0, 0, 1, 0), 4);

                    enemySpawner.addSpawn(new Vector2(325, -150), 1500, 1, 6, 2000, EnemyLaserProjectile_Large, EnemyLaserProjectile_Large_BW, Enemy_Large_1, Enemy_Large_2, Enemy_Large_3, Enemy_Large_BW_1, Enemy_Large_BW_2, Enemy_Large_BW_3, 150, new TimeSpan(0, 0, 58), new TimeSpan(0, 0, 0, 0, 500), 0);

                    enemySpawner.addSpawn(new Vector2(200, -75), 250, 2, 1, 1500, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium_BW, Enemy_Medium_1, Enemy_Medium_2, Enemy_Medium_3, Enemy_Medium_BW_1, Enemy_Medium_BW_2, Enemy_Medium_BW_3, 25, new TimeSpan(0, 1, 3), new TimeSpan(0, 0, 0, 2, 0), 9);
                    enemySpawner.addSpawn(new Vector2(475, -75), 250, 2, 1, 1500, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium_BW, Enemy_Medium_1, Enemy_Medium_2, Enemy_Medium_3, Enemy_Medium_BW_1, Enemy_Medium_BW_2, Enemy_Medium_BW_3, 25, new TimeSpan(0, 1, 3), new TimeSpan(0, 0, 0, 2, 0), 9);
                    enemySpawner.addSpawn(new Vector2(50, -50), 50, 3, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small_1, Enemy_Small_2, Enemy_Small_3, Enemy_Small_BW_1, Enemy_Small_BW_2, Enemy_Small_BW_3, 5, new TimeSpan(0, 1, 7), new TimeSpan(0, 0, 0, 0, 500), 24);
                    enemySpawner.addSpawn(new Vector2(350, -50), 50, 3, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small_1, Enemy_Small_2, Enemy_Small_3, Enemy_Small_BW_1, Enemy_Small_BW_2, Enemy_Small_BW_3, 5, new TimeSpan(0, 1, 7), new TimeSpan(0, 0, 0, 0, 500), 24);
                    enemySpawner.addSpawn(new Vector2(650, -50), 50, 3, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small_1, Enemy_Small_2, Enemy_Small_3, Enemy_Small_BW_1, Enemy_Small_BW_2, Enemy_Small_BW_3, 5, new TimeSpan(0, 1, 7), new TimeSpan(0, 0, 0, 0, 500), 24);

                    enemySpawner.addSpawn(new Vector2(50, -50), 20, 4, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small_1, Enemy_Small_2, Enemy_Small_3, Enemy_Small_BW_1, Enemy_Small_BW_2, Enemy_Small_BW_3, 3, new TimeSpan(0, 1, 28), new TimeSpan(0, 0, 0, 0, 200), 9);
                    enemySpawner.addSpawn(new Vector2(150, -50), 20, 4, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small_1, Enemy_Small_2, Enemy_Small_3, Enemy_Small_BW_1, Enemy_Small_BW_2, Enemy_Small_BW_3, 3, new TimeSpan(0, 1, 28), new TimeSpan(0, 0, 0, 0, 200), 9);
                    enemySpawner.addSpawn(new Vector2(250, -50), 20, 4, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small_1, Enemy_Small_2, Enemy_Small_3, Enemy_Small_BW_1, Enemy_Small_BW_2, Enemy_Small_BW_3, 3, new TimeSpan(0, 1, 28), new TimeSpan(0, 0, 0, 0, 200), 9);
                    enemySpawner.addSpawn(new Vector2(350, -50), 20, 4, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small_1, Enemy_Small_2, Enemy_Small_3, Enemy_Small_BW_1, Enemy_Small_BW_2, Enemy_Small_BW_3, 3, new TimeSpan(0, 1, 28), new TimeSpan(0, 0, 0, 0, 200), 9);
                    enemySpawner.addSpawn(new Vector2(450, -50), 20, 4, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small_1, Enemy_Small_2, Enemy_Small_3, Enemy_Small_BW_1, Enemy_Small_BW_2, Enemy_Small_BW_3, 3, new TimeSpan(0, 1, 28), new TimeSpan(0, 0, 0, 0, 200), 9);
                    enemySpawner.addSpawn(new Vector2(550, -50), 20, 4, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small_1, Enemy_Small_2, Enemy_Small_3, Enemy_Small_BW_1, Enemy_Small_BW_2, Enemy_Small_BW_3, 3, new TimeSpan(0, 1, 28), new TimeSpan(0, 0, 0, 0, 200), 9);
                    enemySpawner.addSpawn(new Vector2(650, -50), 20, 4, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small_1, Enemy_Small_2, Enemy_Small_3, Enemy_Small_BW_1, Enemy_Small_BW_2, Enemy_Small_BW_3, 3, new TimeSpan(0, 1, 28), new TimeSpan(0, 0, 0, 0, 200), 9);

                    enemySpawner.addSpawn(new Vector2(-75, 200), 500, 2, 4, 1500, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium_BW, Enemy_Medium_1, Enemy_Medium_2, Enemy_Medium_3, Enemy_Medium_BW_1, Enemy_Medium_BW_2, Enemy_Medium_BW_3, 25, new TimeSpan(0, 1, 32), new TimeSpan(0, 0, 0, 1, 0), 12);
                    enemySpawner.addSpawn(new Vector2(750, 100), 500, 2, 5, 1500, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium_BW, Enemy_Medium_1, Enemy_Medium_2, Enemy_Medium_3, Enemy_Medium_BW_1, Enemy_Medium_BW_2, Enemy_Medium_BW_3, 25, new TimeSpan(0, 1, 32), new TimeSpan(0, 0, 0, 1, 0), 12);
                    enemySpawner.addSpawn(new Vector2(-50, 300), 100, 2, 4, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small_1, Enemy_Small_2, Enemy_Small_3, Enemy_Small_BW_1, Enemy_Small_BW_2, Enemy_Small_BW_3, 5, new TimeSpan(0, 1, 35), new TimeSpan(0, 0, 0, 0, 500), 19);
                    enemySpawner.addSpawn(new Vector2(750, 375), 100, 2, 5, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small_1, Enemy_Small_2, Enemy_Small_3, Enemy_Small_BW_1, Enemy_Small_BW_2, Enemy_Small_BW_3, 5, new TimeSpan(0, 1, 35), new TimeSpan(0, 0, 0, 0, 500), 19);

                    enemySpawner.addSpawn(new Vector2(325, -150), 2500, 1, 6, 1500, EnemyLaserProjectile_Large, EnemyLaserProjectile_Large_BW, Enemy_Large_1, Enemy_Large_2, Enemy_Large_3, Enemy_Large_BW_1, Enemy_Large_BW_2, Enemy_Large_BW_3, 200, new TimeSpan(0, 1, 49), new TimeSpan(0, 0, 0, 5, 0), 2);
                    enemySpawner.addSpawn(new Vector2(50, -150), 2000, 1, 6, 1500, EnemyLaserProjectile_Large, EnemyLaserProjectile_Large_BW, Enemy_Large_1, Enemy_Large_2, Enemy_Large_3, Enemy_Large_BW_1, Enemy_Large_BW_2, Enemy_Large_BW_3, 175, new TimeSpan(0, 1, 55), new TimeSpan(0, 0, 0, 5, 0), 2);
                    enemySpawner.addSpawn(new Vector2(550, -150), 2000, 1, 6, 1500, EnemyLaserProjectile_Large, EnemyLaserProjectile_Large_BW, Enemy_Large_1, Enemy_Large_2, Enemy_Large_3, Enemy_Large_BW_1, Enemy_Large_BW_2, Enemy_Large_BW_3, 175, new TimeSpan(0, 1, 55), new TimeSpan(0, 0, 0, 5, 0), 2);

                    enemySpawner.addSpawn(new Vector2(325, -150), 1500, 2, 1, 1000, EnemyLaserProjectile_Large, EnemyLaserProjectile_Large_BW, Enemy_Large_1, Enemy_Large_2, Enemy_Large_3, Enemy_Large_BW_1, Enemy_Large_BW_2, Enemy_Large_BW_3, 150, new TimeSpan(0, 2, 15), new TimeSpan(0, 0, 0, 3, 0), 4);
                    enemySpawner.addSpawn(new Vector2(-75, -75), 500, 2, 2, 1500, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium_BW, Enemy_Medium_1, Enemy_Medium_2, Enemy_Medium_3, Enemy_Medium_BW_1, Enemy_Medium_BW_2, Enemy_Medium_BW_3, 25, new TimeSpan(0, 2, 15), new TimeSpan(0, 0, 0, 1, 0), 9);
                    enemySpawner.addSpawn(new Vector2(750, -75), 500, 2, 3, 1500, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium_BW, Enemy_Medium_1, Enemy_Medium_2, Enemy_Medium_3, Enemy_Medium_BW_1, Enemy_Medium_BW_2, Enemy_Medium_BW_3, 25, new TimeSpan(0, 2, 15), new TimeSpan(0, 0, 0, 1, 0), 9);
                    enemySpawner.addSpawn(new Vector2(-50, 300), 50, 3, 4, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small_1, Enemy_Small_2, Enemy_Small_3, Enemy_Small_BW_1, Enemy_Small_BW_2, Enemy_Small_BW_3, 3, new TimeSpan(0, 2, 16), new TimeSpan(0, 0, 0, 0, 500), 14);
                    enemySpawner.addSpawn(new Vector2(750, 300), 50, 3, 5, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small_1, Enemy_Small_2, Enemy_Small_3, Enemy_Small_BW_1, Enemy_Small_BW_2, Enemy_Small_BW_3, 3, new TimeSpan(0, 2, 16), new TimeSpan(0, 0, 0, 0, 500), 14);

                    enemySpawner.addSpawn(new Vector2(100, -150), 3500, 1, 1, 500, EnemyLaserProjectile_Large, EnemyLaserProjectile_Large_BW, Enemy_Large_1, Enemy_Large_2, Enemy_Large_3, Enemy_Large_BW_1, Enemy_Large_BW_2, Enemy_Large_BW_3, 200, new TimeSpan(0, 2, 35), new TimeSpan(0, 0, 0, 10, 0), 4);
                    enemySpawner.addSpawn(new Vector2(500, -150), 3500, 1, 1, 500, EnemyLaserProjectile_Large, EnemyLaserProjectile_Large_BW, Enemy_Large_1, Enemy_Large_2, Enemy_Large_3, Enemy_Large_BW_1, Enemy_Large_BW_2, Enemy_Large_BW_3, 200, new TimeSpan(0, 2, 40), new TimeSpan(0, 0, 0, 10, 0), 4);
                    enemySpawner.addSpawn(new Vector2(275, -75), 750, 3, 1, 1000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium_BW, Enemy_Medium_1, Enemy_Medium_2, Enemy_Medium_3, Enemy_Medium_BW_1, Enemy_Medium_BW_2, Enemy_Medium_BW_3, 25, new TimeSpan(0, 2, 37), new TimeSpan(0, 0, 0, 10, 0), 4);
                    enemySpawner.addSpawn(new Vector2(400, -75), 750, 3, 1, 1000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium_BW, Enemy_Medium_1, Enemy_Medium_2, Enemy_Medium_3, Enemy_Medium_BW_1, Enemy_Medium_BW_2, Enemy_Medium_BW_3, 25, new TimeSpan(0, 2, 37), new TimeSpan(0, 0, 0, 10, 0), 4);
                    enemySpawner.addSpawn(new Vector2(275, -75), 750, 3, 1, 1000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium_BW, Enemy_Medium_1, Enemy_Medium_2, Enemy_Medium_3, Enemy_Medium_BW_1, Enemy_Medium_BW_2, Enemy_Medium_BW_3, 25, new TimeSpan(0, 2, 38), new TimeSpan(0, 0, 0, 10, 0), 4);
                    enemySpawner.addSpawn(new Vector2(400, -75), 750, 3, 1, 1000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium_BW, Enemy_Medium_1, Enemy_Medium_2, Enemy_Medium_3, Enemy_Medium_BW_1, Enemy_Medium_BW_2, Enemy_Medium_BW_3, 25, new TimeSpan(0, 2, 38), new TimeSpan(0, 0, 0, 10, 0), 4);
                    enemySpawner.addSpawn(new Vector2(275, -75), 750, 3, 1, 1000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium_BW, Enemy_Medium_1, Enemy_Medium_2, Enemy_Medium_3, Enemy_Medium_BW_1, Enemy_Medium_BW_2, Enemy_Medium_BW_3, 25, new TimeSpan(0, 2, 39), new TimeSpan(0, 0, 0, 10, 0), 4);
                    enemySpawner.addSpawn(new Vector2(400, -75), 750, 3, 1, 1000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium_BW, Enemy_Medium_1, Enemy_Medium_2, Enemy_Medium_3, Enemy_Medium_BW_1, Enemy_Medium_BW_2, Enemy_Medium_BW_3, 25, new TimeSpan(0, 2, 39), new TimeSpan(0, 0, 0, 10, 0), 4);
                    enemySpawner.addSpawn(new Vector2(325, -50), 100, 4, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small_1, Enemy_Small_2, Enemy_Small_3, Enemy_Small_BW_1, Enemy_Small_BW_2, Enemy_Small_BW_3, 2, new TimeSpan(0, 0, 2, 42, 0), new TimeSpan(0, 0, 0, 10, 0), 4);
                    enemySpawner.addSpawn(new Vector2(375, -50), 100, 4, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small_1, Enemy_Small_2, Enemy_Small_3, Enemy_Small_BW_1, Enemy_Small_BW_2, Enemy_Small_BW_3, 2, new TimeSpan(0, 0, 2, 42, 0), new TimeSpan(0, 0, 0, 10, 0), 4);
                    enemySpawner.addSpawn(new Vector2(275, -50), 100, 4, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small_1, Enemy_Small_2, Enemy_Small_3, Enemy_Small_BW_1, Enemy_Small_BW_2, Enemy_Small_BW_3, 2, new TimeSpan(0, 0, 2, 42, 250), new TimeSpan(0, 0, 0, 10, 0), 4);
                    enemySpawner.addSpawn(new Vector2(325, -50), 100, 4, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small_1, Enemy_Small_2, Enemy_Small_3, Enemy_Small_BW_1, Enemy_Small_BW_2, Enemy_Small_BW_3, 2, new TimeSpan(0, 0, 2, 42, 250), new TimeSpan(0, 0, 0, 10, 0), 4);
                    enemySpawner.addSpawn(new Vector2(375, -50), 100, 4, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small_1, Enemy_Small_2, Enemy_Small_3, Enemy_Small_BW_1, Enemy_Small_BW_2, Enemy_Small_BW_3, 2, new TimeSpan(0, 0, 2, 42, 250), new TimeSpan(0, 0, 0, 10, 0), 4);
                    enemySpawner.addSpawn(new Vector2(425, -50), 100, 4, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small_1, Enemy_Small_2, Enemy_Small_3, Enemy_Small_BW_1, Enemy_Small_BW_2, Enemy_Small_BW_3, 2, new TimeSpan(0, 0, 2, 42, 250), new TimeSpan(0, 0, 0, 10, 0), 4);
                    enemySpawner.addSpawn(new Vector2(275, -50), 100, 4, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small_1, Enemy_Small_2, Enemy_Small_3, Enemy_Small_BW_1, Enemy_Small_BW_2, Enemy_Small_BW_3, 2, new TimeSpan(0, 0, 2, 42, 500), new TimeSpan(0, 0, 0, 10, 0), 4);
                    enemySpawner.addSpawn(new Vector2(325, -50), 100, 4, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small_1, Enemy_Small_2, Enemy_Small_3, Enemy_Small_BW_1, Enemy_Small_BW_2, Enemy_Small_BW_3, 2, new TimeSpan(0, 0, 2, 42, 500), new TimeSpan(0, 0, 0, 10, 0), 4);
                    enemySpawner.addSpawn(new Vector2(375, -50), 100, 4, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small_1, Enemy_Small_2, Enemy_Small_3, Enemy_Small_BW_1, Enemy_Small_BW_2, Enemy_Small_BW_3, 2, new TimeSpan(0, 0, 2, 42, 500), new TimeSpan(0, 0, 0, 10, 0), 4);
                    enemySpawner.addSpawn(new Vector2(425, -50), 100, 4, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small_1, Enemy_Small_2, Enemy_Small_3, Enemy_Small_BW_1, Enemy_Small_BW_2, Enemy_Small_BW_3, 2, new TimeSpan(0, 0, 2, 42, 500), new TimeSpan(0, 0, 0, 10, 0), 4);
                    enemySpawner.addSpawn(new Vector2(275, -50), 100, 4, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small_1, Enemy_Small_2, Enemy_Small_3, Enemy_Small_BW_1, Enemy_Small_BW_2, Enemy_Small_BW_3, 2, new TimeSpan(0, 0, 2, 42, 750), new TimeSpan(0, 0, 0, 10, 0), 4);
                    enemySpawner.addSpawn(new Vector2(325, -50), 100, 4, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small_1, Enemy_Small_2, Enemy_Small_3, Enemy_Small_BW_1, Enemy_Small_BW_2, Enemy_Small_BW_3, 2, new TimeSpan(0, 0, 2, 42, 750), new TimeSpan(0, 0, 0, 10, 0), 4);
                    enemySpawner.addSpawn(new Vector2(375, -50), 100, 4, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small_1, Enemy_Small_2, Enemy_Small_3, Enemy_Small_BW_1, Enemy_Small_BW_2, Enemy_Small_BW_3, 2, new TimeSpan(0, 0, 2, 42, 750), new TimeSpan(0, 0, 0, 10, 0), 4);
                    enemySpawner.addSpawn(new Vector2(425, -50), 100, 4, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small_1, Enemy_Small_2, Enemy_Small_3, Enemy_Small_BW_1, Enemy_Small_BW_2, Enemy_Small_BW_3, 2, new TimeSpan(0, 0, 2, 42, 750), new TimeSpan(0, 0, 0, 10, 0), 4);
                    enemySpawner.addSpawn(new Vector2(275, -50), 100, 4, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small_1, Enemy_Small_2, Enemy_Small_3, Enemy_Small_BW_1, Enemy_Small_BW_2, Enemy_Small_BW_3, 2, new TimeSpan(0, 0, 2, 43, 0), new TimeSpan(0, 0, 0, 10, 0), 4);
                    enemySpawner.addSpawn(new Vector2(325, -50), 100, 4, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small_1, Enemy_Small_2, Enemy_Small_3, Enemy_Small_BW_1, Enemy_Small_BW_2, Enemy_Small_BW_3, 2, new TimeSpan(0, 0, 2, 43, 0), new TimeSpan(0, 0, 0, 10, 0), 4);
                    enemySpawner.addSpawn(new Vector2(375, -50), 100, 4, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small_1, Enemy_Small_2, Enemy_Small_3, Enemy_Small_BW_1, Enemy_Small_BW_2, Enemy_Small_BW_3, 2, new TimeSpan(0, 0, 2, 43, 0), new TimeSpan(0, 0, 0, 10, 0), 4);
                    enemySpawner.addSpawn(new Vector2(425, -50), 100, 4, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small_1, Enemy_Small_2, Enemy_Small_3, Enemy_Small_BW_1, Enemy_Small_BW_2, Enemy_Small_BW_3, 2, new TimeSpan(0, 0, 2, 43, 0), new TimeSpan(0, 0, 0, 10, 0), 4);
                    enemySpawner.addSpawn(new Vector2(275, -50), 100, 4, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small_1, Enemy_Small_2, Enemy_Small_3, Enemy_Small_BW_1, Enemy_Small_BW_2, Enemy_Small_BW_3, 2, new TimeSpan(0, 0, 2, 43, 250), new TimeSpan(0, 0, 0, 10, 0), 4);
                    enemySpawner.addSpawn(new Vector2(325, -50), 100, 4, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small_1, Enemy_Small_2, Enemy_Small_3, Enemy_Small_BW_1, Enemy_Small_BW_2, Enemy_Small_BW_3, 2, new TimeSpan(0, 0, 2, 43, 250), new TimeSpan(0, 0, 0, 10, 0), 4);
                    enemySpawner.addSpawn(new Vector2(375, -50), 100, 4, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small_1, Enemy_Small_2, Enemy_Small_3, Enemy_Small_BW_1, Enemy_Small_BW_2, Enemy_Small_BW_3, 2, new TimeSpan(0, 0, 2, 43, 250), new TimeSpan(0, 0, 0, 10, 0), 4);
                    enemySpawner.addSpawn(new Vector2(425, -50), 100, 4, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small_1, Enemy_Small_2, Enemy_Small_3, Enemy_Small_BW_1, Enemy_Small_BW_2, Enemy_Small_BW_3, 2, new TimeSpan(0, 0, 2, 43, 250), new TimeSpan(0, 0, 0, 10, 0), 4);
                    enemySpawner.addSpawn(new Vector2(275, -50), 100, 4, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small_1, Enemy_Small_2, Enemy_Small_3, Enemy_Small_BW_1, Enemy_Small_BW_2, Enemy_Small_BW_3, 2, new TimeSpan(0, 0, 2, 43, 500), new TimeSpan(0, 0, 0, 10, 0), 4);
                    enemySpawner.addSpawn(new Vector2(325, -50), 100, 4, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small_1, Enemy_Small_2, Enemy_Small_3, Enemy_Small_BW_1, Enemy_Small_BW_2, Enemy_Small_BW_3, 2, new TimeSpan(0, 0, 2, 43, 500), new TimeSpan(0, 0, 0, 10, 0), 4);
                    enemySpawner.addSpawn(new Vector2(375, -50), 100, 4, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small_1, Enemy_Small_2, Enemy_Small_3, Enemy_Small_BW_1, Enemy_Small_BW_2, Enemy_Small_BW_3, 2, new TimeSpan(0, 0, 2, 43, 500), new TimeSpan(0, 0, 0, 10, 0), 4);
                    enemySpawner.addSpawn(new Vector2(425, -50), 100, 4, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small_1, Enemy_Small_2, Enemy_Small_3, Enemy_Small_BW_1, Enemy_Small_BW_2, Enemy_Small_BW_3, 2, new TimeSpan(0, 0, 2, 43, 500), new TimeSpan(0, 0, 0, 10, 0), 4);
                    enemySpawner.addSpawn(new Vector2(275, -50), 100, 4, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small_1, Enemy_Small_2, Enemy_Small_3, Enemy_Small_BW_1, Enemy_Small_BW_2, Enemy_Small_BW_3, 2, new TimeSpan(0, 0, 2, 43, 750), new TimeSpan(0, 0, 0, 10, 0), 4);
                    enemySpawner.addSpawn(new Vector2(325, -50), 100, 4, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small_1, Enemy_Small_2, Enemy_Small_3, Enemy_Small_BW_1, Enemy_Small_BW_2, Enemy_Small_BW_3, 2, new TimeSpan(0, 0, 2, 43, 750), new TimeSpan(0, 0, 0, 10, 0), 4);
                    enemySpawner.addSpawn(new Vector2(375, -50), 100, 4, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small_1, Enemy_Small_2, Enemy_Small_3, Enemy_Small_BW_1, Enemy_Small_BW_2, Enemy_Small_BW_3, 2, new TimeSpan(0, 0, 2, 43, 750), new TimeSpan(0, 0, 0, 10, 0), 4);
                    enemySpawner.addSpawn(new Vector2(425, -50), 100, 4, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small_1, Enemy_Small_2, Enemy_Small_3, Enemy_Small_BW_1, Enemy_Small_BW_2, Enemy_Small_BW_3, 2, new TimeSpan(0, 0, 2, 43, 750), new TimeSpan(0, 0, 0, 10, 0), 4);
                    enemySpawner.addSpawn(new Vector2(275, -50), 100, 4, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small_1, Enemy_Small_2, Enemy_Small_3, Enemy_Small_BW_1, Enemy_Small_BW_2, Enemy_Small_BW_3, 2, new TimeSpan(0, 0, 2, 44, 0), new TimeSpan(0, 0, 0, 10, 0), 4);
                    enemySpawner.addSpawn(new Vector2(325, -50), 100, 4, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small_1, Enemy_Small_2, Enemy_Small_3, Enemy_Small_BW_1, Enemy_Small_BW_2, Enemy_Small_BW_3, 2, new TimeSpan(0, 0, 2, 44, 0), new TimeSpan(0, 0, 0, 10, 0), 4);
                    enemySpawner.addSpawn(new Vector2(375, -50), 100, 4, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small_1, Enemy_Small_2, Enemy_Small_3, Enemy_Small_BW_1, Enemy_Small_BW_2, Enemy_Small_BW_3, 2, new TimeSpan(0, 0, 2, 44, 0), new TimeSpan(0, 0, 0, 10, 0), 4);
                    enemySpawner.addSpawn(new Vector2(425, -50), 100, 4, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small_1, Enemy_Small_2, Enemy_Small_3, Enemy_Small_BW_1, Enemy_Small_BW_2, Enemy_Small_BW_3, 2, new TimeSpan(0, 0, 2, 44, 0), new TimeSpan(0, 0, 0, 10, 0), 4);
                    

                    Boss = new Boss(new Vector2(120, -200), 100000, Boss1_1, Boss1_2, Boss1_3, Boss1_BW_1, Boss1_BW_2, Boss1_BW_3, new TimeSpan(0, 3, 30));
                    Boss.Weapons.Add(new Weapon(new Vector2(178, -57), MathHelper.ToRadians(195), 15, 20, 10, 3, 3, Laser, Laser_BW));
                    Boss.Weapons.Add(new Weapon(new Vector2(565, -57), MathHelper.ToRadians(165), 15, 20, 10, 3, 3, Laser, Laser_BW));
                    Boss.Weapons.Add(new Weapon(new Vector2(363, -62), MathHelper.ToRadians(150), 50, 5, 10, 3, 1, LaserProjectile, LaserProjectile_BW));
                    Boss.addEvent(1, TimeSpan.Zero, 0, 220, 100, 0);
                    Boss.addEvent(3, 50000, 1, 1, 1, 0);
                    Boss.addEvent(3, 50000, 2, 1, 1, 0);
                    Boss.addEvent(3, new TimeSpan(0, 0, 3), 3, 1, 103, -1);
                    Boss.addEvent(3, new TimeSpan(0, 0, 4), 3, 0, 103, -1);
                    Boss.addEvent(2, TimeSpan.Zero, 3, 60, 20, -1);
                    break;


                case 2:
                    enemySpawner = new EnemySpawner();
                    enemySpawner.addSpawn(new Vector2(325, -50), 10, 3, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small_1, Enemy_Small_2, Enemy_Small_3, Enemy_Small_BW_1, Enemy_Small_BW_2, Enemy_Small_BW_3, 70, new TimeSpan(0, 0, 3), new TimeSpan(0, 0, 0, 0, 250), 9);
                    enemySpawner.addSpawn(new Vector2(375, -50), 10, 3, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small_1, Enemy_Small_2, Enemy_Small_3, Enemy_Small_BW_1, Enemy_Small_BW_2, Enemy_Small_BW_3, 70, new TimeSpan(0, 0, 3), new TimeSpan(0, 0, 0, 0, 250), 9);
                    enemySpawner.addSpawn(new Vector2(75, -50), 10, 3, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small_1, Enemy_Small_2, Enemy_Small_3, Enemy_Small_BW_1, Enemy_Small_BW_2, Enemy_Small_BW_3, 70, new TimeSpan(0, 0, 8), new TimeSpan(0, 0, 0, 0, 250), 9);
                    enemySpawner.addSpawn(new Vector2(125, -50), 10, 3, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small_1, Enemy_Small_2, Enemy_Small_3, Enemy_Small_BW_1, Enemy_Small_BW_2, Enemy_Small_BW_3, 70, new TimeSpan(0, 0, 8), new TimeSpan(0, 0, 0, 0, 250), 9);
                    enemySpawner.addSpawn(new Vector2(625, -50), 10, 3, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small_1, Enemy_Small_2, Enemy_Small_3, Enemy_Small_BW_1, Enemy_Small_BW_2, Enemy_Small_BW_3, 70, new TimeSpan(0, 0, 13), new TimeSpan(0, 0, 0, 0, 250), 9);
                    enemySpawner.addSpawn(new Vector2(675, -50), 10, 3, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small_1, Enemy_Small_2, Enemy_Small_3, Enemy_Small_BW_1, Enemy_Small_BW_2, Enemy_Small_BW_3, 70, new TimeSpan(0, 0, 13), new TimeSpan(0, 0, 0, 0, 250), 9);

                    enemySpawner.addSpawn(new Vector2(750, 250), 50, 3, 5, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small2_1, Enemy_Small2_2, Enemy_Small2_3, Enemy_Small2_BW_1, Enemy_Small2_BW_2, Enemy_Small2_BW_3, 3, new TimeSpan(0, 0, 18), new TimeSpan(0, 0, 0, 0, 500), 9);
                    enemySpawner.addSpawn(new Vector2(750, -50), 50, 3, 3, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small2_1, Enemy_Small2_2, Enemy_Small2_3, Enemy_Small2_BW_1, Enemy_Small2_BW_2, Enemy_Small2_BW_3, 3, new TimeSpan(0, 0, 22), new TimeSpan(0, 0, 0, 0, 500), 9);
                    enemySpawner.addSpawn(new Vector2(-75, 250), 50, 3, 4, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small2_1, Enemy_Small2_2, Enemy_Small2_3, Enemy_Small2_BW_1, Enemy_Small2_BW_2, Enemy_Small2_BW_3, 3, new TimeSpan(0, 0, 26), new TimeSpan(0, 0, 0, 0, 500), 9);
                    enemySpawner.addSpawn(new Vector2(-75, -50), 50, 3, 2, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small2_1, Enemy_Small2_2, Enemy_Small2_3, Enemy_Small2_BW_1, Enemy_Small2_BW_2, Enemy_Small2_BW_3, 3, new TimeSpan(0, 0, 30), new TimeSpan(0, 0, 0, 0, 500), 9);
                    enemySpawner.addSpawn(new Vector2(290, -50), 50, 3, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small2_1, Enemy_Small2_2, Enemy_Small2_3, Enemy_Small2_BW_1, Enemy_Small2_BW_2, Enemy_Small2_BW_3, 3, new TimeSpan(0, 0, 34), new TimeSpan(0, 0, 0, 0, 500), 9);
                    enemySpawner.addSpawn(new Vector2(385, -50), 50, 3, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small2_1, Enemy_Small2_2, Enemy_Small2_3, Enemy_Small2_BW_1, Enemy_Small2_BW_2, Enemy_Small2_BW_3, 3, new TimeSpan(0, 0, 34), new TimeSpan(0, 0, 0, 0, 500), 9);

                    enemySpawner.addSpawn(new Vector2(325, -200), 2700, 1, 1, 15000, EnemyLaserProjectile_Large, EnemyLaserProjectile_Large, Enemy_Large2_1, Enemy_Large2_2, Enemy_Large2_3, Enemy_Large2_BW_1, Enemy_Large2_BW_2, Enemy_Large2_BW_3, 150, new TimeSpan(0, 0, 40), new TimeSpan(0, 0, 0, 4, 0), 4);
                    enemySpawner.addSpawn(new Vector2(225, -200), 3500, 1, 1, 15000, EnemyLaserProjectile_Large, EnemyLaserProjectile_Large, Enemy_Large2_1, Enemy_Large2_2, Enemy_Large2_3, Enemy_Large2_BW_1, Enemy_Large2_BW_2, Enemy_Large2_BW_3, 200, new TimeSpan(0, 1, 3), new TimeSpan(0, 0, 0, 10, 0), 2);
                    enemySpawner.addSpawn(new Vector2(425, -200), 3500, 1, 1, 15000, EnemyLaserProjectile_Large, EnemyLaserProjectile_Large, Enemy_Large2_1, Enemy_Large2_2, Enemy_Large2_3, Enemy_Large2_BW_1, Enemy_Large2_BW_2, Enemy_Large2_BW_3, 200, new TimeSpan(0, 1, 8), new TimeSpan(0, 0, 0, 10, 0), 2);
                    enemySpawner.addSpawn(new Vector2(325, -100), 500, 3, 1, 1000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium_BW, Enemy_Medium2_1, Enemy_Medium2_2, Enemy_Medium2_3, Enemy_Medium2_BW_1, Enemy_Medium2_BW_2, Enemy_Medium2_BW_3, 50, new TimeSpan(0, 1, 5), new TimeSpan(0, 0, 0, 2, 0), 14);
                    enemySpawner.addSpawn(new Vector2(100, -100), 500, 3, 1, 1000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium_BW, Enemy_Medium2_1, Enemy_Medium2_2, Enemy_Medium2_3, Enemy_Medium2_BW_1, Enemy_Medium2_BW_2, Enemy_Medium2_BW_3, 50, new TimeSpan(0, 1, 9), new TimeSpan(0, 0, 0, 2, 0), 12);
                    enemySpawner.addSpawn(new Vector2(550, -100), 500, 3, 1, 1000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium_BW, Enemy_Medium2_1, Enemy_Medium2_2, Enemy_Medium2_3, Enemy_Medium2_BW_1, Enemy_Medium2_BW_2, Enemy_Medium2_BW_3, 50, new TimeSpan(0, 1, 9), new TimeSpan(0, 0, 0, 2, 0), 12);

                    enemySpawner.addSpawn(new Vector2(325, -200), 7500, 1, 1, 15000, EnemyLaserProjectile_Large, EnemyLaserProjectile_Large_BW, Enemy_Large2_1, Enemy_Large2_2, Enemy_Large2_3, Enemy_Large2_BW_1, Enemy_Large2_BW_2, Enemy_Large2_BW_3, 350, new TimeSpan(0, 1, 40), new TimeSpan(0, 0, 0, 5, 0), 3);
                    enemySpawner.addSpawn(new Vector2(350, 100), 75, 3, 3, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small_1, Enemy_Small_2, Enemy_Small_3, Enemy_Small_BW_1, Enemy_Small_BW_2, Enemy_Small_BW_3, 2, new TimeSpan(0, 1, 43), new TimeSpan(0, 0, 0, 0, 500), 2);
                    enemySpawner.addSpawn(new Vector2(350, 75), 75, 3, 2, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small_1, Enemy_Small_2, Enemy_Small_3, Enemy_Small_BW_1, Enemy_Small_BW_2, Enemy_Small_BW_3, 2, new TimeSpan(0, 1, 43), new TimeSpan(0, 0, 0, 0, 500), 2);
                    enemySpawner.addSpawn(new Vector2(350, 100), 75, 3, 3, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small_1, Enemy_Small_2, Enemy_Small_3, Enemy_Small_BW_1, Enemy_Small_BW_2, Enemy_Small_BW_3, 2, new TimeSpan(0, 1, 48), new TimeSpan(0, 0, 0, 0, 500), 2);
                    enemySpawner.addSpawn(new Vector2(350, 75), 75, 3, 2, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small_1, Enemy_Small_2, Enemy_Small_3, Enemy_Small_BW_1, Enemy_Small_BW_2, Enemy_Small_BW_3, 2, new TimeSpan(0, 1, 48), new TimeSpan(0, 0, 0, 0, 500), 2);
                    enemySpawner.addSpawn(new Vector2(350, 100), 75, 3, 3, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small_1, Enemy_Small_2, Enemy_Small_3, Enemy_Small_BW_1, Enemy_Small_BW_2, Enemy_Small_BW_3, 2, new TimeSpan(0, 1, 53), new TimeSpan(0, 0, 0, 0, 500), 2);
                    enemySpawner.addSpawn(new Vector2(350, 75), 75, 3, 2, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small_1, Enemy_Small_2, Enemy_Small_3, Enemy_Small_BW_1, Enemy_Small_BW_2, Enemy_Small_BW_3, 2, new TimeSpan(0, 1, 53), new TimeSpan(0, 0, 0, 0, 500), 2);
                    enemySpawner.addSpawn(new Vector2(350, 100), 75, 3, 3, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small_1, Enemy_Small_2, Enemy_Small_3, Enemy_Small_BW_1, Enemy_Small_BW_2, Enemy_Small_BW_3, 2, new TimeSpan(0, 1, 58), new TimeSpan(0, 0, 0, 0, 500), 2);
                    enemySpawner.addSpawn(new Vector2(350, 75), 75, 3, 2, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small_1, Enemy_Small_2, Enemy_Small_3, Enemy_Small_BW_1, Enemy_Small_BW_2, Enemy_Small_BW_3, 2, new TimeSpan(0, 1, 58), new TimeSpan(0, 0, 0, 0, 500), 2);

                    enemySpawner.addSpawn(new Vector2(200, -50), 150, 4, 1, 1500, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium_BW, Enemy_Small2_1, Enemy_Small2_2, Enemy_Small2_3, Enemy_Small2_BW_1, Enemy_Small2_BW_2, Enemy_Small2_BW_3, 3, new TimeSpan(0, 2, 5), new TimeSpan(0, 0, 0, 0, 500), 19);
                    enemySpawner.addSpawn(new Vector2(475, -50), 150, 4, 1, 1500, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium_BW, Enemy_Small2_1, Enemy_Small2_2, Enemy_Small2_3, Enemy_Small2_BW_1, Enemy_Small2_BW_2, Enemy_Small2_BW_3, 3, new TimeSpan(0, 2, 5), new TimeSpan(0, 0, 0, 0, 500), 19);
                    enemySpawner.addSpawn(new Vector2(337, -50), 150, 4, 1, 1500, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium_BW, Enemy_Small2_1, Enemy_Small2_2, Enemy_Small2_3, Enemy_Small2_BW_1, Enemy_Small2_BW_2, Enemy_Small2_BW_3, 3, new TimeSpan(0, 2, 8), new TimeSpan(0, 0, 0, 0, 500), 14);
                    enemySpawner.addSpawn(new Vector2(100, -50), 150, 4, 1, 1500, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium_BW, Enemy_Small2_1, Enemy_Small2_2, Enemy_Small2_3, Enemy_Small2_BW_1, Enemy_Small2_BW_2, Enemy_Small2_BW_3, 3, new TimeSpan(0, 2, 8), new TimeSpan(0, 0, 0, 0, 500), 14);
                    enemySpawner.addSpawn(new Vector2(613, -50), 150, 4, 1, 1500, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium_BW, Enemy_Small2_1, Enemy_Small2_2, Enemy_Small2_3, Enemy_Small2_BW_1, Enemy_Small2_BW_2, Enemy_Small2_BW_3, 3, new TimeSpan(0, 2, 8), new TimeSpan(0, 0, 0, 0, 500), 14);
                    enemySpawner.addSpawn(new Vector2(-75, -50), 150, 4, 2, 1500, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium_BW, Enemy_Small2_1, Enemy_Small2_2, Enemy_Small2_3, Enemy_Small2_BW_1, Enemy_Small2_BW_2, Enemy_Small2_BW_3, 3, new TimeSpan(0, 2, 11), new TimeSpan(0, 0, 0, 0, 500), 9);
                    enemySpawner.addSpawn(new Vector2(750, -50), 150, 4, 3, 1500, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium_BW, Enemy_Small2_1, Enemy_Small2_2, Enemy_Small2_3, Enemy_Small2_BW_1, Enemy_Small2_BW_2, Enemy_Small2_BW_3, 3, new TimeSpan(0, 2, 11), new TimeSpan(0, 0, 0, 0, 500), 9);
                    enemySpawner.addSpawn(new Vector2(-75, 350), 150, 4, 4, 1500, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium_BW, Enemy_Small2_1, Enemy_Small2_2, Enemy_Small2_3, Enemy_Small2_BW_1, Enemy_Small2_BW_2, Enemy_Small2_BW_3, 3, new TimeSpan(0, 2, 14), new TimeSpan(0, 0, 0, 0, 500), 4);
                    enemySpawner.addSpawn(new Vector2(750, 350), 150, 4, 5, 1500, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium_BW, Enemy_Small2_1, Enemy_Small2_2, Enemy_Small2_3, Enemy_Small2_BW_1, Enemy_Small2_BW_2, Enemy_Small2_BW_3, 3, new TimeSpan(0, 2, 14), new TimeSpan(0, 0, 0, 0, 500), 4);

                    enemySpawner.addSpawn(new Vector2(25, -100), 1000, 3, 1, 1000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium_BW, Enemy_Medium2_1, Enemy_Medium2_2, Enemy_Medium2_3, Enemy_Medium2_BW_1, Enemy_Medium2_BW_2, Enemy_Medium2_BW_3, 60, new TimeSpan(0, 2, 19), new TimeSpan(0, 0, 0, 5, 0), 1);
                    enemySpawner.addSpawn(new Vector2(125, -100), 1000, 3, 1, 1000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium_BW, Enemy_Medium2_1, Enemy_Medium2_2, Enemy_Medium2_3, Enemy_Medium2_BW_1, Enemy_Medium2_BW_2, Enemy_Medium2_BW_3, 60, new TimeSpan(0, 2, 19), new TimeSpan(0, 0, 0, 5, 0), 1);
                    enemySpawner.addSpawn(new Vector2(225, -100), 1000, 3, 1, 1000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium_BW, Enemy_Medium2_1, Enemy_Medium2_2, Enemy_Medium2_3, Enemy_Medium2_BW_1, Enemy_Medium2_BW_2, Enemy_Medium2_BW_3, 60, new TimeSpan(0, 2, 19), new TimeSpan(0, 0, 0, 5, 0), 1);
                    enemySpawner.addSpawn(new Vector2(325, -100), 1000, 3, 1, 1000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium_BW, Enemy_Medium2_1, Enemy_Medium2_2, Enemy_Medium2_3, Enemy_Medium2_BW_1, Enemy_Medium2_BW_2, Enemy_Medium2_BW_3, 60, new TimeSpan(0, 2, 19), new TimeSpan(0, 0, 0, 5, 0), 1);
                    enemySpawner.addSpawn(new Vector2(425, -100), 1000, 3, 1, 1000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium_BW, Enemy_Medium2_1, Enemy_Medium2_2, Enemy_Medium2_3, Enemy_Medium2_BW_1, Enemy_Medium2_BW_2, Enemy_Medium2_BW_3, 60, new TimeSpan(0, 2, 19), new TimeSpan(0, 0, 0, 5, 0), 1);
                    enemySpawner.addSpawn(new Vector2(525, -100), 1000, 3, 1, 1000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium_BW, Enemy_Medium2_1, Enemy_Medium2_2, Enemy_Medium2_3, Enemy_Medium2_BW_1, Enemy_Medium2_BW_2, Enemy_Medium2_BW_3, 60, new TimeSpan(0, 2, 19), new TimeSpan(0, 0, 0, 5, 0), 1);
                    enemySpawner.addSpawn(new Vector2(625, -100), 1000, 3, 1, 1000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium_BW, Enemy_Medium2_1, Enemy_Medium2_2, Enemy_Medium2_3, Enemy_Medium2_BW_1, Enemy_Medium2_BW_2, Enemy_Medium2_BW_3, 60, new TimeSpan(0, 2, 19), new TimeSpan(0, 0, 0, 5, 0), 1);
                    enemySpawner.addSpawn(new Vector2(37, -50), 150, 3, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium_BW, Enemy_Small2_1, Enemy_Small2_2, Enemy_Small2_3, Enemy_Small2_BW_1, Enemy_Small2_BW_2, Enemy_Small2_BW_3, 3, new TimeSpan(0, 2, 20), new TimeSpan(0, 0, 0, 0, 500), 7);
                    enemySpawner.addSpawn(new Vector2(137, -50), 150, 3, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium_BW, Enemy_Small2_1, Enemy_Small2_2, Enemy_Small2_3, Enemy_Small2_BW_1, Enemy_Small2_BW_2, Enemy_Small2_BW_3, 3, new TimeSpan(0, 2, 20), new TimeSpan(0, 0, 0, 0, 500), 7);
                    enemySpawner.addSpawn(new Vector2(237, -50), 150, 3, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium_BW, Enemy_Small2_1, Enemy_Small2_2, Enemy_Small2_3, Enemy_Small2_BW_1, Enemy_Small2_BW_2, Enemy_Small2_BW_3, 3, new TimeSpan(0, 2, 20), new TimeSpan(0, 0, 0, 0, 500), 7);
                    enemySpawner.addSpawn(new Vector2(337, -50), 150, 3, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium_BW, Enemy_Small2_1, Enemy_Small2_2, Enemy_Small2_3, Enemy_Small2_BW_1, Enemy_Small2_BW_2, Enemy_Small2_BW_3, 3, new TimeSpan(0, 2, 20), new TimeSpan(0, 0, 0, 0, 500), 7);
                    enemySpawner.addSpawn(new Vector2(437, -50), 150, 3, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium_BW, Enemy_Small2_1, Enemy_Small2_2, Enemy_Small2_3, Enemy_Small2_BW_1, Enemy_Small2_BW_2, Enemy_Small2_BW_3, 3, new TimeSpan(0, 2, 20), new TimeSpan(0, 0, 0, 0, 500), 7);
                    enemySpawner.addSpawn(new Vector2(537, -50), 150, 3, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium_BW, Enemy_Small2_1, Enemy_Small2_2, Enemy_Small2_3, Enemy_Small2_BW_1, Enemy_Small2_BW_2, Enemy_Small2_BW_3, 3, new TimeSpan(0, 2, 20), new TimeSpan(0, 0, 0, 0, 500), 7);
                    enemySpawner.addSpawn(new Vector2(637, -50), 150, 3, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium_BW, Enemy_Small2_1, Enemy_Small2_2, Enemy_Small2_3, Enemy_Small2_BW_1, Enemy_Small2_BW_2, Enemy_Small2_BW_3, 3, new TimeSpan(0, 2, 20), new TimeSpan(0, 0, 0, 0, 500), 7);

                    enemySpawner.addSpawn(new Vector2(325, -200), 5000, 1, 6, 15000, EnemyLaserProjectile_Large, EnemyLaserProjectile_Large_BW, Enemy_Large2_1, Enemy_Large2_2, Enemy_Large2_3, Enemy_Large2_BW_1, Enemy_Large2_BW_2, Enemy_Large2_BW_3, 300, new TimeSpan(0, 2, 26), new TimeSpan(0, 0, 0, 9, 0), 4);
                    enemySpawner.addSpawn(new Vector2(100, -200), 5000, 1, 6, 15000, EnemyLaserProjectile_Large, EnemyLaserProjectile_Large_BW, Enemy_Large2_1, Enemy_Large2_2, Enemy_Large2_3, Enemy_Large2_BW_1, Enemy_Large2_BW_2, Enemy_Large2_BW_3, 300, new TimeSpan(0, 2, 29), new TimeSpan(0, 0, 0, 9, 0), 4);
                    enemySpawner.addSpawn(new Vector2(550, -200), 5000, 1, 6, 15000, EnemyLaserProjectile_Large, EnemyLaserProjectile_Large_BW, Enemy_Large2_1, Enemy_Large2_2, Enemy_Large2_3, Enemy_Large2_BW_1, Enemy_Large2_BW_2, Enemy_Large2_BW_3, 300, new TimeSpan(0, 2, 32), new TimeSpan(0, 0, 0, 9, 0), 4);
                    enemySpawner.addSpawn(new Vector2(212, -100), 1000, 2, 1, 1000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium_BW, Enemy_Medium2_1, Enemy_Medium2_2, Enemy_Medium2_3, Enemy_Medium2_BW_1, Enemy_Medium2_BW_2, Enemy_Medium2_BW_3, 60, new TimeSpan(0, 2, 28), new TimeSpan(0, 0, 0, 5, 0), 9);
                    enemySpawner.addSpawn(new Vector2(438, -100), 1000, 2, 1, 1000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium_BW, Enemy_Medium2_1, Enemy_Medium2_2, Enemy_Medium2_3, Enemy_Medium2_BW_1, Enemy_Medium2_BW_2, Enemy_Medium2_BW_3, 60, new TimeSpan(0, 2, 35), new TimeSpan(0, 0, 0, 5, 0), 6);
                    enemySpawner.addSpawn(new Vector2(-100, -100), 1000, 2, 2, 1000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium_BW, Enemy_Medium2_1, Enemy_Medium2_2, Enemy_Medium2_3, Enemy_Medium2_BW_1, Enemy_Medium2_BW_2, Enemy_Medium2_BW_3, 60, new TimeSpan(0, 2, 38), new TimeSpan(0, 0, 0, 5, 0), 5);
                    enemySpawner.addSpawn(new Vector2(750, -100), 1000, 2, 3, 1000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium_BW, Enemy_Medium2_1, Enemy_Medium2_2, Enemy_Medium2_3, Enemy_Medium2_BW_1, Enemy_Medium2_BW_2, Enemy_Medium2_BW_3, 60, new TimeSpan(0, 2, 42), new TimeSpan(0, 0, 0, 5, 0), 4);
                    enemySpawner.addSpawn(new Vector2(-75, -50), 200, 3, 2, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium_BW, Enemy_Small2_1, Enemy_Small2_2, Enemy_Small2_3, Enemy_Small2_BW_1, Enemy_Small2_BW_2, Enemy_Small2_BW_3, 10, new TimeSpan(0, 2, 36), new TimeSpan(0, 0, 0, 5, 0), 6);
                    enemySpawner.addSpawn(new Vector2(750, -50), 200, 3, 3, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium_BW, Enemy_Small2_1, Enemy_Small2_2, Enemy_Small2_3, Enemy_Small2_BW_1, Enemy_Small2_BW_2, Enemy_Small2_BW_3, 10, new TimeSpan(0, 2, 40), new TimeSpan(0, 0, 0, 5, 0), 5);
                    enemySpawner.addSpawn(new Vector2(-75, 300), 200, 3, 4, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium_BW, Enemy_Small2_1, Enemy_Small2_2, Enemy_Small2_3, Enemy_Small2_BW_1, Enemy_Small2_BW_2, Enemy_Small2_BW_3, 10, new TimeSpan(0, 2, 45), new TimeSpan(0, 0, 0, 3, 0), 9);
                    enemySpawner.addSpawn(new Vector2(750, 300), 200, 3, 5, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium_BW, Enemy_Small2_1, Enemy_Small2_2, Enemy_Small2_3, Enemy_Small2_BW_1, Enemy_Small2_BW_2, Enemy_Small2_BW_3, 10, new TimeSpan(0, 2, 48), new TimeSpan(0, 0, 0, 3, 0), 8);
                    

                    Boss = new Boss(new Vector2(130, -200), 100000, Boss2_1, Boss2_2, Boss2_3, Boss2_BW_1, Boss2_BW_2, Boss2_BW_3, new TimeSpan(0, 3, 25));
                    Boss.Weapons.Add(new Weapon(new Vector2(375, -40), MathHelper.ToRadians(150), 750, 8, 5, 50, 4, HomingProjectile, HomingProjectile_BW));
                    Boss.Weapons.Add(new Weapon(new Vector2(375, -40), MathHelper.ToRadians(150), 500, 10, 5, 50, 4, HomingProjectile, HomingProjectile_BW));
                    Boss.Weapons.Add(new Weapon(new Vector2(170, -30), MathHelper.ToRadians(120), 2000, 5, 15, 2, 6, HomingProjectile, HomingProjectile_BW));
                    Boss.Weapons.Add(new Weapon(new Vector2(555, -30), MathHelper.ToRadians(240), 2000, 5, 15, 2, 6, HomingProjectile, HomingProjectile_BW));
                    Boss.addEvent(1, TimeSpan.Zero, 0, 260, 100, 0);
                    Boss.addEvent(3, new TimeSpan(0, 0, 3), 1, 1, 1, 0);
                    Boss.addEvent(2, TimeSpan.Zero, 1, 60, 300, -1);
                    Boss.addEvent(2, TimeSpan.Zero, 2, 60, 300, -1);
                    Boss.addEvent(2, 33333, 3, 40, 200, -1);
                    Boss.addEvent(2, 33333, 4, -40, 200, -1);
                    Boss.addEvent(3, 66666, 2, 1, 1, 0);
                    Boss.addEvent(3, 66666, 1, 0, 1, 0);
                    Boss.addEvent(3, 33333, 3, 1, 1, 0);
                    Boss.addEvent(3, 33333, 4, 1, 1, 0);
                    break;


                case 3:
                    enemySpawner = new EnemySpawner();
                    enemySpawner.addSpawn(new Vector2(50, -50), 10, 2, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small_1, Enemy_Small_2, Enemy_Small_3, Enemy_Small_BW_1, Enemy_Small_BW_2, Enemy_Small_BW_3, 100, new TimeSpan(0, 0, 3), new TimeSpan(0, 0, 0, 0, 500), 1);
                    enemySpawner.addSpawn(new Vector2(100, -50), 10, 2, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small_1, Enemy_Small_2, Enemy_Small_3, Enemy_Small_BW_1, Enemy_Small_BW_2, Enemy_Small_BW_3, 100, new TimeSpan(0, 0, 3), new TimeSpan(0, 0, 0, 0, 500), 1);
                    enemySpawner.addSpawn(new Vector2(150, -50), 10, 2, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small_1, Enemy_Small_2, Enemy_Small_3, Enemy_Small_BW_1, Enemy_Small_BW_2, Enemy_Small_BW_3, 100, new TimeSpan(0, 0, 3), new TimeSpan(0, 0, 0, 0, 500), 1);
                    enemySpawner.addSpawn(new Vector2(200, -50), 10, 2, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small_1, Enemy_Small_2, Enemy_Small_3, Enemy_Small_BW_1, Enemy_Small_BW_2, Enemy_Small_BW_3, 100, new TimeSpan(0, 0, 3), new TimeSpan(0, 0, 0, 0, 500), 1);
                    enemySpawner.addSpawn(new Vector2(250, -50), 10, 2, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small_1, Enemy_Small_2, Enemy_Small_3, Enemy_Small_BW_1, Enemy_Small_BW_2, Enemy_Small_BW_3, 100, new TimeSpan(0, 0, 3), new TimeSpan(0, 0, 0, 0, 500), 1);
                    enemySpawner.addSpawn(new Vector2(300, -50), 10, 2, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small_1, Enemy_Small_2, Enemy_Small_3, Enemy_Small_BW_1, Enemy_Small_BW_2, Enemy_Small_BW_3, 100, new TimeSpan(0, 0, 3), new TimeSpan(0, 0, 0, 0, 500), 1);
                    enemySpawner.addSpawn(new Vector2(350, -50), 10, 2, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small_1, Enemy_Small_2, Enemy_Small_3, Enemy_Small_BW_1, Enemy_Small_BW_2, Enemy_Small_BW_3, 100, new TimeSpan(0, 0, 3), new TimeSpan(0, 0, 0, 0, 500), 1);
                    enemySpawner.addSpawn(new Vector2(400, -50), 10, 2, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small_1, Enemy_Small_2, Enemy_Small_3, Enemy_Small_BW_1, Enemy_Small_BW_2, Enemy_Small_BW_3, 100, new TimeSpan(0, 0, 3), new TimeSpan(0, 0, 0, 0, 500), 1);
                    enemySpawner.addSpawn(new Vector2(450, -50), 10, 2, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small_1, Enemy_Small_2, Enemy_Small_3, Enemy_Small_BW_1, Enemy_Small_BW_2, Enemy_Small_BW_3, 100, new TimeSpan(0, 0, 3), new TimeSpan(0, 0, 0, 0, 500), 1);
                    enemySpawner.addSpawn(new Vector2(500, -50), 10, 2, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small_1, Enemy_Small_2, Enemy_Small_3, Enemy_Small_BW_1, Enemy_Small_BW_2, Enemy_Small_BW_3, 100, new TimeSpan(0, 0, 3), new TimeSpan(0, 0, 0, 0, 500), 1);
                    enemySpawner.addSpawn(new Vector2(550, -50), 10, 2, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small_1, Enemy_Small_2, Enemy_Small_3, Enemy_Small_BW_1, Enemy_Small_BW_2, Enemy_Small_BW_3, 100, new TimeSpan(0, 0, 3), new TimeSpan(0, 0, 0, 0, 500), 1);
                    enemySpawner.addSpawn(new Vector2(600, -50), 10, 2, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small_1, Enemy_Small_2, Enemy_Small_3, Enemy_Small_BW_1, Enemy_Small_BW_2, Enemy_Small_BW_3, 100, new TimeSpan(0, 0, 3), new TimeSpan(0, 0, 0, 0, 500), 1);
                    enemySpawner.addSpawn(new Vector2(650, -50), 10, 2, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small_1, Enemy_Small_2, Enemy_Small_3, Enemy_Small_BW_1, Enemy_Small_BW_2, Enemy_Small_BW_3, 100, new TimeSpan(0, 0, 3), new TimeSpan(0, 0, 0, 0, 500), 1);

                    enemySpawner.addSpawn(new Vector2(-50, 350), 25, 5, 4, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small3_1, Enemy_Small3_2, Enemy_Small3_3, Enemy_Small3_BW_1, Enemy_Small3_BW_2, Enemy_Small3_BW_3, 2, new TimeSpan(0, 0, 10), new TimeSpan(0, 0, 0, 0, 500), 0);
                    enemySpawner.addSpawn(new Vector2(750, 350), 25, 5, 5, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small3_1, Enemy_Small3_2, Enemy_Small3_3, Enemy_Small3_BW_1, Enemy_Small3_BW_2, Enemy_Small3_BW_3, 2, new TimeSpan(0, 0, 10), new TimeSpan(0, 0, 0, 0, 500), 0);
                    enemySpawner.addSpawn(new Vector2(-50, -50), 25, 5, 2, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small3_1, Enemy_Small3_2, Enemy_Small3_3, Enemy_Small3_BW_1, Enemy_Small3_BW_2, Enemy_Small3_BW_3, 2, new TimeSpan(0, 0, 11), new TimeSpan(0, 0, 0, 0, 500), 0);
                    enemySpawner.addSpawn(new Vector2(750, -50), 25, 5, 3, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small3_1, Enemy_Small3_2, Enemy_Small3_3, Enemy_Small3_BW_1, Enemy_Small3_BW_2, Enemy_Small3_BW_3, 2, new TimeSpan(0, 0, 11), new TimeSpan(0, 0, 0, 0, 500), 0);
                    enemySpawner.addSpawn(new Vector2(325, -50), 25, 5, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small3_1, Enemy_Small3_2, Enemy_Small3_3, Enemy_Small3_BW_1, Enemy_Small3_BW_2, Enemy_Small3_BW_3, 2, new TimeSpan(0, 0, 12), new TimeSpan(0, 0, 0, 0, 500), 0);
                    enemySpawner.addSpawn(new Vector2(375, -50), 25, 5, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small3_1, Enemy_Small3_2, Enemy_Small3_3, Enemy_Small3_BW_1, Enemy_Small3_BW_2, Enemy_Small3_BW_3, 2, new TimeSpan(0, 0, 12), new TimeSpan(0, 0, 0, 0, 500), 0);
                    enemySpawner.addSpawn(new Vector2(-50, 350), 25, 5, 4, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small3_1, Enemy_Small3_2, Enemy_Small3_3, Enemy_Small3_BW_1, Enemy_Small3_BW_2, Enemy_Small3_BW_3, 2, new TimeSpan(0, 0, 14), new TimeSpan(0, 0, 0, 0, 250), 4);
                    enemySpawner.addSpawn(new Vector2(750, 350), 25, 5, 5, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small3_1, Enemy_Small3_2, Enemy_Small3_3, Enemy_Small3_BW_1, Enemy_Small3_BW_2, Enemy_Small3_BW_3, 2, new TimeSpan(0, 0, 14), new TimeSpan(0, 0, 0, 0, 250), 4);
                    enemySpawner.addSpawn(new Vector2(-50, -50), 25, 5, 2, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small3_1, Enemy_Small3_2, Enemy_Small3_3, Enemy_Small3_BW_1, Enemy_Small3_BW_2, Enemy_Small3_BW_3, 2, new TimeSpan(0, 0, 14), new TimeSpan(0, 0, 0, 0, 250), 4);
                    enemySpawner.addSpawn(new Vector2(750, -50), 25, 5, 3, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small3_1, Enemy_Small3_2, Enemy_Small3_3, Enemy_Small3_BW_1, Enemy_Small3_BW_2, Enemy_Small3_BW_3, 2, new TimeSpan(0, 0, 14), new TimeSpan(0, 0, 0, 0, 250), 4);
                    enemySpawner.addSpawn(new Vector2(325, -50), 25, 5, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small3_1, Enemy_Small3_2, Enemy_Small3_3, Enemy_Small3_BW_1, Enemy_Small3_BW_2, Enemy_Small3_BW_3, 2, new TimeSpan(0, 0, 14), new TimeSpan(0, 0, 0, 0, 250), 4);
                    enemySpawner.addSpawn(new Vector2(375, -50), 25, 5, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small3_1, Enemy_Small3_2, Enemy_Small3_3, Enemy_Small3_BW_1, Enemy_Small3_BW_2, Enemy_Small3_BW_3, 2, new TimeSpan(0, 0, 14), new TimeSpan(0, 0, 0, 0, 250), 4);

                    enemySpawner.addSpawn(new Vector2(100, -50), 50, 10, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small3_1, Enemy_Small3_2, Enemy_Small3_3, Enemy_Small3_BW_1, Enemy_Small3_BW_2, Enemy_Small3_BW_3, 2, new TimeSpan(0, 0, 17), new TimeSpan(0, 0, 0, 6, 0), 4);
                    enemySpawner.addSpawn(new Vector2(300, -50), 50, 10, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small3_1, Enemy_Small3_2, Enemy_Small3_3, Enemy_Small3_BW_1, Enemy_Small3_BW_2, Enemy_Small3_BW_3, 2, new TimeSpan(0, 0, 18), new TimeSpan(0, 0, 0, 6, 0), 4);
                    enemySpawner.addSpawn(new Vector2(600, -50), 50, 10, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small3_1, Enemy_Small3_2, Enemy_Small3_3, Enemy_Small3_BW_1, Enemy_Small3_BW_2, Enemy_Small3_BW_3, 2, new TimeSpan(0, 0, 19), new TimeSpan(0, 0, 0, 6, 0), 4);
                    enemySpawner.addSpawn(new Vector2(200, -50), 50, 10, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small3_1, Enemy_Small3_2, Enemy_Small3_3, Enemy_Small3_BW_1, Enemy_Small3_BW_2, Enemy_Small3_BW_3, 2, new TimeSpan(0, 0, 20), new TimeSpan(0, 0, 0, 6, 0), 4);
                    enemySpawner.addSpawn(new Vector2(500, -50), 50, 10, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small3_1, Enemy_Small3_2, Enemy_Small3_3, Enemy_Small3_BW_1, Enemy_Small3_BW_2, Enemy_Small3_BW_3, 2, new TimeSpan(0, 0, 21), new TimeSpan(0, 0, 0, 6, 0), 4);
                    enemySpawner.addSpawn(new Vector2(400, -50), 50, 10, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small3_1, Enemy_Small3_2, Enemy_Small3_3, Enemy_Small3_BW_1, Enemy_Small3_BW_2, Enemy_Small3_BW_3, 2, new TimeSpan(0, 0, 22), new TimeSpan(0, 0, 0, 6, 0), 4);
                    enemySpawner.addSpawn(new Vector2(-50, -50), 50, 10, 2, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small3_1, Enemy_Small3_2, Enemy_Small3_3, Enemy_Small3_BW_1, Enemy_Small3_BW_2, Enemy_Small3_BW_3, 2, new TimeSpan(0, 0, 23), new TimeSpan(0, 0, 0, 6, 0), 3);
                    enemySpawner.addSpawn(new Vector2(750, -50), 50, 10, 3, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small3_1, Enemy_Small3_2, Enemy_Small3_3, Enemy_Small3_BW_1, Enemy_Small3_BW_2, Enemy_Small3_BW_3, 2, new TimeSpan(0, 0, 24), new TimeSpan(0, 0, 0, 6, 0), 3);
                    enemySpawner.addSpawn(new Vector2(350, -75), 300, 6, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium_BW, Enemy_Medium3_1, Enemy_Medium3_2, Enemy_Medium3_3, Enemy_Medium3_BW_1, Enemy_Medium3_BW_2, Enemy_Medium3_BW_3, 20, new TimeSpan(0, 0, 20), new TimeSpan(0, 0, 0, 4, 0), 6);
                    enemySpawner.addSpawn(new Vector2(550, -75), 300, 6, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium_BW, Enemy_Medium3_1, Enemy_Medium3_2, Enemy_Medium3_3, Enemy_Medium3_BW_1, Enemy_Medium3_BW_2, Enemy_Medium3_BW_3, 20, new TimeSpan(0, 0, 23), new TimeSpan(0, 0, 0, 4, 0), 6);
                    enemySpawner.addSpawn(new Vector2(250, -75), 300, 6, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium_BW, Enemy_Medium3_1, Enemy_Medium3_2, Enemy_Medium3_3, Enemy_Medium3_BW_1, Enemy_Medium3_BW_2, Enemy_Medium3_BW_3, 20, new TimeSpan(0, 0, 25), new TimeSpan(0, 0, 0, 4, 0), 5);

                    enemySpawner.addSpawn(new Vector2(275, -100), 1000, 4, 1, 15000, EnemyLaserProjectile_Large, EnemyLaserProjectile_Large_BW, Enemy_Large3_1, Enemy_Large3_2, Enemy_Large3_3, Enemy_Large3_BW_1, Enemy_Large3_BW_2, Enemy_Large3_BW_3, 80, new TimeSpan(0, 0, 50), new TimeSpan(0, 0, 0, 5, 0), 3);
                    enemySpawner.addSpawn(new Vector2(75, -100), 1000, 4, 1, 15000, EnemyLaserProjectile_Large, EnemyLaserProjectile_Large_BW, Enemy_Large3_1, Enemy_Large3_2, Enemy_Large3_3, Enemy_Large3_BW_1, Enemy_Large3_BW_2, Enemy_Large3_BW_3, 80, new TimeSpan(0, 0, 51), new TimeSpan(0, 0, 0, 5, 0), 3);
                    enemySpawner.addSpawn(new Vector2(475, -100), 1000, 4, 1, 15000, EnemyLaserProjectile_Large, EnemyLaserProjectile_Large_BW, Enemy_Large3_1, Enemy_Large3_2, Enemy_Large3_3, Enemy_Large3_BW_1, Enemy_Large3_BW_2, Enemy_Large3_BW_3, 80, new TimeSpan(0, 0, 51), new TimeSpan(0, 0, 0, 5, 0), 3);

                    enemySpawner.addSpawn(new Vector2(175, -100), 1250, 4, 1, 15000, EnemyLaserProjectile_Large, EnemyLaserProjectile_Large_BW, Enemy_Large3_1, Enemy_Large3_2, Enemy_Large3_3, Enemy_Large3_BW_1, Enemy_Large3_BW_2, Enemy_Large3_BW_3, 120, new TimeSpan(0, 1, 10), new TimeSpan(0, 0, 0, 3, 0), 19);
                    enemySpawner.addSpawn(new Vector2(375, -100), 1250, 4, 1, 15000, EnemyLaserProjectile_Large, EnemyLaserProjectile_Large_BW, Enemy_Large3_1, Enemy_Large3_2, Enemy_Large3_3, Enemy_Large3_BW_1, Enemy_Large3_BW_2, Enemy_Large3_BW_3, 120, new TimeSpan(0, 1, 10), new TimeSpan(0, 0, 0, 3, 0), 19);
                    enemySpawner.addSpawn(new Vector2(325, -75), 400, 6, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium_BW, Enemy_Medium3_1, Enemy_Medium3_2, Enemy_Medium3_3, Enemy_Medium3_BW_1, Enemy_Medium3_BW_2, Enemy_Medium3_BW_3, 30, new TimeSpan(0, 1, 17), new TimeSpan(0, 0, 0, 3, 0), 17);
                    enemySpawner.addSpawn(new Vector2(225, -75), 400, 6, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium_BW, Enemy_Medium3_1, Enemy_Medium3_2, Enemy_Medium3_3, Enemy_Medium3_BW_1, Enemy_Medium3_BW_2, Enemy_Medium3_BW_3, 30, new TimeSpan(0, 1, 17), new TimeSpan(0, 0, 0, 3, 0), 17);
                    enemySpawner.addSpawn(new Vector2(425, -75), 400, 6, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium_BW, Enemy_Medium3_1, Enemy_Medium3_2, Enemy_Medium3_3, Enemy_Medium3_BW_1, Enemy_Medium3_BW_2, Enemy_Medium3_BW_3, 30, new TimeSpan(0, 1, 17), new TimeSpan(0, 0, 0, 3, 0), 17);
                    enemySpawner.addSpawn(new Vector2(325, -75), 400, 6, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium_BW, Enemy_Medium3_1, Enemy_Medium3_2, Enemy_Medium3_3, Enemy_Medium3_BW_1, Enemy_Medium3_BW_2, Enemy_Medium3_BW_3, 30, new TimeSpan(0, 1, 18), new TimeSpan(0, 0, 0, 3, 0), 17);
                    enemySpawner.addSpawn(new Vector2(225, -75), 400, 6, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium_BW, Enemy_Medium3_1, Enemy_Medium3_2, Enemy_Medium3_3, Enemy_Medium3_BW_1, Enemy_Medium3_BW_2, Enemy_Medium3_BW_3, 30, new TimeSpan(0, 1, 18), new TimeSpan(0, 0, 0, 3, 0), 17);
                    enemySpawner.addSpawn(new Vector2(425, -75), 400, 6, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium_BW, Enemy_Medium3_1, Enemy_Medium3_2, Enemy_Medium3_3, Enemy_Medium3_BW_1, Enemy_Medium3_BW_2, Enemy_Medium3_BW_3, 30, new TimeSpan(0, 1, 18), new TimeSpan(0, 0, 0, 3, 0), 17);
                    enemySpawner.addSpawn(new Vector2(-50, -150), 50, 8, 2, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small3_1, Enemy_Small3_2, Enemy_Small3_3, Enemy_Small3_BW_1, Enemy_Small3_BW_2, Enemy_Small3_BW_3, 2, new TimeSpan(0, 1, 14), new TimeSpan(0, 0, 0, 3, 0), 18);
                    enemySpawner.addSpawn(new Vector2(-50, -100), 50, 8, 2, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small3_1, Enemy_Small3_2, Enemy_Small3_3, Enemy_Small3_BW_1, Enemy_Small3_BW_2, Enemy_Small3_BW_3, 2, new TimeSpan(0, 1, 14), new TimeSpan(0, 0, 0, 3, 0), 18);
                    enemySpawner.addSpawn(new Vector2(-50, -50), 50, 8, 2, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small3_1, Enemy_Small3_2, Enemy_Small3_3, Enemy_Small3_BW_1, Enemy_Small3_BW_2, Enemy_Small3_BW_3, 2, new TimeSpan(0, 1, 14), new TimeSpan(0, 0, 0, 3, 0), 18);
                    enemySpawner.addSpawn(new Vector2(750, -50), 50, 8, 3, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small3_1, Enemy_Small3_2, Enemy_Small3_3, Enemy_Small3_BW_1, Enemy_Small3_BW_2, Enemy_Small3_BW_3, 2, new TimeSpan(0, 1, 14), new TimeSpan(0, 0, 0, 3, 0), 18);
                    enemySpawner.addSpawn(new Vector2(750, -100), 50, 8, 3, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small3_1, Enemy_Small3_2, Enemy_Small3_3, Enemy_Small3_BW_1, Enemy_Small3_BW_2, Enemy_Small3_BW_3, 2, new TimeSpan(0, 1, 14), new TimeSpan(0, 0, 0, 3, 0), 18);
                    enemySpawner.addSpawn(new Vector2(750, -150), 50, 8, 3, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small3_1, Enemy_Small3_2, Enemy_Small3_3, Enemy_Small3_BW_1, Enemy_Small3_BW_2, Enemy_Small3_BW_3, 2, new TimeSpan(0, 1, 14), new TimeSpan(0, 0, 0, 3, 0), 18);
                    enemySpawner.addSpawn(new Vector2(-50, -150), 50, 8, 2, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small3_1, Enemy_Small3_2, Enemy_Small3_3, Enemy_Small3_BW_1, Enemy_Small3_BW_2, Enemy_Small3_BW_3, 2, new TimeSpan(0, 1, 15), new TimeSpan(0, 0, 0, 3, 0), 18);
                    enemySpawner.addSpawn(new Vector2(-50, -100), 50, 8, 2, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small3_1, Enemy_Small3_2, Enemy_Small3_3, Enemy_Small3_BW_1, Enemy_Small3_BW_2, Enemy_Small3_BW_3, 2, new TimeSpan(0, 1, 15), new TimeSpan(0, 0, 0, 3, 0), 18);
                    enemySpawner.addSpawn(new Vector2(-50, -50), 50, 8, 2, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small3_1, Enemy_Small3_2, Enemy_Small3_3, Enemy_Small3_BW_1, Enemy_Small3_BW_2, Enemy_Small3_BW_3, 2, new TimeSpan(0, 1, 15), new TimeSpan(0, 0, 0, 3, 0), 18);
                    enemySpawner.addSpawn(new Vector2(750, -50), 50, 8, 3, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small3_1, Enemy_Small3_2, Enemy_Small3_3, Enemy_Small3_BW_1, Enemy_Small3_BW_2, Enemy_Small3_BW_3, 2, new TimeSpan(0, 1, 15), new TimeSpan(0, 0, 0, 3, 0), 18);
                    enemySpawner.addSpawn(new Vector2(750, -100), 50, 8, 3, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small3_1, Enemy_Small3_2, Enemy_Small3_3, Enemy_Small3_BW_1, Enemy_Small3_BW_2, Enemy_Small3_BW_3, 2, new TimeSpan(0, 1, 15), new TimeSpan(0, 0, 0, 3, 0), 18);
                    enemySpawner.addSpawn(new Vector2(750, -150), 50, 8, 3, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small3_1, Enemy_Small3_2, Enemy_Small3_3, Enemy_Small3_BW_1, Enemy_Small3_BW_2, Enemy_Small3_BW_3, 2, new TimeSpan(0, 1, 15), new TimeSpan(0, 0, 0, 3, 0), 18);
                    enemySpawner.addSpawn(new Vector2(-100, 325), 400, 6, 4, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium_BW, Enemy_Medium3_1, Enemy_Medium3_2, Enemy_Medium3_3, Enemy_Medium3_BW_1, Enemy_Medium3_BW_2, Enemy_Medium3_BW_3, 30, new TimeSpan(0, 1, 20), new TimeSpan(0, 0, 0, 3, 0), 16);
                    enemySpawner.addSpawn(new Vector2(750, 325), 400, 6, 5, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium_BW, Enemy_Medium3_1, Enemy_Medium3_2, Enemy_Medium3_3, Enemy_Medium3_BW_1, Enemy_Medium3_BW_2, Enemy_Medium3_BW_3, 30, new TimeSpan(0, 1, 20), new TimeSpan(0, 0, 0, 3, 0), 16);
                    enemySpawner.addSpawn(new Vector2(-50, 400), 50, 6, 4, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small3_1, Enemy_Small3_2, Enemy_Small3_3, Enemy_Small3_BW_1, Enemy_Small3_BW_2, Enemy_Small3_BW_3, 2, new TimeSpan(0, 1, 20), new TimeSpan(0, 0, 0, 3, 0), 16);
                    enemySpawner.addSpawn(new Vector2(750, 400), 50, 6, 5, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small3_1, Enemy_Small3_2, Enemy_Small3_3, Enemy_Small3_BW_1, Enemy_Small3_BW_2, Enemy_Small3_BW_3, 2, new TimeSpan(0, 1, 20), new TimeSpan(0, 0, 0, 3, 0), 16);
                    enemySpawner.addSpawn(new Vector2(-100, 325), 400, 6, 4, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium_BW, Enemy_Medium3_1, Enemy_Medium3_2, Enemy_Medium3_3, Enemy_Medium3_BW_1, Enemy_Medium3_BW_2, Enemy_Medium3_BW_3, 30, new TimeSpan(0, 1, 21), new TimeSpan(0, 0, 0, 3, 0), 16);
                    enemySpawner.addSpawn(new Vector2(750, 325), 400, 6, 5, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium_BW, Enemy_Medium3_1, Enemy_Medium3_2, Enemy_Medium3_3, Enemy_Medium3_BW_1, Enemy_Medium3_BW_2, Enemy_Medium3_BW_3, 30, new TimeSpan(0, 1, 21), new TimeSpan(0, 0, 0, 3, 0), 16);
                    enemySpawner.addSpawn(new Vector2(-50, 400), 50, 6, 4, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small3_1, Enemy_Small3_2, Enemy_Small3_3, Enemy_Small3_BW_1, Enemy_Small3_BW_2, Enemy_Small3_BW_3, 2, new TimeSpan(0, 1, 21), new TimeSpan(0, 0, 0, 3, 0), 16);
                    enemySpawner.addSpawn(new Vector2(750, 400), 50, 6, 5, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small3_1, Enemy_Small3_2, Enemy_Small3_3, Enemy_Small3_BW_1, Enemy_Small3_BW_2, Enemy_Small3_BW_3, 2, new TimeSpan(0, 1, 21), new TimeSpan(0, 0, 0, 3, 0), 16);

                    enemySpawner.addSpawn(new Vector2(-100, 825), 300, 3, 4, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium_BW, Enemy_Medium3_1, Enemy_Medium3_2, Enemy_Medium3_3, Enemy_Medium3_BW_1, Enemy_Medium3_BW_2, Enemy_Medium3_BW_3, 20, new TimeSpan(0, 2, 15), new TimeSpan(0, 0, 0, 1, 0), 19);
                    enemySpawner.addSpawn(new Vector2(750, 750), 300, 3, 5, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium_BW, Enemy_Medium3_1, Enemy_Medium3_2, Enemy_Medium3_3, Enemy_Medium3_BW_1, Enemy_Medium3_BW_2, Enemy_Medium3_BW_3, 20, new TimeSpan(0, 2, 15), new TimeSpan(0, 0, 0, 1, 0), 19);
                    enemySpawner.addSpawn(new Vector2(-100, 75), 300, 3, 4, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium_BW, Enemy_Medium3_1, Enemy_Medium3_2, Enemy_Medium3_3, Enemy_Medium3_BW_1, Enemy_Medium3_BW_2, Enemy_Medium3_BW_3, 20, new TimeSpan(0, 2, 15), new TimeSpan(0, 0, 0, 1, 0), 19);
                    enemySpawner.addSpawn(new Vector2(750, 0), 300, 3, 5, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium_BW, Enemy_Medium3_1, Enemy_Medium3_2, Enemy_Medium3_3, Enemy_Medium3_BW_1, Enemy_Medium3_BW_2, Enemy_Medium3_BW_3, 20, new TimeSpan(0, 2, 15), new TimeSpan(0, 0, 0, 1, 0), 19);
                    enemySpawner.addSpawn(new Vector2(-50, 700), 50, 5, 4, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small3_1, Enemy_Small3_2, Enemy_Small3_3, Enemy_Small3_BW_1, Enemy_Small3_BW_2, Enemy_Small3_BW_3, 2, new TimeSpan(0, 2, 20), new TimeSpan(0, 0, 0, 1, 0), 14);
                    enemySpawner.addSpawn(new Vector2(750, 650), 50, 5, 5, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small3_1, Enemy_Small3_2, Enemy_Small3_3, Enemy_Small3_BW_1, Enemy_Small3_BW_2, Enemy_Small3_BW_3, 2, new TimeSpan(0, 2, 20), new TimeSpan(0, 0, 0, 1, 0), 14);
                    enemySpawner.addSpawn(new Vector2(-50, 200), 50, 5, 4, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small3_1, Enemy_Small3_2, Enemy_Small3_3, Enemy_Small3_BW_1, Enemy_Small3_BW_2, Enemy_Small3_BW_3, 2, new TimeSpan(0, 2, 20), new TimeSpan(0, 0, 0, 1, 0), 14);
                    enemySpawner.addSpawn(new Vector2(750, 150), 50, 5, 5, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small3_1, Enemy_Small3_2, Enemy_Small3_3, Enemy_Small3_BW_1, Enemy_Small3_BW_2, Enemy_Small3_BW_3, 2, new TimeSpan(0, 2, 20), new TimeSpan(0, 0, 0, 1, 0), 14);

                    enemySpawner.addSpawn(new Vector2(275, -100), 3000, 8, 6, 15000, EnemyLaserProjectile_Large, EnemyLaserProjectile_Large_BW, Enemy_Large3_1, Enemy_Large3_2, Enemy_Large3_3, Enemy_Large3_BW_1, Enemy_Large3_BW_2, Enemy_Large3_BW_3, 100, new TimeSpan(0, 2, 38), new TimeSpan(0, 0, 0, 2, 0), 14);
                    enemySpawner.addSpawn(new Vector2(-100, -75), 1000, 6, 2, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium_BW, Enemy_Medium3_1, Enemy_Medium3_2, Enemy_Medium3_3, Enemy_Medium3_BW_1, Enemy_Medium3_BW_2, Enemy_Medium3_BW_3, 50, new TimeSpan(0, 2, 39), new TimeSpan(0, 0, 0, 4, 0), 9);
                    enemySpawner.addSpawn(new Vector2(750, -75), 1000, 6, 3, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium_BW, Enemy_Medium3_1, Enemy_Medium3_2, Enemy_Medium3_3, Enemy_Medium3_BW_1, Enemy_Medium3_BW_2, Enemy_Medium3_BW_3, 50, new TimeSpan(0, 2, 41), new TimeSpan(0, 0, 0, 4, 0), 8);
                    

                    Boss = new Boss(new Vector2(200, -230), 100000, Boss3_1, Boss3_2, Boss3_3, Boss3_BW_1, Boss3_BW_2, Boss3_BW_3, new TimeSpan(0, 3, 15));
                    Boss.Weapons.Add(new Weapon(new Vector2(375, -125), MathHelper.ToRadians(140), 15, 20, 10, 1, 3, Laser, Laser_BW));
                    Boss.Weapons.Add(new Weapon(new Vector2(515, -185), MathHelper.ToRadians(45), 200, 2, 5, 15, 5, Laser, Laser_BW));
                    Boss.Weapons.Add(new Weapon(new Vector2(230, -185), MathHelper.ToRadians(315), 200, 2, 5, 15, 5, Laser, Laser_BW));
                    Boss.Weapons.Add(new Weapon(new Vector2(495, -60), MathHelper.ToRadians(220), 15, 20, 10, 1, 3, Laser, Laser_BW));
                    Boss.Weapons.Add(new Weapon(new Vector2(250, -60), MathHelper.ToRadians(140), 15, 20, 10, 1, 3, Laser, Laser_BW));
                    Boss.addEvent(1, TimeSpan.Zero, 0, 300, 100, 0);
                    Boss.addEvent(3, new TimeSpan(0, 0, 3), 1, 1, 10, -1);
                    Boss.addEvent(3, new TimeSpan(0, 0, 4), 1, 0, 20, -1);
                    Boss.addEvent(2, new TimeSpan(0, 0, 3), 1, 80, 400, -1);
                    Boss.addEvent(1, new TimeSpan(0, 0, 15), -200, 0, 100, 0);
                    Boss.addEvent(1, new TimeSpan(0, 0, 17), 500, 0, 200, -1);
                    Boss.addEvent(3, new TimeSpan(0, 0, 20), 2, 1, 10, 0);
                    Boss.addEvent(3, new TimeSpan(0, 0, 20), 3, 1, 10, 0);
                    Boss.addEvent(3, 66666, 4, 1, 10, -1);
                    Boss.addEvent(3, 66666, 4, 0, 20, -1);
                    Boss.addEvent(2, 66666, 4, -80, 300, -1);
                    Boss.addEvent(3, 33333, 5, 1, 10, -1);
                    Boss.addEvent(3, 33333, 5, 0, 20, -1);
                    Boss.addEvent(2, 33333, 5, 80, 300, -1);
                    break;


                case 4:
                    enemySpawner = new EnemySpawner();
                    enemySpawner.addSpawn(new Vector2(325, -50), 10, 2, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small_1, Enemy_Small_2, Enemy_Small_3, Enemy_Small_BW_1, Enemy_Small_BW_2, Enemy_Small_BW_3, 80, new TimeSpan(0, 0, 3), new TimeSpan(0, 0, 0, 0, 500), 3);
                    enemySpawner.addSpawn(new Vector2(275, -50), 10, 2, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small_1, Enemy_Small_2, Enemy_Small_3, Enemy_Small_BW_1, Enemy_Small_BW_2, Enemy_Small_BW_3, 80, new TimeSpan(0, 0, 3), new TimeSpan(0, 0, 0, 0, 500), 3);
                    enemySpawner.addSpawn(new Vector2(375, -50), 10, 2, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small_1, Enemy_Small_2, Enemy_Small_3, Enemy_Small_BW_1, Enemy_Small_BW_2, Enemy_Small_BW_3, 80, new TimeSpan(0, 0, 3), new TimeSpan(0, 0, 0, 0, 500), 3);
                    enemySpawner.addSpawn(new Vector2(425, -50), 10, 2, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small_1, Enemy_Small_2, Enemy_Small_3, Enemy_Small_BW_1, Enemy_Small_BW_2, Enemy_Small_BW_3, 80, new TimeSpan(0, 0, 3), new TimeSpan(0, 0, 0, 0, 500), 3);
                    enemySpawner.addSpawn(new Vector2(75, -50), 10, 2, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small_1, Enemy_Small_2, Enemy_Small_3, Enemy_Small_BW_1, Enemy_Small_BW_2, Enemy_Small_BW_3, 80, new TimeSpan(0, 0, 5), new TimeSpan(0, 0, 0, 0, 500), 3);
                    enemySpawner.addSpawn(new Vector2(25, -50), 10, 2, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small_1, Enemy_Small_2, Enemy_Small_3, Enemy_Small_BW_1, Enemy_Small_BW_2, Enemy_Small_BW_3, 80, new TimeSpan(0, 0, 5), new TimeSpan(0, 0, 0, 0, 500), 3);
                    enemySpawner.addSpawn(new Vector2(125, -50), 10, 2, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small_1, Enemy_Small_2, Enemy_Small_3, Enemy_Small_BW_1, Enemy_Small_BW_2, Enemy_Small_BW_3, 80, new TimeSpan(0, 0, 5), new TimeSpan(0, 0, 0, 0, 500), 3);
                    enemySpawner.addSpawn(new Vector2(175, -50), 10, 2, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small_1, Enemy_Small_2, Enemy_Small_3, Enemy_Small_BW_1, Enemy_Small_BW_2, Enemy_Small_BW_3, 80, new TimeSpan(0, 0, 5), new TimeSpan(0, 0, 0, 0, 500), 3);
                    enemySpawner.addSpawn(new Vector2(525, -50), 10, 2, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small_1, Enemy_Small_2, Enemy_Small_3, Enemy_Small_BW_1, Enemy_Small_BW_2, Enemy_Small_BW_3, 80, new TimeSpan(0, 0, 5), new TimeSpan(0, 0, 0, 0, 500), 3);
                    enemySpawner.addSpawn(new Vector2(575, -50), 10, 2, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small_1, Enemy_Small_2, Enemy_Small_3, Enemy_Small_BW_1, Enemy_Small_BW_2, Enemy_Small_BW_3, 80, new TimeSpan(0, 0, 5), new TimeSpan(0, 0, 0, 0, 500), 3);
                    enemySpawner.addSpawn(new Vector2(625, -50), 10, 2, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small_1, Enemy_Small_2, Enemy_Small_3, Enemy_Small_BW_1, Enemy_Small_BW_2, Enemy_Small_BW_3, 80, new TimeSpan(0, 0, 5), new TimeSpan(0, 0, 0, 0, 500), 3);
                    enemySpawner.addSpawn(new Vector2(675, -50), 10, 2, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small_1, Enemy_Small_2, Enemy_Small_3, Enemy_Small_BW_1, Enemy_Small_BW_2, Enemy_Small_BW_3, 80, new TimeSpan(0, 0, 5), new TimeSpan(0, 0, 0, 0, 500), 3);

                    enemySpawner.addSpawn(new Vector2(150, -50), 400, 3, 1, 200, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium_BW, Enemy_Medium4_1, Enemy_Medium4_2, Enemy_Medium4_3, Enemy_Medium4_BW_1, Enemy_Medium4_BW_2, Enemy_Medium4_BW_3, 25, new TimeSpan(0, 0, 15), new TimeSpan(0, 0, 0, 1, 500), 4);
                    enemySpawner.addSpawn(new Vector2(500, -50), 400, 3, 1, 200, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium_BW, Enemy_Medium4_1, Enemy_Medium4_2, Enemy_Medium4_3, Enemy_Medium4_BW_1, Enemy_Medium4_BW_2, Enemy_Medium4_BW_3, 25, new TimeSpan(0, 0, 15), new TimeSpan(0, 0, 0, 1, 500), 4);
                    enemySpawner.addSpawn(new Vector2(325, -50), 1250, 3, 1, 350, EnemyLaserProjectile_Large, EnemyLaserProjectile_Large_BW, Enemy_Large4_1, Enemy_Large4_2, Enemy_Large4_3, Enemy_Large4_BW_1, Enemy_Large4_BW_2, Enemy_Large4_BW_3, 100, new TimeSpan(0, 0, 17), new TimeSpan(0, 0, 0, 3, 0), 2);

                    enemySpawner.addSpawn(new Vector2(-50, -75), 75, 4, 2, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small4_1, Enemy_Small4_2, Enemy_Small4_3, Enemy_Small4_BW_1, Enemy_Small4_BW_2, Enemy_Small4_BW_3, 4, new TimeSpan(0, 0, 24), new TimeSpan(0, 0, 0, 0, 500), 7);
                    enemySpawner.addSpawn(new Vector2(750, -75), 75, 4, 3, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small4_1, Enemy_Small4_2, Enemy_Small4_3, Enemy_Small4_BW_1, Enemy_Small4_BW_2, Enemy_Small4_BW_3, 4, new TimeSpan(0, 0, 24), new TimeSpan(0, 0, 0, 0, 500), 7);
                    enemySpawner.addSpawn(new Vector2(-50, 350), 75, 4, 4, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small4_1, Enemy_Small4_2, Enemy_Small4_3, Enemy_Small4_BW_1, Enemy_Small4_BW_2, Enemy_Small4_BW_3, 4, new TimeSpan(0, 0, 24), new TimeSpan(0, 0, 0, 0, 500), 7);
                    enemySpawner.addSpawn(new Vector2(750, 350), 75, 4, 5, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small4_1, Enemy_Small4_2, Enemy_Small4_3, Enemy_Small4_BW_1, Enemy_Small4_BW_2, Enemy_Small4_BW_3, 4, new TimeSpan(0, 0, 24), new TimeSpan(0, 0, 0, 0, 500), 7);

                    enemySpawner.addSpawn(new Vector2(275, -50), 500, 3, 1, 200, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium_BW, Enemy_Medium4_1, Enemy_Medium4_2, Enemy_Medium4_3, Enemy_Medium4_BW_1, Enemy_Medium4_BW_2, Enemy_Medium4_BW_3, 27, new TimeSpan(0, 0, 28), new TimeSpan(0, 0, 0, 2, 0), 14);
                    enemySpawner.addSpawn(new Vector2(375, -50), 500, 3, 1, 200, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium_BW, Enemy_Medium4_1, Enemy_Medium4_2, Enemy_Medium4_3, Enemy_Medium4_BW_1, Enemy_Medium4_BW_2, Enemy_Medium4_BW_3, 27, new TimeSpan(0, 0, 28), new TimeSpan(0, 0, 0, 2, 0), 14);
                    enemySpawner.addSpawn(new Vector2(-100, 300), 1400, 2, 4, 350, EnemyLaserProjectile_Large, EnemyLaserProjectile_Large_BW, Enemy_Large4_1, Enemy_Large4_2, Enemy_Large4_3, Enemy_Large4_BW_1, Enemy_Large4_BW_2, Enemy_Large4_BW_3, 110, new TimeSpan(0, 0, 30), new TimeSpan(0, 0, 0, 3, 0), 9);
                    enemySpawner.addSpawn(new Vector2(750, 300), 1400, 2, 5, 350, EnemyLaserProjectile_Large, EnemyLaserProjectile_Large_BW, Enemy_Large4_1, Enemy_Large4_2, Enemy_Large4_3, Enemy_Large4_BW_1, Enemy_Large4_BW_2, Enemy_Large4_BW_3, 110, new TimeSpan(0, 0, 30), new TimeSpan(0, 0, 0, 3, 0), 9);
                    enemySpawner.addSpawn(new Vector2(225, -75), 75, 4, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small4_1, Enemy_Small4_2, Enemy_Small4_3, Enemy_Small4_BW_1, Enemy_Small4_BW_2, Enemy_Small4_BW_3, 4, new TimeSpan(0, 0, 32), new TimeSpan(0, 0, 0, 1, 0), 25);
                    enemySpawner.addSpawn(new Vector2(475, -75), 75, 4, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small4_1, Enemy_Small4_2, Enemy_Small4_3, Enemy_Small4_BW_1, Enemy_Small4_BW_2, Enemy_Small4_BW_3, 4, new TimeSpan(0, 0, 32), new TimeSpan(0, 0, 0, 1, 0), 25);
                    enemySpawner.addSpawn(new Vector2(-50, 600), 75, 4, 4, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small4_1, Enemy_Small4_2, Enemy_Small4_3, Enemy_Small4_BW_1, Enemy_Small4_BW_2, Enemy_Small4_BW_3, 4, new TimeSpan(0, 0, 33), new TimeSpan(0, 0, 0, 4, 0), 5);
                    enemySpawner.addSpawn(new Vector2(750, 600), 75, 4, 5, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small4_1, Enemy_Small4_2, Enemy_Small4_3, Enemy_Small4_BW_1, Enemy_Small4_BW_2, Enemy_Small4_BW_3, 4, new TimeSpan(0, 0, 33), new TimeSpan(0, 0, 0, 4, 0), 5);

                    enemySpawner.addSpawn(new Vector2(25, -100), 1500, 1, 6, 1000, EnemyLaserProjectile_Large, EnemyLaserProjectile_Large_BW, Enemy_Large4_1, Enemy_Large4_2, Enemy_Large4_3, Enemy_Large4_BW_1, Enemy_Large4_BW_2, Enemy_Large4_BW_3, 120, new TimeSpan(0, 1, 3), new TimeSpan(0, 0, 0, 5, 0), 4);
                    enemySpawner.addSpawn(new Vector2(175, -100), 1500, 1, 6, 1000, EnemyLaserProjectile_Large, EnemyLaserProjectile_Large_BW, Enemy_Large4_1, Enemy_Large4_2, Enemy_Large4_3, Enemy_Large4_BW_1, Enemy_Large4_BW_2, Enemy_Large4_BW_3, 120, new TimeSpan(0, 1, 3), new TimeSpan(0, 0, 0, 5, 0), 4);
                    enemySpawner.addSpawn(new Vector2(325, -100), 1500, 1, 6, 1000, EnemyLaserProjectile_Large, EnemyLaserProjectile_Large_BW, Enemy_Large4_1, Enemy_Large4_2, Enemy_Large4_3, Enemy_Large4_BW_1, Enemy_Large4_BW_2, Enemy_Large4_BW_3, 120, new TimeSpan(0, 1, 3), new TimeSpan(0, 0, 0, 5, 0), 4);
                    enemySpawner.addSpawn(new Vector2(475, -100), 1500, 1, 6, 1000, EnemyLaserProjectile_Large, EnemyLaserProjectile_Large_BW, Enemy_Large4_1, Enemy_Large4_2, Enemy_Large4_3, Enemy_Large4_BW_1, Enemy_Large4_BW_2, Enemy_Large4_BW_3, 120, new TimeSpan(0, 1, 3), new TimeSpan(0, 0, 0, 5, 0), 4);
                    enemySpawner.addSpawn(new Vector2(625, -100), 1500, 1, 6, 1000, EnemyLaserProjectile_Large, EnemyLaserProjectile_Large_BW, Enemy_Large4_1, Enemy_Large4_2, Enemy_Large4_3, Enemy_Large4_BW_1, Enemy_Large4_BW_2, Enemy_Large4_BW_3, 120, new TimeSpan(0, 1, 3), new TimeSpan(0, 0, 0, 5, 0), 4);
                    enemySpawner.addSpawn(new Vector2(-100, 100), 500, 2, 2, 200, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium_BW, Enemy_Medium4_1, Enemy_Medium4_2, Enemy_Medium4_3, Enemy_Medium4_BW_1, Enemy_Medium4_BW_2, Enemy_Medium4_BW_3, 27, new TimeSpan(0, 1, 5), new TimeSpan(0, 0, 0, 3, 0), 9);
                    enemySpawner.addSpawn(new Vector2(-100, 150), 500, 2, 2, 200, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium_BW, Enemy_Medium4_1, Enemy_Medium4_2, Enemy_Medium4_3, Enemy_Medium4_BW_1, Enemy_Medium4_BW_2, Enemy_Medium4_BW_3, 27, new TimeSpan(0, 1, 5), new TimeSpan(0, 0, 0, 3, 0), 9);
                    enemySpawner.addSpawn(new Vector2(-100, 200), 500, 2, 2, 200, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium_BW, Enemy_Medium4_1, Enemy_Medium4_2, Enemy_Medium4_3, Enemy_Medium4_BW_1, Enemy_Medium4_BW_2, Enemy_Medium4_BW_3, 27, new TimeSpan(0, 1, 5), new TimeSpan(0, 0, 0, 3, 0), 9);
                    enemySpawner.addSpawn(new Vector2(750, 100), 500, 2, 3, 200, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium_BW, Enemy_Medium4_1, Enemy_Medium4_2, Enemy_Medium4_3, Enemy_Medium4_BW_1, Enemy_Medium4_BW_2, Enemy_Medium4_BW_3, 27, new TimeSpan(0, 1, 5), new TimeSpan(0, 0, 0, 3, 0), 9);
                    enemySpawner.addSpawn(new Vector2(750, 150), 500, 2, 3, 200, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium_BW, Enemy_Medium4_1, Enemy_Medium4_2, Enemy_Medium4_3, Enemy_Medium4_BW_1, Enemy_Medium4_BW_2, Enemy_Medium4_BW_3, 27, new TimeSpan(0, 1, 5), new TimeSpan(0, 0, 0, 3, 0), 9);
                    enemySpawner.addSpawn(new Vector2(750, 200), 500, 2, 3, 200, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium_BW, Enemy_Medium4_1, Enemy_Medium4_2, Enemy_Medium4_3, Enemy_Medium4_BW_1, Enemy_Medium4_BW_2, Enemy_Medium4_BW_3, 27, new TimeSpan(0, 1, 5), new TimeSpan(0, 0, 0, 3, 0), 9);

                    enemySpawner.addSpawn(new Vector2(350, -75), 100, 4, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small4_1, Enemy_Small4_2, Enemy_Small4_3, Enemy_Small4_BW_1, Enemy_Small4_BW_2, Enemy_Small4_BW_3, 5, new TimeSpan(0, 1, 30), new TimeSpan(0, 0, 0, 1, 0), 49);
                    enemySpawner.addSpawn(new Vector2(250, -50), 750, 2, 1, 200, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium_BW, Enemy_Medium4_1, Enemy_Medium4_2, Enemy_Medium4_3, Enemy_Medium4_BW_1, Enemy_Medium4_BW_2, Enemy_Medium4_BW_3, 30, new TimeSpan(0, 1, 30), new TimeSpan(0, 0, 0, 5, 0), 9);
                    enemySpawner.addSpawn(new Vector2(375, -50), 750, 2, 1, 200, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium_BW, Enemy_Medium4_1, Enemy_Medium4_2, Enemy_Medium4_3, Enemy_Medium4_BW_1, Enemy_Medium4_BW_2, Enemy_Medium4_BW_3, 30, new TimeSpan(0, 1, 30), new TimeSpan(0, 0, 0, 5, 0), 9);
                    enemySpawner.addSpawn(new Vector2(-100, 100), 2000, 1, 4, 500, EnemyLaserProjectile_Large, EnemyLaserProjectile_Large_BW, Enemy_Large4_1, Enemy_Large4_2, Enemy_Large4_3, Enemy_Large4_BW_1, Enemy_Large4_BW_2, Enemy_Large4_BW_3, 150, new TimeSpan(0, 1, 31), new TimeSpan(0, 0, 0, 5, 0), 9);
                    enemySpawner.addSpawn(new Vector2(750, 100), 2000, 1, 5, 500, EnemyLaserProjectile_Large, EnemyLaserProjectile_Large_BW, Enemy_Large4_1, Enemy_Large4_2, Enemy_Large4_3, Enemy_Large4_BW_1, Enemy_Large4_BW_2, Enemy_Large4_BW_3, 150, new TimeSpan(0, 1, 31), new TimeSpan(0, 0, 0, 5, 0), 9);
                    enemySpawner.addSpawn(new Vector2(-100, 250), 2000, 1, 4, 500, EnemyLaserProjectile_Large, EnemyLaserProjectile_Large_BW, Enemy_Large4_1, Enemy_Large4_2, Enemy_Large4_3, Enemy_Large4_BW_1, Enemy_Large4_BW_2, Enemy_Large4_BW_3, 150, new TimeSpan(0, 1, 32), new TimeSpan(0, 0, 0, 5, 0), 9);
                    enemySpawner.addSpawn(new Vector2(750, 250), 2000, 1, 5, 500, EnemyLaserProjectile_Large, EnemyLaserProjectile_Large_BW, Enemy_Large4_1, Enemy_Large4_2, Enemy_Large4_3, Enemy_Large4_BW_1, Enemy_Large4_BW_2, Enemy_Large4_BW_3, 150, new TimeSpan(0, 1, 32), new TimeSpan(0, 0, 0, 5, 0), 9);
                    enemySpawner.addSpawn(new Vector2(-100, 400), 2000, 1, 4, 500, EnemyLaserProjectile_Large, EnemyLaserProjectile_Large_BW, Enemy_Large4_1, Enemy_Large4_2, Enemy_Large4_3, Enemy_Large4_BW_1, Enemy_Large4_BW_2, Enemy_Large4_BW_3, 150, new TimeSpan(0, 1, 33), new TimeSpan(0, 0, 0, 5, 0), 9);
                    enemySpawner.addSpawn(new Vector2(750, 400), 2000, 1, 5, 500, EnemyLaserProjectile_Large, EnemyLaserProjectile_Large_BW, Enemy_Large4_1, Enemy_Large4_2, Enemy_Large4_3, Enemy_Large4_BW_1, Enemy_Large4_BW_2, Enemy_Large4_BW_3, 150, new TimeSpan(0, 1, 33), new TimeSpan(0, 0, 0, 5, 0), 9);

                    enemySpawner.addSpawn(new Vector2(200, -75), 75, 6, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small4_1, Enemy_Small4_2, Enemy_Small4_3, Enemy_Small4_BW_1, Enemy_Small4_BW_2, Enemy_Small4_BW_3, 5, new TimeSpan(0, 2, 27), new TimeSpan(0, 0, 0, 2, 0), 23);
                    enemySpawner.addSpawn(new Vector2(500, -75), 75, 6, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small4_1, Enemy_Small4_2, Enemy_Small4_3, Enemy_Small4_BW_1, Enemy_Small4_BW_2, Enemy_Small4_BW_3, 5, new TimeSpan(0, 2, 28), new TimeSpan(0, 0, 0, 2, 0), 23);
                    enemySpawner.addSpawn(new Vector2(325, -100), 1500, 4, 1, 250, EnemyLaserProjectile_Large, EnemyLaserProjectile_Large_BW, Enemy_Large4_1, Enemy_Large4_2, Enemy_Large4_3, Enemy_Large4_BW_1, Enemy_Large4_BW_2, Enemy_Large4_BW_3, 150, new TimeSpan(0, 2, 30), new TimeSpan(0, 0, 0, 5, 0), 8);
                    enemySpawner.addSpawn(new Vector2(50, -100), 1500, 4, 1, 250, EnemyLaserProjectile_Large, EnemyLaserProjectile_Large_BW, Enemy_Large4_1, Enemy_Large4_2, Enemy_Large4_3, Enemy_Large4_BW_1, Enemy_Large4_BW_2, Enemy_Large4_BW_3, 150, new TimeSpan(0, 2, 31), new TimeSpan(0, 0, 0, 5, 0), 8);
                    enemySpawner.addSpawn(new Vector2(600, -100), 1500, 4, 1, 250, EnemyLaserProjectile_Large, EnemyLaserProjectile_Large_BW, Enemy_Large4_1, Enemy_Large4_2, Enemy_Large4_3, Enemy_Large4_BW_1, Enemy_Large4_BW_2, Enemy_Large4_BW_3, 150, new TimeSpan(0, 2, 32), new TimeSpan(0, 0, 0, 5, 0), 8);
                    enemySpawner.addSpawn(new Vector2(-100, 400), 500, 5, 4, 200, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium_BW, Enemy_Medium4_1, Enemy_Medium4_2, Enemy_Medium4_3, Enemy_Medium4_BW_1, Enemy_Medium4_BW_2, Enemy_Medium4_BW_3, 30, new TimeSpan(0, 2, 29), new TimeSpan(0, 0, 0, 8, 0), 4);
                    enemySpawner.addSpawn(new Vector2(-100, -50), 500, 5, 2, 200, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium_BW, Enemy_Medium4_1, Enemy_Medium4_2, Enemy_Medium4_3, Enemy_Medium4_BW_1, Enemy_Medium4_BW_2, Enemy_Medium4_BW_3, 30, new TimeSpan(0, 2, 30), new TimeSpan(0, 0, 0, 8, 0), 4);
                    enemySpawner.addSpawn(new Vector2(750, -50), 500, 5, 3, 200, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium_BW, Enemy_Medium4_1, Enemy_Medium4_2, Enemy_Medium4_3, Enemy_Medium4_BW_1, Enemy_Medium4_BW_2, Enemy_Medium4_BW_3, 30, new TimeSpan(0, 2, 31), new TimeSpan(0, 0, 0, 8, 0), 4);
                    enemySpawner.addSpawn(new Vector2(750, 400), 500, 5, 5, 200, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium_BW, Enemy_Medium4_1, Enemy_Medium4_2, Enemy_Medium4_3, Enemy_Medium4_BW_1, Enemy_Medium4_BW_2, Enemy_Medium4_BW_3, 30, new TimeSpan(0, 2, 32), new TimeSpan(0, 0, 0, 8, 0), 4);
                    enemySpawner.addSpawn(new Vector2(750, 400), 500, 5, 5, 200, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium_BW, Enemy_Medium4_1, Enemy_Medium4_2, Enemy_Medium4_3, Enemy_Medium4_BW_1, Enemy_Medium4_BW_2, Enemy_Medium4_BW_3, 30, new TimeSpan(0, 2, 33), new TimeSpan(0, 0, 0, 8, 0), 4);
                    enemySpawner.addSpawn(new Vector2(750, -50), 500, 5, 3, 200, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium_BW, Enemy_Medium4_1, Enemy_Medium4_2, Enemy_Medium4_3, Enemy_Medium4_BW_1, Enemy_Medium4_BW_2, Enemy_Medium4_BW_3, 30, new TimeSpan(0, 2, 34), new TimeSpan(0, 0, 0, 8, 0), 4);
                    enemySpawner.addSpawn(new Vector2(-100, -50), 500, 5, 2, 200, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium_BW, Enemy_Medium4_1, Enemy_Medium4_2, Enemy_Medium4_3, Enemy_Medium4_BW_1, Enemy_Medium4_BW_2, Enemy_Medium4_BW_3, 30, new TimeSpan(0, 2, 35), new TimeSpan(0, 0, 0, 8, 0), 4);
                    enemySpawner.addSpawn(new Vector2(-100, 400), 500, 5, 4, 200, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium_BW, Enemy_Medium4_1, Enemy_Medium4_2, Enemy_Medium4_3, Enemy_Medium4_BW_1, Enemy_Medium4_BW_2, Enemy_Medium4_BW_3, 30, new TimeSpan(0, 2, 36), new TimeSpan(0, 0, 0, 8, 0), 4);
                    

                    Boss = new Boss(new Vector2(30, -284), 100000, Boss4_1, Boss4_2, Boss4_3, Boss4_BW_1, Boss4_BW_2, Boss4_BW_3, new TimeSpan(0, 3, 20));
                    Boss.Weapons.Add(new Weapon(new Vector2(121, -78), MathHelper.ToRadians(220), 100, 9, 10, 2, 5, Laser, Laser_BW));
                    Boss.Weapons.Add(new Weapon(new Vector2(625, -78), MathHelper.ToRadians(140), 100, 9, 10, 2, 5, Laser, Laser_BW));
                    Boss.Weapons.Add(new Weapon(new Vector2(270, -85), MathHelper.ToRadians(160), 2000, 4, 15, 5, 1, LaserProjectile, LaserProjectile_BW));
                    Boss.Weapons.Add(new Weapon(new Vector2(470, -85), MathHelper.ToRadians(200), 2000, 4, 15, 5, 1, LaserProjectile, LaserProjectile_BW));
                    Boss.Weapons.Add(new Weapon(new Vector2(373, -30), MathHelper.ToRadians(180), 2000, 8, 5, 3, 6, Laser, Laser_BW));
                    Boss.Weapons.Add(new Weapon(new Vector2(373, -30), MathHelper.ToRadians(180), 500, 6, 5, 2, 6, Laser, Laser_BW));
                    Boss.addEvent(1, TimeSpan.Zero, 0, 300, 100, 0);
                    Boss.addEvent(3, new TimeSpan(0, 0, 3), 1, 1, 1, 0);
                    Boss.addEvent(3, new TimeSpan(0, 0, 3), 2, 1, 1, 0);
                    Boss.addEvent(2, new TimeSpan(0, 0, 3), 1, 15, 100, -1);
                    Boss.addEvent(2, new TimeSpan(0, 0, 3), 2, -15, 100, -1);
                    Boss.addEvent(3, new TimeSpan(0, 0, 4), 3, 1, 1, 0);
                    Boss.addEvent(3, new TimeSpan(0, 0, 5), 4, 1, 1, 0);
                    Boss.addEvent(2, new TimeSpan(0, 0, 4), 3, 30, 100, -1);
                    Boss.addEvent(2, new TimeSpan(0, 0, 5), 4, -30, 100, -1);
                    Boss.addEvent(3, 80000, 5, 1, 1, 0);
                    Boss.addEvent(3, 50000, 5, 0, 1, 0);
                    Boss.addEvent(3, 50000, 6, 1, 1, 0);
                    Boss.addEvent(2, 50000, 5, -30, 1, 0);
                    Boss.addEvent(2, 50000, 6, -30, 1, 0);
                    Boss.addEvent(2, 50000, 5, 120, 150, -1);
                    Boss.addEvent(2, 50000, 6, 120, 150, -1);
                    Boss.addEvent(2, 30000, 1, -10, 1, 0);
                    Boss.addEvent(2, 30000, 2, 10, 1, 0);
                    break;


                case 5:
                    enemySpawner = new EnemySpawner();
                    enemySpawner.addSpawn(new Vector2(350, -50), 10, 5, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small_1, Enemy_Small_2, Enemy_Small_3, Enemy_Small_BW_1, Enemy_Small_BW_2, Enemy_Small_BW_3, 200, new TimeSpan(0, 0, 3), new TimeSpan(0, 0, 0, 7, 0), 1);
                    enemySpawner.addSpawn(new Vector2(500, -50), 10, 5, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small_1, Enemy_Small_2, Enemy_Small_3, Enemy_Small_BW_1, Enemy_Small_BW_2, Enemy_Small_BW_3, 200, new TimeSpan(0, 0, 4), new TimeSpan(0, 0, 0, 7, 0), 1);
                    enemySpawner.addSpawn(new Vector2(200, -50), 10, 5, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small_1, Enemy_Small_2, Enemy_Small_3, Enemy_Small_BW_1, Enemy_Small_BW_2, Enemy_Small_BW_3, 200, new TimeSpan(0, 0, 5), new TimeSpan(0, 0, 0, 7, 0), 1);
                    enemySpawner.addSpawn(new Vector2(275, -50), 10, 5, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small_1, Enemy_Small_2, Enemy_Small_3, Enemy_Small_BW_1, Enemy_Small_BW_2, Enemy_Small_BW_3, 200, new TimeSpan(0, 0, 6), new TimeSpan(0, 0, 0, 7, 0), 1);
                    enemySpawner.addSpawn(new Vector2(650, -50), 10, 5, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small_1, Enemy_Small_2, Enemy_Small_3, Enemy_Small_BW_1, Enemy_Small_BW_2, Enemy_Small_BW_3, 200, new TimeSpan(0, 0, 7), new TimeSpan(0, 0, 0, 7, 0), 1);
                    enemySpawner.addSpawn(new Vector2(450, -50), 10, 5, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small_1, Enemy_Small_2, Enemy_Small_3, Enemy_Small_BW_1, Enemy_Small_BW_2, Enemy_Small_BW_3, 200, new TimeSpan(0, 0, 8), new TimeSpan(0, 0, 0, 7, 0), 1);
                    enemySpawner.addSpawn(new Vector2(50, -50), 10, 5, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small_1, Enemy_Small_2, Enemy_Small_3, Enemy_Small_BW_1, Enemy_Small_BW_2, Enemy_Small_BW_3, 200, new TimeSpan(0, 0, 9), new TimeSpan(0, 0, 0, 7, 0), 1);

                    enemySpawner.addSpawn(new Vector2(100, -50), 95, 2, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small5_1, Enemy_Small5_2, Enemy_Small5_3, Enemy_Small5_BW_1, Enemy_Small5_BW_2, Enemy_Small5_BW_3, 4, new TimeSpan(0, 0, 13), new TimeSpan(0, 0, 0, 0, 250), 99);
                    enemySpawner.addSpawn(new Vector2(600, -50), 95, 2, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small5_1, Enemy_Small5_2, Enemy_Small5_3, Enemy_Small5_BW_1, Enemy_Small5_BW_2, Enemy_Small5_BW_3, 4, new TimeSpan(0, 0, 13), new TimeSpan(0, 0, 0, 0, 250), 99);
                    enemySpawner.addSpawn(new Vector2(225, -100), 750, 6, 1, 250, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium_BW, Enemy_Medium5_1, Enemy_Medium5_2, Enemy_Medium5_3, Enemy_Medium5_BW_1, Enemy_Medium5_BW_2, Enemy_Medium5_BW_3, 50, new TimeSpan(0, 0, 16), new TimeSpan(0, 0, 0, 3, 0), 8);
                    enemySpawner.addSpawn(new Vector2(325, -100), 750, 6, 1, 250, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium_BW, Enemy_Medium5_1, Enemy_Medium5_2, Enemy_Medium5_3, Enemy_Medium5_BW_1, Enemy_Medium5_BW_2, Enemy_Medium5_BW_3, 50, new TimeSpan(0, 0, 16), new TimeSpan(0, 0, 0, 3, 0), 8);
                    enemySpawner.addSpawn(new Vector2(425, -100), 750, 6, 1, 250, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium_BW, Enemy_Medium5_1, Enemy_Medium5_2, Enemy_Medium5_3, Enemy_Medium5_BW_1, Enemy_Medium5_BW_2, Enemy_Medium5_BW_3, 50, new TimeSpan(0, 0, 16), new TimeSpan(0, 0, 0, 3, 0), 8);
                    enemySpawner.addSpawn(new Vector2(-150, 400), 1500, 3, 4, 500, EnemyLaserProjectile_Large, EnemyLaserProjectile_Large_BW, Enemy_Large5_1, Enemy_Large5_2, Enemy_Large5_3, Enemy_Large5_BW_1, Enemy_Large5_BW_2, Enemy_Large5_BW_3, 130, new TimeSpan(0, 0, 18), new TimeSpan(0, 0, 0, 2, 0), 9);
                    enemySpawner.addSpawn(new Vector2(750, 400), 1500, 3, 5, 500, EnemyLaserProjectile_Large, EnemyLaserProjectile_Large_BW, Enemy_Large5_1, Enemy_Large5_2, Enemy_Large5_3, Enemy_Large5_BW_1, Enemy_Large5_BW_2, Enemy_Large5_BW_3, 130, new TimeSpan(0, 0, 18), new TimeSpan(0, 0, 0, 2, 0), 9);

                    enemySpawner.addSpawn(new Vector2(-50, 0), 75, 3, 4, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small5_1, Enemy_Small5_2, Enemy_Small5_3, Enemy_Small5_BW_1, Enemy_Small5_BW_2, Enemy_Small5_BW_3, 3, new TimeSpan(0, 0, 45), new TimeSpan(0, 0, 0, 0, 500), 4);
                    enemySpawner.addSpawn(new Vector2(-50, 50), 75, 3, 4, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small5_1, Enemy_Small5_2, Enemy_Small5_3, Enemy_Small5_BW_1, Enemy_Small5_BW_2, Enemy_Small5_BW_3, 3, new TimeSpan(0, 0, 45), new TimeSpan(0, 0, 0, 0, 500), 4);
                    enemySpawner.addSpawn(new Vector2(-50, 100), 75, 3, 4, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small5_1, Enemy_Small5_2, Enemy_Small5_3, Enemy_Small5_BW_1, Enemy_Small5_BW_2, Enemy_Small5_BW_3, 3, new TimeSpan(0, 0, 45), new TimeSpan(0, 0, 0, 0, 500), 4);
                    enemySpawner.addSpawn(new Vector2(-50, 150), 75, 3, 4, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small5_1, Enemy_Small5_2, Enemy_Small5_3, Enemy_Small5_BW_1, Enemy_Small5_BW_2, Enemy_Small5_BW_3, 3, new TimeSpan(0, 0, 45), new TimeSpan(0, 0, 0, 0, 500), 4);
                    enemySpawner.addSpawn(new Vector2(-50, 200), 75, 3, 4, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small5_1, Enemy_Small5_2, Enemy_Small5_3, Enemy_Small5_BW_1, Enemy_Small5_BW_2, Enemy_Small5_BW_3, 3, new TimeSpan(0, 0, 45), new TimeSpan(0, 0, 0, 0, 500), 4);
                    enemySpawner.addSpawn(new Vector2(-50, 300), 75, 3, 4, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small5_1, Enemy_Small5_2, Enemy_Small5_3, Enemy_Small5_BW_1, Enemy_Small5_BW_2, Enemy_Small5_BW_3, 3, new TimeSpan(0, 0, 45), new TimeSpan(0, 0, 0, 0, 500), 4);
                    enemySpawner.addSpawn(new Vector2(-50, 350), 75, 3, 4, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small5_1, Enemy_Small5_2, Enemy_Small5_3, Enemy_Small5_BW_1, Enemy_Small5_BW_2, Enemy_Small5_BW_3, 3, new TimeSpan(0, 0, 45), new TimeSpan(0, 0, 0, 0, 500), 4);
                    enemySpawner.addSpawn(new Vector2(-50, 400), 75, 3, 4, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small5_1, Enemy_Small5_2, Enemy_Small5_3, Enemy_Small5_BW_1, Enemy_Small5_BW_2, Enemy_Small5_BW_3, 3, new TimeSpan(0, 0, 45), new TimeSpan(0, 0, 0, 0, 500), 4);
                    enemySpawner.addSpawn(new Vector2(-50, 450), 75, 3, 4, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small5_1, Enemy_Small5_2, Enemy_Small5_3, Enemy_Small5_BW_1, Enemy_Small5_BW_2, Enemy_Small5_BW_3, 3, new TimeSpan(0, 0, 45), new TimeSpan(0, 0, 0, 0, 500), 4);
                    enemySpawner.addSpawn(new Vector2(-50, 500), 75, 3, 4, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small5_1, Enemy_Small5_2, Enemy_Small5_3, Enemy_Small5_BW_1, Enemy_Small5_BW_2, Enemy_Small5_BW_3, 3, new TimeSpan(0, 0, 45), new TimeSpan(0, 0, 0, 0, 500), 4);
                    enemySpawner.addSpawn(new Vector2(-50, 550), 75, 3, 4, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small5_1, Enemy_Small5_2, Enemy_Small5_3, Enemy_Small5_BW_1, Enemy_Small5_BW_2, Enemy_Small5_BW_3, 3, new TimeSpan(0, 0, 45), new TimeSpan(0, 0, 0, 0, 500), 4);
                    enemySpawner.addSpawn(new Vector2(-50, 600), 75, 3, 4, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small5_1, Enemy_Small5_2, Enemy_Small5_3, Enemy_Small5_BW_1, Enemy_Small5_BW_2, Enemy_Small5_BW_3, 3, new TimeSpan(0, 0, 45), new TimeSpan(0, 0, 0, 0, 500), 4);
                    enemySpawner.addSpawn(new Vector2(-50, 650), 75, 3, 4, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small5_1, Enemy_Small5_2, Enemy_Small5_3, Enemy_Small5_BW_1, Enemy_Small5_BW_2, Enemy_Small5_BW_3, 3, new TimeSpan(0, 0, 45), new TimeSpan(0, 0, 0, 0, 500), 4);
                    enemySpawner.addSpawn(new Vector2(-50, 700), 75, 3, 4, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small5_1, Enemy_Small5_2, Enemy_Small5_3, Enemy_Small5_BW_1, Enemy_Small5_BW_2, Enemy_Small5_BW_3, 3, new TimeSpan(0, 0, 45), new TimeSpan(0, 0, 0, 0, 500), 4);
                    enemySpawner.addSpawn(new Vector2(-50, 750), 75, 3, 4, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small5_1, Enemy_Small5_2, Enemy_Small5_3, Enemy_Small5_BW_1, Enemy_Small5_BW_2, Enemy_Small5_BW_3, 3, new TimeSpan(0, 0, 45), new TimeSpan(0, 0, 0, 0, 500), 4);
                    enemySpawner.addSpawn(new Vector2(-50, 800), 75, 3, 4, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small5_1, Enemy_Small5_2, Enemy_Small5_3, Enemy_Small5_BW_1, Enemy_Small5_BW_2, Enemy_Small5_BW_3, 3, new TimeSpan(0, 0, 45), new TimeSpan(0, 0, 0, 0, 500), 4);
                    enemySpawner.addSpawn(new Vector2(-50, 850), 75, 3, 4, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small5_1, Enemy_Small5_2, Enemy_Small5_3, Enemy_Small5_BW_1, Enemy_Small5_BW_2, Enemy_Small5_BW_3, 3, new TimeSpan(0, 0, 45), new TimeSpan(0, 0, 0, 0, 500), 4);

                    enemySpawner.addSpawn(new Vector2(750, 0), 75, 3, 5, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small5_1, Enemy_Small5_2, Enemy_Small5_3, Enemy_Small5_BW_1, Enemy_Small5_BW_2, Enemy_Small5_BW_3, 3, new TimeSpan(0, 0, 52), new TimeSpan(0, 0, 0, 0, 500), 4);
                    enemySpawner.addSpawn(new Vector2(750, 50), 75, 3, 5, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small5_1, Enemy_Small5_2, Enemy_Small5_3, Enemy_Small5_BW_1, Enemy_Small5_BW_2, Enemy_Small5_BW_3, 3, new TimeSpan(0, 0, 52), new TimeSpan(0, 0, 0, 0, 500), 4);
                    enemySpawner.addSpawn(new Vector2(750, 100), 75, 3, 5, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small5_1, Enemy_Small5_2, Enemy_Small5_3, Enemy_Small5_BW_1, Enemy_Small5_BW_2, Enemy_Small5_BW_3, 3, new TimeSpan(0, 0, 52), new TimeSpan(0, 0, 0, 0, 500), 4);
                    enemySpawner.addSpawn(new Vector2(750, 150), 75, 3, 5, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small5_1, Enemy_Small5_2, Enemy_Small5_3, Enemy_Small5_BW_1, Enemy_Small5_BW_2, Enemy_Small5_BW_3, 3, new TimeSpan(0, 0, 52), new TimeSpan(0, 0, 0, 0, 500), 4);
                    enemySpawner.addSpawn(new Vector2(750, 200), 75, 3, 5, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small5_1, Enemy_Small5_2, Enemy_Small5_3, Enemy_Small5_BW_1, Enemy_Small5_BW_2, Enemy_Small5_BW_3, 3, new TimeSpan(0, 0, 52), new TimeSpan(0, 0, 0, 0, 500), 4);
                    enemySpawner.addSpawn(new Vector2(750, 250), 75, 3, 5, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small5_1, Enemy_Small5_2, Enemy_Small5_3, Enemy_Small5_BW_1, Enemy_Small5_BW_2, Enemy_Small5_BW_3, 3, new TimeSpan(0, 0, 52), new TimeSpan(0, 0, 0, 0, 500), 4);
                    enemySpawner.addSpawn(new Vector2(750, 300), 75, 3, 5, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small5_1, Enemy_Small5_2, Enemy_Small5_3, Enemy_Small5_BW_1, Enemy_Small5_BW_2, Enemy_Small5_BW_3, 3, new TimeSpan(0, 0, 52), new TimeSpan(0, 0, 0, 0, 500), 4);
                    enemySpawner.addSpawn(new Vector2(750, 350), 75, 3, 5, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small5_1, Enemy_Small5_2, Enemy_Small5_3, Enemy_Small5_BW_1, Enemy_Small5_BW_2, Enemy_Small5_BW_3, 3, new TimeSpan(0, 0, 52), new TimeSpan(0, 0, 0, 0, 500), 4);
                    enemySpawner.addSpawn(new Vector2(750, 400), 75, 3, 5, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small5_1, Enemy_Small5_2, Enemy_Small5_3, Enemy_Small5_BW_1, Enemy_Small5_BW_2, Enemy_Small5_BW_3, 3, new TimeSpan(0, 0, 52), new TimeSpan(0, 0, 0, 0, 500), 4);
                    enemySpawner.addSpawn(new Vector2(750, 450), 75, 3, 5, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small5_1, Enemy_Small5_2, Enemy_Small5_3, Enemy_Small5_BW_1, Enemy_Small5_BW_2, Enemy_Small5_BW_3, 3, new TimeSpan(0, 0, 52), new TimeSpan(0, 0, 0, 0, 500), 4);
                    enemySpawner.addSpawn(new Vector2(750, 500), 75, 3, 5, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small5_1, Enemy_Small5_2, Enemy_Small5_3, Enemy_Small5_BW_1, Enemy_Small5_BW_2, Enemy_Small5_BW_3, 3, new TimeSpan(0, 0, 52), new TimeSpan(0, 0, 0, 0, 500), 4);
                    enemySpawner.addSpawn(new Vector2(750, 600), 75, 3, 5, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small5_1, Enemy_Small5_2, Enemy_Small5_3, Enemy_Small5_BW_1, Enemy_Small5_BW_2, Enemy_Small5_BW_3, 3, new TimeSpan(0, 0, 52), new TimeSpan(0, 0, 0, 0, 500), 4);
                    enemySpawner.addSpawn(new Vector2(750, 650), 75, 3, 5, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small5_1, Enemy_Small5_2, Enemy_Small5_3, Enemy_Small5_BW_1, Enemy_Small5_BW_2, Enemy_Small5_BW_3, 3, new TimeSpan(0, 0, 52), new TimeSpan(0, 0, 0, 0, 500), 4);
                    enemySpawner.addSpawn(new Vector2(750, 700), 75, 3, 5, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small5_1, Enemy_Small5_2, Enemy_Small5_3, Enemy_Small5_BW_1, Enemy_Small5_BW_2, Enemy_Small5_BW_3, 3, new TimeSpan(0, 0, 52), new TimeSpan(0, 0, 0, 0, 500), 4);
                    enemySpawner.addSpawn(new Vector2(750, 750), 75, 3, 5, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small5_1, Enemy_Small5_2, Enemy_Small5_3, Enemy_Small5_BW_1, Enemy_Small5_BW_2, Enemy_Small5_BW_3, 3, new TimeSpan(0, 0, 52), new TimeSpan(0, 0, 0, 0, 500), 4);
                    enemySpawner.addSpawn(new Vector2(750, 800), 75, 3, 5, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small5_1, Enemy_Small5_2, Enemy_Small5_3, Enemy_Small5_BW_1, Enemy_Small5_BW_2, Enemy_Small5_BW_3, 3, new TimeSpan(0, 0, 52), new TimeSpan(0, 0, 0, 0, 500), 4);
                    enemySpawner.addSpawn(new Vector2(750, 850), 75, 3, 5, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small5_1, Enemy_Small5_2, Enemy_Small5_3, Enemy_Small5_BW_1, Enemy_Small5_BW_2, Enemy_Small5_BW_3, 3, new TimeSpan(0, 0, 52), new TimeSpan(0, 0, 0, 0, 500), 4);
                    
                    enemySpawner.addSpawn(new Vector2(-50, 0), 75, 3, 4, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small5_1, Enemy_Small5_2, Enemy_Small5_3, Enemy_Small5_BW_1, Enemy_Small5_BW_2, Enemy_Small5_BW_3, 3, new TimeSpan(0, 0, 59), new TimeSpan(0, 0, 0, 0, 500), 4);
                    enemySpawner.addSpawn(new Vector2(-50, 50), 75, 3, 4, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small5_1, Enemy_Small5_2, Enemy_Small5_3, Enemy_Small5_BW_1, Enemy_Small5_BW_2, Enemy_Small5_BW_3, 3, new TimeSpan(0, 0, 59), new TimeSpan(0, 0, 0, 0, 500), 4);
                    enemySpawner.addSpawn(new Vector2(-50, 100), 75, 3, 4, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small5_1, Enemy_Small5_2, Enemy_Small5_3, Enemy_Small5_BW_1, Enemy_Small5_BW_2, Enemy_Small5_BW_3, 3, new TimeSpan(0, 0, 59), new TimeSpan(0, 0, 0, 0, 500), 4);
                    enemySpawner.addSpawn(new Vector2(-50, 150), 75, 3, 4, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small5_1, Enemy_Small5_2, Enemy_Small5_3, Enemy_Small5_BW_1, Enemy_Small5_BW_2, Enemy_Small5_BW_3, 3, new TimeSpan(0, 0, 59), new TimeSpan(0, 0, 0, 0, 500), 4);
                    enemySpawner.addSpawn(new Vector2(-50, 200), 75, 3, 4, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small5_1, Enemy_Small5_2, Enemy_Small5_3, Enemy_Small5_BW_1, Enemy_Small5_BW_2, Enemy_Small5_BW_3, 3, new TimeSpan(0, 0, 59), new TimeSpan(0, 0, 0, 0, 500), 4);
                    enemySpawner.addSpawn(new Vector2(-50, 250), 75, 3, 4, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small5_1, Enemy_Small5_2, Enemy_Small5_3, Enemy_Small5_BW_1, Enemy_Small5_BW_2, Enemy_Small5_BW_3, 3, new TimeSpan(0, 0, 59), new TimeSpan(0, 0, 0, 0, 500), 4);
                    enemySpawner.addSpawn(new Vector2(-50, 300), 75, 3, 4, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small5_1, Enemy_Small5_2, Enemy_Small5_3, Enemy_Small5_BW_1, Enemy_Small5_BW_2, Enemy_Small5_BW_3, 3, new TimeSpan(0, 0, 59), new TimeSpan(0, 0, 0, 0, 500), 4);
                    enemySpawner.addSpawn(new Vector2(-50, 350), 75, 3, 4, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small5_1, Enemy_Small5_2, Enemy_Small5_3, Enemy_Small5_BW_1, Enemy_Small5_BW_2, Enemy_Small5_BW_3, 3, new TimeSpan(0, 0, 59), new TimeSpan(0, 0, 0, 0, 500), 4);
                    enemySpawner.addSpawn(new Vector2(-50, 450), 75, 3, 4, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small5_1, Enemy_Small5_2, Enemy_Small5_3, Enemy_Small5_BW_1, Enemy_Small5_BW_2, Enemy_Small5_BW_3, 3, new TimeSpan(0, 0, 59), new TimeSpan(0, 0, 0, 0, 500), 4);
                    enemySpawner.addSpawn(new Vector2(-50, 500), 75, 3, 4, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small5_1, Enemy_Small5_2, Enemy_Small5_3, Enemy_Small5_BW_1, Enemy_Small5_BW_2, Enemy_Small5_BW_3, 3, new TimeSpan(0, 0, 59), new TimeSpan(0, 0, 0, 0, 500), 4);
                    enemySpawner.addSpawn(new Vector2(-50, 550), 75, 3, 4, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small5_1, Enemy_Small5_2, Enemy_Small5_3, Enemy_Small5_BW_1, Enemy_Small5_BW_2, Enemy_Small5_BW_3, 3, new TimeSpan(0, 0, 59), new TimeSpan(0, 0, 0, 0, 500), 4);
                    enemySpawner.addSpawn(new Vector2(-50, 600), 75, 3, 4, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small5_1, Enemy_Small5_2, Enemy_Small5_3, Enemy_Small5_BW_1, Enemy_Small5_BW_2, Enemy_Small5_BW_3, 3, new TimeSpan(0, 0, 59), new TimeSpan(0, 0, 0, 0, 500), 4);
                    enemySpawner.addSpawn(new Vector2(-50, 650), 75, 3, 4, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small5_1, Enemy_Small5_2, Enemy_Small5_3, Enemy_Small5_BW_1, Enemy_Small5_BW_2, Enemy_Small5_BW_3, 3, new TimeSpan(0, 0, 59), new TimeSpan(0, 0, 0, 0, 500), 4);
                    enemySpawner.addSpawn(new Vector2(-50, 700), 75, 3, 4, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small5_1, Enemy_Small5_2, Enemy_Small5_3, Enemy_Small5_BW_1, Enemy_Small5_BW_2, Enemy_Small5_BW_3, 3, new TimeSpan(0, 0, 59), new TimeSpan(0, 0, 0, 0, 500), 4);
                    enemySpawner.addSpawn(new Vector2(-50, 750), 75, 3, 4, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small5_1, Enemy_Small5_2, Enemy_Small5_3, Enemy_Small5_BW_1, Enemy_Small5_BW_2, Enemy_Small5_BW_3, 3, new TimeSpan(0, 0, 59), new TimeSpan(0, 0, 0, 0, 500), 4);
                    enemySpawner.addSpawn(new Vector2(-50, 800), 75, 3, 4, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small5_1, Enemy_Small5_2, Enemy_Small5_3, Enemy_Small5_BW_1, Enemy_Small5_BW_2, Enemy_Small5_BW_3, 3, new TimeSpan(0, 0, 59), new TimeSpan(0, 0, 0, 0, 500), 4);
                    enemySpawner.addSpawn(new Vector2(-50, 850), 75, 3, 4, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small5_1, Enemy_Small5_2, Enemy_Small5_3, Enemy_Small5_BW_1, Enemy_Small5_BW_2, Enemy_Small5_BW_3, 3, new TimeSpan(0, 0, 59), new TimeSpan(0, 0, 0, 0, 500), 4);
                    enemySpawner.addSpawn(new Vector2(750, 0), 75, 3, 5, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small5_1, Enemy_Small5_2, Enemy_Small5_3, Enemy_Small5_BW_1, Enemy_Small5_BW_2, Enemy_Small5_BW_3, 3, new TimeSpan(0, 0, 59), new TimeSpan(0, 0, 0, 0, 500), 4);
                    enemySpawner.addSpawn(new Vector2(750, 50), 75, 3, 5, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small5_1, Enemy_Small5_2, Enemy_Small5_3, Enemy_Small5_BW_1, Enemy_Small5_BW_2, Enemy_Small5_BW_3, 3, new TimeSpan(0, 0, 59), new TimeSpan(0, 0, 0, 0, 500), 4);
                    enemySpawner.addSpawn(new Vector2(750, 100), 75, 3, 5, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small5_1, Enemy_Small5_2, Enemy_Small5_3, Enemy_Small5_BW_1, Enemy_Small5_BW_2, Enemy_Small5_BW_3, 3, new TimeSpan(0, 0, 59), new TimeSpan(0, 0, 0, 0, 500), 4);
                    enemySpawner.addSpawn(new Vector2(750, 150), 75, 3, 5, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small5_1, Enemy_Small5_2, Enemy_Small5_3, Enemy_Small5_BW_1, Enemy_Small5_BW_2, Enemy_Small5_BW_3, 3, new TimeSpan(0, 0, 59), new TimeSpan(0, 0, 0, 0, 500), 4);
                    enemySpawner.addSpawn(new Vector2(750, 200), 75, 3, 5, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small5_1, Enemy_Small5_2, Enemy_Small5_3, Enemy_Small5_BW_1, Enemy_Small5_BW_2, Enemy_Small5_BW_3, 3, new TimeSpan(0, 0, 59), new TimeSpan(0, 0, 0, 0, 500), 4);
                    enemySpawner.addSpawn(new Vector2(750, 250), 75, 3, 5, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small5_1, Enemy_Small5_2, Enemy_Small5_3, Enemy_Small5_BW_1, Enemy_Small5_BW_2, Enemy_Small5_BW_3, 3, new TimeSpan(0, 0, 59), new TimeSpan(0, 0, 0, 0, 500), 4);
                    enemySpawner.addSpawn(new Vector2(750, 300), 75, 3, 5, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small5_1, Enemy_Small5_2, Enemy_Small5_3, Enemy_Small5_BW_1, Enemy_Small5_BW_2, Enemy_Small5_BW_3, 3, new TimeSpan(0, 0, 59), new TimeSpan(0, 0, 0, 0, 500), 4);
                    enemySpawner.addSpawn(new Vector2(750, 350), 75, 3, 5, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small5_1, Enemy_Small5_2, Enemy_Small5_3, Enemy_Small5_BW_1, Enemy_Small5_BW_2, Enemy_Small5_BW_3, 3, new TimeSpan(0, 0, 59), new TimeSpan(0, 0, 0, 0, 500), 4);
                    enemySpawner.addSpawn(new Vector2(750, 450), 75, 3, 5, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small5_1, Enemy_Small5_2, Enemy_Small5_3, Enemy_Small5_BW_1, Enemy_Small5_BW_2, Enemy_Small5_BW_3, 3, new TimeSpan(0, 0, 59), new TimeSpan(0, 0, 0, 0, 500), 4);
                    enemySpawner.addSpawn(new Vector2(750, 500), 75, 3, 5, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small5_1, Enemy_Small5_2, Enemy_Small5_3, Enemy_Small5_BW_1, Enemy_Small5_BW_2, Enemy_Small5_BW_3, 3, new TimeSpan(0, 0, 59), new TimeSpan(0, 0, 0, 0, 500), 4);
                    enemySpawner.addSpawn(new Vector2(750, 550), 75, 3, 5, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small5_1, Enemy_Small5_2, Enemy_Small5_3, Enemy_Small5_BW_1, Enemy_Small5_BW_2, Enemy_Small5_BW_3, 3, new TimeSpan(0, 0, 59), new TimeSpan(0, 0, 0, 0, 500), 4);
                    enemySpawner.addSpawn(new Vector2(750, 600), 75, 3, 5, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small5_1, Enemy_Small5_2, Enemy_Small5_3, Enemy_Small5_BW_1, Enemy_Small5_BW_2, Enemy_Small5_BW_3, 3, new TimeSpan(0, 0, 59), new TimeSpan(0, 0, 0, 0, 500), 4);
                    enemySpawner.addSpawn(new Vector2(750, 650), 75, 3, 5, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small5_1, Enemy_Small5_2, Enemy_Small5_3, Enemy_Small5_BW_1, Enemy_Small5_BW_2, Enemy_Small5_BW_3, 3, new TimeSpan(0, 0, 59), new TimeSpan(0, 0, 0, 0, 500), 4);
                    enemySpawner.addSpawn(new Vector2(750, 700), 75, 3, 5, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small5_1, Enemy_Small5_2, Enemy_Small5_3, Enemy_Small5_BW_1, Enemy_Small5_BW_2, Enemy_Small5_BW_3, 3, new TimeSpan(0, 0, 59), new TimeSpan(0, 0, 0, 0, 500), 4);
                    enemySpawner.addSpawn(new Vector2(750, 750), 75, 3, 5, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small5_1, Enemy_Small5_2, Enemy_Small5_3, Enemy_Small5_BW_1, Enemy_Small5_BW_2, Enemy_Small5_BW_3, 3, new TimeSpan(0, 0, 59), new TimeSpan(0, 0, 0, 0, 500), 4);
                    enemySpawner.addSpawn(new Vector2(750, 800), 75, 3, 5, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small5_1, Enemy_Small5_2, Enemy_Small5_3, Enemy_Small5_BW_1, Enemy_Small5_BW_2, Enemy_Small5_BW_3, 3, new TimeSpan(0, 0, 59), new TimeSpan(0, 0, 0, 0, 500), 4);
                    enemySpawner.addSpawn(new Vector2(750, 850), 75, 3, 5, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small5_1, Enemy_Small5_2, Enemy_Small5_3, Enemy_Small5_BW_1, Enemy_Small5_BW_2, Enemy_Small5_BW_3, 3, new TimeSpan(0, 0, 59), new TimeSpan(0, 0, 0, 0, 500), 4);

                    enemySpawner.addSpawn(new Vector2(750, 200), 75, -3, 2, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small5_1, Enemy_Small5_2, Enemy_Small5_3, Enemy_Small5_BW_1, Enemy_Small5_BW_2, Enemy_Small5_BW_3, 3, new TimeSpan(0, 1, 8), new TimeSpan(0, 0, 0, 0, 500), 99);
                    enemySpawner.addSpawn(new Vector2(200, -50), 75, 3, 3, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small5_1, Enemy_Small5_2, Enemy_Small5_3, Enemy_Small5_BW_1, Enemy_Small5_BW_2, Enemy_Small5_BW_3, 3, new TimeSpan(0, 1, 10), new TimeSpan(0, 0, 0, 0, 500), 95);
                    enemySpawner.addSpawn(new Vector2(-50, 650), 75, 3, 2, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small5_1, Enemy_Small5_2, Enemy_Small5_3, Enemy_Small5_BW_1, Enemy_Small5_BW_2, Enemy_Small5_BW_3, 3, new TimeSpan(0, 1, 12), new TimeSpan(0, 0, 0, 0, 500), 91);
                    enemySpawner.addSpawn(new Vector2(500, 900), 75, -3, 3, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small5_1, Enemy_Small5_2, Enemy_Small5_3, Enemy_Small5_BW_1, Enemy_Small5_BW_2, Enemy_Small5_BW_3, 3, new TimeSpan(0, 1, 14), new TimeSpan(0, 0, 0, 0, 500), 87);
                    enemySpawner.addSpawn(new Vector2(750, 0), 75, 6, 5, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small5_1, Enemy_Small5_2, Enemy_Small5_3, Enemy_Small5_BW_1, Enemy_Small5_BW_2, Enemy_Small5_BW_3, 3, new TimeSpan(0, 1, 16), new TimeSpan(0, 0, 0, 0, 500), 15);
                    enemySpawner.addSpawn(new Vector2(0, -50), 75, 6, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small5_1, Enemy_Small5_2, Enemy_Small5_3, Enemy_Small5_BW_1, Enemy_Small5_BW_2, Enemy_Small5_BW_3, 3, new TimeSpan(0, 1, 18), new TimeSpan(0, 0, 0, 0, 500), 23);
                    enemySpawner.addSpawn(new Vector2(-50, 850), 75, 6, 4, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small5_1, Enemy_Small5_2, Enemy_Small5_3, Enemy_Small5_BW_1, Enemy_Small5_BW_2, Enemy_Small5_BW_3, 3, new TimeSpan(0, 1, 20), new TimeSpan(0, 0, 0, 0, 500), 33);
                    enemySpawner.addSpawn(new Vector2(700, 900), 75, -6, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small5_1, Enemy_Small5_2, Enemy_Small5_3, Enemy_Small5_BW_1, Enemy_Small5_BW_2, Enemy_Small5_BW_3, 3, new TimeSpan(0, 1, 22), new TimeSpan(0, 0, 0, 0, 500), 15);
                    enemySpawner.addSpawn(new Vector2(225, -150), 1500, 5, 1, 1500, EnemyLaserProjectile_Large, EnemyLaserProjectile_Large_BW, Enemy_Large5_1, Enemy_Large5_2, Enemy_Large5_3, Enemy_Large5_BW_1, Enemy_Large5_BW_2, Enemy_Large5_BW_3, 130, new TimeSpan(0, 1, 25), new TimeSpan(0, 0, 0, 1, 0), 3);
                    enemySpawner.addSpawn(new Vector2(375, -150), 1500, 5, 1, 1500, EnemyLaserProjectile_Large, EnemyLaserProjectile_Large_BW, Enemy_Large5_1, Enemy_Large5_2, Enemy_Large5_3, Enemy_Large5_BW_1, Enemy_Large5_BW_2, Enemy_Large5_BW_3, 130, new TimeSpan(0, 1, 25), new TimeSpan(0, 0, 0, 1, 0), 3);
                    enemySpawner.addSpawn(new Vector2(750, 0), 75, 6, 5, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small5_1, Enemy_Small5_2, Enemy_Small5_3, Enemy_Small5_BW_1, Enemy_Small5_BW_2, Enemy_Small5_BW_3, 3, new TimeSpan(0, 1, 28), new TimeSpan(0, 0, 0, 0, 500), 29);
                    enemySpawner.addSpawn(new Vector2(-100, 350), 750, 4, 4, 500, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium_BW, Enemy_Medium5_1, Enemy_Medium5_2, Enemy_Medium5_3, Enemy_Medium5_BW_1, Enemy_Medium5_BW_2, Enemy_Medium5_BW_3, 50, new TimeSpan(0, 1, 32), new TimeSpan(0, 0, 0, 0, 750), 4);
                    enemySpawner.addSpawn(new Vector2(-100, 450), 750, 4, 4, 500, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium_BW, Enemy_Medium5_1, Enemy_Medium5_2, Enemy_Medium5_3, Enemy_Medium5_BW_1, Enemy_Medium5_BW_2, Enemy_Medium5_BW_3, 50, new TimeSpan(0, 1, 32), new TimeSpan(0, 0, 0, 0, 750), 4);
                    enemySpawner.addSpawn(new Vector2(750, 350), 750, 4, 5, 500, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium_BW, Enemy_Medium5_1, Enemy_Medium5_2, Enemy_Medium5_3, Enemy_Medium5_BW_1, Enemy_Medium5_BW_2, Enemy_Medium5_BW_3, 50, new TimeSpan(0, 1, 32), new TimeSpan(0, 0, 0, 0, 750), 4);
                    enemySpawner.addSpawn(new Vector2(750, 450), 750, 4, 5, 500, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium_BW, Enemy_Medium5_1, Enemy_Medium5_2, Enemy_Medium5_3, Enemy_Medium5_BW_1, Enemy_Medium5_BW_2, Enemy_Medium5_BW_3, 50, new TimeSpan(0, 1, 32), new TimeSpan(0, 0, 0, 0, 750), 4);
                    enemySpawner.addSpawn(new Vector2(0, -50), 75, 6, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small5_1, Enemy_Small5_2, Enemy_Small5_3, Enemy_Small5_BW_1, Enemy_Small5_BW_2, Enemy_Small5_BW_3, 3, new TimeSpan(0, 1, 35), new TimeSpan(0, 0, 0, 0, 500), 15);
                    enemySpawner.addSpawn(new Vector2(700, 900), 75, -6, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small5_1, Enemy_Small5_2, Enemy_Small5_3, Enemy_Small5_BW_1, Enemy_Small5_BW_2, Enemy_Small5_BW_3, 3, new TimeSpan(0, 1, 35), new TimeSpan(0, 0, 0, 0, 500), 15);
                    enemySpawner.addSpawn(new Vector2(225, 900), 1500, -5, 1, 1500, EnemyLaserProjectile_Large, EnemyLaserProjectile_Large_BW, Enemy_Large5_1, Enemy_Large5_2, Enemy_Large5_3, Enemy_Large5_BW_1, Enemy_Large5_BW_2, Enemy_Large5_BW_3, 130, new TimeSpan(0, 1, 37), new TimeSpan(0, 0, 0, 1, 0), 3);
                    enemySpawner.addSpawn(new Vector2(375, 900), 1500, -5, 1, 1500, EnemyLaserProjectile_Large, EnemyLaserProjectile_Large_BW, Enemy_Large5_1, Enemy_Large5_2, Enemy_Large5_3, Enemy_Large5_BW_1, Enemy_Large5_BW_2, Enemy_Large5_BW_3, 130, new TimeSpan(0, 1, 37), new TimeSpan(0, 0, 0, 1, 0), 3);
                    enemySpawner.addSpawn(new Vector2(-50, 850), 75, 6, 4, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small5_1, Enemy_Small5_2, Enemy_Small5_3, Enemy_Small5_BW_1, Enemy_Small5_BW_2, Enemy_Small5_BW_3, 3, new TimeSpan(0, 1, 40), new TimeSpan(0, 0, 0, 0, 500), 5);
                    enemySpawner.addSpawn(new Vector2(225, -150), 1500, 5, 1, 1500, EnemyLaserProjectile_Large, EnemyLaserProjectile_Large_BW, Enemy_Large5_1, Enemy_Large5_2, Enemy_Large5_3, Enemy_Large5_BW_1, Enemy_Large5_BW_2, Enemy_Large5_BW_3, 130, new TimeSpan(0, 1, 45), new TimeSpan(0, 0, 0, 1, 0), 3);
                    enemySpawner.addSpawn(new Vector2(375, -150), 1500, 5, 1, 1500, EnemyLaserProjectile_Large, EnemyLaserProjectile_Large_BW, Enemy_Large5_1, Enemy_Large5_2, Enemy_Large5_3, Enemy_Large5_BW_1, Enemy_Large5_BW_2, Enemy_Large5_BW_3, 130, new TimeSpan(0, 1, 45), new TimeSpan(0, 0, 0, 1, 0), 3);
                    enemySpawner.addSpawn(new Vector2(-100, 350), 750, 4, 4, 500, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium_BW, Enemy_Medium5_1, Enemy_Medium5_2, Enemy_Medium5_3, Enemy_Medium5_BW_1, Enemy_Medium5_BW_2, Enemy_Medium5_BW_3, 50, new TimeSpan(0, 1, 45), new TimeSpan(0, 0, 0, 0, 750), 4);
                    enemySpawner.addSpawn(new Vector2(-100, 450), 750, 4, 4, 500, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium_BW, Enemy_Medium5_1, Enemy_Medium5_2, Enemy_Medium5_3, Enemy_Medium5_BW_1, Enemy_Medium5_BW_2, Enemy_Medium5_BW_3, 50, new TimeSpan(0, 1, 45), new TimeSpan(0, 0, 0, 0, 750), 4);
                    enemySpawner.addSpawn(new Vector2(750, 350), 750, 4, 5, 500, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium_BW, Enemy_Medium5_1, Enemy_Medium5_2, Enemy_Medium5_3, Enemy_Medium5_BW_1, Enemy_Medium5_BW_2, Enemy_Medium5_BW_3, 50, new TimeSpan(0, 1, 45), new TimeSpan(0, 0, 0, 0, 750), 4);
                    enemySpawner.addSpawn(new Vector2(750, 450), 750, 4, 5, 500, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium_BW, Enemy_Medium5_1, Enemy_Medium5_2, Enemy_Medium5_3, Enemy_Medium5_BW_1, Enemy_Medium5_BW_2, Enemy_Medium5_BW_3, 50, new TimeSpan(0, 1, 45), new TimeSpan(0, 0, 0, 0, 750), 4);
                    enemySpawner.addSpawn(new Vector2(225, 900), 1500, -5, 1, 1500, EnemyLaserProjectile_Large, EnemyLaserProjectile_Large_BW, Enemy_Large5_1, Enemy_Large5_2, Enemy_Large5_3, Enemy_Large5_BW_1, Enemy_Large5_BW_2, Enemy_Large5_BW_3, 130, new TimeSpan(0, 1, 45), new TimeSpan(0, 0, 0, 1, 0), 3);
                    enemySpawner.addSpawn(new Vector2(375, 900), 1500, -5, 1, 1500, EnemyLaserProjectile_Large, EnemyLaserProjectile_Large_BW, Enemy_Large5_1, Enemy_Large5_2, Enemy_Large5_3, Enemy_Large5_BW_1, Enemy_Large5_BW_2, Enemy_Large5_BW_3, 130, new TimeSpan(0, 1, 45), new TimeSpan(0, 0, 0, 1, 0), 3);

                    enemySpawner.addSpawn(new Vector2(0, -50), 100000, 2, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small5_1, Enemy_Small5_2, Enemy_Small5_3, Enemy_Small5_BW_1, Enemy_Small5_BW_2, Enemy_Small5_BW_3, 3, new TimeSpan(0, 2, 0), new TimeSpan(0, 0, 0, 0, 500), 149);
                    enemySpawner.addSpawn(new Vector2(50, -50), 100000, 2, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small5_1, Enemy_Small5_2, Enemy_Small5_3, Enemy_Small5_BW_1, Enemy_Small5_BW_2, Enemy_Small5_BW_3, 3, new TimeSpan(0, 2, 0), new TimeSpan(0, 0, 0, 0, 500), 59);
                    enemySpawner.addSpawn(new Vector2(100, -50), 100000, 2, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small5_1, Enemy_Small5_2, Enemy_Small5_3, Enemy_Small5_BW_1, Enemy_Small5_BW_2, Enemy_Small5_BW_3, 3, new TimeSpan(0, 2, 0), new TimeSpan(0, 0, 0, 0, 500), 9);
                    enemySpawner.addSpawn(new Vector2(150, -50), 100000, 2, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small5_1, Enemy_Small5_2, Enemy_Small5_3, Enemy_Small5_BW_1, Enemy_Small5_BW_2, Enemy_Small5_BW_3, 3, new TimeSpan(0, 2, 0), new TimeSpan(0, 0, 0, 0, 500), 9);
                    enemySpawner.addSpawn(new Vector2(200, -50), 100000, 2, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small5_1, Enemy_Small5_2, Enemy_Small5_3, Enemy_Small5_BW_1, Enemy_Small5_BW_2, Enemy_Small5_BW_3, 3, new TimeSpan(0, 2, 0), new TimeSpan(0, 0, 0, 0, 500), 9);
                    enemySpawner.addSpawn(new Vector2(250, -50), 100000, 2, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small5_1, Enemy_Small5_2, Enemy_Small5_3, Enemy_Small5_BW_1, Enemy_Small5_BW_2, Enemy_Small5_BW_3, 3, new TimeSpan(0, 2, 0), new TimeSpan(0, 0, 0, 0, 500), 9);
                    enemySpawner.addSpawn(new Vector2(400, -50), 100000, 2, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small5_1, Enemy_Small5_2, Enemy_Small5_3, Enemy_Small5_BW_1, Enemy_Small5_BW_2, Enemy_Small5_BW_3, 3, new TimeSpan(0, 2, 0), new TimeSpan(0, 0, 0, 0, 500), 19);
                    enemySpawner.addSpawn(new Vector2(450, -50), 100000, 2, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small5_1, Enemy_Small5_2, Enemy_Small5_3, Enemy_Small5_BW_1, Enemy_Small5_BW_2, Enemy_Small5_BW_3, 3, new TimeSpan(0, 2, 0), new TimeSpan(0, 0, 0, 0, 500), 19);
                    enemySpawner.addSpawn(new Vector2(500, -50), 100000, 2, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small5_1, Enemy_Small5_2, Enemy_Small5_3, Enemy_Small5_BW_1, Enemy_Small5_BW_2, Enemy_Small5_BW_3, 3, new TimeSpan(0, 2, 0), new TimeSpan(0, 0, 0, 0, 500), 19);
                    enemySpawner.addSpawn(new Vector2(550, -50), 100000, 2, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small5_1, Enemy_Small5_2, Enemy_Small5_3, Enemy_Small5_BW_1, Enemy_Small5_BW_2, Enemy_Small5_BW_3, 3, new TimeSpan(0, 2, 0), new TimeSpan(0, 0, 0, 0, 500), 19);
                    enemySpawner.addSpawn(new Vector2(600, -50), 100000, 2, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small5_1, Enemy_Small5_2, Enemy_Small5_3, Enemy_Small5_BW_1, Enemy_Small5_BW_2, Enemy_Small5_BW_3, 3, new TimeSpan(0, 2, 0), new TimeSpan(0, 0, 0, 0, 500), 27);
                    enemySpawner.addSpawn(new Vector2(650, -50), 100000, 2, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small5_1, Enemy_Small5_2, Enemy_Small5_3, Enemy_Small5_BW_1, Enemy_Small5_BW_2, Enemy_Small5_BW_3, 3, new TimeSpan(0, 2, 0), new TimeSpan(0, 0, 0, 0, 500), 59);
                    enemySpawner.addSpawn(new Vector2(700, -50), 100000, 2, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small5_1, Enemy_Small5_2, Enemy_Small5_3, Enemy_Small5_BW_1, Enemy_Small5_BW_2, Enemy_Small5_BW_3, 3, new TimeSpan(0, 2, 0), new TimeSpan(0, 0, 0, 0, 500), 149);
                    enemySpawner.addSpawn(new Vector2(300, -50), 100000, 2, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small5_1, Enemy_Small5_2, Enemy_Small5_3, Enemy_Small5_BW_1, Enemy_Small5_BW_2, Enemy_Small5_BW_3, 3, new TimeSpan(0, 2, 6), new TimeSpan(0, 0, 0, 0, 500), 7);
                    enemySpawner.addSpawn(new Vector2(350, -50), 100000, 2, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small5_1, Enemy_Small5_2, Enemy_Small5_3, Enemy_Small5_BW_1, Enemy_Small5_BW_2, Enemy_Small5_BW_3, 3, new TimeSpan(0, 2, 6), new TimeSpan(0, 0, 0, 0, 500), 7);
                    enemySpawner.addSpawn(new Vector2(200, -50), 100000, 2, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small5_1, Enemy_Small5_2, Enemy_Small5_3, Enemy_Small5_BW_1, Enemy_Small5_BW_2, Enemy_Small5_BW_3, 3, new TimeSpan(0, 2, 6), new TimeSpan(0, 0, 0, 0, 500), 11);
                    enemySpawner.addSpawn(new Vector2(250, -50), 100000, 2, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small5_1, Enemy_Small5_2, Enemy_Small5_3, Enemy_Small5_BW_1, Enemy_Small5_BW_2, Enemy_Small5_BW_3, 3, new TimeSpan(0, 2, 6), new TimeSpan(0, 0, 0, 0, 500), 11);
                    enemySpawner.addSpawn(new Vector2(400, -50), 100000, 2, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small5_1, Enemy_Small5_2, Enemy_Small5_3, Enemy_Small5_BW_1, Enemy_Small5_BW_2, Enemy_Small5_BW_3, 3, new TimeSpan(0, 2, 11), new TimeSpan(0, 0, 0, 0, 500), 37);
                    enemySpawner.addSpawn(new Vector2(450, -50), 100000, 2, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small5_1, Enemy_Small5_2, Enemy_Small5_3, Enemy_Small5_BW_1, Enemy_Small5_BW_2, Enemy_Small5_BW_3, 3, new TimeSpan(0, 2, 11), new TimeSpan(0, 0, 0, 0, 500), 5);
                    enemySpawner.addSpawn(new Vector2(100, -50), 100000, 2, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small5_1, Enemy_Small5_2, Enemy_Small5_3, Enemy_Small5_BW_1, Enemy_Small5_BW_2, Enemy_Small5_BW_3, 3, new TimeSpan(0, 2, 13), new TimeSpan(0, 0, 0, 0, 500), 33);
                    enemySpawner.addSpawn(new Vector2(150, -50), 100000, 2, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small5_1, Enemy_Small5_2, Enemy_Small5_3, Enemy_Small5_BW_1, Enemy_Small5_BW_2, Enemy_Small5_BW_3, 3, new TimeSpan(0, 2, 13), new TimeSpan(0, 0, 0, 0, 500), 33);
                    enemySpawner.addSpawn(new Vector2(200, -50), 100000, 2, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small5_1, Enemy_Small5_2, Enemy_Small5_3, Enemy_Small5_BW_1, Enemy_Small5_BW_2, Enemy_Small5_BW_3, 3, new TimeSpan(0, 2, 13), new TimeSpan(0, 0, 0, 0, 500), 33);
                    enemySpawner.addSpawn(new Vector2(250, -50), 100000, 2, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small5_1, Enemy_Small5_2, Enemy_Small5_3, Enemy_Small5_BW_1, Enemy_Small5_BW_2, Enemy_Small5_BW_3, 3, new TimeSpan(0, 2, 13), new TimeSpan(0, 0, 0, 0, 500), 33);
                    enemySpawner.addSpawn(new Vector2(300, -50), 100000, 2, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small5_1, Enemy_Small5_2, Enemy_Small5_3, Enemy_Small5_BW_1, Enemy_Small5_BW_2, Enemy_Small5_BW_3, 3, new TimeSpan(0, 2, 13), new TimeSpan(0, 0, 0, 0, 500), 33);
                    enemySpawner.addSpawn(new Vector2(350, -50), 100000, 2, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small5_1, Enemy_Small5_2, Enemy_Small5_3, Enemy_Small5_BW_1, Enemy_Small5_BW_2, Enemy_Small5_BW_3, 3, new TimeSpan(0, 2, 13), new TimeSpan(0, 0, 0, 0, 500), 33);
                    enemySpawner.addSpawn(new Vector2(500, -100), 750, 4, 1, 1000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium_BW, Enemy_Medium5_1, Enemy_Medium5_2, Enemy_Medium5_3, Enemy_Medium5_BW_1, Enemy_Medium5_BW_2, Enemy_Medium5_BW_3, 50, new TimeSpan(0, 2, 15), new TimeSpan(0, 0, 0, 2, 0), 5);
                    enemySpawner.addSpawn(new Vector2(450, -100), 500, 4, 1, 2000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium_BW, Enemy_Medium5_1, Enemy_Medium5_2, Enemy_Medium5_3, Enemy_Medium5_BW_1, Enemy_Medium5_BW_2, Enemy_Medium5_BW_3, 50, new TimeSpan(0, 2, 16), new TimeSpan(0, 0, 0, 2, 0), 5);
                    enemySpawner.addSpawn(new Vector2(550, -100), 500, 4, 1, 2000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium_BW, Enemy_Medium5_1, Enemy_Medium5_2, Enemy_Medium5_3, Enemy_Medium5_BW_1, Enemy_Medium5_BW_2, Enemy_Medium5_BW_3, 50, new TimeSpan(0, 2, 16), new TimeSpan(0, 0, 0, 2, 0), 5);
                    enemySpawner.addSpawn(new Vector2(200, -50), 100000, 2, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small5_1, Enemy_Small5_2, Enemy_Small5_3, Enemy_Small5_BW_1, Enemy_Small5_BW_2, Enemy_Small5_BW_3, 3, new TimeSpan(0, 2, 31), new TimeSpan(0, 0, 0, 0, 500), 38);
                    enemySpawner.addSpawn(new Vector2(250, -50), 100000, 2, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small5_1, Enemy_Small5_2, Enemy_Small5_3, Enemy_Small5_BW_1, Enemy_Small5_BW_2, Enemy_Small5_BW_3, 3, new TimeSpan(0, 2, 31), new TimeSpan(0, 0, 0, 0, 500), 38);
                    enemySpawner.addSpawn(new Vector2(300, -50), 100000, 2, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small5_1, Enemy_Small5_2, Enemy_Small5_3, Enemy_Small5_BW_1, Enemy_Small5_BW_2, Enemy_Small5_BW_3, 3, new TimeSpan(0, 2, 31), new TimeSpan(0, 0, 0, 0, 500), 38);
                    enemySpawner.addSpawn(new Vector2(350, -50), 100000, 2, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small5_1, Enemy_Small5_2, Enemy_Small5_3, Enemy_Small5_BW_1, Enemy_Small5_BW_2, Enemy_Small5_BW_3, 3, new TimeSpan(0, 2, 31), new TimeSpan(0, 0, 0, 0, 500), 38);
                    enemySpawner.addSpawn(new Vector2(400, -50), 100000, 2, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small5_1, Enemy_Small5_2, Enemy_Small5_3, Enemy_Small5_BW_1, Enemy_Small5_BW_2, Enemy_Small5_BW_3, 3, new TimeSpan(0, 2, 31), new TimeSpan(0, 0, 0, 0, 500), 38);
                    enemySpawner.addSpawn(new Vector2(450, -50), 100000, 2, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small5_1, Enemy_Small5_2, Enemy_Small5_3, Enemy_Small5_BW_1, Enemy_Small5_BW_2, Enemy_Small5_BW_3, 3, new TimeSpan(0, 2, 31), new TimeSpan(0, 0, 0, 0, 500), 38);
                    enemySpawner.addSpawn(new Vector2(500, -50), 100000, 2, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small5_1, Enemy_Small5_2, Enemy_Small5_3, Enemy_Small5_BW_1, Enemy_Small5_BW_2, Enemy_Small5_BW_3, 3, new TimeSpan(0, 2, 31), new TimeSpan(0, 0, 0, 0, 500), 38);
                    enemySpawner.addSpawn(new Vector2(550, -50), 100000, 2, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small5_1, Enemy_Small5_2, Enemy_Small5_3, Enemy_Small5_BW_1, Enemy_Small5_BW_2, Enemy_Small5_BW_3, 3, new TimeSpan(0, 2, 31), new TimeSpan(0, 0, 0, 0, 500), 38);
                    enemySpawner.addSpawn(new Vector2(50, -150), 1500, 3, 1, 1500, EnemyLaserProjectile_Large, EnemyLaserProjectile_Large_BW, Enemy_Large5_1, Enemy_Large5_2, Enemy_Large5_3, Enemy_Large5_BW_1, Enemy_Large5_BW_2, Enemy_Large5_BW_3, 130, new TimeSpan(0, 2, 35), new TimeSpan(0, 0, 0, 3, 0), 4);
                    enemySpawner.addSpawn(new Vector2(50, -50), 100000, 2, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small5_1, Enemy_Small5_2, Enemy_Small5_3, Enemy_Small5_BW_1, Enemy_Small5_BW_2, Enemy_Small5_BW_3, 3, new TimeSpan(0, 2, 51), new TimeSpan(0, 0, 0, 0, 500), 47);
                    enemySpawner.addSpawn(new Vector2(100, -50), 100000, 2, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small5_1, Enemy_Small5_2, Enemy_Small5_3, Enemy_Small5_BW_1, Enemy_Small5_BW_2, Enemy_Small5_BW_3, 3, new TimeSpan(0, 2, 51), new TimeSpan(0, 0, 0, 0, 500), 47);
                    enemySpawner.addSpawn(new Vector2(150, -50), 100000, 2, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small5_1, Enemy_Small5_2, Enemy_Small5_3, Enemy_Small5_BW_1, Enemy_Small5_BW_2, Enemy_Small5_BW_3, 3, new TimeSpan(0, 2, 51), new TimeSpan(0, 0, 0, 0, 500), 47);
                    enemySpawner.addSpawn(new Vector2(200, -50), 100000, 2, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small5_1, Enemy_Small5_2, Enemy_Small5_3, Enemy_Small5_BW_1, Enemy_Small5_BW_2, Enemy_Small5_BW_3, 3, new TimeSpan(0, 2, 51), new TimeSpan(0, 0, 0, 0, 500), 47);
                    enemySpawner.addSpawn(new Vector2(500, -50), 100000, 2, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small5_1, Enemy_Small5_2, Enemy_Small5_3, Enemy_Small5_BW_1, Enemy_Small5_BW_2, Enemy_Small5_BW_3, 3, new TimeSpan(0, 2, 51), new TimeSpan(0, 0, 0, 0, 500), 47);
                    enemySpawner.addSpawn(new Vector2(550, -50), 100000, 2, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small5_1, Enemy_Small5_2, Enemy_Small5_3, Enemy_Small5_BW_1, Enemy_Small5_BW_2, Enemy_Small5_BW_3, 3, new TimeSpan(0, 2, 51), new TimeSpan(0, 0, 0, 0, 500), 47);
                    enemySpawner.addSpawn(new Vector2(600, -50), 100000, 2, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small5_1, Enemy_Small5_2, Enemy_Small5_3, Enemy_Small5_BW_1, Enemy_Small5_BW_2, Enemy_Small5_BW_3, 3, new TimeSpan(0, 2, 51), new TimeSpan(0, 0, 0, 0, 500), 47);
                    enemySpawner.addSpawn(new Vector2(650, -50), 100000, 2, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small5_1, Enemy_Small5_2, Enemy_Small5_3, Enemy_Small5_BW_1, Enemy_Small5_BW_2, Enemy_Small5_BW_3, 3, new TimeSpan(0, 2, 51), new TimeSpan(0, 0, 0, 0, 500), 47);
                    enemySpawner.addSpawn(new Vector2(300, -150), 1500, 3, 1, 1500, EnemyLaserProjectile_Large, EnemyLaserProjectile_Large_BW, Enemy_Large5_1, Enemy_Large5_2, Enemy_Large5_3, Enemy_Large5_BW_1, Enemy_Large5_BW_2, Enemy_Large5_BW_3, 130, new TimeSpan(0, 2, 54), new TimeSpan(0, 0, 0, 4, 0), 2);
                    enemySpawner.addSpawn(new Vector2(250, -100), 750, 3, 1, 2000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium_BW, Enemy_Medium5_1, Enemy_Medium5_2, Enemy_Medium5_3, Enemy_Medium5_BW_1, Enemy_Medium5_BW_2, Enemy_Medium5_BW_3, 50, new TimeSpan(0, 2, 56), new TimeSpan(0, 0, 0, 4, 0), 2);
                    enemySpawner.addSpawn(new Vector2(400, -100), 750, 3, 1, 2000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium_BW, Enemy_Medium5_1, Enemy_Medium5_2, Enemy_Medium5_3, Enemy_Medium5_BW_1, Enemy_Medium5_BW_2, Enemy_Medium5_BW_3, 50, new TimeSpan(0, 2, 56), new TimeSpan(0, 0, 0, 4, 0), 2);
                    enemySpawner.addSpawn(new Vector2(300, -50), 100000, 2, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small5_1, Enemy_Small5_2, Enemy_Small5_3, Enemy_Small5_BW_1, Enemy_Small5_BW_2, Enemy_Small5_BW_3, 3, new TimeSpan(0, 3, 9), new TimeSpan(0, 0, 0, 0, 500), 3);
                    enemySpawner.addSpawn(new Vector2(400, -50), 100000, 2, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small5_1, Enemy_Small5_2, Enemy_Small5_3, Enemy_Small5_BW_1, Enemy_Small5_BW_2, Enemy_Small5_BW_3, 3, new TimeSpan(0, 3, 9), new TimeSpan(0, 0, 0, 0, 500), 3);
                    enemySpawner.addSpawn(new Vector2(250, -50), 100000, 2, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small5_1, Enemy_Small5_2, Enemy_Small5_3, Enemy_Small5_BW_1, Enemy_Small5_BW_2, Enemy_Small5_BW_3, 3, new TimeSpan(0, 3, 12), new TimeSpan(0, 0, 0, 0, 500), 3);
                    enemySpawner.addSpawn(new Vector2(350, -50), 100000, 2, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small5_1, Enemy_Small5_2, Enemy_Small5_3, Enemy_Small5_BW_1, Enemy_Small5_BW_2, Enemy_Small5_BW_3, 3, new TimeSpan(0, 3, 12), new TimeSpan(0, 0, 0, 0, 500), 3);
                    enemySpawner.addSpawn(new Vector2(450, -50), 100000, 2, 1, 15000, EnemyLaserProjectile_Medium, EnemyLaserProjectile_Medium, Enemy_Small5_1, Enemy_Small5_2, Enemy_Small5_3, Enemy_Small5_BW_1, Enemy_Small5_BW_2, Enemy_Small5_BW_3, 3, new TimeSpan(0, 3, 12), new TimeSpan(0, 0, 0, 0, 500), 3);
                    

                    Boss = new Boss(new Vector2(160, -260), 100000, Boss5_1, Boss5_2, Boss5_3, Boss5_BW_1, Boss5_BW_2, Boss5_BW_3, new TimeSpan(0, 3, 20));
                    Boss.Weapons.Add(new Weapon(new Vector2(375, -30), MathHelper.ToRadians(160), 500, 3, 10, 11, 1, LaserProjectile, LaserProjectile_BW));
                    Boss.Weapons.Add(new Weapon(new Vector2(190, -170), MathHelper.ToRadians(200), 1000, 10, 3, 50, 4, HomingProjectile, HomingProjectile_BW));
                    Boss.Weapons.Add(new Weapon(new Vector2(560, -170), MathHelper.ToRadians(160), 1000, 10, 3, 50, 4, HomingProjectile, HomingProjectile_BW));
                    Boss.Weapons.Add(new Weapon(new Vector2(260, -200), MathHelper.ToRadians(260), 50, 10, 100, 1, 1, LaserProjectile, LaserProjectile_BW));
                    Boss.Weapons.Add(new Weapon(new Vector2(480, -200), MathHelper.ToRadians(100), 50, 10, 100, 1, 1, LaserProjectile, LaserProjectile_BW));
                    Boss.Weapons.Add(new Weapon(new Vector2(280, -200), MathHelper.ToRadians(140), 1000, 15, 5, 1, 6, LaserProjectile, LaserProjectile_BW));
                    Boss.Weapons.Add(new Weapon(new Vector2(460, -200), MathHelper.ToRadians(120), 1000, 15, 5, 1, 6, LaserProjectile, LaserProjectile_BW));
                    Boss.Weapons.Add(new Weapon(new Vector2(260, -200), MathHelper.ToRadians(270), 50, 15, 10, 1, 5, LaserProjectile, LaserProjectile_BW));
                    Boss.Weapons.Add(new Weapon(new Vector2(480, -200), MathHelper.ToRadians(180), 50, 15, 10, 1, 5, LaserProjectile, LaserProjectile_BW));
                    Boss.Weapons.Add(new Weapon(new Vector2(380, -50), MathHelper.ToRadians(120), 10, 10, 5, 1, 3, Laser, Laser_BW));
                    Boss.Weapons.Add(new Weapon(new Vector2(380, -50), MathHelper.ToRadians(120), 10, 10, 5, 1, 3, Laser, Laser_BW));
                    Boss.Weapons.Add(new Weapon(new Vector2(380, -50), MathHelper.ToRadians(120), 10, 10, 5, 1, 3, Laser, Laser_BW));

                    Boss.addEvent(1, TimeSpan.Zero, 0, 300, 100, 0);
                    Boss.addEvent(2, TimeSpan.Zero, 1, 40, 50, -1);
                    Boss.addEvent(2, TimeSpan.Zero, 2, -40, 200, -1);
                    Boss.addEvent(2, TimeSpan.Zero, 3, 40, 200, -1);
                    Boss.addEvent(3, new TimeSpan(0, 0, 2), 1, 1, 1, 0);
                    Boss.addEvent(3, new TimeSpan(0, 0, 10), 2, 1, 1, 0);
                    Boss.addEvent(3, new TimeSpan(0, 0, 10), 3, 1, 1, 0);

                    Boss.addEvent(3, 67000, 1, 0, 1, 0);
                    Boss.addEvent(3, 67000, 2, 0, 1, 0);
                    Boss.addEvent(3, 67000, 3, 0, 1, 0);
                    Boss.addEvent(3, 65000, 4, 1, 1, 0);
                    Boss.addEvent(3, 65000, 5, 1, 1, 0);
                    Boss.addEvent(2, 65000, 4, -120, 100, 0);
                    Boss.addEvent(2, 65000, 5, 20, 100, 0);
                    Boss.addEvent(2, 63000, 4, 100, 300, -1);
                    Boss.addEvent(2, 63000, 5, 100, 300, -1);
                    Boss.addEvent(3, 62000, 6, 1, 1, 0);
                    Boss.addEvent(3, 61750, 7, 1, 1, 0);
                    Boss.addEvent(2, 63000, 6, 100, 300, -1);
                    Boss.addEvent(2, 63000, 7, 100, 300, -1);
                    
                    Boss.addEvent(3, 36000, 4, 0, 1, 0);
                    Boss.addEvent(3, 36000, 5, 0, 1, 0);
                    Boss.addEvent(3, 36000, 6, 0, 1, 0);
                    Boss.addEvent(3, 36000, 7, 0, 1, 0);
                    Boss.addEvent(3, 34000, 8, 1, 1, 0);
                    Boss.addEvent(3, 34000, 9, 1, 1, 0);
                    Boss.addEvent(2, 34000, 8, -90, 200, -1);
                    Boss.addEvent(2, 34000, 9, -90, 200, -1);
                    Boss.addEvent(3, 33000, 10, 0, 10, -1);
                    Boss.addEvent(2, 33000, 10, 120, 150, -1);
                    Boss.addEvent(3, 33000, 10, 1, 40, -1);
                    Boss.addEvent(3, 31000, 11, 0, 10, -1);
                    Boss.addEvent(2, 31000, 11, 120, 200, -1);
                    Boss.addEvent(3, 31000, 11, 1, 50, -1);
                    Boss.addEvent(3, 29000, 12, 0, 10, -1);
                    Boss.addEvent(2, 29000, 12, 120, 250, -1);
                    Boss.addEvent(3, 29000, 12, 1, 60, -1);
                    break;
            }
        }
    }
}
