// Initial tcp/ip server component taken from MSDN 101 examples, advanced networking in C#
// Modified to display a little more information and to ignore invalid packets
// Most of the server piece is still intact for simple chat
// This is for Proof of Concept ONLY 
// This is to show how to make a simple game server
// At this point, all messages received come across with a CHAT and is parsed

using System;
using System.Collections;
using System.Threading;
using System.Windows.Forms;
using System.Net.Sockets;

public class frmMain : System.Windows.Forms.Form
{

    #region " Windows Form Designer generated code "

    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        Application.Run(new frmMain());
    }

    public frmMain()
    {
        //This call is required by the Windows Form Designer.
        InitializeComponent();
        //Add any initialization after the InitializeComponent() call
        // So that we only need to set the title of the application once,
        // we use the AssemblyInfo class (defined in the AssemblyInfo.cs file)
        // to read the AssemblyTitle attribute.
        //AssemblyInfo ainfo = new AssemblyInfo();
        //this.Text = ainfo.Title;
        //this.mnuAbout.Text = string.Format("&About {0} ...", ainfo.Title);
    }

    //Form overrides dispose to clean up the component list.
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            if (components != null)
            {
                components.Dispose();
            }
        }
        base.Dispose(disposing);
    }
    //Required by the Windows Form Designer
    private System.ComponentModel.IContainer components = null;
    //NOTE: The following procedure is required by the Windows Form Designer
    //It can be modified using the Windows Form Designer.  
    //Do not modify it using the code editor.
    private System.Windows.Forms.MainMenu mnuMain;

    private System.Windows.Forms.MenuItem mnuFile;

    private System.Windows.Forms.MenuItem mnuExit;

    private System.Windows.Forms.MenuItem mnuHelp;

    private System.Windows.Forms.MenuItem mnuAbout;

    private System.Windows.Forms.ListBox lstStatus;

    private System.Windows.Forms.Button btnBroadcast;

    private System.Windows.Forms.TextBox txtBroadcast;

    private System.Windows.Forms.Label lblInstructions;
    private System.Windows.Forms.ListBox lstPlayers;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Button btnPM;
    private System.Windows.Forms.Button btnKick;
    private System.Windows.Forms.TextBox textBox1;
    private System.Windows.Forms.Label label3;

    private System.Windows.Forms.Label Label1;

    private void InitializeComponent()
    {
            this.components = new System.ComponentModel.Container();
            this.mnuMain = new System.Windows.Forms.MainMenu(this.components);
            this.mnuFile = new System.Windows.Forms.MenuItem();
            this.mnuExit = new System.Windows.Forms.MenuItem();
            this.mnuHelp = new System.Windows.Forms.MenuItem();
            this.mnuAbout = new System.Windows.Forms.MenuItem();
            this.lstStatus = new System.Windows.Forms.ListBox();
            this.txtBroadcast = new System.Windows.Forms.TextBox();
            this.btnBroadcast = new System.Windows.Forms.Button();
            this.lblInstructions = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.lstPlayers = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnPM = new System.Windows.Forms.Button();
            this.btnKick = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // mnuMain
            // 
            this.mnuMain.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuFile,
            this.mnuHelp});
            this.mnuMain.RightToLeft = System.Windows.Forms.RightToLeft.No;
            // 
            // mnuFile
            // 
            this.mnuFile.Enabled = false;
            this.mnuFile.Index = 0;
            this.mnuFile.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuExit});
            this.mnuFile.ShowShortcut = false;
            this.mnuFile.Text = "";
            this.mnuFile.Visible = false;
            // 
            // mnuExit
            // 
            this.mnuExit.Enabled = false;
            this.mnuExit.Index = 0;
            this.mnuExit.ShowShortcut = false;
            this.mnuExit.Text = "";
            this.mnuExit.Visible = false;
            this.mnuExit.Click += new System.EventHandler(this.mnuExit_Click);
            // 
            // mnuHelp
            // 
            this.mnuHelp.Enabled = false;
            this.mnuHelp.Index = 1;
            this.mnuHelp.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuAbout});
            this.mnuHelp.ShowShortcut = false;
            this.mnuHelp.Text = "";
            this.mnuHelp.Visible = false;
            // 
            // mnuAbout
            // 
            this.mnuAbout.Enabled = false;
            this.mnuAbout.Index = 0;
            this.mnuAbout.ShowShortcut = false;
            this.mnuAbout.Text = "";
            this.mnuAbout.Visible = false;
            this.mnuAbout.Click += new System.EventHandler(this.mnuAbout_Click);
            // 
            // lstStatus
            // 
            this.lstStatus.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lstStatus.Enabled = false;
            this.lstStatus.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lstStatus.IntegralHeight = false;
            this.lstStatus.Location = new System.Drawing.Point(161, 82);
            this.lstStatus.Name = "lstStatus";
            this.lstStatus.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lstStatus.Size = new System.Drawing.Size(0, 0);
            this.lstStatus.TabIndex = 0;
            this.lstStatus.Visible = false;
            // 
            // txtBroadcast
            // 
            this.txtBroadcast.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtBroadcast.Enabled = false;
            this.txtBroadcast.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.txtBroadcast.Location = new System.Drawing.Point(161, 82);
            this.txtBroadcast.MaxLength = 0;
            this.txtBroadcast.Name = "txtBroadcast";
            this.txtBroadcast.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtBroadcast.Size = new System.Drawing.Size(0, 20);
            this.txtBroadcast.TabIndex = 0;
            this.txtBroadcast.Visible = false;
            this.txtBroadcast.WordWrap = false;
            // 
            // btnBroadcast
            // 
            this.btnBroadcast.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnBroadcast.Enabled = false;
            this.btnBroadcast.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBroadcast.ImageIndex = 0;
            this.btnBroadcast.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnBroadcast.Location = new System.Drawing.Point(161, 82);
            this.btnBroadcast.Name = "btnBroadcast";
            this.btnBroadcast.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnBroadcast.Size = new System.Drawing.Size(0, 0);
            this.btnBroadcast.TabIndex = 0;
            this.btnBroadcast.Visible = false;
            this.btnBroadcast.Click += new System.EventHandler(this.btnBroadcast_Click);
            // 
            // lblInstructions
            // 
            this.lblInstructions.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblInstructions.Enabled = false;
            this.lblInstructions.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblInstructions.ImageIndex = 0;
            this.lblInstructions.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblInstructions.Location = new System.Drawing.Point(161, 82);
            this.lblInstructions.Name = "lblInstructions";
            this.lblInstructions.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblInstructions.Size = new System.Drawing.Size(0, 0);
            this.lblInstructions.TabIndex = 0;
            this.lblInstructions.Visible = false;
            // 
            // Label1
            // 
            this.Label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Label1.Enabled = false;
            this.Label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.Label1.ImageIndex = 0;
            this.Label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Label1.Location = new System.Drawing.Point(161, 82);
            this.Label1.Name = "Label1";
            this.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Label1.Size = new System.Drawing.Size(0, 0);
            this.Label1.TabIndex = 0;
            this.Label1.Visible = false;
            // 
            // lstPlayers
            // 
            this.lstPlayers.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lstPlayers.Enabled = false;
            this.lstPlayers.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lstPlayers.IntegralHeight = false;
            this.lstPlayers.Location = new System.Drawing.Point(161, 82);
            this.lstPlayers.Name = "lstPlayers";
            this.lstPlayers.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lstPlayers.Size = new System.Drawing.Size(0, 0);
            this.lstPlayers.TabIndex = 0;
            this.lstPlayers.Visible = false;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.Enabled = false;
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label2.ImageIndex = 0;
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(161, 82);
            this.label2.Name = "label2";
            this.label2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label2.Size = new System.Drawing.Size(0, 0);
            this.label2.TabIndex = 0;
            this.label2.Visible = false;
            // 
            // btnPM
            // 
            this.btnPM.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnPM.Enabled = false;
            this.btnPM.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPM.ImageIndex = 0;
            this.btnPM.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnPM.Location = new System.Drawing.Point(161, 82);
            this.btnPM.Name = "btnPM";
            this.btnPM.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnPM.Size = new System.Drawing.Size(0, 0);
            this.btnPM.TabIndex = 0;
            this.btnPM.Visible = false;
            // 
            // btnKick
            // 
            this.btnKick.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnKick.Enabled = false;
            this.btnKick.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKick.ImageIndex = 0;
            this.btnKick.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnKick.Location = new System.Drawing.Point(161, 82);
            this.btnKick.Name = "btnKick";
            this.btnKick.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnKick.Size = new System.Drawing.Size(0, 0);
            this.btnKick.TabIndex = 0;
            this.btnKick.Visible = false;
            // 
            // textBox1
            // 
            this.textBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBox1.Enabled = false;
            this.textBox1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.textBox1.Location = new System.Drawing.Point(161, 82);
            this.textBox1.MaxLength = 0;
            this.textBox1.Name = "textBox1";
            this.textBox1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBox1.Size = new System.Drawing.Size(0, 20);
            this.textBox1.TabIndex = 0;
            this.textBox1.Visible = false;
            this.textBox1.WordWrap = false;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.Enabled = false;
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label3.ImageIndex = 0;
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(161, 82);
            this.label3.Name = "label3";
            this.label3.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label3.Size = new System.Drawing.Size(0, 0);
            this.label3.TabIndex = 0;
            this.label3.Visible = false;
            // 
            // frmMain
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(323, 165);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.btnKick);
            this.Controls.Add(this.btnPM);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lstPlayers);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.lblInstructions);
            this.Controls.Add(this.btnBroadcast);
            this.Controls.Add(this.txtBroadcast);
            this.Controls.Add(this.lstStatus);
            this.Enabled = false;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Menu = this.mnuMain;
            this.Name = "frmMain";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Closing += new System.ComponentModel.CancelEventHandler(this.frmMain_Closing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    #region " Standard Menu Code "

    // This code simply shows the About form.
    private void mnuAbout_Click(object sender, System.EventArgs e)
    {
        // Open the About form in Dialog Mode
        //frmAbout frm = new frmAbout();
        //frm.ShowDialog(this);
        //frm.Dispose();
    }

    // This code will close the form.
    private void mnuExit_Click(object sender, System.EventArgs e)
    {
        // Close the current form
        this.Close();
    }

    #endregion

    const int PORT_NUM = 4551;
    private Hashtable clients = new Hashtable();
    private TcpListener listener;
    private Thread listenerThread;

    // This subroutine sends a message to all attached clients
    private void Broadcast(string strMessage)
    {
        UserConnection client;
        // All entries in the clients Hashtable are UserConnection so it is possible
        // to assign it safely.
        foreach (DictionaryEntry entry in clients)
        {
            client = (UserConnection)entry.Value;
            client.SendData(strMessage);
        }
    }

    // This subroutine sends the contents of the Broadcast textbox to all clients, if
    // it is not empty, and clears the textbox
    private void btnBroadcast_Click(object sender, System.EventArgs e)
    {
        if (txtBroadcast.Text != "")
        {
            UpdateStatus("Broadcasting: " + txtBroadcast.Text);
            Broadcast("BROAD|" + txtBroadcast.Text);
            txtBroadcast.Text = string.Empty;
        }
    }

    // This subroutine checks to see if username already exists in the clients 
    // Hashtable.  if it does, send a REFUSE message, otherwise confirm with a JOIN.
    private void ConnectUser(string userName, UserConnection sender)
    {
        if (clients.Contains(userName))
        {
            ReplyToSender("REFUSE", sender);
        }
        else
        {
            sender.Name = userName;
            UpdateStatus(userName + " has joined the chat.");
            clients.Add(userName, sender);
            lstPlayers.Items.Add(sender.Name);
            // Send a JOIN to sender, and notify all other clients that sender joined
            ReplyToSender("JOIN", sender);
            SendToClients("CHAT|" + sender.Name + " has joined the chat.", sender);
        }
    }

    // This subroutine notifies other clients that sender left the chat, and removes
    // the name from the clients Hashtable
    private void DisconnectUser(UserConnection sender)
    {
        UpdateStatus(sender.Name + " has left the chat.");
        SendToClients("CHAT|" + sender.Name + " has left the chat.", sender);
        clients.Remove(sender.Name);
        lstPlayers.Items.Remove(sender.Name);
    }

    // This subroutine is used a background listener thread to allow reading incoming
    // messages without lagging the user interface.
    private void DoListen()
    {
        try
        {
            // Listen for new connections.
            listener = new TcpListener(System.Net.IPAddress.Any, PORT_NUM);
            listener.Start();

            do
            {
                // Create a new user connection using TcpClient returned by
                // TcpListener.AcceptTcpClient()
                UserConnection client = new UserConnection(listener.AcceptTcpClient());
                // Create an event handler to allow the UserConnection to communicate
                // with the window.
                client.LineReceived += new LineReceive(OnLineReceived);
                //AddHandler client.LineReceived, AddressOf OnLineReceived;
                UpdateStatus("new connection found: waiting for log-in");
            } while (true);
        }
        catch (Exception ex)
        {
            //MessageBox.Show(ex.ToString());
        }
    }

    // When the window closes, stop the listener.
    private void frmMain_Closing(object sender, System.ComponentModel.CancelEventArgs e) //base.Closing;
    {
        listener.Stop();
    }

    // Start the background listener thread.
    private void frmMain_Load(object sender, System.EventArgs e)
    {
        listenerThread = new Thread(new ThreadStart(DoListen));
        listenerThread.Start();
        UpdateStatus("Listener started");
    }

    // Concatenate all the client names and send them to the user who requested user list
    private void ListUsers(UserConnection sender)
    {
        UserConnection client;
        string strUserList;
        UpdateStatus("Sending " + sender.Name + " a list of users online.");
        strUserList = "LISTUSERS";
        // All entries in the clients Hashtable are UserConnection so it is possible
        // to assign it safely.

        foreach (DictionaryEntry entry in clients)
        {
            client = (UserConnection)entry.Value;
            strUserList = strUserList + "|" + client.Name;
        }

        // Send the list to the sender.
        ReplyToSender(strUserList, sender);
    }




    // This is the event handler for the UserConnection when it receives a full line.
    // Parse the cammand and parameters and take appropriate action.
    private void OnLineReceived(UserConnection sender, string data)
    {
        string[] dataArray;
        // Message parts are divided by "|"  Break the string into an array accordingly.
        // Basically what happens here is that it is possible to get a flood of data during
        // the lock where we have combined commands and overflow
        // to simplify this proble, all I do is split the response by char 13 and then look
        // at the command, if the command is unknown, I consider it a junk message
        // and dump it, otherwise I act on it
        dataArray = data.Split((char)13);
        dataArray = dataArray[0].Split((char)124);

        // dataArray(0) is the command.
        switch (dataArray[0])
        {
            case "CONNECT":
                ConnectUser(dataArray[1], sender);
                break;
            case "CHAT":
                SendChat(dataArray[1], sender);
                break;
            case "DISCONNECT":
                DisconnectUser(sender);
                break;
            case "REQUESTUSERS":
                ListUsers(sender);
                break;
            default:
                // Message is junk do nothing with it.
                break;
        }
    }

    // This subroutine sends a response to the sender.
    private void ReplyToSender(string strMessage, UserConnection sender)
    {
        sender.SendData(strMessage);
    }

    // Send a chat message to all clients except sender.
    private void SendChat(string message, UserConnection sender)
    {
        UpdateStatus(sender.Name + ": " + message);
        SendToClients("CHAT|" + sender.Name + ": " + message, sender);
    }

    // This subroutine sends a message to all attached clients except the sender.
    private void SendToClients(string strMessage, UserConnection sender)
    {
        UserConnection client;
        // All entries in the clients Hashtable are UserConnection so it is possible
        // to assign it safely.
        foreach (DictionaryEntry entry in clients)
        {
            client = (UserConnection)entry.Value;
            // Exclude the sender.
            if (client.Name != sender.Name)
            {
                client.SendData(strMessage);
            }
        }
    }

    // This subroutine adds line to the Status listbox
    private void UpdateStatus(string statusMessage)
    {
        lstStatus.Items.Add(statusMessage);
    }
}
