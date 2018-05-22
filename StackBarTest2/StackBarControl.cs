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
    public class StackBarControl : ItemsControl
    {
        static StackBarControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(StackBarControl), new FrameworkPropertyMetadata(typeof(StackBarControl)));
        }

        public static DependencyProperty CellValueFieldProperty = DependencyProperty.Register("CellValueField",
            typeof(string), typeof(StackBarControl));

        public string CellValueField
        {
            get { return (string)GetValue(CellValueFieldProperty); }
            set { SetValue(CellValueFieldProperty, value); }
        }

        public static readonly DependencyProperty CellTemplateProperty = DependencyProperty.Register(
            "CellTemplate", typeof(DataTemplate), typeof(StackBarControl), new PropertyMetadata(default(DataTemplate)));

        public DataTemplate CellTemplate
        {
            get { return (DataTemplate)GetValue(CellTemplateProperty); }
            set { SetValue(CellTemplateProperty, value); }
        }

        public static readonly DependencyProperty HeaderTemplateProperty = DependencyProperty.Register(
            "HeaderTemplate", typeof(DataTemplate), typeof(StackBarControl), new PropertyMetadata(default(DataTemplate)));

        public DataTemplate HeaderTemplate
        {
            get { return (DataTemplate)GetValue(HeaderTemplateProperty); }
            set { SetValue(HeaderTemplateProperty, value); }
        }

        public static DependencyProperty RowItemsSourceFieldProperty = DependencyProperty.Register("RowItemsSourceField",
            typeof(string), typeof(StackBarControl));

        public string RowItemsSourceField
        {
            get { return (string)GetValue(RowItemsSourceFieldProperty); }
            set { SetValue(RowItemsSourceFieldProperty, value); }
        }

        private static readonly DependencyPropertyKey ScalePropertyKey = DependencyProperty.RegisterReadOnly("Scale",
                typeof(double), typeof(StackBarControl), new PropertyMetadata());

        public static readonly DependencyProperty ScaleProperty =
            ScalePropertyKey.DependencyProperty;

        public double Scale
        {
            get { return (double)GetValue(ScaleProperty); }
            private set { SetValue(ScalePropertyKey, value); }
        }

        protected override void OnItemsSourceChanged(IEnumerable oldValue, IEnumerable newValue)
        {
            base.OnItemsSourceChanged(oldValue, newValue);
            SetScale();
        }

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);
            SetScale();
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            SetScale();
        }

        private void SetScale()
        {
            if (MinCellWidth > 0)
                SetScaleByCellValue();
            else
                SetScaleByWidth();
        }
        private void SetScaleByWidth()
        {
            double maxValue = 0;
            double barValue = 0;
            if (ItemsSource == null)
                return;
            foreach (object item in ItemsSource)
            {
                barValue = 0;
                foreach (object cell in (IEnumerable)item.GetType().GetProperty(RowItemsSourceField).GetValue(item))
                {
                    barValue += (double)cell.GetType().GetProperty(CellValueField).GetValue(cell);
                }
                if (barValue > maxValue)
                    maxValue = barValue;
            }
            
            Border border = this.GetVisualChild(0) as Border;
            ListBox header = border?.FindName("HeaderContainerListBox") as ListBox;
            double headerWidth = header?.ActualWidth ?? 0;
            Scale = (ActualWidth - headerWidth) / maxValue;
        }

        protected override Size MeasureOverride(Size constraint)
        {
            SetScale();
            return base.MeasureOverride(constraint);
        }

        private void SetScaleByCellValue()
        {
            double minValue = double.MaxValue;

            if (ItemsSource == null)
                return;
            foreach (object item in ItemsSource)
            {
                foreach (object cell in (IEnumerable)item.GetType().GetProperty(RowItemsSourceField).GetValue(item))
                {
                    double cellValue = (double)cell.GetType().GetProperty(CellValueField).GetValue(cell);
                    if (cellValue < minValue)
                        minValue = cellValue;
                }
            }

            Scale = MinCellWidth / minValue;
        }

        public static DependencyProperty MinCellWidthProperty = DependencyProperty.Register("MinCellWidth",
            typeof(double), typeof(StackBarControl), new PropertyMetadata(0.0, MinCellWidthPropertyChangedCallback));

        private static void MinCellWidthPropertyChangedCallback(DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            var caller = (StackBarControl)d;
            caller.SetScale();
        }

        public double MinCellWidth
        {
            get { return (double)GetValue(MinCellWidthProperty); }
            set { SetValue(MinCellWidthProperty, value); }
        }

        public static DependencyProperty RowHeightProperty = DependencyProperty.Register("RowHeight",
            typeof(double), typeof(StackBarControl));

        public double RowHeight
        {
            get { return (double)GetValue(RowHeightProperty); }
            set { SetValue(RowHeightProperty, value); }
        }
    }
}
