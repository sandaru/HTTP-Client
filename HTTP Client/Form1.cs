/*TODO:
 * Implement a verifying technique for client applications which can veryfy otherized client applications.
 * dynamic MD5 UID code canbe used
 */
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
using System.IO;
using System.Security.Cryptography;

namespace HTTP_Client
{
    public partial class Form1 : Form
    {
        //Coppy of text field data
        private string[] data = null;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UIDMatch();
            //getResponse();
        }

        /// <summary>
        /// iterative method to get response
        /// this is listning for the server state related to the client
        /// </summary>
        /// <param name="value"></param>
        public void getResponse()
        {
            timer1.Start();
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
            data = stundata;
        }

        /// <summary>
        /// Connect to a stun server to get the public IP and PORT
        /// Call stun server ("stunserver.org")
        /// !!!Dont change the port of outgoing request(3478)
        /// </summary>
        /// <returns></returns>
        private string[] getStundata()
        {
            string[] stundata = new String[3];
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            socket.Bind(new IPEndPoint(IPAddress.Any, 0));
            STUN_Result result = STUN_Client.Query("stunserver.org", 3478, socket);//Call stun server
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
 
        /// <summary>
        /// Time thread to request availability states
        /// run onece a two seconds
        /// can change the HTTP destination by replacing the URL
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
           sendRequest(null);
           applicationOn();
        }


        /// <summary>
        /// Send Port data via HTTP
        /// can change the http destination by replacing the URL
        /// </summary>
        private void applicationOn()
        {
            try
            {
                using (var client = new WebClient())
                {
                    var values = new NameValueCollection();
                    values["port"] = data[1];
                    values["ip"] = data[2];
                    values["comName"] = System.Environment.MachineName;;


                    var ret = client.UploadValues("http://localhost/TestAPP/index.php","POST",values);
                    MessageBox.Show(Encoding.ASCII.GetString(ret));
                }
            }
            catch (WebException ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
        }


        /// <summary>
        /// Verify the application
        /// uses MD5 Encription algorithm to verify the application
        /// </summary>
        /// <returns></returns>
        private bool UIDMatch()
        {
            //Create complex file name
            string filename = MD5Hash("uiddfile");
            //If not the first time
            if (File.Exists(filename+".uid"))
            {
                // Read a text file using StreamReader
                using (System.IO.StreamReader sr = new System.IO.StreamReader(filename+".uid"))
                {
                    Boolean match = false;
                    String line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        using (var client= new WebClient())
                        {
                            var values = new NameValueCollection();
                            values["uid"] = line;
                            var ret = client.UploadValues("http://localhost/TestAPP/authenticate.php", "POST", values);
                            MessageBox.Show(Encoding.ASCII.GetString(ret));
                            if (Encoding.ASCII.GetString(ret) == "true")
                            {
                                match=true;
                            }
                            else
                            {
                                match=false;
                            }
                        }
                    }
                    return match;
                }
            }
            //If this is first time
            else
            {
                File.Create(filename+".uid").Dispose();
                String dataToEncript = data[0] + ":" + data[1] + data[2];
                File.WriteAllText((filename + ".uid"), MD5Hash(dataToEncript));
                using (var client = new WebClient())
                {
                    var values = new NameValueCollection();
                    values["uid"] = dataToEncript;
                    var ret = client.UploadValues("http://localhost/TestAPP/insertNewID.php", "POST", values);
                    MessageBox.Show(Encoding.ASCII.GetString(ret));
                }
                MessageBox.Show("Registered");
                return true;
            }
        }


        /// <summary>
        /// Encrypt given text using MD5 algorithm
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string MD5Hash(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();            
            //compute hash from the bytes of text
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));
            //get hash result after compute it
            byte[] result = md5.Hash;
            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                //change it into 2 hexadecimal digits
                //for each byte
                strBuilder.Append(result[i].ToString("x2"));
            }
            return strBuilder.ToString();
        }

    }
}
