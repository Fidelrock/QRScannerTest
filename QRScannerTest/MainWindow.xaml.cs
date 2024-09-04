using System.Windows;
using System.Windows.Controls;

namespace QRScannerTest
{
    public partial class MainWindow : Window
    {
        private System.Timers.Timer _inputTimer;
        private const int InputDelay = 500;

        public MainWindow()
        {
            InitializeComponent();
            HiddenScannerInput.Focus();

            // Initialize the timer in the constructor
            _inputTimer = new System.Timers.Timer(InputDelay);
            _inputTimer.Elapsed += onInputTimerElapsed;
            _inputTimer.AutoReset = false;  // Ensure the timer triggers only once per input sequence
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Focus the hidden input field when the window is loaded
            HiddenScannerInput.Focus();
        }

        private void onInputTimerElapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                string scannedData = HiddenScannerInput.Text;

                // Move data to Text0
                Text0.Text = scannedData;

                // Optionally process the data
                // ProcessScannedData(scannedData);

                // Clear the hidden input for the next scan
               // HiddenScannerInput.Clear();

                // Refocus on the hidden field
                HiddenScannerInput.Focus();
            });
        }

        private void HiddenScannerInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Reset the timer each time the text changes
            _inputTimer.Stop();
            _inputTimer.Start();
        }
    }
}
