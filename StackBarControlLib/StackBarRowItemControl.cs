using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace StackBarControlLib
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
