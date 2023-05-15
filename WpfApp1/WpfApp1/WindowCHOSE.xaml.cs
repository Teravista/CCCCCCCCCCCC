

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class WindowCHOSE : System.Windows.Window
    {
        TreeViewItem clieckedNodeLocal = new TreeViewItem();
        ContextMenu contextmenuFileC = null;
        ContextMenu contextmenuDirectoryC = null;
        public WindowCHOSE(TreeViewItem clieckedNode, ContextMenu contextmenuFile,
        ContextMenu contextmenuDirectory)
        {
            clieckedNodeLocal = clieckedNode;
            contextmenuFileC = contextmenuFile;
            contextmenuDirectoryC = contextmenuDirectory;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            String fileName = NewFileName.Text;
            string pattern = @"^\A[a-zA-Z0-9_~-]{1,8}(.txt|.php|.html)$";
            Match match = Regex.Match(fileName, pattern, RegexOptions.IgnoreCase);
            if (match.Success|| RadioD.IsChecked == true)
            {
                String Path = clieckedNodeLocal.Tag.ToString();
                Path += "\\" + fileName;
                if (RadioF.IsChecked == true)
                {
                    File.Create(Path);
                    FileInfo info = new FileInfo(Path);
                    FileAttributes att = info.Attributes;
                    att = att & ~FileAttributes.Hidden;
                    att = att & ~FileAttributes.ReadOnly;
                    att = att & ~FileAttributes.Archive;
                    att = att & ~FileAttributes.System;
                    File.SetAttributes(Path, att);
                    if (Readonly.IsChecked == true)
                    {
                        File.SetAttributes(Path, File.GetAttributes(Path) | FileAttributes.ReadOnly);
                    }
                    if (Hidden.IsChecked == true)
                    {
                        File.SetAttributes(Path, File.GetAttributes(Path) | FileAttributes.Hidden);
                    }
                    if (Archive.IsChecked == true)
                    {
                        File.SetAttributes(Path, File.GetAttributes(Path) | FileAttributes.Archive);
                    }
                    if (SystemCheckBox.IsChecked == true)
                    {
                        File.SetAttributes(Path, File.GetAttributes(Path) | FileAttributes.System);
                    }

                    var newFile = new TreeViewItem
                    {
                        Header = fileName,
                        Tag = Path,
                        ContextMenu = contextmenuFileC
                    };
                    clieckedNodeLocal.Items.Add(newFile);
                    this.Close();
                }
                else if (RadioD.IsChecked == true)
                {
                    Directory.CreateDirectory(Path);
                    var newFile = new TreeViewItem
                    {
                        Header = fileName,
                        Tag = Path,
                        ContextMenu = contextmenuDirectoryC
                    };
                    clieckedNodeLocal.Items.Add(newFile);
                    this.Close();

                }
            }
            else
            {
                string messageBoxText = "Wrong Name for File";
                string caption = "!!!!!!!!!";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Warning;
                MessageBoxResult result;
                result = MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.Yes);
            }

        }
    }
}
