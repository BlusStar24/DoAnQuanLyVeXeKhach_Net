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
    public partial class FRQLLoaiXe : Form
    {
        CONNECT db = new CONNECT();
        public FRQLLoaiXe()
        {
            InitializeComponent();
        }

        public static int MaLX;
        private void FRQLLoaiXe_Load(object sender, EventArgs e)
        {
            
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToResizeColumns = false;
            dataGridView1.ScrollBars = ScrollBars.Both;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.DataSource = db.LayLoaiXe();

            dataGridView1.Columns[0].HeaderText = "Mã Xe";
            dataGridView1.Columns[1].HeaderText = "Tên Loại Xe";
            dataGridView1.Columns[2].HeaderText = "Sức Chứa";
            dataGridView1.Columns[3].HeaderText = "Loại Ghế";

            txt_tenlx.Text = "";
            txt_succhua.Text = "";
            txt_loaighe.Text = "";

            btn_xoa.Enabled = false;
            btn_luu.Enabled = false;
            btn_them.Enabled = true;
        }

    

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult r = MessageBox.Show("Xác nhận xóa", "Thông báo", MessageBoxButtons.YesNo);
            if (r == DialogResult.Yes)
            {
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    db.XoaLoaiXe(int.Parse(row.Cells["MaLoaiXe"].Value.ToString()));
                }
                FRQLLoaiXe_Load(sender, e);
            }
        }

        

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];

                MaLX = int.Parse(selectedRow.Cells[0].Value.ToString());
                txt_tenlx.Text = selectedRow.Cells[1].Value.ToString();
                txt_succhua.Text = selectedRow.Cells[2].Value.ToString();
                txt_loaighe.Text = selectedRow.Cells[3].Value.ToString();

                btn_xoa.Enabled = true;
                btn_luu.Enabled = true;
                btn_them.Enabled = false;
            }
        }

        private void btn_them_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_tenlx.Text))
            {
                errorProvider1.SetError(txt_tenlx, "Không được bỏ trống");

            }
            else
            {
                errorProvider1.Clear();
                if (string.IsNullOrEmpty(txt_succhua.Text))
                {
                    errorProvider1.SetError(txt_succhua, "Không được bỏ trống");
                }
                else
                {
                    errorProvider1.Clear();
                    if (string.IsNullOrEmpty(txt_loaighe.Text))
                    {
                        errorProvider1.SetError(txt_loaighe, "Không được bỏ trống");
                    }
                    else
                    {
                        errorProvider1.Clear();
                        if (db.ThemLoaiXe(db.LayMaLX(), txt_tenlx.Text, int.Parse(txt_succhua.Text), txt_loaighe.Text) != 0)
                        {
                            MessageBox.Show("Thêm Thành Công");
                            FRQLLoaiXe_Load(sender, e);
                        }
                        else
                        {
                            MessageBox.Show("Thêm thất bại");
                        }
                    }
                }
            }
        }

        private void btn_xoa_Click(object sender, EventArgs e)
        {
            if(db.XoaLoaiXe(MaLX) == 1)
            {
                MessageBox.Show("Xóa thành công");
                FRQLLoaiXe_Load(sender, e);
            }
        }

        private void btn_luu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_tenlx.Text))
            {
                errorProvider1.SetError(txt_tenlx, "Không được bỏ trống");

            }
            else
            {
                errorProvider1.Clear();
                if (string.IsNullOrEmpty(txt_succhua.Text))
                {
                    errorProvider1.SetError(txt_succhua, "Không được bỏ trống");
                }
                else
                {
                    errorProvider1.Clear();
                    if (string.IsNullOrEmpty(txt_loaighe.Text))
                    {
                        errorProvider1.SetError(txt_loaighe, "Không được bỏ trống");
                    }
                    else
                    {
                        errorProvider1.Clear();
                        if (db.SuaLoaiXe(MaLX, txt_tenlx.Text, int.Parse(txt_succhua.Text), txt_loaighe.Text) != 0)
                        {
                            MessageBox.Show("Sửa thành công");
                            FRQLLoaiXe_Load(sender, e);
                        }
                        else
                        {
                            MessageBox.Show("Sửa thất bại");
                        }
                    }
                }
            }
        }

        private void btn_load_Click(object sender, EventArgs e)
        {
            FRQLLoaiXe_Load(sender, e);
        }

        private void btn_timkiem_Click(object sender, EventArgs e)
        {
            string dieukien = " where";
            string caulenh = "select * from LOAIXE";
            if (!string.IsNullOrEmpty(txt_tenlx.Text))
            {
                dieukien += $" TenLoaiXe like N'%{txt_tenlx.Text}%'";
            }

            if (!string.IsNullOrEmpty(txt_succhua.Text))
            {
                if (dieukien == " where")
                {
                    dieukien += $" SucChuaXe = {txt_succhua.Text}";
                }
                else
                {
                    dieukien += $" and SucChuaXe = {txt_succhua.Text}";
                }
            }

            if (!string.IsNullOrEmpty(txt_loaighe.Text))
            {
                if (dieukien == " where")
                {
                    dieukien += $" LoaiGhe = '{txt_loaighe.Text}'";
                }
                else
                {
                    dieukien += $" and LoaiGhe = '{txt_loaighe.Text}'";
                }
            }            

            dataGridView1.DataSource = db.LoadTaiXe(caulenh + dieukien);
        }
    }    
}
