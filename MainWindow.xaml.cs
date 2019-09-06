using System;
using System.Collections.Generic;
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
using ConsoleApp1;
using System.Collections.ObjectModel;

namespace WpfApp1
{
    public class TowerObject {

        public int Disk0 { get; set; }
        public int Disk1 { get; set; }
        public int Disk2 { get; set; }

    }

    public class TodoItem
    {
        public string Title { get; set; }
        public int Completion { get; set; }
    }


    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<Stack<int>> ObservableTowers;
        string towersCount = "";
        //Towers of Hanoi
        Hanoi HanoiObj;
        int turnsTaken = 0; //just used for display purposes; used to calculate turn max. please delete if the text display is gone
        int totalTowers; //just used for display purposes; used to calculate turn max. please delete if the text display is gone

        public MainWindow()
        {
            InitializeComponent();

            // You can set window properties in code
            this.Title = "Towers Of Hanoi Solver";
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            //Default Initialize the Hanoi Tower
            totalTowers = 3;
            HanoiObj = new Hanoi(3);
            Display_Towers_Status_Message(null, null);

            //Update visual display with our HanoiObj
            UpdateTowersWithHanoi();
        }

        //Function that will take the current HanoiObj
        //Then make the three Tower Display Objects reflect that
        public void UpdateTowersWithHanoi()
        {
            //Clear the Canvas of the old disks
            _canvas.Children.RemoveRange(3, _canvas.Children.Count - 3);

            //Get the stack
            Stack<int>[] towers = HanoiObj.Towers;

            //This height will be used for each of the three towers, reflecting the height of that specific tower
            //We need this to iterate through the array this number of times
            int thisTowerHeight = 0;
            
            //For each of our THREE towers
            for (int i = 0; i < 3; i++)
            {
                //Get the {i}th Tower to work with for this loop iteration
                int[] towersArray = towers[i].ToArray();
                Array.Reverse(towersArray);
                thisTowerHeight = towers[i].Count();

                //Just some values
                double diskHeight = 60;
                double currentTowerCenter = 200 + i * 400; //200 / 600 / 1000 are the center of the pillars
                double WidthMultiplier = 80; //if disk is size 3, width will be 80*3

                //now we have the tower Array-ified. We run through the array and generate disks as appropriate, then display the disks
                for (int j = 0; j < thisTowerHeight; j++)
                {
                    //first add the disk
                    int thisDiskContent = towersArray[j];
                    double width = WidthMultiplier*thisDiskContent;
                    var disc = new DiscControl
                    {
                        Text = (thisDiskContent).ToString(),
                        FontSize = 30,
                        Width = width,
                        Height = 30,
                        Foreground = Brushes.White
                    };
                    _canvas.Children.Add(disc);

                    //re-center the disk to the appropriate spot
                    Canvas.SetLeft(disc, currentTowerCenter - width / 2);
                    Canvas.SetBottom(disc, j*diskHeight); //each disk will be (disks on tower * diskHeight) above 0 to be appropriate
                }
            }
        }

        // When the button is clicked it closes the window
        private void ButtonNextMoveClicked(object sender, RoutedEventArgs e)
        {
            //if the game is not solved
            //then move the disk
            //then update the towers view
            if (!HanoiObj.IsGameSolved())
            {
                HanoiObj.MoveDisks();
                Display_Towers_Status_Message(sender, e);
                UpdateTowersWithHanoi();
            }
            //else, just say we already solved the game
            else MessageBox.Show("You have solved the Towers !");
        }

        // Event handling that posts the mouse x, y position in title
        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            Title = e.GetPosition(this).ToString();
        }

        //check if user entered a valid number; if so create fresh towers
        private void Send_Button_Click(object sender, RoutedEventArgs e)
        {
            towersCount = TowersCount.Text;

            byte num = 0;
            bool canConvert = byte.TryParse(towersCount, out num);
            if (canConvert == true)
            {
                MessageBox.Show($"Generating {num} towers");
                //Generate a new tower and display
                totalTowers = num;
                turnsTaken = 0;
                HanoiObj = new Hanoi(num);
                Display_Towers_Status_Message(sender, e);
                UpdateTowersWithHanoi();
            }
            else MessageBox.Show($"{towersCount} is not a valid number");

        }

        //This function is called at: Startup, Next Move, Reload Tower
        //It reads the current tower stacks and displays them onto the screen
        //Will update TowersSolution textbox
        private void Display_Towers_Status_Message(object sender, RoutedEventArgs e)
        {
            Stack<int>[] towers = HanoiObj.Towers;

            //for each of the three towers, make a string
            //then eventually, display the whole thing in a message box
            //for now; until we make a more visual setup
            
            //eventually this string logic will be converted to just update a more visual display directly
            String completeString = $"Turns Taken: {turnsTaken}/{Math.Pow (2, totalTowers)-1}\r\n";
            for (int i = 0; i < 3; i++)
            {
                completeString += $"Tower {i + 1} contains these disks: ";
                var myArray = towers[i].ToArray().Reverse();
                completeString += string.Join(" ", myArray);
                completeString += "\r\n";
            }
            turnsTaken++;

            //display the messagebox
            TowersSolution.Text = completeString;
        }

    }
}
