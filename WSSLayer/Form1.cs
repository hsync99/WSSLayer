using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WSSLayer
{
    public partial class Form1 : Form
    {
        public string xmlSign;
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {


            // Set initial directory (optional)
            openFileDialog1.InitialDirectory = "C:\\";

            // Set the title of the dialog
            openFileDialog1.Title = "Select a File";

            // Set the file filter (optional)
            openFileDialog1.Filter = "XML (*.xml)|*.xml|All Files (*.*)|*.*";

            // Allow selection of multiple files (optional)
            openFileDialog1.Multiselect = false;

            // Show the dialog and capture the result
            DialogResult result = openFileDialog1.ShowDialog();

            // Process the selected file
            if (result == DialogResult.OK)
            {
                // Get the selected file name and display it
                string selectedFileName = openFileDialog1.FileName;
                textBox1.Text = selectedFileName;
                
                string fileContent = File.ReadAllText(selectedFileName);
                xmlSign = fileContent;  
                MessageBox.Show("file content: " + fileContent);
                // You can further process the selected file here, e.g., load its contents, etc.
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            string serverUrl = "http://127.0.0.1:13579/";

            WebSocketServer server = new WebSocketServer();
            await server.StartWebSocketServer(serverUrl);
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string pattern = @"<document>.*?</document>";

            // Replace using Regex.Replace method
            string replacedString = Regex.Replace(xmlSign, pattern, "<document>" + textBox3.Text + "</document>");
            
            Dictionary<string, string> dictionaryData = new Dictionary<string, string>
            {
                { "signature", replacedString }
            };
            string jsonString = JsonConvert.SerializeObject(dictionaryData);
            textBox2.Text = jsonString;

        }
    }
}
