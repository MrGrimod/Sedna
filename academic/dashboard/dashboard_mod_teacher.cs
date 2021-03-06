﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using academic.mysql;
using academic.dashboard;
using academic.chat;

namespace academic
{
    public partial class dashboard_mod_teacher : UserControl
    {
        /// <summary>
        /// String for selected class
        /// </summary>
        public static String selected="";

        /// <summary>
        /// Instance
        /// </summary>
        private static dashboard_mod_teacher dashboard_mod_inst;

        private static Control c = new Control();

        /// <summary>
        /// Instance const.
        /// </summary>
        public static dashboard_mod_teacher Instance
        {
            get
            {
                if (dashboard_mod_inst == null)
                    dashboard_mod_inst = new dashboard_mod_teacher();

                return dashboard_mod_inst;
            }
        }

        public void init_self()
        {
        }

        /// <summary>
        /// Const.
        /// </summary>
        public dashboard_mod_teacher()
        {
            InitializeComponent();
            if (TEACHER_OBJ.checkIfIsTeacher())
                {
                dash_methods.INSERT_LIST_VIEW(tv_classes, "SELECT * FROM CLASSES WHERE class_teacher = '" + TEACHER_OBJ.name + "' OR teachers LIKE '%" + TEACHER_OBJ.name + "%'");
                if (TEACHER_OBJ.checkHasClass())
                    {
                        btn_create_class.Hide();
                        pan_create_class.Hide();
                        pan_join.Hide();
                    Console.WriteLine("HASCLASS");
                    }
                    else
                    {
                        pan_create_class.Hide();
                        pan_join.Hide();
                        Console.WriteLine("NOTHASCLASS");
                    }
            }
            else
            {
                btn_create_class.Hide();
                pan_create_class.Hide();
                btn_join_class.Hide();
                pan_join.Hide();
                btn_dash_update.Hide();
            }
        }

        /// <summary>
        /// Event-> Creat class
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_create_class_Click(object sender, EventArgs e)
        {
            pan_create_class.Show();
            btn_create_class.Hide();
        }

        /// <summary>
        /// Evemt-> Creats class
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_create_Click(object sender, EventArgs e)
        {
            String name = tb_class_name.text;
            String pw = tb_class_pw.text;
            String class_pw = tb_class_pw_class.text;
            if (!mysql_basic_methods.runMYSQL_EXISTS("SELECT count(*) FROM CLASSES WHERE class_name = '" + name.Trim() + "'", mysql_connection_manager.connection))
            {
                mysql_basic_methods.runMYSQL("INSERT INTO CLASSES (	class_name,class_pw,class_school,class_teacher,created_date,class_id,teachers,teacher_calss_pw) VALUES ('" + name.Trim() + "','" + pw.Trim() + "','" + TEACHER_OBJ.get_teacher_school().Trim() + "','" + TEACHER_OBJ.name.Trim() + "','"+ System.DateTime.Now.ToShortDateString().Trim() + "','" + TEACHER_OBJ.get_tid().Trim() + "','','" + class_pw.Trim() + "')", mysql_connection_manager.connection);
                mysql_basic_methods.runMYSQL("UPDATE TEACHER SET user_class='" + name.Trim() + "',user_class_pw='" + pw.Trim() + "' WHERE user_name='" + TEACHER_OBJ.name.Trim() + "'", mysql_connection_manager.connection);
                dash_methods.INSERT_LIST_VIEW(tv_classes, "SELECT * FROM CLASSES WHERE class_teacher = '" + TEACHER_OBJ.name.Trim() + "' OR teachers LIKE '%" + TEACHER_OBJ.name.Trim() + "%'");
                btn_create_class.Hide();
                pan_create_class.Hide();
            }
            else
            {
                bunifuCustomLabel_err.Text = "Name Already in use";
            }
        }

        /// <summary>
        /// Event-> Inserts classes into class list.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_dash_update_Click(object sender, EventArgs e)
        {
            dash_methods.INSERT_LIST_VIEW(tv_classes, "SELECT * FROM CLASSES WHERE class_teacher = '" +TEACHER_OBJ.name+ "' OR teachers LIKE '%" + TEACHER_OBJ.name+ "%'");
        }


