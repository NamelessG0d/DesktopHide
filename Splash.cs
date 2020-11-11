using System;
using DevExpress.Data;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Linq;
using System.IO;
using System.Diagnostics;

// set attributes
[assembly: AssemblyTitle("DesktopHide")]
[assembly: AssemblyDescription("Hook & Virtual Desktop")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyProduct("DesktopHide")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]

namespace DesktopHide
{

	public partial class Splash : Form
    {
        private const String AppID = "DesktopHide";
        private const String HookNotif = "f52d225f-96ab-497f-ad18-77156edc40a5";
        private const String UnHookNotif = "0f0af202-6553-41e9-8b7a-8228410a8052";
        private const String ByeNotif = "50s5f0a5-6969-4200-8500-6969re6969re";
        private const String BasicInstallFolder = @"C:\Program Files (x86)\DesktopHide";
        private const String BasicAppFolder = BasicInstallFolder + @"\" + AppID + ".exe";

        int HiddenDesktop;
        bool switched = false;

        private string[] menuItemStrings = { "Exit", "Unhook", "Created by NamelessGod", "Hook" };
        private ContextMenu contextMenu;
        MenuItem[] menuItem = new MenuItem[3];
        private bool isFinished = false;
        private string iconPath = Application.StartupPath + @"\Images\icon.ico";

        private globalKeyboardHook gkh;
        Keys pressed = Keys.None;

        public Splash()
        {
            InitializeComponent();
        }

        private void Splash_Load(object sender, EventArgs e)
        {
            //Automatically copy itself in a proper location and restart
            if (!File.Exists(BasicAppFolder) || Application.StartupPath != BasicInstallFolder)
            {
                if (!Directory.Exists(BasicInstallFolder))
                    Directory.CreateDirectory(BasicInstallFolder);
                try
                {
                    if(!File.Exists(BasicAppFolder))
                        File.Copy(Process.GetCurrentProcess().MainModule.FileName, BasicAppFolder);
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = BasicAppFolder
                    });
                    Environment.Exit(0);
                }
                catch (Exception)
                {
                    MessageBox.Show("Error with the executable relocation, the program might not work as intended");
                    throw;
                }
            }
            //Extract Icon For Shortcut for Notifications
            if (!File.Exists(iconPath))
            {
                if (!Directory.Exists(Path.GetDirectoryName(iconPath)))
                    Directory.CreateDirectory(Path.GetDirectoryName(iconPath));
                if (!WriteResourceToFile("DesktopHide.Images.icon.ico", iconPath))
                    MessageBox.Show("Error with the icon extraction, notifications might not work as intended");
            }

            SplashTimer.Tick += new EventHandler(TimerEventProcessor);
            SplashTimer.Start();

            Init();
        }
        private void TimerEventProcessor(Object myObject, EventArgs myEventArgs)
        {
            if (isFinished)
            {
                SplashTimer.Stop();
                this.Hide();
                if (System.Diagnostics.Process.GetProcessesByName(Path.GetFileNameWithoutExtension(Assembly.GetEntryAssembly().Location)).Count() > 1)
                    Application.Exit();
            }
            else
                Console.WriteLine("[LOG] : Hasen't finished initializing...");
        }
        private bool WriteResourceToFile(string resourceName, string fileName)
        {
            using (var resource = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
            {
                if (resource == null)
                    return false;
                using (var file = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                {
                    resource.CopyTo(file);
                    return true;
                }
            }
        }
        private void Init()
        {
            #region MenuItem Init
            for (int i = 0; i < 3; i++)
            {
                menuItem[i] = new MenuItem() { Text = "" };
            }
            InitMenuItems(menuItem);
            #endregion

            #region ContextMenu Init
            contextMenu = new ContextMenu();
            contextMenu.MenuItems.AddRange(this.menuItem);
            #endregion

            #region TrayIcon Init
            toastNotificationsManager.ApplicationId = AppID;
            if (!ShellHelper.IsApplicationShortcutExist("DesktopHide"))
            {
                ShellHelper.TryCreateShortcut(
                    applicationId: AppID,
                    name: "DesktopHide",
                    iconPath: Application.StartupPath + @"\Images\icon.ico");
                //restart the app
                Process.Start(BasicAppFolder);
                Environment.Exit(0);
            }

            trayIcon.ContextMenu = contextMenu;
            trayIcon.Visible = true;
            #endregion

            #region HookInit
            InitHook();
            gkh.hook();
            #endregion

            isFinished = true;
        }
        private void InitHook()
        {
            gkh = new globalKeyboardHook();
            gkh.HookedKeys.Add(Keys.RShiftKey);
            gkh.HookedKeys.Add(Keys.End);
            gkh.KeyDown += new KeyEventHandler(gkh_KeyDown);
            gkh.KeyUp += new KeyEventHandler(gkh_KeyUp);
        }
        private void InitMenuItems(MenuItem[] items)
        {
            for (int i = 0; i < items.Length; i++)
            {
                items[i].Index = i;
                items[i].Text = menuItemStrings[i];
                items[i].Click += new System.EventHandler(this.menuItem_Click);
            }
        }
        private void menuItem_Click(object Sender, EventArgs e)
        {
            var finished = false;
            MenuItem item = (MenuItem)Sender;
            if (item.Text == menuItemStrings[0])
            {
                SendNotification("bye");
                Application.Exit();
            }
            else if (item.Text == menuItemStrings[1] && !finished)
            {
                item.Text = menuItemStrings[3];
                gkh.unhook();
                SendNotification("unhook");
                finished = !finished;
                Thread.Sleep(100);
            }
            else if (item.Text == menuItemStrings[2] && !finished)
            {
                System.Diagnostics.Process.Start("https://discord.gg/MGuu5tF");
            }
            else if (item.Text == menuItemStrings[3] && !finished)
            {
                item.Text = menuItemStrings[1];
                gkh.hook();
                SendNotification("hook");
                finished = !finished;
                Thread.Sleep(100);
            }
        }
        void gkh_KeyDown(object sender, KeyEventArgs e)
        {
            if (pressed != Keys.None && pressed != e.KeyCode)
            {
				RotateScreen();
                pressed = Keys.None;
			}
            else
                pressed = e.KeyCode;
            //e.Handled = true; //Would cancel the actual keypress
        } 
        void gkh_KeyUp(object sender, KeyEventArgs e)
        {
            pressed = Keys.None;
            //e.Handled = true; //Would cancel the actual keypress
        }
        private void SendNotification(string type)
        {
            if (type == "hook")
                toastNotificationsManager.ShowNotification(HookNotif);
            else if (type == "unhook")
                toastNotificationsManager.ShowNotification(UnHookNotif);
            else if(type == "bye")
                toastNotificationsManager.ShowNotification(ByeNotif);
            else
                MessageBox.Show("Error with Notifications!");
        }
		private void RotateScreen()
        {
			Thread a = new Thread(this.threadRotateScreen);
			a.SetApartmentState(ApartmentState.STA);
			a.Start();
		}
        private void threadRotateScreen()
        {
            if (!switched) { 
                HiddenDesktop = VirtualDesktop.Desktop.FromDesktop(VirtualDesktop.Desktop.Create());
                VirtualDesktop.Desktop.FromIndex(HiddenDesktop).SetName("");
                VirtualDesktop.Desktop.FromIndex(HiddenDesktop).MakeVisible();
            }
            else
            {
                VirtualDesktop.Desktop.FromIndex(HiddenDesktop).Remove();
            }
            switched = !switched;
        }
    }
}

