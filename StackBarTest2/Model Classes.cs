using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using System.Windows.Media;

namespace StackBarTest2
{
    public class Floor
    {
        public string FullName
        {
            get { return this.ToString(); }
        }
        public Floor(string name)
        {
            Name = name;
            Rooms = new ObservableCollection<Room>();
        }
        public string Name { get; }
        public ObservableCollection<Room> Rooms { get; }
        public double Area
        {
            get { return Rooms.Sum(x => x.Area); }
        }

        //public double TrueArea
        //{
        //    get { return Rooms.Where(x => x.Type != UseType.Fake).Sum(x => x.Area); }
        //}
    }

    public enum UseType
    {
        Office = 0,
        Vacant = 1,
        Residential = 2,
        Storage = 3
    }

    public class Room
    {
        public Room(string name, double area, UseType type)
        {
            Name = name;
            Area = area;
            Type = type;
        }
        public string Name { get; }
        public double Area { get; }

        public UseType Type { get; }
    }
}
