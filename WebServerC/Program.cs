
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace WebServer
{
    public class Program
    {
         

        public static void Main(string[] args)
        {
            Start();
        }

        public static void Start()
        {
            IPHostEntry ipHostInfo =Dns.GetHostEntry("localhost");
            IPAddress iPAddress = ipHostInfo.AddressList[1];
            IPEndPoint localEndPoint = new IPEndPoint(iPAddress,42069);
            try
            {
                using Socket listener = new Socket(localEndPoint.AddressFamily,SocketType.Stream,ProtocolType.Tcp);
                listener.Bind(localEndPoint);
                listener.Listen(100);

                Console.WriteLine("Awaiting for a message");
                Socket clientConnection = listener.Accept();
                while (true)
                {
                    byte[] buffer = new byte[2048];
                    int receivedData = clientConnection.Receive(buffer,SocketFlags.None);
                    string data = Encoding.UTF8.GetString(buffer, 0 , receivedData);
                    Console.WriteLine(data);
                    byte[] returnMsg = Encoding.UTF8.GetBytes("" +
                "HTTP/1.1 404 Not Found\r\n" +
                "Content-Type: text/html\r\n" +
                "\r\n" +"Awaiting for a message");
                    clientConnection.Send(returnMsg);
                }
            }
            catch (System.Exception)
            {
                
                throw;
            }
        }

    }
}