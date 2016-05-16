using System;

namespace MVC5App.Services.Models
{
    public class Difficulty
    {
        public enum DifficultyEnum {Easy, Medium, Hard, Deadly};
        public Difficulty(int level)
        {
            SetDifficulty(level);
        }


        public int Easy { get; set; }
        public int Medium { get; set; }
        public int Hard { get; set; }
        public int Deadly { get; set; }

        private void SetDifficulty(int level)
        {
            switch (level)
            {
                case 1:
                    Easy = 25;
                    Medium = 50;
                    Hard = 75;
                    Deadly = 100;
                    break;
                case 2:
                    Easy = 50;
                    Medium = 100;
                    Hard = 150 ;
                    Deadly =  200;
                    break;
                case 3:
                    Easy = 75;
                    Medium = 150;
                    Hard = 225;
                    Deadly = 400;
                    break;

                //We need to cover up to level 20 here
            }
        }
    }
}