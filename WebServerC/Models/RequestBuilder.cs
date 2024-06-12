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
                data.SetMethodandPath(dividedMsg[0]);
                Console.WriteLine(data.ToString());
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