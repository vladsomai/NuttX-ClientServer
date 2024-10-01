using System.Data;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NuttX_Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            StartServer();
            //ConnectToNuttx();
        }

        public void StartServer()
        {
            int listeningPort = 5471;
            TcpListener server = new TcpListener(IPAddress.Any, listeningPort);
            server.Start();

            Console.WriteLine("Server listening on port: " + listeningPort);
            while (true)
            {
                TcpClient client = server.AcceptTcpClient();
                NetworkStream stream = client.GetStream();

                while (true)
                {
                    byte[] data = new byte[256];
                    int bytesRead = stream.Read(data, 0, data.Length);
                    string dataReceived = Encoding.ASCII.GetString(data, 0, bytesRead);

                    //Console.WriteLine("Received from client: " + dataReceived);
                }

            }

        }

        public void ConnectToNuttx()
        {

            string serverIpAddress = "192.168.1.29";
            int serverPort = 5471;

            TcpClient client = new TcpClient();

            client.Connect(serverIpAddress, serverPort);

            NetworkStream stream = client.GetStream();

            string dummy = new string('*', 100 * 1024 * 1024);
            while (true)
            {
                byte[] data = Encoding.ASCII.GetBytes(dummy);
                stream.Write(data, 0, data.Length);
            }
        }
    }
}