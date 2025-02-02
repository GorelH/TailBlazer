﻿using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Threading;
using StructureMap;
using TailBlazer.Views;

namespace TailBlazer.Infrastucture
{
    public class BootStrap
    {
        [STAThread]
        public static void Main(string[] args)
        {

            var app = new App { ShutdownMode = ShutdownMode.OnLastWindowClose };
            app.InitializeComponent();


           var tempWindowToGetDispatcher = new MainWindow();

            var container = new Container(x => x.AddRegistry<AppRegistry>());
            container.Configure(x => x.For<Dispatcher>().Add(tempWindowToGetDispatcher.Dispatcher));
            container.GetInstance<StartupController>();

            var factory = container.GetInstance<WindowFactory>();

            var window = factory.Create(args);


            tempWindowToGetDispatcher.Close();
            window.Show();
            app.Run();
        }
    }
}
