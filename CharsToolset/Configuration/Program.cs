using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using PublicMethodLibrary;
namespace CharsToolset
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]

        static void Main()
        {
            //Application.ThreadException += ApplicationExc.Application_ThreadException;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new RootDisplayForm());
        }
    }
}
