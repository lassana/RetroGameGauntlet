namespace RetroGameGauntlet.Core.Adapters

/// The web launcher that uses Safari ViewContoller on iOS and Chrome custom tabs on Android.
type IWebLauncherAdapter = 
    abstract member OpenUri: url:string -> unit