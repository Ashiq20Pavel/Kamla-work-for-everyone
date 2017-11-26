using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NetworksApi.TCP.CLIENT;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace kamla1
{
    public delegate void Updatetext(string text);
    public partial class ChatWindow : Form
    {
        Client clientside;
        public static string name;
        public static string other;
        public ChatWindow()
        {
            InitializeComponent();
        }

        private void ChatWindow_Load(object sender, EventArgs e)
        {
            clientside = new Client();
            clientside.ServerIp = GetLocalIPAddress();
            clientside.ServerPort = "90";
            clientside.ClientName = name;
            clientside.Connect();
            clientside.OnClientConnected += new OnClientConnectedDelegate(clientside_OnClientConnected);
            clientside.OnClientConnecting += new OnClientConnectingDelegate(clientside_OnClientConnecting);
            clientside.OnClientDisconnected += new OnClientDisconnectedDelegate(clientside_OnClientDisconnected);
            clientside.OnClientError += new OnClientErrorDelegate(clientside_OnClientError);
            clientside.OnDataReceived += new OnClientReceivedDelegate(clientside_OnDataReceived);
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

        void ChangeText(string text)
        {
            if (ChatLog.InvokeRequired)
            {
                Invoke(new Updatetext(ChangeText), new Object[] { text });
            }
            else
                ChatLog.Text += text + "\r\n";
        }

        private void clientside_OnDataReceived(object Sender, ClientReceivedArguments R)
        {
            ChangeText(R.ReceivedData);
        }

        private void clientside_OnClientError(object Sender, ClientErrorArguments R)
        {
            MessageBox.Show(R.ErrorMessage+"\n"+R.Exception);
        }

        private void clientside_OnClientDisconnected(object Sender, ClientDisconnectedArguments R)
        {
            ChangeText(R.EventMessage);
        }

        private void clientside_OnClientConnecting(object Sender, ClientConnectingArguments R)
        {
            ChangeText(R.EventMessage);
        }

        private void clientside_OnClientConnected(object Sender, ClientConnectedArguments R)
        {
            ChangeText(R.EventMessage);
        }

        private void SendButton_Click(object sender, EventArgs e)
        {
            if(clientside.IsConnected && ChatBox!=null)
            {
                Server.Server.recruiter = name;
                Server.Server.seeker = other;
                clientside.Send(ChatBox.Text);
                ChangeText("Me : "+ChatBox.Text);
                ChatBox.Clear();

            }
        }

        private void ChatBox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Enter)
            {
                SendButton_Click(sender,e);
            }
        }

        private void ChatWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            try {  }
            catch(Exception)
            {}
            
        }
    }
}
