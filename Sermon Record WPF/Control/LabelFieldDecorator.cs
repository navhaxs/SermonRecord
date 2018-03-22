using System.Windows;
using System.Windows.Controls;

namespace SermonRecord.Control
{
    class LabelFieldDecorator : ContentControl
    {
        static LabelFieldDecorator()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LabelFieldDecorator), new FrameworkPropertyMetadata(typeof(LabelFieldDecorator)));
        }

        public string FieldLabelText
        {
            get { return (string)GetValue(FieldLabelTextProperty); }
            set { SetValue(FieldLabelTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HighlightBackground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FieldLabelTextProperty =
            DependencyProperty.Register("FieldLabelText", typeof(string), typeof(LabelFieldDecorator), new PropertyMetadata("My new control field"));

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
        }
    }
}
