namespace StoperApp;

using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

public partial class Form1 : Form
{
    private readonly Stopwatch _stopwatch = new();
    private readonly System.Windows.Forms.Timer _timer = new();

    private Label _lblHeader = null!;
    private Label _lblTime = null!;
    private Label _lblStatus = null!;
    private Button _btnStart = null!;
    private Button _btnStop = null!;
    private Button _btnReset = null!;

    public Form1()
    {
        InitializeCustomComponents();
    }

    private void InitializeCustomComponents()
    {
        // Ustawienia okna
        Text = "Stoper - Zadanie 2.1";
        ClientSize = new Size(420, 240);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        StartPosition = FormStartPosition.CenterScreen;
        BackColor = Color.FromArgb(30, 30, 46);

        // Nagłówek
        _lblHeader = new Label
        {
            Text = "STOPER CYFROWY",
            Font = new Font("Segoe UI", 10, FontStyle.Bold),
            ForeColor = Color.FromArgb(180, 190, 210),
            Location = new Point(0, 20),
            Size = new Size(420, 22),
            TextAlign = ContentAlignment.MiddleCenter
        };

        // Wyświetlacz czasu
        _lblTime = new Label
        {
            Text = "00:00:00.00",
            Font = new Font("Consolas", 32, FontStyle.Bold),
            ForeColor = Color.FromArgb(137, 220, 235),
            Location = new Point(0, 48),
            Size = new Size(420, 56),
            TextAlign = ContentAlignment.MiddleCenter
        };

        // Etykieta stanu
        _lblStatus = new Label
        {
            Text = "Stan: Gotowy do startu",
            Font = new Font("Segoe UI", 9, FontStyle.Italic),
            ForeColor = Color.FromArgb(166, 173, 200),
            Location = new Point(0, 110),
            Size = new Size(420, 22),
            TextAlign = ContentAlignment.MiddleCenter
        };

        // Przycisk Start
        _btnStart = new Button
        {
            Text = "Start",
            Location = new Point(35, 150),
            Size = new Size(105, 45),
            Font = new Font("Segoe UI", 11, FontStyle.Bold),
            BackColor = Color.FromArgb(166, 227, 161),
            ForeColor = Color.FromArgb(24, 24, 37),
            FlatStyle = FlatStyle.Flat,
            Cursor = Cursors.Hand
        };
        _btnStart.FlatAppearance.BorderSize = 0;
        _btnStart.Click += BtnStart_Click;

        // Przycisk Stop
        _btnStop = new Button
        {
            Text = "Stop",
            Location = new Point(157, 150),
            Size = new Size(105, 45),
            Font = new Font("Segoe UI", 11, FontStyle.Bold),
            BackColor = Color.FromArgb(243, 139, 168),
            ForeColor = Color.FromArgb(24, 24, 37),
            FlatStyle = FlatStyle.Flat,
            Enabled = false,
            Cursor = Cursors.Hand
        };
        _btnStop.FlatAppearance.BorderSize = 0;
        _btnStop.Click += BtnStop_Click;

        // Przycisk Reset
        _btnReset = new Button
        {
            Text = "Reset",
            Location = new Point(280, 150),
            Size = new Size(105, 45),
            Font = new Font("Segoe UI", 11, FontStyle.Bold),
            BackColor = Color.FromArgb(137, 180, 250),
            ForeColor = Color.FromArgb(24, 24, 37),
            FlatStyle = FlatStyle.Flat,
            Enabled = false,
            Cursor = Cursors.Hand
        };
        _btnReset.FlatAppearance.BorderSize = 0;
        _btnReset.Click += BtnReset_Click;

        // Timer odświeżający interfejs (~30 FPS)
        _timer.Interval = 30;
        _timer.Tick += (s, e) => UpdateDisplay();

        // Dodanie kontrolek do okna
        Controls.Add(_lblHeader);
        Controls.Add(_lblTime);
        Controls.Add(_lblStatus);
        Controls.Add(_btnStart);
        Controls.Add(_btnStop);
        Controls.Add(_btnReset);
    }

    private void UpdateDisplay()
    {
        TimeSpan ts = _stopwatch.Elapsed;
        _lblTime.Text = $"{ts.Hours:00}:{ts.Minutes:00}:{ts.Seconds:00}.{(ts.Milliseconds / 10):00}";
    }

    private void BtnStart_Click(object? sender, EventArgs e)
    {
        _stopwatch.Start();
        _timer.Start();

        _btnStart.Enabled = false;
        _btnStop.Enabled = true;
        _btnReset.Enabled = true;
        _lblStatus.Text = "Stan: Odmierzanie w toku...";
    }

    private void BtnStop_Click(object? sender, EventArgs e)
    {
        _stopwatch.Stop();
        _timer.Stop();
        UpdateDisplay();

        _btnStart.Enabled = true;
        _btnStop.Enabled = false;
        _btnReset.Enabled = true;
        _lblStatus.Text = "Stan: Wstrzymany";
    }

    private void BtnReset_Click(object? sender, EventArgs e)
    {
        _stopwatch.Reset();
        _timer.Stop();
        _lblTime.Text = "00:00:00.00";

        _btnStart.Enabled = true;
        _btnStop.Enabled = false;
        _btnReset.Enabled = false;
        _lblStatus.Text = "Stan: Wyzerowany";
    }
}
