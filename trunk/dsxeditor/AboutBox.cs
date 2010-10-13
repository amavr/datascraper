using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

// 1.4 - добавлено действие вычисление даты


namespace DataScraper
{
    partial class AboutBox : Form
    {
        public AboutBox()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string url = "http://amavr.narod.ru/#scraper";
            System.Diagnostics.Process.Start(url);
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string url = String.Format("mailto:{0}?subject=DataScraper", (sender as LinkLabel).Text);
            System.Diagnostics.Process.Start(url);
        }

    }
}
