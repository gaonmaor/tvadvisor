using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using CommonLayer;

namespace Testings
{
    class Program
    {
        static void Main(string[] args)
        {
            tv epg;
            epg = Utils.DeserializeXml<tv>("epg.xml");
            foreach (var channel in epg.channel)
            {
                Console.WriteLine(channel.id + ": " + channel.displayname);
            }
            
            Console.ReadLine();
        }
    }
}
