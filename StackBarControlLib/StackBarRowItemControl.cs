using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using StackBarControlLib.ViewModelInterfaces;

namespace StackBarControlLib
{
    public class StackBarRowItemControl : ItemsControl
    {
        static StackBarRowItemControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(StackBarRowItemControl),
                new FrameworkPropertyMetadata(typeof(StackBarRowItemControl)));
        }


        public static readonly DependencyProperty IsPreviewModeProperty = DependencyProperty.Register("IsPreviewMode", typeof(bool), typeof(StackBarRowItemControl), new PropertyMetadata(true, PreviewModeChangedCallback));

        private static void PreviewModeChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue == false)
            {
                StackBarRowItemControl row = (StackBarRowItemControl) d;
                row.SetScaleByCellValue();
            }
        }

        public static DependencyProperty CellTemplateProperty = DependencyProperty.Register("CellTemplate", typeof(DataTemplate), typeof(StackBarRowItemControl), new PropertyMetadata(default(DataTemplate)));
        public static DependencyProperty HeaderTemplateProperty = DependencyProperty.Register("HeaderTemplate", typeof(DataTemplate), typeof(StackBarRowItemControl), new PropertyMetadata(default(DataTemplate)));
        public static DependencyProperty PreviewBarTemplateProperty = DependencyProperty.Register("PreviewBarTemplate", typeof(DataTemplate), typeof(StackBarRowItemControl), new PropertyMetadata(default(DataTemplate)));
        private static readonly DependencyPropertyKey RowInnerScalePropertyKey = DependencyProperty.RegisterReadOnly("RowInnerScaleProperty", typeof(double), typeof(StackBarRowItemControl), new PropertyMetadata());
        public static readonly DependencyProperty RowInnerScaleProperty = RowInnerScalePropertyKey.DependencyProperty;
        public static DependencyProperty ScaleProperty = DependencyProperty.Register("Scale", typeof(double), typeof(StackBarRowItemControl), new PropertyMetadata(1.0));

        public double Scale
        {
            get { return (double)GetValue(ScaleProperty); }
            private set { SetValue(ScaleProperty, value); }
        }
        public bool IsPreviewMode
        {
            get { return (bool)GetValue(IsPreviewModeProperty); }
            set { SetValue(IsPreviewModeProperty, value); }
        }

        public DataTemplate CellTemplate
        {
            get { return (DataTemplate)GetValue(CellTemplateProperty); }
            set { SetValue(CellTemplateProperty, value); }
        }
        public DataTemplate HeaderTemplate
        {
            get { return (DataTemplate)GetValue(HeaderTemplateProperty); }
            set { SetValue(HeaderTemplateProperty, value); }
        }

        public DataTemplate PreviewBarTemplate
        {
            get { return (DataTemplate)GetValue(PreviewBarTemplateProperty); }
            set { SetValue(PreviewBarTemplateProperty, value); }
        }
        public double RowScale
        {
            get { return (double)GetValue(RowInnerScaleProperty); }
            private set { SetValue(RowInnerScalePropertyKey, value); }
        }

        private double MinCellWidth { get; set; } = 40;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            SetScaleByCellValue();
        }

        private void SetScaleByCellValue()
        {
            if (ItemsSource == null)
                return;

            ObservableCollection<StackBarCellModel> cells = (ObservableCollection<StackBarCellModel>)ItemsSource;
            double minValue = cells.Min(c => c.Value);

            RowScale = MinCellWidth / minValue;
        }
    }
}
