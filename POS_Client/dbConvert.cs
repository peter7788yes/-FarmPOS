using DbAccess;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace POS_Client
{
	public class dbConvert : Form
	{
		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass1_0
		{
			public string msg;

			public int percent;

			public bool done;

			public bool success;

			public dbConvert _003C_003E4__this;

			internal void _003CdbConvert_Load_003Eb__1()
			{
				_003C_003E4__this.lblMessage.Text = msg;
				_003C_003E4__this.pbrProgress.Value = percent;
				if (done)
				{
					_003C_003E4__this.Cursor = Cursors.Default;
					if (success)
					{
						MessageBox.Show(_003C_003E4__this, msg, "資料移轉成功", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
						_003C_003E4__this.Close();
						return;
					}
					MessageBox.Show(_003C_003E4__this, msg, "資料移轉失敗", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					_003C_003E4__this.pbrProgress.Value = 0;
					_003C_003E4__this.lblMessage.Text = string.Empty;
					Application.Exit();
				}
			}
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass1_1
		{
			public ViewSchema vs;

			public string updated;

			public dbConvert _003C_003E4__this;

			internal void _003CdbConvert_Load_003Eb__3()
			{
				ViewFailureDialog viewFailureDialog = new ViewFailureDialog();
				viewFailureDialog.View = vs;
				if (viewFailureDialog.ShowDialog(_003C_003E4__this) == DialogResult.OK)
				{
					updated = viewFailureDialog.ViewSQL;
				}
				else
				{
					updated = null;
				}
			}
		}

		private bool _shouldExit;

		private IContainer components;

		private ProgressBar pbrProgress;

		private Label lblMessage;

		public dbConvert()
		{
			InitializeComponent();
		}

		private void dbConvert_Load(object sender, EventArgs e)
		{
			string text = "";
			string appSettings = ConfigOperation.GetAppSettings("OLD_POS_DATABASE_NAME");
			text = ((!bool.Parse(ConfigOperation.GetAppSettings("OLD_POS_DATABASE_SSPI"))) ? string.Format("Data Source=(local)\\SQLExpress;Initial Catalog={0};User ID=sa;Password=1031", appSettings) : string.Format("Data Source=(local)\\SQLExpress;Initial Catalog={0};Integrated Security=SSPI;", appSettings));
			string sqlitePath = Program.DataPath + "\\Old_db.db3";
			Cursor = Cursors.WaitCursor;
			SqlConversionHandler handler = new SqlConversionHandler(_003CdbConvert_Load_003Eb__1_0);
			SqlTableSelectionHandler selectionHandler = null;
			FailedViewDefinitionHandler viewFailureHandler = new FailedViewDefinitionHandler(_003CdbConvert_Load_003Eb__1_2);
			string password = "1031";
			bool createViews = false;
			bool createTriggers = false;
			SqlServerToSQLite.ConvertSqlServerToSQLiteDatabase(text, sqlitePath, password, handler, selectionHandler, viewFailureHandler, createTriggers, createViews);
		}

		private void dbConvert_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (SqlServerToSQLite.IsActive)
			{
				SqlServerToSQLite.CancelConversion();
				_shouldExit = true;
				e.Cancel = true;
			}
			else
			{
				e.Cancel = false;
			}
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
			pbrProgress = new System.Windows.Forms.ProgressBar();
			lblMessage = new System.Windows.Forms.Label();
			SuspendLayout();
			pbrProgress.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			pbrProgress.Location = new System.Drawing.Point(14, 75);
			pbrProgress.Name = "pbrProgress";
			pbrProgress.Size = new System.Drawing.Size(506, 30);
			pbrProgress.TabIndex = 16;
			lblMessage.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			lblMessage.Location = new System.Drawing.Point(12, 18);
			lblMessage.Name = "lblMessage";
			lblMessage.Size = new System.Drawing.Size(508, 43);
			lblMessage.TabIndex = 15;
			lblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(540, 128);
			base.Controls.Add(lblMessage);
			base.Controls.Add(pbrProgress);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.Name = "MainForm";
			Text = "防檢局資料移轉";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(dbConvert_FormClosing);
			base.Load += new System.EventHandler(dbConvert_Load);
			ResumeLayout(false);
		}

		[CompilerGenerated]
		private void _003CdbConvert_Load_003Eb__1_0(bool done, bool success, int percent, string msg)
		{
			_003C_003Ec__DisplayClass1_0 _003C_003Ec__DisplayClass1_ = new _003C_003Ec__DisplayClass1_0();
			_003C_003Ec__DisplayClass1_._003C_003E4__this = this;
			_003C_003Ec__DisplayClass1_.msg = msg;
			_003C_003Ec__DisplayClass1_.percent = percent;
			_003C_003Ec__DisplayClass1_.done = done;
			_003C_003Ec__DisplayClass1_.success = success;
			Invoke(new MethodInvoker(_003C_003Ec__DisplayClass1_._003CdbConvert_Load_003Eb__1));
		}

		[CompilerGenerated]
		private string _003CdbConvert_Load_003Eb__1_2(ViewSchema vs)
		{
			_003C_003Ec__DisplayClass1_1 _003C_003Ec__DisplayClass1_ = new _003C_003Ec__DisplayClass1_1();
			_003C_003Ec__DisplayClass1_._003C_003E4__this = this;
			_003C_003Ec__DisplayClass1_.vs = vs;
			_003C_003Ec__DisplayClass1_.updated = null;
			Invoke(new MethodInvoker(_003C_003Ec__DisplayClass1_._003CdbConvert_Load_003Eb__3));
			return _003C_003Ec__DisplayClass1_.updated;
		}
	}
}
