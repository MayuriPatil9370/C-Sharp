﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SGM_Student_Mgt_System_2022
{
    public partial class frm_Add_New_Student : Form
    {
        public frm_Add_New_Student()
        {
            InitializeComponent();
        }


        SqlConnection Con = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=Student_Information_DB;Integrated Security=True");

        void Con_Open()
        {
            if (Con.State != ConnectionState.Open)
            {
                Con.Open();
            }
        }
        void Con_Close()
        {
            if (Con.State != ConnectionState.Closed)
            {
                Con.Close();
            }
        }
    

        private void Only_Numeric(object sender, KeyPressEventArgs e)
        {
            if(!(Char.IsDigit(e.KeyChar)||(e.KeyChar==(char)Keys.Back)))
            {
                e.Handled=true;
            }
        }

       
        int Auto_Incr()
        {
            int Cnt = 0;
            
            Con_Open();

            SqlCommand Cmd=new SqlCommand();

            Cmd.Connection=Con;
            Cmd.CommandText="Select Count(*)From Student_Information";

            Cnt=Convert.ToInt32(Cmd.ExecuteScalar());

            if(Cnt > 0)
            {
                Cmd.CommandText="Select Max(Roll_No)From Student_Information";

                Cnt=Convert.ToInt32(Cmd.ExecuteScalar()) + 1;
            }
            else
            {
                Cnt = 101;
            }
            Con_Close();

            return Cnt;
        }

        void Clear_Controls()
        {
            tb_Roll_No.Text=Convert.ToString(Auto_Incr());

            tb_Name.Clear();
            tb_Mobile_No.Clear();
            cmd_Course.SelectedIndex = -1;
            dtp_DOB.Text="1/1/2005";
        }

        private void frm_Add_New_Student_Load(object sender, EventArgs e)
        {
            Clear_Controls();
            tb_Roll_No.Focus();
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            Con_Open();
            
                if(tb_Roll_No.Text !=""&&tb_Name.Text !=""&&tb_Mobile_No.Text !=""&& cmd_Course.Text !="")
                {
                    SqlCommand Cmd = new SqlCommand();
                    Cmd.Connection= Con;
                    Cmd.CommandText="Insert Into Student_Information(Roll_No,Name,DOB,Mobile_No,Course)values(@RNo,@Nm,@Dob,@MNo,@Course)";

                    Cmd.Parameters.Add("RNo",SqlDbType.Int).Value=tb_Roll_No.Text;
                    Cmd.Parameters.Add("Nm",SqlDbType.VarChar).Value=tb_Name.Text;
                    Cmd.Parameters.Add("Dob", SqlDbType.Date).Value = dtp_DOB.Value.Date;
                    Cmd.Parameters.Add("MNo",SqlDbType.Decimal).Value=tb_Mobile_No.Text;
                    Cmd.Parameters.Add("Course",SqlDbType.NVarChar).Value=cmd_Course.Text;

                    Cmd.ExecuteNonQuery();

                    MessageBox.Show("Record Saved");

                    Clear_Controls();
                }
                else
                { 
                    MessageBox.Show("First Fill All Cumpulsary Feilds");
                }
                Con_Close();





        }

        private void btn_View_Student_List_Click(object sender, EventArgs e)
        {
            frm_View_Student_List obj = new frm_View_Student_List();
            obj.Show();
            this.Hide();

        }

        private void btn_Search_Student_Details_Click(object sender, EventArgs e)
        {
            frm_Search_Student_Details obj = new frm_Search_Student_Details();
            obj.Show();
            this.Hide();
        }

        private void btn_Log_Out_Click(object sender, EventArgs e)
        {
            DialogResult Res = MessageBox.Show("Are You Shoure Want To Out???", "LOGOUT", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (Res == DialogResult.Yes)
            {
                frm_Login_Form obj = new frm_Login_Form();
                obj.Show();
                this.Hide();
            }
        }

        private void Only_Text(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsLetter(e.KeyChar) || (e.KeyChar == (char)Keys.Back) || (e.KeyChar == (char)Keys.Space)))
            {
                e.Handled = true;
            }
        }

      
      

       

        
    }
}
