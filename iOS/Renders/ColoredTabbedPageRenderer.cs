using Xamarin.Forms.Platform.iOS;
using RetroGameGauntlet.Core;
using Xamarin.Forms;
using RetroGameGauntlet.iOS.Renders;

[assembly: ExportRenderer(typeof(TabbedPage), typeof(ColoredTabbedPageRenderer))]
namespace RetroGameGauntlet.iOS.Renders
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