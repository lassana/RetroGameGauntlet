namespace RetroGameGauntlet.PCL.Adapters

/// The app information adapter.
type IAppInfoAdapter = 
    abstract member VersionName: string
    abstract member VersionCode: string