namespace TaxDemo
{
    partial class ucReport2
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
            this.lblYearTitle = new System.Windows.Forms.Label();
            this.lblNote1 = new System.Windows.Forms.Label();
            this.lblTitle2 = new System.Windows.Forms.Label();
            this.c1FlexGrid2 = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1FlexGrid2)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblNote2);
            this.groupBox1.Controls.Add(this.lblYearTitle);
            this.groupBox1.Controls.Add(this.lblNote1);
            this.groupBox1.Controls.Add(this.lblTitle2);
            this.groupBox1.Controls.Add(this.c1FlexGrid2);
            this.groupBox1.Location = new System.Drawing.Point(26, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1046, 494);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // lblNote2
            // 
            this.lblNote2.AutoSize = true;
            this.lblNote2.Location = new System.Drawing.Point(83, 465);
            this.lblNote2.Name = "lblNote2";
            this.lblNote2.Size = new System.Drawing.Size(209, 12);
            this.lblNote2.TabIndex = 11;
            this.lblNote2.Text = "2. 2012年澳大利亚已纳税额12000美元";
            // 
            // lblYearTitle
            // 
            this.lblYearTitle.AutoSize = true;
            this.lblYearTitle.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblYearTitle.Location = new System.Drawing.Point(618, 46);
            this.lblYearTitle.Name = "lblYearTitle";
            this.lblYearTitle.Size = new System.Drawing.Size(143, 13);
            this.lblYearTitle.TabIndex = 10;
            this.lblYearTitle.Text = "2012年1月-2012年12月";
            // 
            // lblNote1
            // 
            this.lblNote1.AutoSize = true;
            this.lblNote1.Location = new System.Drawing.Point(59, 442);
            this.lblNote1.Name = "lblNote1";
            this.lblNote1.Size = new System.Drawing.Size(245, 12);
            this.lblNote1.TabIndex = 9;
            this.lblNote1.Text = "注：1. 2013年6月28日1美元=6.2298元人民币";
            // 
            // lblTitle2
            // 
            this.lblTitle2.AutoSize = true;
            this.lblTitle2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle2.Location = new System.Drawing.Point(320, 46);
            this.lblTitle2.Name = "lblTitle2";
            this.lblTitle2.Size = new System.Drawing.Size(146, 16);
            this.lblTitle2.TabIndex = 8;
            this.lblTitle2.Text = "张三--工资-个税 ";
            // 
            // c1FlexGrid2
            // 
            this.c1FlexGrid2.ColumnInfo = "10,0,0,0,0,100,Columns:0{AllowSorting:False;Name:\"Month\";AllowDragging:False;Allo" +
    "wResizing:False;AllowEditing:False;Style:\"DataType:System.String;TextAlign:LeftC" +
    "enter;\";}\t";
            this.c1FlexGrid2.Location = new System.Drawing.Point(26, 75);
            this.c1FlexGrid2.Name = "c1FlexGrid2";
            this.c1FlexGrid2.Rows.Count = 2;
            this.c1FlexGrid2.Rows.DefaultSize = 20;
            this.c1FlexGrid2.Rows.Fixed = 2;
            this.c1FlexGrid2.Size = new System.Drawing.Size(1014, 364);
            this.c1FlexGrid2.TabIndex = 7;
            // 
            // ucReport2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "ucReport2";
            this.Size = new System.Drawing.Size(1095, 505);
            this.Load += new System.EventHandler(this.ucReport2_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1FlexGrid2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblNote1;
        private System.Windows.Forms.Label lblTitle2;
        private C1.Win.C1FlexGrid.C1FlexGrid c1FlexGrid2;
        private System.Windows.Forms.Label lblYearTitle;
        private System.Windows.Forms.Label lblNote2;
    }
}
