using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GraphicsToolKit
{
    public partial class GradientForm : Form
    {
        Point ShowPoint = new Point(0, 0);
        List<Point> Points1 { get; set; } = new List<Point>();
        List<Point> Points2 { get; set; } = new List<Point>();
        List<Point> Points3 { get; set; } = new List<Point>();
        GraphicsPath Path1 { get; set; } = new GraphicsPath();
        GraphicsPath Path2 { get; set; } = new GraphicsPath();
        GraphicsPath Path3 { get; set; } = new GraphicsPath();
        float Tention { set; get; } = 0.1f;
        public GradientForm()
        {
            InitializeComponent();
            listBox1.DisplayMember = nameof(PointShow.ToString);
            listBox2.DisplayMember = nameof(PointShow.ToString);
            listBox3.DisplayMember = nameof(PointShow.ToString);
            float labelsWidth = panel1.Width / 255f;
            label1.Width = (int)labelsWidth;
            int left = label1.Left;

            for (int i = 2; i <= 255; i++)
            {
                var label = new Label()
                {
                    Name = $"label{i}",
                    Width = (int)labelsWidth,
                    Left = (int)((float)left + labelsWidth * (float)(i - 1)),
                    Top = label1.Top,
                    BackColor = Color.Black,
                    Height = label1.Height
                };
                Controls.Add(label);
            }
            ClearToolStripMenuItem_Click(new object(), new EventArgs());
        }

        private void ColorLabels()
        {
            float count = 0;
            foreach (Control label in Controls)
            {
                if (label.GetType() == typeof(Label))
                {
                    count += 1;
                    int r = 0;
                    for (r = 0; r <= panel1.Height; r++)
                    {
                        if (Path1.IsOutlineVisible(count / 255f * panel1.Width, r, new Pen(Color.Red, 0.01f)))
                            break;
                    }
                    r = (int)((float)r / (float)panel1.Height * 255f);
                    int g = 0;
                    for (g = 0; g <= panel2.Height; g++)
                    {
                        if (Path2.IsOutlineVisible(count / 255f * panel2.Width, g, new Pen(Color.Red, 0.01f)))
                            break;
                    }
                    g = (int)((float)g / (float)panel2.Height * 255f);
                    int b = 0;
                    for (b = 0; b <= panel3.Height; b++)
                    {
                        if (Path3.IsOutlineVisible(count / 255f * panel3.Width, b, new Pen(Color.Red, 0.01f)))
                            break;
                    }
                    b = (int)((float)b / (float)panel3.Height * 255f);
                    CutForColor(ref r);
                    CutForColor(ref g);
                    CutForColor(ref b);
                    label.BackColor = Color.FromArgb(r, g, b);
                }
            }
        }

        private static void CutForColor(ref int r)
        {
            r = r > 255 ? 255 : r < 0 ? r = 0 : r;
        }

        private void ClearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Points1.Clear();
            listBox1.Items.Clear();
            Point point0 = new Point(0, 0);
            Points1.Add(point0);
            Points1.Add(new Point(panel1.Width, panel1.Height));
            listBox1.Items.Add(new PointShow() { point = point0 });
            listBox1.Items.Add(new PointShow() { point = new Point(panel1.Width, panel1.Height) });
            Path1 = new GraphicsPath();
            Path1.AddCurve(Points1.OrderBy(p => p.X).ToArray(), Tention);
            Points2.Clear();
            listBox2.Items.Clear();
            Points2.Add(point0);
            Points2.Add(new Point(panel2.Width, panel2.Height));
            listBox2.Items.Add(new PointShow() { point = point0 });
            listBox2.Items.Add(new PointShow() { point = new Point(panel2.Width, panel1.Height) });
            Path2 = new GraphicsPath();
            Path2.AddCurve(Points2.OrderBy(p => p.X).ToArray(), Tention);
            Points3.Clear();
            listBox3.Items.Clear();
            Points3.Add(point0);
            Points3.Add(new Point(panel3.Width, panel3.Height));
            listBox3.Items.Add(new PointShow() { point = point0 });
            listBox3.Items.Add(new PointShow() { point = new Point(panel3.Width, panel1.Height) });
            Path3 = new GraphicsPath();
            Path3.AddCurve(Points3.OrderBy(p => p.X).ToArray(), Tention);
            ColorLabels();
            panel1.Refresh();
            panel2.Refresh();
            panel3.Refresh();
            listBox1.Refresh();
            listBox2.Refresh();
            listBox3.Refresh();
        }

        private void Panel1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            // Draw the points.
            foreach (Point point in Points1)
                e.Graphics.FillEllipse(Brushes.Black,
                    point.X - 3, point.Y - 3, 5, 5);
            if (Points1.Count < 2) return;


            // Draw the curve.
            Path1 = new GraphicsPath();
            Path1.AddCurve(Points1.OrderBy(p => p.X).ToArray(), Tention);
            e.Graphics.DrawPath(Pens.Red, Path1);
            e.Graphics.FillEllipse(Brushes.Blue, ShowPoint.X - 3, ShowPoint.Y - 3, 5, 5);
        }

        private void Panel1_MouseClick(object sender, MouseEventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                var thePoint = (PointShow)listBox1.SelectedItem;
                listBox1.Items.Remove(listBox1.SelectedItem);
                Points1.Remove(thePoint.point);
            }
            Points1.Add(e.Location);
            listBox1.Items.Add(new PointShow() { point = e.Location });
            listBox1.Refresh();
            panel1.Refresh();
            ColorLabels();
        }
        private struct PointShow
        {
            public Point point;

            override
            public string ToString()
            {
                return $"X: {point.X} Y: {point.Y}";
            }
        }
        private void Panel2_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            // Draw the points.
            foreach (Point point in Points2)
                e.Graphics.FillEllipse(Brushes.Black,
                    point.X - 3, point.Y - 3, 5, 5);
            if (Points2.Count < 2) return;


            // Draw the curve.
            Path2 = new GraphicsPath();
            Path2.AddCurve(Points2.OrderBy(p => p.X).ToArray(), Tention);
            e.Graphics.DrawPath(Pens.Red, Path2);
        }

        private void Panel2_MouseClick(object sender, MouseEventArgs e)
        {
            if (listBox2.SelectedItem != null)
            {
                var thePoint = (PointShow)listBox2.SelectedItem;
                listBox2.Items.Remove(listBox2.SelectedItem);
                Points2.Remove(thePoint.point);
            }
            Points2.Add(e.Location);
            listBox2.Items.Add(new PointShow() { point = e.Location });
            listBox2.Refresh();
            panel2.Refresh();
            ColorLabels();
        }

        private void Panel3_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            // Draw the points.
            foreach (Point point in Points3)
                e.Graphics.FillEllipse(Brushes.Black,
                    point.X - 3, point.Y - 3, 5, 5);
            if (Points3.Count < 2) return;


            // Draw the curve.
            Path3 = new GraphicsPath();
            Path3.AddCurve(Points3.OrderBy(p => p.X).ToArray(), Tention);
            e.Graphics.DrawPath(Pens.Red, Path3);
        }

        private void Panel3_MouseClick(object sender, MouseEventArgs e)
        {
            if (listBox3.SelectedItem != null)
            {
                var thePoint = (PointShow)listBox3.SelectedItem;
                listBox3.Items.Remove(listBox3.SelectedItem);
                Points3.Remove(thePoint.point);
            }
            Points3.Add(e.Location);
            listBox3.Items.Add(new PointShow() { point = e.Location });
            listBox3.Refresh();
            panel3.Refresh();
            ColorLabels();
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            Tention = float.Parse(textBox1.Text);
        }
    }
}
