using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace IPS_TokenTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btn_breakdown_Click(object sender, EventArgs e)
        {
            var dicVariables = BreakResponseString(richTextBox1.Text.Trim());
            string error = dicVariables.ToList().Find(a => a.Key == "ERROR_CODE").Value;
            string hash = dicVariables.ToList().Find(a => a.Key == "HASH").Value;
            string UUID = ReadFromXmlString(dicVariables.ToList().Find(a => a.Key == "PTRECEIPT").Value);
            richTextBox2.Clear();
            richTextBox2.AppendText(error + Environment.NewLine + hash + Environment.NewLine + UUID);
            richTextBox2.AppendText(Environment.NewLine);
          
        }

        private string ReadFromXmlString(string UUIDXmlString)
        {
            string UUio = "";
            using (XmlReader reader = XmlReader.Create(new StringReader(UUIDXmlString)))
            {
                while (reader.Read())
                {
                    if (reader.Name.ToString().ToUpper() == "txn_uuid".ToUpper())
                    {
                        UUio = reader.ReadString();
                        
                    }
                }
            }
            return UUio;
        }


      


        Dictionary<string,string> BreakResponseString(string input) 
        {
            Dictionary<string, string> theList = new Dictionary<string, string>();
            var breakdown = input.Split('&').ToList();
            if (breakdown.Count > 0)
            {
                foreach (var item in breakdown)
                {
                    var secondbreak = item.Split('=');
                    if(secondbreak.Length == 2)
                    {
                        var converted = "";
                        switch (secondbreak[0]) {
                            case "PTRECEIPT":
                                converted = Encoding.ASCII.GetString(FromHex(secondbreak[1]));
                                theList.Add(secondbreak[0], converted);
                                break;
                            default:
                                theList.Add(secondbreak[0], secondbreak[1]);
                                break;
                        }
                       
                    }
                }
                var toLst = theList.ToList();
            }
            else {
                theList.Add("ERROR_CODE", "NullException");
            }
            return theList;
        }

        public static byte[] FromHex(string hex)
        {
            byte[] raw = new byte[hex.Length / 2];
            for (int i = 0; i < raw.Length; i++)
            {
                raw[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
            }
            return raw;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AdvanceReadFromXmlString("<res><txn_amt>400.00</txn_amt><cur>LKR</cur><server_time>2020-11-19 10:42:25</server_time><reason>ERR_04_28 - Internal error occurred. Contact bank administrator with error code.|ERR_04_31 - Credit sale failed.</reason><ipg_txn_id>VIDUNATEXT160576274503141</ipg_txn_id><bank_ref_id>39485</bank_ref_id><acc_no>376657XXXXX4809</acc_no><name>test</name><action>SaleTxn</action><lang>eng</lang><txn_status>REJECTED</txn_status><mer_txn_id>81945</mer_txn_id></res>");
        }


        private string AdvanceReadFromXmlString(string UUIDXmlString)
        {
            richTextBox2.Clear();
            string UUio = "";
            using (XmlReader reader = XmlReader.Create(new StringReader(UUIDXmlString)))
            {
                while (reader.Read())
                {
                    richTextBox2.AppendText("Tag -" + reader.Name.ToString() + " : " + reader.ReadString() + Environment.NewLine);

                    if (reader.Name.ToString().ToUpper() == "txn_uuid".ToUpper())
                    {
                        UUio = reader.ReadString();

                    }
                }
            }
            return UUio;
        }
    }
}
