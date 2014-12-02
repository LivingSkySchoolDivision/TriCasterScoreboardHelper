using System;
using System.Collections.Generic;
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
using TricasterHelper.GameTypes;

namespace TricasterHelper
{
    /// <summary>
    /// Interaction logic for HockeyScore.xaml
    /// </summary>
    public partial class HockeyScore : Window
    {
        private HockeyGame game;
        private System.Timers.Timer uiRefreshTimer;

        private void uiRefreshTimerElapsedHandler(object source, ElapsedEventArgs e)
        {
            Dispatcher.Invoke((Action)(() => updateClockFields(game)));
        }

        public HockeyScore(string fileName)
        {
            game = new HockeyGame(fileName);
            uiRefreshTimer = new System.Timers.Timer(50);
            uiRefreshTimer.Elapsed += uiRefreshTimerElapsedHandler;
            uiRefreshTimer.Enabled = true;
            InitializeComponent();
        }

        private void updateClockFields(HockeyGame game)
        {
            // Timer
            txtGameTimerFriendly.Text = game.Clock.TotalMinutes.ToString("D2") + ":" + game.Clock.Seconds.ToString("D2");
            txtAwayTimerFriendly.Text = game.AwayPowerPlayClock.TotalMinutes.ToString("D2") + ":" + game.AwayPowerPlayClock.Seconds.ToString("D2");
            txtHomeTimerFriendly.Text = game.HomePowerPlayClock.TotalMinutes.ToString("D2") + ":" + game.HomePowerPlayClock.Seconds.ToString("D2");

            // Save the file
            game.Save();
        }

        private void updateFields(HockeyGame game)
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
                case HockeyGameSegment.FirstPeriod:
                    btnGameSegment_FirstPeriod.IsEnabled = false;
                    btnGameSegment_SecondPeriod.IsEnabled = true;
                    btnGameSegment_ThirdPeriod.IsEnabled = true;
                    btnGameSegment_Shootout.IsEnabled = true;
                    btnGameSegment_Overtime.IsEnabled = true;
                    break;
                case HockeyGameSegment.SecondPeriod:
                    btnGameSegment_FirstPeriod.IsEnabled = true;
                    btnGameSegment_SecondPeriod.IsEnabled = false;
                    btnGameSegment_ThirdPeriod.IsEnabled = true;
                    btnGameSegment_Shootout.IsEnabled = true;
                    btnGameSegment_Overtime.IsEnabled = true;
                    break;
                case HockeyGameSegment.ThirdPeriod:
                    btnGameSegment_FirstPeriod.IsEnabled = true;
                    btnGameSegment_SecondPeriod.IsEnabled = true;
                    btnGameSegment_ThirdPeriod.IsEnabled = false;
                    btnGameSegment_Shootout.IsEnabled = true;
                    btnGameSegment_Overtime.IsEnabled = true;
                    break;
                case HockeyGameSegment.Overtime:
                    btnGameSegment_FirstPeriod.IsEnabled = true;
                    btnGameSegment_SecondPeriod.IsEnabled = true;
                    btnGameSegment_ThirdPeriod.IsEnabled = true;
                    btnGameSegment_Shootout.IsEnabled = true;
                    btnGameSegment_Overtime.IsEnabled = false;
                    break;
                case HockeyGameSegment.Shootout:
                    btnGameSegment_FirstPeriod.IsEnabled = true;
                    btnGameSegment_SecondPeriod.IsEnabled = true;
                    btnGameSegment_ThirdPeriod.IsEnabled = true;
                    btnGameSegment_Shootout.IsEnabled = false;
                    btnGameSegment_Overtime.IsEnabled = true;
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

            // Game clock count up/down toggles
            if (game.Clock.CountsDown)
            {
                btnTimerCountsUp.IsEnabled = true;
                btnTimerCountsDown.IsEnabled = false;
            }
            else
            {
                btnTimerCountsUp.IsEnabled = false;
                btnTimerCountsDown.IsEnabled = true;
            }

            // Power play clock count up/down toggles (Home team)
            if (game.HomePowerPlayClock.CountsDown)
            {
                btnHomeTimerCountsUp.IsEnabled = true;
                btnHomeTimerCountsDown.IsEnabled = false;
            }
            else
            {
                btnHomeTimerCountsUp.IsEnabled = false;
                btnHomeTimerCountsDown.IsEnabled = true;
            }

            // Power play clock count up/down toggles (Away team)
            if (game.AwayPowerPlayClock.CountsDown)
            {
                btnAwayTimerCountsUp.IsEnabled = true;
                btnAwayTimerCountsDown.IsEnabled = false;
            }
            else
            {
                btnAwayTimerCountsUp.IsEnabled = false;
                btnAwayTimerCountsDown.IsEnabled = true;
            }