        private void tv_classes_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem selectedItem = tv_classes.SelectedItems[0];
            Console.WriteLine(selectedItem.SubItems[1].Text);
        }

        /// <summary>
        /// Event-> Joins class
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_join_class_Click(object sender, EventArgs e)
        {
            btn_join_class.Hide();
            pan_join.Show();
        }

        /// <summary>
        /// Event-> Joins class
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bunifuThinButton1_Click(object sender, EventArgs e)
        {
            String class_name;
            String teacher_name;
            if (PUPIL_OBJ.checkIfIsPupil())
            {
                class_name = PUPIL_OBJ.get_user_class();
                teacher_name = PUPIL_OBJ.name;
            }
            else
            {
                class_name = dashboard_mod_teacher.selected;
                teacher_name = TEACHER_OBJ.name;
            }

            if (mysql_basic_methods.runMYSQL_EXISTS("SELECT count(*) FROM CLASSES WHERE class_name = '" + tb_join_name.text.Trim() + "' AND class_pw = '" + tb_join_pw.text.Trim() + "'", mysql_connection_manager.connection) &&
                mysql_basic_methods.runMYSQL_EXISTS("SELECT count(*) FROM CLASSES WHERE class_name = '" + tb_join_name.text.Trim() + "' AND teacher_calss_pw = '" + tb_join_pw_class.text.Trim() + "'", mysql_connection_manager.connection))
            {
                if (mysql_basic_methods.runMYSQL_EXISTS("SELECT count(*) FROM WHITELIST WHERE objects LIKE '%" + teacher_name.Trim()  + "%' AND class_name='" + tb_join_name.text.Trim() + "'", mysql_connection_manager.connection))
                {

                t_join_class_alert.Text = "Joined! Please Update list to see the class.";
                    mysql_basic_methods.runMYSQL("UPDATE CLASSES SET teachers= CONCAT(teachers,'" + TEACHER_OBJ.name.Trim() + "." + "') WHERE class_name='" + tb_join_name.text.Trim() + "' AND class_pw='" + tb_join_pw.text.Trim() + "'", mysql_connection_manager.connection);
                //FINISH OK
                t_join_class_alert.Text = "Join a random class!";
                tb_join_name.text = "Class name";
                tb_join_pw.text = "Class Password";
                pan_join.Hide();
                btn_join_class.Show();
                    //FINISH
                    dash_methods.INSERT_LIST_VIEW(tv_classes, "SELECT * FROM CLASSES WHERE class_teacher = '" + TEACHER_OBJ.name.Trim() + "' OR teachers LIKE '%" + TEACHER_OBJ.name.Trim() + "%'");
                }
                else
                {
                    t_join_class_alert.Text = "You are not whitelisted!";
                    Console.WriteLine(teacher_name);
                }
            }
            else
            {
                t_join_class_alert.Text = "Error!";
            }
        }

        /// <summary>
        /// Event-> Left class
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pb_class_delet_Click(object sender, EventArgs e)
        {
            if(getSelectedClass() == TEACHER_OBJ.get_teacher_class())
            {
                Console.WriteLine("You cannot delete your own class!");
            } else
            {
                mysql_basic_methods.runMYSQL("UPDATE CLASSES SET teachers=REPLACE(teachers,'" + TEACHER_OBJ.name + "." + "','') WHERE class_name='" + getSelectedClass() + "'", mysql_connection_manager.connection);
                dash_methods.INSERT_LIST_VIEW(tv_classes, "SELECT * FROM CLASSES WHERE class_teacher = '" + TEACHER_OBJ.name + "' OR teachers LIKE '%" + TEACHER_OBJ.name + "%'");
            }
        }
        private void dashboard_mod_Load(object sender, EventArgs e)
        {

        }

        private void bunifuCustomLabel_err_Click(object sender, EventArgs e)
        {

        }

        private void pan_join_Paint(object sender, PaintEventArgs e)
        {

        }

