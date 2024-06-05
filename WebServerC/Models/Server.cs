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
                    await HandleRequest(context);
                    await ReturnResponse(context);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }

        private static async Task HandleRequest(Socket localContext){
            string requestString;
            requestString = await GetData(localContext);
            //ADD VERIFICATION IF STRING IS NULL TO PARSE THE REQUEST DATA
            Console.WriteLine(requestString);
        }

        private static async Task ReturnResponse(Socket localContext){
            try{
                byte[] returnMsg = Encoding.UTF8.GetBytes("" +
                    "HTTP/1.1 404 Not Found\r\n" +
                    "Content-Type: text/html\r\n" +
                    "\r\n" +"<html><head><meta http-equiv='content-type' content='text/html; charset=utf-8'/></head>Hello Browser!</html>");
                await localContext.SendAsync(returnMsg);
                localContext.Shutdown(SocketShutdown.Both);
            } catch (Exception ex){
                Console.WriteLine(ex);
            } finally {
                localContext.Close();
            }
        }

        private static async Task<string> GetData(Socket localContext){
            byte[] buffer = new byte[1024];
            string data = null;
            try{
                while (localContext.Available>0){
                    Console.WriteLine("Looping read Data");
                    int receivedData = await localContext.ReceiveAsync(buffer,SocketFlags.None);
                    data += Encoding.UTF8.GetString(buffer, 0 , receivedData);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return data;
        }
    }

    struct HTTPRequest{
        string method;
        string path;
        string httpVersion;
        string header;
        string body;
    }
}