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
    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:StackBarTest2"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:StackBarTest2;assembly=StackBarTest2"
    ///
    /// You will also need to add a project reference from the project where the XAML file lives
    /// to this project and Rebuild to avoid compilation errors:
    ///
    ///     Right click on the target project in the Solution Explorer and
    ///     "Add Reference"->"Projects"->[Browse to and select this project]
    ///
    ///
    /// Step 2)
    /// Go ahead and use your control in the XAML file.
    ///
    ///     <MyNamespace:StackBarParentItemControl/>
    ///
    /// </summary>
    public class StackBarParentItemControl : ItemsControl
    {
        static StackBarParentItemControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(StackBarParentItemControl),
                new FrameworkPropertyMetadata(typeof(StackBarParentItemControl)));
        }

      
        public static DependencyProperty BarValueFieldProperty = DependencyProperty.Register("BarValueField", typeof(string), typeof(StackBarParentItemControl), new PropertyMetadata(null, BarValueFieldPropertyChangedCallback));

        private static void BarValueFieldPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var caller = (StackBarParentItemControl)d;
            caller.SetBinding(BarValueProperty, new Binding(caller.BarValueField));
        }

        public string BarValueField
        {
            get { return (string)GetValue(BarValueFieldProperty); }
            set { SetValue(BarValueFieldProperty, value); }
        }

        public static DependencyProperty BarTextFieldProperty = DependencyProperty.Register("BarTextField",
            typeof(string), typeof(StackBarParentItemControl));

        public string BarTextField
        {
            get { return (string)GetValue(BarTextFieldProperty); }
            set { SetValue(BarTextFieldProperty, value); }
        }

        public static DependencyProperty BarItemsSourceFieldProperty = DependencyProperty.Register("BarItemsSourceField",
            typeof(string), typeof(StackBarParentItemControl), new PropertyMetadata(null, BarItemsSourceFieldPropertyChangedCallback));

        private static void BarItemsSourceFieldPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var caller = (StackBarParentItemControl)d;
            caller.SetBinding(ItemsSourceProperty, new Binding(caller.BarItemsSourceField));
        }

        public string BarItemsSourceField
        {
            get { return (string)GetValue(BarItemsSourceFieldProperty); }
            set { SetValue(BarItemsSourceFieldProperty, value); }
        }
        
        public static DependencyProperty BarValueProperty = DependencyProperty.Register("BarValue",
            typeof(double), typeof(StackBarParentItemControl));

        public double BarValue
        {
            get { return (double)GetValue(BarValueProperty); }
            set { SetValue(BarValueProperty, value); }
        }

        public static DependencyProperty HeaderWidthProperty = DependencyProperty.Register("HeaderWidth",
            typeof(double), typeof(StackBarParentItemControl));

        public double HeaderWidth
        {
            get { return (double)GetValue(HeaderWidthProperty); }
            set
            {
                SetValue(HeaderWidthProperty, value);
                FillWidth = BarParentWidth - HeaderWidth;
            }
        }

        public static DependencyProperty BarParentWidthProperty = DependencyProperty.Register("BarParentWidth",
            typeof(double), typeof(StackBarParentItemControl));

        public double BarParentWidth
        {
            get { return (double)GetValue(BarParentWidthProperty); }
            set
            {
                SetValue(BarParentWidthProperty, value);
                FillWidth = BarParentWidth - HeaderWidth;
            }
        }

        public static DependencyProperty FillWidthProperty = DependencyProperty.Register("FillWidth",
            typeof(double), typeof(StackBarParentItemControl));

        public double FillWidth
        {
            get { return (double)GetValue(FillWidthProperty); }
            set { SetValue(FillWidthProperty, value); }
        }
    }
}
