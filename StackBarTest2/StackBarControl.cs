using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    ///     <MyNamespace:StackBarControl/>
    ///
    /// </summary>
    public class StackBarControl : ItemsControl
    {
        static StackBarControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(StackBarControl), new FrameworkPropertyMetadata(typeof(StackBarControl)));
        }

        public static DependencyProperty MaxValueProperty = DependencyProperty.Register("MaxValue",
            typeof(double), typeof(StackBarControl));

        public double MaxValue
        {
            get { return (double)GetValue(MaxValueProperty); }
            set { SetValue(MaxValueProperty, value); }
        }

        public static DependencyProperty UnitValueFieldProperty = DependencyProperty.Register("UnitValueField",
            typeof(string), typeof(StackBarControl));

        public string UnitValueField
        {
            get { return (string)GetValue(UnitValueFieldProperty); }
            set { SetValue(UnitValueFieldProperty, value); }
        }

        public static readonly DependencyProperty UnitTemplateProperty = DependencyProperty.Register(
            "UnitTemplate", typeof(DataTemplate), typeof(StackBarControl), new PropertyMetadata(default(DataTemplate)));

        public DataTemplate UnitTemplate
        {
            get { return (DataTemplate)GetValue(UnitTemplateProperty); }
            set { SetValue(UnitTemplateProperty, value); }
        }

        public static readonly DependencyProperty HeaderTemplateProperty = DependencyProperty.Register(
            "HeaderTemplate", typeof(DataTemplate), typeof(StackBarControl), new PropertyMetadata(default(DataTemplate)));

        public DataTemplate HeaderTemplate
        {
            get { return (DataTemplate)GetValue(HeaderTemplateProperty); }
            set { SetValue(HeaderTemplateProperty, value); }
        }

        public static DependencyProperty BarValueFieldProperty = DependencyProperty.Register("BarValueField", typeof(string), typeof(StackBarControl));

        public string BarValueField
        {
            get { return (string)GetValue(BarValueFieldProperty); }
            set { SetValue(BarValueFieldProperty, value); }
        }

        public static DependencyProperty BarItemsSourceFieldProperty = DependencyProperty.Register("BarItemsSourceField",
            typeof(string), typeof(StackBarControl));

        public string BarItemsSourceField
        {
            get { return (string)GetValue(BarItemsSourceFieldProperty); }
            set { SetValue(BarItemsSourceFieldProperty, value); }
        }
    }
}
