using BatchManager.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;

namespace BatchManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private List<BatchCommand> _batchCommands = new List<BatchCommand>();
        private BatchCommand _selectedBatchCommand;

        public event PropertyChangedEventHandler PropertyChanged;

        //public Dictionary<string, string> BatchCommands
        //{
        //    get { return batchCommands; }
        //    set
        //    {
        //        batchCommands = value;
        //        OnPropertyChanged("BatchCommands");
        //    }
        //}

        public List<BatchCommand> BatchCommands
        {
            get => _batchCommands;
            set => SetProperty(ref _batchCommands, value);
        }

        public BatchCommand SelectedBatchCommand
        {
            get => _selectedBatchCommand;
            set => SetProperty(ref _selectedBatchCommand, value);
        }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            //BatchCommands.Insert(0, new BatchCommand("--请选择命令--", "echo 请选择命令!"));
            //BatchCommands.Add(new BatchCommand("--请选择命令--", "echo 请选择命令!"));
            BatchCommands.Add(new BatchCommand("输出Hello, World!", "echo Hello, World!"));
            BatchCommands.Add(new BatchCommand("列出目录中的文件及子目录的名称", "dir"));
            BatchCommands.Add(new BatchCommand("显示IP配置值", "ipconfig"));
            BatchCommands.Add(new BatchCommand("文件下所有文件名保存txt", "DIR *.*  / B > FileNameList.txt"));
        }

        private async void myButton_Click(object sender, RoutedEventArgs e)
        {
            string commandText = myTextBox.Text;
            string output = await RunCommandAsync(commandText);
            MessageBox.Show(output);
        }

        private async void myButtontwo_ClickAsync(object sender, RoutedEventArgs e)
        {
            #region List结构

            if (SelectedBatchCommand != null)
            {
                string commandText = SelectedBatchCommand.Command;
                string output = await RunCommandAsync(commandText);
                MessageBox.Show(output);
            }

            #endregion List结构

            #region Dictionary结构

            //if (SelectedBatchCommand != null)
            //{
            //    string command = BatchCommands[SelectedBatchCommand.Name];
            //    Process.Start("cmd.exe", "/c " + command);
            //}

            #endregion Dictionary结构
        }

        protected void SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (!EqualityComparer<T>.Default.Equals(field, value))
            {
                field = value;
                OnPropertyChanged(propertyName);
            }
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="commandText"></param>
        /// <returns></returns>
        private async Task<string> RunCommandAsync(string commandText)
        {
            string output = string.Empty;
            try
            {
                Process process = new Process
                {
                    StartInfo =
                    {
                        FileName = "cmd.exe",
                        Arguments = "/c " + commandText,
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        CreateNoWindow = true
                    }
                };
                process.Start();
                var executeResult = await process.StandardOutput.ReadToEndAsync();
                output = string.IsNullOrEmpty(executeResult)
                    ? "执行成功"
                    : executeResult;
                await process.WaitForExitAsync();
            }
            catch (Exception ex)
            {
                output = ex.Message;
            }
            return output;
        }

        private void myButtonthree_ClickAsync(object sender, RoutedEventArgs e)
        {
            // 创建新的窗体对象
            var readFileAndRun = new ReadFileAndRun();

            // 调用 Show 方法以非模式窗口的方式显示新窗口
            readFileAndRun.Show();

            // 调用 ShowDialog 方法以模式窗口的方式显示新窗口
            //readFileAndRun.ShowDialog();
        }
    }
}