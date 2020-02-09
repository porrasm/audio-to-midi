using PitchToMidi.SoundAnalysis;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace PitchToMidi.GUI {
    public class FrequencyChart : Chart {

        #region fields
        private double chartZoomXScrollMultiplier = 0.05;
        private double chartZoomYScrollMultiplier = 0.01;

        private double chartScrollXMultiplier = 0.01;
        private double chartScrollYMultiplier = 0.05;

        private bool mouseDown;
        private Point lastPoint;

        private KeyState chartKeys;
        #endregion

        public FrequencyChart() {
            chartKeys = new KeyState();
            InitializeChart();
        }

        public void Clear() {
            Series.Clear();
        }
        public void AddFrequencyData(AudioFrequencyData data, string name) {

            Series series = NewSeries(name);

            foreach (NoteEvent e in data.Events) {
                series.Points.AddXY(data.NoteToTime(e), e.Note.AccurateMidiNote);
            }
        }

        public void InitializeChart() {

            ChartArea area = new ChartArea("Frequency data");

            area.AxisX.Title = "Time(s)";
            area.AxisY.Title = "Frequency(note)";

            area.AxisX.IntervalAutoMode = IntervalAutoMode.FixedCount;
            area.AxisX.Interval = 1;
            area.AxisX.IntervalOffset = 0;
            area.AxisX.LabelStyle.Format = "0.00";
            area.AxisX.MajorGrid.LineColor = Color.Gray;

            area.AxisY.IntervalAutoMode = IntervalAutoMode.FixedCount;
            area.AxisY.Interval = 1;
            area.AxisY.IntervalOffset = 0;
            area.AxisY.MajorGrid.LineColor = Color.Gray;

            for (int i = 0; i < 128; i++) {
                area.AxisY.CustomLabels.Add(new CustomLabel(i, i + 0.05, Note.NoteToString(i), 0, LabelMarkStyle.Box));
            }

            area.BackColor = Color.LightGray;

            ChartAreas[0] = area;

            KeyDown += new KeyEventHandler(ChartKeyDown);
            KeyUp += new KeyEventHandler(ChartKeyUp);

            MouseDown += new MouseEventHandler(OnChartMouseDown);
            MouseUp += new MouseEventHandler(OnChartMouseUp);
            MouseMove += new MouseEventHandler(OnChartMouseMove);

            MouseWheel += new MouseEventHandler(OnChartMouseWheel);
        }

        public Series NewSeries(string name) {
            Series series = new Series(name);
            series.BorderWidth = 2;
            series.ChartType = SeriesChartType.Line;
            return series;
        }

        private void ChartKeyDown(object sender, KeyEventArgs e) {
            chartKeys.KeyEvent(e.KeyCode, true);
        }
        private void ChartKeyUp(object sender, KeyEventArgs e) {
            chartKeys.KeyEvent(e.KeyCode, false);
        }

        private void OnChartMouseDown(object sender, MouseEventArgs e) {
            mouseDown = true;
            lastPoint = e.Location;
        }
        private void OnChartMouseUp(object sender, MouseEventArgs e) {
            mouseDown = false;
            lastPoint = e.Location;
        }
        private void OnChartMouseWheel(object sender, MouseEventArgs e) {
            Console.WriteLine("Xooming: " + e.Delta);

            if (chartKeys.Pressing(Keys.ShiftKey)) {
                double yCenter = Area.AxisY.PixelPositionToValue(e.Location.Y);
                ZoomAxis(Area.AxisY, yCenter, chartZoomYScrollMultiplier, e);
            } else {
                double xCenter = Area.AxisX.PixelPositionToValue(e.Location.X);
                ZoomAxis(Area.AxisX, xCenter, chartZoomXScrollMultiplier, e);
            }

            SetLineOffsets();
        }

        private void ZoomAxis(Axis axis, double mouseCenter, double multiplier, MouseEventArgs e) {
            double viewStart = axis.ScaleView.ViewMinimum;
            double viewEnd = axis.ScaleView.ViewMaximum;

            double distance = viewEnd - viewStart;

            double startFactor = (mouseCenter - viewStart) / distance;
            double endFactor = (viewEnd - mouseCenter) / distance;

            if (chartKeys.Pressing(Keys.ControlKey)) {
                multiplier *= 0.2;
            }

            viewStart += e.Delta * multiplier * startFactor;
            viewEnd -= e.Delta * multiplier * endFactor;

            if (viewStart >= viewEnd) {
                return;
            }

            axis.ScaleView.Zoom(viewStart, viewEnd);
        }

        private void OnChartMouseMove(object sender, MouseEventArgs e) {

            if (lastPoint == e.Location || !mouseDown) {
                return;
            }

            int xDelta = e.Location.X - lastPoint.X;
            int yDelta = e.Location.Y - lastPoint.Y;

            Console.WriteLine("delta: " + new Point(xDelta, yDelta));

            lastPoint = e.Location;

            double xMult = chartScrollXMultiplier;
            double yMult = chartScrollYMultiplier;

            if (chartKeys.Pressing(Keys.ControlKey)) {
                xMult *= 0.2;
                yMult *= 0.2;
            }

            double newX = Area.AxisX.ScaleView.Position - xDelta * xMult;
            double newY = Area.AxisY.ScaleView.Position + yDelta * yMult;

            if (newX < 0) {
                newX = 0;
            }
            if (newY < 0) {
                newY = 0;
            }

            Area.AxisX.ScaleView.Position = newX;
            Area.AxisY.ScaleView.Position = newY;

            SetLineOffsets();
        }

        private void SetLineOffsets() {

            double xOffset = Area.AxisX.ScaleView.Position % 1;
            double yOffset = Area.AxisY.ScaleView.Position % 1;

            Area.AxisX.MajorGrid.IntervalOffset = -xOffset;
            Area.AxisY.MajorGrid.IntervalOffset = -yOffset;
        }

        private ChartArea Area {
            get {
                return ChartAreas[0];
            }
        }
    }
}
