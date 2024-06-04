using System;

namespace Robot
{
    public class Entity
    {
        public int Health { get; set; }
        public bool AttackBack { get; set; }

        public Entity(int health, bool attack)
        {
            Health = health;
            AttackBack = attack;
        }

        public virtual int Attack(Entity target)
        {
            return 0;
        }

        public virtual void TakeDamage(int damage)
        {
            Health -= damage;
            if (Health < 0)
                Health = 0;
        }
    }

    public class Animal : Entity
    {
        public Animal(int health) : base(health, false) { }
    }

    public class Human : Entity
    {
        public Human(int health) : base(health, false) { }
    }
}
