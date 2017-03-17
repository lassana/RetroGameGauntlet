using Xamarin.Forms.Platform.iOS;
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
            TabBar.BarTintColor = ((Color)Xamarin.Forms.Application.Current.Resources["backgroundColor"]).ToUIColor();
            TabBar.BackgroundColor = Color.Blue.ToUIColor();
        }
    }
}