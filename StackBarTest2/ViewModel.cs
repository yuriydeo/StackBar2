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
            FloorsCollection = new ObservableCollection<StackBarRowModel>();
            foreach (Floor floor in Floors)
            {
                var roomscol = new ObservableCollection<StackBarCellModel>(floor.Rooms.Select(i=> new StackBarCellModel(i, f=>((Room)f).Area)));
                FloorsCollection.Add(new StackBarRowModel(floor, roomscol));
            }
            testFloor = new StackBarRowModel(_floors[0], new ObservableCollection<StackBarCellModel>(_floors[0].Rooms.Select(i => new StackBarCellModel(i, f => ((Room)f).Area))));
            testFloor2 = new StackBarRowModel(_floors[1], new ObservableCollection<StackBarCellModel>(_floors[1].Rooms.Select(i => new StackBarCellModel(i, f => ((Room)f).Area))));
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

        public StackBarRowModel testFloor { get; set; }
        public StackBarRowModel testFloor2  { get; set; }

        public ObservableCollection<StackBarRowModel> FloorsCollection { get; set; }
    }
}
