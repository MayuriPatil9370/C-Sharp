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
    public partial class frm_Search_Student_Details : Form
    {
        public frm_Search_Student_Details()
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

        void Clear_Controls()
        {
            tb_Roll_No.Text = " ";
            tb_Name.Clear();
            tb_Mobile_No.Clear();
            cmb_Course.SelectedIndex = -1;
            dtp_DOB.Text = "01/01/2005";
        }
        


        
        private void btn_Add_New_Student_Click(object sender, EventArgs e)
        {
            frm_Add_New_Student obj = new frm_Add_New_Student();
            obj.Show();
            this.Hide();
        }

        private void btn_View_Student_List_Click(object sender, EventArgs e)
        {
            frm_View_Student_List obj = new frm_View_Student_List();
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





      private void btn_Refresh_Click(object sender, EventArgs e)
        {
            Clear_Controls();
        }

      private void btn_Search_Click(object sender, EventArgs e)
      {
          Con_Open();
          SqlCommand Cmd = new SqlCommand("Select * From Student_Information Where Roll_No= @RNo ", Con);
          Cmd.Parameters.Add("RNo",SqlDbType.Int).Value = tb_Roll_No.Text;
          SqlDataReader Dr = Cmd.ExecuteReader();
          if(Dr.Read())
          {
              tb_Name.Text=Dr.GetString(Dr.GetOrdinal("Name"));
              tb_Mobile_No.Text=(Dr["Mobile_No"].ToString());
              cmb_Course.Text=Dr.GetString(Dr.GetOrdinal("Course"));
              dtp_DOB.Text=(Dr["DOB"].ToString());
          }
          else
          {
              MessageBox.Show("No Record Found","Invalid Roll No");
              tb_Roll_No.Clear();
          }
          Con_Close();


      }

     

       
    }
}
