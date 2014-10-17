using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace TricasterHelper
{
    class Team
    {
        private int MinScore { get; set; }
        private int MaxScore { get; set; }

        public string Name { get; set; }

        private int score { get; set; }

        public int Score
        {
            get { return score; }
            set
            {
                score = value;
                if (score > MaxScore)
                {
                    score = MaxScore;
                }
                if (score < MinScore)
                {
                    score = MinScore;
                }
            }
        }

        public Team(string name)
        {
            this.MinScore = 0;
            this.MaxScore = 999;

            this.Name = name;
            this.Score = 0;
        }
    }
}
