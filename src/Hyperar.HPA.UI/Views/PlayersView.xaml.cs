namespace Hyperar.HPA.UI.Views
{
    using System;
    using System.Windows.Controls;
    using Application.Models.Players;
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

        private Player? selectedComparisonPlayer;

        private Player? selectedPlayer;

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
            if (sender is ComboBox combobox && combobox.SelectedItem is Player player)
            {
                this.selectedComparisonPlayer = player;

                this.RenderRadarChart();
            }
        }

        private void DataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (sender is DataGrid dataGrid && dataGrid.SelectedItem is Player player)
            {
                this.selectedPlayer = player;

                this.RenderRadarChart();
            }
        }

        private void RenderRadarChart()
        {
            this.PlayerSkillSetRadarChart.Plot.Clear();

            ArgumentNullException.ThrowIfNull(this.selectedPlayer, nameof(this.selectedPlayer));

            var plt = new Plot();

            double[,] values;
            string[] groupNames;

            if (this.selectedComparisonPlayer == null)
            {
                groupNames = new string[] { this.selectedPlayer.FullName };

                values = new double[,]
                {
                    {
                        (int)this.selectedPlayer.Keeper,
                        (int)this.selectedPlayer.Playmaking,
                        (int)this.selectedPlayer.Passing,
                        (int)this.selectedPlayer.Scoring,
                        (int)this.selectedPlayer.SetPieces,
                        (int)this.selectedPlayer.Winger,
                        (int)this.selectedPlayer.Defending
                    }
                };
            }
            else
            {
                groupNames = new string[] { this.selectedPlayer.FullName, this.selectedComparisonPlayer.FullName };

                values = new double[,]
                {
                    {
                        (int)this.selectedPlayer.Keeper,
                        (int)this.selectedPlayer.Playmaking,
                        (int)this.selectedPlayer.Passing,
                        (int)this.selectedPlayer.Scoring,
                        (int)this.selectedPlayer.SetPieces,
                        (int)this.selectedPlayer.Winger,
                        (int)this.selectedPlayer.Defending
                    },
                    {
                        (int)selectedComparisonPlayer.Keeper,
                        (int)selectedComparisonPlayer.Playmaking,
                        (int)selectedComparisonPlayer.Passing,
                        (int)selectedComparisonPlayer.Scoring,
                        (int)selectedComparisonPlayer.SetPieces,
                        (int)selectedComparisonPlayer.Winger,
                        (int)selectedComparisonPlayer.Defending
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

            this.PlayerSkillSetRadarChart.AllowDrop =
            this.PlayerSkillSetRadarChart.Configuration.Pan =
            this.PlayerSkillSetRadarChart.Configuration.Zoom =
            this.PlayerSkillSetRadarChart.Configuration.ScrollWheelZoom =
            this.PlayerSkillSetRadarChart.Configuration.MiddleClickDragZoom = false;

            this.PlayerSkillSetRadarChart.Plot.XAxis.SetBoundary(-1.3, 1.3);
            this.PlayerSkillSetRadarChart.Plot.YAxis.SetBoundary(-1.3, 1.4);
            this.PlayerSkillSetRadarChart.Plot.Frameless(true);

            this.PlayerSkillSetRadarChart.Plot.Add(radarPlot);
            this.PlayerSkillSetRadarChart.Refresh();
        }
    }
}