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

        public static DependencyProperty LegendProperty = DependencyProperty.Register("Legend",
            typeof(Dictionary<string, Color>), typeof(StackBarParentItemControl));

        public Dictionary<string, Color> Legend
        {
            get { return (Dictionary<string, Color>) GetValue(LegendProperty); }
            set { SetValue(LegendProperty, value); }
        }

        public static DependencyProperty UnitValueFieldProperty = DependencyProperty.Register("UnitValueField",
            typeof(string), typeof(StackBarParentItemControl));

        public string UnitValueField
        {
            get { return (string) GetValue(UnitValueFieldProperty); }
            set { SetValue(UnitValueFieldProperty, value); }
        }

        public Binding UnitValueBinding
        {
            get
            {
                if (String.IsNullOrEmpty(UnitValueField))
                    throw new Exception("Unit value binding failed: field name is not set");
                var binding = new Binding("/" + UnitValueField);
                SetBinding(WidthProperty, binding);
                return binding;
            }

        }

        //public Binding UnitLabelBinding
        //{
        //    get
        //    {
        //        if (String.IsNullOrEmpty(UnitLabelField))
        //            throw new Exception("Unit label binding failed: field name is not set");
        //        //return new Binding("/" + UnitLabelField);
        //        return new Binding("Name");
        //    }
        //}

        public static readonly DependencyProperty UnitTemplateProperty = DependencyProperty.Register(
            "UnitTemplate", typeof(DataTemplate), typeof(StackBarParentItemControl), new PropertyMetadata(default(DataTemplate)));

        public DataTemplate UnitTemplate
        {
            get { return (DataTemplate)GetValue(UnitTemplateProperty); }
            set { SetValue(UnitTemplateProperty, value); }
        }
    }
}
