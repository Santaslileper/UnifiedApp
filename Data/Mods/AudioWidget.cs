using System;
using System.Drawing;
using System.Windows.Forms;
using UnifiedApp;
using UnifiedApp.Volume;

public class AudioWidget : WidgetHost {
    public AudioWidget() : base("Audio Control", 300, 180) {
        var flow = new FlowLayoutPanel { Dock = DockStyle.Fill, AutoScroll = true, BackColor = Color.Transparent, Padding = new Padding(5) };
        foreach (var dev in AudioUtil.GetDevices()) {
            string name = AudioUtil.GetName(dev);
            var card = new Panel { Width = 260, Height = 60, Margin = new Padding(0,0,0,10) };
            card.Paint += delegate(object s, PaintEventArgs e) { e.Graphics.DrawString(name, new Font("Segoe UI", 9, FontStyle.Bold), new SolidBrush(Color.White), 5, 5); };
            
            var meter = new ProgressBar { Location = new Point(5, 25), Size = new Size(250, 6), Maximum = 100, Style = ProgressBarStyle.Continuous };
            var timer = new System.Windows.Forms.Timer { Interval = 50 };
            timer.Tick += delegate { float peak; AudioUtil.GetMeter(dev).G(out peak); meter.Value = (int)(peak * 100); };
            timer.Start();

            var sl = new TrackBar { Location = new Point(0, 32), Size = new Size(260, 25), Maximum = 100, TickStyle = TickStyle.None };
            float vol; AudioUtil.GetVolume(dev).GM(out vol); sl.Value = (int)(vol * 100);
            sl.Scroll += delegate { Guid g = Guid.Empty; AudioUtil.GetVolume(dev).SM(sl.Value / 100f, ref g); };

            card.Controls.Add(meter); card.Controls.Add(sl);
            flow.Controls.Add(card);
        }
        ContentPanel.Controls.Add(flow);
    }
}
