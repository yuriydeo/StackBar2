using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using StackBarTest2;
using XQ.FloorStackLib.ViewModels;

namespace StackBarTest2
{
    public class ViewModel
    {
        public ViewModel()
        {
            PopulateData();
            FloorsViewCollection = new FloorStackBarData(Floors);
        }

        private void PopulateData()
        {
            _floors = new ObservableCollection<Floor>();
            for (int i = 1; i < 5; i++)
            {
                Floor floor = new Floor($"floor{i}");
                for (int j = 1; j < 8 - i; j++)
                {
                    Room room = new Room($"room{i}{j}", (i * 95.5 * j % 7) * 10.0, (UseType)((i + j) % 4));
                    floor.Rooms.Add(room);
                }
                _floors.Add(floor);
            }

            LegendDictionary = new Dictionary<string, Color>();
            LegendDictionary.Add(UseType.Vacant.ToString(), Colors.Red);
            LegendDictionary.Add(UseType.Office.ToString(), Colors.Blue);
            LegendDictionary.Add(UseType.Residential.ToString(), Colors.Green);
            LegendDictionary.Add(UseType.Storage.ToString(), Colors.Purple);

            

        }

        public Dictionary<string, Color> LegendDictionary { get; set; }

        private ObservableCollection<Floor> _floors;
        public ObservableCollection<Floor> Floors => _floors;

        public FloorStackBarData FloorsViewCollection { get; set; } 
    }

    public class RoomCellModel : StackBarCellViewModelBase<Room>
    {
        public RoomCellModel(Room room) : base(room)
        {
        }

        public override double Value => DataObject.Area;
    }

    public class FloorRowModel :StackBarRowDataModel<Floor>
    {
        public FloorRowModel(Floor floor) : base(floor)
        {
        }
        
        public override ObservableCollection<ICellViewModel> Cells
        {
            get
            {
                ObservableCollection<RoomCellModel> rooms = new ObservableCollection<RoomCellModel>();
                foreach (Room room in DataObject.Rooms)
                {
                    rooms.Add(new RoomCellModel(room));
                }
                return new ObservableCollection<ICellViewModel>(rooms);
            }
        }
    }

    public class FloorStackBarData : StackBarViewModelBase
    {
        public FloorStackBarData(ObservableCollection<Floor> floors) : base(ConstructCollection(floors))
        {
        }

        private static ObservableCollection<IRowViewModel> ConstructCollection(ObservableCollection<Floor> floors)
        {
            ObservableCollection<IRowViewModel> result = new ObservableCollection<IRowViewModel>();
            foreach (Floor floor in floors)
            {
                ObservableCollection<ICellViewModel> roomsCollection =  new ObservableCollection<ICellViewModel>();
                result.Add(new FloorRowModel(floor));
            }

            return result;
        }
    }
}
