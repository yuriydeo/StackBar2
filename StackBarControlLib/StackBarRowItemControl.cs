using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
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

        private static void PreviewModeChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            StackBarRowItemControl row = (StackBarRowItemControl)d;
            row.SetVisibility();
            //if ((bool)e.NewValue == false)
            //{
            //    row.SetScaleByCellValue();
            //}
        }

        public static DependencyProperty IsPreviewModeProperty = DependencyProperty.Register("IsPreviewMode", typeof(bool), typeof(StackBarRowItemControl), new PropertyMetadata(true, PreviewModeChangedCallback));
        public static DependencyProperty CellTemplateProperty = DependencyProperty.Register("CellTemplate", typeof(DataTemplate), typeof(StackBarRowItemControl), new PropertyMetadata(default(DataTemplate)));
        public static DependencyProperty HeaderTemplateProperty = DependencyProperty.Register("HeaderTemplate", typeof(DataTemplate), typeof(StackBarRowItemControl), new PropertyMetadata(default(DataTemplate)));
        public static DependencyProperty PreviewBarTemplateProperty = DependencyProperty.Register("PreviewBarTemplate", typeof(DataTemplate), typeof(StackBarRowItemControl), new PropertyMetadata(default(DataTemplate)));
        private static readonly DependencyPropertyKey RowInnerScalePropertyKey = DependencyProperty.RegisterReadOnly("RowScale", typeof(double), typeof(StackBarRowItemControl), new PropertyMetadata());
        public static readonly DependencyProperty RowInnerScaleProperty = RowInnerScalePropertyKey.DependencyProperty;
        public static DependencyProperty ScaleProperty = DependencyProperty.Register("Scale", typeof(double), typeof(StackBarRowItemControl), new PropertyMetadata(1.0));
        public static DependencyProperty PreviewVisibilityProperty = DependencyProperty.Register("PreviewVisibility", typeof(Visibility), typeof(StackBarRowItemControl), new PropertyMetadata(Visibility.Visible));
        public static DependencyProperty DetailedVisibilityProperty = DependencyProperty.Register("DetailedVisibility", typeof(Visibility), typeof(StackBarRowItemControl), new PropertyMetadata(Visibility.Collapsed));

        public double Scale
        {
            get { return (double)GetValue(ScaleProperty); }
            set { SetValue(ScaleProperty, value); }
        }
        public bool IsPreviewMode
        {
            get { return (bool)GetValue(IsPreviewModeProperty); }
            set { SetValue(IsPreviewModeProperty, value); }
        }

        public Visibility PreviewVisibility
        {
            get { return (Visibility)GetValue(PreviewVisibilityProperty); }
            set { SetValue(PreviewVisibilityProperty, value); }
        }

        public Visibility DetailedVisibility
        {
            get { return (Visibility)GetValue(DetailedVisibilityProperty); }
            set { SetValue(DetailedVisibilityProperty, value); }
        }

        protected override void OnMouseDoubleClick(MouseButtonEventArgs e)
        {
            //base.OnMouseDoubleClick(e);
            IsPreviewMode = !IsPreviewMode;
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
            //SetScaleByCellValue();
            SetVisibility();
        }

        private void SetVisibility()
        {
            if (IsPreviewMode)
            {
                PreviewVisibility = Visibility.Visible;
                DetailedVisibility = Visibility.Collapsed;
            }
            else
            {
                PreviewVisibility = Visibility.Collapsed;
                DetailedVisibility = Visibility.Visible;
            }
        }

        //private void SetScaleByCellValue()
        //{
        //    if (ItemsSource == null)
        //        return;

        //    ObservableCollection<StackBarCellModel> cells = (ObservableCollection<StackBarCellModel>)ItemsSource;
        //    double minValue = cells.Min(c => c.Value);

        //    RowScale = MinCellWidth / minValue;
        //}
    }
}
