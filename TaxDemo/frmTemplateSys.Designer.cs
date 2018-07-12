namespace TaxDemo
{
    partial class frmTemplateSys
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
            this.btnDel = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.dgvCols = new System.Windows.Forms.DataGridView();
            this.btnEdit = new System.Windows.Forms.Button();
            this.gbx1 = new System.Windows.Forms.GroupBox();
            this.txtColName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtMark = new System.Windows.Forms.TextBox();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.cbxType = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.默认表头 = new System.Windows.Forms.Label();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCols)).BeginInit();
            this.gbx1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnDel
            // 
            this.btnDel.Location = new System.Drawing.Point(444, 509);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(75, 23);
            this.btnDel.TabIndex = 8;
            this.btnDel.Text = "删除";
            this.btnDel.UseVisualStyleBackColor = true;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(550, 509);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // dgvCols
            // 
            this.dgvCols.AllowUserToAddRows = false;
            this.dgvCols.AllowUserToDeleteRows = false;
            this.dgvCols.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCols.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column5,
            this.Column1,
            this.Column2,
            this.Column4,
            this.Column6,
            this.Column7,
            this.Column3});
            this.dgvCols.Location = new System.Drawing.Point(46, 141);
            this.dgvCols.Name = "dgvCols";
            this.dgvCols.ReadOnly = true;
            this.dgvCols.RowTemplate.Height = 23;
            this.dgvCols.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCols.Size = new System.Drawing.Size(752, 345);
            this.dgvCols.TabIndex = 6;
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(313, 509);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(98, 23);
            this.btnEdit.TabIndex = 7;
            this.btnEdit.Text = "编辑可用表头";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // gbx1
            // 
            this.gbx1.Controls.Add(this.txtColName);
            this.gbx1.Controls.Add(this.label1);
            this.gbx1.Controls.Add(this.txtMark);
            this.gbx1.Controls.Add(this.btnUpdate);
            this.gbx1.Controls.Add(this.label4);
            this.gbx1.Controls.Add(this.cbxType);
            this.gbx1.Controls.Add(this.label3);
            this.gbx1.Controls.Add(this.txtName);
            this.gbx1.Controls.Add(this.默认表头);
            this.gbx1.Location = new System.Drawing.Point(46, 12);
            this.gbx1.Name = "gbx1";
            this.gbx1.Size = new System.Drawing.Size(663, 103);
            this.gbx1.TabIndex = 27;
            this.gbx1.TabStop = false;
            this.gbx1.Text = "基本项";
            // 
            // txtColName
            // 
            this.txtColName.Location = new System.Drawing.Point(390, 21);
            this.txtColName.Name = "txtColName";
            this.txtColName.Size = new System.Drawing.Size(153, 21);
            this.txtColName.TabIndex = 2;
            this.txtColName.Text = "SI_";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(320, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 24;
            this.label1.Text = "英文标识";
            // 
            // txtMark
            // 
            this.txtMark.Location = new System.Drawing.Point(389, 61);
            this.txtMark.Name = "txtMark";
            this.txtMark.Size = new System.Drawing.Size(154, 21);
            this.txtMark.TabIndex = 4;
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(565, 58);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 23);
            this.btnUpdate.TabIndex = 5;
            this.btnUpdate.Text = "新增";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(325, 61);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 21;
            this.label4.Text = "备注";
            // 
            // cbxType
            // 
            this.cbxType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxType.FormattingEnabled = true;
            this.cbxType.Items.AddRange(new object[] {
            "数字型",
            "文字型"});
            this.cbxType.Location = new System.Drawing.Point(94, 61);
            this.cbxType.Name = "cbxType";
            this.cbxType.Size = new System.Drawing.Size(171, 20);
            this.cbxType.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(28, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 19;
            this.label3.Text = "数据类型";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(94, 21);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(171, 21);
            this.txtName.TabIndex = 1;
            // 
            // 默认表头
            // 
            this.默认表头.AutoSize = true;
            this.默认表头.Location = new System.Drawing.Point(30, 28);
            this.默认表头.Name = "默认表头";
            this.默认表头.Size = new System.Drawing.Size(41, 12);
            this.默认表头.TabIndex = 13;
            this.默认表头.Text = "表项名";
            // 
            // Column5
            // 
            this.Column5.DataPropertyName = "CI_ID";
            this.Column5.HeaderText = "CI_ID";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Visible = false;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "CI_NAME";
            this.Column1.HeaderText = "表项名";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 150;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "CI_COLNAME";
            this.Column2.HeaderText = "英文标识";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "CT_NAME";
            this.Column4.HeaderText = "数据类型";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // Column6
            // 
            this.Column6.DataPropertyName = "CI_NAME";
            this.Column6.HeaderText = "默认表头";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.Width = 150;
            // 
            // Column7
            // 
            this.Column7.DataPropertyName = "CI_ALIAS";
            this.Column7.HeaderText = "可用表头";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "CI_MARK";
            this.Column3.HeaderText = "备注";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // frmTemplateSys
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(810, 563);
            this.Controls.Add(this.gbx1);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.dgvCols);
            this.Controls.Add(this.btnDel);
            this.Controls.Add(this.btnCancel);
            this.Name = "frmTemplateSys";
            this.Text = "系统原始表项";
            this.Load += new System.EventHandler(this.frmTemplateSys_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCols)).EndInit();
            this.gbx1.ResumeLayout(false);
            this.gbx1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.DataGridView dgvCols;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.GroupBox gbx1;
        private System.Windows.Forms.TextBox txtColName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtMark;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbxType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label 默认表头;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;

    }
}