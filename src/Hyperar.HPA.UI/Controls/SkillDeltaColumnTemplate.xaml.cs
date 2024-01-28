namespace Hyperar.HPA.UI.Controls
{
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Controls;
    using Hyperar.HPA.Common.Enums;

    /// <summary>
    /// Interaction logic for SkillDeltaColumnTemplate.xaml
    /// </summary>
    public partial class SkillDeltaColumnTemplate : UserControl
    {
        public static readonly DependencyProperty deltaProperty = DependencyProperty.Register("Delta", typeof(int), typeof(SkillDeltaColumnTemplate), new FrameworkPropertyMetadata(null));

        public static readonly DependencyProperty valueProperty = DependencyProperty.Register("Value", typeof(SkillLevel), typeof(SkillDeltaColumnTemplate), new FrameworkPropertyMetadata(null));

        public SkillDeltaColumnTemplate()
        {
            this.InitializeComponent();
        }

        public int Delta
        {
            get
            {
                return (int)this.GetValue(deltaProperty);
            }
            set
            {
                this.SetValue(deltaProperty, value);
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