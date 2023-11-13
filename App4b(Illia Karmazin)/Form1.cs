using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Globalization;

namespace App4b_Illia_Karmazin_
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void GetNumbers(string num)
        {
            if (label1.Text.Length < 16)
            {
                if (Program.status == false && label2.Text != "" && ((label2.Text[label2.Text.Length - 2] == '+') || (label2.Text[label2.Text.Length - 2] == '-') || (label2.Text[label2.Text.Length - 2] == '*') || (label2.Text[label2.Text.Length - 2] == '/')))
                {
                    Program.status = true;
                    label1.Text = "";
                }
                if (Program.status == false && label2.Text != "" && label2.Text[label2.Text.Length - 1] == '=')
                {
                    Program.left = "";
                    Program.status = true;
                    label1.Text = "";
                }
                if (label2.Text != "" && label2.Text[label2.Text.Length - 1] == '=')
                {
                    label1.Text = "";
                    label2.Text = "";
                }
                if (num == "0" && label1.Text == "0")
                    label1.Text = num;
                else
                {
                    if (num != "0" && label1.Text == "0")
                        label1.Text = num;
                    else
                        label1.Text += num;
                }
                if (label1.Text.Length > 12 && label1.Text.Length < 16)
                    label1.Font = new System.Drawing.Font("Segoe UI", label1.Font.Size - 2);
                Program.current = label1.Text;
            }
        }
        private void GetFunction(string func)
        {
            if (func == "+")
            {
                if (label2.Text != "" && label2.Text[label2.Text.Length - 1] == '=')
                {
                    Program.full = Program.left + "+";
                    label2.Text = label1.Text + " + ";
                    Program.status = false;
                }
                else
                {
                    Program.full = Program.left + "+" + Program.current;
                    label2.Text = label1.Text + " + ";
                    double result;
                    DataTable table = new DataTable();
                    table.Columns.Add("expression", typeof(double), Program.full);
                    DataRow row = table.NewRow();
                    table.Rows.Add(row);
                    result = (double)row["expression"];
                    label2.Text = result.ToString(CultureInfo.InvariantCulture) + " + ";
                    Program.status = false;
                    Program.left = result.ToString(CultureInfo.InvariantCulture);
                }
                label1.Text = Program.left;
                Program.point_status = false;
            }
            else if (func == "-")
            {
                if (label2.Text != "" && label2.Text[label2.Text.Length - 1] == '=')
                {
                    Program.full = Program.left + "-";
                    label2.Text = label1.Text + " - ";
                    Program.status = false;
                }
                else
                {
                    if (Program.minus_status == false)
                    {
                        Program.full = Program.current;
                        Program.minus_status = true;
                    }
                    else
                        Program.full = Program.left + "-" + Program.current;
                    label2.Text = label1.Text + " - ";
                    double result;
                    DataTable table = new DataTable();
                    table.Columns.Add("expression", typeof(double), Program.full);
                    DataRow row = table.NewRow();
                    table.Rows.Add(row);
                    result = (double)row["expression"];
                    label2.Text = result.ToString(CultureInfo.InvariantCulture) + " - ";
                    Program.status = false;
                    Program.left = result.ToString(CultureInfo.InvariantCulture);
                }
                label1.Text = Program.left;
                Program.point_status = false;
            }
            else if (func == "=")
            {
                if (label2.Text != "" && label2.Text[label2.Text.Length - 1] == '=') { }
                else
                {
                    if (label2.Text != "")
                        Program.full += label2.Text[label2.Text.Length - 2] + Program.current;
                    else
                        Program.full += Program.left + "+" + Program.current;
                    label2.Text += Program.current;
                    double result;
                    DataTable table = new DataTable();
                    table.Columns.Add("expression", typeof(double), Program.full);
                    DataRow row = table.NewRow();
                    table.Rows.Add(row);
                    result = (double)row["expression"];
                    label1.Text = result.ToString(CultureInfo.InvariantCulture);
                    label2.Text += " =";
                    Program.full = result.ToString(CultureInfo.InvariantCulture);
                    Program.left = Program.current = result.ToString(CultureInfo.InvariantCulture);
                }
                Program.point_status = true;
            }
            else if (func == "x")
            {
                if (Program.current != "")
                {
                    if (Program.current.Length == 1)
                    {
                        Program.current = "0";
                        label1.Text = Program.current;
                    }
                    else
                    {
                        string copy = Program.current;
                        Program.current = "";
                        for (int i = 0; i < copy.Length - 1; ++i)
                        {
                            Program.current += copy[i];
                            label1.Text = Program.current;
                        }
                    }
                }
            }
            else if (func == "C")
            {
                Program.current = "";
                Program.left = "0";
                Program.status = Program.minus_status = Program.multiplication_status = Program.point_status = Program.div_status = false;
                label1.Text = "";
                label2.Text = "";
            }
            else if (func == "CE")
            {
                Program.current = "0";
                label1.Text = Program.current;
            }
            else if (func == ",")
            {
                if (label1.Text != "" && Program.point_status == false && label1.Text[label1.Text.Length - 1] != '.')
                {
                    label1.Text += ".";
                    Program.current += ".";
                    Program.point_status = true;
                }
            }
            else if (func == "*")
            {
                if (label2.Text != "" && label2.Text[label2.Text.Length - 1] == '=')
                {
                    Program.full = Program.left + "*";
                    label2.Text = label1.Text + " * ";
                    Program.status = false;
                }
                else
                {
                    if (Program.multiplication_status == false)
                    {
                        Program.full = Program.current;
                        Program.multiplication_status = true;
                    }
                    else
                        Program.full = Program.left + "*" + Program.current;
                    label2.Text = label1.Text + " * ";
                    double result;
                    DataTable table = new DataTable();
                    table.Columns.Add("expression", typeof(double), Program.full);
                    DataRow row = table.NewRow();
                    table.Rows.Add(row);
                    result = (double)row["expression"];
                    label2.Text = result.ToString(CultureInfo.InvariantCulture) + " * ";
                    Program.status = false;
                    Program.left = result.ToString(CultureInfo.InvariantCulture);
                }
                label1.Text = Program.left;
                Program.point_status = false;
            }
            else if (func == "/")
            {
                if (label2.Text != "" && label2.Text[label2.Text.Length - 1] == '=')
                {
                    Program.full = Program.left + "/";
                    label2.Text = label1.Text + " / ";
                    Program.status = false;
                }
                else
                {
                    if (Program.div_status == false)
                    {
                        Program.full = Program.current;
                        Program.div_status = true;
                    }
                    else
                        Program.full = Program.left + "/" + Program.current;
                    label2.Text = label1.Text + " / ";
                    double result;
                    DataTable table = new DataTable();
                    table.Columns.Add("expression", typeof(double), Program.full);
                    DataRow row = table.NewRow();
                    table.Rows.Add(row);
                    result = (double)row["expression"];
                    label2.Text = result.ToString(CultureInfo.InvariantCulture) + " / ";
                    Program.status = false;
                    Program.left = result.ToString(CultureInfo.InvariantCulture);
                }
                label1.Text = Program.left;
                Program.current = label1.Text;
                Program.point_status = false;
            }
            else if (func == "N" && label1.Text != "")
            {
                if (label1.Text[0] != '-')
                {
                    string copy = "-"+label1.Text;
                    label1.Text = copy;
                }
                else
                {
                    string copy = "";
                    for (int i = 1; i < label1.Text.Length;++i)
                    {
                        copy += label1.Text[i];
                    }
                    label1.Text = copy;
                }
                Program.current = label1.Text;
            }
        }
        private void button22_Click(object sender, EventArgs e)
        {
            GetNumbers("0");
        }

        private void button17_Click(object sender, EventArgs e)
        {
            GetNumbers("1");
        }

        private void button18_Click(object sender, EventArgs e)
        {
            GetNumbers("2");
        }

        private void button19_Click(object sender, EventArgs e)
        {
            GetNumbers("3");
        }

        private void button13_Click(object sender, EventArgs e)
        {
            GetNumbers("4");
        }

        private void button14_Click(object sender, EventArgs e)
        {
            GetNumbers("5");
        }

        private void button15_Click(object sender, EventArgs e)
        {
            GetNumbers("6");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            GetNumbers("7");
        }

        private void button10_Click(object sender, EventArgs e)
        {
            GetNumbers("8");
        }

        private void button11_Click(object sender, EventArgs e)
        {
            GetNumbers("9");
        }

        private void button20_Click(object sender, EventArgs e)
        {
            GetFunction("+");
        }

        private void button24_Click(object sender, EventArgs e)
        {
            GetFunction("=");
        }

        private void button16_Click(object sender, EventArgs e)
        {
            GetFunction("-");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            GetFunction("x");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            GetFunction("C");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            GetFunction("CE");
        }

        private void button23_Click(object sender, EventArgs e)
        {
            GetFunction(",");
        }

        private void button12_Click(object sender, EventArgs e)
        {
            GetFunction("*");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            GetFunction("/");
        }

        private void button21_Click(object sender, EventArgs e)
        {
            GetFunction("N");
        }
    }
}
