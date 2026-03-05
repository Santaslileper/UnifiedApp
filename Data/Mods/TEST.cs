using System;
using System.Drawing;
using System.Windows.Forms;
using DesktopWidgets;

public class ModernTimerWidget : WidgetHost
{
    private Label timerLabel;
    private NumericUpDown minutesInput;
    private Button startButton;
    private Button pauseButton;
    private Button resetButton;
    private Timer timer;
    private TimeSpan remainingTime;
    private bool isRunning = false;

    // Modern colors
    private Color bgColor = Color.FromArgb(30, 30, 30);
    private Color accent = Color.FromArgb(140, 82, 255);
    private Color buttonColor = Color.FromArgb(45, 45, 45);

    public ModernTimerWidget(string id = null)
        : base("ModernTimer", 300, 180, id)
    {
        remainingTime = TimeSpan.FromMinutes(60);

        ContentPanel.BackColor = bgColor;

        // Timer Display
        timerLabel = new Label
        {
            Text = remainingTime.ToString(@"mm\:ss"),
            ForeColor = accent,
            Dock = DockStyle.Top,
            Height = 70,
            TextAlign = ContentAlignment.MiddleCenter,
            Font = new Font("Segoe UI", 28, FontStyle.Bold),
            BackColor = bgColor
        };

        // Minutes Input
        minutesInput = new NumericUpDown
        {
            Minimum = 1,
            Maximum = 999,
            Value = 60,
            Width = 80,
            Font = new Font("Segoe UI", 10, FontStyle.Bold),
            BackColor = buttonColor,
            ForeColor = Color.White,
            BorderStyle = BorderStyle.FixedSingle
        };

        Label minutesLabel = new Label
        {
            Text = "Minutes",
            ForeColor = Color.Gray,
            AutoSize = true,
            Font = new Font("Segoe UI", 9),
            BackColor = bgColor
        };

        FlowLayoutPanel inputPanel = new FlowLayoutPanel
        {
            Dock = DockStyle.Top,
            Height = 40,
            BackColor = bgColor,
            FlowDirection = FlowDirection.LeftToRight,
            Padding = new Padding(10, 5, 10, 5)
        };

        inputPanel.Controls.Add(minutesInput);
        inputPanel.Controls.Add(minutesLabel);

        // Buttons
        FlowLayoutPanel buttonPanel = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            BackColor = bgColor,
            FlowDirection = FlowDirection.LeftToRight,
            Padding = new Padding(10)
        };

        startButton = CreateModernButton("START");
        pauseButton = CreateModernButton("PAUSE");
        resetButton = CreateModernButton("RESET");

        startButton.Click += StartButton_Click;
        pauseButton.Click += PauseButton_Click;
        resetButton.Click += ResetButton_Click;

        buttonPanel.Controls.Add(startButton);
        buttonPanel.Controls.Add(pauseButton);
        buttonPanel.Controls.Add(resetButton);

        // Timer
        timer = new Timer();
        timer.Interval = 1000;
        timer.Tick += Timer_Tick;

        ContentPanel.Controls.Add(buttonPanel);
        ContentPanel.Controls.Add(inputPanel);
        ContentPanel.Controls.Add(timerLabel);

        Load();
    }

    private Button CreateModernButton(string text)
    {
        Button btn = new Button
        {
            Text = text,
            Width = 80,
            Height = 35,
            FlatStyle = FlatStyle.Flat,
            BackColor = buttonColor,
            ForeColor = Color.White,
            Font = new Font("Segoe UI", 9, FontStyle.Bold)
        };

        btn.FlatAppearance.BorderSize = 0;

        btn.MouseEnter += (s, e) => btn.BackColor = accent;
        btn.MouseLeave += (s, e) => btn.BackColor = buttonColor;

        return btn;
    }

    private void StartButton_Click(object sender, EventArgs e)
    {
        if (!isRunning)
        {
            if (remainingTime.TotalSeconds <= 0)
                remainingTime = TimeSpan.FromMinutes((double)minutesInput.Value);

            timer.Start();
            isRunning = true;
            timerLabel.ForeColor = accent;
        }
    }

    private void PauseButton_Click(object sender, EventArgs e)
    {
        timer.Stop();
        isRunning = false;
    }

    private void ResetButton_Click(object sender, EventArgs e)
    {
        timer.Stop();
        isRunning = false;
        remainingTime = TimeSpan.FromMinutes((double)minutesInput.Value);
        timerLabel.Text = remainingTime.ToString(@"mm\:ss");
        timerLabel.ForeColor = accent;
    }

    private void Timer_Tick(object sender, EventArgs e)
    {
        if (remainingTime.TotalSeconds > 0)
        {
            remainingTime = remainingTime.Subtract(TimeSpan.FromSeconds(1));
            timerLabel.Text = remainingTime.ToString(@"mm\:ss");
        }
        else
        {
            timer.Stop();
            timerLabel.Text = "TIME'S UP";
            timerLabel.ForeColor = Color.Red;
            isRunning = false;
        }
    }
}