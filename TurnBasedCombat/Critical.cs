using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurnBasedCombat
{
    public class Critical
    {
        //private int critChance;
        private readonly int critMultiplier = 2;
        private readonly Random random;

        public Critical()
        {
            this.critMultiplier = 2;
            this.random = new Random();
        }

        public void PrintCriticalHit()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("*#* Lucky Hit! *#*");
            Console.ResetColor();
        }

        public bool RollForCrit()
        {
            int rng = random.Next(1, 11);
            bool willCrit = rng == 1; // does rng match 1? If rng matches 1, critical hit
            return willCrit;
        }

        public int ApplyCritDamage(int randomDamage)
        {
            return randomDamage * critMultiplier;
        }
    }
}
