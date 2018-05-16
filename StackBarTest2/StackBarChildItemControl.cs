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
    ///     <MyNamespace:StackBarChildItemControl/>
    ///
    /// </summary>
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
