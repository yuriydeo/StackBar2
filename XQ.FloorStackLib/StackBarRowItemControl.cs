using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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
using System.Windows.Shapes;

namespace XQ.FloorStackLib
{
    public class StackBarRowItemControl : ItemsControl
    {
        static StackBarRowItemControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(StackBarRowItemControl),
                new FrameworkPropertyMetadata(typeof(StackBarRowItemControl)));
        }

        //public static DependencyProperty RowItemsSourceFieldProperty = DependencyProperty.Register("RowItemsSourceField",
        //    typeof(string), typeof(StackBarRowItemControl), new PropertyMetadata(null, BarItemsSourceFieldPropertyChangedCallback));

        //private static void BarItemsSourceFieldPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        //{
        //    var caller = (StackBarRowItemControl)d;
        //    caller.SetBinding(ItemsSourceProperty, new Binding(caller.RowItemsSourceField));
        //}

        //public string RowItemsSourceField
        //{
        //    get => (string)GetValue(RowItemsSourceFieldProperty);
        //    set => SetValue(RowItemsSourceFieldProperty, value);
        //}
    }
}
