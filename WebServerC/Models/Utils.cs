using System.Collections;

namespace WebServer
{
    public struct HTTPRequestData{
        public Methods method;
        public string path;
        public string parms;
        public string httpVersion;
        public string header;
        public string body;

        public void SetMethodandPath(string reqMsg){
            string[] sections = reqMsg.Split(" ");
            httpVersion= sections[2];
            method = SetMethod(sections[0]);
            int hasQuery = sections[1].IndexOf("?");
            if(hasQuery>0)
            {
                path = sections[1].Substring(0,hasQuery);
                parms = sections[1].Substring(hasQuery+1);
                return;
            }
            path = sections[1];
            parms = null;
        }

        private Methods SetMethod(string methodMsg){
            switch (methodMsg)
            {
                case "GET":
                    return Methods.GET;
                case "POST":
                    return Methods.POST;
                case "PUT":
                    return Methods.PUT;
                case "DELETE":
                    return Methods.DELETE;
                case "HEAD":
                    return Methods.HEAD;
                case "OPTIONS":
                    return Methods.OPTIONS;
                case "TRACE":
                    return Methods.TRACE;
                case "CONNECT":
                    return Methods.CONNECT;
                default:
                    return Methods.NOTIMP;
            }
        }
        public override string ToString(){
            return String.Format("Method: {0}\n Path: {1}\n Params: {2}\n HTTPVersion: {3}",method,path,parms,httpVersion);
        }
    }

    public enum Methods
    {
        OPTIONS,
        GET,
        HEAD,
        POST,
        PUT,
        DELETE,
        TRACE,
        CONNECT,
        NOTIMP
    }
}