            // Save the file
            game.Save();
        }

        private void BtnGameSegment_FirstPeriod_OnClick(object sender, RoutedEventArgs e)
        {
            game.GameSegment = HockeyGameSegment.FirstPeriod;
            updateFields(game);
        }

        private void BtnGameSegment_ThirdPeriod_OnClick(object sender, RoutedEventArgs e)
        {
            game.GameSegment = HockeyGameSegment.ThirdPeriod;
            updateFields(game);
        }

        private void BtnGameSegment_Overtime_OnClick(object sender, RoutedEventArgs e)
        {
            game.GameSegment = HockeyGameSegment.Overtime;
            updateFields(game);
        }

        private void BtnGameSegment_SecondPeriod_OnClick(object sender, RoutedEventArgs e)
        {
            game.GameSegment = HockeyGameSegment.SecondPeriod;
            updateFields(game);
        }

        private void BtnGameSegment_Shootout_OnClick(object sender, RoutedEventArgs e)
        {
            game.GameSegment = HockeyGameSegment.Shootout;
            updateFields(game);
        }

        private void btnGameTimerSet_Click(object sender, RoutedEventArgs e)
        {
            game.Clock.Set(
                Helpers.ParseInt(txtGameTimerMinutes_Reset.Text),
                Helpers.ParseInt(txtGameTimerSeconds_Reset.Text)
                );
        }

        private void btnGameTimerStart_Click(object sender, RoutedEventArgs e)
        {
            game.Clock.Start();
            game.AwayPowerPlayClock.Start();
            game.HomePowerPlayClock.Start();
            updateFields(game);
        }

        private void btnGameTimerStop_Click(object sender, RoutedEventArgs e)
        {
            game.Clock.Stop();
            game.AwayPowerPlayClock.Stop();
            game.HomePowerPlayClock.Stop();
            updateFields(game);
        }

        private void btnAwayTeamSave_Click(object sender, RoutedEventArgs e)
        {
            game.AwayTeam.Name = txtAwayTeamName.Text;
            game.Save();
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

        private void btnAwayTeamResetScore_Click(object sender, RoutedEventArgs e)
        {
            game.AwayTeam.Score = 0;
            updateFields(game);
        }

        private void btnHomeTeamResetScore_Click(object sender, RoutedEventArgs e)
        {
            game.HomeTeam.Score = 0;
            updateFields(game);
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

        private void btnHomeTeamMinus1_Click(object sender, RoutedEventArgs e)
        {
            game.HomeTeam.Score -= 1;
            updateFields(game);
        }

        private void BtnTimerCountsUp_OnClick(object sender, RoutedEventArgs e)
        {
            game.Clock.CountsDown = false;
            updateFields(game);
        }

        private void BtnTimerCountsDown_OnClick(object sender, RoutedEventArgs e)
        {
            game.Clock.CountsDown = true;
            updateFields(game);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            updateFields(game);
            uiRefreshTimer.Start();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            uiRefreshTimer.Stop();
            uiRefreshTimer.Enabled = false;
            uiRefreshTimer.Dispose();
        }

        private void BtnHomeTimerSet_OnClick(object sender, RoutedEventArgs e)
        {
            game.HomePowerPlayClock.Set(
                Helpers.ParseInt(txtHomeTimerMinutes_Reset.Text),
                Helpers.ParseInt(txtHomeTimerSeconds_Reset.Text)
                );
        }

        private void BtnAwayTimerSet_OnClick(object sender, RoutedEventArgs e)
        {
            game.AwayPowerPlayClock.Set(
                Helpers.ParseInt(txtAwayTimerMinutes_Reset.Text),
                Helpers.ParseInt(txtAwayTimerSeconds_Reset.Text)
                );
        }

        private void BtnHomeTimerCountsUp_OnClick(object sender, RoutedEventArgs e)
        {
            game.HomePowerPlayClock.CountsDown = false;
            updateFields(game);
        }

        private void BtnHomeTimerCountsDown_OnClick(object sender, RoutedEventArgs e)
        {
            game.HomePowerPlayClock.CountsDown = true;
            updateFields(game);
        }

        private void BtnAwayTimerCountsUp_OnClick(object sender, RoutedEventArgs e)
        {
            game.AwayPowerPlayClock.CountsDown = false;
            updateFields(game);
        }

        private void BtnAwayTimerCountsDown_OnClick(object sender, RoutedEventArgs e)
        {
            game.AwayPowerPlayClock.CountsDown = true;
            updateFields(game);
        }
    }
}
