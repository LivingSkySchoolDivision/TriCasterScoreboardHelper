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
            try
            {
                StringBuilder saveBuffer = new StringBuilder();

                saveBuffer.AppendLine("GameSegment = " + this.GameSegment);

                saveBuffer.AppendLine("HomeTeamName = " + this.HomeTeam.Name);
                saveBuffer.AppendLine("HomeTeamScore = " + this.HomeTeam.Score);

                saveBuffer.AppendLine("AwayTeamName = " + this.AwayTeam.Name);
                saveBuffer.AppendLine("AwayTeamScore = " + this.AwayTeam.Score);

                saveBuffer.AppendLine("GameClockSeconds = " + this.Clock.Seconds);
                saveBuffer.AppendLine("GameClockMinutes = " + this.Clock.Minutes);
                saveBuffer.AppendLine("GameClockHours = " + this.Clock.Hours);
                saveBuffer.AppendLine("GameClockTotalMinutes = " + this.Clock.TotalMinutes);

                saveBuffer.AppendLine("GameClockFriendlyNoHours = " + this.Clock.TotalMinutes.ToString("D2") + ":" + this.Clock.Seconds.ToString("D2"));
                saveBuffer.AppendLine("GameClockFriendly = " + this.Clock.Hours.ToString("D1") + ":" + this.Clock.Minutes.ToString("D2") + ":" + this.Clock.Seconds.ToString("D2"));

                using (StreamWriter outfile = new StreamWriter(base.fileName))
                {
                    outfile.Write(saveBuffer.ToString());
                }

            }
            catch (Exception) // Yes, this is bad form, but in this case the program needs to keep attempting to save, even if a failure occurs
            {
            }
        }
    }
}
