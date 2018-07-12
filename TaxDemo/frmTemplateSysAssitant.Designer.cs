namespace TaxDemo
{
    partial class frmTemplateSysAssitant
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
            this.gbx2 = new System.Windows.Forms.GroupBox();
            this.lbxAlias = new System.Windows.Forms.ListBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.txtTitleAlias = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.gbx2.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbx2
            // 
            this.gbx2.Controls.Add(this.lbxAlias);
            this.gbx2.Controls.Add(this.btnAdd);
            this.gbx2.Controls.Add(this.txtTitleAlias);
            this.gbx2.Controls.Add(this.label5);
            this.gbx2.Location = new System.Drawing.Point(68, 35);
            this.gbx2.Name = "gbx2";
            this.gbx2.Size = new System.Drawing.Size(395, 333);
            this.gbx2.TabIndex = 24;
            this.gbx2.TabStop = false;
            this.gbx2.Text = "可用表头";
            // 
            // lbxAlias
            // 
            this.lbxAlias.FormattingEnabled = true;
            this.lbxAlias.ItemHeight = 12;
            this.lbxAlias.Location = new System.Drawing.Point(78, 76);
            this.lbxAlias.Name = "lbxAlias";
            this.lbxAlias.Size = new System.Drawing.Size(229, 196);
            this.lbxAlias.TabIndex = 3;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(224, 29);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(83, 23);
            this.btnAdd.TabIndex = 2;
            this.btnAdd.Text = "添加";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // txtTitleAlias
            // 
            this.txtTitleAlias.Location = new System.Drawing.Point(78, 29);
            this.txtTitleAlias.Name = "txtTitleAlias";
            this.txtTitleAlias.Size = new System.Drawing.Size(137, 21);
            this.txtTitleAlias.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 36);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 36;
            this.label5.Text = "可用表头";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(222, 398);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // frmTemplateSysAssitant
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(547, 444);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.gbx2);
            this.Name = "frmTemplateSysAssitant";
            this.Text = "frmTemplateSysAssitant";
            this.Load += new System.EventHandler(this.frmTemplateSysAssitant_Load);
            this.gbx2.ResumeLayout(false);
            this.gbx2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbx2;
        private System.Windows.Forms.ListBox lbxAlias;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.TextBox txtTitleAlias;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnCancel;
    }
}