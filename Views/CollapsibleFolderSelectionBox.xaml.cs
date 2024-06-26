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
        }
        private void RemoveFolderButton_Click(object sender, RoutedEventArgs e)
        {
            // Logic to add a new folder to the list
        }
    }

}
