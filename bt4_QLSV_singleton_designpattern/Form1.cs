using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace bt4_QLSV_singleton_designpattern
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            DisPlayTable();
            DisPlayItemCbb();
            
        }
        private void DisPlayTable()
        {
            DataTable dt = new DataTable();
            //cau leng truy van 
            string query = "select sv.Msv,sv.Ten,sv.NgaySinh,sv.Dtb,sv.Sex,sv.Pic,sv.HocBa,sv.Cccd,Lsh.TenLop from  LopSinhHoat as Lsh , SV as sv where Lsh.IdLop = sv.idLop";
            dt =  DBHelper.getInStance.getInfo(query);
            dataGridView1.DataSource = dt;
           
        }
        private void button4_Click(object sender, EventArgs e)
        {
        }       private void button2_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn Có muốn xóa không", "Câu hỏi", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            string query = "delete from SV where Msv = " + msv;
            DBHelper.getInStance.ExectuteNonQuery(query);
            if (result == DialogResult.OK)
            {
                DisPlayTable();
                MessageBox.Show("Xoa thanh cong ");
            }
            else
            {
                Application.Exit();
            }   
        }
        int vt = -1;
        int msv = -1;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {          
            vt = e.RowIndex;
            // lay ra hang tai vi tri kick
            DataGridViewRow row = dataGridView1.Rows[vt];
            msv = Convert.ToInt32(row.Cells[0].Value.ToString());
        }
        private void button5_Click(object sender, EventArgs e)
        {
            // sort 
            if (cbbSearch.SelectedIndex == -1) return;
            string query = "select * from SV ORDER BY "+cbbSearch.SelectedItem.ToString().Trim() +" DESC";

            dataGridView1.DataSource = DBHelper.getInStance.getInfo(query);
            MessageBox.Show("Sort Thanh Cong Giam Dan");
        }

        // code hien thi len cbb 
        private void DisPlayItemCbb()
        {
            List<object> values = new List<object>();
           // List<string> NameClass -new List<string>("");
           //string[] NameClass1 = { "21cntt", "20cntt" };
            //cbbClass.Items.AddRange(NameClass1);


           foreach(DataGridViewRow row in dataGridView1.Rows)
            {
                object value = row.Cells["TenLop"].Value;
                if(value != null)
                {
                    if (values.Contains(value)==false)
                    {
                        values.Add(value);
                        cbbClass.Items.Add(value);

                    }
                }
            }
          
        }
        String ItemCbbClass = "";
        private void cbbClass_SelectedIndexChanged(object sender, EventArgs e)
        {

            if(cbbClass.SelectedIndex != -1)
          
                ItemCbbClass = cbbClass.SelectedItem.ToString().Trim();
                 {
                SqlParameter[] listPar = {
             
                new SqlParameter("@ten",ItemCbbClass)
                 };
               // string query = "select * from SV where idlop = @ten";
                string query1 = "select sv.Msv,sv.Ten,sv.NgaySinh,sv.Dtb,sv.Sex,sv.Pic,sv.HocBa,sv.Cccd,Lsh.TenLop from  LopSinhHoat as Lsh , SV as sv where Lsh.IdLop = sv.idLop and Lsh.TenLop = @ten";
                dataGridView1.DataSource = DBHelper.getInStance.getInfo(query1, listPar);
            }
        }
        private void textName_TextChanged(object sender, EventArgs e)
        {
            string textName1 = textName.Text.Trim();
            SqlParameter[] listPar =
            {
                new SqlParameter("@ten",ItemCbbClass)
            };
            {
                //string query = "select * from SV where  ten LIKE N'%" + textName1 + "%' and  idlop = @ten ";
                string query1 = "select sv.Msv,sv.Ten,sv.NgaySinh,sv.Dtb,sv.Sex,sv.Pic,sv.HocBa,sv.Cccd,Lsh.TenLop from  LopSinhHoat as Lsh , SV as sv where Lsh.IdLop = sv.idLop and Lsh.TenLop = @ten and sv.ten Like '%"+ textName1 +"%'";
                dataGridView1.DataSource = DBHelper.getInStance.getInfo(query1, listPar);
            }
        }
        // check ma so sinh vien da ton tai chua
        private bool checkMsv( int msv)
        {
            string query = "select Count(*) from SV where Msv ="+ msv ;
            if (DBHelper.getInStance.ExecuteScale(query) == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private void Add(string msv, string ten, string idlop, DateTime ngaysinh, string dtb, bool sex, bool anh, bool hb, bool ccnd)
        {
             int msv1 = Convert.ToInt32(msv);
            if (checkMsv(msv1) == true)
            {
                MessageBox.Show( "ĐÃ TỒN TẠI Mã ");
                return; // da ton tai 
            }
            SqlParameter[] sqlParameter =
            {

                new SqlParameter("@msv",msv1),
                new SqlParameter("@ten",ten),
                new SqlParameter("@lopsh",idlop),
                new SqlParameter("@ngaySinh",ngaysinh),
                new SqlParameter("@dtb",dtb),
                new SqlParameter("@nam",sex),
                new SqlParameter("@anh",anh),
                new SqlParameter("@hb", hb),
                new SqlParameter("@cccd",ccnd)

            };
            string query = "insert into SV(Msv,Ten,NgaySinh,dtb,sex,pic,HocBa,Cccd,idLop) values (@msv1,@ten,@ngaysinh,@dtb,@nam,@anh,@hb,@cccd,@lopsh)";
            //DBHelper.getInStance.getInfo(query, sqlParameter);
            //DataTable dt = new DataTable();
           // dt = DBHelper.getInStance.getInfo(query,sqlParameter);
            DBHelper.getInStance.ExecuteNonQuery(query, sqlParameter);
            DisPlayTable();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Form2 a = new Form2();
            a.d += new Form2.MyDelegate(Add);
            a.Show();
       }
        private void EditSV(string msv, string ten, string idlop, DateTime ngaysinh, string dtb, bool sex, bool anh, bool hb, bool ccnd)
        {
          SqlParameter[] sqlParameter =
         {
                new SqlParameter("@msv",msv),
                new SqlParameter("@ten",ten),
                new SqlParameter("@lopsh",idlop),
                new SqlParameter("@ngaySinh",ngaysinh),
                new SqlParameter("@dtb",dtb),
                new SqlParameter("@nam",sex),
                new SqlParameter("@anh",anh),
                new SqlParameter("@hb", hb),
                new SqlParameter("@cccd",ccnd)
            };
            string query = "update SV set Msv = @msv,Ten =@ten,NgaySinh=@ngaysinh,dtb = @dtb,sex= @nam,pic = @anh,HocBa = @hb,Cccd = @cccd,idLop = @lopsh where Msv = @msv";
            DBHelper.getInStance.ExecuteNonQuery(query, sqlParameter);
            DisPlayTable();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            Form2 Edit = new Form2();
            Edit.d += new Form2.MyDelegate(EditSV);
            Edit.Show();
        } 
    }
}
