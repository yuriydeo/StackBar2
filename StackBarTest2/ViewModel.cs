using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace StackBarTest2
{
    public class ViewModel
    {
        public ViewModel()
        {
            PopulateData();
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
        public ObservableCollection<Floor> Floors { get { return _floors; } }

        public double FloorMaxValue
        {
            get { return _floors.Max(x => x.Area); }
        }

        public Floor TestFloor
        {
            get { return _floors[0]; }
        }
    }
}
