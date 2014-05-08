using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using LumiSoft.Net.STUN.Client;
using System.Net.Sockets;
using System.Collections.Specialized;

namespace HTTP_Client
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //getResponse();
            //sendRequest(null);
            applicationOn();
        }

        /// <summary>
        /// iterative method to get response
        /// this is listning for the server state related to the client
        /// </summary>
        /// <param name="value"></param>

        public void getResponse()
        {
            //timer1.Start();
        }

        /// <summary>
        /// Method to send request with the PORT and public IP
        /// Run when application is loading.
        /// </summary>
        /// <param name="value"></param>
        public void sendRequest(string value)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string> (sendRequest), new object[] {value});
                return;
            }
            String[] stundata = getStundata();
            textNatType.Text = stundata[0];//NAt type
            textPort.Text = stundata[1];//Public Port
            textPublicIP.Text = stundata[2];//Public IP
            textMachineName.Text = System.Environment.MachineName;//Computer name
            textDataString.Text = stundata[0] + ":" + stundata[1] + ":" + stundata[2] + ":" + System.Environment.MachineName;
        }

        /// <summary>
        /// Connect to a stun server to get the public IP and PORT
        /// </summary>
        /// <returns></returns>
        private string[] getStundata()
        {
            string[] stundata = new String[3];
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            socket.Bind(new IPEndPoint(IPAddress.Any, 0));
            STUN_Result result = STUN_Client.Query("stunserver.org", 3478, socket);
            stundata[0] = result.NetType.ToString();
            if (result.NetType != STUN_NetType.UdpBlocked)
            {
                String[] temp = result.PublicEndPoint.ToString().Split(':');
                stundata[1] = temp[1];
                stundata[2] = temp[0];
            }
            else
            {
                MessageBox.Show("Error Public IP");
            }
            return stundata;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                using (var client = new WebClient())
                {
                    try
                    {
                        var response = client.DownloadString("http://localhost:8787/");
                        richTextBoxRespose.Text += response + Environment.NewLine;
                    }
                    catch (ArgumentException)//if url is empty
                    {
                        MessageBox.Show("Download String is null or wrong");
                    }
                }
            }
            catch (Exception)
            {
                timer1.Stop();
                MessageBox.Show("Error in Request sending procedure");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //sendRequest(null);
           // applicationOn();
        }

        private void applicationOn()
        {
            try
            {
                using (var client = new WebClient())
                {
                    var values = new NameValueCollection();
                    values["port"] = "hello";

                    var ret = client.UploadValues("http://localhost:8787/postval", values);
                    MessageBox.Show(Encoding.ASCII.GetString(ret));
                }
            }
            catch (WebException ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
        }


    }
}
