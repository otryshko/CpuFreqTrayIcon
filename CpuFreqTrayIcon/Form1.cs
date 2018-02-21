namespace CpuFreqTrayIcon
{
    using System;
    using System.Drawing;
    using System.Drawing.Text;
    using System.Management;
    using System.Windows.Forms;

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public void CreateTextIcon(uint str, uint maxsp)
        {
            var fontToUse = new Font("Trebuchet MS", 60, FontStyle.Regular, GraphicsUnit.Pixel);
            var bitmapText = new Bitmap(128, 128);

            var g = Graphics.FromImage(bitmapText);
            g.Clear(Color.Blue);
            g.FillRectangle(Brushes.DarkOrange, 0, 80, (float) str / maxsp * 128, 128);
            g.TextRenderingHint = TextRenderingHint.SingleBitPerPixelGridFit;
            g.DrawString(str.ToString(), fontToUse, Brushes.White, -10, 0);

            var hIcon = bitmapText.GetHicon();
            notifyIcon1.Text = str.ToString();
            notifyIcon1.Icon?.Dispose();
            notifyIcon1.Icon = Icon.FromHandle(hIcon);
            bitmapText.Dispose();
            g.Dispose();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            uint currentsp, Maxsp;
            using (var Mo = new ManagementObject("Win32_Processor.DeviceID='CPU0'"))
            {
                currentsp = (uint) Mo["CurrentClockSpeed"];
                Maxsp = (uint) Mo["MaxClockSpeed"];
            }
            CreateTextIcon(currentsp, Maxsp);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Visible = false;
        }
    }
}