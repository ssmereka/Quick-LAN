using System;
using System.Net;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Management;
using System.Net.NetworkInformation;


namespace Media_Project_C_Sharp
{
    public partial class Form1 : Form
    {
        Process start = new Process();
        private string main_dir = "";
        string curServer = "none";
        
        public Form1()
        {
            InitializeComponent();
        }

        private bool HDLS(ref string directory)
          {/*Pre: None
            *Post: String becomes HDLS directory location. 
            *      If not installed or dir does not exist the users is 
            *      prompted to locate/Install it. Directory then written to dir.
            */
            Directory.SetCurrentDirectory(main_dir);
            if (System.IO.File.Exists("dir"))
              {
                directory = readFile("dir",main_dir);
                return true;
              }
            else
              {
                MessageBox.Show("HDLS update tool must first be installed or located.");
                OpenFileDialog fDialog = new OpenFileDialog();
                fDialog.Title = "Open HdlsUpdateTool.exe";
                fDialog.Filter = "EXE Files|*.exe";
                fDialog.InitialDirectory = @"C:\Program Files\Valve\HLServer";
                if (fDialog.ShowDialog() == DialogResult.OK)
                  {
                    directory = fDialog.FileName.ToString();
                    if (directory != "")
                      writeFile(directory,"dir",main_dir);
                  } 
                else
                  {
                    File.Delete("dir");
                    OutputBox.Visible = true;
                    OutputBox.Text = "Error Locating HDLS update tool" + "\r" + "\n" + OutputBox.Text;
                    CloseOutputBox.Visible = true;
                  }
                return false;
              }
          }

        private bool installServer(string install, string saveDir)
        {
            string installDir = "";
            string dir = "";
            string temp = "";
            string verify = " -verify_all -retry";
            if (HDLS(ref dir))
            {
                temp = getDirectory();
                if (temp == "")
                    return false;
                writeFile(temp, saveDir, main_dir);
                installDir = "\"" + temp + "\"";
                writeFile("\""+dir+"\""+install+"\""+temp+
                          "\"" + verify, "install.bat", main_dir);
                Process run = new Process();
                run.StartInfo.FileName = "install.bat";
                run.Start();
                //run.WaitForExit();
                File.Delete(main_dir + "install.bat");
                return true;
            }
            else
                return false;
        }

        private void writeFile(string data, string filename, string location)
          {
            if (System.IO.File.Exists(location + "/" + filename))
              File.Delete(location + "/" + filename);
            TextWriter tw = new StreamWriter(location + "/" + filename);
            tw.WriteLine(data);
            tw.Close();
            tw.Dispose();
          }
        private string readFile(string filename, string location)
          {
            string dir = "";
            if(System.IO.File.Exists(location + "/" + filename))
              {
                TextReader tw = new StreamReader(location + "/" + filename);
                dir = tw.ReadLine();
                tw.Close();
                tw.Dispose();
                return dir;
              }
            return null;
          }
        private string MyText
        {
            get
            {
                return this.OutputBox.Text;
            }
            set
            {
                this.OutputBox.Text = value + "\r" + "\n" + this.OutputBox.Text;
            }
        }

        private void loadSettings()
        {
            string temp = "";
            Directory.SetCurrentDirectory(main_dir);
            if (System.IO.File.Exists(main_dir + "/custom_settings"))
            {
                TextReader settings = new StreamReader(main_dir + "/custom_settings");
                temp = settings.ReadLine();
                if (temp != null)
                {
                    Bitmap bit = new Bitmap(temp);
                    this.BackgroundImage = bit;
                }
                settings.Close();
            }
        }

        private void saveSettings(string str)
        {
            Directory.SetCurrentDirectory(main_dir);
            File.Delete(main_dir + "/images/custom_settings");
            TextWriter settings = new StreamWriter(main_dir + "/custom_settings");
            settings.WriteLine(str);
            settings.Close();
            settings.Dispose();
        }

        private string getDirectory()
        {
            string directory = "";
            folderBrowserDialog1.ShowDialog();
            directory = folderBrowserDialog1.SelectedPath;
            return directory;
        }

        private void viewServer(bool viewBool)
        {
            Cfg.Items.Clear();
            statusBar.Text = "";
            start_stop.Visible = viewBool;
            cfgButton.Visible = viewBool;
            commandBox.Visible = viewBool;
            enter.Visible = viewBool;
            Cfg.Visible = false;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            main_dir = Path.GetDirectoryName(Application.ExecutablePath);
            loadSettings();
            //getProcess();
        }

