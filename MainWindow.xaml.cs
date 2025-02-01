using System.Windows;
using System.Windows.Controls;
using System.IO;
using System.Text.RegularExpressions;
using AsarSharp;

namespace AdPatcher
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

        private string GetManual() {
            string path = Path.Text;
            if (!File.Exists(path))
                throw new FileNotFoundException("Could not find app.asar from provided path!");
            return path;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            bool flag = false;

            string path = "";
            string temp = "";
            string[] files;

            string contents = "";

            // find any owadview tags
            Regex regex = new Regex("<owadview.+?>");

            Output.Text = "...";

            try
            {
                path = GetManual();

                if (path.Length == 0)
                    throw new FileNotFoundException("Could not find app.asar!");

                temp = Directory.CreateTempSubdirectory("adpatcher").FullName;

                if (!File.Exists(path + ".backup"))
                    File.Copy(path, path + ".backup");

                AsarExtractor extractor = new AsarExtractor(path, temp);
                extractor.Extract();
                extractor.Dispose();
                
                files = Directory.GetFiles(temp, "*.js", SearchOption.AllDirectories);
                
                foreach (string file_path in files) {
                    contents = File.ReadAllText(file_path);
                    if (regex.IsMatch(contents)) {
                        flag = true;
                        contents = regex.Replace(contents, "");
                        File.WriteAllText(file_path, contents);
                    }
                }

                if (!flag)
                    throw new NotSupportedException("Could not patch! This could mean:\n1. app.asar was already patched.\n2. app.asar does not contain any owadview tags.");

                AsarArchiver archiver = new AsarArchiver(temp, path);
                archiver.Archive();
                archiver.Dispose();

                Output.Text = "Success!";
            }
            catch (Exception ex) {
                Output.Text = ex.Message;
            }

            try
            {
                if (temp.Length != 0)
                    Directory.Delete(temp, true);
            } 
            catch (Exception ex) 
            {
                Output.Text += "\nException while cleaning up: " + ex.Message;
            }

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Path.Text.Length == 0) Path.Text = "...";
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.FileName = "app";
            dialog.DefaultExt = ".asar";
            dialog.Filter = "Asar archives (.asar)|*.asar";

            bool? result = dialog.ShowDialog();

            if (result == true) {
                Path.Text = dialog.FileName;
            }
        }
    }
}