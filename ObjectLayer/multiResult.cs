using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Json;

namespace mR
{
    public class Result
    {
        public string result { get; set; }
    }

    public class Madeup
    {
        public Result result { get; set; }
    }

    public class multiResult
    {
        public List<Madeup> madeup { get; set; }

        public multiResult()
        {

        }

        public multiResult(String json)
        {
            multiResult deserializedUser = new multiResult();
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(json));
            DataContractJsonSerializer ser = new DataContractJsonSerializer(deserializedUser.GetType());
            deserializedUser = ser.ReadObject(ms) as multiResult;
            ms.Close();
            madeup = deserializedUser.madeup;
            //return deserializedUser;
        }

    }
}