        /// <summary>
        /// Event-> Selecting class
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tv_classes_Click(object sender, EventArgs e)
        {
            ListViewItem selectedItem = tv_classes.SelectedItems[0];
            //selected = selectedItem.SubItems[1].Text;
            //Console.WriteLine(selected);
        }

        /// <summary>
        /// Methode for getting selected class 
        /// </summary>
        /// <returns></returns>
        public String getSelectedClass()
        {
            ListViewItem selectedItem = tv_classes.SelectedItems[0];
            selected= selectedItem.SubItems[1].Text;
            return selectedItem.SubItems[1].Text;
        }

        /// <summary>
        /// Methode for loading popup
        /// </summary>
        /// <param name="name"></param>
        public void load_popup_teacher_info(String name)
        {
            t_class_name.Text = name;
            String school = mysql_basic_methods.runMYSQL_GET("SELECT * FROM CLASSES WHERE class_name='"+name+"'", mysql_connection_manager.connection, "class_school");
            String date = mysql_basic_methods.runMYSQL_GET("SELECT * FROM CLASSES WHERE class_name='" + name + "'", mysql_connection_manager.connection, "created_date");
            String cl_email = mysql_basic_methods.runMYSQL_GET("SELECT * FROM TEACHER WHERE user_class='" + name + "'", mysql_connection_manager.connection, "email");
            t_info_school.Text = school;
            t_info_email.Text = cl_email;
        }

        /// <summary>
        /// Event-> Loading Popup for class
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tv_classes_MouseDoubleClick_1(object sender, MouseEventArgs e)
        {
            ListViewItem selectedItem = tv_classes.SelectedItems[0];
            String selectedd = selectedItem.SubItems[1].Text;
            selected = selectedd;
            load_popup_teacher_info(selectedd);
        }
        

        /// <summary>
        /// Event-> Methode for selecting class with popup
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            ListViewItem selectedItem = tv_classes.SelectedItems[0];
            selected = selectedItem.SubItems[1].Text;
            //Console.WriteLine(selected);

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }


        //<----------Clear TextBoxes when clicked------------->


        bool tb_name = false;
        private void tb_join_name_Enter(object sender, EventArgs e)
        {
            if (!tb_name)
                tb_join_name.text = "";
                tb_name = true;
        }

        bool tb_pw = false;
        private void tb_join_pw_Enter(object sender, EventArgs e)
        {
            if (!tb_pw)
                tb_join_pw.text = "";
                tb_pw = true;
        }

        bool tb_pw_class = false;
        private void tb_join_pw_classEnter(object sender, EventArgs e)
        {
            if (!tb_pw_class)
                tb_join_pw_class.text = "";
            tb_pw_class = true;
        }

        bool tb_create_name = false;
        private void tb_class_name_Enter(object sender, EventArgs e)
        {
            if (!tb_create_name)
                tb_class_name.text = "";
                tb_create_name = true;
        }

        bool tb_create_pw = false;
        private void tb_class_pw_Enter(object sender, EventArgs e)
        {
            if (!tb_create_pw)
                tb_class_pw.text = "";
                tb_create_pw = true;
        }

        bool tb_create_pw_class = false;
        private void tb_class_pw_class_Enter(object sender, EventArgs e)
        {
            if (!tb_create_pw_class)
                tb_class_pw_class.text = "";
            tb_create_pw_class = true;
        }

        private void btn_send_msg_name_Click(object sender, EventArgs e)
        {
            String msg = tb_msg.Text;
            chat_methods.sendMSG_SPE(msg, getSelectedClass());
            tb_msg.Text = "";
        }

        private void bunifuCustomLabel2_Click(object sender, EventArgs e)
        {

        }

        private void bunifuCustomLabel3_Click(object sender, EventArgs e)
        {

        }

        private void t_class_name_Click(object sender, EventArgs e)
        {

        }

        private void t_selected_class_Click(object sender, EventArgs e)
        {

        }

        private void bunifuTextbox1_OnTextChange(object sender, EventArgs e)
        {

        }

        private void tb_join_pw_OnTextChange(object sender, EventArgs e)
        {

        }

        private void tb_join_name_OnTextChange(object sender, EventArgs e)
        {

        }
        //<----------Clear TextBoxes when clicked------------->
    }
}
