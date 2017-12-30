namespace RetroGameGauntlet.PCL

open RetroGameGauntlet.PCL.Views
open System.Diagnostics
open Xamarin.Forms

///The application type.
type GauntletApp() as this = 
    inherit Application()

    do
        let backgroundColor = Color.FromHex("#444444")
        let textColor = Color.White;
        let backgroundColorLighter = Color.FromHex("#888888")

        this.Resources <- ResourceDictionary()
        this.Resources.Add("not", ValueConverters.BooleanNegationConverter())
        this.Resources.Add("backgroundColor", backgroundColor)
        this.Resources.Add("textColor", textColor)
        this.Resources.Add("backgroundColorLighter", backgroundColorLighter)
        base.MainPage <- NavigationPage(
            RootPage(),
            BarBackgroundColor = backgroundColor,
            BarTextColor = textColor)

    override this.OnStart() = Debug.WriteLine "OnStart"

    override this.OnSleep() = Debug.WriteLine "OnSleep"

    override this.OnResume() = Debug.WriteLine "OnResume"
