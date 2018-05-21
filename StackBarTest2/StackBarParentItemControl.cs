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

namespace StackBarTest2
{
    public class StackBarParentItemControl : ItemsControl
    {
        static StackBarParentItemControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(StackBarParentItemControl),
                new FrameworkPropertyMetadata(typeof(StackBarParentItemControl)));
        }

        public static DependencyProperty BarTextFieldProperty = DependencyProperty.Register("BarTextField",
            typeof(string), typeof(StackBarParentItemControl));

        public static DependencyProperty BarItemsSourceFieldProperty = DependencyProperty.Register("BarItemsSourceField",
            typeof(string), typeof(StackBarParentItemControl), new PropertyMetadata(null, BarItemsSourceFieldPropertyChangedCallback));

        private static void BarItemsSourceFieldPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var caller = (StackBarParentItemControl)d;
            caller.SetBinding(ItemsSourceProperty, new Binding(caller.BarItemsSourceField));
        }

        public string BarItemsSourceField
        {
            get => (string)GetValue(BarItemsSourceFieldProperty);
            set => SetValue(BarItemsSourceFieldProperty, value);
        }
    }
}
