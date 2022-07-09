using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel2Other;

namespace Excel2Other.Test
{
    public partial class Form1 : Form
    {
        ExcelReader excelReader = new ExcelReader();
        public Form1()
        {
            InitializeComponent();
            JsonSettings settings = new JsonSettings();
            excelReader.CreateConverter(ConvertType.Json,settings);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = excelReader.GetContent("ExampleData.xlsx", ConvertType.Json)[0].content.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = excelReader.GetContent("test.xlsx", ConvertType.Json)[0].content.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = excelReader.GetContent("ExampleData.xlsx", ConvertType.Json)[0].content.ToString();

        }
    }
}
