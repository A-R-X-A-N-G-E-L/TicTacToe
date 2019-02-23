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

namespace TicTacToe
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const int numberOfColumns = 3;
        private const int numberOfLines = 3;
        private const string pathToGameIco = @"art/game.ico";

        private int[,] fieldCurrentState;
        private SolidColorBrush defaultBrush;
        private bool gameFinished;
        private Logic logic;

        public MainWindow()
        {
            InitializeComponent();
            fieldCurrentState = new int[numberOfLines, numberOfColumns];
            defaultBrush = new SolidColorBrush(Colors.Transparent);
            gameFinished = false;
            this.Icon = BitmapFrame.Create(new Uri(pathToGameIco, UriKind.Relative));

            logic = new Logic();        
        }

        private void NewGame_Button_Click(object sender, RoutedEventArgs e)
        {
            logic.StartNewGame(fieldCurrentState, numberOfLines, numberOfColumns);

            gameFinished = false;
            Field1_Button.Background = defaultBrush;
            Field2_Button.Background = defaultBrush;
            Field3_Button.Background = defaultBrush;
            Field4_Button.Background = defaultBrush;
            Field5_Button.Background = defaultBrush;
            Field6_Button.Background = defaultBrush;
            Field7_Button.Background = defaultBrush;
            Field8_Button.Background = defaultBrush;
            Field9_Button.Background = defaultBrush;
        }

        private void Exit_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Checker(bool state)
        {
            if (state)
            {
                gameFinished = true;
            }
        }

        private void Field1_Button_Click(object sender, RoutedEventArgs e)
        {
            if (!gameFinished)
            {
                Checker(logic.MakeAMove(fieldCurrentState, logic.CurrentPlayer, 0, 0, (Button)sender));
            }
        }

        private void Field2_Button_Click(object sender, RoutedEventArgs e)
        {
            if (!gameFinished)
            {
                Checker(logic.MakeAMove(fieldCurrentState, logic.CurrentPlayer, 0, 1, (Button)sender));
            }
        }

        private void Field3_Button_Click(object sender, RoutedEventArgs e)
        {
            if (!gameFinished)
            {
                Checker(logic.MakeAMove(fieldCurrentState, logic.CurrentPlayer, 0, 2, (Button)sender));
            }
        }

        private void Field4_Button_Click(object sender, RoutedEventArgs e)
        {
            if (!gameFinished)
            {
                Checker(logic.MakeAMove(fieldCurrentState, logic.CurrentPlayer, 1, 0, (Button)sender));
            }
        }

        private void Field5_Button_Click(object sender, RoutedEventArgs e)
        {
            if (!gameFinished)
            {
                Checker(logic.MakeAMove(fieldCurrentState, logic.CurrentPlayer, 1, 1, (Button)sender));
            }
        }

        private void Field6_Button_Click(object sender, RoutedEventArgs e)
        {
            if (!gameFinished)
            {
                Checker(logic.MakeAMove(fieldCurrentState, logic.CurrentPlayer, 1, 2, (Button)sender));
            }
        }

        private void Field7_Button_Click(object sender, RoutedEventArgs e)
        {
            if (!gameFinished)
            {
                Checker(logic.MakeAMove(fieldCurrentState, logic.CurrentPlayer, 2, 0, (Button)sender));
            }
        }

        private void Field8_Button_Click(object sender, RoutedEventArgs e)
        {
            if (!gameFinished)
            {
                Checker(logic.MakeAMove(fieldCurrentState, logic.CurrentPlayer, 2, 1, (Button)sender));
            }
        }

        private void Field9_Button_Click(object sender, RoutedEventArgs e)
        {
            if (!gameFinished)
            {
                Checker(logic.MakeAMove(fieldCurrentState, logic.CurrentPlayer, 2, 2, (Button)sender));
            }
        }
    }

    public class Logic
    {
        private const int maxMoves = 9;
        private const string pathToCrossImage = @"art/cross.png";
        private const string pathToCircleImage = @"art/circle.png";
        private const string player1 = "Cross";
        private const string player2 = "Circle";

        private string currentPlayer;
        private int movesDone = 0;
        private ImageBrush crossBrush;
        private ImageBrush circleBrush;

        public Logic()
        {
            this.currentPlayer = player1;

            this.crossBrush = new ImageBrush();
            this.crossBrush.ImageSource = new BitmapImage(new Uri(pathToCrossImage, UriKind.Relative));

            this.circleBrush = new ImageBrush();
            this.circleBrush.ImageSource = new BitmapImage(new Uri(pathToCircleImage, UriKind.Relative));
        }

        public void StartNewGame(int[,] allFields, int numberOfLines, int numberOfColumns)
        {
            for (int i = 0; i < numberOfLines; i++)
            {
                for (int j = 0; j < numberOfColumns; j++)
                {
                    allFields[i, j] = 0;
                }
            }

            movesDone = 0;
            currentPlayer = player1;
        }

        public string CurrentPlayer
        {
            get
            {
                return currentPlayer;
            }
        }

        public bool MakeAMove(int[,] allFields, string currentPlayer, int fieldLine, int fieldColumn, Button fieldClicked)
        {
            if (allFields[fieldLine, fieldColumn] == 0)
            {
                if (currentPlayer == player1)
                {
                    allFields[fieldLine, fieldColumn] = 1;
                    fieldClicked.Background = crossBrush;
                    this.currentPlayer = player2;
                }
                else
                {
                    allFields[fieldLine, fieldColumn] = 2;
                    fieldClicked.Background = circleBrush;
                    this.currentPlayer = player1;
                }

                movesDone++;

                switch (MoveLeadsToResult(allFields, currentPlayer))
                {
                    case player1:
                        MessageBox.Show($"{player1} Is The Winner!");
                        return true;

                    case player2:
                        MessageBox.Show($"{player2} Is The Winner!");
                        return true;

                    case "Draw":
                        MessageBox.Show("Draw!");
                        return true;                       
                }
            }

            return false;
        }

        private string MoveLeadsToResult(int[,] allFields, string lastMoveWasMadeBy)
        {
            if ((allFields[0, 0] == allFields[0, 1] && allFields[0, 0] == allFields[0, 2] && allFields[0, 0] != 0) ||
                (allFields[1, 0] == allFields[1, 1] && allFields[1, 0] == allFields[1, 2] && allFields[1, 0] != 0) ||
                (allFields[2, 0] == allFields[2, 1] && allFields[2, 0] == allFields[2, 2] && allFields[2, 0] != 0) ||
                (allFields[0, 0] == allFields[1, 0] && allFields[0, 0] == allFields[2, 0] && allFields[0, 0] != 0) ||
                (allFields[0, 1] == allFields[1, 1] && allFields[0, 1] == allFields[2, 1] && allFields[0, 1] != 0) ||
                (allFields[0, 2] == allFields[1, 2] && allFields[0, 2] == allFields[2, 2] && allFields[0, 2] != 0) ||
                (allFields[0, 0] == allFields[1, 1] && allFields[0, 0] == allFields[2, 2] && allFields[0, 0] != 0) ||
                (allFields[2, 0] == allFields[1, 1] && allFields[2, 0] == allFields[0, 2] && allFields[2, 0] != 0))
            {
                if (lastMoveWasMadeBy == player1)
                {
                    return player1;
                }

                return player2;
            }
            else
            {
                if (movesDone == maxMoves)
                {
                    return "Draw";
                }
    
                return "Continue";
            }   
        }
    }
}
