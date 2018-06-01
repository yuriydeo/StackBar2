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
using StackBarControlLib.ViewModelInterfaces;

namespace StackBarTest2
{
    public class ViewModel
    {
        public ViewModel()
        {
            PopulateData();
            FloorsCollection = new ObservableCollection<IStackBarRowModel>();
            foreach (Floor floor in Floors)
            {
                FloorsCollection.Add(new FloorRowModel(floor));
            }
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

       
        public ObservableCollection<IStackBarRowModel> FloorsCollection { get; set; }
    }

    public class RoomCellModel : IStackBarCellModel
    {
        public RoomCellModel(Room room)
        {
            DataObject = room;
        }

        public Room DataObject { get; }

        public double Value => DataObject.Area;
    }

    public class FloorRowModel : IStackBarRowModel
    {
        public FloorRowModel(Floor floor)
        {
            DataObject = floor;
        }

        public Floor DataObject { get; }

        public ObservableCollection<IStackBarCellModel> Cells
        {
            get
            {
                ObservableCollection<RoomCellModel> rooms = new ObservableCollection<RoomCellModel>();
                foreach (Room room in DataObject.Rooms)
                {
                    rooms.Add(new RoomCellModel(room));
                }
                return new ObservableCollection<IStackBarCellModel>(rooms);
            }
        }
    }
}
