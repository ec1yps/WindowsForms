using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clock
{
	public partial class MainForm : Form
	{
		readonly PrivateFontCollection pfc = new PrivateFontCollection();
		public MainForm()
		{
			InitializeComponent();
			this.Location = new Point(Screen.PrimaryScreen.Bounds.Width - this.Width, 50);

			//InitCustomLabelFont(Properties.Resources.LTRailway_Regular);
			labelTime.BackColor = Color.AliceBlue;
		}

		void InitCustomLabelFont(byte[] fontData)
		{
			IntPtr fontPtr = System.Runtime.InteropServices.Marshal.AllocCoTaskMem(fontData.Length);
			System.Runtime.InteropServices.Marshal.Copy(fontData, 0, fontPtr, fontData.Length);
			pfc.AddMemoryFont(fontPtr, fontData.Length);
			System.Runtime.InteropServices.Marshal.FreeCoTaskMem(fontPtr);

			labelTime.Font = new Font(pfc.Families[0], labelTime.Font.Size, FontStyle.Bold);
		}

		void SetVisibility(bool visible)
		{
			cbShowDate.Visible = visible;
			cbShowWeekDay.Visible = visible;
			btnHideControls.Visible = visible;
			this.TransparencyKey = visible ? Color.Empty : this.BackColor;
			this.FormBorderStyle = visible ? FormBorderStyle.FixedToolWindow : FormBorderStyle.None;
			this.ShowInTaskbar = visible;
		}

		private void timer_Tick(object sender, EventArgs e)
		{
			labelTime.Text = DateTime.Now.ToString("hh:mm:ss tt", System.Globalization.CultureInfo.InvariantCulture);

			if (cbShowDate.Checked)
			{
				labelTime.Text += "\n";
				labelTime.Text += DateTime.Now.ToString("yyyy.MM.dd");
			}
			if (cbShowWeekDay.Checked)
			{
				labelTime.Text += "\n";
				labelTime.Text += DateTime.Now.DayOfWeek;
			}

			notifyIcon.Text = labelTime.Text;
		}

		private void btnHideControls_Click(object sender, EventArgs e)
		{
			SetVisibility(false);
		}

		private void labelTime_DoubleClick(object sender, EventArgs e)
		{
			SetVisibility(true);
		}

		private void cmExit_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void cmTopmost_CheckedChanged(object sender, EventArgs e)
		{
			this.TopMost = cmTopmost.Checked;
		}

		private void cmShowDate_CheckedChanged(object sender, EventArgs e)
		{
			cbShowDate.Checked = cmShowDate.Checked;
		}

		private void cbShowDate_CheckedChanged(object sender, EventArgs e)
		{
			cmShowDate.Checked = cbShowDate.Checked;
		}

		private void cmShoeWeekday_CheckedChanged(object sender, EventArgs e)
		{
			cbShowWeekDay.Checked = cmShoeWeekday.Checked;
		}

		private void cbShowWeekDay_CheckedChanged(object sender, EventArgs e)
		{
			cmShoeWeekday.Checked = cbShowWeekDay.Checked;
		}

		private void notifyIcon_DoubleClick(object sender, EventArgs e)
		{
			if (!this.TopMost)
			{
				this.TopMost = true;
				this.TopMost = false;
			}
		}

		private void cmBackColor_Click(object sender, EventArgs e)
		{
			colorDialog.Color = labelTime.BackColor;
			if (colorDialog.ShowDialog() == DialogResult.OK)
				labelTime.BackColor = colorDialog.Color;
		}

		private void cmForeColor_Click(object sender, EventArgs e)
		{
			colorDialog.Color = labelTime.ForeColor;
			if (colorDialog.ShowDialog() == DialogResult.OK)
				labelTime.ForeColor = colorDialog.Color;
		}

		private void cmLTRailway_Click(object sender, EventArgs e)
		{
			InitCustomLabelFont(Properties.Resources.LTRailway_Regular); 
		}

		private void cmAlarmClock_Click(object sender, EventArgs e)
		{
			InitCustomLabelFont(Properties.Resources.alarm_clock);
		}
	}
}
