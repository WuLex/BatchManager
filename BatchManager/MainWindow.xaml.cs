using BatchManager.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
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

namespace BatchManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private List<BatchCommand> batchCommands = new List<BatchCommand>();
        private BatchCommand selectedBatchCommand;

        public event PropertyChangedEventHandler PropertyChanged;

        public List<BatchCommand> BatchCommands
        {
            get { return batchCommands; }
            set
            {
                batchCommands = value;
                OnPropertyChanged("BatchCommands");
            }
        }

        public BatchCommand SelectedBatchCommand
        {
            get { return selectedBatchCommand; }
            set
            {
                selectedBatchCommand = value;
                OnPropertyChanged("SelectedBatchCommand");
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            BatchCommands.Add(new BatchCommand("Command1", "echo Hello, World!"));
            BatchCommands.Add(new BatchCommand("Command2", "dir"));
            BatchCommands.Add(new BatchCommand("Command3", "ipconfig"));
        }

        private void myButton_Click(object sender, RoutedEventArgs e)
        {
            string text = myTextBox.Text;
            Console.WriteLine("Button clicked! Text: " + text);
        }


        private void myButtontwo_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedBatchCommand != null)
            {
                Console.WriteLine("Executing batch command: " + SelectedBatchCommand.Name);
                Console.WriteLine(SelectedBatchCommand.Command);
            }
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
