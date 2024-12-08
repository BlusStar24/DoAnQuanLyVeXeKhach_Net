using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace wdfxekhach.Admin
{
    public partial class FRQL_NhanVien : Form
    {
        public FRQL_NhanVien()
        {
            InitializeComponent();
        }

        CONNECT db = new CONNECT();

        public static int MaNhanVien;
        
        private void FRQL_NhanVien_Load(object sender, EventArgs e)
        {
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToResizeColumns = false;
            dataGridView1.ScrollBars = ScrollBars.Both;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.DataSource = db.LayTTNhanVien();


            txt_ten.Text = "";
            txt_email.Text = "";
            txt_sdt.Text = "";
            txt_taikhoan.Text = "";
            txt_matkhau.Text = "";


            btn_them.Enabled = true;
            btn_xoa.Enabled = false;
            btn_luu.Enabled = false;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];

                MaNhanVien = int.Parse(selectedRow.Cells[0].Value.ToString());
                txt_ten.Text = selectedRow.Cells[1].Value.ToString();
                txt_sdt.Text = selectedRow.Cells[2].Value.ToString();
                txt_email.Text = selectedRow.Cells[3].Value.ToString();
                txt_taikhoan.Text = selectedRow.Cells[4].Value.ToString();
                txt_matkhau.Text = selectedRow.Cells[5].Value.ToString();


                btn_xoa.Enabled = true;
                btn_luu.Enabled= true;
                btn_them.Enabled = false;


            }
           
        }

        private void btn_load_Click(object sender, EventArgs e)
        {
            FRQL_NhanVien_Load(sender, e);
        }

        private void btn_them_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_ten.Text))
            {
                errorProvider1.SetError(txt_ten, "Không được bỏ trống");
            }
            else
            {
                errorProvider1.Clear();
                if (string.IsNullOrEmpty(txt_sdt.Text))
                {
                    errorProvider1.SetError(txt_sdt, "Không được bỏ trống");
                }
                else
                {
                    errorProvider1.Clear();
                    if (string.IsNullOrEmpty(txt_email.Text))
                    {
                        errorProvider1.SetError(txt_email, "Không được bỏ trống");
                    }
                    else
                    {
                        errorProvider1.Clear();
                        if (string.IsNullOrEmpty(txt_taikhoan.Text))
                        {
                            errorProvider1.SetError(txt_taikhoan, "không được bỏ trống");
                        }
                        else
                        {
                            errorProvider1.Clear();
                            if (string.IsNullOrEmpty(txt_matkhau.Text))
                            {
                                errorProvider1.SetError(txt_matkhau, "Không được bỏ trống");
                            }
                            else
                            {
                                errorProvider1.Clear();
                                if(db.ThemNhanVien(db.LayMaNV(), txt_ten.Text, txt_email.Text,txt_sdt.Text,txt_taikhoan.Text,txt_matkhau.Text) == 1)
                                {
                                    MessageBox.Show("Thêm thành công");
                                    FRQL_NhanVien_Load(sender, e);
                                }
                                else
                                {
                                    MessageBox.Show("Thêm thất bại");
                                }
                            }
                        }
                    }
                }
            }
        }

        private void btn_xoa_Click(object sender, EventArgs e)
        {
            if(db.XoaNhanVien(MaNhanVien) == 1)
            {
                MessageBox.Show("Xóa thành công");
                FRQL_NhanVien_Load(sender, e);
            }
        }

        private void btn_luu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_ten.Text))
            {
                errorProvider1.SetError(txt_ten, "Không được bỏ trống");
            }
            else
            {
                errorProvider1.Clear();
                if (string.IsNullOrEmpty(txt_sdt.Text))
                {
                    errorProvider1.SetError(txt_sdt, "Không được bỏ trống");
                }
                else
                {
                    errorProvider1.Clear();
                    if (string.IsNullOrEmpty(txt_email.Text))
                    {
                        errorProvider1.SetError(txt_email, "Không được bỏ trống");
                    }
                    else
                    {
                        errorProvider1.Clear();
                        if (string.IsNullOrEmpty(txt_taikhoan.Text))
                        {
                            errorProvider1.SetError(txt_taikhoan, "không được bỏ trống");
                        }
                        else
                        {
                            errorProvider1.Clear();
                            if (string.IsNullOrEmpty(txt_matkhau.Text))
                            {
                                errorProvider1.SetError(txt_matkhau, "Không được bỏ trống");
                            }
                            else
                            {
                                errorProvider1.Clear();
                                if (db.SuaThongTinNhanVien(MaNhanVien, txt_ten.Text, txt_email.Text, txt_sdt.Text, txt_taikhoan.Text, txt_matkhau.Text) == 1)
                                {
                                    MessageBox.Show("Sửa thành công");
                                    FRQL_NhanVien_Load(sender, e);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void btn_timkiem_Click(object sender, EventArgs e)
        {
            string dieukien = " where nv.MaNhanVien = tk.UserID";

            if (!string.IsNullOrEmpty(txt_ten.Text))
            {
                dieukien += $" and TenNhanVien like N'%{txt_ten.Text}%'";
            }

            if (!string.IsNullOrEmpty(txt_sdt.Text))
            {
                if(dieukien != " where nv.MaNhanVien = tk.UserID")
                {
                    dieukien += $" and SDT = '{txt_sdt.Text}'";
                }
                else
                {
                    dieukien += $" and SDT = '{txt_sdt.Text}'";
                }
            }

            if (!string.IsNullOrEmpty(txt_email.Text))
            {
                if (dieukien != " where nv.MaNhanVien = tk.UserID")
                {
                    dieukien += $" and Email = '{txt_email.Text}'";
                }
                else
                {
                    dieukien += $" and Email = '{txt_email.Text}'";
                }
            }

            dataGridView1.DataSource = db.LayTTNhanVien("select nv.*, tk.UserName, tk.Pass from NHANVIEN as nv, TAIKHOAN as tk" + ((dieukien != " where nv.MaNhanVien = tk.UserID") ? dieukien : ""));
        }
    }
}
