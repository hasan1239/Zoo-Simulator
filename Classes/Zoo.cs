using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoo_Simulator.Classes
{
    public class Zoo
    {
        private List<Giraffe> giraffes = new List<Giraffe>();
        private List<Monkey> monkeys = new List<Monkey>();
        private List<Elephant> elephants = new List<Elephant>();
        private Random rand = new Random();

        public List<Giraffe> Giraffes
        {
            get { return giraffes; }
            set { giraffes = value; }
        }

        public List<Monkey> Monkeys
        {
            get { return monkeys; }
            set { monkeys = value; }
        }

        public List<Elephant> Elephants
        {
            get { return elephants; }
            set { elephants = value; }
        }

        public bool AllAnimalsDead
        {
            get
            {
                bool allGiraffesDead = Giraffes.Where(g => g.IsAlive == true).ToList().Count == 0;
                bool allElephantsDead = Elephants.Where(e => e.IsAlive == true).ToList().Count == 0;
                bool allMonkeysDead = Monkeys.Where(m => m.IsAlive == true).ToList().Count == 0;

                return allGiraffesDead && allElephantsDead && allMonkeysDead;
            }
        }

        public Zoo()
        {
            PopulateZoo();
        }

        public void PopulateZoo(int startingAnimals = 5)
        {
            for (int i = 0; i < startingAnimals; i++)
            {
                Giraffes.Add(new Giraffe());
                Monkeys.Add(new Monkey());
                Elephants.Add(new Elephant());
            }
        }

        public void UpdateAnimals()
        {
            foreach (Elephant elephant in Elephants)
            {
                elephant.PrevHealth = elephant.Health;
                elephant.ReduceHealth(rand.Next(0, 20));
                elephant.CheckAlive();
            }

            foreach (Giraffe giraffe in Giraffes)
            {
                giraffe.ReduceHealth(rand.Next(0, 20));
                giraffe.CheckAlive();
            }

            foreach (Monkey monkey in Monkeys)
            {
                monkey.ReduceHealth(rand.Next(0, 20));
                monkey.CheckAlive();
            }
        }

        public void FeedAnimals()
        {
            int feedElephant = rand.Next(10, 25);
            int feedGiraffe = rand.Next(10, 25);
            int feedMonkey = rand.Next(10, 25);

            foreach (Elephant elephant in Elephants)
            {
                elephant.Feed(feedElephant);
            }

            foreach (Giraffe giraffe in Giraffes)
            {
                giraffe.Feed(feedGiraffe);
            }

            foreach (Monkey monkey in Monkeys)
            {
                monkey.Feed(feedMonkey);
            }
        }
    }
}
