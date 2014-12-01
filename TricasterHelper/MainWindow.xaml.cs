using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace TricasterHelper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void DisplayError(string errorMessage)
        {
            MessageBox.Show(errorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        
        private void btnSoccer_Click(object sender, RoutedEventArgs e)
        {
            // Set up a file to save with
            SaveFileDialog saveDialog = new SaveFileDialog
            {
                Filter = "Tricaster Variable Files|*.txt",
                Title = "Create a file"
            };
            saveDialog.ShowDialog();
            
            if (saveDialog.FileName != "")
            {
                string fileName = saveDialog.FileName;
                
                // Try to open the file - if we are able to open the file, continue
                try
                {
                    // Open the score window
                    SoccerScore newWindow = new SoccerScore(fileName);
                    newWindow.Show();
                }
                catch (Exception ex)
                {
                    DisplayError("Unable to write to file: " + ex.Message);
                }
            }

            //this.Close();
        }
        
        private void BtnHockey_OnClick(object sender, RoutedEventArgs e)
        {
            // Set up a file to save with
            SaveFileDialog saveDialog = new SaveFileDialog
            {
                Filter = "Tricaster Variable Files|*.txt",
                Title = "Create a file"
            };
            saveDialog.ShowDialog();

            if (saveDialog.FileName != "")
            {
                string fileName = saveDialog.FileName;

                // Try to open the file - if we are able to open the file, continue
                try
                {
                    // Open the score window
                    HockeyScore newWindow = new HockeyScore(fileName);
                    newWindow.Show();
                }
                catch (Exception ex)
                {
                    DisplayError("Unable to write to file: " + ex.Message);
                }
            }
        }
    }
}
