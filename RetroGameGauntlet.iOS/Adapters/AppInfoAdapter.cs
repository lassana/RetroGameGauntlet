using System;
using Foundation;
using RetroGameGauntlet.PCL.Adapters;

namespace RetroGameGauntlet.iOS.Adapters
{
    public class AppInfoAdapter : IAppInfoAdapter
    {
        public AppInfoAdapter()
        {
        }

        public string VersionName
        {
            get
            {
                try
                {
                    var version = NSBundle.MainBundle.InfoDictionary.ValueForKey(new NSString("CFBundleShortVersionString"));
                    return version.ToString();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Cannot get CFBundleShortVersionString: " + e);
                    return string.Empty;
                }
            }
        }

        public string VersionCode
        {
            get
            {
                try
                {
                    var build = NSBundle.MainBundle.InfoDictionary.ValueForKey(new NSString("CFBundleVersion"));
                    return build.ToString();
                }
                catch (Exception e)
                {
                    //We cannot log it using MetroLog (will cause StackOverflow crash)
                    Console.WriteLine("Cannot get CFBundleVersion: " + e);
                    return string.Empty;
                }
            }
        }
    }
}
