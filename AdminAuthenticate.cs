using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.IO;
using System.Web;
using System.Net;


namespace SecureTransferMI
{
    public partial class AdminAuthenticate : Form
    {
        BaseConnection con = new BaseConnection();
        BaseConnection ob = new BaseConnection();
        public static string fc = "";
        public AdminAuthenticate()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public void fillcombo()
        {
           
            con.dr = con.ret_dr("select fid from datatb where status=0");
            
            while (con.dr.Read())
            {
               comboBox1.Items.Add(con.dr[0].ToString());
            }
            
        }
        public string getaccount()
        {
            int c = 0;
            con.dr = con.ret_dr("select isnull(max(transid)+1,500) from transdb");
            if (con.dr.Read())
            {
                c = Convert.ToInt32(con.dr[0].ToString());
            }
            return c.ToString();
        }
        private void AdminAuthenticate_Load(object sender, EventArgs e)
        {
          //  Program.uname2 = "anu123";
            fillcombo();
            textBox3.Text = DateTime.Now.ToString();
            textBox6.Text = getaccount();
            string path = Program.ppath;


            // Open the file to read from.
            using (StreamReader sr = File.OpenText(path))
            {
                string s = "";
                while ((s = sr.ReadLine()) != null)
                {
                    fc=s;
                }
                
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            con.dr = con.ret_dr("select * from datatb where fid='"+comboBox1.SelectedItem.ToString()+"'");

            if (con.dr.Read())
            {
               textBox9.Text= con.dr[1].ToString();
               textBox2.Text = con.dr[2].ToString();
               textBox7.Text = con.dr[3].ToString();
            }
        }
        public void updatedb()
        {
            ob.exec("update datatb set status=1 where fid=" + comboBox1.SelectedItem.ToString() + "");
        }
        public void insertdb()
        {

            int status = 0;
          
            string query = "insert into transdb values(" + textBox6.Text + "," + comboBox1.SelectedItem.ToString() + ",'" + textBox9.Text + "'," + textBox1.Text + ",'" + textBox2.Text + "'," + status + ",'"+textBox3.Text+"')";
            con.exec1(query);
            MessageBox.Show("Transcation Successfully");

        }
        private void Upload(string fileName)
        {
            var client = new WebClient();
            var uri = new Uri("http://localhost/samp/");
            try
            {
                client.Headers.Add("fileName", System.IO.Path.GetFileName(fileName));
                client.UploadFileAsync(uri, fileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btbdesc_Click(object sender, EventArgs e)
        {
            int idata = Convert.ToInt32(fc);
            string str = textBox7.Text;
            Blockchain phillyCoin = new Blockchain();
            int count = 0;
            int chunkSize = 50;
            int stringLength = str.Length;
            for (int i = 0; i < stringLength; i += chunkSize)
            {
                count++;
                if (i + chunkSize > stringLength)
                {
                    chunkSize = stringLength - i;
                    str.Substring(i, chunkSize);

                }
                phillyCoin.AddBlock(new Block(idata,DateTime.Now, null, str.Substring(i, chunkSize)));
                idata++;
            }
            textBox1.Text = count.ToString();
           
            //this.Close();
            richTextBox1.SelectionColor = Color.Green;
            richTextBox1.AppendText("Connecting to Server..........");
            richTextBox1.AppendText("\n\n");
            richTextBox1.SelectionColor = Color.Green;
            richTextBox1.AppendText("Connecting to http://localhost:8081/..........");
            richTextBox1.AppendText("\n\n");
            richTextBox1.SelectionColor = Color.Green;
            richTextBox1.AppendText("Checking Network Credentials..........");
            richTextBox1.AppendText("\n\n");
            richTextBox1.SelectionColor = Color.Green;
            richTextBox1.AppendText("Connection to Chain......");
            richTextBox1.AppendText("\n\n");
            richTextBox1.SelectionColor = Color.Red;
            File.AppendAllText(Application.StartupPath+"\\bchain.json", JsonConvert.SerializeObject(phillyCoin, Formatting.Indented));
        
            richTextBox1.AppendText(JsonConvert.SerializeObject(phillyCoin, Formatting.Indented));
            richTextBox1.AppendText("\n\n");
            richTextBox1.SelectionColor = Color.Green;
            richTextBox1.AppendText($"Is Chain Valid: {phillyCoin.IsValid()}");

            // phillyCoin.Chain[1].Data = "{sender:Henry,receiver:MaHesh,amount:1000}";
            richTextBox1.AppendText("\n\n");
            richTextBox1.SelectionColor = Color.Green;
            // Conso richTextBox1.AppendText("\n\n");le.WriteLine($"Is Chain Valid: {phillyCoin.IsValid()}");
            richTextBox1.AppendText($"Is Chain Valid: {phillyCoin.IsValid()}");
            // Console.WriteLine($"Update hash");
            richTextBox1.AppendText("\n\n");
            richTextBox1.SelectionColor = Color.Green;
            richTextBox1.AppendText($"Update hash");
            phillyCoin.Chain[1].Hash = phillyCoin.Chain[1].CalculateHash();

           // Console.WriteLine($"Is Chain Valid: {phillyCoin.IsValid()}");
            richTextBox1.AppendText($"Is Chain Valid: {phillyCoin.IsValid()}");
            // Console.WriteLine($"Update the entire chain");
            richTextBox1.AppendText("\n\n");
            richTextBox1.SelectionColor = Color.Green;
            richTextBox1.AppendText($"Update the entire chain");
            phillyCoin.Chain[2].PreviousHash = phillyCoin.Chain[1].Hash;
            phillyCoin.Chain[2].Hash = phillyCoin.Chain[2].CalculateHash();
            phillyCoin.Chain[3].PreviousHash = phillyCoin.Chain[2].Hash;
            phillyCoin.Chain[3].Hash = phillyCoin.Chain[3].CalculateHash();
            
            // Console.WriteLine($"Is Chain Valid: {phillyCoin.IsValid()}");
            richTextBox1.AppendText($"Is Chain Valid: {phillyCoin.IsValid()}");

            //   Console.ReadKey();
            StreamWriter sw = new StreamWriter(Program.ppath);
            
            sw.WriteLine(idata);
           
           
            sw.Close();
              insertdb();
               updatedb();

        }
    }
}
