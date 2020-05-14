using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace BoxTransferTest.ViewUc
{
    public partial class UcLoadPort : UserControl
    {
        int counter = 0;
        string beenReadData;
        bool flag;
        bool errorcode = true;
        TcpClient _clientSocket = new TcpClient();
        NetworkStream _streamFromServer = default(NetworkStream);

        public UcLoadPort()
        {
            InitializeComponent();
        }

        private void LP_ConnectBtn_Click(object sender, EventArgs e)
        {
            if (errorcode)
            {
                errorcode = false;

                _clientSocket = new TcpClient();
                try
                {
                    _clientSocket.Connect(LP_IPList.Text, Int32.Parse(LP_Port.Text));
                }
                catch (Exception except)
                {
                    MessageBox.Show(except.Message);
                }
                if (_clientSocket.Connected)
                {
                    labConnection.Text = "Connected";
                    labConnection.BackColor = Color.Green;
                }
                else
                {
                    labConnection.Text = "Disconnected";
                    labConnection.BackColor = Color.Red;
                }
                Thread controlThread = new Thread(getMessage);
                controlThread.Start();

            }
            else
            {
                errorcode = false;

                _clientSocket = new TcpClient();
                try
                {
                    _clientSocket.Connect(LP_IPList.Text, Int32.Parse(LP_Port.Text));
                }
                catch (Exception except)
                {
                    MessageBox.Show(except.Message);
                }

                if (_clientSocket.Connected)
                {
                    labConnection.Text = "Connected";
                    labConnection.BackColor = Color.Green;
                }
                else
                {
                    labConnection.Text = "Disconnected";
                    labConnection.BackColor = Color.Red;
                }
                Thread controlThread = new Thread(getMessage);
                controlThread.Start();
            }
        }

        private void getMessage()
        {
            bool closed = false;
            byte[] testByte = new byte[1];
            string toReturnData = "";
            string finalstr = "";
            while (_clientSocket.Connected && !closed)
            {
                if (_clientSocket.Available > 0)
                {
                    _streamFromServer = _clientSocket.GetStream();
                    //var bufferSize = _clientSocket.ReceiveBufferSize;
                    byte[] inStreamBuffer = new byte[1024];
                    _streamFromServer.Read(inStreamBuffer, 0, inStreamBuffer.Length);
                    // toReturnData = Encoding.ASCII.GetString(inStreamBuffer);

                    for (int y = 0; y < inStreamBuffer.Length; y++)
                    {

                        if (Convert.ToChar(inStreamBuffer[y]).ToString() == "\0") break;
                        toReturnData += Convert.ToChar(inStreamBuffer[y]).ToString();
                        finalstr = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + " S >> : " + toReturnData;
                    }
                    beenReadData = finalstr;
                    msg();
                    finalstr = "";
                    toReturnData = "";
                }

                try
                {
                    // peek to test connection
                    if (_clientSocket.Connected && _clientSocket.Client.Poll(0, SelectMode.SelectRead))
                        closed =
                    _clientSocket.Client.Receive(testByte, SocketFlags.Peek) == 0;
                }
                catch (SocketException socketException)
                {
                    closed = true;
                    MessageBox.Show(socketException.Message);
                }
                Thread.Sleep(20);
            }
            _clientSocket.Close();

            try
            {
                _streamFromServer.Close();
            }
            catch (Exception)
            {
                flag = true;

                if (counter == 0)
                {
                    errorcode = true;
                    counter++;
                }
                else
                {
                    errorcode = false;
                    counter = 0;
                }

                disconnectmsg();
                MessageBox.Show("Connection Closed!");
            }
            if (!flag)
            {
                if (counter == 0)
                {
                    errorcode = true;
                    counter++;
                }
                else
                {
                    errorcode = false;
                    counter = 0;
                }

                MessageBox.Show("Connection Closed!");
                disconnectmsg();
                flag = false;
            }
        }

        private void msg()
        {

            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(msg));

            }
            else
            {
                richTextBox1.Text += beenReadData + Environment.NewLine;
                richTextBox1.SelectionStart = richTextBox1.TextLength;
                richTextBox1.ScrollToCaret();
            }
        }

        private void disconnectmsg()
        {

            if (InvokeRequired)
            {
                this.Invoke(new MethodInvoker(delegate ()
                {

                    labConnection.Text = "Disconnected";
                    labConnection.BackColor = Color.Red;


                }));

            }
            else
            {
                labConnection.Text = "Disconnected";
                labConnection.BackColor = Color.Red;
            }
        }

        private void LP_SendMsgList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
