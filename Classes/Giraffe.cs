using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoo_Simulator.Classes
{
    public class Giraffe : Animal
    {
        public Giraffe()
        {

        }

        public override void CheckAlive()
        {
            if (Health < 50)
            {
                IsAlive = false;
            }
        }
    }
}
