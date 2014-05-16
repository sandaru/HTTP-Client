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
        //Copy of text field data
        private string[] data = null;
        private string application_id = null;
        public Form1()
        {
            InitializeComponent();
        }
        /// <summary>
        /// When form load
        /// Authenticate the application using UID
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            if (UIDMatch())//If application is registered and authorized
            {
                setDisplayValues(null);
                applicationOn();
            }
            else//If not secure
            {
                MessageBox.Show("Register your application now");
                Application.Exit();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
             connectedDeviceDetails();
             getConnectedDeviceState();
        }

        /// <summary>
        /// iterative method to get response
        /// this is listening for the server state related to the client
        /// </summary>
        /// <param name="value"></param>
        public void getConnectedDeviceState()
        {
            timer1.Start();
        }

        /// <summary>
        /// Method to send request with the PORT and public IP
        /// Run when application is loading.
        /// </summary>
        /// <param name="value"></param>
        public void setDisplayValues(string value)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(setDisplayValues), new object[] { value });
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
        /// !!!Don't change the port of outgoing request(3478)
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
        /// Time thread to request availability states of connected devices
        /// run once for two seconds
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

                    var values = new NameValueCollection();
                    values["uid"] = application_id;

                    var response = Encoding.ASCII.GetString(client.UploadValues
                           ("http://sandarurdp.net76.net/server.php", "POST", values));
                    richTextBoxRespose.Text = response;

                }
            }
            catch (Exception)
            {
                timer1.Stop();
                MessageBox.Show("Error in Request sending procedure");
            }
        }

        /// <summary>
        /// Send Port data via HTTP
        /// can change the HTTP destination by replacing the URL
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
                    values["comName"] = System.Environment.MachineName;
                    values["uid"] = application_id;


                    var ret = client.UploadValues("http://sandarurdp.net76.net/appOn.php", "POST", values);
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
        /// uses MD5 Encryption algorithm to verify the application
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
                            application_id = line;//Assign the application id gloabally 
                            var ret = client.UploadValues("http://sandarurdp.net76.net/authenticate.php", "POST", values);
                            MessageBox.Show(Encoding.ASCII.GetString(ret));
                            if (Encoding.ASCII.GetString(ret)=="true")
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
                application_id = MD5Hash(dataToEncript);//Assign the application id gloabally 
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
        /// This method returns array of device details that are paired with the client device
        /// data represent as 2x2 array
        /// Eg. [[ip,port],[ip,port]...]
        /// </summary>
        /// <returns></returns>
        public List<string[]> connectedDeviceDetails()
        {
            List<string[]> connectedDevices = new List<string[]>();
            using (var client = new WebClient())
            {
                var values = new NameValueCollection();
                values["uid"] = application_id;
                var ret = client.UploadValues("http://sandarurdp.net76.net/exchange.php", "POST", values);
                string[] details = Encoding.ASCII.GetString(ret).Split(':');
                connectedDevices.Add(details);
                MessageBox.Show(Encoding.ASCII.GetString(ret));
            }
            return connectedDevices;
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
        
        /// <summary>
        /// Change the application state when form is closed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            using (var client = new WebClient())
            {
                var values = new NameValueCollection();
                values["uid"] = application_id;
                client.UploadValues("http://sandarurdp.net76.net/exchange.php", "POST", values);
            }
        }

    }
}
