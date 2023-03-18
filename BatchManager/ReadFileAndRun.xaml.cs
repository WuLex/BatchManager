using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Input;
using Path = System.IO.Path;

namespace BatchManager
{
    /// <summary>
    /// ReadFileAndRun.xaml 的交互逻辑
    /// </summary>
    public partial class ReadFileAndRun : Window
    {
        private readonly ViewModel _viewModel;

        public ReadFileAndRun()
        {
            InitializeComponent();
            _viewModel = new ViewModel();
            DataContext = _viewModel;
        }

        public class ViewModel : ViewModelBase
        {
            private ObservableCollection<string> _batchFiles;
            private string _selectedBatchFile;
            private ICommand _runCommand;

            public ObservableCollection<string> BatchFiles
            {
                get { return _batchFiles; }
                set { SetProperty(ref _batchFiles, value); }
            }

            public string SelectedBatchFile
            {
                get { return _selectedBatchFile; }
                set { SetProperty(ref _selectedBatchFile, value); }
            }

            public ICommand RunCommand
            {
                get
                {
                    if (_runCommand == null)
                    {
                        _runCommand = new RelayCommand(param => ExecuteBatchFile(), param => CanExecuteBatchFile());
                    }
                    return _runCommand;
                }
            }

            public ViewModel()
            {
                BatchFiles = new ObservableCollection<string>();
                LoadBatchFiles();
            }

            private void LoadBatchFiles()
            {
                //// 获取项目目录的路径
                //string projectPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
                //// 获取所有批处理文件的路径
                //string[] batchFiles = Directory.GetFiles(projectPath, "*.bat");

                // 获取上一级目录的路径
                //string parentDirectoryPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
                // 获取上三级目录的路径
                string parentDirectoryPath = Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName).FullName;

                // 获取批处理文件所在的目录的路径
                string batchFilesDirectoryPath = Path.Combine(parentDirectoryPath, "batfiles");

                // 获取所有批处理文件的路径
                string[] batchFiles = Directory.GetFiles(batchFilesDirectoryPath, "*.bat");
              

                // 添加文件名到ObservableCollection中
                foreach (string batchFile in batchFiles)
                {
                    BatchFiles.Add(Path.GetFileName(batchFile));
                }
            }

            private bool CanExecuteBatchFile()
            {
                return !string.IsNullOrEmpty(SelectedBatchFile);
            }

            private void ExecuteBatchFile()
            {
                try
                {
                    //// 获取项目目录的路径
                    //string projectPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
                    // 获取上三级目录的路径
                    string parentDirectoryPath = Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName).FullName;

                    // 获取批处理文件所在的目录的路径
                    string batchFilesDirectoryPath = Path.Combine(parentDirectoryPath, "batfiles");
                    // 构建要执行的批处理文件的完整路径
                    string batchFilePath = Path.Combine(batchFilesDirectoryPath, SelectedBatchFile);

                    // 执行批处理命令
                    Process.Start(batchFilePath);

                    MessageBox.Show("批处理文件已成功执行！", "提示");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"执行批处理文件时发生错误：{ex.Message}", "错误");
                }
               
            }
        }

        public class RelayCommand : ICommand
        {
            private readonly Action<object> _execute;
            private readonly Predicate<object> _canExecute;

            public RelayCommand(Action<object> execute, Predicate<object> canExecute)
            {
                _execute = execute;
                _canExecute = canExecute;
            }

            public event EventHandler CanExecuteChanged
            {
                add { CommandManager.RequerySuggested += value; }
                remove { CommandManager.RequerySuggested -= value; }
            }

            public bool CanExecute(object parameter)
            {
                return _canExecute == null ? true : _canExecute(parameter);
            }

            public void Execute(object parameter)
            {
                _execute(parameter);
            }
        }

        public abstract class ViewModelBase : System.ComponentModel.INotifyPropertyChanged
        {
            public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

            protected void RaisePropertyChanged(string propertyName)
            {
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
                }
            }

            protected bool SetProperty<T>(ref T storage, T value, string propertyName = null)
            {
                if (object.Equals(storage, value)) return false;

                storage = value;
                RaisePropertyChanged(propertyName);

                return true;
            }
        }
    }
}