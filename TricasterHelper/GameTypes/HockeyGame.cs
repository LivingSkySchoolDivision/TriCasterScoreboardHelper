using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;

namespace TricasterHelper.GameTypes
{
    enum HockeyGameSegment
    {
        FirstPeriod,
        SecondPeriod,
        ThirdPeriod,
        Overtime,
        Shootout
    }

    class HockeyGame : Game
    {
        public Team HomeTeam { get; set; }
        public Team AwayTeam { get; set; }

        public HockeyGameSegment GameSegment { get; set; }
        public GameClock Clock { get; set; }
        public GameClock HomePowerPlayClock { get; set; }
        public GameClock AwayPowerPlayClock { get; set; }

        public HockeyGame(string fileName)
            : base(fileName)
        {
            HomeTeam = new Team("Home Team");
            AwayTeam = new Team("Away Team");
            GameSegment = HockeyGameSegment.FirstPeriod;
            this.Clock = new GameClock(0,0);
            this.Clock.CountsDown = true;

            this.HomePowerPlayClock = new GameClock(0,0);
            this.HomePowerPlayClock.CountsDown = true;

            this.AwayPowerPlayClock = new GameClock(0,0);
            this.AwayPowerPlayClock.CountsDown = true;
        }
        public override void Save()
        {
            try
            {
                StringBuilder saveBuffer = new StringBuilder();

                switch (this.GameSegment)
                {
                    case HockeyGameSegment.FirstPeriod:
                        saveBuffer.AppendLine("GameSegmentFull = First Period");
                        saveBuffer.AppendLine("GameSegmentReallyShort = 1st");
                        saveBuffer.AppendLine("GameSegmentShort = First");
                        break;
                    case HockeyGameSegment.SecondPeriod:
                        saveBuffer.AppendLine("GameSegmentFull = Second Period");
                        saveBuffer.AppendLine("GameSegmentReallyShort = 2nd");
                        saveBuffer.AppendLine("GameSegmentShort = Second");
                        break;
                    case HockeyGameSegment.ThirdPeriod:
                        saveBuffer.AppendLine("GameSegmentFull = Third Period");
                        saveBuffer.AppendLine("GameSegmentReallyShort = 3rd");
                        saveBuffer.AppendLine("GameSegmentShort = Third");
                        break;
                    case HockeyGameSegment.Overtime:
                        saveBuffer.AppendLine("GameSegmentFull = Overtime");
                        saveBuffer.AppendLine("GameSegmentReallyShort = OT");
                        saveBuffer.AppendLine("GameSegmentShort = OT");
                        break;
                    case HockeyGameSegment.Shootout:
                        saveBuffer.AppendLine("GameSegmentFull = Shootout");
                        saveBuffer.AppendLine("GameSegmentReallyShort = SO");
                        saveBuffer.AppendLine("GameSegmentShort = Shootout");
                        break;
                }

                saveBuffer.AppendLine("HomeTeamName = " + this.HomeTeam.Name);
                saveBuffer.AppendLine("HomeTeamScore = " + this.HomeTeam.Score);

                saveBuffer.AppendLine("AwayTeamName = " + this.AwayTeam.Name);
                saveBuffer.AppendLine("AwayTeamScore = " + this.AwayTeam.Score);

                saveBuffer.AppendLine("GameClockSeconds = " + this.Clock.Seconds);
                saveBuffer.AppendLine("GameClockMinutes = " + this.Clock.Minutes);
                saveBuffer.AppendLine("GameClockHours = " + this.Clock.Hours);
                saveBuffer.AppendLine("GameClockTotalMinutes = " + this.Clock.TotalMinutes);
                saveBuffer.AppendLine("GameClockFriendly = " + this.Clock.TotalMinutes.ToString("D2") + ":" + this.Clock.Seconds.ToString("D2"));
                saveBuffer.AppendLine("GameClockFriendlyWithHours = " + this.Clock.Hours.ToString("D1") + ":" + this.Clock.Minutes.ToString("D2") + ":" + this.Clock.Seconds.ToString("D2"));

                saveBuffer.AppendLine("HomePowerPlayClockSeconds = " + this.HomePowerPlayClock.Seconds);
                saveBuffer.AppendLine("HomePowerPlayClockMinutes = " + this.HomePowerPlayClock.Minutes);
                saveBuffer.AppendLine("HomePowerPlayClockHours = " + this.HomePowerPlayClock.Hours);
                saveBuffer.AppendLine("HomePowerPlayClockTotalMinutes = " + this.HomePowerPlayClock.TotalMinutes);
                saveBuffer.AppendLine("HomePowerPlayClockFriendly = " + this.HomePowerPlayClock.TotalMinutes.ToString("D2") + ":" + this.HomePowerPlayClock.Seconds.ToString("D2"));
                saveBuffer.AppendLine("HomePowerPlayClockFriendlyWithHours = " + this.HomePowerPlayClock.Hours.ToString("D1") + ":" + this.HomePowerPlayClock.Minutes.ToString("D2") + ":" + this.HomePowerPlayClock.Seconds.ToString("D2"));

                saveBuffer.AppendLine("AwayPowerPlayClockSeconds = " + this.AwayPowerPlayClock.Seconds);
                saveBuffer.AppendLine("AwayPowerPlayClockMinutes = " + this.AwayPowerPlayClock.Minutes);
                saveBuffer.AppendLine("AwayPowerPlayClockHours = " + this.AwayPowerPlayClock.Hours);
                saveBuffer.AppendLine("AwayPowerPlayClockTotalMinutes = " + this.AwayPowerPlayClock.TotalMinutes);
                saveBuffer.AppendLine("AwayPowerPlayClockFriendly = " + this.AwayPowerPlayClock.TotalMinutes.ToString("D2") + ":" + this.AwayPowerPlayClock.Seconds.ToString("D2"));
                saveBuffer.AppendLine("AwayPowerPlayClockFriendlyWithHours = " + this.AwayPowerPlayClock.Hours.ToString("D1") + ":" + this.AwayPowerPlayClock.Minutes.ToString("D2") + ":" + this.AwayPowerPlayClock.Seconds.ToString("D2"));

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
