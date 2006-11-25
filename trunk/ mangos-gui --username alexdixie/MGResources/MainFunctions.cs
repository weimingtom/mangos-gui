using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.Diagnostics;
using System.Management;
using System.Threading;

namespace MGUI
{
    public class MainFunctions
    {

        public static void SendToConsole(string toWrite)
        {
            char[] toWriteArray = toWrite.ToCharArray();
            foreach (char c in toWriteArray)
            {
                SendKeys.SendWait(Convert.ToString(c));
            }
        }

        public static void WriteBans(string[] toWrite)
        {
            if (File.Exists("banned.xml") == true)
            {
                File.Delete("banned.xml");
            }
            XmlTextWriter banWriter = new XmlTextWriter("banned.xml", null); //Create new XML file ready for output
            banWriter.WriteStartDocument();
            banWriter.WriteComment("Time Bans for MGUI");
            banWriter.WriteStartElement("Banned");
            foreach (string s in toWrite)
            {
                banWriter.WriteString(s);
            }
            banWriter.WriteEndElement();
            banWriter.WriteEndDocument();
            banWriter.Close();
        }

        public static bool ClearTimeBans()
        {
            DialogResult error = MessageBox.Show("Are you sure that you wish to delete ALL time bans?", "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (error == DialogResult.OK)
            {
                File.Delete("banned.xml");
                return true;
            }
            else
            {
                return false;
            }
            
        }

        
    }
}
