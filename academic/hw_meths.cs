﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace academic
{
    class hw_meths
    {
        /// <summary>
        /// Methode for check if class_hw is already inserted in hw table. When not -> Insert 
        /// </summary>
        public static void check_and_create()
        {
            String class_name="";
            if (dashboard_mod_teacher.selected == "") { } else { class_name = dashboard_mod_teacher.selected;
                
                if (Program.runMYSQL_EXISTS("SELECT count(*) FROM HOMEWORK WHERE class_name = '" + class_name + "'", Program.connection))
                {
                    Console.WriteLine("HASHW");
                }
                else
                {
                    Program.runMYSQL("INSERT INTO HOMEWORK (hw,class_name) VALUES ('PUT SOMETHING HERE.','" + class_name + "')", Program.connection);
                }
            }
        }

        /// <summary>
        /// Methode for setting hw
        /// </summary>
        /// <param name="hw"></param>
        /// <param name="class_name"></param>
        public static void push_set_hw(String hw, String class_name)
        {
            hw_meths.check_and_create();
            Program.runMYSQL(@"UPDATE HOMEWORK SET hw='" + hw + "' WHERE class_name='" + class_name + "'", Program.connection);
        }

        /// <summary>
        /// Methode for getting hw
        /// </summary>
        /// <param name="class_name"></param>
        /// <returns></returns>
        public static String get_hw(String class_name)
        {
            hw_meths.check_and_create();
            return Program.runMYSQL_GET("SELECT * FROM HOMEWORK WHERE class_name='"+class_name+"'", Program.connection, "hw");
        }

    }
}
