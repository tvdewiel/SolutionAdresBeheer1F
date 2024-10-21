using Microsoft.Win32;
using StreetNamesBL.Exceptions;
using StreetNamesBL.Managers;
using StreetNamesDL;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StreetNamesUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private OpenFileDialog fileDialog = new OpenFileDialog();
        private OpenFolderDialog folderDialog = new OpenFolderDialog();
        private FileManager fileManager = new FileManager(new FileProcessor());
        public MainWindow()
        {
            InitializeComponent();
            fileDialog.DefaultExt = "*.zip";
            fileDialog.Filter = "Zip files (.zip)|*.zip";
            fileDialog.Multiselect = false;
            fileDialog.InitialDirectory = @"C:\tmp\PG1";
            folderDialog.InitialDirectory= @"C:\tmp\PG1\tmp";
        }

        private void SourceFileButton_Click(object sender, RoutedEventArgs e)
        {
            bool? result=fileDialog.ShowDialog();
            if (result == true && !string.IsNullOrWhiteSpace(fileDialog.FileName)) 
            {
                try
                {
                    SourceFileTextBox.Text = fileDialog.FileName;
                    List<string> fileNames = fileManager.GetFilesFromZip(fileDialog.FileName);
                    ZipListBox.ItemsSource = fileNames;
                    fileManager.CheckZipFile(fileDialog.FileName,fileNames);
                }
                catch(ZipFileManagerException ex)
                {
                    List<string> errors = new();
                    foreach (var key in ex.Data.Keys) errors.Add($"'{key}' - error : {ex.Data[key]}");
                    ZipListBox.ItemsSource = errors;
                    MessageBox.Show(ex.Message, "FileManager", MessageBoxButton.OK, MessageBoxImage.Error);
                    SourceFileTextBox.Text = null;
                }
                catch(FileManagerException ex)
                {
                    ZipListBox.ItemsSource = null;
                    MessageBox.Show(ex.Message,"FileManager",MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void DestinatonFolderButton_Click(object sender, RoutedEventArgs e)
        {
            bool? result=folderDialog.ShowDialog();
            if(result==true && !string.IsNullOrWhiteSpace(folderDialog.FolderName))
            {
                DestinationFolderTextBox.Text = folderDialog.FolderName;

            }
        }

        private void ExecuteButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}