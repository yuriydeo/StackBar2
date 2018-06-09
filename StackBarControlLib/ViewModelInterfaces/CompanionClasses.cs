using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackBarControlLib.ViewModelInterfaces
{
    public class StackBarRowModel
    {
        public StackBarRowModel(object dataObject, ObservableCollection<StackBarCellModel> col)
        {
            Cells = col;
            DataObject = dataObject;
        }
        public object DataObject { get; }
        public virtual ObservableCollection<StackBarCellModel> Cells { get; }
        public double Value
        {
            get { return Cells.Sum(c => c.Value); }
        }
    }

    public class StackBarCellModel 
    {
        public StackBarCellModel(object dataObject, Func<object, double> valueFunc)
        {
            DataObject = dataObject;
            _storedFunc = valueFunc;
        }

        public object DataObject { get; }
        private Func<object, double> _storedFunc;

        public double Value
        {
            get { return _storedFunc(DataObject); }
        }
    }
}
