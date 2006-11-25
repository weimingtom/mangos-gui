using System;
using System.Collections;
using System.Text;
using System.Xml;
using System.IO;
using System.Windows.Forms;

public class ConfigLoadFunc
    {
        //Function to load values from config.xml
        public static string ConfigLoad(string sSearch, string filename)
        {
            XmlDocument xmlDoc = new XmlDocument();
            try
            {
                //Try to open config.xml
                xmlDoc.Load(filename);
                //If successful and sSearch = "loadtest" then simply return
                if (sSearch == "loadtest")
                {
                    return "loadok";
                }
            }
            //Catch errors in XML format and delete config.xml if it is corrupted
            catch (System.Xml.XmlException)
            {
                System.Windows.Forms.DialogResult error = MessageBox.Show("The Config File was found to be corrupt and will be deleted.", "Error with Config File", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (error == DialogResult.OK)
                {
                    File.Delete("config.xml");
                    return "default";
                }
            }

            //Catch FileNotFound
            catch (System.IO.FileNotFoundException)
            {
                return "default";
            }
            
            //Create new XmlNodeList
            XmlNodeList list = null;
            //Populate list with XML elements matching sSearch
            list = xmlDoc.GetElementsByTagName(sSearch);
            string s = null;
            foreach (XmlNode n in list)
            {
                //Iterate across list and return the InnerText (i.e. plain text value of element)
                s = n.InnerText;
                //Return the InnerText;
                return s;
            }

            return "Error";
        }

        //Function to load values from banned.xml
        public static string[] TimeBanLoad(string filename)
        {

            if (File.Exists("banned.xml"))
            {
                XmlDocument xmlDocBan = new XmlDocument();
                try
                {
                    //Try to open banned.xml
                    xmlDocBan.Load(filename);

                }
                //Catch errors in XML format and delete config.xml if it is corrupted
                catch (System.Xml.XmlException)
                {
                    System.Windows.Forms.DialogResult error = MessageBox.Show("The Banned File was found to be corrupt and will be deleted.", "Error with Config File", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (error == DialogResult.OK)
                    {
                        File.Delete("banned.xml");
                        return null;
                    }
                }

                //Catch FileNotFound
                catch (System.IO.FileNotFoundException)
                {
                    return null;
                }

                //Create new XmlNodeList
                XmlNodeList banlist = null;
                //Populate list with XML elements matching sSearch
                banlist = xmlDocBan.GetElementsByTagName("Banned");
                ArrayList s = new ArrayList();
                foreach (XmlNode n in banlist)
                {
                    //Iterate across list and return the InnerText (i.e. plain text value of element)
                    s.Add(n.InnerText);
                    //Return the InnerText;

                }
                return (string[])s.ToArray(typeof(string));
            }
            else
            {
                ArrayList s = new ArrayList(1);
                s.Add("none");
                return (string[])s.ToArray(typeof(string));
            }
        }
    }


