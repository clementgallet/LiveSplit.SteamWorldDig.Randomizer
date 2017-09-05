namespace LiveSplit.SteamWorldDig {
	partial class SteamWorldSettings {
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.flowMain = new System.Windows.Forms.FlowLayoutPanel();
            this.flowOptions = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.listDifficulty = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textSeed = new System.Windows.Forms.TextBox();
            this.btnRandomize = new System.Windows.Forms.Button();
            this.textBoxInfo = new System.Windows.Forms.TextBox();
            this.toolTips = new System.Windows.Forms.ToolTip(this.components);
            this.flowMain.SuspendLayout();
            this.flowOptions.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowMain
            // 
            this.flowMain.AutoSize = true;
            this.flowMain.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowMain.Controls.Add(this.flowOptions);
            this.flowMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowMain.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowMain.Location = new System.Drawing.Point(0, 0);
            this.flowMain.Margin = new System.Windows.Forms.Padding(0);
            this.flowMain.Name = "flowMain";
            this.flowMain.Size = new System.Drawing.Size(463, 439);
            this.flowMain.TabIndex = 0;
            this.flowMain.WrapContents = false;
            // 
            // flowOptions
            // 
            this.flowOptions.Controls.Add(this.flowLayoutPanel1);
            this.flowOptions.Controls.Add(this.label1);
            this.flowOptions.Controls.Add(this.textSeed);
            this.flowOptions.Controls.Add(this.btnRandomize);
            this.flowOptions.Controls.Add(this.textBoxInfo);
            this.flowOptions.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowOptions.Location = new System.Drawing.Point(0, 0);
            this.flowOptions.Margin = new System.Windows.Forms.Padding(0);
            this.flowOptions.Name = "flowOptions";
            this.flowOptions.Size = new System.Drawing.Size(463, 439);
            this.flowOptions.TabIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.Controls.Add(this.label2);
            this.flowLayoutPanel1.Controls.Add(this.listDifficulty);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(457, 36);
            this.flowLayoutPanel1.TabIndex = 21;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.label2.Location = new System.Drawing.Point(3, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 21;
            this.label2.Text = "Difficulty:";
            // 
            // listDifficulty
            // 
            this.listDifficulty.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.listDifficulty.FormattingEnabled = true;
            this.listDifficulty.Items.AddRange(new object[] {
            "Speedrunner",
            "Casual"});
            this.listDifficulty.Location = new System.Drawing.Point(59, 3);
            this.listDifficulty.Name = "listDifficulty";
            this.listDifficulty.Size = new System.Drawing.Size(81, 30);
            this.listDifficulty.TabIndex = 20;
            this.listDifficulty.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(226, 13);
            this.label1.TabIndex = 22;
            this.label1.Text = "Seed (leave blank to generate a random seed)";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // textSeed
            // 
            this.textSeed.Location = new System.Drawing.Point(3, 58);
            this.textSeed.Name = "textSeed";
            this.textSeed.Size = new System.Drawing.Size(457, 20);
            this.textSeed.TabIndex = 21;
            // 
            // btnRandomize
            // 
            this.btnRandomize.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnRandomize.Location = new System.Drawing.Point(346, 84);
            this.btnRandomize.Name = "btnRandomize";
            this.btnRandomize.Size = new System.Drawing.Size(114, 23);
            this.btnRandomize.TabIndex = 18;
            this.btnRandomize.Text = "Randomize";
            this.toolTips.SetToolTip(this.btnRandomize, "Randomize items and areas");
            this.btnRandomize.UseVisualStyleBackColor = true;
            this.btnRandomize.Click += new System.EventHandler(this.BtnRandomize_Click);
            // 
            // textBoxInfo
            // 
            this.textBoxInfo.Location = new System.Drawing.Point(3, 113);
            this.textBoxInfo.Multiline = true;
            this.textBoxInfo.Name = "textBoxInfo";
            this.textBoxInfo.ReadOnly = true;
            this.textBoxInfo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxInfo.Size = new System.Drawing.Size(457, 323);
            this.textBoxInfo.TabIndex = 19;
            // 
            // SteamWorldSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.flowMain);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "SteamWorldSettings";
            this.Size = new System.Drawing.Size(463, 439);
            this.flowMain.ResumeLayout(false);
            this.flowOptions.ResumeLayout(false);
            this.flowOptions.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.FlowLayoutPanel flowMain;
		private System.Windows.Forms.FlowLayoutPanel flowOptions;
		private System.Windows.Forms.ToolTip toolTips;
        private System.Windows.Forms.Button btnRandomize;
        private System.Windows.Forms.TextBox textBoxInfo;
        private System.Windows.Forms.ListBox listDifficulty;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textSeed;
        private System.Windows.Forms.Label label2;
    }
}
