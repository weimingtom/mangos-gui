using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
using ConsoleHook;
using ConsoleHookWIN32;
using MGUI;
using System.Text.RegularExpressions;
using System.Xml;


namespace MaNGOS_GUI
{
    public partial class GUIForm : Form
    {
        static int oCount;
        static int cCount = 0;
        static int maxoCount = 0;
        static bool rStarted = false;
        static int iTime = 0;
        static int mangosHosted, realmHosted;
        static string mangosName, realmName, AppPath, rAppPath;
        static string hashedText = "";
        public static double sTimer = 0.0;
        public static double timeLeft = 6.0;
        public static IntPtr pPointer = IntPtr.Zero;
        public static IntPtr m_consoleOutputHandle = IntPtr.Zero;
        public static string[] pOnline;
        public static string[] cOnline;
        public static string databaseHost, databaseUser, databasePass, mangosDatabase, realmDatabase;


        public GUIForm()
        {
            InitializeComponent();
            label19.Text = Convert.ToString(cCount);
            label16.Text = "0";
            label17.Text = "0";
            generalTimer.Enabled = true;
            ConsoleText.Text = "";
            label7.Text = "Restarter Stopped.";
            WriteLog("GUI Loaded", Color.Green);
                //Check config hash
                SignedCheck();
                // Load all variables from config -> better at start or as needed from xml?
                if (ConfigLoadFunc.ConfigLoad("loadtest", "config.xml") == "loadok")
                {
                    restarterTimer.Interval = Convert.ToInt32(ConfigLoadFunc.ConfigLoad("TimerInterval", "config.xml"));
                    mangosHosted = Convert.ToInt16(ConfigLoadFunc.ConfigLoad("MangosHosted", "config.xml"));
                    realmHosted = Convert.ToInt16(ConfigLoadFunc.ConfigLoad("RealmHosted", "config.xml"));
                    mangosName = ConfigLoadFunc.ConfigLoad("MangosName", "config.xml");
                    realmName = ConfigLoadFunc.ConfigLoad("RealmName", "config.xml");
                    databaseHost = ConfigLoadFunc.ConfigLoad("DatabaseHost", "config.xml");
                    databaseUser = ConfigLoadFunc.ConfigLoad("DatabaseUsername", "config.xml");
                    databasePass = ConfigLoadFunc.ConfigLoad("DatabasePassword", "config.xml");
                    mangosDatabase = ConfigLoadFunc.ConfigLoad("MangosDatabase", "config.xml");
                    realmDatabase = ConfigLoadFunc.ConfigLoad("RealmDatabase", "config.xml");


                    //Replace @CURRENT@ tags - included simply to help users
                    if (ConfigLoadFunc.ConfigLoad("AppPath", "config.xml") == "@CURRENT@")
                    {
                        AppPath = ".";
                    }
                    else
                    {
                        AppPath = ConfigLoadFunc.ConfigLoad("AppPath", "config.xml");
                    }
                    if (ConfigLoadFunc.ConfigLoad("RealmPath", "config.xml") == "@CURRENT@")
                    {
                        rAppPath = ".";
                    }
                    else
                    {
                        rAppPath = ConfigLoadFunc.ConfigLoad("RealmPath", "config.xml");
                    }
                }
                else
                {
                    Application.Exit();
                }
                WriteLog("Config Loaded Successfully", Color.Green);
        }

        public void WriteLog(string toWrite, Color Colour)
        {
            logBox.SelectionColor = Colour;
            logBox.AppendText("[" + DateTime.Now + "] " + toWrite + "\r\n");
        }

        private void button2_Click(object sender, EventArgs e)
        {

		}

