using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Threading;
using StackBarControlLib.ViewModelInterfaces;


namespace StackBarControlLib
{
    /// <summary>
    /// ItemSource for this control must implement IStackBarRowModel
    /// </summary>
    public class StackBarControl : ItemsControl
    {
        #region Constructors
        static StackBarControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(StackBarControl), new FrameworkPropertyMetadata(typeof(StackBarControl)));
        }
        public StackBarControl()
        {
            resizeTimer = new DispatcherTimer();
            resizeTimer.Interval = TimeSpan.FromMilliseconds(30);
            resizeTimer.Tick += ResizeTimerFinished;
        }
        #endregion

        #region Fields
        private ScrollViewer _barScroll;
        private ScrollViewer _headerScroll;
        /// <summary>
        /// timer postphones scaling so resizing is already finished when it fires,
        /// and also aggregates multiple calls from MeasureOverride/RenderSizeChanged into single call
        /// </summary>
        private DispatcherTimer resizeTimer;
        #endregion

        #region DependancyProperties
        public static readonly DependencyProperty IsPreviewModeProperty = DependencyProperty.Register("IsPreviewMode", typeof(bool), typeof(StackBarControl), new PropertyMetadata(true));
        public static DependencyProperty CellTemplateProperty = DependencyProperty.Register("CellTemplate", typeof(DataTemplate), typeof(StackBarControl), new PropertyMetadata(default(DataTemplate)));
        public static DependencyProperty HeaderTemplateProperty = DependencyProperty.Register("HeaderTemplate", typeof(DataTemplate), typeof(StackBarControl), new PropertyMetadata(default(DataTemplate)));
        public static DependencyProperty PreviewBarTemplateProperty = DependencyProperty.Register("PreviewBarTemplate", typeof(ControlTemplate), typeof(StackBarControl), new PropertyMetadata(default(DataTemplate)));
        private static readonly DependencyPropertyKey ScalePropertyKey = DependencyProperty.RegisterReadOnly("Scale",typeof(double), typeof(StackBarControl), new PropertyMetadata());
        public static readonly DependencyProperty ScaleProperty = ScalePropertyKey.DependencyProperty;
        public static DependencyProperty MinCellWidthProperty = DependencyProperty.Register("MinCellWidth", typeof(double), typeof(StackBarControl), new PropertyMetadata(0.0, MinCellWidthPropertyChangedCallback));
        public static DependencyProperty RowHeightProperty = DependencyProperty.Register("RowHeight", typeof(double), typeof(StackBarControl));
        #endregion

        #region Properties
        public bool IsPreviewMode
        {
            get { return (bool) GetValue(IsPreviewModeProperty); }
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
        public ControlTemplate PreviewBarTemplate
        {
            get { return (ControlTemplate)GetValue(PreviewBarTemplateProperty); }
            set { SetValue(PreviewBarTemplateProperty, value); }
        }
        public double Scale
        {
            get { return (double)GetValue(ScaleProperty); }
            private set { SetValue(ScalePropertyKey, value); }
        }
        public double MinCellWidth
        {
            get { return (double)GetValue(MinCellWidthProperty); }
            set { SetValue(MinCellWidthProperty, value); }
        }
        public double RowHeight
        {
            get { return (double)GetValue(RowHeightProperty); }
            set { SetValue(RowHeightProperty, value); }
        }
        #endregion

        #region Methods
        private void BindScrollViewers()
        {
            if (this.VisualChildrenCount < 1)
                throw new Exception("StackBarControl didn't load properly");

            Border border = this.GetVisualChild(0) as Border;
            _headerScroll = border?.FindName("HeaderScrollViewer") as ScrollViewer;
            _barScroll = border?.FindName("BarScrollViewer") as ScrollViewer;

            _headerScroll.ScrollChanged += OnScrollChanged;
            _headerScroll.SizeChanged += OnHeaderSizeChanged;
            _barScroll.ScrollChanged += OnScrollChanged;
        }
        private void SetScale()
        {
            if (MinCellWidth <= 0 || IsPreviewMode)
                SetScaleByWidth();
            else
                SetScaleByCellValue();
        }

        /// <summary>
        /// timer postphones scaling so resizing is already finished when it fires,
        /// and also aggregates multiple calls from MeasureOverride/RenderSizeChanged into single call
        /// </summary>
        private void ResizeTimerFinished(object sender, EventArgs e)
        {
            ((DispatcherTimer)sender).Stop();
            SetScale();
        }
        private void SetScaleByWidth()
        {
            if (ItemsSource == null)
                return;

            ObservableCollection<IStackBarRowModel> rows = (ObservableCollection<IStackBarRowModel>)ItemsSource;

            double maxValue = rows.Max(r => r.Cells.Sum(c => c.Value));
            double barWidth = _barScroll?.ViewportWidth ?? 0;

            //Sometimes StackBar happen to not fit by width a little. I suspect a rounding error
            //Nudging measure a little seems to fix the problem
            double cellsDensity = barWidth / rows.Sum(r => r.Cells.Count);
            double multiplier = IsPreviewMode ? 1 : 1 - (0.05 / cellsDensity);

            Scale = (barWidth * multiplier) / maxValue;
        }
        private void SetScaleByCellValue()
        {
            if (ItemsSource == null)
                return;

            ObservableCollection<IStackBarRowModel> rows = (ObservableCollection<IStackBarRowModel>)ItemsSource;
            double minValue = rows.Min(r => r.Cells.Min(c => c.Value));

            Scale = MinCellWidth / minValue;
        }
        private static void MinCellWidthPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var caller = (StackBarControl)d;
            caller.SetScale();
        }
        protected override Size MeasureOverride(Size constraint)
        {
            resizeTimer.Stop();
            resizeTimer.Start();
            return base.MeasureOverride(constraint);
        }
        protected override void OnItemsSourceChanged(IEnumerable oldValue, IEnumerable newValue)
        {
            if (newValue == null)
                return;
            if (!(newValue is ObservableCollection<IStackBarRowModel>))
                throw new ArgumentException("StackBarControl expects ObservableCollection which implements IRowViewModel");

            ////unsubscribe from old collection events 
            //if (oldValue is INotifyCollectionChanged oldCollection1)
            //    oldCollection1.CollectionChanged -= OnItemSourceCollectionChanged;
            //if (oldValue is INotifyPropertyChanged oldCollection2)
            //    oldCollection2.PropertyChanged -= OnItemSourcePropertyChanged;

            ////subscribe to new collection events 
            //if (newValue is INotifyCollectionChanged newCollection1)
            //    newCollection1.CollectionChanged += OnItemSourceCollectionChanged;
            //if (newValue is INotifyPropertyChanged newCollection2)
            //    newCollection2.PropertyChanged += OnItemSourcePropertyChanged;

            base.OnItemsSourceChanged(oldValue, newValue);

            SetScale();
        }

        //private void OnItemSourcePropertyChanged(object sender, PropertyChangedEventArgs e)
        //{
        //    SetScale();
        //}
        //private void OnItemSourceCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        //{
        //    SetScale();
        //}

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);
            resizeTimer.Stop();
            resizeTimer.Start();
        }
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            BindScrollViewers();
            SetScale();
        }
        private void OnScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            _barScroll.ScrollToVerticalOffset(e.VerticalOffset);

            if (sender == _headerScroll)
            {
                _barScroll.ScrollToVerticalOffset(e.VerticalOffset);
            }
            else
            {
                _headerScroll.ScrollToVerticalOffset(e.VerticalOffset);
            }
        }

        private void OnHeaderSizeChanged(object sender, SizeChangedEventArgs e)
        {
            SetScale();
        }
        #endregion 
    }
}
