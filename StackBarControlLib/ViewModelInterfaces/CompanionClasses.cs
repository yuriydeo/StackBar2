using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackBarControlLib.ViewModelInterfaces
{
    public interface IStackBarCellModel
    {
        double Value { get; }
    }

    public interface IStackBarRowModel
    {
        ObservableCollection<IStackBarCellModel> Cells { get; }
    }

    public class StackBarDataObjectWrapper<T> : IStackBarCellModel
    {
        public StackBarDataObjectWrapper(T dataObject, Func<T, double> valueFunc)
        {
            DataObject = dataObject;
            _storedFunc = valueFunc;
        }

        public T DataObject { get; }
        private Func<T, double> _storedFunc;

        public double Value
        {
            get { return _storedFunc(DataObject); }
        }
    }
}
