using DbAccess;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace POS_Client
{
	public class ViewFailureDialog : Form
	{
		private ViewSchema _view;

		private IContainer components;

		private Label label1;

		private TextBox txtSQL;

		private Button btnCancel;

		private Button btnOK;

		public ViewSchema View
		{
			get
			{
				return _view;
			}
			set
			{
				_view = value;
				Text = "SQL Error: " + _view.ViewName;
				txtSQL.Text = _view.ViewSQL;
			}
		}

		public string ViewSQL
		{
			get
			{
				return txtSQL.Text;
			}
		}

		public ViewFailureDialog()
		{
			InitializeComponent();
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.OK;
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Cancel;
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && components != null)
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			label1 = new System.Windows.Forms.Label();
			txtSQL = new System.Windows.Forms.TextBox();
			btnCancel = new System.Windows.Forms.Button();
			btnOK = new System.Windows.Forms.Button();
			SuspendLayout();
			label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 177);
			label1.ForeColor = System.Drawing.Color.Red;
			label1.Location = new System.Drawing.Point(9, 9);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(491, 30);
			label1.TabIndex = 0;
			label1.Text = "View syntax cannot be transferred automatically to SQLite. Please edit the view definition or press Cancel to discard the view from the generated SQLite database.";
			txtSQL.Location = new System.Drawing.Point(12, 45);
			txtSQL.Multiline = true;
			txtSQL.Name = "txtSQL";
			txtSQL.Size = new System.Drawing.Size(488, 125);
			txtSQL.TabIndex = 1;
			btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			btnCancel.Location = new System.Drawing.Point(425, 181);
			btnCancel.Name = "btnCancel";
			btnCancel.Size = new System.Drawing.Size(75, 23);
			btnCancel.TabIndex = 2;
			btnCancel.Text = "Cancel";
			btnCancel.UseVisualStyleBackColor = true;
			btnCancel.Click += new System.EventHandler(btnCancel_Click);
			btnOK.Location = new System.Drawing.Point(344, 181);
			btnOK.Name = "btnOK";
			btnOK.Size = new System.Drawing.Size(75, 23);
			btnOK.TabIndex = 3;
			btnOK.Text = "OK";
			btnOK.UseVisualStyleBackColor = true;
			btnOK.Click += new System.EventHandler(btnOK_Click);
			base.AcceptButton = btnOK;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = btnCancel;
			base.ClientSize = new System.Drawing.Size(515, 216);
			base.Controls.Add(btnOK);
			base.Controls.Add(btnCancel);
			base.Controls.Add(txtSQL);
			base.Controls.Add(label1);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "ViewFailureDialog";
			base.ShowIcon = false;
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
