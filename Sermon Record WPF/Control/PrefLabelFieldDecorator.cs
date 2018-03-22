using System;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SermonRecord.Control
{
    class PrefLabelFieldDecorator : ContentControl
    {
        static PrefLabelFieldDecorator()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PrefLabelFieldDecorator), new FrameworkPropertyMetadata(typeof(PrefLabelFieldDecorator)));
        }

        public string FieldLabelText
        {
            get { return (string)GetValue(FieldLabelTextProperty); }
            set { SetValue(FieldLabelTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HighlightBackground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FieldLabelTextProperty =
            DependencyProperty.Register("FieldLabelText", typeof(string), typeof(PrefLabelFieldDecorator), new PropertyMetadata("My new control field"));

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
        }
    }
}
