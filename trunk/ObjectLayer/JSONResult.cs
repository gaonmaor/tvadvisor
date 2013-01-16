using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.Serialization.Json;

namespace ObjectLayer
{
    //[Serializable]
    public class Result
    {
        public string mid { get; set; }
        public string name { get; set; }
        public string type { get; set; }
    }

    //[Serializable]
    public class JSONResult
    {
        public Result result { get; set; }

        public JSONResult()
        {

        }

        public JSONResult(String json)
        {
            JSONResult deserializedUser = new JSONResult();
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(json));
            DataContractJsonSerializer ser = new DataContractJsonSerializer(deserializedUser.GetType());
            deserializedUser = ser.ReadObject(ms) as JSONResult;
            ms.Close();
            result = deserializedUser.result;
            //return deserializedUser;
        }
    }

    public class JSONResult2
    {
        public string result { get; set; }

        public JSONResult2()
        {

        }

        public JSONResult2(String json)
        {
            JSONResult2 deserializedUser = new JSONResult2();
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(json));
            DataContractJsonSerializer ser = new DataContractJsonSerializer(deserializedUser.GetType());
            deserializedUser = ser.ReadObject(ms) as JSONResult2;
            ms.Close();
            result = deserializedUser.result;
            //return deserializedUser;
        }
    }
}
