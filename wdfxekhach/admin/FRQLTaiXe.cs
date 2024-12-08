using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace wdfxekhach
{
    public partial class FRQLTaiXe : Form
    {
        CONNECT db = new CONNECT();
        public FRQLTaiXe()
        {
            InitializeComponent();
        }

        public static int MaTX;
        private void FRQLTaiXe_Load(object sender, EventArgs e)
        {

            dataGridView1.ReadOnly = true;
            dataGridView1.ScrollBars = ScrollBars.Both;
            dataGridView1.AllowUserToResizeColumns = false;
            dataGridView1.DataSource = db.LoadTaiXe();
            // kich thuoc cot to ra khi noi dung tang
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dataGridView1.Columns[0].HeaderText = "Mã TX";
            dataGridView1.Columns[1].HeaderText = "Tên TX";
            dataGridView1.Columns[2].HeaderText = "SDT";
            dataGridView1.Columns[3].HeaderText = "CCCD";
            dataGridView1.Columns[4].HeaderText = "Địa Chỉ";

            txt_name.Text = "";
            txt_sdt.Text = "";
            txt_cccd.Text = "";
            txt_diachi.Text = "";

            btn_xoa.Enabled = false;
            btn_luu.Enabled = false;
            btn_them.Enabled = true;            
        }

                

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult r = MessageBox.Show("Xác nhận xóa", "Thông báo", MessageBoxButtons.YesNo);
            if (r == DialogResult.Yes)
            {
                foreach(DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    db.XoaTaiXe(int.Parse(row.Cells["MaTaiXe"].Value.ToString()));
                }
                FRQLTaiXe_Load(sender, e);
            }
        }

        private void btn_them_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_name.Text))
            {
                errorProvider1.SetError(txt_name, "Tên không được bỏ trống");
            }
            else
            {
                errorProvider1.Clear();
                if (string.IsNullOrEmpty(txt_sdt.Text))
                {
                    errorProvider1.SetError(txt_sdt, "SDT không được bỏ trống");
                }
                else
                {
                    errorProvider1.Clear();
                    if (string.IsNullOrEmpty(txt_cccd.Text))
                    {
                        errorProvider1.SetError(txt_cccd, "CCCD không được bỏ trống");
                    }
                    else
                    {
                        errorProvider1.Clear();
                        if (string.IsNullOrEmpty(txt_diachi.Text))
                        {
                            errorProvider1.SetError(txt_diachi, "Không được bỏ trống");
                        }
                        else
                        {
                            errorProvider1.Clear();

                            if (db.ThemTaiXe(db.LayMaTaiXe(), txt_name.Text, txt_sdt.Text, txt_cccd.Text, txt_diachi.Text) == 1)
                            {
                                MessageBox.Show("Thêm thành công");
                                FRQLTaiXe_Load(sender, e);

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

        private void btn_xoa_Click(object sender, EventArgs e)
        {
            if(db.XoaTaiXe(MaTX) == 1)
            {
                MessageBox.Show("Xóa thành công");
                FRQLTaiXe_Load(sender, e);
            }
        }

        private void btn_luu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_name.Text))
            {
                errorProvider1.SetError(txt_name, "Tên không được bỏ trống");
            }
            else
            {
                errorProvider1.Clear();
                if (string.IsNullOrEmpty(txt_sdt.Text))
                {
                    errorProvider1.SetError(txt_sdt, "SDT không được bỏ trống");
                }
                else
                {
                    errorProvider1.Clear();
                    if (string.IsNullOrEmpty(txt_cccd.Text))
                    {
                        errorProvider1.SetError(txt_cccd, "CCCD không được bỏ trống");
                    }
                    else
                    {
                        errorProvider1.Clear();
                        if (string.IsNullOrEmpty(txt_diachi.Text))
                        {
                            errorProvider1.SetError(txt_diachi, "Không được bỏ trống");
                        }
                        else
                        {
                            errorProvider1.Clear();

                            if(db.SuaTaiXe(MaTX, txt_name.Text, txt_sdt.Text, txt_cccd.Text, txt_diachi.Text) == 1)
                            {
                                MessageBox.Show("Sửa thành công");
                                FRQLTaiXe_Load(sender, e);
                            }                            
                        }
                    }
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];

                MaTX = int.Parse(selectedRow.Cells[0].Value.ToString());
                txt_name.Text = selectedRow.Cells[1].Value.ToString();
                txt_sdt.Text = selectedRow.Cells[2].Value.ToString();
                txt_cccd.Text = selectedRow.Cells[3].Value.ToString();
                txt_diachi.Text = selectedRow.Cells[4].Value.ToString();


                btn_xoa.Enabled = true;
                btn_luu.Enabled = true;
                btn_them.Enabled = false;
            }
        }

        private void btn_load_Click(object sender, EventArgs e)
        {
            FRQLTaiXe_Load(sender, e);
        }

        private void btn_timkiem_Click(object sender, EventArgs e)
        {
            string dieukien = " where";
            string caulenh = "select * from TAIXE";
            if (!string.IsNullOrEmpty(txt_name.Text))
            {
                dieukien += $" TenTaiXe like N'%{txt_name.Text}%'";
            }

            if (!string.IsNullOrEmpty(txt_sdt.Text))
            {
                if(dieukien == " where")
                {
                    dieukien += $" SoDienThoai = '{txt_sdt.Text}'";
                }
                else
                {
                    dieukien += $" and SoDienThoai = '{txt_sdt.Text}'";
                }
            }

            if (!string.IsNullOrEmpty(txt_cccd.Text))
            {
                if(dieukien == " where")
                {
                    dieukien += $" CCCD = '{txt_cccd.Text}'";
                }
                else
                {
                    dieukien += $" and CCCD = '{txt_cccd.Text}'";
                }
            }

            if (!string.IsNullOrEmpty(txt_diachi.Text))
            {
                if (dieukien == " where")
                {
                    dieukien += $" DiaChi like N'%{txt_diachi.Text}%'";
                }
                else
                {
                    dieukien += $" DiaChi like N'%{txt_diachi.Text}%'";
                }
            }

            dataGridView1.DataSource = db.LoadTaiXe(caulenh + dieukien);
        }
    }
}
