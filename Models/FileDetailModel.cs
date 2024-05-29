using DazFileManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class FileDetailModel : ViewModelBase
{
    private bool isSelected;
    public bool IsSelected
    {
        get => isSelected;
        set=> SetProperty(ref isSelected, value);
    }
    public string Filename { get; set; }
    public long Filesize { get; set; }
    public DateTime DownloadDate { get; set; }

    public RelayCommand ToggleSelectionCommand { get; }

    public FileDetailModel()
    {
        ToggleSelectionCommand = new RelayCommand(ToggleSelection);
    }

    private void ToggleSelection()
    {
        IsSelected = !IsSelected;
    }
}
