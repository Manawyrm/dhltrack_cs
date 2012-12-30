using System;
using System.Collections.Generic;
using System.Text;

namespace dhltrack
{
    class Program
    {
        static void Main(string[] args)
        {
            //Inkorrekte Argumente
            if (args.Length != 1)
            {
                //Falsche Anzahl der Argumente
                Console.WriteLine("Benutzung: dhltrack [Delivery-ID]");
            }
            else
            {
                //The joy of working with modern languages...
                string id = args[0]; //Building up the URL...
                string url = "http://nolp.dhl.de/nextt-online-public/set_identcodes.do?lang=de&idc=" + id;

                System.Net.WebClient wc = new System.Net.WebClient();
                wc.Encoding = UTF8Encoding.UTF8;
                string htmldata = wc.DownloadString(url);

                if (htmldata.Contains("<div class=\"error\">")) //DHL gibt bei nicht vorhandener ID den Error in dieser CSS-Klasse heraus.
                {
                    //Leider nicht vorhanden.
                    Console.WriteLine("Es ist keine Sendung mit der ID " + id + " bekannt!");
                }
                else
                {
                    //Status der Sendung extrahieren -- evtl. wäre hier ein RegExp besser... 
                    string status = htmldata.Split(new[] { "<td class=\"status\">" }, StringSplitOptions.None)[1].Split(new[] { "</td>" }, StringSplitOptions.None)[0].Replace("<div class=\"statusZugestellt\">", "").Replace("</div>", "").Trim();
                    Console.WriteLine("Status der Sendung mit ID: " + id);
                    Console.WriteLine(status);
                }
            }

        }
    }
}
