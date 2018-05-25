using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace XQ.FloorStackLib
{
    public class StackBarCellItemControl : ContentControl
    {
        static StackBarCellItemControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(StackBarCellItemControl), new FrameworkPropertyMetadata(typeof(StackBarCellItemControl)));
        }

        public static DependencyProperty ValueFieldNameProperty = DependencyProperty.Register("ValueFieldName", typeof(string), typeof(StackBarCellItemControl), 
            new PropertyMetadata(null, ValueFieldNamePropertyChangedCallback));

        private static void ValueFieldNamePropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var caller = (StackBarCellItemControl) d;
            caller.SetBinding(CellValueProperty, new Binding(caller.ValueFieldName));
        }

        public string ValueFieldName
        {
            get { return (string)GetValue(ValueFieldNameProperty); }
            set { SetValue(ValueFieldNameProperty, value); }
        }

        public static DependencyProperty CellValueProperty = DependencyProperty.Register("CellValue",
            typeof(double), typeof(StackBarCellItemControl));

        public double CellValue
        {
            get { return (double)GetValue(CellValueProperty); }
            set { SetValue(CellValueProperty, value); }
        }
    }
}
