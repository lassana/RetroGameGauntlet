namespace RetroGameGauntlet.PCL

open SimpleInjector
open System.Diagnostics
open System.Net
open System.Net.Http
open Xamarin.Forms

[<AbstractClass; Sealed>]
type RetroGameGauntletCore() = 
    static let container = new Container()
    static let httpClient = new HttpClient()

    static do
        container.Register<HttpClient>(fun _ -> httpClient);
    
    static member Container = container

    static member HttpClient = container.GetInstance<HttpClient>()