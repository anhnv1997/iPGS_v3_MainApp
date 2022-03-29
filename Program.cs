using iParking.Forms;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace iParking
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            CreateHostBuilder().Build().RunAsync();
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            bool createdNew;

            Mutex m = new Mutex(true, "myApp", out createdNew);

            if (!createdNew)
            {
                // myApp is already running...
                MessageBox.Show("iPGS is already running!", "Multiple Instances");
                return;
            }
            frmLoading frm = new frmLoading();
            frm.LoadingData();
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                Form = new Form1();
                Application.Run(Form);
            }
        }
        public static IHostBuilder CreateHostBuilder() =>
    Host.CreateDefaultBuilder()
        .ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder.UseUrls("http://*:8081");
            webBuilder.UseStartup<Startup>();
        });
        public static Form1 Form { get; private set; }
    }
}
