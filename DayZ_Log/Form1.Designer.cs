namespace DayZ_Log
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            btnScan = new Button();
            label1 = new Label();
            button1 = new Button();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            splitContainer1 = new SplitContainer();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            SuspendLayout();
            // 
            // btnScan
            // 
            btnScan.BackColor = SystemColors.Control;
            btnScan.Location = new Point(13, 68);
            btnScan.Name = "btnScan";
            btnScan.Size = new Size(75, 23);
            btnScan.TabIndex = 0;
            btnScan.Text = "Scan Logs";
            btnScan.UseVisualStyleBackColor = false;
            btnScan.Click += btnScan_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = SystemColors.ActiveBorder;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label1.Location = new Point(9, 15);
            label1.Name = "label1";
            label1.Size = new Size(156, 15);
            label1.TabIndex = 1;
            label1.Text = "DayZ log scanner tool 1.0.3";
            label1.Click += label1_Click;
            // 
            // button1
            // 
            button1.BackColor = SystemColors.ButtonFace;
            button1.Location = new Point(13, 135);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 2;
            button1.Text = "Delete";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = SystemColors.ActiveBorder;
            label2.Location = new Point(13, 50);
            label2.Name = "label2";
            label2.Size = new Size(239, 15);
            label2.TabIndex = 3;
            label2.Text = "Scan log files and output errors and crashes:";
            label2.Click += label2_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = SystemColors.ActiveBorder;
            label3.Location = new Point(13, 117);
            label3.Name = "label3";
            label3.Size = new Size(180, 15);
            label3.TabIndex = 4;
            label3.Text = "Delete log files from DayZ folder:";
            label3.Click += label3_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(3, 23);
            label4.Name = "label4";
            label4.Size = new Size(104, 15);
            label4.TabIndex = 5;
            label4.Text = "No file info found.";
            label4.Click += label4_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label5.Location = new Point(3, 6);
            label5.Name = "label5";
            label5.Size = new Size(120, 15);
            label5.TabIndex = 6;
            label5.Text = "Log file information:";
            label5.Click += label5_Click;
            // 
            // splitContainer1
            // 
            splitContainer1.Location = new Point(4, 9);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.BackColor = SystemColors.ActiveBorder;
            splitContainer1.Panel1.Paint += splitContainer1_Panel1_Paint;
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.BackColor = SystemColors.ButtonFace;
            splitContainer1.Panel2.Controls.Add(label5);
            splitContainer1.Panel2.Controls.Add(label4);
            splitContainer1.Panel2.Paint += splitContainer1_Panel2_Paint;
            splitContainer1.Size = new Size(463, 163);
            splitContainer1.SplitterDistance = 260;
            splitContainer1.TabIndex = 7;
            // 
            // MainForm
            // 
            BackColor = SystemColors.ActiveBorder;
            ClientSize = new Size(472, 177);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(button1);
            Controls.Add(label1);
            Controls.Add(btnScan);
            Controls.Add(splitContainer1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "MainForm";
            Load += MainForm_Load;
            splitContainer1.Panel2.ResumeLayout(false);
            splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.Button btnScan;
        private Label label1;
        private Button button1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private SplitContainer splitContainer1;
    }
}
