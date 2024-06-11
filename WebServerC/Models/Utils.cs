namespace WebServer
{
    public struct HTTPRequestData{
        public string method;
        public string path;
        public string httpVersion;
        public string header;
        public string body;
    }
}