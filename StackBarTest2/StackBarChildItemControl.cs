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

namespace StackBarTest2
{
    public class StackBarChildItemControl : ContentControl
    {
        static StackBarChildItemControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(StackBarChildItemControl), new FrameworkPropertyMetadata(typeof(StackBarChildItemControl)));
        }

        public static DependencyProperty ValueFieldNameProperty = DependencyProperty.Register("ValueFieldName", typeof(string), typeof(StackBarChildItemControl), new PropertyMetadata(null, ValueFieldNamePropertyChangedCallback));

        private static void ValueFieldNamePropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var caller = (StackBarChildItemControl) d;
            caller.SetBinding(UnitValueProperty, new Binding(caller.ValueFieldName));
        }

        public string ValueFieldName
        {
            get { return (string)GetValue(ValueFieldNameProperty); }
            set { SetValue(ValueFieldNameProperty, value); }
        }

        public static DependencyProperty UnitValueProperty = DependencyProperty.Register("UnitValue",
            typeof(double), typeof(StackBarChildItemControl));

        public double UnitValue
        {
            get { return (double)GetValue(UnitValueProperty); }
            set { SetValue(UnitValueProperty, value); }
        }
    }
}
