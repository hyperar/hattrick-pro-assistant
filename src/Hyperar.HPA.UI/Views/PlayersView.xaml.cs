namespace Hyperar.HPA.UI.Views
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
    using Hyperar.HPA.Application.Models.Players;
    using ScottPlot;

    /// <summary>
    /// Interaction logic for PlayersView.xaml
    /// </summary>
    public partial class PlayersView : UserControl
    {
        private readonly string[] radarChartCategoryNames = new string[]
        {
            Globalization.Strings.Keeper,
                Globalization.Strings.Playmaking,
                Globalization.Strings.Passing,
                Globalization.Strings.Scoring,
                Globalization.Strings.SetPieces,
                Globalization.Strings.Winger,
                Globalization.Strings.Defending
            };

        private readonly double[] radarChartMaxValues = new double[] { 20, 20, 20, 20, 20, 20, 20 };

        private SeniorPlayer? selectedComparisonSeniorPlayer;

        private SeniorPlayer? selectedSeniorPlayer;

        public PlayersView()
        {
            this.InitializeComponent();
            ScottPlot.Version.ShouldBe(4, 1, 71);
        }

        private static string CustomLabelFormatter(double position)
        {
            return position.ToString("N0");
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ComboBox combobox && combobox.SelectedItem is SeniorPlayer seniorPlayer)
            {
                this.selectedComparisonSeniorPlayer = seniorPlayer;

                this.RenderRadarChart();
            }
        }

        private void DataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (sender is DataGrid dataGrid && dataGrid.SelectedItem is SeniorPlayer seniorPlayer)
            {
                this.selectedSeniorPlayer = seniorPlayer;

                this.RenderRadarChart();
            }
        }

        private void RenderRadarChart()
        {
            this.SeniorPlayerSkillRadarChart.Plot.Clear();

            ArgumentNullException.ThrowIfNull(this.selectedSeniorPlayer, nameof(this.selectedSeniorPlayer));

            var plt = new Plot();

            double[,] values;
            string[] groupNames;

            if (this.selectedComparisonSeniorPlayer == null)
            {
                groupNames = [this.selectedSeniorPlayer.FullName];
                values = new double[,]
                {
                    {
                        (int)this.selectedSeniorPlayer.Keeper,
                        (int)this.selectedSeniorPlayer.Playmaking,
                        (int)this.selectedSeniorPlayer.Passing,
                        (int)this.selectedSeniorPlayer.Scoring,
                        (int)this.selectedSeniorPlayer.SetPieces,
                        (int)this.selectedSeniorPlayer.Winger,
                        (int)this.selectedSeniorPlayer.Defending
                    }
                };
            }
            else
            {
                groupNames = [this.selectedSeniorPlayer.FullName, this.selectedComparisonSeniorPlayer.FullName];
                values = new double[,]
                {
                    {
                        (int)this.selectedSeniorPlayer.Keeper,
                        (int)this.selectedSeniorPlayer.Playmaking,
                        (int)this.selectedSeniorPlayer.Passing,
                        (int)this.selectedSeniorPlayer.Scoring,
                        (int)this.selectedSeniorPlayer.SetPieces,
                        (int)this.selectedSeniorPlayer.Winger,
                        (int)this.selectedSeniorPlayer.Defending
                    },
                    {
                        (int)selectedComparisonSeniorPlayer.Keeper,
                        (int)selectedComparisonSeniorPlayer.Playmaking,
                        (int)selectedComparisonSeniorPlayer.Passing,
                        (int)selectedComparisonSeniorPlayer.Scoring,
                        (int)selectedComparisonSeniorPlayer.SetPieces,
                        (int)selectedComparisonSeniorPlayer.Winger,
                        (int)selectedComparisonSeniorPlayer.Defending
                    }
                };
            }

            plt.Legend(true, Alignment.LowerLeft);

            var radarPlot = plt.AddRadar(
                values,
                true,
                radarChartMaxValues,
                true,
                100);

            radarPlot.AxisLabelStringFormatter = CustomLabelFormatter;
            radarPlot.AxisType = RadarAxis.Polygon;
            radarPlot.CategoryLabels = radarChartCategoryNames;
            radarPlot.GroupLabels = groupNames;
            radarPlot.ShowAxisValues = true;
            radarPlot.ShowCategoryLabels = true;
            radarPlot.OutlineWidth = 2;

            this.SeniorPlayerSkillRadarChart.AllowDrop =
            this.SeniorPlayerSkillRadarChart.Configuration.Pan =
            this.SeniorPlayerSkillRadarChart.Configuration.Zoom =
            this.SeniorPlayerSkillRadarChart.Configuration.ScrollWheelZoom =
            this.SeniorPlayerSkillRadarChart.Configuration.MiddleClickDragZoom = false;

            this.SeniorPlayerSkillRadarChart.Plot.Add(radarPlot);
            this.SeniorPlayerSkillRadarChart.Refresh();
        }
    }
}