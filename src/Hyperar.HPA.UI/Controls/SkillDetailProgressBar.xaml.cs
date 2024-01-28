namespace Hyperar.HPA.UI.Controls
{
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
    using System.Windows.Media.Imaging;
    using System.Windows.Navigation;
    using System.Windows.Shapes;
    using Hyperar.HPA.Common.Enums;

    /// <summary>
    /// Interaction logic for SkillDetailProgressBar.xaml
    /// </summary>
    public partial class SkillDetailProgressBar : UserControl
    {
        public static readonly DependencyProperty maximumProperty = DependencyProperty.Register("Maximum", typeof(int), typeof(SkillDeltaColumnTemplate), new FrameworkPropertyMetadata(null));

        public static readonly DependencyProperty textProperty = DependencyProperty.Register("Text", typeof(string), typeof(SkillDetailProgressBar), new FrameworkPropertyMetadata(null));

        public static readonly DependencyProperty valueProperty = DependencyProperty.Register("Value", typeof(SkillLevel), typeof(SkillDetailProgressBar), new FrameworkPropertyMetadata(null));

        public SkillDetailProgressBar()
        {
            this.InitializeComponent();
        }

        public int Maximum
        {
            get
            {
                return (int)this.GetValue(maximumProperty);
            }
            set
            {
                this.SetValue(maximumProperty, value);
            }
        }

        public string Text
        {
            get
            {
                return (string)this.GetValue(textProperty);
            }
            set
            {
                this.SetValue(textProperty, value);
            }
        }

        public SkillLevel Value
        {
            get
            {
                return (SkillLevel)this.GetValue(valueProperty);
            }
            set
            {
                this.SetValue(valueProperty, value);
            }
        }
    }
}