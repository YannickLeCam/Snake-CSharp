using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace Snake
{
    public partial class MainWindow : Window
    {
        private DispatcherTimer _gameTimer;
        private Game _game;
        private char _lastKeyPressed = '♥';

        public MainWindow()
        {
            InitializeComponent();
            _game = new Game(38, 22);
            this.DataContext = _game;
            this.KeyDown += new KeyEventHandler(OnKeyDown);

            _gameTimer = new DispatcherTimer();
            _gameTimer.Interval = TimeSpan.FromSeconds(0.1); // Définir l'intervalle de mise à jour
            _gameTimer.Tick += GameTimer_Tick;
            _gameTimer.Start();
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            _game.UpdateGameState(_lastKeyPressed);
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Up:
                    _lastKeyPressed = 'U';
                    break;
                case Key.Down:
                    _lastKeyPressed = 'D';
                    break; 
                case Key.Left:
                    _lastKeyPressed = 'L';
                    break;
                case Key.Right:
                    _lastKeyPressed = 'R';
                    break;
            }
        }
    }
}
