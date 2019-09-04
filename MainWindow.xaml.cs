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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<Stack<int>> ObservableTowers;
        string towersCount = "";
        //Towers of Hanoi
        Hanoi HanoiObj;

        public MainWindow()
        {
            InitializeComponent();

            // You can set window properties in code
            this.Title = "Towers Of Hanoi Solver";
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            //Default Initialize the Hanoi Tower
            HanoiObj = new Hanoi(3);
            Display_Towers_Status_Message(null, null);
            
            //Hook up our HanoiObj Towers to an ObservableCollection to automatically update
            ObservableTowers = new ObservableCollection<Stack<int>>()
            {
                HanoiObj.Towers[0],
                HanoiObj.Towers[1],
                HanoiObj.Towers[2]
            };
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
                HanoiObj = new Hanoi(num);
                Display_Towers_Status_Message(sender, e);
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
            String completeString = "";
            for (int i = 0; i < 3; i++)
            {
                completeString += $"Tower {i + 1} contains these disks: ";
                var myArray = towers[i].ToArray().Reverse();
                completeString += string.Join(" ", myArray);
                completeString += "\r\n";
            }

            //display the messagebox
            TowersSolution.Text = completeString;
            //MessageBox.Show(completeString);
        }

    }
}
