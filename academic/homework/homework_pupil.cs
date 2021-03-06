﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace academic
{
    public partial class homework_pupil : UserControl
    {
        /// <summary>
        /// Instance
        /// </summary>
        private static homework_pupil homework_pupil_inst;

        /// <summary>
        /// Instance const.
        /// </summary>
        public static homework_pupil Instance
        {
            get
            {
                if (homework_pupil_inst == null)
                    homework_pupil_inst = new homework_pupil();
                return homework_pupil_inst;
            }
        }

        /// <summary>
        /// Const.
        /// </summary>
        public homework_pupil()
        {
            InitializeComponent();
            if (!TEACHER_OBJ.checkIfIsTeacher()) { 
            rtb_hw.Text = hw_meths.get_hw(PUPIL_OBJ.get_user_class());
            }
        }

        /// <summary>
        /// Event-> Reloading hw text
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (!TEACHER_OBJ.checkIfIsTeacher())
            {
                rtb_hw.Text = hw_meths.get_hw(PUPIL_OBJ.get_user_class());
            }
        }
    }
}
