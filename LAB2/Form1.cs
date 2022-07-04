﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }



        private void Form1_Load(object sender, EventArgs e)
        {
            //load SQL data
            List<Student> a = SQLHandle.getAllStudent();
            dataGridView1.DataSource = a;

            //load major combobox data
            comboBox1.Items.Add("All");
            List<string> major = new List<string>();
            for (int i = 0; i < a.Count; i++)
            {
                if (!major.Contains(a[i].Major))
                {
                    major.Add(a[i].Major);
                }
            }
            for (int i = 0; i < major.Count; i++)
            {
                comboBox1.Items.Add(major[i]);
            }

            numericUpDown3.Maximum = 31;
            numericUpDown4.Maximum = DateTime.Now.Year;
            numericUpDown7.Maximum = DateTime.Now.Year;
            numericUpDown5.Value = DateTime.Now.Month;
            numericUpDown6.Value = DateTime.Now.Day;
            numericUpDown7.Value = DateTime.Now.Year;

            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<bool> sex = new List<bool>();
            List<bool> active = new List<bool>();
            string major;
            if (comboBox1.Text == "All")
            {
                major = "";
            }
            else major = comboBox1.Text;
            //load data for sex radio button
            if (radioButton3.Checked)
            {
                sex.Add(true);
                sex.Add(false);
            }
            else if (radioButton1.Checked)
            {
                sex.Add(true);
            }
            else sex.Add(false);
            sex.Sort();
            active.Sort();
            //load data for active
            if (radioButton4.Checked)
            {
                active.Add(true);
                active.Add(false);
            }
            else if (radioButton5.Checked)
            {
                active.Add(true);
            }
            else active.Add(false);
            DateTime min = new DateTime((int)numericUpDown4.Value, (int)numericUpDown2.Value, (int)numericUpDown3.Value);
            DateTime max = new DateTime((int)numericUpDown7.Value, (int)numericUpDown5.Value, (int)numericUpDown6.Value);
            string command = "select * from Student";
            DataTable table = SQLHandle.getData(command, null);
            List<Student> list = SQLHandle.ToStuList(table);
            dataGridView1.DataSource = 
                list.Where(x => x.Id.ToString().Contains(textBox7.Text)
                 && sex.Contains(x.Sex)
                 && x.Name.ToLower().Contains(textBox5.Text.ToLower())
                 && x.Major.ToLower().Contains(major.ToLower())
                 && x.Scholarship >= (float)numericUpDown1.Value
                 && active.Contains(x.Active)
                 && (x.Dob >= min && x.Dob < max)).ToList();
        }

        public Dictionary<int, int> limit()
        {
            Dictionary<int, int> limit = new Dictionary<int, int>();
            limit.Add(1, 31); limit.Add(2,29); limit.Add(3, 31);
            limit.Add(4, 30); limit.Add(5, 31); limit.Add(6, 30);
            limit.Add(7, 31); limit.Add(8, 31); limit.Add(9, 30);
            limit.Add(10, 31); limit.Add(11, 30); limit.Add(12, 31);
            return limit;
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            Dictionary<int, int> Limit = limit();
            numericUpDown3.Maximum = Limit[(int)numericUpDown2.Value];
        }

        private void numericUpDown5_ValueChanged(object sender, EventArgs e)
        {
            Dictionary<int, int> Limit = limit();
            numericUpDown6.Maximum = Limit[(int)numericUpDown5.Value];
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //load SQL data
            List<Student> a = SQLHandle.getAllStudent();
            dataGridView1.DataSource = a;

            //reset value 
            textBox7.Text = "";
            radioButton3.Checked = true;
            textBox5.Text = "";
            comboBox1.Text = "All";
            numericUpDown1.Value = 0;
            radioButton4.Checked = true;
            numericUpDown3.Value = 1;
            numericUpDown2.Value = 1;
            numericUpDown4.Value = 1970;
            numericUpDown6.Value = DateTime.Now.Day;
            numericUpDown5.Value = DateTime.Now.Month;
            numericUpDown7.Value = DateTime.Now.Year;
        }
    }
}
