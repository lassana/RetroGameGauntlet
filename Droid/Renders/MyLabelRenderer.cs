using System;
using Xamarin.Forms;
using RetroGameGauntlet.Droid.Renders;

//[assembly: ExportRenderer(typeof(Label), typeof(MyLabelRenderer))]
//[assembly: ExportRenderer(typeof(Button), typeof(MyLabelRenderer))]
namespace RetroGameGauntlet.Droid.Renders
{
    //public class MyLabelRenderer : Xamarin.Forms.Platform.Android.LabelRenderer
    public class MyLabelRenderer : Xamarin.Forms.Platform.Android.AppCompat.ButtonRenderer
    {
        public MyLabelRenderer()
        {
            throw new Exception("MyLabelRenderer#MyLabelRenderer");
        }
    }
}