        public void ReadConsole()
        {
            ConsoleText.Text = "";
            Process[] proc = Process.GetProcessesByName(mangosName);
            if (proc.Length == 0)
            {
                ConsoleText.Text = "Error process not found!";
            }
            else
            {
                pPointer = (IntPtr)proc[0].Id;
                HookToConsole.AttachTo(pPointer);

                Point startPoint = new Point(0, 0);
                Size recSize = new Size(75, 5);
                CharInfo[] chars = HookToConsole.ReadOutput(startPoint, recSize);

                int index = 0;
                foreach (CharInfo ci in chars)
                {
                    if (index != 0 && (index % recSize.Width) == 0)
                        ConsoleText.Text += "\r\n";
                    ConsoleText.Text += ci.Char;
                    index++;
                }

                HookToConsole.RemoveFrom();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            WriteLog("Shutdown process started. MaNGOS will be restarted in " + timeLeft + "minutes", Color.Blue);
            timeLeft = Convert.ToInt16(textBox2.Text);
            timer1.Interval = 60000;
            timer1.Enabled = true;
            timer1_Tick(null, null);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            WriteLog("Count Players Online", Color.Gray);
            string connectionString;
            connectionString = "Data Source=" + databaseHost + ";User Id=" + databaseUser + ";Password=" + databasePass +";Database="+realmDatabase;
            MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection(connectionString);
            conn.Open();
            string query = "SELECT COUNT(*)FROM account WHERE online=\"1\"";
            MySql.Data.MySqlClient.MySqlCommand resQuery = new MySql.Data.MySqlClient.MySqlCommand(query, conn);
            oCount = Convert.ToInt16(resQuery.ExecuteScalar());
            label1.Text = "Players Online: " + oCount;
            conn.Close();
        }

  

        private void button4_Click(object sender, EventArgs e)
        {
            WriteLog("Get players online list", Color.Gray);
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            string connectionString;
            connectionString = "Data Source=" + databaseHost + ";User Id=" + databaseUser + ";Password=" + databasePass +";Database="+realmDatabase;
            MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection(connectionString);
            conn.Open();
            string query = "SELECT username FROM account WHERE online=\"1\"";
            MySql.Data.MySqlClient.MySqlCommand resQuery = new MySql.Data.MySqlClient.MySqlCommand(query, conn);
            MySql.Data.MySqlClient.MySqlDataReader myReader = resQuery.ExecuteReader();
            while (myReader.Read())
            {
                listBox1.Items.Add(myReader.GetString(0));
            }
            myReader.Close();
            conn.Close();
            pOnline = new string[listBox1.Items.Count];
            listBox1.Items.CopyTo(pOnline,0);
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            WriteLog("Account: " + listBox1.SelectedItem + " Banned!", Color.Red);
            string connectionString;
            connectionString = "Data Source=" + databaseHost + ";User Id=" + databaseUser + ";Password=" + databasePass +";Database="+realmDatabase;
            MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection(connectionString);
            conn.Open();
            string query = "UPDATE account SET banned = \"1\" WHERE username = \""+Convert.ToString(listBox1.SelectedItem)+"\"";
            MySql.Data.MySqlClient.MySqlCommand resQuery = new MySql.Data.MySqlClient.MySqlCommand(query, conn);
            resQuery.ExecuteNonQuery();
            conn.Close();
            button12_Click(null, null);

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            IntPtr pWindow;
            Process[] proc = Process.GetProcessesByName(mangosName);
            if (proc.Length == 0)
            {

            }
            pPointer = (IntPtr)proc[0].Id;
            pWindow = (IntPtr)proc[0].MainWindowHandle;
            Win32.SetForegroundWindow(pWindow);
            if (sTimer < Convert.ToDouble(textBox2.Text))
            {
                if (sTimer < (Convert.ToDouble(textBox2.Text) - 1 ))
                {
                    timeLeft = timeLeft - 1;
                    MainFunctions.SendToConsole("broadcast Server shutting down in: " + timeLeft + " minutes");
                    SendKeys.SendWait("{ENTER}");
                    sTimer++;
                }
                else
                {
                    if (timeLeft == 1) { timeLeft += 0.25; }
                    timer1.Interval = 15000;
                    timeLeft = timeLeft - 0.25;
                    MainFunctions.SendToConsole("broadcast Server shutting down in: " + (timeLeft * 60) + " seconds");
                    SendKeys.SendWait("{ENTER}");
                    sTimer += 0.25;
                }
            }
            else
            {
                MainFunctions.SendToConsole("exit");
                SendKeys.SendWait("{ENTER}");
                WriteLog("Shutdown counter ended! Shutting down MaNGOS NOW!", Color.Red);
                sTimer = 0;
                timeLeft = 5;
                timer1.Enabled = false;
            }
            Win32.SetForegroundWindow(this.Handle);
            HookToConsole.RemoveFrom();
            ReadConsole();
        }


        private void button7_Click(object sender, EventArgs e)
        {
            WriteLog("Scripts loaded", Color.Gray);
            IntPtr pWindow;
            Process[] proc = Process.GetProcessesByName(mangosName);
            if (proc.Length == 0)
            {

            }
            pPointer = (IntPtr)proc[0].Id;
            pWindow = (IntPtr)proc[0].MainWindowHandle;
            Win32.SetForegroundWindow(pWindow);
            MainFunctions.SendToConsole("loadscripts");
            SendKeys.SendWait("{ENTER}");
            Win32.SetForegroundWindow(this.Handle);
            HookToConsole.RemoveFrom();
            ReadConsole();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            WriteLog("Broadcast: \"" + textBox3.Text + "\" Sent", Color.Blue);
            IntPtr pWindow;
            Process[] proc = Process.GetProcessesByName(mangosName);
            if (proc.Length == 0)
            {

            }
            pPointer = (IntPtr)proc[0].Id;
            pWindow = (IntPtr)proc[0].MainWindowHandle;
            Win32.SetForegroundWindow(pWindow);
            MainFunctions.SendToConsole("broadcast " + textBox3.Text);
            SendKeys.SendWait("{ENTER}");
            Win32.SetForegroundWindow(this.Handle);
            HookToConsole.RemoveFrom();
            ReadConsole();
            
        }

        private void button9_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            string connectionString;
            connectionString = "Data Source=" + databaseHost + ";User Id=" + databaseUser + ";Password=" + databasePass +";Database="+realmDatabase;
            MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection(connectionString);
            conn.Open();
            string query = "SELECT username FROM account WHERE banned=\"1\"";
            MySql.Data.MySqlClient.MySqlCommand resQuery = new MySql.Data.MySqlClient.MySqlCommand(query, conn);
            MySql.Data.MySqlClient.MySqlDataReader myReader = resQuery.ExecuteReader();
            while (myReader.Read())
            {
                listBox1.Items.Add(myReader.GetString(0));
            }
            myReader.Close();
            conn.Close();
            pOnline = new string[listBox1.Items.Count];
            listBox1.Items.CopyTo(pOnline,0);
            if (listBox1.Items.Count == 0)
            {
                listBox1.Items.Add("No banned accounts");
            }
            WriteLog("Get banned list", Color.Gray);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            
            string connectionString;
            connectionString = "Data Source=" + databaseHost + ";User Id=" + databaseUser + ";Password=" + databasePass +";Database="+realmDatabase;
            MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection(connectionString);
            conn.Open();

            string query = "SELECT banned FROM account WHERE username=\"" + Convert.ToString(listBox1.SelectedItem) + "\"";
            MySql.Data.MySqlClient.MySqlCommand resQuery = new MySql.Data.MySqlClient.MySqlCommand(query, conn);
            MySql.Data.MySqlClient.MySqlDataReader myReader = resQuery.ExecuteReader();
            while (myReader.Read())
            {
                label2.Text = myReader.GetString(0);
            }
            myReader.Close();
            if (label2.Text == "0")
            {
                MessageBox.Show("Selected account is not banned!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                query = "UPDATE account SET banned = \"0\" WHERE username = \"" + Convert.ToString(listBox1.SelectedItem) + "\"";
                MySql.Data.MySqlClient.MySqlCommand bQuery = new MySql.Data.MySqlClient.MySqlCommand(query, conn);
                bQuery.ExecuteNonQuery();
            }
            conn.Close();
            WriteLog("Ban on account: "+listBox1.SelectedItem + " REMOVED!", Color.Red);
            button4_Click(null, null);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            label2.Text = "";
            string connectionString;
            connectionString = "Data Source=" + databaseHost + ";User Id=" + databaseUser + ";Password=" + databasePass +";Database="+realmDatabase;
            MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection(connectionString);
            conn.Open();

            string query = "SELECT username FROM account WHERE username=\"" + userBox.Text + "\"";
            MySql.Data.MySqlClient.MySqlCommand resQuery = new MySql.Data.MySqlClient.MySqlCommand(query, conn);
            MySql.Data.MySqlClient.MySqlDataReader myReader = resQuery.ExecuteReader();
            while (myReader.Read())
            {
                label2.Text = myReader.GetString(0);
            }
            myReader.Close();
            if (label2.Text != "")
            {
                MessageBox.Show("Account already exists!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                query = "INSERT INTO account(username,password,gmlevel,sessionkey,email,banned,last_ip,failed_logins,locked) VALUES (\"" + userBox.Text + "\",\"" + passBox.Text + "\",\"0\",\"\",\"\",\"0\",\"0\",\"0\",\"0\")";
                MySql.Data.MySqlClient.MySqlCommand bQuery = new MySql.Data.MySqlClient.MySqlCommand(query, conn);
                bQuery.ExecuteNonQuery();
                MessageBox.Show("Account: " + userBox.Text + " created!", "Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            conn.Close();
            WriteLog("Account: "+ userBox.Text + " Created!", Color.Blue);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            IntPtr pWindow;
            Process[] proc = Process.GetProcessesByName(mangosName);
            if (proc.Length == 0)
            {

            }
            pPointer = (IntPtr)proc[0].Id;
            pWindow = (IntPtr)proc[0].MainWindowHandle;
            Win32.SetForegroundWindow(pWindow);
            MainFunctions.SendToConsole("kick " + Convert.ToString(listBox1.SelectedItem));
            SendKeys.SendWait("{ENTER}");
            Win32.SetForegroundWindow(this.Handle);
            HookToConsole.RemoveFrom();
            ReadConsole();
            WriteLog("Account: " + listBox1.SelectedItem + " kicked!", Color.Red);
            button4_Click(null, null);
            
       }

        private void listBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
            Int32 accID = 0;
            string connectionString;
            connectionString = "Data Source="+databaseHost+";User Id="+databaseUser+";Password="+databasePass;
            MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection(connectionString);
            conn.Open();
            string query = "SELECT id FROM `"+realmDatabase+"`.`account` WHERE username=\"" + Convert.ToString(listBox1.SelectedItem) + "\"";
            MySql.Data.MySqlClient.MySqlCommand sQuery = new MySql.Data.MySqlClient.MySqlCommand(query, conn);
            MySql.Data.MySqlClient.MySqlDataReader myReader = sQuery.ExecuteReader();
            while (myReader.Read())
            {
                accID = Convert.ToInt32(myReader.GetString(0));
            }
            myReader.Close();
            conn.Close();
            conn.ConnectionString = connectionString;
            conn.Open();
            string nQuery = "SELECT name FROM `"+mangosDatabase+"`.`character` WHERE account=\"" + Convert.ToString(accID) + "\"";
            MySql.Data.MySqlClient.MySqlCommand resQuery = new MySql.Data.MySqlClient.MySqlCommand(nQuery, conn);
            MySql.Data.MySqlClient.MySqlDataReader myReader2 = resQuery.ExecuteReader();
            while (myReader2.Read())
            {
                listBox2.Items.Add(myReader2.GetString(0));
            }
            myReader2.Close();
            conn.Close();
            cOnline = new string[listBox2.Items.Count];
            listBox2.Items.CopyTo(cOnline, 0);
            WriteLog("Character list loaded for account: "+listBox1.SelectedItem, Color.Gray);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (listBox1.Items.Count > 0)
            {
                string[] filterArray = new string[listBox1.Items.Count];
                listBox1.Items.CopyTo(filterArray, 0);
                listBox1.Items.Clear();

                string sPattern = @"^" + textBox1.Text + @"\w*";

                if (sPattern == @"^\w*")
                {
                    foreach (string toload in pOnline)
                    {
                        listBox1.Items.Add(toload);
                    }
                }
                else
                {
                    foreach (string s in filterArray)
                    {
                        if (System.Text.RegularExpressions.Regex.IsMatch(s, sPattern, System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                        {
                            listBox1.Items.Add(s);
                        }
                    }
                    if (listBox1.Items.Count == 0)
                    {
                        listBox1.Items.Add("No matches found");
                    }
                }
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (listBox2.Items.Count > 0)
            {
                string[] filterArray2 = new string[listBox2.Items.Count];
                listBox2.Items.CopyTo(filterArray2, 0);
                listBox2.Items.Clear();

                string s2Pattern = @"^" + textBox4.Text + @"\w*";

                if (s2Pattern == @"^\w*")
                {
                    foreach (string toload2 in cOnline)
                    {
                        listBox2.Items.Add(toload2);
                    }
                }
                else
                {
                    foreach (string s2 in filterArray2)
                    {
                        if (System.Text.RegularExpressions.Regex.IsMatch(s2, s2Pattern, System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                        {
                            listBox2.Items.Add(s2);
                        }
                    }
                    if (listBox2.Items.Count == 0)
                    {
                        listBox2.Items.Add("No characters found");
                    }
                }
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            //TODO: Load MOTD from mangos.conf
        }

        //################################//
        //###    Restarter Functions   ###//
        //################################//

        //Initial variable load

        //Simple string to byte[] conversion for hash
        public static byte[] StrToByteArray(string str)
        {
            System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
            return encoding.GetBytes(str);
        }

        //Function to check the hash value of the config file
        static void SignedCheck()
        {
            string[] aHash = { "TimerInterval", "MangosName", "RealmName", "MangosHosted", "RealmHosted", "RealmPath", "AppPath", "DatabaseHost", "DatabaseUsername", "DatabasePassword", "MangosDatabase", "RealmDatabase" };
            foreach (string hash in aHash)
            {
                //SimpleHash - see SHA1.cs class file
                hashedText += SimpleHash.ComputeHash(ConfigLoadFunc.ConfigLoad(hash, "config.xml"), "SHA1", StrToByteArray("mangoshash"));
            }
            //If matches then return
            if (hashedText == ConfigLoadFunc.ConfigLoad("Signed", "config.xml"))
            {
                return;
            }
            //If hash doesn't match then launch configuration editor
            else
            {
                
                DialogResult hashError = MessageBox.Show("Error: Configuration file incorrectly signed! Please use the configuration editor instead of making manual changes!", "Error in Signed Configuration File", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
                if (hashError == DialogResult.OK)
                {
                    try
                    {
                        
                        Process.Start("\".\\Configuration Editor.exe\"");
                    }
                    catch (Win32Exception)
                    {
                        DialogResult error = MessageBox.Show("Error: Configuration Editor not found! Please check the location of \"Configuration Editor.exe\"", "Error Loading Configuration Editor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        if (error == DialogResult.OK)
                        {
                            Application.Exit();
                        }
                    }
                    
                    
                }
            }
            
        }

        //Function to handle uptime - formats Millisecs to a time format - showing and hiding values as necessary
        static string FormatMSec(Int64 MSec)
        {
            int H, M, S, D;
            string Ds, Hs, Ms, Ss, fString;

            D = (int)(MSec / 86400000);
            H = (int)(MSec / 3600000);
            M = (int)(MSec / 60000) - (H * 60);
            S = (int)(MSec / 1000) - (M * 60 + H * 3600 + D * 86400);

            if (D == 0)
            {
                Ds = "";
            }
            else
            {
                Ds = D + " Days, ";
            }

            if (H == 0)
            {
                if (Ds == D + " Days, ")
                {
                    Hs = "0 Hours, ";
                }
                else
                {
                    Hs = "";
                }
            }
            else
            {
                if (H < 10)
                {
                    Hs = "0" + H + " Hours, ";
                }
                else
                {
                    Hs = H + " Hours, ";
                }
            }

            if (M < 10)
            {
                Ms = "0" + M;
            }
            else
            {
                Ms = "" + M;
            }

            if (S < 10)
            {
                Ss = "0" + S;
            }
            else
            {
                Ss = "" + S;
            }

            fString = Ds + Hs + Ms + ":" + Ss;

            return fString;
        }

        private void restarterButton_Click(object sender, EventArgs e)
        {
            if (rStarted == true)
            {
                restarterTimer.Enabled = false;
                //restarterTimer2.Enabled = false;
                
                restarterButton.Text = "Start Restarter";
                label7.Text = "Restarter Stopped.";
                WriteLog("Restarter Stopped", Color.Red);
                rStarted = false;
            }
            else
            {
                restarterTimer.Enabled = true;
                restarterTimer2.Enabled = true;
                restarterButton.Text = "Stop Restarter";
                label7.Text = "Restarter Active";
                WriteLog("Restarter Started", Color.Red);
                rStarted = true; //Important - used to switch on/off restarter
                if (mangosHosted == 1) //Look to see if Mangos is hosted before checking for existence.
                {
                    if (Process.GetProcessesByName(mangosName).Length > 0) //Get processes which match mangos into array. If array length > 0 then Mangos is running and does not need restarting.
                    {

                    }
                    else //Otherwise array length = 0 and mangos needs restarting
                    {
                        try
                        {
                            Process.Start("\"" + AppPath + "\\" + mangosName + ".exe\"");
                            
                        }
                        catch (Win32Exception) //Need exception code for "file not found" currently catches all exceptions and treats as not finding the file.
                        {
                            restarterTimer.Enabled = false; //Stop spamming error boxes on each timer tick
                            restarterTimer2.Enabled = false;
                            DialogResult error = MessageBox.Show("Error: " + mangosName + " file not found!", "Error Loading " + mangosName + " File", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            if (error == DialogResult.OK)
                            {
                                Application.Exit();
                            }
                        }
                    }
                }
                if (realmHosted == 1) //Same as mangos check but for realmd
                {
                    if (Process.GetProcessesByName(realmName).Length > 0)
                    {

                    }
                    else
                    {
                        try
                        {
                            Process.Start("\"" + rAppPath + "\\" + realmName + ".exe\"");
                        }
                        catch (Win32Exception)
                        {
                            restarterTimer.Enabled = false;
                            restarterTimer2.Enabled = false;
                            DialogResult error = MessageBox.Show("Error: " + realmName + " file not found!", "Error Loading " + realmName + " File", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            if (error == DialogResult.OK)
                            {
                                Application.Exit();
                            }
                        }
                    }
                }
            }
        }

        private void restarterTimer_Tick(object sender, EventArgs e)
        {
            if (mangosHosted == 1)
            {
                if (Process.GetProcessesByName(mangosName).Length > 0)
                {

                }
                else
                {
                    try
                    {
                        WriteLog("MaNGOS Crashed!", Color.Red);
                        Process.Start("\"" + AppPath + "\\" + mangosName + ".exe\"");
                        iTime = 0;
                        cCount += 1;
                        label19.Text = Convert.ToString(cCount);
                        WriteLog("MaNGOS Restarted!", Color.Green);
                        if (checkBox1.Checked == true)
                        {
                            Process[] realmProcess = Process.GetProcessesByName(realmName);
                            if (realmProcess.Length > 0)
                            {
                                realmProcess[0].Kill();
                                Process.Start("\"" + rAppPath + "\\" + realmName + ".exe\"");
                                WriteLog("Checkbox checked, Realm restarted on MaNGOS crash!", Color.Red);
                            }
                            else
                            {
                                WriteLog("Checkbox checked, but Realm not found to be running!", Color.Red);
                            }
                        }
                    }
                    catch (Win32Exception)
                    {
                        restarterTimer.Enabled = false;
                        restarterTimer2.Enabled = false;
                        DialogResult error = MessageBox.Show("Error: " + mangosName + " file not found!", "Error Loading " + mangosName + " File", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        if (error == DialogResult.OK)
                        {
                            Application.Exit();
                        }
                    }
                }
            }
            if (realmHosted == 1)
            {
                if (Process.GetProcessesByName(realmName).Length > 0)
                {

                }
                else
                {
                    try
                    {
                        WriteLog("Realm Crashed!", Color.Red);
                        Process.Start("\"" + rAppPath + "\\" + realmName + ".exe\"");
                        iTime = 0;
                        cCount += 1;
                        label19.Text = Convert.ToString(cCount);
                        WriteLog("Realm Started!", Color.Green);
                    }
                    catch (Win32Exception)
                    {
                        restarterTimer.Enabled = false;
                        restarterTimer2.Enabled = false;
                        DialogResult error = MessageBox.Show("Error: " + realmName + " file not found!", "Error Loading " + realmName + " File", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        if (error == DialogResult.OK)
                        {
                            Application.Exit();
                        }
                    }
                }
            }
        }

        private void restartertimer2_Tick(object sender, EventArgs e)
        {
            //Simple timer for uptime (both with and without the restarter running)
            if (Process.GetProcessesByName(mangosName).Length > 0)
            {
                iTime = iTime + restarterTimer2.Interval;
                label8.Text = "Running for: " + FormatMSec(iTime);
                label20.Text = FormatMSec(iTime);
            }
            else
            {
                iTime = 0;
                label20.Text = "00:00:00";
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                //Load up configuration editor
                Process.Start("\".\\Configuration Editor.exe\"");
                WriteLog("Configuration Editor Launched", Color.Blue);
            }
            catch (Win32Exception)
            {
                DialogResult error = MessageBox.Show("Error: Configuration Editor not found! Please check the location of \"Configuration Editor.exe\"", "Error Loading Configuration Editor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (error == DialogResult.OK)
                {
                    WriteLog("Configuration Editor not found! Check location of \"Configuration Editor\"!", Color.Red);
                }
            }
        }

        private void generalTimer_Tick(object sender, EventArgs e)
        {
            button3_Click(null, null);
            label17.Text = Convert.ToString(oCount);
            if (oCount > maxoCount)
            {
                label16.Text = Convert.ToString(oCount);
            }
            //Check TimeBans
            string[] Banned = null;
            Banned = ConfigLoadFunc.TimeBanLoad("banned.xml");
            ArrayList toBanList = new ArrayList();
            foreach (string s in Banned)
            {
                int a = 0;
                string bannedname = "";
                string bantime = "";
                Int64 unix_time = (Int64)((DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds);
                ArrayList indvBans = new ArrayList();
                indvBans = ArrayList.Adapter(s.Split(Convert.ToChar(";")));
                foreach (string i in indvBans)
                {
                    a = i.IndexOf(@"%");
                    if (a != -1)
                    {
                        bannedname = i.Substring(0, a);
                        bantime = i.Substring(a + 1);
                        bantime.Remove(bantime.Length - 1);
                       
                        if (Convert.ToInt64(bantime) < unix_time)
                        {
                            string connectionString;
                            connectionString = "Data Source=" + databaseHost + ";User Id=" + databaseUser + ";Password=" + databasePass + ";Database=" + realmDatabase;
                            MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection(connectionString);
                            conn.Open();
                            string query = "UPDATE account SET banned = \"0\" WHERE username = \"" + bannedname + "\"";
                            MySql.Data.MySqlClient.MySqlCommand resQuery = new MySql.Data.MySqlClient.MySqlCommand(query, conn);
                            resQuery.ExecuteNonQuery();
                            conn.Close();
                            WriteLog("Time ban on "+ bannedname + " removed!", Color.Red);
                        }
                        else
                        {
                            toBanList.Add(i);
                        }
                    }
                }
                MainFunctions.WriteBans((string[])(toBanList.ToArray(typeof(string))));
            }
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }

        private void textBox4_Enter(object sender, EventArgs e)
        {
            textBox4.Text = "";
        }

        private void textBox4_Leave(object sender, EventArgs e)
        {
            if (textBox4.Text == "")
            {
                textBox4.Text = "<Search>";
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                textBox1.Text = "<Search>";
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            ArrayList alreadyBanned = new ArrayList();
            alreadyBanned.AddRange(ArrayList.Adapter(ConfigLoadFunc.TimeBanLoad("banned.xml")));
            Int64 unix_timebanned = (Int64)(DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds;
            string connectionString;
            connectionString = "Data Source=" + databaseHost + ";User Id=" + databaseUser + ";Password=" + databasePass + ";Database=" + realmDatabase;
            MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection(connectionString);
            conn.Open();
            string query = "UPDATE account SET banned = \"1\" WHERE username = \"" + Convert.ToString(listBox1.SelectedItem) + "\"";
            MySql.Data.MySqlClient.MySqlCommand resQuery = new MySql.Data.MySqlClient.MySqlCommand(query, conn);
            resQuery.ExecuteNonQuery();
            conn.Close();
            string tobeBanned = Convert.ToString(listBox1.SelectedItem);
            Int64 timeBan = unix_timebanned + (Convert.ToInt64(timebanBox.Text)*60);
            alreadyBanned.Add(tobeBanned + @"%" + timeBan + @";");
            if ((string)alreadyBanned[0] == "none")
            {
                alreadyBanned.RemoveAt(0);
            }
            MainFunctions.WriteBans((string[])alreadyBanned.ToArray(typeof(string)));
            WriteLog("Account: " + listBox1.SelectedItem + " BANNNED FOR " + timebanBox.Text + " minutes!", Color.Red);
            button12_Click(null, null);

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabPage2)
            {
                timer2.Enabled = true;
                cpuTimer.Enabled = false;
                timer2_Tick(null, null);
            }
            if (tabControl1.SelectedTab == Overview)
            {
                cpuTimer.Enabled = true;
                timer2.Enabled = false;
                cpuTimer_Tick(null, null);
            }
            if (tabControl1.SelectedTab == tabPage1)
            {
                cpuTimer.Enabled = false;
                timer2.Enabled = false;
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            ReadConsole();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            IntPtr pWindow;
            Process[] proc = Process.GetProcessesByName(mangosName);
            if (proc.Length == 0)
            {

            }
            pPointer = (IntPtr)proc[0].Id;
            pWindow = (IntPtr)proc[0].MainWindowHandle;
            Win32.SetForegroundWindow(pWindow);
            MainFunctions.SendToConsole(textBox6.Text);
            SendKeys.SendWait("{ENTER}");
            Win32.SetForegroundWindow(this.Handle);
            HookToConsole.RemoveFrom();
            ReadConsole();
            WriteLog("Custom command: \""+textBox6.Text + "\" sent to MaNGOS console!", Color.Blue);
        }

        private void timebanBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void button17_Click(object sender, EventArgs e)
        {
            bool clBans;
            clBans = MainFunctions.ClearTimeBans();
            if (clBans == true)
            {
                WriteLog("ALL TIME BANS CLEARED!", Color.Red);
            }
            else
            {
                WriteLog("Time ban clearance CANCELLED!", Color.Blue);
            }
        }

        private void cpuTimer_Tick(object sender, EventArgs e)
        {
            if (Process.GetProcessesByName(mangosName).Length > 0)
            {
                performanceCounter1.InstanceName = mangosName;

                progressBar1.Value = (int)performanceCounter1.NextValue();
                label10.Text = "MaNGOS CPU Usage: " + progressBar1.Value + @"%";
            }
            else
            {
                label10.Text = "MaNGOS not running!";
            }
            
        }

        private void button13_Click(object sender, EventArgs e)
        {
            IntPtr pWindow;
            Process[] proc = Process.GetProcessesByName(mangosName);
            if (proc.Length == 0)
            {

            }
            pPointer = (IntPtr)proc[0].Id;
            pWindow = (IntPtr)proc[0].MainWindowHandle;
            Win32.SetForegroundWindow(pWindow);
            MainFunctions.SendToConsole("motd " + Convert.ToString(textBox5.Text));
            SendKeys.SendWait("{ENTER}");
            Win32.SetForegroundWindow(this.Handle);
            HookToConsole.RemoveFrom();
            ReadConsole();
            WriteLog("MOTD changed to: " + textBox5.Text, Color.Red);
            textBox6.Text = "";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            rStarted = true;
            restarterButton_Click(null, null);
            if (checkBox2.Checked == true)
            {
                if (Process.GetProcessesByName(mangosName).Length > 0)
                {
                    Process[] Proc = Process.GetProcessesByName(mangosName);
                    Proc[0].Kill();
                    WriteLog("MaNGOS Killed Manually!", Color.Red);
                }
                else
                {
                    WriteLog("MaNGOS requested to be killed, but process not found!", Color.Red);
                }

            }
            if (checkBox3.Checked == true)
            {
                if (Process.GetProcessesByName(realmName).Length > 0)
                {
                    Process[] Proc = Process.GetProcessesByName(realmName);
                    Proc[0].Kill();
                    WriteLog("Realm Killed Manually!", Color.Red);
                }
                else
                {
                    WriteLog("Realm requested to be killed, but process not found!", Color.Red);
                }

            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            rStarted = false;
            
            if (checkBox2.Checked == true)
            {
                if (Process.GetProcessesByName(mangosName).Length > 0)
                {
                    IntPtr pWindow;
                    Process[] proc = Process.GetProcessesByName(mangosName);
                    pPointer = (IntPtr)proc[0].Id;
                    pWindow = (IntPtr)proc[0].MainWindowHandle;
                    Win32.SetForegroundWindow(pWindow);
                    MainFunctions.SendToConsole("exit");
                    SendKeys.SendWait("{ENTER}");
                    Win32.SetForegroundWindow(this.Handle);
                    HookToConsole.RemoveFrom();
                    ReadConsole();
                    WriteLog("MaNGOS Restarted Manually!", Color.Red);
                }
                else
                {
                    WriteLog("MaNGOS requested to be restarted, but process not found!", Color.Red);
                }

            }
            if (checkBox3.Checked == true)
            {
                if (Process.GetProcessesByName(realmName).Length > 0)
                {
                    Process[] Proc = Process.GetProcessesByName(realmName);
                    Proc[0].Kill();
                    WriteLog("Realm Restarted Manually!", Color.Red);
                }
                else
                {
                    WriteLog("Realm requested to be restarted, but process not found!", Color.Red);
                }

            }
            restarterButton_Click(null, null);
        }

        private void button19_Click(object sender, EventArgs e)
        {
            rStarted = true;
            restarterButton_Click(null, null);
            if (checkBox2.Checked == true)
            {
                if (Process.GetProcessesByName(mangosName).Length > 0)
                {
                    IntPtr pWindow;
                    Process[] proc = Process.GetProcessesByName(mangosName);
                    pPointer = (IntPtr)proc[0].Id;
                    pWindow = (IntPtr)proc[0].MainWindowHandle;
                    Win32.SetForegroundWindow(pWindow);
                    MainFunctions.SendToConsole("exit");
                    SendKeys.SendWait("{ENTER}");
                    Win32.SetForegroundWindow(this.Handle);
                    HookToConsole.RemoveFrom();
                    ReadConsole();
                    WriteLog("MaNGOS Shutdown Manually!", Color.Red);
                }
                else
                {
                    WriteLog("MaNGOS requested to be restarted, but process not found!", Color.Red);
                }

            }
            if (checkBox3.Checked == true)
            {
                if (Process.GetProcessesByName(realmName).Length > 0)
                {
                    Process[] Proc = Process.GetProcessesByName(realmName);
                    Proc[0].Kill();
                    WriteLog("Realm Shutdown Manually!", Color.Red);
                }
                else
                {
                    WriteLog("Realm requested to be shutdown, but process not found!", Color.Red);
                }

            }
        }

   }
}
