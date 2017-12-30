using System;
using Android.App;
using Android.Content.PM;
using Android.Util;
using RetroGameGauntlet.PCL.Adapters;

namespace RetroGameGauntlet.Droid.Adapters
{
    public class AppInfoAdapter : IAppInfoAdapter
    {
        private const string Tag = "AppInfoAdapter";

        public AppInfoAdapter()
        {
        }

        public string VersionName
        {
            get
            {
                try
                {
                    return Application.Context.PackageManager.GetPackageInfo(Application.Context.PackageName, 
                                                                             PackageInfoFlags.MetaData).VersionName;
                }
                catch (Exception e)
                {
                    //We cannot log it using MetroLog (will cause StackOverflow crash)
                    Log.Error(Tag, "Cannot get app version", e);
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
                    return Application.Context.PackageManager.GetPackageInfo(Application.Context.PackageName,
                                                                             PackageInfoFlags.MetaData).VersionCode.ToString();
                }
                catch (Exception e)
                {
                    //We cannot log it using MetroLog (will cause StackOverflow crash)
                    Log.Error(Tag, "Cannot get app version code", e);
                    return "0";
                }
            }
        }
    }
}