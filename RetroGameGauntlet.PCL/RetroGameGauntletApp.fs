namespace RetroGameGauntlet.PCL

open RetroGameGauntlet.PCL.Views
open System.Diagnostics
open Xamarin.Forms

type RetroGameGauntletApp() as this = 
    inherit Application()

    do
        this.Resources <- ResourceDictionary()
        this.Resources.Add("not", ValueConverters.BooleanNegationConverter())
        this.Resources.Add("backgroundColor", Color.FromHex("#444444"))
        this.Resources.Add("backgroundColorLighter", Color.FromHex("#888888"))
        base.MainPage <- NavigationPage(RootPage())

    override this.OnStart() = Debug.WriteLine "OnStart"

    override this.OnSleep() = Debug.WriteLine "OnSleep"

    override this.OnResume() = Debug.WriteLine "OnResume"
