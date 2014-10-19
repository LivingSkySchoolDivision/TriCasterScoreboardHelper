using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TricasterHelper
{
    enum SoccerGameSegment
    {
        FirstHalf,
        SecondHalf,
        Overtime
    }
    
    class SoccerGame : Game
    {
        public Team HomeTeam { get; set; }
        public Team AwayTeam { get; set; }
        public FileStream VariableFile { get; set; }
        public SoccerGameSegment GameSegment { get; set; }
        public GameClock Clock { get; set; }
        
        public SoccerGame(string fileName)
            : base(fileName)
        {
            HomeTeam = new Team("Home Team");
            AwayTeam = new Team("Away Team");
            GameSegment = SoccerGameSegment.FirstHalf;
            this.Clock = new GameClock(0, 0);
            this.Clock.CountsDown = false;
        }

        public override void Save()
        {
            
        }
    }
}
