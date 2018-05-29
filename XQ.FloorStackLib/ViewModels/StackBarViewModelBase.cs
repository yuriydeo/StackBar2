using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace XQ.FloorStackLib.ViewModels
{
    public interface ICellViewModel
    {
        double Value { get; }
    }

    public interface IRowViewModel
    {
        ObservableCollection<ICellViewModel> Cells { get; }
    }
}
