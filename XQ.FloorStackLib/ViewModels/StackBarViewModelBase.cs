using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace XQ.FloorStackLib.ViewModels
{
    public abstract class StackBarViewModelBase
    {
        public StackBarViewModelBase(ObservableCollection<IRowViewModel> collection)
        {
            RowsData = new ObservableCollection<IRowViewModel>(collection);
        }

        public ObservableCollection<IRowViewModel> RowsData { get; }

        public double MaxRowValue
        {
            get { return RowsData.Max(r => r.Cells.Sum(c => c.Value)); }
        }

        public double MinCellValue
        {
            get { return RowsData.Min(r => r.Cells.Min(c => c.Value)); }
        }
    }

    public abstract class StackBarRowDataModel<T> : IRowViewModel
    {
        public StackBarRowDataModel(T dataObject)
        {
            DataObject = dataObject;
        }
        public T DataObject { get; set; }
        public abstract ObservableCollection<ICellViewModel> Cells { get; }
    }

    public abstract class StackBarCellViewModelBase<T> : ICellViewModel
    {
        public StackBarCellViewModelBase(T dataObject)
        {
            DataObject = dataObject;
        }
        public T DataObject { get; set; }
        public abstract double Value { get; }
    }

    public interface ICellViewModel
    {
        double Value { get; }
    }

    public interface IRowViewModel
    {
        ObservableCollection<ICellViewModel> Cells { get; }
    }
}
