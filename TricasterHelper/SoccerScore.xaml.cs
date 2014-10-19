using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TricasterHelper
{
    /// <summary>
    /// Interaction logic for SoccerScore.xaml
    /// </summary>
    public partial class SoccerScore : Window
    {
        private static SoccerGame game;

        private System.Timers.Timer uiRefreshTimer;

        private void uiRefreshTimerElapsedHandler(object source, ElapsedEventArgs e)
        {
            Dispatcher.Invoke((Action)delegate()
            {
                updateClockFields(game);
            });
            
        }

        public SoccerScore(string fileName)
        {
            game = new SoccerGame(fileName);
            uiRefreshTimer = new System.Timers.Timer(50);
            uiRefreshTimer.Elapsed += uiRefreshTimerElapsedHandler;
            uiRefreshTimer.Enabled = true;
            InitializeComponent();
        }

        private void updateClockFields(SoccerGame game)
        {
            // Timer
            txtGameTimerHours.Text = game.Clock.Hours.ToString("D2");
            txtGameTimerMinutes.Text = game.Clock.Minutes.ToString("D2");
            txtGameTimerSeconds.Text = game.Clock.Seconds.ToString("D2");
            txtGameTimerFriendly.Text = game.Clock.TotalMinutes.ToString("D2") + ":" + game.Clock.Seconds.ToString("D2");

            // Save the file
            game.Save();          
        }

        private void updateFields(SoccerGame game)
        {
            // Home Team
            txtHomeTeamScore.Text = game.HomeTeam.Score.ToString();
            txtHomeTeamName.Text = game.HomeTeam.Name;

            // Away Team
            txtAwayTeamScore.Text = game.AwayTeam.Score.ToString();
            txtAwayTeamName.Text = game.AwayTeam.Name;

            // Game segment
            switch (game.GameSegment)
            {
                case SoccerGameSegment.FirstHalf:
                    btnGameSegment_FirstHalf.IsEnabled = false;
                    btnGameSegment_SecondHalf.IsEnabled = true;
                    btnGameSegment_OverTime.IsEnabled = true;
                    break;
                case SoccerGameSegment.SecondHalf:
                    btnGameSegment_FirstHalf.IsEnabled = true;
                    btnGameSegment_SecondHalf.IsEnabled = false;
                    btnGameSegment_OverTime.IsEnabled = true;
                    break;
                case SoccerGameSegment.Overtime:
                    btnGameSegment_FirstHalf.IsEnabled = true;
                    btnGameSegment_SecondHalf.IsEnabled = true;
                    btnGameSegment_OverTime.IsEnabled = false;

                    break;
            }

            if (game.Clock.IsRunning())
            {
                btnGameTimerStart.IsEnabled = false;
                btnGameTimerStop.IsEnabled = true;
            }
            else
            {
                btnGameTimerStart.IsEnabled = true;
                btnGameTimerStop.IsEnabled = false;
            }

            // Save the file
            game.Save();
        }

        private void btnHomeTeamSave_Click(object sender, RoutedEventArgs e)
        {
            game.HomeTeam.Name = txtHomeTeamName.Text;
            game.Save();
        }

        private void btnHomeTeamPlus1_Click(object sender, RoutedEventArgs e)
        {
            game.HomeTeam.Score += 1;
            updateFields(game);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            updateFields(game);
            uiRefreshTimer.Start();
        }

        private void btnHomeTeamMinus1_Click(object sender, RoutedEventArgs e)
        {
            game.HomeTeam.Score -= 1;
            updateFields(game);
        }

        private void btnAwayTeamPlus2_Click(object sender, RoutedEventArgs e)
        {
            game.AwayTeam.Score += 1;
            updateFields(game);
        }

        private void btnAwayTeamMinus2_Click(object sender, RoutedEventArgs e)
        {
            game.AwayTeam.Score -= 1;
            updateFields(game);
        }

        private void btnHomeTeamResetScore_Click(object sender, RoutedEventArgs e)
        {
            game.HomeTeam.Score = 0;
            updateFields(game);
        }

        private void btnAwayTeamResetScore_Click(object sender, RoutedEventArgs e)
        {
            game.AwayTeam.Score = 0;
            updateFields(game);
        }

        private void btnGameSegment_FirstHalf_Click(object sender, RoutedEventArgs e)
        {
            game.GameSegment = SoccerGameSegment.FirstHalf;
            updateFields(game);
        }

        private void btnGameSegment_SecondHalf_Click(object sender, RoutedEventArgs e)
        {
            game.GameSegment = SoccerGameSegment.SecondHalf;
            updateFields(game);
        }

        private void btnGameSegment_OverTime_Click(object sender, RoutedEventArgs e)
        {
            game.GameSegment = SoccerGameSegment.Overtime;
            updateFields(game);
        }

        private void btnGameTimerStart_Click(object sender, RoutedEventArgs e)
        {
            game.Clock.Start();
            updateFields(game);
        }

        private void btnGameTimerStop_Click(object sender, RoutedEventArgs e)
        {
            game.Clock.Stop();
            updateFields(game);
        }

        private void btnGameTimerSet_Click(object sender, RoutedEventArgs e)
        {
            game.Clock.Set(
                Helpers.ParseInt(txtGameTimerHours_Reset.Text),
                Helpers.ParseInt(txtGameTimerMinutes_Reset.Text),
                Helpers.ParseInt(txtGameTimerSeconds_Reset.Text)
                );
        }

    }
}
