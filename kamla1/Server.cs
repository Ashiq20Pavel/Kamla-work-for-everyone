using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NetworksApi.TCP.SERVER;
using System.Net;
using System.Net.Sockets;

namespace Server
{
    public delegate void Updatetext(string text);
    public delegate void UpdateListBox(ListBox list,string value,bool Remove);
    public partial class Server : Form
    {
        NetworksApi.TCP.SERVER.Server serverside;
        public static string recruiter, seeker;
        public Server()
        {
            InitializeComponent();
        }

        private void Server_Load(object sender, EventArgs e)
        {
            serverside = new NetworksApi.TCP.SERVER.Server(GetLocalIPAddress(), "90");
            serverside.OnClientConnected += new OnConnectedDelegate(serverside_OnClientConnected);
            serverside.OnClientDisconnected += new OnDisconnectedDelegate(serverside_OnClientDisconnected);
            serverside.OnDataReceived += new OnReceivedDelegate(serverside_OnDataReceived);
            serverside.OnServerError += new OnErrorDelegate(serverside_OnServerError);
            serverside.Start();
            
        }

        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("Local IP Address Not Found!");
        }

        void ChangeTextBoxContents(string text)
        {
            if (txtServerLog.InvokeRequired)
            {
                Invoke(new Updatetext(ChangeTextBoxContents), new Object[] { text });
            }
            else
            {
                txtServerLog.Text += text + "\r\n";
            }
        }

        void ChangeListBox(ListBox list,string value,bool Remove)
        {
            if (list.InvokeRequired)
            {
                Invoke(new UpdateListBox(ChangeListBox),new Object[]{list,value,Remove});
            }
            else
            {
                if (Remove)
                {
                    list.Items.Remove(value);
                }
                else
                {
                    list.Items.Add(value);
                }
            }
        }

        private void serverside_OnDataReceived(object Sender, ReceivedArguments R)
        {
            if (R.Name == Server.seeker)
            {
                serverside.SendTo(Server.recruiter, R.Name + " : " + R.ReceivedData);
                //serverside.BroadCast(R.Name + " : " + kamla1.Form1.recruiterName);
                ChangeTextBoxContents(R.Name + " : " + R.ReceivedData);
            }

            else if (R.Name == Server.recruiter)
            {
                serverside.SendTo(Server.seeker, R.Name + " : " + R.ReceivedData);
                //serverside.BroadCast(R.Name + " : " + kamla1.Form1.seekerName);
                ChangeTextBoxContents(R.Name + " : " + R.ReceivedData);
            }
            
        }

        private void serverside_OnServerError(object Sender, ErrorArguments R)
        {
            MessageBox.Show(R.ErrorMessage+"\n"+R.Exception);
            ChangeTextBoxContents(R.ErrorMessage);
        }
        
        private void serverside_OnClientDisconnected(object Sender, DisconnectedArguments R)
        {
            //serverside.BroadCast(R.Name+" Has Disconnected ");
            ChangeTextBoxContents(R.Name + " Has Disconnected at : " + DateTime.Now.ToShortTimeString());
            ChangeListBox(listName,R.Name,true);
            ChangeListBox(listIp, R.Ip, true);
            
        }

        private void serverside_OnClientConnected(object Sender, ConnectedArguments R)
        {
            //serverside.BroadCast(R.Name+" Has Connected ");
            ChangeTextBoxContents(R.Name + " Has Connected at : " + DateTime.Now.ToShortTimeString());
            ChangeListBox(listName, R.Name, false);
            ChangeListBox(listIp, R.Ip, false);
        }

        private void Server_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Environment.Exit(System.Environment.ExitCode);
        }

        private void listName_SelectedIndexChanged(object sender, EventArgs e)
        {
            listIp.SelectedIndex = listName.SelectedIndex;
        }

        private void listIp_SelectedIndexChanged(object sender, EventArgs e)
        {
            listName.SelectedIndex = listIp.SelectedIndex;
        }

        private void btnPrivate_Click(object sender, EventArgs e)
        {
            serverside.SendTo((string)listName.SelectedItem,txtMessage.Text);
            ChangeTextBoxContents("Server: "+txtMessage.Text);
        }

        private void btnBradcast_Click(object sender, EventArgs e)
        {
            serverside.BroadCast(txtMessage.Text);
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            serverside.DisconnectClient((string)listName.SelectedItem);
        }

        private void txtServerLog_TextChanged(object sender, EventArgs e)
        {

        }


    }
}
