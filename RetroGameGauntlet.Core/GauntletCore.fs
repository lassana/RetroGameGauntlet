namespace RetroGameGauntlet.Core

open SimpleInjector
open System.Net.Http

/// The app's core.
[<AbstractClass; Sealed>]
type GauntletCore() = 
    static let container = new Container()
    static let httpClient = new HttpClient()

    static do
        container.Register<HttpClient>(fun _ -> httpClient);
    
    static member Container = container

    static member HttpClient = container.GetInstance<HttpClient>()