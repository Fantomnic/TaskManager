using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace TaskManager.CustomControls
{
    public class SpeedScrollViewer : ScrollViewer
    {
        public static readonly DependencyProperty StepProperty =
            DependencyProperty.Register(nameof(Step), typeof(double), typeof(SpeedScrollViewer), new PropertyMetadata(1.0));

        public double Step
        {
            get => (double)GetValue(StepProperty);
            set => SetValue(StepProperty, value);
        }

        protected override void OnPreviewMouseWheel(MouseWheelEventArgs e)
        {
            if (ScrollInfo is not ScrollContentPresenter scrollInfo)
                return;

            double resultDelta = e.Delta * Step;

            if (ComputedVerticalScrollBarVisibility == Visibility.Visible)
                scrollInfo.SetVerticalOffset(VerticalOffset - resultDelta);
            else if (ComputedHorizontalScrollBarVisibility == Visibility.Visible)
                scrollInfo.SetHorizontalOffset(HorizontalOffset - resultDelta);

            e.Handled = true;
        }
    };
}
