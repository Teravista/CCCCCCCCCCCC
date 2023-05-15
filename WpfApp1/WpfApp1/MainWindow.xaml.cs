using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows;
using System.Windows.Forms;
using System.IO;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Reflection.Metadata;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        ContextMenu contextmenuFileC = null;
        ContextMenu contextmenuDirectoryC = null;
        FolderBrowserDialog dialogResultC = null;
        private  void DeleteDictionary(object sender, RoutedEventArgs e)
        {
            if (TreeView1.SelectedItem!=null) 
            {
                TreeViewItem clieckedNode = TreeView1.SelectedItem as TreeViewItem;
                String Path = clieckedNode.Tag.ToString();
                TreeViewItem parent = (TreeViewItem)clieckedNode.Parent;
                parent.Items.Remove(TreeView1.SelectedItem);
                Directory.Delete(Path,true); 
            }
        }
    private void DeleteFile(object sender, RoutedEventArgs e)
    {
            if (TreeView1.SelectedItem != null)
            {
                TreeViewItem clieckedNode = TreeView1.SelectedItem as TreeViewItem;
                String Path = clieckedNode.Tag.ToString();
                TreeViewItem parent = (TreeViewItem)clieckedNode.Parent;
                parent.Items.Remove(TreeView1.SelectedItem);
                File.Delete(Path);
            }
        }
    private  void CreateDictionary(object sender, RoutedEventArgs e)
        {
            if (TreeView1.SelectedItem != null)
            {
                WindowCHOSE w = new WindowCHOSE((TreeViewItem)TreeView1.SelectedItem,contextmenuFileC, contextmenuDirectoryC);
                w.ShowDialog();
                
            }
        }
        private  void OpenFile(object sender, RoutedEventArgs e)
        {
            if (TreeView1.SelectedItem != null)
            {
                TreeViewItem clieckedNode = TreeView1.SelectedItem as TreeViewItem;
                FileInfo Path = (FileInfo)clieckedNode.Tag;
                string[] lines = System.IO.File.ReadAllLines(Path.ToString());
                string output = "";
                foreach (var line in lines)
                {
                    output +=line+"\n";
                }
                TextBox1.Text = output;
            }
        }
        public MainWindow()
        {
            ContextMenu contextmenuDictionary = new ContextMenu();
            var create = new MenuItem();
            create.Header = "Create";
            create.Click += new RoutedEventHandler(CreateDictionary);
            var delete = new MenuItem();
            delete.Header = "Delete";
            delete.Click += new RoutedEventHandler(DeleteDictionary);

            contextmenuDictionary.Items.Add(create);
            contextmenuDictionary.Items.Add(delete);
            contextmenuDirectoryC = contextmenuDictionary;

            ContextMenu contextmenuFile = new ContextMenu();
            var Open = new MenuItem();
            Open.Header = "Open";
            Open.Click += new RoutedEventHandler(OpenFile);
            var delete2 = new MenuItem();
            delete2.Header = "Delete";
            delete2.Click += new RoutedEventHandler(DeleteFile);
            contextmenuFile.Items.Add(Open);
            contextmenuFile.Items.Add(delete2);
            contextmenuFileC = contextmenuFile;

            InitializeComponent();
        }
        String Rahs(FileAttributes att2)
        {
            String rahs2 = "";
            if ((att2 & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                rahs2 += "r";
            else
                rahs2 += "-";
            if ((att2 & FileAttributes.Archive) == FileAttributes.Archive)
                rahs2 += "a";
            else
                rahs2 += "-";

            if ((att2 & FileAttributes.Hidden) == FileAttributes.Hidden)
                rahs2 += "h";
            else
                rahs2 += "-";
            if ((att2 & FileAttributes.System) == FileAttributes.System)
                rahs2 += "s";
            else
                rahs2 += "-";
            return rahs2;
        }
        void SelectedItemTree(object sender, RoutedEventArgs e)
        {
            TreeViewItem clieckedNode = TreeView1.SelectedItem as TreeViewItem;
            String Path = clieckedNode.Tag.ToString();
            FileInfo info = new FileInfo(Path);
            FileAttributes att = info.Attributes;
            String rahs = Rahs(att);
            rahsText.Text = rahs;
        }
        void TreeViewCreator(FolderBrowserDialog dlg, ContextMenu contextmenuDictionary)
        {
            var root = new TreeViewItem
            {
                Header = new DirectoryInfo(dlg.SelectedPath).Name,
                Tag = dlg.SelectedPath,
                ContextMenu = contextmenuDictionary
            };
            root.Selected += new RoutedEventHandler(SelectedItemTree) ;
            TreeView1.Items.Add(root);
           
            DirectoryInfo info = new DirectoryInfo(dlg.SelectedPath);
            Wypisywacz(info, root);
        }
         void Wypisywacz(DirectoryInfo folder, TreeViewItem curRoot)
        {
            DirectoryInfo[] dir = folder.GetDirectories();
            FileInfo[] files = folder.GetFiles();

          
            
            foreach (DirectoryInfo di in dir)
            {
                
                ContextMenu contextmenuDictionary = new ContextMenu();
                var create = new MenuItem();
                create.Header = "Create";
                create.Click += new RoutedEventHandler(CreateDictionary);
                var delete = new MenuItem();
                delete.Header = "Delete";
                delete.Click += new RoutedEventHandler(DeleteDictionary);
                contextmenuDictionary.Items.Add(create);
                contextmenuDictionary.Items.Add(delete);
                var newFile = new TreeViewItem
                {
                    Header = di.Name,
                    Tag = di,
                    ContextMenu=contextmenuDictionary
                };
                curRoot.Items.Add(newFile);

                Wypisywacz(di, newFile);
            }
            foreach (FileInfo file in files)
            {
                
                var newFile = new TreeViewItem
                {
                    Header = file.Name,
                    Tag = file,
                    ContextMenu= contextmenuFileC
                };
                curRoot.Items.Add(newFile);



            }
        }
        private void Opener(object sender, RoutedEventArgs e)
        {
            dialogResultC = new FolderBrowserDialog() { Description = "Select directory to open" };
            DialogResult result= dialogResultC.ShowDialog();


            TreeViewCreator(dialogResultC, contextmenuDirectoryC);



        }
        private void Exiter(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void Abouter(object sender, RoutedEventArgs e)
        {
            string messageBoxText = "Author: Robert Chudy";
            string caption = "Apllication WPF File Explorer";
            MessageBoxButton button = MessageBoxButton.OK;
            MessageBoxImage icon = MessageBoxImage.Information;
            MessageBoxResult result;
            result = System.Windows.MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.Yes);

        }


    }
}
