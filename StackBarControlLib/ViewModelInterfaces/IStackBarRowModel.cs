using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackBarControlLib.ViewModelInterfaces
{
    public interface IStackBarRowModel
    {
        ObservableCollection<IStackBarCellModel> Cells { get; }
    }
}
