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
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            label1.Text = Program.current;
        }
        private void GetNumbers(string num)
        {

            if (Program.status == false && label2.Text != "" && ((label2.Text[label2.Text.Length - 2] == '+') || (label2.Text[label2.Text.Length - 2] == '-') || (label2.Text[label2.Text.Length - 2] == '*') || (label2.Text[label2.Text.Length - 2] == '/')))
            {
                Program.status = true;
                label1.Text = "";
            }
            if (Program.status == false && Program.math_status == true)
            {
                Program.math_status = false;
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
                label1.Font = new System.Drawing.Font("Segoe UI", label1.Font.Size - 1);
            Program.current = label1.Text;

        }
        private void GetFunction(string func)
        {
            if (func == "+" && label1.Text != "")
            {
                try
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
                    Program.current = label1.Text;
                }
                catch(Exception e)
                {
                    //
                }
            }
            else if (func == "-" && label1.Text != "")
            {
                try
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
                    Program.current = label1.Text;
                }
                catch (Exception e)
                {
                    //
                }
            }
            else if (func == "=")
            {
                try
                {
                    if (label2.Text != "" && label2.Text[label2.Text.Length - 1] == '=') { }
                    else
                    {
                        if (label2.Text != "")
                            Program.full += label2.Text[label2.Text.Length - 2] + Program.current;
                        else
                            Program.full += Program.left + "+" + Program.current;
                        label2.Text += Program.current;
                        string copy = "";
                        copy += Program.full[0];
                        for (int i = 1; i < Program.full.Length; ++i)
                        {
                            if (Program.full[i] == '*' && Program.full[i - 1] == '*') { }
                            else
                            {
                                copy += Program.full[i];
                            }
                        }
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
                catch (Exception e)
                {
                    //
                }
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
            else if (func == "*" && label1.Text != "")
            {
                try
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
                    Program.current = label1.Text;
                }
                catch (Exception e)
                {
                    //
                }
            }
            else if (func == "/" && label1.Text != "")
            {
                try
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
                    Program.current = label1.Text;
                }
                catch (Exception e)
                {
                    //
                }
            }
            else if (func == "N" && label1.Text != "")
            {
                if (label1.Text[0] != '-')
                {
                    string copy = "-" + label1.Text;
                    label1.Text = copy;
                }
                else
                {
                    string copy = "";
                    for (int i = 1; i < label1.Text.Length; ++i)
                    {
                        copy += label1.Text[i];
                    }
                    label1.Text = copy;
                }
                Program.current = label1.Text;
            }
            else if (func == "Q" && label1.Text != "")
            {
                try
                {
                    double result = double.Parse(label1.Text, System.Globalization.CultureInfo.InvariantCulture);
                    result = Math.Sqrt(result);
                    label1.Text = result.ToString(CultureInfo.InvariantCulture);
                    Program.current = label1.Text;
                    Program.math_status = true;
                    Program.status = false;
                }
                catch (Exception e)
                {
                    //
                }
            }
            else if (func == "^" && label1.Text != "")
            {
                try
                {
                    double result = double.Parse(label1.Text, System.Globalization.CultureInfo.InvariantCulture);
                    result = Math.Pow(result, 2);
                    label1.Text = result.ToString(CultureInfo.InvariantCulture);
                    Program.current = label1.Text;
                    Program.math_status = true;
                    Program.status = false;
                }
                catch (Exception e)
                {
                    //
                }
            }
            else if (func == "D" && label1.Text != "")
            {
                try
                {
                    double result = double.Parse(label1.Text, System.Globalization.CultureInfo.InvariantCulture);
                    if (result != 0)
                    {
                        result = 1 / result;
                        label1.Text = result.ToString(CultureInfo.InvariantCulture);
                        Program.current = label1.Text;
                        Program.math_status = true;
                        Program.status = false;
                    }
                }
                catch (Exception e)
                {
                    //
                }
            }
            else if (func == "%" && label1.Text != "")
            {
                try
                {
                    if (label2.Text != "" && (label2.Text[label2.Text.Length - 2] == '+' || label2.Text[label2.Text.Length - 2] == '-'))
                    {
                        double result = (double.Parse(label1.Text, System.Globalization.CultureInfo.InvariantCulture) * double.Parse(Program.left)) / 100;
                        label1.Text = result.ToString(CultureInfo.InvariantCulture);
                        Program.current = result.ToString(CultureInfo.InvariantCulture);
                        Program.math_status = true;
                        Program.status = false;
                    }
                    else if (label2.Text != "" && (label2.Text[label2.Text.Length - 2] == '*' || label2.Text[label2.Text.Length - 2] == '/'))
                    {
                        double result = double.Parse(label1.Text, System.Globalization.CultureInfo.InvariantCulture) / 100;
                        label1.Text = result.ToString(CultureInfo.InvariantCulture);
                        Program.current = result.ToString(CultureInfo.InvariantCulture);
                        Program.math_status = true;
                        Program.status = false;
                    }
                }
                catch (Exception e)
                {
                    //
                }
            }
            else if (func == "M+" && label1.Text != "")
            {
                if (Program.memory.Count > 0)
                {
                    int index = Program.memory.Count - 1;
                    Program.memory[index] += double.Parse(label1.Text, System.Globalization.CultureInfo.InvariantCulture);
                    Program.math_status = true;
                    Program.status = false;
                }
                else
                {
                    Program.memory.Add(int.Parse(label1.Text, System.Globalization.CultureInfo.InvariantCulture));
                    Program.math_status = true;
                    Program.status = false;
                }
                label3.ForeColor = Color.Black;
                label4.ForeColor = Color.Black;
            }
            else if (func == "M-" && label1.Text != "")
            {
                if (Program.memory.Count > 0)
                {
                    int index = Program.memory.Count - 1;
                    Program.memory[index] -= double.Parse(label1.Text, System.Globalization.CultureInfo.InvariantCulture);
                    Program.math_status = true;
                    Program.status = false;
                }
                else
                {
                    Program.memory.Add(-(int.Parse(label1.Text, System.Globalization.CultureInfo.InvariantCulture)));
                    Program.math_status = true;
                    Program.status = false;
                }
                label3.ForeColor = Color.Black;
                label4.ForeColor = Color.Black;
            }
            else if (func == "MC")
            {
                Program.memory.Clear();
                Program.math_status = true;
                Program.status = false;
                label3.ForeColor = Color.Gray;
                label4.ForeColor = Color.Gray;
            }
            else if (func == "MR" && Program.memory.Count != 0)
            {
                int index = Program.memory.Count - 1;
                Program.current = label1.Text = Program.memory[index].ToString(CultureInfo.InvariantCulture);
                Program.math_status = true;
                Program.status = false;
            }
            else if (func == "MS" && label1.Text != "")
            {
                Program.memory.Add(int.Parse(label1.Text, System.Globalization.CultureInfo.InvariantCulture));
                Program.math_status = true;
                Program.status = false;
                label3.ForeColor = Color.Black;
                label4.ForeColor = Color.Black;
            }
        }
        private void button22_Click(object sender, EventArgs e)
        {
            if (label1.Text != "Infinity" && label1.Text != "NaN" && label1.Text != "-Infinity" && label1.Text != "-NaN")
            {
                GetNumbers("0");
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            if (label1.Text != "Infinity" && label1.Text != "NaN" && label1.Text != "-Infinity" && label1.Text != "-NaN")
            {
                GetNumbers("1");
            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            if (label1.Text != "Infinity" && label1.Text != "NaN" && label1.Text != "-Infinity" && label1.Text != "-NaN")
            {
                GetNumbers("2");
            }
        }

        private void button19_Click(object sender, EventArgs e)
        {
            if (label1.Text != "Infinity" && label1.Text != "NaN" && label1.Text != "-Infinity" && label1.Text != "-NaN")
            {
                GetNumbers("3");
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (label1.Text != "Infinity" && label1.Text != "NaN" && label1.Text != "-Infinity" && label1.Text != "-NaN")
            {
                GetNumbers("4");
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            if (label1.Text != "Infinity" && label1.Text != "NaN" && label1.Text != "-Infinity" && label1.Text != "-NaN")
            {
                GetNumbers("5");
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            if (label1.Text != "Infinity" && label1.Text != "NaN" && label1.Text != "-Infinity" && label1.Text != "-NaN")
            {
                GetNumbers("6");
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (label1.Text != "Infinity" && label1.Text != "NaN" && label1.Text != "-Infinity" && label1.Text != "-NaN")
            {
                GetNumbers("7");
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (label1.Text != "Infinity" && label1.Text != "NaN" && label1.Text != "-Infinity" && label1.Text != "-NaN")
            {
                GetNumbers("8");
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (label1.Text != "Infinity" && label1.Text != "NaN" && label1.Text != "-Infinity" && label1.Text != "-NaN")
            {
                GetNumbers("9");
            }
        }

        private void button20_Click(object sender, EventArgs e)
        {
            if (label1.Text != "Infinity" && label1.Text != "NaN" && label1.Text != "-Infinity" && label1.Text != "-NaN")
            {
                GetFunction("+");
            }
        }

        private void button24_Click(object sender, EventArgs e)
        {
            if (label1.Text != "Infinity" && label1.Text != "NaN" && label1.Text != "-Infinity" && label1.Text != "-NaN")
            {
                GetFunction("=");
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            if (label1.Text != "Infinity" && label1.Text != "NaN" && label1.Text != "-Infinity" && label1.Text != "-NaN")
            {
                GetFunction("-");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (label1.Text != "Infinity" && label1.Text != "NaN" && label1.Text != "-Infinity" && label1.Text != "-NaN")
            {
                GetFunction("x");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            GetFunction("C");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (label1.Text != "Infinity" && label1.Text != "NaN" && label1.Text != "-Infinity" && label1.Text != "-NaN")
            {
                GetFunction("CE");
            }
        }

        private void button23_Click(object sender, EventArgs e)
        {
            if (label1.Text != "Infinity" && label1.Text != "NaN" && label1.Text != "-Infinity" && label1.Text != "-NaN")
            {
                GetFunction(",");
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (label1.Text != "Infinity" && label1.Text != "NaN" && label1.Text != "-Infinity" && label1.Text != "-NaN")
            {
                GetFunction("*");
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (label1.Text != "Infinity" && label1.Text != "NaN" && label1.Text != "-Infinity" && label1.Text != "-NaN")
            {
                GetFunction("/");
            }
        }

        private void button21_Click(object sender, EventArgs e)
        {
            if (label1.Text != "Infinity" && label1.Text != "NaN" && label1.Text != "-Infinity" && label1.Text != "-NaN")
            {
                GetFunction("N");
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (label1.Text != "Infinity" && label1.Text != "NaN" && label1.Text != "-Infinity" && label1.Text != "-NaN")
            {
                GetFunction("Q");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (label1.Text != "Infinity" && label1.Text != "NaN" && label1.Text != "-Infinity" && label1.Text != "-NaN")
            {
                GetFunction("^");
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            if (label1.Text != "Infinity" && label1.Text != "NaN" && label1.Text != "-Infinity" && label1.Text != "-NaN")
            {
                GetFunction("D");
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (label1.Text != "Infinity" && label1.Text != "NaN" && label1.Text != "-Infinity" && label1.Text != "-NaN")
            {
                GetFunction("%");
            }
        }
        private void label5_Click(object sender, EventArgs e)
        {
            if (label1.Text != "Infinity" && label1.Text != "NaN" && label1.Text != "-Infinity" && label1.Text != "-NaN")
            {
                GetFunction("M+");
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {
            if (label1.Text != "Infinity" && label1.Text != "NaN" && label1.Text != "-Infinity" && label1.Text != "-NaN")
            {
                GetFunction("M-");
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {
            if (label1.Text != "Infinity" && label1.Text != "NaN" && label1.Text != "-Infinity" && label1.Text != "-NaN")
            {
                GetFunction("MC");
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {
            if (label1.Text != "Infinity" && label1.Text != "NaN" && label1.Text != "-Infinity" && label1.Text != "-NaN")
            {
                GetFunction("MR");
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {
            GetFunction("MS");
        }
    }
}
