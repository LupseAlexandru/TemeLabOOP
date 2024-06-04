using System;
using System.Collections.Generic;

namespace Robot
{
    public class Superman : Entity
    {
        private static Random random = new Random();
        private static List<string> powers = new List<string> { "Freeze", "PowerPunch", "LaserEye" };

        public Superman(int health, bool hitback) : base(health, hitback) { }

        public override int Attack(Entity target)
        {
            string power = powers[random.Next(powers.Count)];
            int damage = 0;

            switch (power)
            {
                case "Freeze":
                    damage = 15;
                    break;
                case "PowerPunch":
                    damage = 20;
                    break;
                case "LaserEye":
                    damage = 40;
                    break;
            }

            target.TakeDamage(damage);
            Console.WriteLine($"{GetType().Name} -> {power} -> {(target as Robot)?.Name ?? target.GetType().Name}  {(target as Robot)?.Name ?? target.GetType().Name} Health = {target.Health}%");

            return damage;
        }
    }
}