namespace wdfxekhach
{
    partial class FRDoiMatKhau
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.txt_xacnhanmk = new System.Windows.Forms.TextBox();
            this.x2 = new System.Windows.Forms.Label();
            this.txt_matkhaumoi = new System.Windows.Forms.TextBox();
            this.x1 = new System.Windows.Forms.Label();
            this.txt_matkhau = new System.Windows.Forms.TextBox();
            this.btn_xacnhan = new System.Windows.Forms.Button();
            this.llbl_Back = new System.Windows.Forms.LinkLabel();
            this.x = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel1.Controls.Add(this.txt_xacnhanmk);
            this.panel1.Controls.Add(this.x2);
            this.panel1.Controls.Add(this.txt_matkhaumoi);
            this.panel1.Controls.Add(this.x1);
            this.panel1.Controls.Add(this.txt_matkhau);
            this.panel1.Controls.Add(this.btn_xacnhan);
            this.panel1.Controls.Add(this.llbl_Back);
            this.panel1.Controls.Add(this.x);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(144, 47);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(512, 527);
            this.panel1.TabIndex = 17;
            // 
            // txt_xacnhanmk
            // 
            this.txt_xacnhanmk.Location = new System.Drawing.Point(111, 352);
            this.txt_xacnhanmk.Multiline = true;
            this.txt_xacnhanmk.Name = "txt_xacnhanmk";
            this.txt_xacnhanmk.PasswordChar = '*';
            this.txt_xacnhanmk.Size = new System.Drawing.Size(299, 45);
            this.txt_xacnhanmk.TabIndex = 20;
            this.txt_xacnhanmk.Leave += new System.EventHandler(this.txt_xacnhanmk_Leave);
            // 
            // x2
            // 
            this.x2.AutoSize = true;
            this.x2.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.x2.Location = new System.Drawing.Point(108, 315);
            this.x2.Name = "x2";
            this.x2.Size = new System.Drawing.Size(192, 23);
            this.x2.TabIndex = 19;
            this.x2.Text = "Xác nhận mật khẩu";
            // 
            // txt_matkhaumoi
            // 
            this.txt_matkhaumoi.Location = new System.Drawing.Point(112, 246);
            this.txt_matkhaumoi.Multiline = true;
            this.txt_matkhaumoi.Name = "txt_matkhaumoi";
            this.txt_matkhaumoi.PasswordChar = '*';
            this.txt_matkhaumoi.Size = new System.Drawing.Size(299, 45);
            this.txt_matkhaumoi.TabIndex = 18;
            this.txt_matkhaumoi.Leave += new System.EventHandler(this.txt_matkhaumoi_Leave);
            // 
            // x1
            // 
            this.x1.AutoSize = true;
            this.x1.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.x1.Location = new System.Drawing.Point(109, 209);
            this.x1.Name = "x1";
            this.x1.Size = new System.Drawing.Size(139, 23);
            this.x1.TabIndex = 17;
            this.x1.Text = "Mật khẩu Mới";
            // 
            // txt_matkhau
            // 
            this.txt_matkhau.Location = new System.Drawing.Point(111, 143);
            this.txt_matkhau.Multiline = true;
            this.txt_matkhau.Name = "txt_matkhau";
            this.txt_matkhau.PasswordChar = '*';
            this.txt_matkhau.Size = new System.Drawing.Size(299, 45);
            this.txt_matkhau.TabIndex = 16;
            this.txt_matkhau.Leave += new System.EventHandler(this.txt_matkhau_Leave);
            // 
            // btn_xacnhan
            // 
            this.btn_xacnhan.BackColor = System.Drawing.Color.LightGreen;
            this.btn_xacnhan.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Magenta;
            this.btn_xacnhan.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btn_xacnhan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_xacnhan.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_xacnhan.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btn_xacnhan.Location = new System.Drawing.Point(141, 437);
            this.btn_xacnhan.Name = "btn_xacnhan";
            this.btn_xacnhan.Size = new System.Drawing.Size(239, 46);
            this.btn_xacnhan.TabIndex = 11;
            this.btn_xacnhan.Text = "Xác Nhận";
            this.btn_xacnhan.UseVisualStyleBackColor = false;
            this.btn_xacnhan.Click += new System.EventHandler(this.btn_xacnhan_Click);
            // 
            // llbl_Back
            // 
            this.llbl_Back.AutoSize = true;
            this.llbl_Back.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.llbl_Back.LinkColor = System.Drawing.Color.Black;
            this.llbl_Back.Location = new System.Drawing.Point(17, 21);
            this.llbl_Back.Name = "llbl_Back";
            this.llbl_Back.Size = new System.Drawing.Size(56, 17);
            this.llbl_Back.TabIndex = 10;
            this.llbl_Back.TabStop = true;
            this.llbl_Back.Text = "Quay lại";
            this.llbl_Back.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llbl_Back_LinkClicked);
            // 
            // x
            // 
            this.x.AutoSize = true;
            this.x.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.x.Location = new System.Drawing.Point(108, 106);
            this.x.Name = "x";
            this.x.Size = new System.Drawing.Size(126, 23);
            this.x.TabIndex = 5;
            this.x.Text = "Mật khẩu cũ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.label1.Location = new System.Drawing.Point(81, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(385, 33);
            this.label1.TabIndex = 8;
            this.label1.Text = "Vui lòng xác nhận thông tin";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // FRDoiMatKhau
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ClientSize = new System.Drawing.Size(800, 614);
            this.Controls.Add(this.panel1);
            this.Name = "FRDoiMatKhau";
            this.Text = "FRDoiMatKhau";
            this.Load += new System.EventHandler(this.FRDoiMatKhau_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txt_xacnhanmk;
        private System.Windows.Forms.Label x2;
        private System.Windows.Forms.TextBox txt_matkhaumoi;
        private System.Windows.Forms.Label x1;
        private System.Windows.Forms.TextBox txt_matkhau;
        private System.Windows.Forms.Button btn_xacnhan;
        private System.Windows.Forms.LinkLabel llbl_Back;
        private System.Windows.Forms.Label x;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}