using DazFileManager.Infrastructure;
using DazFileManager.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
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
    /// Interaction logic for ExtractView.xaml
    /// </summary>
    public partial class ExtractView : UserControl
    {
        public ExtractView()
        {
            InitializeComponent();

            var viewModel = DIContainer.ServiceProvider.GetRequiredService<ExtractViewModel>();
            DataContext = viewModel;
        }
    }
}
