using System.Net;
using System.Net.Sockets;
using System.Text;

namespace WebServer
{
    public static class Server
    {
        private static Socket listener;

        public static void Start(){
            InitializeListener();
            listener.Listen(100);
            RunServer(listener);
        }

        private static void InitializeListener(){
            IPHostEntry ipHostInfo =Dns.GetHostEntry("localhost");
            IPAddress iPAddress = ipHostInfo.AddressList[1];
            IPEndPoint localEndPoint = new IPEndPoint(iPAddress,42069);
            Console.WriteLine("Listening on : http://"+ localEndPoint.Address.ToString()+"/");
            listener = new Socket(localEndPoint.AddressFamily,SocketType.Stream, ProtocolType.Tcp);
            listener.Bind(localEndPoint);
        }

        private static void RunServer(Socket localListener){
            while (true){
                Socket context = localListener.Accept();
                try
                {
                    ReturnResponse(context);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }

        private static void ReturnResponse(Socket localContext){
            byte[] buffer = new byte[2048];
            int receivedData = localContext.Receive(buffer,SocketFlags.None);
            string data = Encoding.UTF8.GetString(buffer, 0 , receivedData);
            Console.WriteLine(data);
            byte[] returnMsg = Encoding.UTF8.GetBytes("" +
                "HTTP/1.1 404 Not Found\r\n" +
                "Content-Type: text/html\r\n" +
                "\r\n" +"Awaiting for a message");
            localContext.Send(returnMsg);
        }
    }
}