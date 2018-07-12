namespace TaxDemo
{
    partial class frmImportSalary
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
            this.components = new System.ComponentModel.Container();
            this.dgvSals = new System.Windows.Forms.DataGridView();
            this.cmsRight = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.删除记录ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtYear = new System.Windows.Forms.TextBox();
            this.cbxNeedCheck = new System.Windows.Forms.CheckBox();
            this.btnImport = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnBrowOther = new System.Windows.Forms.Button();
            this.txtDirOther = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbxOthers = new System.Windows.Forms.ComboBox();
            this.btnBrow = new System.Windows.Forms.Button();
            this.txtDir = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbxAgentCs = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbxAgents = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtYearAnnu = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.cbxRMBDirect = new System.Windows.Forms.CheckBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtYearTitle2 = new System.Windows.Forms.TextBox();
            this.dtDeclare = new System.Windows.Forms.DateTimePicker();
            this.label10 = new System.Windows.Forms.Label();
            this.dtTableOn = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtYearTitle1 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtERate2 = new System.Windows.Forms.TextBox();
            this.lblCurrent = new System.Windows.Forms.Label();
            this.txtERate1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.lblErrMonth = new System.Windows.Forms.Label();
            this.lblErrBonus = new System.Windows.Forms.Label();
            this.lblErrCheck = new System.Windows.Forms.Label();
            this.lblErrNo = new System.Windows.Forms.Label();
            this.lblInfo = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSals)).BeginInit();
            this.cmsRight.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvSals
            // 
            this.dgvSals.AllowUserToAddRows = false;
            this.dgvSals.AllowUserToDeleteRows = false;
            this.dgvSals.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSals.Location = new System.Drawing.Point(35, 272);
            this.dgvSals.Name = "dgvSals";
            this.dgvSals.RowTemplate.ContextMenuStrip = this.cmsRight;
            this.dgvSals.RowTemplate.Height = 23;
            this.dgvSals.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSals.Size = new System.Drawing.Size(1441, 435);
            this.dgvSals.TabIndex = 53;
            this.dgvSals.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSals_CellDoubleClick);
            this.dgvSals.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvSals_CellFormatting);
            this.dgvSals.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvSals_CellMouseDown);
            this.dgvSals.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSals_CellValueChanged);
            this.dgvSals.CurrentCellDirtyStateChanged += new System.EventHandler(this.dgvSals_CurrentCellDirtyStateChanged);
            // 
            // cmsRight
            // 
            this.cmsRight.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem3,
            this.toolStripMenuItem1,
            this.删除记录ToolStripMenuItem,
            this.toolStripMenuItem2});
            this.cmsRight.Name = "cmsRight";
            this.cmsRight.Size = new System.Drawing.Size(149, 76);
            this.cmsRight.Click += new System.EventHandler(this.cmsRight_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(148, 22);
            this.toolStripMenuItem3.Text = "按月均分纳税";
            this.toolStripMenuItem3.Click += new System.EventHandler(this.toolStripMenuItem3_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(148, 22);
            this.toolStripMenuItem1.Text = "查看报表";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // 删除记录ToolStripMenuItem
            // 
            this.删除记录ToolStripMenuItem.Name = "删除记录ToolStripMenuItem";
            this.删除记录ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.删除记录ToolStripMenuItem.Text = "删除记录";
            this.删除记录ToolStripMenuItem.Click += new System.EventHandler(this.删除记录ToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(145, 6);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtYear);
            this.groupBox1.Controls.Add(this.cbxNeedCheck);
            this.groupBox1.Controls.Add(this.btnImport);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.btnAdd);
            this.groupBox1.Controls.Add(this.btnBrowOther);
            this.groupBox1.Controls.Add(this.txtDirOther);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cbxOthers);
            this.groupBox1.Controls.Add(this.btnBrow);
            this.groupBox1.Controls.Add(this.txtDir);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cbxAgentCs);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cbxAgents);
            this.groupBox1.Location = new System.Drawing.Point(35, 99);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(828, 125);
            this.groupBox1.TabIndex = 62;
            this.groupBox1.TabStop = false;
            // 
            // txtYear
            // 
            this.txtYear.Location = new System.Drawing.Point(654, 22);
            this.txtYear.Name = "txtYear";
            this.txtYear.Size = new System.Drawing.Size(123, 21);
            this.txtYear.TabIndex = 10;
            // 
            // cbxNeedCheck
            // 
            this.cbxNeedCheck.AutoSize = true;
            this.cbxNeedCheck.Location = new System.Drawing.Point(541, 58);
            this.cbxNeedCheck.Name = "cbxNeedCheck";
            this.cbxNeedCheck.Size = new System.Drawing.Size(96, 16);
            this.cbxNeedCheck.TabIndex = 14;
            this.cbxNeedCheck.Text = "忽略表头检查";
            this.cbxNeedCheck.UseVisualStyleBackColor = true;
            // 
            // btnImport
            // 
            this.btnImport.Location = new System.Drawing.Point(424, 56);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(104, 23);
            this.btnImport.TabIndex = 13;
            this.btnImport.Text = "确定";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(574, 26);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(53, 12);
            this.label11.TabIndex = 70;
            this.label11.Text = "申报年度";
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(479, 85);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(49, 23);
            this.btnAdd.TabIndex = 18;
            this.btnAdd.Text = "添加";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnBrowOther
            // 
            this.btnBrowOther.Location = new System.Drawing.Point(424, 85);
            this.btnBrowOther.Name = "btnBrowOther";
            this.btnBrowOther.Size = new System.Drawing.Size(49, 23);
            this.btnBrowOther.TabIndex = 17;
            this.btnBrowOther.Text = "...";
            this.btnBrowOther.UseVisualStyleBackColor = true;
            this.btnBrowOther.Click += new System.EventHandler(this.btnBrowOther_Click);
            // 
            // txtDirOther
            // 
            this.txtDirOther.Location = new System.Drawing.Point(240, 88);
            this.txtDirOther.Name = "txtDirOther";
            this.txtDirOther.Size = new System.Drawing.Size(151, 21);
            this.txtDirOther.TabIndex = 16;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(26, 91);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 12);
            this.label4.TabIndex = 66;
            this.label4.Text = "其它数据导入";
            // 
            // cbxOthers
            // 
            this.cbxOthers.FormattingEnabled = true;
            this.cbxOthers.Items.AddRange(new object[] {
            "已纳税额(总额)",
            "已纳税额(按月)"});
            this.cbxOthers.Location = new System.Drawing.Point(113, 88);
            this.cbxOthers.Name = "cbxOthers";
            this.cbxOthers.Size = new System.Drawing.Size(121, 20);
            this.cbxOthers.TabIndex = 15;
            // 
            // btnBrow
            // 
            this.btnBrow.Location = new System.Drawing.Point(342, 54);
            this.btnBrow.Name = "btnBrow";
            this.btnBrow.Size = new System.Drawing.Size(49, 23);
            this.btnBrow.TabIndex = 12;
            this.btnBrow.Text = "...";
            this.btnBrow.UseVisualStyleBackColor = true;
            this.btnBrow.Click += new System.EventHandler(this.btnBrow_Click);
            // 
            // txtDir
            // 
            this.txtDir.Location = new System.Drawing.Point(110, 56);
            this.txtDir.Name = "txtDir";
            this.txtDir.Size = new System.Drawing.Size(218, 21);
            this.txtDir.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(26, 59);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 62;
            this.label3.Text = "文件路径";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(355, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 61;
            this.label2.Text = "国别名称";
            // 
            // cbxAgentCs
            // 
            this.cbxAgentCs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxAgentCs.FormattingEnabled = true;
            this.cbxAgentCs.Location = new System.Drawing.Point(428, 22);
            this.cbxAgentCs.Name = "cbxAgentCs";
            this.cbxAgentCs.Size = new System.Drawing.Size(121, 20);
            this.cbxAgentCs.TabIndex = 9;
            this.cbxAgentCs.SelectedIndexChanged += new System.EventHandler(this.cbxAgentCs_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 58;
            this.label1.Text = "公司名称";
            // 
            // cbxAgents
            // 
            this.cbxAgents.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxAgents.FormattingEnabled = true;
            this.cbxAgents.Location = new System.Drawing.Point(113, 23);
            this.cbxAgents.Name = "cbxAgents";
            this.cbxAgents.Size = new System.Drawing.Size(215, 20);
            this.cbxAgents.TabIndex = 8;
            this.cbxAgents.SelectedIndexChanged += new System.EventHandler(this.cbxAgents_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtYearAnnu);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.cbxRMBDirect);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.txtYearTitle2);
            this.groupBox2.Controls.Add(this.dtDeclare);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.dtTableOn);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txtYearTitle1);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.txtERate2);
            this.groupBox2.Controls.Add(this.lblCurrent);
            this.groupBox2.Controls.Add(this.txtERate1);
            this.groupBox2.Location = new System.Drawing.Point(35, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1047, 81);
            this.groupBox2.TabIndex = 68;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "参数";
            // 
            // txtYearAnnu
            // 
            this.txtYearAnnu.Location = new System.Drawing.Point(899, 46);
            this.txtYearAnnu.Name = "txtYearAnnu";
            this.txtYearAnnu.Size = new System.Drawing.Size(73, 21);
            this.txtYearAnnu.TabIndex = 81;
            this.txtYearAnnu.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(823, 51);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(53, 12);
            this.label12.TabIndex = 80;
            this.label12.Text = "年金限额";
            // 
            // cbxRMBDirect
            // 
            this.cbxRMBDirect.AutoSize = true;
            this.cbxRMBDirect.Location = new System.Drawing.Point(825, 22);
            this.cbxRMBDirect.Name = "cbxRMBDirect";
            this.cbxRMBDirect.Size = new System.Drawing.Size(156, 16);
            this.cbxRMBDirect.TabIndex = 7;
            this.cbxRMBDirect.Text = "脚注汇率直接换算人民币";
            this.cbxRMBDirect.UseVisualStyleBackColor = true;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(24, 51);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(83, 12);
            this.label14.TabIndex = 79;
            this.label14.Text = "报表2年度标识";
            // 
            // txtYearTitle2
            // 
            this.txtYearTitle2.Location = new System.Drawing.Point(118, 47);
            this.txtYearTitle2.Name = "txtYearTitle2";
            this.txtYearTitle2.Size = new System.Drawing.Size(210, 21);
            this.txtYearTitle2.TabIndex = 2;
            // 
            // dtDeclare
            // 
            this.dtDeclare.Location = new System.Drawing.Point(654, 20);
            this.dtDeclare.Name = "dtDeclare";
            this.dtDeclare.Size = new System.Drawing.Size(123, 21);
            this.dtDeclare.TabIndex = 5;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(571, 24);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(65, 12);
            this.label10.TabIndex = 76;
            this.label10.Text = "申报所属月";
            // 
            // dtTableOn
            // 
            this.dtTableOn.Location = new System.Drawing.Point(654, 47);
            this.dtTableOn.Name = "dtTableOn";
            this.dtTableOn.Size = new System.Drawing.Size(123, 21);
            this.dtTableOn.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(571, 51);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 74;
            this.label6.Text = "填表时间";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(508, 51);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(41, 12);
            this.label9.TabIndex = 73;
            this.label9.Text = "人民币";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(508, 24);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 12);
            this.label8.TabIndex = 72;
            this.label8.Text = "美元";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(24, 24);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 12);
            this.label5.TabIndex = 71;
            this.label5.Text = "报表1年度标识";
            // 
            // txtYearTitle1
            // 
            this.txtYearTitle1.Location = new System.Drawing.Point(117, 20);
            this.txtYearTitle1.Name = "txtYearTitle1";
            this.txtYearTitle1.Size = new System.Drawing.Size(211, 21);
            this.txtYearTitle1.TabIndex = 1;
            this.txtYearTitle1.TextChanged += new System.EventHandler(this.txtYearTitle1_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(345, 49);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(47, 12);
            this.label7.TabIndex = 69;
            this.label7.Text = "1美元 =";
            // 
            // txtERate2
            // 
            this.txtERate2.Location = new System.Drawing.Point(429, 46);
            this.txtERate2.Name = "txtERate2";
            this.txtERate2.Size = new System.Drawing.Size(73, 21);
            this.txtERate2.TabIndex = 4;
            this.txtERate2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtERate2.TextChanged += new System.EventHandler(this.txtERate2_TextChanged);
            // 
            // lblCurrent
            // 
            this.lblCurrent.AutoSize = true;
            this.lblCurrent.Location = new System.Drawing.Point(346, 23);
            this.lblCurrent.Name = "lblCurrent";
            this.lblCurrent.Size = new System.Drawing.Size(47, 12);
            this.lblCurrent.TabIndex = 67;
            this.lblCurrent.Text = "1美元 =";
            // 
            // txtERate1
            // 
            this.txtERate1.Location = new System.Drawing.Point(429, 19);
            this.txtERate1.Name = "txtERate1";
            this.txtERate1.Size = new System.Drawing.Size(73, 21);
            this.txtERate1.TabIndex = 3;
            this.txtERate1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtERate1.TextChanged += new System.EventHandler(this.txtERate1_TextChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1328, 216);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 70;
            this.button1.Text = "批量导出";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // lblErrMonth
            // 
            this.lblErrMonth.AutoSize = true;
            this.lblErrMonth.BackColor = System.Drawing.SystemColors.Control;
            this.lblErrMonth.ForeColor = System.Drawing.Color.Black;
            this.lblErrMonth.Location = new System.Drawing.Point(971, 183);
            this.lblErrMonth.Name = "lblErrMonth";
            this.lblErrMonth.Size = new System.Drawing.Size(65, 12);
            this.lblErrMonth.TabIndex = 74;
            this.lblErrMonth.Text = "月份不齐：";
            // 
            // lblErrBonus
            // 
            this.lblErrBonus.AutoSize = true;
            this.lblErrBonus.BackColor = System.Drawing.SystemColors.Control;
            this.lblErrBonus.ForeColor = System.Drawing.Color.Black;
            this.lblErrBonus.Location = new System.Drawing.Point(971, 199);
            this.lblErrBonus.Name = "lblErrBonus";
            this.lblErrBonus.Size = new System.Drawing.Size(89, 12);
            this.lblErrBonus.TabIndex = 75;
            this.lblErrBonus.Text = "年终奖大于一：";
            // 
            // lblErrCheck
            // 
            this.lblErrCheck.AutoSize = true;
            this.lblErrCheck.BackColor = System.Drawing.SystemColors.Control;
            this.lblErrCheck.ForeColor = System.Drawing.Color.Black;
            this.lblErrCheck.Location = new System.Drawing.Point(971, 215);
            this.lblErrCheck.Name = "lblErrCheck";
            this.lblErrCheck.Size = new System.Drawing.Size(65, 12);
            this.lblErrCheck.TabIndex = 76;
            this.lblErrCheck.Text = "验证错误：";
            // 
            // lblErrNo
            // 
            this.lblErrNo.AutoSize = true;
            this.lblErrNo.BackColor = System.Drawing.SystemColors.Control;
            this.lblErrNo.ForeColor = System.Drawing.Color.Black;
            this.lblErrNo.Location = new System.Drawing.Point(971, 166);
            this.lblErrNo.Name = "lblErrNo";
            this.lblErrNo.Size = new System.Drawing.Size(101, 12);
            this.lblErrNo.TabIndex = 78;
            this.lblErrNo.Text = "无已纳税额数据：";
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.BackColor = System.Drawing.SystemColors.Control;
            this.lblInfo.ForeColor = System.Drawing.SystemColors.Desktop;
            this.lblInfo.Location = new System.Drawing.Point(33, 246);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(113, 12);
            this.lblInfo.TabIndex = 79;
            this.lblInfo.Text = "共计**人，**条记录";
            // 
            // frmImportSalary
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1584, 762);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.lblErrNo);
            this.Controls.Add(this.lblErrCheck);
            this.Controls.Add(this.lblErrBonus);
            this.Controls.Add(this.lblErrMonth);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dgvSals);
            this.Name = "frmImportSalary";
            this.Text = "工资类原始信息导入";
            this.Load += new System.EventHandler(this.frmImportSalary_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSals)).EndInit();
            this.cmsRight.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvSals;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnBrow;
        private System.Windows.Forms.TextBox txtDir;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbxAgentCs;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbxAgents;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnBrowOther;
        private System.Windows.Forms.TextBox txtDirOther;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbxOthers;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtYearTitle1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtERate2;
        private System.Windows.Forms.Label lblCurrent;
        private System.Windows.Forms.TextBox txtERate1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.CheckBox cbxNeedCheck;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtYear;
        private System.Windows.Forms.ContextMenuStrip cmsRight;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.DateTimePicker dtTableOn;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DateTimePicker dtDeclare;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblErrMonth;
        private System.Windows.Forms.Label lblErrBonus;
        private System.Windows.Forms.Label lblErrCheck;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtYearTitle2;
        private System.Windows.Forms.Label lblErrNo;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.CheckBox cbxRMBDirect;
        private System.Windows.Forms.ToolStripMenuItem 删除记录ToolStripMenuItem;
        private System.Windows.Forms.TextBox txtYearAnnu;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lblInfo;
    }
}