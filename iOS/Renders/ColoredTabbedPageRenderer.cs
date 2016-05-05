using System;
using Xamarin.Forms.Platform.iOS;
using Core;
using Xamarin.Forms;
using Renders;

[assembly: ExportRenderer(typeof(TabbedPage), typeof(ColoredTabbedPageRenderer))]
namespace Renders
{
    public class ColoredTabbedPageRenderer : TabbedRenderer 
    {
        public ColoredTabbedPageRenderer ()
        {
            TabBar.TintColor = Color.White.ToUIColor();
            TabBar.BarTintColor = Colors.BackgroundColor.ToUIColor();
            TabBar.BackgroundColor = Color.Blue.ToUIColor();
        }
    }
}