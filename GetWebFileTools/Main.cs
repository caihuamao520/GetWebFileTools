using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace GetWebFileTools
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void Form6_Load(object sender, EventArgs e)
        {

        }

        private void webBrowser1_NewWindow(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            try
            {
                string url = this.webBrowser1.Document.ActiveElement.GetAttribute("href");

                this.webBrowser1.Url = new Uri(url);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtURL.Text)) return;

            this.webBrowser1.Url = new Uri(this.txtURL.Text); ;
        }

        private void btnGetViewDownURL_Click(object sender, EventArgs e)
        {
            try
            {
                HtmlElementCollection hec = this.webBrowser1.Document.Body.GetElementsByTagName("video");
                if (hec.Count > 0)
                {
                    this.txtViewDownURL.Text = hec[0].GetAttribute("src");

                    Clipboard.SetText(this.txtViewDownURL.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string strOutHtml = this.webBrowser1.Document.Body.OuterHtml;
                if (!string.IsNullOrEmpty(strOutHtml))
                {
                    string regMatch = "<div\\sclass=\"docBox\"\\s.+downloadurl=\"(?<downUrl>.+)\\?response-content-disposition=.+;filename=(?<fileName>.+\\.ppt)\".+</div>";
                    Regex reg = new Regex(regMatch);
                    if (reg.IsMatch(strOutHtml))
                    {
                        Match m = reg.Match(strOutHtml);
                        if (m.Groups.Count > 0)
                        {
                            this.txtPPTDownURL.Text = m.Groups["downUrl"].Value;
                            this.txtPPTName.Text = m.Groups["fileName"].Value;

                            Clipboard.SetText(this.txtPPTDownURL.Text);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
