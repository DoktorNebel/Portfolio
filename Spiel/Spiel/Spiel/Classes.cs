using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Classes
{
    public class Player
    {
        public Vector2 Position { get; set; }
        public int Speed { get; set; }
        public int MaxHealth { get; set; }
        public int Health { get; set; }
        public int Shield { get; set; }
        public int Energy { get; set; }
        public Texture2D Sprite100 { get; set; }
        public Texture2D Sprite66 { get; set; }
        public Texture2D Sprite33 { get; set; }
        public List<Weapon> Weapons { get; set; }

        public Player(Vector2 position, int speed, int maxHealth, int shield, int energy, Texture2D sprite100, Texture2D sprite66, Texture2D sprite33)
        {
            this.Position = position;
            this.Speed = speed;
            this.MaxHealth = maxHealth;
            this.Health = maxHealth;
            this.Shield = shield;
            this.Energy = energy;
            this.Sprite100 = sprite100;
            this.Sprite66 = sprite66;
            this.Sprite33 = sprite33;
            Weapons = new List<Weapon>();
        }

        public void Move(int direction)
        {
            switch (direction)
            {
                case 1:
                    this.Position = new Vector2(this.Position.X, this.Position.Y - this.Speed);
                    foreach (Weapon Weapon in this.Weapons)
                    {
                        Weapon.Position = new Vector2(Weapon.Position.X, Weapon.Position.Y - this.Speed);
                    }
                    break;
                case 2:
                    this.Position = new Vector2(this.Position.X + this.Speed, this.Position.Y);
                    foreach (Weapon Weapon in this.Weapons)
                    {
                        Weapon.Position = new Vector2(Weapon.Position.X + this.Speed, Weapon.Position.Y);
                    }
                    break;
                case 3:
                    this.Position = new Vector2(this.Position.X, (float)(this.Position.Y + this.Speed * .6));
                    foreach (Weapon Weapon in this.Weapons)
                    {
                        Weapon.Position = new Vector2(Weapon.Position.X, (float)(Weapon.Position.Y + this.Speed * .6));
                    }
                    break;
                case 4:
                    this.Position = new Vector2(this.Position.X - this.Speed, this.Position.Y);
                    foreach (Weapon Weapon in this.Weapons)
                    {
                        Weapon.Position = new Vector2(Weapon.Position.X - this.Speed, Weapon.Position.Y);
                    }
                    break;
            }
        }
    }

    public class Enemy
    {
        public Vector2 Position { get; set; }
        public int MaxHealth { get; set; }
        public int Health { get; set; }
        public int Speed { get; set; }
        public int MovePattern { get; set; }
        public Texture2D AttackSprite { get; set; }
        public Texture2D BWAttackSprite { get; set; }
        public Texture2D Sprite100 { get; set; }
        public Texture2D Sprite66 { get; set; }
        public Texture2D Sprite33 { get; set; }
        public Texture2D BWSprite100 { get; set; }
        public Texture2D BWSprite66 { get; set; }
        public Texture2D BWSprite33 { get; set; }
        public int AttackSpeed { get; set; }
        public int ShotTime { get; set; }
        public int DropChance { get; set; }
        private int moveCounter;

        public Enemy(Vector2 position, int health, int speed, int movePattern, int attackSpeed, Texture2D attackSprite, Texture2D bwAttackSprite, Texture2D sprite100, Texture2D sprite66, Texture2D sprite33, Texture2D bwSprite100, Texture2D bwSprite66, Texture2D bwSprite33, int dropChance)
        {
            this.Position = position;
            this.Health = health;
            this.MaxHealth = health;
            this.Speed = speed;
            this.MovePattern = movePattern;
            this.AttackSpeed = attackSpeed;
            this.AttackSprite = attackSprite;
            this.BWAttackSprite = bwAttackSprite;
            this.Sprite100 = sprite100;
            this.Sprite66 = sprite66;
            this.Sprite33 = sprite33;
            this.BWSprite100 = bwSprite100;
            this.BWSprite66 = bwSprite66;
            this.BWSprite33 = bwSprite33;
            this.ShotTime = 0;
            this.DropChance = dropChance;
            this.moveCounter = 200 / this.Speed;
        }

        /* Move Patterns:
         * 1: downward Movement
         * 2: top-left to lower-right 
         * 3: top-right to lower-left 
         * 4: left to right
         * 5: right to left
         * 6: downward Movement with stop*/
        public void Move()
        {
            switch (this.MovePattern)
            {
                case 1:
                    this.Position = new Vector2(this.Position.X, this.Position.Y + this.Speed);
                    break;
                case 2:
                    this.Position = new Vector2(this.Position.X + this.Speed, this.Position.Y + this.Speed);
                    break;
                case 3:
                    this.Position = new Vector2(this.Position.X - this.Speed, this.Position.Y + this.Speed);
                    break;
                case 4:
                    this.Position = new Vector2(this.Position.X + this.Speed, this.Position.Y);
                    break;
                case 5:
                    this.Position = new Vector2(this.Position.X - this.Speed, this.Position.Y);
                    break;
                case 6:
                    if (this.moveCounter > 1)
                    {
                        this.Position = new Vector2(this.Position.X, this.Position.Y + this.Speed);
                        this.moveCounter--;
                    }
                    else if (this.moveCounter == 1)
                    {
                        this.moveCounter = -100;
                    }
                    else if (this.moveCounter < 0)
                    {
                        this.moveCounter++;
                    }
                    else
                    {
                        this.Position = new Vector2(this.Position.X, this.Position.Y + this.Speed);
                    }
                    break;
            }
        }
    }

    public class Boss
    {
        public Vector2 Position { get; set; }
        public int MaxHealth { get; set; }
        public int Health { get; set; }
        public TimeSpan LifeTime { get; set; }
        public List<Weapon> Weapons { get; set; }
        private List<Event> Events { get; set; }
        public Texture2D Sprite100 { get; set; }
        public Texture2D Sprite66 { get; set; }
        public Texture2D Sprite33 { get; set; }
        public Texture2D BWSprite100 { get; set; }
        public Texture2D BWSprite66 { get; set; }
        public Texture2D BWSprite33 { get; set; }
        public TimeSpan SpawnTime { get; set; }
        public bool Spawned { get; set; }

        private class Event
        {
            public int Kind { get; set; }
            public int TriggerHealth { get; set; }
            public TimeSpan TriggerTime { get; set; }
            public int Value1 { get; set; }
            public int Value2 { get; set; }
            public int Steps { get; set; }
            public int steps { get; set; }
            public int Loops { get; set; }
            public bool reverse { get; set; }
            public bool Activated { get; set; }

            /* Event Types:
               1: Position change
               2: Weapon rotation change
               3: Activate/Deactivate Weapon */

            public Event(int kind, int triggerHealth, int value1, int value2, int steps, int loops)
            {
                this.Kind = kind;
                this.TriggerHealth = triggerHealth;
                this.TriggerTime = TimeSpan.Zero;
                this.Value1 = value1;
                this.Value2 = value2;
                this.Steps = steps;
                this.steps = steps;
                this.Loops = loops;
                this.reverse = false;
                this.Activated = false;
            }

            public Event(int kind, TimeSpan triggerTime, int value1, int value2, int steps, int loops)
            {
                this.Kind = kind;
                this.TriggerTime = triggerTime;
                this.TriggerHealth = -1;
                this.Value1 = value1;
                this.Value2 = value2;
                this.Steps = steps;
                this.steps = steps;
                this.Loops = loops;
                this.reverse = false;
                this.Activated = false;
            }
        }

        public Boss(Vector2 position, int health, Texture2D sprite100, Texture2D sprite66, Texture2D sprite33, Texture2D bwSprite100, Texture2D bwSprite66, Texture2D bwSprite33, TimeSpan spawnTime)
        {
            this.Position = position;
            this.Health = health;
            this.MaxHealth = health;
            this.LifeTime = TimeSpan.Zero;
            this.Sprite100 = sprite100;
            this.Sprite66 = sprite66;
            this.Sprite33 = sprite33;
            this.BWSprite100 = bwSprite100;
            this.BWSprite66 = bwSprite66;
            this.BWSprite33 = bwSprite33;
            this.Events = new List<Event>();
            this.SpawnTime = spawnTime;
            this.Spawned = false;
            this.Weapons = new List<Weapon>();
        }

        public void Update(GameTime gameTime)
        {
            this.LifeTime += gameTime.ElapsedGameTime;

            if (this.LifeTime >= this.SpawnTime & !this.Spawned & this.SpawnTime != TimeSpan.Zero)
            {
                this.Spawned = true;
                this.LifeTime = TimeSpan.Zero;
                this.SpawnTime = TimeSpan.Zero;
            }
            else if (this.Spawned)
            {
                for (int i = 0; i < this.Events.Count; i++)
                {
                    Event Event = Events.ElementAt<Event>(i);

                    if (Event.steps == 0)
                    {
                        if (Event.Loops == 0)
                        {
                            Events.RemoveAt(i);
                        }
                        else
                        {
                            Event.steps = Event.Steps;
                            Event.Loops--;
                            Event.reverse = !Event.reverse;
                        }
                    }

                    int j;
                    if (Event.reverse)
                    {
                        j = -1;
                    }
                    else
                    {
                        j = 1;
                    }

                    if (this.LifeTime >= Event.TriggerTime & (this.Health <= Event.TriggerHealth | Event.TriggerHealth == -1))
                    {
                        Event.Activated = true;
                    }

                    if (Event.Activated)
                    {
                        switch (Event.Kind)
                        {
                            case 1:
                                this.Position = new Vector2(this.Position.X + Event.Value1 * j / Event.Steps, this.Position.Y + Event.Value2 * j / Event.Steps);
                                foreach (Weapon Weapon in this.Weapons)
                                {
                                    Weapon.Position = new Vector2(Weapon.Position.X + Event.Value1 * j / Event.Steps, Weapon.Position.Y + Event.Value2 * j / Event.Steps);
                                }
                                break;
                            case 2:
                                this.Weapons.ElementAt<Weapon>(Event.Value1 - 1).Rotation += MathHelper.ToRadians(((float)Event.Value2 * j) / (float)Event.Steps);
                                break;
                            case 3:
                                if (!Event.reverse)
                                    this.Weapons.ElementAt<Weapon>(Event.Value1 - 1).Activated = Convert.ToBoolean(Event.Value2);
                                break;
                        }
                        Event.steps--;
                    }
                }
            }
        }

        public void addEvent(int kind, int triggerHealth, int value1, int value2, int steps, int loops)
        {
            this.Events.Add(new Event(kind, triggerHealth, value1, value2, steps, loops));
        }

        public void addEvent(int kind, TimeSpan triggerTime, int value1, int value2, int steps, int loops)
        {
            this.Events.Add(new Event(kind, triggerTime, value1, value2, steps, loops));
        }
    }

    public class EnemySpawner
    {
        public List<Spawn> Spawns { get; private set; }

        public EnemySpawner()
        {
            Spawns = new List<Spawn>();
        }

        public void update(List<Enemy> enemies, GameTime gameTime)
        {
            for (int i = 0; i <= Spawns.Count - 1; i++)
            {
                Spawn Spawn = Spawns.ElementAt<Spawn>(i);
                Spawn.Time += gameTime.ElapsedGameTime;
                if (Spawn.Started)
                {
                    if (Spawn.Loops == 0)
                    {
                        Spawns.RemoveAt(i);
                    }
                    else if (Spawn.Time >= Spawn.Interval)
                    {
                        enemies.Add(new Enemy(Spawn.EnemySpawnPosition, Spawn.EnemyHealth, Spawn.EnemySpeed, Spawn.EnemyMovePattern, Spawn.EnemyAttackSpeed, Spawn.EnemyAttackSprite, Spawn.EnemyBWAttackSprite, Spawn.EnemySprite100, Spawn.EnemySprite66, Spawn.EnemySprite33, Spawn.EnemyBWSprite100, Spawn.EnemyBWSprite66, Spawn.EnemyBWSprite33, Spawn.EnemyDropChance));
                        Spawn.Time = TimeSpan.Zero;
                        Spawn.Loops--;
                    }
                }
                else
                {
                    if (Spawn.Time >= Spawn.StartTime)
                    {
                        enemies.Add(new Enemy(Spawn.EnemySpawnPosition, Spawn.EnemyHealth, Spawn.EnemySpeed, Spawn.EnemyMovePattern, Spawn.EnemyAttackSpeed, Spawn.EnemyAttackSprite, Spawn.EnemyBWAttackSprite, Spawn.EnemySprite100, Spawn.EnemySprite66, Spawn.EnemySprite33, Spawn.EnemyBWSprite100, Spawn.EnemyBWSprite66, Spawn.EnemyBWSprite33, Spawn.EnemyDropChance));
                        Spawn.Time = TimeSpan.Zero;
                        Spawn.Started = true;
                    }
                }
            }
        }

        public void addSpawn(Vector2 enemySpawnPosition, int enemyHealth, int enemySpeed, int enemyMovePattern, int enemyAttackSpeed, Texture2D enemyAttackSprite, Texture2D enemyBWAttackSprite, Texture2D enemySprite100, Texture2D enemySprite66, Texture2D enemySprite33, Texture2D enemyBWSprite100, Texture2D enemyBWSprite66, Texture2D enemyBWSprite33, int enemyDropChance, TimeSpan startTime, TimeSpan interval, int loops)
        {
            Spawns.Add(new Spawn(startTime, interval, loops, enemySpawnPosition, enemyHealth, enemySpeed, enemyMovePattern, enemyAttackSpeed, enemyAttackSprite, enemyBWAttackSprite, enemySprite100, enemySprite66, enemySprite33, enemyBWSprite100, enemyBWSprite66, enemyBWSprite33, enemyDropChance));
        }
    }

    public class Spawn
    {
        public TimeSpan StartTime { get; set; }
        public bool Started { get; set; }
        public TimeSpan Interval { get; set; }
        public TimeSpan Time { get; set; }
        public int Loops { get; set; }
        public Vector2 EnemySpawnPosition { get; set; }
        public int EnemyHealth { get; set; }
        public int EnemySpeed { get; set; }
        public int EnemyMovePattern { get; set; }
        public Texture2D EnemyAttackSprite { get; set; }
        public Texture2D EnemyBWAttackSprite { get; set; }
        public int EnemyAttackSpeed { get; set; }
        public Texture2D EnemySprite100 { get; set; }
        public Texture2D EnemySprite66 { get; set; }
        public Texture2D EnemySprite33 { get; set; }
        public Texture2D EnemyBWSprite100 { get; set; }
        public Texture2D EnemyBWSprite66 { get; set; }
        public Texture2D EnemyBWSprite33 { get; set; }
        public int EnemyDropChance { get; set; }

        public Spawn(TimeSpan startTime, TimeSpan interval, int loops, Vector2 enemySpawnPosition, int enemyHealth, int enemySpeed, int enemyMovePattern, int enemyAttackSpeed, Texture2D enemyAttackSprite, Texture2D enemyBWAttackSprite, Texture2D enemySprite100, Texture2D enemySprite66, Texture2D enemySprite33, Texture2D enemyBWSprite100, Texture2D enemyBWSprite66, Texture2D enemyBWSprite33, int enemyDropChance)
        {
            this.StartTime = startTime;
            this.Interval = interval;
            this.Loops = loops;
            this.Time = TimeSpan.Zero;
            this.Started = false;
            this.EnemySpawnPosition = enemySpawnPosition;
            this.EnemyHealth = enemyHealth;
            this.EnemySpeed = enemySpeed;
            this.EnemyMovePattern = enemyMovePattern;
            this.EnemyAttackSprite = enemyAttackSprite;
            this.EnemyBWAttackSprite = enemyBWAttackSprite;
            this.EnemyAttackSpeed = enemyAttackSpeed;
            this.EnemySprite100 = enemySprite100;
            this.EnemySprite66 = enemySprite66;
            this.EnemySprite33 = enemySprite33;
            this.EnemyBWSprite100 = enemyBWSprite100;
            this.EnemyBWSprite66 = enemyBWSprite66;
            this.EnemyBWSprite33 = enemyBWSprite33;
            this.EnemyDropChance = enemyDropChance;
        }
    }

    public class Weapon
    {
        public Vector2 Position { get; set; }
        public float Rotation { get; set; }
        public int FireRate { get; set; }
        public int ProjectileSpeed { get; set; }
        public int Damage { get; set; }
        public int Projectiles { get; set; }
        public int Type { get; set; }
        public Texture2D AttackSprite { get; set; }
        public Texture2D BWAttackSprite { get; set; }
        public int ShotTime { get; set; }
        public bool Activated { get; set; }
        /* Attack Types:
           1: LaserProjectile
           2: HomingProjectile 
           3: Laser
           4: Homing Volley
           5: Lightning
           6: Shockwave */

        public Weapon(Vector2 position, float rotation, int fireRate, int projectileSpeed, int damage, int projectiles, int type, Texture2D attackSprite, Texture2D bwAttackSprite)
        {
            this.Position = position;
            this.Rotation = rotation;
            this.FireRate = fireRate;
            this.ProjectileSpeed = projectileSpeed;
            this.Damage = damage;
            this.Projectiles = projectiles;
            this.Type = type;
            this.AttackSprite = attackSprite;
            this.BWAttackSprite = bwAttackSprite;
            this.ShotTime = 0;
            this.Activated = false;
        }
    }

    public class Attack
    {
        public Vector2 Position { get; set; }
        public int Damage { get; set; }
        public Texture2D Sprite { get; set; }
        public Texture2D BWSprite { get; set; }
        public float Rotation { get; set; }

        public Attack(Vector2 position, float rotation, int damage, Texture2D sprite, Texture2D bwSprite)
        {
            this.Position = position;
            this.Damage = damage;
            this.Sprite = sprite;
            this.BWSprite = bwSprite;
            this.Rotation = rotation;
        }

        public virtual void Move()
        {
        }

        public virtual bool HitTest(Vector2 upperLeft, Vector2 lowerRight)
        {
            if (this.Position.X + this.Sprite.Width > upperLeft.X & this.Position.X < lowerRight.X & this.Position.Y < lowerRight.Y & this.Position.Y + this.Sprite.Height > upperLeft.Y)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public class LaserProjectile : Attack
    {
        public int Speed { get; set; }
        private float X;
        private float Y;

        public LaserProjectile(Vector2 position, float rotation, int damage, int speed, Texture2D sprite, Texture2D bwSprite)
            : base(position, rotation, damage, sprite, bwSprite)
        {
            this.Speed = speed;
            this.X = (float)Math.Cos(this.Rotation) * this.Speed;
            this.Y = (float)Math.Sin(this.Rotation) * this.Speed;
        }

        public override void Move()
        {
            this.Position = new Vector2(this.Position.X + this.X, this.Position.Y + this.Y);
        }
    }

    public class HomingProjectile : Attack
    {
        public int Speed { get; set; }
        public Enemy Target { get; set; }
        public List<Enemy> Targets { get; set; }
        public Boss Boss { get; set; }
        private Vector2 TargetPosition;
        private bool LockedOn;
        private int MoveCount;

        public HomingProjectile(Vector2 position, float rotation, List<Enemy> targets, Boss boss, int damage, int speed, Texture2D sprite, Texture2D bwSprite)
            : base(position, rotation, damage, sprite, bwSprite)
        {
            this.Speed = speed;
            this.LockedOn = false;
            this.Targets = targets;
            this.Boss = boss;
            this.MoveCount = 2;
        }

        public override void Move()
        {
            if (this.MoveCount > 0)
            {
                this.MoveCount--;
                this.Position = new Vector2(this.Position.X + (float)Math.Cos(this.Rotation) * this.Speed, this.Position.Y + (float)Math.Sin(this.Rotation) * this.Speed);
            }
            else if (this.MoveCount == 0)
            {
                this.MoveCount--;
                if (this.Targets.Count > 0)
                {
                    Enemy NearestTarget = this.Targets.ElementAt<Enemy>(0);
                    double NearestTargetDist = Math.Sqrt(Math.Pow(this.Position.X - NearestTarget.Position.X, 2) + Math.Pow(this.Position.Y - NearestTarget.Position.Y, 2));
                    foreach (Enemy Target in this.Targets)
                    {
                        double TargetDist = Math.Sqrt(Math.Pow(this.Position.X - Target.Position.X, 2) + Math.Pow(this.Position.Y - Target.Position.Y, 2));
                        if (TargetDist < NearestTargetDist)
                        {
                            NearestTarget = Target;
                            NearestTargetDist = TargetDist;
                        }
                    }
                    this.Target = NearestTarget;
                    this.TargetPosition = new Vector2(this.Target.Position.X + this.Target.Sprite100.Width / 2 + (float)Math.Cos(this.Rotation) * 10000, this.Target.Position.Y + this.Target.Sprite100.Height / 2 + (float)Math.Sin(this.Rotation) * 10000);
                }
                else
                {
                    this.Target = null;
                    this.TargetPosition = new Vector2(this.Position.X + (float)Math.Cos(this.Rotation) * 10000, this.Position.Y + (float)Math.Sin(this.Rotation) * 10000);
                }
            }
            else
            {
                if (this.Targets.Contains<Enemy>(this.Target))
                {
                    float DesiredRotation = (float)Math.Atan2(this.Target.Position.Y + this.Target.Sprite100.Height / 2 - this.Position.Y, this.Target.Position.X + this.Target.Sprite100.Width / 2 - this.Position.X);
                    if (this.Rotation - DesiredRotation < MathHelper.ToRadians(-7) & this.Rotation - DesiredRotation > MathHelper.ToRadians(-180) & !this.LockedOn)
                    {
                        this.Rotation += MathHelper.ToRadians(7);
                    }
                    else if (((this.Rotation - DesiredRotation <= MathHelper.ToRadians(-180) & this.Rotation - DesiredRotation > MathHelper.ToRadians(-360)) | (this.Rotation - DesiredRotation > MathHelper.ToRadians(7) & this.Rotation - DesiredRotation < MathHelper.ToRadians(90))) & !this.LockedOn)
                    {
                        this.Rotation -= MathHelper.ToRadians(7);
                    }
                    else
                    {
                        this.Rotation = DesiredRotation;
                        this.LockedOn = true;
                    }
                    this.Position = new Vector2(this.Position.X + (float)Math.Cos(this.Rotation) * this.Speed, this.Position.Y + (float)Math.Sin(this.Rotation) * this.Speed);
                    this.TargetPosition = new Vector2(this.Target.Position.X + this.Target.Sprite100.Width / 2 + (float)Math.Cos(this.Rotation) * 10000, this.Target.Position.Y + this.Target.Sprite100.Height / 2 + (float)Math.Sin(this.Rotation) * 10000);
                }
                else if (this.Boss != null && this.Boss.Spawned)
                {
                    float DesiredRotation = (float)Math.Atan2(this.Boss.Position.Y + this.Boss.Sprite100.Height / 2 - this.Position.Y, this.Boss.Position.X + this.Boss.Sprite100.Width / 2 - this.Position.X);
                    if (this.Rotation - DesiredRotation < MathHelper.ToRadians(-10) & this.Rotation - DesiredRotation > MathHelper.ToRadians(-170) & !this.LockedOn)
                    {
                        this.Rotation += MathHelper.ToRadians(10);
                    }
                    else if (this.Rotation - DesiredRotation > MathHelper.ToRadians(10) & this.Rotation - DesiredRotation < MathHelper.ToRadians(170) & !this.LockedOn)
                    {
                        this.Rotation -= MathHelper.ToRadians(10);
                    }
                    else
                    {
                        this.Rotation = DesiredRotation;
                        this.LockedOn = true;
                    }
                    this.Position = new Vector2(this.Position.X + (float)Math.Cos(this.Rotation) * this.Speed, this.Position.Y + (float)Math.Sin(this.Rotation) * this.Speed);
                    this.TargetPosition = new Vector2(this.Boss.Position.X + this.Boss.Sprite100.Width / 2 + (float)Math.Cos(this.Rotation) * 10000, this.Boss.Position.Y + this.Boss.Sprite100.Height / 2 + (float)Math.Sin(this.Rotation) * 10000);
                }
                else
                {
                    this.Rotation = (float)Math.Atan2(this.TargetPosition.Y - this.Position.Y, this.TargetPosition.X - this.Position.X);
                    this.Position = new Vector2(this.Position.X + (float)Math.Cos(this.Rotation) * this.Speed, this.Position.Y + (float)Math.Sin(this.Rotation) * this.Speed);
                }
            }
        }
    }

    public class Laser : Attack
    {
        public Weapon Weapon { get; set; }
        private int MoveCount;

        public Laser(Vector2 position, float rotation, Weapon weapon, int damage, Texture2D sprite, Texture2D bwSprite)
            : base(position, rotation, damage, sprite, bwSprite)
        {
            this.Weapon = weapon;
            this.MoveCount = 1;
        }

        public override void Move()
        {
            this.Position = new Vector2(this.Weapon.Position.X - this.Sprite.Width / 2 + (float)Math.Cos(this.Rotation) * 40 * this.MoveCount, this.Weapon.Position.Y + (float)Math.Sin(this.Rotation) * 40 * this.MoveCount);
            this.MoveCount++;
        }
    }

    public class DelayedHomingProjectile : Attack
    {
        public int Speed { get; set; }
        public List<Enemy> Targets { get; set; }
        public Boss Boss { get; set; }
        public Player Player { get; set; }
        private int LockOnDelay;
        public int speed { get; set; }
        private float X;
        private float Y;

        public DelayedHomingProjectile(Vector2 position, float rotation, List<Enemy> targets, Boss boss, int damage, int speed, int startSpeed, Texture2D sprite, Texture2D bwSprite)
            : base(position, rotation, damage, sprite, bwSprite)
        {
            this.Speed = speed;
            this.speed = startSpeed;
            this.LockOnDelay = 100;
            this.Targets = targets;
            this.Boss = boss;
            this.Player = null;
            this.X = (float)Math.Cos(this.Rotation);
            this.Y = (float)Math.Sin(this.Rotation);
        }

        public DelayedHomingProjectile(Vector2 position, float rotation, Player player, int damage, int speed, int startSpeed, Texture2D sprite, Texture2D bwSprite)
            : base(position, rotation, damage, sprite, bwSprite)
        {
            this.Speed = speed;
            this.speed = startSpeed;
            this.LockOnDelay = 100;
            this.Player = player;
            this.Targets = new List<Enemy>();
            this.X = (float)Math.Cos(this.Rotation);
            this.Y = (float)Math.Sin(this.Rotation);
        }

        public override void Move()
        {
            if (this.LockOnDelay < 0)
            {
                this.Position = new Vector2(this.Position.X + this.X, this.Position.Y + this.Y);
            }
            else if (this.LockOnDelay == 0)
            {
                if (this.Targets.Count > 0)
                {
                    Enemy NearestTarget = this.Targets.ElementAt<Enemy>(0);
                    double NearestTargetDist = Math.Sqrt(Math.Pow(this.Position.X - NearestTarget.Position.X, 2) + Math.Pow(this.Position.Y - NearestTarget.Position.Y, 2));
                    foreach (Enemy Target in this.Targets)
                    {
                        double TargetDist = Math.Sqrt(Math.Pow(this.Position.X - Target.Position.X, 2) + Math.Pow(this.Position.Y - Target.Position.Y, 2));
                        if (TargetDist < NearestTargetDist)
                        {
                            NearestTarget = Target;
                            NearestTargetDist = TargetDist;
                        }
                    }
                    this.Rotation = (float)Math.Atan2((NearestTarget.Position.Y + NearestTarget.Sprite100.Height / 2) - this.Position.Y, (NearestTarget.Position.X + NearestTarget.Sprite100.Width / 2) - this.Position.X);
                }
                else if (this.Boss != null && this.Boss.Spawned)
                {
                    this.Rotation = (float)Math.Atan2((Boss.Position.Y + Boss.Sprite100.Height / 2) - this.Position.Y, (Boss.Position.X + Boss.Sprite100.Width / 2) - this.Position.X);
                }
                else if (this.Player != null)
                {
                    this.Rotation = (float)Math.Atan2((Player.Position.Y + Player.Sprite100.Height / 2) - this.Position.Y, (Player.Position.X + Player.Sprite100.Width / 2) - this.Position.X);
                }
                this.X = (float)Math.Cos(this.Rotation) * this.Speed;
                this.Y = (float)Math.Sin(this.Rotation) * this.Speed;
                this.LockOnDelay--;
                this.speed++;
            }
            else
            {
                this.LockOnDelay--;
                if (this.speed > 0)
                {
                    this.Position = new Vector2(this.Position.X + this.X * this.speed, this.Position.Y + this.Y * this.speed);
                    this.speed--;
                }
            }
        }
    }

    public class Lightning : Attack
    {
        public int Length { get; set; }
        public List<Dot> Dots { get; set; }
        private Random rand;

        public Lightning(Vector2 position, float rotation, int damage, int length, int randInit)
            : base(position, rotation, damage, null, null)
        {
            this.Length = length;
            this.Dots = new List<Dot>();
            this.rand = new Random(randInit);
            this.Rotation += MathHelper.ToRadians(rand.Next(-5, 6));
        }

        public override void Move()
        {
            if (Length > 0)
            {
                this.Rotation += MathHelper.ToRadians(rand.Next(-50 / (int)MathHelper.Clamp((this.Length / 2), 1, 10), 51 / (int)MathHelper.Clamp((this.Length / 2), 1, 10)));
                for (int i = 1; i <= rand.Next(80, 121); i++)
                {
                    if (i == 25 | i == 50 | i == 75)
                    {
                        this.Rotation += MathHelper.ToRadians(rand.Next(-25 / (int)MathHelper.Clamp((this.Length / 2), 1, 10), 26 / (int)MathHelper.Clamp((this.Length / 2), 1, 10)));
                    }
                    Dots.Add(new Dot(this.Position, 3));
                    this.Position = new Vector2(this.Position.X + (float)Math.Cos(this.Rotation), this.Position.Y + (float)Math.Sin(this.Rotation));
                }
                this.Length--;
            }
            foreach (Dot Dot in Dots)
            {
                Dot.Life--;
            }
        }

        public override bool HitTest(Vector2 upperLeft, Vector2 lowerRight)
        {
            bool hit = false;
            foreach (Dot Dot in Dots)
            {
                if (Dot.Position.X >= upperLeft.X - 1 & Dot.Position.X <= lowerRight.X + 1 & Dot.Position.Y <= lowerRight.Y + 1 & Dot.Position.Y >= upperLeft.Y - 1)
                {
                    hit = true;
                }
            }
            return hit;
        }
    }

    public class Shockwave : Attack
    {
        public int Speed { get; set; }
        public int Width { get; set; }
        public List<Dot> Dots { get; set; }
        private Vector2 StartPosition;
        private int speed;

        public Shockwave(Vector2 position, float rotation, int damage, int speed, int width)
            : base(position, rotation, damage, null, null)
        {
            this.Speed = speed;
            this.StartPosition = position;
            this.Width = 6 * width;
            this.Dots = new List<Dot>();
        }

        public override void Move()
        {
            for (int i = -this.Width; i <= this.Width; i++)
            {
                for (float j = 0; j <= 1; j += .1f)
                {
                    this.Position = new Vector2(this.StartPosition.X + (float)Math.Cos(this.Rotation + MathHelper.ToRadians(i + j)) * this.speed, this.StartPosition.Y + (float)Math.Sin(this.Rotation + MathHelper.ToRadians(i + j)) * this.speed);
                    this.Dots.Add(new Dot(this.Position, 0));
                }
            }
            foreach (Dot Dot in Dots)
            {
                Dot.Life--;
            }
            this.speed += this.Speed;
        }

        public override bool HitTest(Vector2 upperLeft, Vector2 lowerRight)
        {
            bool hit = false;
            foreach (Dot Dot in Dots)
            {
                if (Dot.Position.X >= upperLeft.X - 1 & Dot.Position.X <= lowerRight.X + 1 & Dot.Position.Y <= lowerRight.Y + 1 & Dot.Position.Y >= upperLeft.Y - 1)
                {
                    hit = true;
                }
            }
            return hit;
        }
    }

    public class Dot
    {
        public Vector2 Position { get; set; }
        public int Life { get; set; }

        public Dot(Vector2 position, int life)
        {
            this.Life = life;
            this.Position = position;
        }
    }

    public class Item
    {
        public Vector2 Position { get; set; }
        public string Name { get; set; }
        public Texture2D Sprite { get; set; }
        public Texture2D BWSprite { get; set; }

        public Item(Vector2 position, Texture2D sprite, Texture2D bwSprite)
        {
            this.Position = position;
            this.Sprite = sprite;
            this.BWSprite = bwSprite;
            if (this.Position.X < 0)
                this.Position = new Vector2(0, this.Position.Y);
            if (this.Position.X > 750 - this.Sprite.Width)
                this.Position = new Vector2(750 - this.Sprite.Width, this.Position.Y);
        }

        public void Move()
        {
            this.Position = new Vector2(this.Position.X, this.Position.Y + 1);
        }

        public bool HitTest(Vector2 upperLeft, Vector2 lowerRight)
        {
            if (this.Position.X + this.Sprite.Width / 2 > upperLeft.X & this.Position.X + this.Sprite.Width / 2 < lowerRight.X & this.Position.Y + this.Sprite.Height / 2 < lowerRight.Y & this.Position.Y + this.Sprite.Height / 2 > upperLeft.Y)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public class WeaponPickup : Item
    {
        public int Type { get; set; }
        public int FireRate { get; set; }
        public int Damage { get; set; }
        public int ProjectileSpeed { get; set; }
        public int Projectiles { get; set; }

        public WeaponPickup(string name, Vector2 position, Texture2D sprite, Texture2D bwSprite, int type, int fireRate, int damage, int projectileSpeed, int projectiles)
            : base(position, sprite, bwSprite)
        {
            this.Name = name;
            this.Type = type;
            this.FireRate = fireRate;
            this.Damage = damage;
            this.ProjectileSpeed = projectileSpeed;
            this.Projectiles = projectiles;
        }

        public WeaponPickup(Vector2 position, Texture2D sprite, Texture2D bwSprite, int randInit)
            : base(position, sprite, bwSprite)
        {
            Random rand = new Random(randInit);
            this.Type = rand.Next(1, 7);
            switch (this.Type)
            {
                case 1:
                    this.FireRate = rand.Next(50, 501);
                    this.Projectiles = rand.Next(1, 11);
                    this.Damage = (int)MathHelper.Clamp(rand.Next(25, 41) * this.FireRate / 50 / this.Projectiles, 1, 1000);
                    this.ProjectileSpeed = rand.Next(15, 26);
                    this.Name = "Laser Projectiles";
                    if (this.Damage >= 70)
                        this.Name = "Powerful " + this.Name;
                    if (this.Damage <= 30)
                        this.Name = "Weak " + this.Name;
                    if (this.FireRate <= 150)
                        this.Name = "Fast " + this.Name;
                    if (this.FireRate >= 400)
                        this.Name = "Slow " + this.Name;
                    if (this.Projectiles >= 7)
                        this.Name = "Many " + this.Name;
                    if (this.Projectiles <= 3)
                        this.Name = "Few " + this.Name;
                    break;
                case 2:
                    this.FireRate = rand.Next(100, 401);
                    this.Projectiles = rand.Next(1, 11);
                    this.Damage = (int)MathHelper.Clamp(rand.Next(35, 51) * this.FireRate / 100 / this.Projectiles, 1, 1000);
                    this.ProjectileSpeed = rand.Next(15, 26);
                    this.Name = "Homing Missiles";
                    if (this.Damage >= 35)
                        this.Name = "Powerful " + this.Name;
                    if (this.Damage <= 15)
                        this.Name = "Weak " + this.Name;
                    if (this.FireRate <= 175)
                        this.Name = "Fast " + this.Name;
                    if (this.FireRate >= 325)
                        this.Name = "Slow " + this.Name;
                    if (this.Projectiles >= 7)
                        this.Name = "Many " + this.Name;
                    if (this.Projectiles <= 3)
                        this.Name = "Few " + this.Name;
                    break;
                case 3:
                    this.FireRate = rand.Next(10, 16);
                    this.Projectiles = rand.Next(1, 6);
                    this.Damage = (int)MathHelper.Clamp(rand.Next(10, 21) / this.Projectiles, 2, 20);
                    this.ProjectileSpeed = 20;
                    this.Name = "Laser";
                    if (this.Projectiles >= 4)
                        this.Name = "Multi " + this.Name;
                    if (this.Projectiles == 1)
                        this.Name = "Single " + this.Name;
                    if (this.Projectiles == 2)
                        this.Name = "Double " + this.Name;
                    if (this.Projectiles == 3)
                        this.Name = "Triple " + this.Name;
                    if (this.Damage >= 10)
                        this.Name = "Powerful " + this.Name;
                    if (this.Damage <= 4)
                        this.Name = "Weak " + this.Name;
                    break;
                case 4:
                    this.FireRate = rand.Next(200, 2001);
                    this.Projectiles = rand.Next(15, 101);
                    this.Damage = (int)MathHelper.Clamp(rand.Next(10, 18) * (this.FireRate / 250) / (this.Projectiles / 10), 1, 1000);
                    this.ProjectileSpeed = rand.Next(15, 26);
                    this.Name = "Homing Volley";
                    if (this.Damage >= 20)
                        this.Name = "Powerful " + this.Name;
                    if (this.Damage <= 7)
                        this.Name = "Weak " + this.Name;
                    if (this.Projectiles >= 75)
                        this.Name = "Large " + this.Name;
                    if (this.Projectiles <= 40)
                        this.Name = "Small " + this.Name;
                    if (this.FireRate <= 500)
                        this.Name = "Fast " + this.Name;
                    if (this.FireRate >= 1500)
                        this.Name = "Slow " + this.Name;
                    break;
                case 5:
                    this.FireRate = rand.Next(50, 1001);
                    this.Projectiles = rand.Next(1, 11);
                    this.ProjectileSpeed = rand.Next(2, 11);
                    this.Damage = (int)MathHelper.Clamp(rand.Next(15, 21) * this.FireRate / (this.Projectiles * 10) / (this.ProjectileSpeed * 5), 1, 1000);
                    this.Name = "Lightning";
                    if (this.Projectiles >= 4)
                        this.Name = "Multi " + this.Name;
                    if (this.Projectiles == 1)
                        this.Name = "Single " + this.Name;
                    if (this.Projectiles == 2)
                        this.Name = "Double " + this.Name;
                    if (this.Projectiles == 3)
                        this.Name = "Triple " + this.Name;
                    if (this.Damage >= 50)
                        this.Name = "Powerful " + this.Name;
                    if (this.Damage <= 20)
                        this.Name = "Weak " + this.Name;
                    if (this.ProjectileSpeed >= 7)
                        this.Name = "Long " + this.Name;
                    if (this.ProjectileSpeed <= 3)
                        this.Name = "Short " + this.Name;
                    if (this.FireRate <= 200)
                        this.Name = "Fast " + this.Name;
                    if (this.FireRate >= 700)
                        this.Name = "Slow " + this.Name;
                    break;
                case 6:
                    this.FireRate = rand.Next(500, 3001);
                    this.Projectiles = rand.Next(1, 11);
                    this.ProjectileSpeed = rand.Next(15, 31);
                    this.Damage = (int)MathHelper.Clamp(rand.Next(2, 5) * (this.FireRate / 150) / this.Projectiles * (this.ProjectileSpeed / 5), 1, 1000);
                    this.Name = "Shockwave";
                    if (this.Damage >= 50)
                        this.Name = "Powerful " + this.Name;
                    if (this.Damage <= 20)
                        this.Name = "Weak " + this.Name;
                    if (this.FireRate <= 800)
                        this.Name = "Fast " + this.Name;
                    if (this.FireRate >= 2000)
                        this.Name = "Slow " + this.Name;
                    if (this.Projectiles >= 7)
                        this.Name = "Wide " + this.Name;
                    if (this.Projectiles <= 3)
                        this.Name = "Small " + this.Name;
                    break;
            }
        }
    }

    public class Effect
    {
        public Vector2 Position { get; set; }
        public float Rotation { get; set; }
        private float X;
        private float Y;
        public int Speed { get; set; }
        public int Duration { get; set; }
        public int Life { get; set; }
        public Texture2D Sprite100 { get; set; }
        public Texture2D Sprite66 { get; set; }
        public Texture2D Sprite33 { get; set; }
        public Texture2D BWSprite100 { get; set; }
        public Texture2D BWSprite66 { get; set; }
        public Texture2D BWSprite33 { get; set; }

        public Effect(Vector2 position, float rotation, int speed, int duration, Texture2D sprite100, Texture2D sprite66, Texture2D sprite33, Texture2D bwSprite100, Texture2D bwSprite66, Texture2D bwSprite33)
        {
            this.Position = position;
            this.Rotation = rotation;
            this.Speed = speed;
            this.X = (float)Math.Cos(this.Rotation) * this.Speed;
            this.Y = (float)Math.Sin(this.Rotation) * this.Speed;
            this.Duration = duration;
            this.Life = duration;
            this.Sprite100 = sprite100;
            this.Sprite66 = sprite66;
            this.Sprite33 = sprite33;
            this.BWSprite100 = bwSprite100;
            this.BWSprite66 = bwSprite66;
            this.BWSprite33 = bwSprite33;
        }

        public void Move()
        {
            this.Position = new Vector2(this.Position.X + this.X, this.Position.Y + this.Y);
            this.Life--;
        }
    }

    public class Button
    {
        public Vector2 Position { get; set; }
        public string Text { get; set; }
        public int Width { get; set; }
        public float Visibility { get; set; }
        public bool Black { get; set; }

        public Button(Vector2 position, string text, float visibility, bool black)
        {
            this.Position = position;
            this.Text = text;
            this.Width = this.Text.Length * 12;
            this.Visibility = visibility;
            this.Black = black;
        }
    }

    [Serializable]
    public class SaveGame
    {
        public List<HighScore> HighScoresL1 { get; set; }
        public List<HighScore> HighScoresL2 { get; set; }
        public List<HighScore> HighScoresL3 { get; set; }
        public List<HighScore> HighScoresL4 { get; set; }
        public List<HighScore> HighScoresL5 { get; set; }
        public string CurrentName { get; set; }
        public List<ProgressFile> ProgressList { get; set; }

        public SaveGame() { }

        public SaveGame(List<HighScore> highScoresL1, List<HighScore> highScoresL2, List<HighScore> highScoresL3, List<HighScore> highScoresL4, List<HighScore> highScoresL5, string currentName, List<ProgressFile> progressList)
        {
            this.HighScoresL1 = highScoresL1;
            this.HighScoresL2 = highScoresL2;
            this.HighScoresL3 = highScoresL3;
            this.HighScoresL4 = highScoresL4;
            this.HighScoresL5 = highScoresL5;
            this.CurrentName = currentName;
            this.ProgressList = progressList;
        }
    }

    [Serializable]
    public class HighScore
    {
        public string Name { get; set; }
        public int Score { get; set; }

        public HighScore() { }

        public HighScore(string name, int score)
        {
            this.Name = name;
            this.Score = score;
        }
    }

    [Serializable]
    public class ProgressFile
    {
        public string Name { get; set; }
        public int UnlockedLevel { get; set; }

        public ProgressFile() { }

        public ProgressFile(string name, int unlockedLevel)
        {
            this.Name = name;
            this.UnlockedLevel = unlockedLevel;
        }
    }
}