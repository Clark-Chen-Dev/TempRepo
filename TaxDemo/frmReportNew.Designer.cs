namespace TaxDemo
{
    partial class frmReportNew
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.ucReport11 = new TaxDemo.ucReport1();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnCanUse2 = new System.Windows.Forms.Button();
            this.txtCanUse2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnTaxCredit = new System.Windows.Forms.Button();
            this.txtCanUse = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ucReport21 = new TaxDemo.ucReport2();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.ucReport31 = new TaxDemo.ucReport3();
            this.lblReviewer = new System.Windows.Forms.Label();
            this.lblScheduler = new System.Windows.Forms.Label();
            this.txtReviewer = new System.Windows.Forms.TextBox();
            this.txtScheduler = new System.Windows.Forms.TextBox();
            this.btnPrint = new System.Windows.Forms.Button();
            this.cbxOpen = new System.Windows.Forms.CheckBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnEm = new System.Windows.Forms.Button();
            this.fbdPath = new System.Windows.Forms.FolderBrowserDialog();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1248, 599);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.ucReport11);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1240, 573);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "报表一";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // ucReport11
            // 
            this.ucReport11.Location = new System.Drawing.Point(54, 48);
            this.ucReport11.Name = "ucReport11";
            this.ucReport11.Size = new System.Drawing.Size(982, 505);
            this.ucReport11.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btnCanUse2);
            this.tabPage2.Controls.Add(this.txtCanUse2);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.btnAdd);
            this.tabPage2.Controls.Add(this.btnTaxCredit);
            this.tabPage2.Controls.Add(this.txtCanUse);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.ucReport21);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1240, 573);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "报表二";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnCanUse2
            // 
            this.btnCanUse2.Location = new System.Drawing.Point(286, 16);
            this.btnCanUse2.Name = "btnCanUse2";
            this.btnCanUse2.Size = new System.Drawing.Size(75, 23);
            this.btnCanUse2.TabIndex = 7;
            this.btnCanUse2.Text = "确定";
            this.btnCanUse2.UseVisualStyleBackColor = true;
            this.btnCanUse2.Click += new System.EventHandler(this.btnCanUse2_Click);
            // 
            // txtCanUse2
            // 
            this.txtCanUse2.Location = new System.Drawing.Point(185, 17);
            this.txtCanUse2.Name = "txtCanUse2";
            this.txtCanUse2.Size = new System.Drawing.Size(84, 21);
            this.txtCanUse2.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(67, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "其它可用已纳税额";
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(705, 16);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 4;
            this.btnAdd.Text = "确定";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnTaxCredit
            // 
            this.btnTaxCredit.Location = new System.Drawing.Point(794, 16);
            this.btnTaxCredit.Name = "btnTaxCredit";
            this.btnTaxCredit.Size = new System.Drawing.Size(145, 23);
            this.btnTaxCredit.TabIndex = 3;
            this.btnTaxCredit.Text = "过往5年可抵扣已纳税";
            this.btnTaxCredit.UseVisualStyleBackColor = true;
            this.btnTaxCredit.Click += new System.EventHandler(this.btnTaxCredit_Click);
            // 
            // txtCanUse
            // 
            this.txtCanUse.Location = new System.Drawing.Point(617, 17);
            this.txtCanUse.Name = "txtCanUse";
            this.txtCanUse.Size = new System.Drawing.Size(76, 21);
            this.txtCanUse.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(468, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(143, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "输入过往5年可用已纳税额";
            // 
            // ucReport21
            // 
            this.ucReport21.Location = new System.Drawing.Point(16, 55);
            this.ucReport21.Name = "ucReport21";
            this.ucReport21.Size = new System.Drawing.Size(1213, 512);
            this.ucReport21.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.ucReport31);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(1240, 573);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "报表三";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // ucReport31
            // 
            this.ucReport31.ExternalTotal = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.ucReport31.InternalTotal = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.ucReport31.Location = new System.Drawing.Point(94, 34);
            this.ucReport31.Name = "ucReport31";
            this.ucReport31.Size = new System.Drawing.Size(758, 426);
            this.ucReport31.TabIndex = 0;
            // 
            // lblReviewer
            // 
            this.lblReviewer.AutoSize = true;
            this.lblReviewer.Location = new System.Drawing.Point(266, 634);
            this.lblReviewer.Name = "lblReviewer";
            this.lblReviewer.Size = new System.Drawing.Size(41, 12);
            this.lblReviewer.TabIndex = 2;
            this.lblReviewer.Text = "复核人";
            // 
            // lblScheduler
            // 
            this.lblScheduler.AutoSize = true;
            this.lblScheduler.Location = new System.Drawing.Point(109, 634);
            this.lblScheduler.Name = "lblScheduler";
            this.lblScheduler.Size = new System.Drawing.Size(41, 12);
            this.lblScheduler.TabIndex = 2;
            this.lblScheduler.Text = "编制人";
            // 
            // txtReviewer
            // 
            this.txtReviewer.Location = new System.Drawing.Point(313, 630);
            this.txtReviewer.Name = "txtReviewer";
            this.txtReviewer.Size = new System.Drawing.Size(100, 21);
            this.txtReviewer.TabIndex = 1;
            this.txtReviewer.Text = "罗英";
            // 
            // txtScheduler
            // 
            this.txtScheduler.Location = new System.Drawing.Point(156, 630);
            this.txtScheduler.Name = "txtScheduler";
            this.txtScheduler.Size = new System.Drawing.Size(100, 21);
            this.txtScheduler.TabIndex = 1;
            this.txtScheduler.Text = "邵赟婕";
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(868, 629);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(75, 23);
            this.btnPrint.TabIndex = 14;
            this.btnPrint.Text = "打印";
            this.btnPrint.UseVisualStyleBackColor = true;
            // 
            // cbxOpen
            // 
            this.cbxOpen.AutoSize = true;
            this.cbxOpen.Checked = true;
            this.cbxOpen.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbxOpen.Location = new System.Drawing.Point(527, 632);
            this.cbxOpen.Name = "cbxOpen";
            this.cbxOpen.Size = new System.Drawing.Size(120, 16);
            this.cbxOpen.TabIndex = 13;
            this.cbxOpen.Text = "导出后打开文件夹";
            this.cbxOpen.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(967, 629);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 12;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(766, 629);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 11;
            this.btnOK.Text = "入库";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnEm
            // 
            this.btnEm.Location = new System.Drawing.Point(669, 629);
            this.btnEm.Name = "btnEm";
            this.btnEm.Size = new System.Drawing.Size(75, 23);
            this.btnEm.TabIndex = 10;
            this.btnEm.Text = "导出";
            this.btnEm.UseVisualStyleBackColor = true;
            this.btnEm.Click += new System.EventHandler(this.btnEm_Click);
            // 
            // frmReportNew
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1257, 670);
            this.Controls.Add(this.lblReviewer);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.lblScheduler);
            this.Controls.Add(this.cbxOpen);
            this.Controls.Add(this.txtReviewer);
            this.Controls.Add(this.txtScheduler);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnEm);
            this.Controls.Add(this.tabControl1);
            this.Name = "frmReportNew";
            this.Text = "报表详细";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmReportNew_FormClosing);
            this.Load += new System.EventHandler(this.frmReportNew_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private ucReport1 ucReport11;
        private ucReport2 ucReport21;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.CheckBox cbxOpen;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnEm;
        private System.Windows.Forms.FolderBrowserDialog fbdPath;
        private System.Windows.Forms.TabPage tabPage3;
        private ucReport3 ucReport31;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnTaxCredit;
        private System.Windows.Forms.TextBox txtCanUse;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCanUse2;
        private System.Windows.Forms.TextBox txtCanUse2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblReviewer;
        private System.Windows.Forms.Label lblScheduler;
        private System.Windows.Forms.TextBox txtReviewer;
        private System.Windows.Forms.TextBox txtScheduler;
    }
}