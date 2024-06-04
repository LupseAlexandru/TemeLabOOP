using System;
using System.Collections.Generic;

namespace Robot
{
    public class Robot : Entity
    {
        private Random random = new Random();
        public string Name { get; set; }
        public bool Active { get; set; }
        public bool Shield { get; set; }
        public Intensity EyeLaserIntensity { get; set; }
        public List<string> Targets { get; set; }
        public Planet CurrentTarget { get; set; }
        public bool HasLife { get; set; }
        public int MaxHealth { get; set; }
        public int Population { get; set; }
        public int KilledPopulation { get; set; }
        public List<Weapon> Weapons { get; set; }

        public enum Intensity
        {
            Low,
            Medium,
            High,
            Kill
        }

        public enum Planet
        {
            Earth,
            Mars,
            Jupiter
        }

        public class Weapon
        {
            public string Name { get; set; }
            public int Damage { get; set; }

            public Weapon(string name, int damage)
            {
                Name = name;
                Damage = damage;
            }
        }

        public Robot(string name, int health) : base(health, false)
        {
            Name = name;
            Active = true;
            Shield = false;
            EyeLaserIntensity = Intensity.Low;
            Targets = new List<string> { "Animals", "Humans", "Superhero" };
            CurrentTarget = Planet.Earth;
            HasLife = true;
            MaxHealth = health;
            Population = 1000000;
            KilledPopulation = 0;
            Weapons = new List<Weapon>
            {
                new Weapon("Machine Gun", 10),
                new Weapon("Sniper", 20),
                new Weapon("Rocket", 30),
                new Weapon("Grenade", 40),
                new Weapon("Homing Missile", 50)
            };
        }
        public void KillInitialPopulation()
        {
            int initialKill = (int)(0.1 * Population);
            KilledPopulation += initialKill;
        }

        public void DisplayPopulationStats()
        {
            Console.WriteLine($"{Name} arrived on planet {CurrentTarget}.");
            Console.WriteLine($"{Name} killed {((float)KilledPopulation / Population) * 80}% of the human population.");
            Console.WriteLine($"{Name} killed {((float)KilledPopulation / Population) * 150}% of the animal population.");
        }

        public int Fight(Entity entity)
        {
            Console.WriteLine($"{entity.GetType().Name} intercepted {Name}!");

            int thisHealth = this.Health;
            int otherHealth = entity.Health;

            while (thisHealth > 0 && otherHealth > 0)
            {
                int supermanDamage = entity.Attack(this);
                thisHealth -= supermanDamage;
                if (thisHealth <= 0) break;

                int robotDamage = this.Attack(entity);
                otherHealth -= robotDamage;

                if (otherHealth <= 0) break;
            }

            if (thisHealth > 0)
            {
                Console.WriteLine($"{Name} has won against {entity.GetType().Name}!");
            }
            else
            {
                Console.WriteLine($"{entity.GetType().Name} has won against {Name}!");
            }

            return thisHealth;
        }

        private List<string> weapons = new List<string> { "Throw", "LaserBeam", "Slash", "Machine Gun", "Sniper", "Rocket", "Homing Missile" };
        public override int Attack(Entity target)
        {
            string weapon = weapons[random.Next(weapons.Count)];
            int damage = 0;

            switch (weapon)
            {
                case "Throw":
                    damage = 25;
                    break;
                case "LaserBeam":
                    damage = 55;
                    break;
                case "Slash":
                    damage = 45;
                    break;
                case "Machine Gun":
                    damage = 10;
                    break;
                case "Sniper":
                    damage = 15;
                    break;
                case "Rocket":
                    damage = 40;
                    break;
                case "Homing Missile":
                    damage = 50;
                    break;
            }

            target.TakeDamage(damage);
            Console.WriteLine($"{Name} -> {weapon} -> {target.GetType().Name}  {target.GetType().Name} Health = {target.Health}%");

            return damage;
        }

    }
}