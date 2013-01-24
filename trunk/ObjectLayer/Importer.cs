using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace ObjectLayer
{
    public class Importer
    {
        //executes an http query, returns the result as a string
        public static String executeAPIQuery(String query)
        {
            HttpWebRequest hreq = (HttpWebRequest)WebRequest.Create(query);
            HttpWebResponse hres = (HttpWebResponse)hreq.GetResponse();
            StreamReader sr = new StreamReader(hres.GetResponseStream());
            String result = sr.ReadToEnd();
            return result;
        }

        //returns the mid from the result json
        private static String process(String json)
        {
            JSONResult ro = new JSONResult(json);// returned JSON
            String result = (ro.result != null ? ro.result.mid : null);
            //String result = ro.result.mid;
            return result;
        }

        //formats json object to be c# class compatible
        private static String process3(String json)
        {
            String result = json.Replace("/m/", "*****");
            result = result.Replace("/", "_");
            result = result.Replace("*****", "/m/");
            return result;
        }

        public static String[] getDescByMID(String[] mids)
        {
            String[] result = new String[mids.Length];
            int count = 0;
            String url = Properties.Resources.APIURLmult + "?key=" + Properties.Resources.APIkey;
            String qw = "[";
            foreach (String mid in mids)
            {
                qw = qw + "{\"params\": {\"id\":[\"" + mid + "\"] }, \"method\": \"freebase.text.get\", \"apiVersion\": \"v1\"}";
                qw = qw + ",";
            }
            qw = qw.Substring(0, qw.Length - 1);
            qw = qw + "]";
            String fr = "";
            try
            {
                HttpWebRequest hreq = (HttpWebRequest)WebRequest.Create(url);
                hreq.Timeout = -1;
                UTF8Encoding encoding = new UTF8Encoding();
                byte[] bytes = encoding.GetBytes(qw);
                hreq.ContentLength = bytes.Length;
                hreq.Method = "POST";
                hreq.ContentType = "application/json";
                Stream writeStream = hreq.GetRequestStream();
                writeStream.Write(bytes, 0, bytes.Length);
                writeStream.Close();
                hreq.Expect = "";

                HttpWebResponse hres = (HttpWebResponse)hreq.GetResponse();
                StreamReader sr = new StreamReader(hres.GetResponseStream());
                fr = sr.ReadToEnd();

                fr = "{\"madeup\":" + fr + "}";
                mR.multiResult fro = new mR.multiResult(fr);

                foreach (mR.Madeup m in fro.madeup)
                {
                    if (m.result != null)
                    {
                        fr = m.result.result;
                        result[count] = fr;
                        count++;
                    }
                }
            }
            catch (System.Net.WebException)
            {
                fr = "Bad Request";
            }

            return result;
        }

        public static TVProgram.TVProgramJSON getProgramByName(String name)
        {
            String mid = getProgramMIDByName(name);
            TVProgram.TVProgramJSON result = null;
            if (mid != null)
            {
                result = getProgramByMID(mid);
            }
            return result;
        }

        public static String getProgramMIDByName(String name)
        {
            String pQuery = "\"type\":\"/tv/tv_program\",\"name\":\"" + name + "\",\"mid\":null";
            String version = Properties.Resources.queryVersion2;//MQL format query
            String fq = Properties.Resources.APIURL + version + "?query={" + pQuery + "}&key=" + Properties.Resources.APIkey;
            String fr = null;
            try
            {
                fr = executeAPIQuery(fq);
                fr = process(fr);
            }
            catch (System.Net.WebException)
            {
                fr = null;// "Bad Request";
            }
            return fr;
        }

        public static TVProgram.TVProgramJSON getProgramByMID(String mid)
        {
            String version = Properties.Resources.queryVersion5;//topic search query
            String fq = Properties.Resources.APIURL + version + mid + "?key=" + Properties.Resources.APIkey;
            TVProgram.TVProgramJSON fro = null;
            try
            {
                String fr = executeAPIQuery(fq);
                fr = process3(fr);
                fro = new TVProgram.TVProgramJSON(fr);
            }
            catch (System.Net.WebException)
            {
                fro = null;
            }
            return fro;
        }

        public static TVActor.TVActorJSON getActorByName(String name)
        {
            String mid = getActorMIDByName(name);
            TVActor.TVActorJSON result = null;
            if (mid != null)
            {
                result = getActorByMID(mid);
            }
            return result;
        }

        public static String getActorMIDByName(String name)
        {
            String pQuery = "\"type\":\"/tv/tv_actor\",\"name\":\"" + name + "\",\"mid\":null";
            String version = Properties.Resources.queryVersion2;//MQL format query
            String fq = Properties.Resources.APIURL + version + "?query={" + pQuery + "}&key=" + Properties.Resources.APIkey;
            String fr = null;
            try
            {
                fr = executeAPIQuery(fq);
                fr = process(fr);
            }
            catch (System.Net.WebException)
            {
                fr = null;// "Bad Request";
            }
            return fr;
        }

        public static TVActor.TVActorJSON getActorByMID(String mid)
        {
            String version = Properties.Resources.queryVersion5;//topic search query
            String fq = Properties.Resources.APIURL + version + mid + "?key=" + Properties.Resources.APIkey;
            TVActor.TVActorJSON fro = null;
            try
            {
                String fr = executeAPIQuery(fq);
                fr = process3(fr);
                fro = new TVActor.TVActorJSON(fr);
            }
            catch (System.Net.WebException)
            {
                fro = null;
            }
            return fro;
        }

        public static TVProducer.TVProducerJSON getProducerByName(String name)
        {
            String mid = getProducerMIDByName(name);
            TVProducer.TVProducerJSON result = null;
            if (mid != null)
            {
                result = getProducerByMID(mid);
            }
            return result;
        }

        public static String getProducerMIDByName(String name)
        {
            String pQuery = "\"type\":\"/tv/tv_producer\",\"name\":\"" + name + "\",\"mid\":null";
            String version = Properties.Resources.queryVersion2;//MQL format query
            String fq = Properties.Resources.APIURL + version + "?query={" + pQuery + "}&key=" + Properties.Resources.APIkey;
            String fr = null;
            try
            {
                fr = executeAPIQuery(fq);
                fr = process(fr);
            }
            catch (System.Net.WebException)
            {
                fr = null;// "Bad Request";
            }
            return fr;
        }

        public static TVProducer.TVProducerJSON getProducerByMID(String mid)
        {
            String version = Properties.Resources.queryVersion5;//topic search query
            String fq = Properties.Resources.APIURL + version + mid + "?key=" + Properties.Resources.APIkey;
            TVProducer.TVProducerJSON fro = null;
            try
            {
                String fr = executeAPIQuery(fq);
                fr = process3(fr);
                fro = new TVProducer.TVProducerJSON(fr);
            }
            catch (System.Net.WebException)
            {
                fro = null;
            }
            return fro;
        }

        public static TVWriter.TVWriterJSON getWriterByName(String name)
        {
            String mid = getWriterMIDByName(name);
            TVWriter.TVWriterJSON result = null;
            if (mid != null)
            {
                result = getWriterByMID(mid);
            }
            return result;
        }

        public static String getWriterMIDByName(String name)
        {
            String pQuery = "\"type\":\"/tv/tv_writer\",\"name\":\"" + name + "\",\"mid\":null";
            String version = Properties.Resources.queryVersion2;//MQL format query
            String fq = Properties.Resources.APIURL + version + "?query={" + pQuery + "}&key=" + Properties.Resources.APIkey;
            String fr = null;
            try
            {
                fr = executeAPIQuery(fq);
                fr = process(fr);
            }
            catch (System.Net.WebException)
            {
                fr = null;// "Bad Request";
            }
            return fr;
        }

        public static TVWriter.TVWriterJSON getWriterByMID(String mid)
        {
            String version = Properties.Resources.queryVersion5;//topic search query
            String fq = Properties.Resources.APIURL + version + mid + "?key=" + Properties.Resources.APIkey;
            TVWriter.TVWriterJSON fro = null;
            try
            {
                String fr = executeAPIQuery(fq);
                fr = process3(fr);
                fro = new TVWriter.TVWriterJSON(fr);
            }
            catch (System.Net.WebException)
            {
                fro = null;
            }
            return fro;
        }

        //genres
    }
}
