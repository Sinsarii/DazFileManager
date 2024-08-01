using System;
using System.Collections;
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

namespace DazFileManager.Views
{
    /// <summary>
    /// Interaction logic for CollapsibleFolderSelectionBox.xaml
    /// </summary>
    public partial class CollapsibleFolderSelectionBox : UserControl
    {
        //define dependancy property for collection
        public static DependencyProperty FoldersProperty = DependencyProperty.Register("Folders", typeof(IEnumerable), typeof(CollapsibleFolderSelectionBox));

        public static readonly DependencyProperty SelectedFolderProperty = DependencyProperty.Register("SelectedFolder", typeof(string), typeof(CollapsibleFolderSelectionBox), new PropertyMetadata(default(string)));

        //bind the collection, public
        public IEnumerable Folders
        {
            get { return (IEnumerable)GetValue(FoldersProperty); }
            set { SetValue(FoldersProperty, value); }
        }

        public CollapsibleFolderSelectionBox()
        {
            InitializeComponent();
        }

        private void ExpandButton_Click(object sender, RoutedEventArgs e)
        {
            if (FoldersListBox.Visibility == Visibility.Collapsed)
            {
                FoldersListBox.Visibility = Visibility.Visible;
                ExpandButton.Content = "▲";
            }
            else
            {
                FoldersListBox.Visibility = Visibility.Collapsed;
                ExpandButton.Content = "▼";
            }
        }

        private void AddFolderButton_Click(object sender, RoutedEventArgs e)
        {
            // Logic to add a new folder to the list
            if (Folders is IList foldersList && !string.IsNullOrEmpty(FolderComboBox.Text) && !foldersList.Contains(FolderComboBox.Text))
            {
                foldersList.Add(FolderComboBox.Text);
                FolderComboBox.Text = string.Empty;
            }
        }
        private void RemoveFolderButton_Click(object sender, RoutedEventArgs e)
        {
            // Logic to add a new folder to the list
            if (sender is Button button && button.DataContext is string folder && Folders is IList foldersList)
            {
                foldersList.Remove(folder);
            }
        }

        
    }

}
