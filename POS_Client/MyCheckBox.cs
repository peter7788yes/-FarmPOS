using System.Drawing;
using System.Windows.Forms;

namespace POS_Client
{
	internal class MyCheckBox : CheckBox
	{
		public override bool AutoSize
		{
			get
			{
				return base.AutoSize;
			}
			set
			{
				base.AutoSize = false;
			}
		}

		public MyCheckBox()
		{
			TextAlign = ContentAlignment.MiddleRight;
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			int num = base.ClientSize.Height - 2;
			Rectangle rectangle = new Rectangle(new Point(0, 1), new Size(num, num));
			ControlPaint.DrawCheckBox(e.Graphics, rectangle, base.Checked ? ButtonState.Checked : ButtonState.Normal);
		}
	}
}
