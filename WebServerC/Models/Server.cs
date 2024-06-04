using System.Net;
using System.Net.Sockets;
using System.Text;

namespace WebServer
{
    public static class Server
    {
        private static Socket listener;

        public static void Start(){
            Console.WriteLine("Starting Server");
            InitializeListener();
            Task.Run(()=> RunServer());
            Console.WriteLine("Server Started");
        }

        private static void InitializeListener(){
            IPHostEntry ipHostInfo =Dns.GetHostEntry("localhost");
            IPAddress iPAddress = ipHostInfo.AddressList[1];
            IPEndPoint localEndPoint = new IPEndPoint(iPAddress,42069);
            Console.WriteLine("Listening on : http://"+ localEndPoint.Address.ToString()+"/");
            listener = new Socket(localEndPoint.AddressFamily,SocketType.Stream, ProtocolType.Tcp);
            listener.Bind(localEndPoint);
            listener.Listen(100);
            Console.WriteLine("Server initialized and listening...");
        }

        private static async Task RunServer(){
            Console.WriteLine("Server is running...");
            while (true){
                try
                {
                    Socket context = await listener.AcceptAsync();
                    await ReturnResponse(context);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }

        private static async Task ReturnResponse(Socket localContext){
            try
            {
                byte[] buffer = new byte[2048];
                int receivedData = await localContext.ReceiveAsync(buffer,SocketFlags.None);
                string data = Encoding.UTF8.GetString(buffer, 0 , receivedData);
                Console.WriteLine(data);
                byte[] returnMsg = Encoding.UTF8.GetBytes("" +
                    "HTTP/1.1 404 Not Found\r\n" +
                    "Content-Type: text/html\r\n" +
                    "\r\n" +"Awaiting for a message");
                await localContext.SendAsync(returnMsg);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}