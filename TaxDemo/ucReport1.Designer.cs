namespace TaxDemo
{
    partial class ucReport1
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblNote2 = new System.Windows.Forms.Label();
            this.lblNote1 = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.c1FlexGrid1 = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1FlexGrid1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblNote2);
            this.groupBox1.Controls.Add(this.lblNote1);
            this.groupBox1.Controls.Add(this.lblTitle);
            this.groupBox1.Controls.Add(this.c1FlexGrid1);
            this.groupBox1.Location = new System.Drawing.Point(35, 23);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(941, 445);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // lblNote2
            // 
            this.lblNote2.AutoSize = true;
            this.lblNote2.Location = new System.Drawing.Point(54, 412);
            this.lblNote2.Name = "lblNote2";
            this.lblNote2.Size = new System.Drawing.Size(209, 12);
            this.lblNote2.TabIndex = 10;
            this.lblNote2.Text = "2. 2012年澳大利亚已纳税额12000美元";
            // 
            // lblNote1
            // 
            this.lblNote1.AutoSize = true;
            this.lblNote1.Location = new System.Drawing.Point(28, 388);
            this.lblNote1.Name = "lblNote1";
            this.lblNote1.Size = new System.Drawing.Size(245, 12);
            this.lblNote1.TabIndex = 9;
            this.lblNote1.Text = "注：1. 2013年6月28日1美元=6.2298元人民币";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle.Location = new System.Drawing.Point(227, 27);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(268, 16);
            this.lblTitle.TabIndex = 8;
            this.lblTitle.Text = "澳大利亚-张三2012-2013年工资单";
            // 
            // c1FlexGrid1
            // 
            this.c1FlexGrid1.ColumnInfo = "7,0,0,0,0,100,Columns:0{AllowSorting:False;Name:\"Month\";AllowDragging:False;Allow" +
    "Resizing:False;AllowEditing:False;Style:\"DataType:System.String;TextAlign:LeftCe" +
    "nter;\";}\t";
            this.c1FlexGrid1.Location = new System.Drawing.Point(28, 63);
            this.c1FlexGrid1.Name = "c1FlexGrid1";
            this.c1FlexGrid1.Rows.Count = 2;
            this.c1FlexGrid1.Rows.DefaultSize = 20;
            this.c1FlexGrid1.Rows.Fixed = 2;
            this.c1FlexGrid1.Size = new System.Drawing.Size(867, 322);
            this.c1FlexGrid1.TabIndex = 7;
            // 
            // ucReport1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "ucReport1";
            this.Size = new System.Drawing.Size(1012, 505);
            this.Load += new System.EventHandler(this.ucReport1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1FlexGrid1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblNote1;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblNote2;
        private C1.Win.C1FlexGrid.C1FlexGrid c1FlexGrid1;
    }
}
