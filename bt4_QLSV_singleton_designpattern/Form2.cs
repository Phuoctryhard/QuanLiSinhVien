using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace bt4_QLSV_singleton_designpattern
{
    public partial class Form2 : Form
    {
        public delegate void MyDelegate(string msv, string ten, string idlop, DateTime ngaysinh, string dtb, bool sex, bool anh, bool hb, bool ccnd);
        public MyDelegate d;
        public Form2()
        {
            InitializeComponent();
        }
        private void btnOki_Click(object sender, EventArgs e)
        {
            getInfoForm2();
        }
        private void getInfoForm2()
        {
            if (checkData() == true)
            {
                string msv = textMsv.Text.Trim();
                string ten = textName.Text.Trim();
                string idlop = "";
                if (cbbClass.SelectedIndex != -1)
                {
                string LopHocPhan = cbbClass.SelectedItem.ToString().Trim();
                idlop = LopHocPhan.Substring(0, 2) + LopHocPhan.Substring(LopHocPhan.Length - 3);
                }
                DateTime ngaysinh = dateTimePicker1.Value;
                string dtb = textDtb.Text.Trim();
                bool sex = radioNam.Checked;
                bool anh = checkPic.Checked;
                bool hb = checkHocBa.Checked;
                bool ccnd = checkCCCD.Checked;
                d(msv, ten, idlop, ngaysinh, dtb, sex, anh, hb, ccnd);
            }
            else
            {

            }
        }

        // ham check du lieu trong do phai dc dien 


        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();

        }
        private bool checkData()
        {
            if (string.IsNullOrEmpty(textMsv.Text))
            {
                MessageBox.Show("Ban chua nhap ");
                textMsv.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(textDtb.Text))
            {
                MessageBox.Show("Ban chua nhap Diem trung binh ");
                textDtb.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(textName.Text))
            {
                MessageBox.Show("Ban chua nhap  ");
                textName.Focus();
                return false;

            }
            if (string.IsNullOrEmpty(cbbClass.SelectedItem.ToString()))
            {
                MessageBox.Show("Ban chua nhap  ");
                cbbClass.Focus();
                return false;
            }
            if (dateTimePicker1.Value == DateTime.MinValue)
            {
                // giá trị chua dc chọn
                dateTimePicker1.Focus();
                return false;
            }
            if (radioNam.Checked == false && radioNu.Checked == false)
            {
                MessageBox.Show("Ban chua nhap sex  ");
                radioNam.Focus();
                radioNu.Focus();
                return false;
            }
            return true;

        }
    }
}
