namespace DesktopHide
{
    partial class Splash
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Splash));
            this.SplashTimer = new System.Windows.Forms.Timer(this.components);
            this.trayIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.toastNotificationsManager = new DevExpress.XtraBars.ToastNotifications.ToastNotificationsManager(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.toastNotificationsManager)).BeginInit();
            this.SuspendLayout();
            // 
            // SplashTimer
            // 
            this.SplashTimer.Enabled = true;
            this.SplashTimer.Interval = 1000;
            // 
            // trayIcon
            // 
            this.trayIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("trayIcon.Icon")));
            this.trayIcon.Text = "DesktopHide";
            // 
            // toastNotificationsManager
            // 
            this.toastNotificationsManager.ApplicationId = "9c6c77bf-39f1-4b22-b461-b0512e530108";
            this.toastNotificationsManager.Notifications.AddRange(new DevExpress.XtraBars.ToastNotifications.IToastNotificationProperties[] {
            new DevExpress.XtraBars.ToastNotifications.ToastNotification("f52d225f-96ab-497f-ad18-77156edc40a5", null, null, ((System.Drawing.Image)(resources.GetObject("toastNotificationsManager.Notifications"))), null, null, null, "Hooked", "Hooked the keys :)", "", null, DevExpress.XtraBars.ToastNotifications.ToastNotificationSound.Default, DevExpress.XtraBars.ToastNotifications.ToastNotificationDuration.Default, null, DevExpress.XtraBars.ToastNotifications.AppLogoCrop.Circle, DevExpress.XtraBars.ToastNotifications.ToastNotificationTemplate.Generic),
            new DevExpress.XtraBars.ToastNotifications.ToastNotification("0f0af202-6553-41e9-8b7a-8228410a8052", null, null, ((System.Drawing.Image)(resources.GetObject("toastNotificationsManager.Notifications1"))), null, null, null, "UnHooked", "Disabled the hook for the keyboard", "", null, DevExpress.XtraBars.ToastNotifications.ToastNotificationSound.Default, DevExpress.XtraBars.ToastNotifications.ToastNotificationDuration.Default, null, DevExpress.XtraBars.ToastNotifications.AppLogoCrop.Circle, DevExpress.XtraBars.ToastNotifications.ToastNotificationTemplate.Generic)});
            // 
            // Splash
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(500, 290);
            this.ControlBox = false;
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Splash";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Splash";
            this.Load += new System.EventHandler(this.Splash_Load);
            ((System.ComponentModel.ISupportInitialize)(this.toastNotificationsManager)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer SplashTimer;
        private System.Windows.Forms.NotifyIcon trayIcon;
        private DevExpress.XtraBars.ToastNotifications.ToastNotificationsManager toastNotificationsManager;
    }
}

