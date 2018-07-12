namespace TaxDemo
{
    partial class ucReport3
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
            this.lblMsg = new System.Windows.Forms.Label();
            this.c1FlexGrid3 = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1FlexGrid3)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.c1FlexGrid3);
            this.groupBox1.Location = new System.Drawing.Point(25, 39);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(785, 393);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // lblMsg
            // 
            this.lblMsg.AutoSize = true;
            this.lblMsg.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblMsg.ForeColor = System.Drawing.Color.Red;
            this.lblMsg.Location = new System.Drawing.Point(59, 24);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(195, 14);
            this.lblMsg.TabIndex = 2;
            this.lblMsg.Text = "*此纳税人不符合报表三条件";
            this.lblMsg.Visible = false;
            // 
            // c1FlexGrid3
            // 
            this.c1FlexGrid3.ColumnInfo = "7,0,0,0,0,100,Columns:0{AllowSorting:False;Name:\"Month\";AllowDragging:False;Allow" +
    "Resizing:False;AllowEditing:False;Style:\"DataType:System.String;TextAlign:LeftCe" +
    "nter;\";}\t";
            this.c1FlexGrid3.Location = new System.Drawing.Point(23, 31);
            this.c1FlexGrid3.Name = "c1FlexGrid3";
            this.c1FlexGrid3.Rows.Count = 1;
            this.c1FlexGrid3.Rows.DefaultSize = 20;
            this.c1FlexGrid3.Size = new System.Drawing.Size(732, 343);
            this.c1FlexGrid3.TabIndex = 8;
            // 
            // ucReport3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblMsg);
            this.Controls.Add(this.groupBox1);
            this.Name = "ucReport3";
            this.Size = new System.Drawing.Size(899, 446);
            this.Load += new System.EventHandler(this.ucReport3_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1FlexGrid3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblMsg;
        private C1.Win.C1FlexGrid.C1FlexGrid c1FlexGrid3;
    }
}
