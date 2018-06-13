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
            _resizeTimer.Tick += ResizeTimerFinished;
        }
        #endregion

        #region Fields
        private DispatcherTimer _resizeTimer = new DispatcherTimer() { Interval = TimeSpan.FromMilliseconds(20) };
        #endregion

        #region DependancyProperties
        public static DependencyProperty CellTemplateProperty = DependencyProperty.Register("CellTemplate", typeof(DataTemplate), typeof(StackBarControl), new PropertyMetadata(default(DataTemplate)));
        public static DependencyProperty HeaderTemplateProperty = DependencyProperty.Register("HeaderTemplate", typeof(DataTemplate), typeof(StackBarControl), new PropertyMetadata(default(DataTemplate)));
        public static DependencyProperty PreviewBarTemplateProperty = DependencyProperty.Register("PreviewBarTemplate", typeof(DataTemplate), typeof(StackBarControl), new PropertyMetadata(default(DataTemplate)));
        private static readonly DependencyPropertyKey ScalePropertyKey = DependencyProperty.RegisterReadOnly("GlobalScale",typeof(double), typeof(StackBarControl), new PropertyMetadata(1.0));
        public static readonly DependencyProperty GlobalScaleProperty = ScalePropertyKey.DependencyProperty;
        public static DependencyProperty RowHeightProperty = DependencyProperty.Register("RowHeight", typeof(double), typeof(StackBarControl), new PropertyMetadata(100.0));
        #endregion

        #region Properties
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
        public double GlobalScale
        {
            get { return (double)GetValue(GlobalScaleProperty); }
            private set { SetValue(ScalePropertyKey, value); }
        }
        public double RowHeight
        {
            get { return (double)GetValue(RowHeightProperty); }
            set { SetValue(RowHeightProperty, value); }
        }
        #endregion

        #region Methods
        //private void BindScrollViewers()
        //{
        //    if (this.VisualChildrenCount < 1)
        //        throw new Exception("StackBarControl didn't load properly");

        //    Border border = this.GetVisualChild(0) as Border;
        //    _headerScroll = border?.FindName("HeaderScrollViewer") as ScrollViewer;
        //    _barScroll = border?.FindName("BarScrollViewer") as ScrollViewer;

        //    _headerScroll.ScrollChanged += OnScrollChanged;
        //    _headerScroll.SizeChanged += OnHeaderSizeChanged;
        //    _barScroll.ScrollChanged += OnScrollChanged;
        //}
        //private void SetScale()
        //{
        //    if (MinCellWidth <= 0 || IsPreviewMode)
        //        SetScaleByWidth();
        //    else
        //        SetScaleByCellValue();
        //}

        /// <summary>
        /// timer postphones scaling so resizing is already finished when it fires,
        /// and also aggregates multiple calls from MeasureOverride/RenderSizeChanged into single call
        /// </summary>
        private void ResizeTimerFinished(object sender, EventArgs e)
        {
            ((DispatcherTimer)sender).Stop();
            SetScaleByWidth();
        }
        private void SetScaleByWidth()
        {
            if (ItemsSource == null)
                return;

            ObservableCollection<StackBarRowModel> rows = (ObservableCollection<StackBarRowModel>)ItemsSource;

            double maxValue = rows.Max(r => r.Cells.Sum(c => c.Value));
            if (this.VisualChildrenCount == 0)
                return;
            Border border = this.GetVisualChild(0) as Border;
            ScrollViewer _barScroll = border?.FindName("BarScrollViewer") as ScrollViewer;
            Grid _grid = border?.FindName("StackBarGrid") as Grid;
            Grid _grid2 = _grid.FindName("StackBarPlaceholderGrid") as Grid;
            double headerWidth = _grid2.ColumnDefinitions[0].ActualWidth;
            double barWidth = _barScroll?.ViewportWidth ?? 0;

            //Sometimes StackBar happen to not fit by width a little. I suspect a rounding error
            //Nudging measure a little seems to fix the problem
            //double cellsDensity = barWidth / rows.Sum(r => r.Cells.Count);
            //double multiplier = IsPreviewMode ? 1 : 1 - (0.05 / cellsDensity);

            GlobalScale = (barWidth - headerWidth) / maxValue;
        }
        protected override Size MeasureOverride(Size constraint)
        {
            //SetScaleByWidth();
            _resizeTimer.Stop();
            _resizeTimer.Start();
            return base.MeasureOverride(constraint);
        }
        protected override void OnItemsSourceChanged(IEnumerable oldValue, IEnumerable newValue)
        {
            if (newValue == null)
                return;
            if (!(newValue is ObservableCollection<StackBarRowModel>))
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

            //SetScaleByWidth();
            _resizeTimer.Stop();
            _resizeTimer.Start();
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
            SetScaleByWidth();
        }
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            //BindScrollViewers();
            //SetScaleByWidth();
            _resizeTimer.Stop();
            _resizeTimer.Start();
        }
        //private void OnScrollChanged(object sender, ScrollChangedEventArgs e)
        //{
        //    _barScroll.ScrollToVerticalOffset(e.VerticalOffset);

        //    if (sender == _headerScroll)
        //    {
        //        _barScroll.ScrollToVerticalOffset(e.VerticalOffset);
        //    }
        //    else
        //    {
        //        _headerScroll.ScrollToVerticalOffset(e.VerticalOffset);
        //    }
        //}

        //private void OnHeaderSizeChanged(object sender, SizeChangedEventArgs e)
        //{
        //    //SetScaleByWidth();
        //    _resizeTimer.Stop();
        //    _resizeTimer.Start();
        //}
        #endregion 
    }
}
