namespace wdfxekhach.Admin
{
    partial class FrThongKe
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbx_loaithongke = new System.Windows.Forms.ComboBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.cbx_nhanvien = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cbx_chuyen = new System.Windows.Forms.ComboBox();
            this.cbx_nam = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 22F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(835, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(210, 52);
            this.label1.TabIndex = 0;
            this.label1.Text = "Thống Kê";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(344, 126);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(177, 29);
            this.label2.TabIndex = 1;
            this.label2.Text = "Loại Thống Kê:";
            // 
            // cbx_loaithongke
            // 
            this.cbx_loaithongke.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbx_loaithongke.FormattingEnabled = true;
            this.cbx_loaithongke.Location = new System.Drawing.Point(543, 123);
            this.cbx_loaithongke.Name = "cbx_loaithongke";
            this.cbx_loaithongke.Size = new System.Drawing.Size(411, 37);
            this.cbx_loaithongke.TabIndex = 2;
            this.cbx_loaithongke.SelectedIndexChanged += new System.EventHandler(this.cbx_loaithongke_SelectedIndexChanged);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.Location = new System.Drawing.Point(315, 298);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 62;
            this.dataGridView1.RowTemplate.Height = 28;
            this.dataGridView1.Size = new System.Drawing.Size(1204, 563);
            this.dataGridView1.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label3.Location = new System.Drawing.Point(345, 200);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 29);
            this.label3.TabIndex = 4;
            this.label3.Text = "Năm:";
            // 
            // cbx_nhanvien
            // 
            this.cbx_nhanvien.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.cbx_nhanvien.FormattingEnabled = true;
            this.cbx_nhanvien.Location = new System.Drawing.Point(1139, 118);
            this.cbx_nhanvien.Name = "cbx_nhanvien";
            this.cbx_nhanvien.Size = new System.Drawing.Size(343, 37);
            this.cbx_nhanvien.TabIndex = 6;
            this.cbx_nhanvien.SelectedIndexChanged += new System.EventHandler(this.cbx_nhanvien_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label4.Location = new System.Drawing.Point(994, 126);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(130, 29);
            this.label4.TabIndex = 7;
            this.label4.Text = "Nhân Viên:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label5.Location = new System.Drawing.Point(994, 208);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(137, 29);
            this.label5.TabIndex = 9;
            this.label5.Text = "Chuyến Xe:";
            // 
            // cbx_chuyen
            // 
            this.cbx_chuyen.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.cbx_chuyen.FormattingEnabled = true;
            this.cbx_chuyen.Location = new System.Drawing.Point(1139, 200);
            this.cbx_chuyen.Name = "cbx_chuyen";
            this.cbx_chuyen.Size = new System.Drawing.Size(343, 37);
            this.cbx_chuyen.TabIndex = 8;
            this.cbx_chuyen.SelectedIndexChanged += new System.EventHandler(this.cbx_chuyen_SelectedIndexChanged);
            // 
            // cbx_nam
            // 
            this.cbx_nam.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbx_nam.FormattingEnabled = true;
            this.cbx_nam.Location = new System.Drawing.Point(543, 192);
            this.cbx_nam.Name = "cbx_nam";
            this.cbx_nam.Size = new System.Drawing.Size(214, 37);
            this.cbx_nam.TabIndex = 10;
            this.cbx_nam.SelectedIndexChanged += new System.EventHandler(this.cbx_nam_SelectedIndexChanged);
            // 
            // FrThongKe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1815, 886);
            this.Controls.Add(this.cbx_nam);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cbx_chuyen);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cbx_nhanvien);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.cbx_loaithongke);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "FrThongKe";
            this.Text = "FrThongKe";
            this.Load += new System.EventHandler(this.FrThongKe_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbx_loaithongke;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbx_nhanvien;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbx_chuyen;
        private System.Windows.Forms.ComboBox cbx_nam;
    }
}