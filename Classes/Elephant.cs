using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoo_Simulator.Classes
{
    public class Elephant : Animal
    {
        bool canWalk = true;
        float prevHealth;

        public float PrevHealth
        {
            get { return prevHealth; }
            set { prevHealth = value; }
        }

        public Elephant()
        {

        }

        public override void CheckAlive()
        {
            if (IsAlive)
            {
                if (!canWalk && PrevHealth < 70)
                {
                    IsAlive = false;
                }

                if (Health < 70)
                {
                    canWalk = false;
                }
                else
                {
                    canWalk = true;
                }
            }
        }
    }
}
