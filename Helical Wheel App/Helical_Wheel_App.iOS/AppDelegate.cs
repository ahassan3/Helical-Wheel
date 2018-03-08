using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using System.IO;
using System.Xml.Linq;

namespace Helical_Wheel_App.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            App.ScreenWidth = (float)UIScreen.MainScreen.Bounds.Width;
            App.ScreenHeight = (float)UIScreen.MainScreen.Bounds.Height;
            LoadApplication(new App());
            FileStream.PathApp = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var docmentPath = Path.Combine(FileStream.PathApp, FileStream.FileName);
            try
            {
                XDocument doc = XDocument.Load(docmentPath);
            }
            catch
            {
                XDocument doc = new XDocument(new XElement("protein_list"));
                Stream stream = File.Open(docmentPath, FileMode.Open);
                doc.Save(stream);
            }
            return base.FinishedLaunching(app, options);
        }
        public UIInterfaceOrientationMask GetSupportedInterfaceOrientations(UIApplication application, IntPtr forWindow)

        {

            return UIInterfaceOrientationMask.Portrait;

        }
    }
}
