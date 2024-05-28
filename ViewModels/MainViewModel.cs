using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DazFileManager.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private ViewModelBase _currentViewModel;
        public ViewModelBase CurrentViewModel
        {
            get => _currentViewModel;
            set => SetProperty(ref _currentViewModel, value);
        }

        public ICommand ShowHomeViewCommand { get; }
        public ICommand ShowSettingsViewCommand { get; }
        public ICommand ShowExtractViewCommand { get; }

        public MainViewModel()
        {
            ShowHomeViewCommand = new RelayCommand(ShowHomeView);
            ShowSettingsViewCommand = new RelayCommand(ShowSettingsView);
            ShowExtractViewCommand = new RelayCommand(ShowExtractView);
            // Default view
            ShowExtractView();
        }

        private void ShowHomeView()
        {
            CurrentViewModel = new HomeViewModel();
        }

        private void ShowSettingsView()
        {
            CurrentViewModel = new SettingsViewModel();
        }
        private void ShowExtractView()
        {
            CurrentViewModel = new ExtractViewModel();
        }
    }
}
