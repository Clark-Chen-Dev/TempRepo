namespace TaxDemo
{
    partial class frmReportList
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbxEndYear = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbxID = new System.Windows.Forms.ComboBox();
            this.cbxName = new System.Windows.Forms.ComboBox();
            this.cbxStartYear = new System.Windows.Forms.ComboBox();
            this.lblStarYear = new System.Windows.Forms.Label();
            this.cbxAgentCs = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.lblID = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.cbxAgents = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dgvReportList = new System.Windows.Forms.DataGridView();
            this.btnSource = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnMore = new System.Windows.Forms.Button();
            this.btnDel = new System.Windows.Forms.Button();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReportList)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbxEndYear);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cbxID);
            this.groupBox1.Controls.Add(this.cbxName);
            this.groupBox1.Controls.Add(this.cbxStartYear);
            this.groupBox1.Controls.Add(this.lblStarYear);
            this.groupBox1.Controls.Add(this.cbxAgentCs);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnSearch);
            this.groupBox1.Controls.Add(this.lblID);
            this.groupBox1.Controls.Add(this.lblName);
            this.groupBox1.Controls.Add(this.cbxAgents);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(51, 24);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1123, 113);
            this.groupBox1.TabIndex = 38;
            this.groupBox1.TabStop = false;
            // 
            // cbxEndYear
            // 
            this.cbxEndYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxEndYear.FormattingEnabled = true;
            this.cbxEndYear.Location = new System.Drawing.Point(544, 74);
            this.cbxEndYear.Name = "cbxEndYear";
            this.cbxEndYear.Size = new System.Drawing.Size(133, 20);
            this.cbxEndYear.TabIndex = 64;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(485, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 63;
            this.label2.Text = "终止年度";
            // 
            // cbxID
            // 
            this.cbxID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxID.FormattingEnabled = true;
            this.cbxID.Location = new System.Drawing.Point(317, 70);
            this.cbxID.Name = "cbxID";
            this.cbxID.Size = new System.Drawing.Size(133, 20);
            this.cbxID.TabIndex = 62;
            // 
            // cbxName
            // 
            this.cbxName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxName.FormattingEnabled = true;
            this.cbxName.Location = new System.Drawing.Point(89, 70);
            this.cbxName.Name = "cbxName";
            this.cbxName.Size = new System.Drawing.Size(133, 20);
            this.cbxName.TabIndex = 61;
            // 
            // cbxStartYear
            // 
            this.cbxStartYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxStartYear.FormattingEnabled = true;
            this.cbxStartYear.Location = new System.Drawing.Point(544, 31);
            this.cbxStartYear.Name = "cbxStartYear";
            this.cbxStartYear.Size = new System.Drawing.Size(133, 20);
            this.cbxStartYear.TabIndex = 60;
            // 
            // lblStarYear
            // 
            this.lblStarYear.AutoSize = true;
            this.lblStarYear.Location = new System.Drawing.Point(485, 35);
            this.lblStarYear.Name = "lblStarYear";
            this.lblStarYear.Size = new System.Drawing.Size(53, 12);
            this.lblStarYear.TabIndex = 59;
            this.lblStarYear.Text = "起始年度";
            // 
            // cbxAgentCs
            // 
            this.cbxAgentCs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxAgentCs.FormattingEnabled = true;
            this.cbxAgentCs.Location = new System.Drawing.Point(317, 31);
            this.cbxAgentCs.Name = "cbxAgentCs";
            this.cbxAgentCs.Size = new System.Drawing.Size(133, 20);
            this.cbxAgentCs.TabIndex = 40;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(258, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 39;
            this.label1.Text = "所属国别";
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(834, 31);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(110, 55);
            this.btnSearch.TabIndex = 38;
            this.btnSearch.Text = "查询";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // lblID
            // 
            this.lblID.AutoSize = true;
            this.lblID.Location = new System.Drawing.Point(258, 74);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(53, 12);
            this.lblID.TabIndex = 34;
            this.lblID.Text = "身份证号";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(31, 74);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(29, 12);
            this.lblName.TabIndex = 34;
            this.lblName.Text = "姓名";
            // 
            // cbxAgents
            // 
            this.cbxAgents.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxAgents.FormattingEnabled = true;
            this.cbxAgents.Location = new System.Drawing.Point(89, 31);
            this.cbxAgents.Name = "cbxAgents";
            this.cbxAgents.Size = new System.Drawing.Size(133, 20);
            this.cbxAgents.TabIndex = 35;
            this.cbxAgents.SelectedIndexChanged += new System.EventHandler(this.cbxAgents_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(31, 35);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 34;
            this.label4.Text = "所属公司";
            // 
            // dgvReportList
            // 
            this.dgvReportList.AllowUserToAddRows = false;
            this.dgvReportList.AllowUserToDeleteRows = false;
            this.dgvReportList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvReportList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column6,
            this.Column4,
            this.Column5,
            this.Column3,
            this.Column8,
            this.Column9,
            this.Column10,
            this.Column11,
            this.Column2});
            this.dgvReportList.Location = new System.Drawing.Point(51, 162);
            this.dgvReportList.Name = "dgvReportList";
            this.dgvReportList.ReadOnly = true;
            this.dgvReportList.RowTemplate.Height = 23;
            this.dgvReportList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvReportList.Size = new System.Drawing.Size(1123, 427);
            this.dgvReportList.TabIndex = 39;
            // 
            // btnSource
            // 
            this.btnSource.Location = new System.Drawing.Point(906, 611);
            this.btnSource.Name = "btnSource";
            this.btnSource.Size = new System.Drawing.Size(75, 23);
            this.btnSource.TabIndex = 40;
            this.btnSource.Text = "原始数据";
            this.btnSource.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(1099, 611);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 41;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnMore
            // 
            this.btnMore.Location = new System.Drawing.Point(814, 611);
            this.btnMore.Name = "btnMore";
            this.btnMore.Size = new System.Drawing.Size(75, 23);
            this.btnMore.TabIndex = 42;
            this.btnMore.Text = "报表详细";
            this.btnMore.UseVisualStyleBackColor = true;
            this.btnMore.Click += new System.EventHandler(this.btnMore_Click);
            // 
            // btnDel
            // 
            this.btnDel.Location = new System.Drawing.Point(1002, 611);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(75, 23);
            this.btnDel.TabIndex = 43;
            this.btnDel.Text = "删除数据";
            this.btnDel.UseVisualStyleBackColor = true;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "RI_ID";
            this.Column1.HeaderText = "";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Visible = false;
            // 
            // Column6
            // 
            this.Column6.DataPropertyName = "RI_AYNAME";
            this.Column6.HeaderText = "年度";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "AI_NAME";
            this.Column4.HeaderText = "公司";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column5
            // 
            this.Column5.DataPropertyName = "AC_NAME";
            this.Column5.HeaderText = "国别";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "TP_NAME";
            this.Column3.HeaderText = "姓名";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column8
            // 
            this.Column8.DataPropertyName = "RI_SUMTAXSALARY";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Column8.DefaultCellStyle = dataGridViewCellStyle1;
            this.Column8.HeaderText = "应纳税所得额";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            // 
            // Column9
            // 
            this.Column9.DataPropertyName = "RI_SUMTAXRMB";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Column9.DefaultCellStyle = dataGridViewCellStyle2;
            this.Column9.HeaderText = "应纳税额合计";
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            // 
            // Column10
            // 
            this.Column10.DataPropertyName = "RI_SUMTAXALREADYRMB";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Column10.DefaultCellStyle = dataGridViewCellStyle3;
            this.Column10.HeaderText = "已纳税额";
            this.Column10.Name = "Column10";
            this.Column10.ReadOnly = true;
            // 
            // Column11
            // 
            this.Column11.DataPropertyName = "UI_NAME";
            this.Column11.HeaderText = "处理人";
            this.Column11.Name = "Column11";
            this.Column11.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "RI_CREATETIME";
            this.Column2.HeaderText = "生成时间";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // frmReportList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1245, 661);
            this.Controls.Add(this.btnDel);
            this.Controls.Add(this.btnMore);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSource);
            this.Controls.Add(this.dgvReportList);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmReportList";
            this.Text = "报表记录查看";
            this.Load += new System.EventHandler(this.frmReportList_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReportList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cbxStartYear;
        private System.Windows.Forms.Label lblStarYear;
        private System.Windows.Forms.ComboBox cbxAgentCs;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.ComboBox cbxAgents;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dgvReportList;
        private System.Windows.Forms.Button btnSource;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnMore;
        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblID;
        private System.Windows.Forms.ComboBox cbxID;
        private System.Windows.Forms.ComboBox cbxName;
        private System.Windows.Forms.ComboBox cbxEndYear;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
    }
}