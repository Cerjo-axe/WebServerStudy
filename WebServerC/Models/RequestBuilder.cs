namespace WebServer
{
    public static class RequestBuilder{
        public static HTTPRequestData GetDataFromMsg(string msg){
            HTTPRequestData data = new HTTPRequestData();
            try
            {
                if(msg==null){
                    return data;
                }
                string[] dividedMsg = msg.Split("\r\n");
                if(dividedMsg.Length==0){
                    return data;
                }
                string[] test = dividedMsg[0].Split(" ");
                data.method = test[0];
                data.path = test[1];
                data.httpVersion = test[2];
                Console.WriteLine(data.method);
                return data;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return data;
        }
    }
}