        private void installServerToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void teamFortress2ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void team4TressToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap bit = (Bitmap)Image.FromFile(main_dir + "/Images/team_4tress.jpg");
            this.BackgroundImage = bit;
            saveSettings(main_dir + "/Images/team_4tress.jpg");
        }

        private void tf2CakeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Bitmap bit = (Bitmap)Image.FromFile(main_dir + "/Images/tf2cake.jpg");
            this.BackgroundImage = bit;
            saveSettings(main_dir + "/Images/tf2cake.jpg");
        }

        private void portalCakeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap bit = (Bitmap)Image.FromFile(main_dir + "/Images/portalcake.jpg");
            this.BackgroundImage = bit;
            saveSettings(main_dir + "/Images/portalcake.jpg");
        }

        private void spyPyroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap bit = (Bitmap)Image.FromFile(main_dir + "/Images/spyro.jpg");
            this.BackgroundImage = bit;
            saveSettings(main_dir + "/Images/spyro.jpg");
        }

        private void hLDSEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Directory.SetCurrentDirectory(main_dir);
            Process hldsHelpFile = new Process();
            hldsHelpFile.StartInfo.FileName = "notepad.exe";
            hldsHelpFile.StartInfo.Arguments = main_dir + "/help files/hlds_error";
            hldsHelpFile.Start();
        }

        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void updateServerToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void installServerToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void hLDSUpdateToolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string dir = "";
            Process hdls = new Process();
            hdls.StartInfo.FileName = main_dir + "/Tools/hldsupdatetool.exe";
            hdls.Start();
            hdls.WaitForExit();
            if (HDLS(ref dir))
                MessageBox.Show("HDLS location is updated");
        }

        private void left4DeadToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (!(installServer(" -command update -game l4d_full -dir ", "l4d_dir")))
            {
                OutputBox.Visible = true;
                OutputBox.Text = "Error installing Left 4 Dead" + "\r" + "\n" + OutputBox.Text;
                CloseOutputBox.Visible = true;
            }
        }
        static void show()
        {

        }
        private void teamFortress2ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (!(installServer(" -command update -game tf -dir ", "tf2_dir")))
            {
                OutputBox.Visible = true;
                OutputBox.Text = "Error installing Team Fortress 2" + "\r" + "\n" + OutputBox.Text;
                CloseOutputBox.Visible = true;
            }
        }

        private void OutputBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e)
        {

        }

        private void outputBoxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (OutputBox.Visible)
                OutputBox.Visible = false;
            else
                OutputBox.Visible = true;
        }

        private void notepadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process notepad = new Process();
            notepad.StartInfo.FileName = "notepad.exe";
            notepad.Start();
        }
        /// <summary>
        /// Install Servers w/out command prompt. Current issue = 5 second time out/crash.
        /// </summary>
        /// <param name="install">install command must be passed.</param>
        /// <returns>true upon success, needs to evaluate integraty. False upon failure</returns>
        private bool InstallServer(string install)
        {
            string installDir = "";
            string dir = "";
            string output = "";
            string temp = "";
            if (HDLS(ref dir))
            {
                temp = getDirectory();
                if (temp == "")
                    return false;
                installDir = "\"" + temp + "\"";
                dir = "\"" + dir + "\"";
                System.Diagnostics.ProcessStartInfo Install = new System.Diagnostics.ProcessStartInfo("cmd.exe");
                Install.RedirectStandardInput = true;
                Install.RedirectStandardOutput = true;
                Install.RedirectStandardError = true;
                Install.UseShellExecute = false;
                //Install.CreateNoWindow = true;
                Install.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                System.Diagnostics.Process console = System.Diagnostics.Process.Start(Install);
                OutputBox.Visible = true;
                console.StandardInput.WriteLine(@dir + install + installDir);
                OutputBox.Refresh();   // re-paint a form
                do
                {
                    output = console.StandardOutput.ReadLine();
                    MyText = output;
                    OutputBox.Refresh();  // re-paint a form
                }
                while (output != null);
                //console.StandardInput.WriteLine("/c");
                console.WaitForExit(); //wait for exit
                OutputBox.Visible = false;
                return true;
            }
            else
                return false;
        }

        private void customToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string source = "";
            string fileName = "";
            string destination = "";
            OpenFileDialog bgrdDialog = new OpenFileDialog();
            bgrdDialog.Title = "Open Custom Background";
            bgrdDialog.InitialDirectory = @"C:\";
            if (bgrdDialog.ShowDialog() == DialogResult.OK)
            {
                source = bgrdDialog.FileName.ToString();
                fileName = System.IO.Path.GetFileName(source);
                destination = main_dir + "/Images/" + fileName;
                File.Delete(destination);
                File.Copy(source, destination, true);
                bgrdDialog.Dispose();
                Bitmap bit = (Bitmap)Image.FromFile(destination);
                this.BackgroundImage = bit;
                saveSettings(destination);
            }
            else
                MessageBox.Show("No file selected / does not exist");
        }

        private void Cfg_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public void OnDoubleClick(object sender, MouseEventArgs e)
        {
            string Value= "";
            Value = Cfg.Items[Cfg.SelectedIndex].ToString();
            CfgSelExe(Value);
        }

        private void startServerToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void CfgSelExe(string value)
        {
            switch (value)
            {
                case "Admin Password": 
                    break;
                case "All Talk":
                    break;
                case "Download Settings":
                    break;
                case "Party Mode":
                    break;
                case "Player Management":
                    break;
                case "Round/Game Timbers":
                    break;
                case "Security":
                    break;
                case "Team Balancing":
                    break;
            }
        }


        private void teamFortress2ToolStripMenuItem3_Click_1(object sender, EventArgs e)
        {
            //Start tf2 Server
            if (readFile("tf2_dir", main_dir) != null)
              {
                viewServer(true);
                //curServer = ServerType.tf2;
                curServer = "tf2";
                statusBar.Text = "Team Fortress 2";
                //Add Items To Listbox "Cfg"
                Cfg.Items.Add("Admin Password");
                Cfg.Items.Add("All Talk");
                Cfg.Items.Add("Download Settings");
                Cfg.Items.Add("Party Mode");
                Cfg.Items.Add("Player Management");
                Cfg.Items.Add("Round/Game Timers");
                Cfg.Items.Add("Security");
                Cfg.Items.Add("Team Balancing");
                //Add Items to Combobox "commandBox"
                commandBox.Items.Add("Start");
              }
            else
              {
                OutputBox.Visible = true;
                CloseOutputBox.Visible = true;
                OutputBox.Text = "You must first install the Team Fortress 2 Server";
              }
        }

        private void start_stop_Click(object sender, EventArgs e)
        {

        }

        private void left4DeadToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            //Start l4d Server
            if (readFile("l4d_dir", main_dir) != null)
              {
                viewServer(true);
                statusBar.Text = "Left 4 Dead";
              }
            else
              {
                OutputBox.Visible = true;
                CloseOutputBox.Visible = true;
                OutputBox.Text = "You must first install the Left 4 Dead Server";
              }
        }

        private void cfgButton_Click(object sender, EventArgs e)
        {
            if (Cfg.Visible)
                Cfg.Visible = false;
            else
                Cfg.Visible = true;
        }

        private void start_stop_Click_1(object sender, EventArgs e)
        {
            string data ="";
            string map = "";
            string maxPlayers = "";
            string argu = "";
            Process tf2 = new Process();
            Process l4d = new Process();
            bool tf2Running = false;
            bool l4dRunning = false;
            if (start_stop.Text == "Start")
            {
                data = "\"" + readFile(curServer + "_dir", main_dir + "/");
                switch (curServer)
                {
                    //orangebox\srcds.exe -console -game tf +map ctf_2fort +maxplayers 24
                    case "tf2": data += "/orangebox/srcds.exe\"";
                                argu = " -console -game tf +map " + map + " +maxplayers " + maxPlayers;
                                map = "ctf_2fort";
                                maxPlayers = "24";
                                //Process tf2 = new Process();

                                tf2.StartInfo.FileName = data;
                                tf2.StartInfo.Arguments = argu;
                                tf2.StartInfo.UseShellExecute = false;
                                tf2.Start();
                           
                        break;
                    case "l4d": data += "/l4d/srcds.exe\" -console -game l4d +map ";
                                data += map + " +maxplayers " + maxPlayers;
                                //Process l4d = new Process();
                                l4d.StartInfo.FileName = data;
                                l4d.StartInfo.Arguments = argu;
                                l4d.StartInfo.UseShellExecute = false;
                                l4d.Start();
                                l4dRunning = true;
                        break;
                }
                //start.StartInfo.RedirectStandardInput = true;
                //start.OutputDataReceived
                //start.StartInfo.RedirectStandardOutput = true;
                //start.StartInfo.RedirectStandardError = true;
                //Process temp = new Process();
                //start here
                //temp.StartInfo = start.StartInfo;
                //temp.Start();
                //temp.StandardInput.AutoFlush = true ;              
                /*
                writeFile(data, curServer + "_start.bat", main_dir);
                Process start = new Process();
                start.StartInfo.FileName = main_dir + "/" + curServer + "_start.bat";
                start.Start();
                start.Close();
                start.Dispose();
                */
                start_stop.Text = "Stop";
            }
            else
            {
                switch(curServer)
                {
                    case "tf2":
                        if (tf2Running)
                        {
                            tf2.Kill();
                            tf2Running = false;
                        }
                        break;
                    case "l4d": 
                        if (l4dRunning)
                        {
                            l4d.Kill();
                            l4dRunning = false;
                        }
                                  
                        break;
                }
                start_stop.Text = "Start";
            }
        }

        private void noneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            viewServer(false);
            OutputBox.Visible = false;
            CloseOutputBox.Visible = false;
        }

        private void commandBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Enter_Click(object sender, EventArgs e)
        {
            string command = "";
            Object selectedItem = new Object();
            selectedItem = commandBox.SelectedItem;
            if ((selectedItem != null) && (start_stop.Text != "Start"))
              {
                command = selectedItem.ToString();
                Process.EnterDebugMode();
                start.BeginOutputReadLine();
                start.StandardInput.WriteLine(@command);
                //commandBox.GetItemText(command);
                //MessageBox.Show(command);
              }
        }

        private void OnEnter(object sender, EventArgs e)
        {
            //Enter_Click(sender,e);
        }

        private void CloseOutputBox_Click(object sender, EventArgs e)
        {
            CloseOutputBox.Visible = false;
            OutputBox.Visible = false;
            OutputBox.Text = "";
        }

        private void commandPromptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.Start();
        }

        private string getExternalIp()
        {
            try
            {
                string whatIsMyIp = "http://www.whatismyip.com/automation/n09230945.asp";
                WebClient wc = new WebClient();
                UTF8Encoding utf8 = new UTF8Encoding();
                string requestHtml = "";

                requestHtml = utf8.GetString(wc.DownloadData(whatIsMyIp));

                IPAddress externalIp = null;

                externalIp = IPAddress.Parse(requestHtml);
                return externalIp.ToString();
            }
            catch
            {
                return "";
            }
        }
        private string getLocalIp()
        {
            string strHostName = Dns.GetHostName();
            IPHostEntry ipEntry = Dns.GetHostEntry(strHostName);
            IPAddress[] addr = ipEntry.AddressList;
            return addr[0].ToString();
            //Console.WriteLine(addr[0]);
            //Console.ReadKey();
        }

        private  string getGatewayIp()
        {
            string _GatewayID = string.Empty;
            NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface adapter in adapters)
            {
                IPInterfaceProperties adapterProperties = adapter.GetIPProperties();
                GatewayIPAddressInformationCollection addresses = adapterProperties.GatewayAddresses;
                if (addresses.Count > 0)
                {
                    foreach (GatewayIPAddressInformation address in addresses)
                    {
                        if (string.IsNullOrEmpty(_GatewayID))
                            _GatewayID += address.Address.ToString();
                        else
                            _GatewayID += address.Address.ToString() + "\r\n";
                    }
                }
            }
            return _GatewayID;
        }
        private void iPAddressToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OutputBox.Visible = true;
            CloseOutputBox.Visible = true;
            OutputBox.Text = "External IP: " + getExternalIp() + "\r" + "\n";
            OutputBox.Text += "IP Address: " + getLocalIp() + "\r" + "\n";
            OutputBox.Text += "Default Gateway: "+ getGatewayIp() + "\r" + "\n";
        }

        private void statusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (statusStrip1.Visible == true)
                statusStrip1.Visible = false;
            else
                statusStrip1.Visible = true;
        }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void getProcess()
        {
            Process[] processlist = Process.GetProcesses();
            foreach (Process theprocess in processlist)
            {
                //string output = "";
                if (theprocess.ProcessName == "srcds")
                    start_stop.Text = "Stop";
                    //MessageBox.Show("Hmm");
                if (theprocess.ProcessName == "cmd")
                { }
                    //output = theprocess.ReadLine();
                //MessageBox.Show(output);
                OutputBox.Visible = true;
                OutputBox.Text += "Process: {0} ID: {1} " + theprocess.ProcessName + theprocess.Id;
                OutputBox.Text += "\r" + "\n";
                //Console.WriteLine("Process: {0} ID: {1}", theprocess.ProcessName, theprocess.Id);
            }
        }
    }
}
