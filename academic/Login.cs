﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace academic
{



    public partial class Login : Form
    {
        public static bool teacher = false;
        public static bool teacher_login = false;
        public Login()
        {
            InitializeComponent();
            dd_te_or_pup.Text = "Pupil";
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

            System.Windows.Forms.Application.Exit();
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }
        
        private void pictureBox2_Click(object sender, EventArgs e)
        {
        
         }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        //-----------------------------
        int mousesX = 0, mousesY = 0;
        int MX = 0, MY = 0;
        bool mousDown;

        private void top_bar_MouseMove(object sender, MouseEventArgs e)
        {
            if (mousDown)
            {
                mousesX = MousePosition.X;
                mousesY = MousePosition.Y;

                this.SetDesktopLocation(mousesX - MX, mousesY - MY);
            }
        }


        //LOGIN
        String user_username;
        String user_pw;
        private void bunifuThinButton1_Click(object sender, EventArgs e)
        {
            user_username = this.tb_login_first_name.text + "," + this.tb_login_last_name.text;
            user_pw = this.tb_pw.text;
            //dd_login
            if (teacher_login)
            {
                if (Program.runMYSQL_EXISTS("SELECT count(*) FROM TEACHER WHERE user_name = '" + user_username + "' AND user_pass='" + user_pw + "'", Program.connection))
                {
                    TEACHER_OBJ.name = user_username;
                    TEACHER_OBJ.pass= user_pw;
                    Form1 main_form = new Form1();
                    main_form.Show();
                    this.Hide();
                }
                else
                {
                    alert_login.Text = "Wrong data, are you a pupil?";
                }
            }
            else
            {
                if (Program.runMYSQL_EXISTS("SELECT count(*) FROM USER WHERE user_name = '" + user_username + "' AND user_pass='" + user_pw + "'", Program.connection))
                {
                    
                    PUPIL_OBJ.name = user_username;
                    PUPIL_OBJ.pass = user_pw;
                    Form1 main_form = new Form1();
                    main_form.Show();
                    this.Hide();
                    Console.WriteLine(PUPIL_OBJ.name);
                }
                else
                {
                    alert_login.Text = "Wrong data, are you a teacher?";
                }
            }


        }
        //LOGIN

        private void bunifuTileButton1_Click(object sender, EventArgs e)
        {

        }

        private void bunifuTextbox1_OnTextChange(object sender, EventArgs e)
        {
        }
        //---------------------------
        private void btn_login_sel_Click(object sender, EventArgs e)
        {
            if (panel_login.Left >= 20)
            {
                toLogin();
            }
        }

        private void btn_reg_sel_Click(object sender, EventArgs e)
        {
            if (panel_reg.Left >= 20)
            {
                toReg();
            }

        }

        //---------------------------
        private void bunifuSeparator2_Load(object sender, EventArgs e)
        {

        }

        private void bunifuTextbox1_OnTextChange_1(object sender, EventArgs e)
        {

        }

        private void bunifuTextbox3_OnTextChange(object sender, EventArgs e)
        {

        }

        private void bunifuTextbox4_OnTextChange(object sender, EventArgs e)
        {

        }

        private void bunifuCustomLabel1_Click(object sender, EventArgs e)
        {

        }

        //REGISTER
        String name;
        String pw1;
        String pw2;
        String user_class;
        String user_class_pw;
        String age;
        String school;
        String teacher_id;
        String email;
        String tel;
        private void btn_reg_Click(object sender, EventArgs e)
        {
            name = this.tb_fn.text + "," + this.tb_ln.text;
            pw1 = this.tb_pw1.text;
            pw2 = this.tb_pw2.text;
            email = this.tb_email.text;
            tel = this.tb_tel.text;
            if (teacher)
            {
                teacher_id = this.tb_class.text;
            } else
            {
                user_class = this.tb_class.text;
            }
            user_class_pw = this.tb_class_pw.text;
            school = this.tb_school.text;
            age = this.tb_age.text;
            //Console.WriteLine(name+pw1+pw2 + user_class + user_class_pw + school);



            if ( name == "First Name,Last Name"  || school == "School name" || school == "School name" || email == "Email" || tel == "Telephone number" || email == "" || name == "" || tel == "" || school == "")
            { 

                reg_alert.Text = "Error, please fill every field!";
            } else
            {
                if ((pw1.Equals(pw2)))
                {
                    reg_alert.Text = "Data Correct";

                        if (teacher)
                    {
                        //////////////////////TEACHER REG
                        if (!Program.runMYSQL_EXISTS("SELECT count(*) FROM TEACHER WHERE user_name = '" + name + "'", Program.connection))
                        {
                        user_class_pw = "";
                        if (teacher_id.Any(x => !char.IsLetter(x)))
                        {
                            age = "0";
                            if (Program.runMYSQL_EXISTS("SELECT count(*) FROM IDs WHERE tid = '" + teacher_id + "'", Program.connection))
                            {
                                if(Program.runMYSQL_EXISTS("SELECT count(*) FROM IDs WHERE tid = '" + teacher_id + "' AND used=1", Program.connection))
                                {
                                    reg_alert.Text = "ID Already in use";
                                } else
                                {
                                    Program.runMYSQL("UPDATE IDs SET name='" + name + "',school='" + school + "',tid='" + teacher_id + "',used='1',blocked='0' WHERE tid='" + teacher_id + "'", Program.connection);
                                    //UPDATE sss SET lastname='Doe'                                                                            WHERE id=2
                                    Program.runMYSQL("INSERT INTO TEACHER (user_name,user_pass,user_class,user_class_pw,user_age,user_school,email,tel,email_seeable,tel_seeable,tid) VALUES ('" + name + "','" + pw1 + "','" + user_class + "','" + user_class_pw + "','" + age + "','" + school + "','" + email + "','" + tel + "',' ',' ','" + teacher_id + "')", Program.connection);
                                    reg_alert.Text = "Registered successfully";
                                    toLogin();
                                }
                            }
                            else
                            {
                                reg_alert.Text = "No ID Found";
                            }
                        } else
                        {
                            reg_alert.Text = "ID Contains Letter!";
                            }
                        } else
                        {
                            reg_alert.Text = "Name Already in use";

                        }
                        //////////////////////TEACHER REG
                    }
                    else
                    {
                        //////////////////////PUPIL REG
                        if (!Program.runMYSQL_EXISTS("SELECT count(*) FROM USER WHERE user_name = '" + name + "'", Program.connection))
                        {
                            if (Program.runMYSQL_EXISTS("SELECT count(*) FROM CLASSES WHERE class_name = '" + user_class + "' AND class_pw = '" + user_class_pw + "'", Program.connection))
                            {
                                Program.runMYSQL("INSERT INTO USER (user_name,user_pass,user_class,user_class_pw,user_age,user_school,email,chat_ban) VALUES ('" + name + "','" + pw1 + "','" + user_class + "','" + user_class_pw + "','" + age + "','" + school + "','" + email + "','off')", Program.connection);
                                reg_alert.Text = "Registered successfully";
                                toLogin();
                                } else
                            {
                                reg_alert.Text = "Class Pw or Name wrong!";
                            }
                        }
                        else
                        {
                            reg_alert.Text = "Name Already in use";

                        }
                       
                        //////////////////////PUPIL REG
                    }
                } else
                {
                    reg_alert.Text = "Passwords don t match";
                }

            }
        }

        //REGISTER
        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {

        }

        private void tb_pw_OnTextChange(object sender, EventArgs e)
        {
            Console.WriteLine(tb_pw.text);
        }

        private void panel_reg_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dd_te_or_pup_onItemSelected(object sender, EventArgs e)
        {
            int row = int.Parse(dd_te_or_pup.selectedIndex.ToString());
            if (row == 0)
            {
                tb_class_pw.Hide();
                tb_age.Hide();
                tb_class.text = "Teacher ID";
                tb_class.Width = 414;
                tb_class.Height = 67;
                //414; 67
                teacher = true;
            }
            else if (row == 1)
            {
                tb_class.text = "Class name";
                tb_class.Width = 194;
                tb_class.Height = 57;
                //194; 57
                tb_class_pw.Show();
                tb_age.Show();
                teacher = false;
            }
        }

        private void tb_class_pw_OnTextChange(object sender, EventArgs e)
        {

        }

        private void top_bar_MouseUp(object sender, MouseEventArgs e)
        {
            mousDown = false;
        }

        private void top_bar_MouseDown(object sender, MouseEventArgs e)
        {
            mousDown = true;
            MX = e.X;
            MY = e.Y;
        }

        private void dd_login_onItemSelected(object sender, EventArgs e)
        {
            int row = int.Parse(dd_login.selectedIndex.ToString());
            if (row == 0)
            {
                t_login.Text = "Login as teacher";
                teacher_login = true;
            }
            else if (row == 1)
            {
                t_login.Text = "Login as pupil";
                teacher_login = false;
            }
        }

        private void tb_login_first_name_OnTextChange(object sender, EventArgs e)
        {

        }

        private void tb_login_first_name_MouseDown(object sender, MouseEventArgs e)
        {
        }

        private void tb_login_first_name_MouseEnter(object sender, EventArgs e)
        {
        }

        private void tb_login_first_name_Click(object sender, EventArgs e)
        {

        }

        private void toLogin()
        {

            line.Left = 124;
            panel_reg.Visible = false;
            panel_reg.Left = 574;

            panel_login.Visible = false;
            panel_login.Left = 53;
            panel_login.Visible = true;
            panel_login.Refresh();
        }

        private void toReg()
        {
            line.Left = 297;
            panel_login.Visible = false;
            panel_login.Left = 574;

            panel_reg.Visible = false;
            panel_reg.Left = 23;
            panel_reg.Visible = true;
            panel_reg.Refresh();
        }
    }
}