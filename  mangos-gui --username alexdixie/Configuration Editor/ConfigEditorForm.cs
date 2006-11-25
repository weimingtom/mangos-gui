using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace ConfigurationEditor
{
    public partial class Form1 : Form
    {
        //Load initial variables
        static string dTimer, dmName, drName, dmHosted, drHosted, dappPath, drappPath,
            dHost, duName, dpWord, dmData, drData;

        public Form1()
        {
            InitializeComponent();
            //Default values for all variables
            dTimer = "10000";
            dmName = "mangosd";
            drName = "realmd";
            drHosted = "Yes";
            dmHosted = "Yes";
            dappPath = "@CURRENT@";
            drappPath = "@CURRENT@";
            dHost = "localhost";
            duName = "root";
            dpWord = "password";
            dmData = "mangos";
            drData = "realmd";

            //Use "loadtest" parameter on ConfigLoad to check for config file existence. If returns default then use default values above
            if (ConfigLoadFunc.ConfigLoad("loadtest", "config.xml") == "default")
            {
                timerBox.Text = dTimer;
                mnameBox.Text = dmName;
                rnameBox.Text = drName;
                rhostedBox.Text = drHosted;
                mhostedBox.Text = dmHosted;
                appBox.Text = dappPath;
                rpathBox.Text = drappPath;
                hostBox.Text = dHost;
                userBox.Text = duName;
                passBox.Text = dpWord;
                mangosData.Text = dmData;
                realmData.Text = drData;
            }
            //Otherwise use values from config.xml
            else
            {
                timerBox.Text = ConfigLoadFunc.ConfigLoad("TimerInterval", "config.xml");
                mnameBox.Text = ConfigLoadFunc.ConfigLoad("MangosName", "config.xml");
                rnameBox.Text = ConfigLoadFunc.ConfigLoad("RealmName", "config.xml");
                if (ConfigLoadFunc.ConfigLoad("RealmHosted", "config.xml") == "1")
                {
                    rhostedBox.Text = "Yes";
                }
                else
                {
                    rhostedBox.Text = "No";
                }
                if (ConfigLoadFunc.ConfigLoad("MangosHosted", "config.xml") == "1")
                {
                    mhostedBox.Text = "Yes";
                }
                else
                {
                    mhostedBox.Text = "No";
                }
                appBox.Text = ConfigLoadFunc.ConfigLoad("AppPath", "config.xml");
                rpathBox.Text = ConfigLoadFunc.ConfigLoad("RealmPath", "config.xml");
                hostBox.Text = ConfigLoadFunc.ConfigLoad("DatabaseHost", "config.xml");
                userBox.Text = ConfigLoadFunc.ConfigLoad("DatabaseUsername", "config.xml");
                passBox.Text = ConfigLoadFunc.ConfigLoad("DatabasePassword", "config.xml");
                mangosData.Text = ConfigLoadFunc.ConfigLoad("MangosDatabase", "config.xml");
                realmData.Text = ConfigLoadFunc.ConfigLoad("RealmDatabase", "config.xml");
            }
        }

        //Exit button without saving
        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult error = MessageBox.Show("Do you wish to exit without saving?", "Confirm Exit", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (error == DialogResult.OK)
            {
                Application.Exit();
            }
            else
            {
            }
        }

        //Simply reset all boxes to their default values
        private void button3_Click(object sender, EventArgs e)
        {
            timerBox.Text = dTimer;
            mnameBox.Text = dmName;
            rnameBox.Text = drName;
            rhostedBox.Text = drHosted;
            mhostedBox.Text = dmHosted;
            appBox.Text = dappPath;
            rpathBox.Text = drappPath;
            hostBox.Text = dHost;
            userBox.Text = duName;
            passBox.Text = dpWord;
            mangosData.Text = dmData;
            realmData.Text = drData;
        }

        //Same string to byte[] conversion for hash checking
        public static byte[] StrToByteArray(string str)
        {
            System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
            return encoding.GetBytes(str);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //If config file exists then delete it
            if (File.Exists ("config.xml")==true)
            {
                File.Delete("config.xml"); 
            }
            XmlTextWriter textWriter = new XmlTextWriter("config.xml", null); //Create new XML file ready for output
            //Write all XML
            textWriter.WriteStartDocument();
            textWriter.WriteComment("MaNGOS GUI Config File");
            textWriter.WriteComment("Generated by Configuration Editor");
            textWriter.WriteStartElement("Config");
            textWriter.WriteStartElement("TimerInterval");
            textWriter.WriteString(timerBox.Text);
            textWriter.WriteEndElement();
            textWriter.WriteStartElement("MangosHosted");
            if (mhostedBox.Text == "Yes")
            {
                textWriter.WriteString("1");
            }
            else
            {
                textWriter.WriteString("0");
            }
            textWriter.WriteEndElement();
            textWriter.WriteStartElement("RealmHosted");
            if (rhostedBox.Text == "Yes")
            {
                textWriter.WriteString("1");
            }
            else
            {
                textWriter.WriteString("0");
            }
            textWriter.WriteEndElement();
            textWriter.WriteStartElement("MangosName");
            textWriter.WriteString(mnameBox.Text);
            textWriter.WriteEndElement();
            textWriter.WriteStartElement("RealmName");
            textWriter.WriteString(rnameBox.Text);
            textWriter.WriteEndElement();
            textWriter.WriteStartElement("AppPath");
            textWriter.WriteString(appBox.Text);
            textWriter.WriteEndElement();
            textWriter.WriteStartElement("RealmPath");
            textWriter.WriteString(rpathBox.Text);
            textWriter.WriteEndElement();
            textWriter.WriteStartElement("DatabaseHost");
            textWriter.WriteString(hostBox.Text);
            textWriter.WriteEndElement();
            textWriter.WriteStartElement("DatabaseUsername");
            textWriter.WriteString(userBox.Text);
            textWriter.WriteEndElement();
            textWriter.WriteStartElement("DatabasePassword");
            textWriter.WriteString(passBox.Text);
            textWriter.WriteEndElement();
            textWriter.WriteStartElement("MangosDatabase");
            textWriter.WriteString(mangosData.Text);
            textWriter.WriteEndElement();
            textWriter.WriteStartElement("RealmDatabase");
            textWriter.WriteString(realmData.Text);
            textWriter.WriteEndElement();
            textWriter.WriteStartElement("Signed");
            textWriter.WriteString("");
            textWriter.WriteEndElement();
            textWriter.WriteEndElement();
            textWriter.WriteEndDocument();
            textWriter.Close();
            //Calculate hash based on written config file then rewrite file including hash.
            //Function will be rewritten as inefficient at the moment
            //Problem with generating hash on the fly is access permissions with ConfigLoad using XML to get values for hash
            string hashedText = "";
            string[] aHash = { "TimerInterval", "MangosName", "RealmName", "MangosHosted", "RealmHosted", "RealmPath", "AppPath", "DatabaseHost", "DatabaseUsername", "DatabasePassword", "MangosDatabase", "RealmDatabase" };
            foreach (string hash in aHash)
            {
                hashedText += SimpleHash.ComputeHash(ConfigLoadFunc.ConfigLoad(hash, "config.xml"), "SHA1", StrToByteArray("mangoshash"));
            }
            if (File.Exists("config.xml") == true)
            {
                File.Delete("config.xml");
            }
            XmlTextWriter finalWriter = new XmlTextWriter("config.xml", null);
            finalWriter.WriteStartDocument();
            finalWriter.WriteComment("MaNGOS GUI Config File");
            finalWriter.WriteComment("Generated by Configuration Editor");
            finalWriter.WriteStartElement("Config");
            finalWriter.WriteStartElement("TimerInterval");
            finalWriter.WriteString(timerBox.Text);
            finalWriter.WriteEndElement();
            finalWriter.WriteStartElement("MangosHosted");
            if (mhostedBox.Text == "Yes")
            {
                finalWriter.WriteString("1");
            }
            else
            {
                finalWriter.WriteString("0");
            }
            finalWriter.WriteEndElement();
            finalWriter.WriteStartElement("RealmHosted");
            if (rhostedBox.Text == "Yes")
            {
                finalWriter.WriteString("1");
            }
            else
            {
                finalWriter.WriteString("0");
            }
            finalWriter.WriteEndElement();
            finalWriter.WriteStartElement("MangosName");
            finalWriter.WriteString(mnameBox.Text);
            finalWriter.WriteEndElement();
            finalWriter.WriteStartElement("RealmName");
            finalWriter.WriteString(rnameBox.Text);
            finalWriter.WriteEndElement();
            finalWriter.WriteStartElement("AppPath");
            finalWriter.WriteString(appBox.Text);
            finalWriter.WriteEndElement();
            finalWriter.WriteStartElement("RealmPath");
            finalWriter.WriteString(rpathBox.Text);
            finalWriter.WriteEndElement();
            finalWriter.WriteStartElement("DatabaseHost");
            finalWriter.WriteString(hostBox.Text);
            finalWriter.WriteEndElement();
            finalWriter.WriteStartElement("DatabaseUsername");
            finalWriter.WriteString(userBox.Text);
            finalWriter.WriteEndElement();
            finalWriter.WriteStartElement("DatabasePassword");
            finalWriter.WriteString(passBox.Text);
            finalWriter.WriteEndElement();
            finalWriter.WriteStartElement("MangosDatabase");
            finalWriter.WriteString(mangosData.Text);
            finalWriter.WriteEndElement();
            finalWriter.WriteStartElement("RealmDatabase");
            finalWriter.WriteString(realmData.Text);
            finalWriter.WriteEndElement();
            finalWriter.WriteStartElement("Signed");
            finalWriter.WriteString(hashedText);
            finalWriter.WriteEndElement();
            finalWriter.WriteEndElement();
            finalWriter.WriteEndDocument();
            finalWriter.Close();
            //If GUI is open, restart it to reload new config file settings
            if (Process.GetProcessesByName("MaNGOS-GUI").Length > 0)
            {
                Process[] restarterProcess = Process.GetProcessesByName("MaNGOS-GUI");
                restarterProcess[0].Kill();
                Process.Start("\".\\MaNGOS-GUI.exe\"");
            }
            //Close this program
            this.Close();
        }
    }
}