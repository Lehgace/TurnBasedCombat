using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurnBasedCombat
{
    internal class Unit
    {
        private int currentHp;
        private int maxHp;
        private int attackPower;
        private int healPower;
        private string unitName;
        private Random random;
        private Critical critical;
        Affiliation affiliation;

        // Unit Constructor
        public Unit(int maxHp, int attackPower, int healPower, string unitName, Affiliation affiliation) 
        { 
            this.maxHp = maxHp;
            this.currentHp = maxHp; // at unit creation, their current HP is maxHp
            this.attackPower = attackPower;
            this.healPower = healPower;
            this.unitName = unitName;
            this.affiliation = affiliation;
            this.random  = new Random();
            this.critical = new Critical();
        }

        public int Hp { get { return currentHp; } }

        public string UnitName { get { return unitName; } }
        public bool IsDead { get { return currentHp <= 0; } }


        // Calculate a modifier between 0.75 - 1.25 to apply to a unit's heal or attack power
        public int CalculateFluctuation(int valueToFluctuate)
        {
            double rng = random.NextDouble(); // random number between 0.0 and 1.0
            rng = rng / 2 + 0.75f;
            return (int)(valueToFluctuate * rng);
        }

        // Function that takes a unit to attack as the input.
        // Calls TakeDamage to inflict random attack damage to target unit's Hp
        public void Attack(Unit unitToAttack)
        {
            int randomDamage = CalculateFluctuation(attackPower);

            if (critical.RollForCrit())
            {
                critical.PrintCriticalHit();
                int criticalDamage = critical.ApplyCritDamage(randomDamage);
                Console.WriteLine(unitName + " critically hits " + unitToAttack.unitName + " and deals " + criticalDamage + " damage!");
                unitToAttack.TakeDamage(criticalDamage);
            }
            else
            {
                Console.WriteLine(unitName + " attacks " + unitToAttack.unitName + " and deals " + randomDamage + " damage!");
                unitToAttack.TakeDamage(randomDamage);
            }
        }

        // Lower HP of specified unit with given damage value
        public void TakeDamage(int damage)
        {
            currentHp -= damage;

            if(IsDead)
            {
                Console.WriteLine(UnitName + " has been defeated!");
            }
        }

        public void Heal()
        {
            int heal = CalculateFluctuation(healPower);
            // Prevent unit from healing higher than their max hp
            currentHp = heal + currentHp > maxHp ? maxHp : currentHp + heal;
            Console.WriteLine(UnitName + " heals " + heal + " Hp!");
        }

        // Apply colors to unit hp printout using their affiliation. Ally affiliation = blue, Foe affiliation = red.
        public void PrintUnitHp()
        {
            Console.ForegroundColor = affiliation == Affiliation.Ally ? Console.ForegroundColor = ConsoleColor.Blue : Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(UnitName + " :: " + Hp + " Hp");
            Console.ResetColor();
        }

    }
}
