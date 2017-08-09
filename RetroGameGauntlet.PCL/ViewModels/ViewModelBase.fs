namespace RetroGameGauntlet.PCL.ViewModels

open System
open System.ComponentModel
open Microsoft.FSharp.Quotations.Patterns

[<AbstractClass>]
type ViewModelBase() =
    let mutable isBusy: bool = false
    let mutable errorText: string = System.String.Empty
    let propertyChanged = Event<_, _>()

    interface INotifyPropertyChanged with
        [<CLIEvent>]
        member this.PropertyChanged = propertyChanged.Publish 

    member this.NotifyPropertyChanged propertyName = 
        propertyChanged.Trigger(this, PropertyChangedEventArgs propertyName)

    member this.NotifyPropertyChanged propertySelector = 
        match propertySelector with 
        | PropertyGet(Some (Value (instance, _)), property, _) when Object.ReferenceEquals(instance, this) -> 
            this.NotifyPropertyChanged property.Name
        | _ -> invalidOp "Expecting property getter expression only (for example `this.SomeProperty`)."

    member this.IsBusy
        with get() = isBusy
        and set parameter =
            isBusy <- parameter
            this.NotifyPropertyChanged <@ this.IsBusy @>

    member this.ErrorText
        with get() = errorText
        and set parameter =
            errorText <- parameter
            this.NotifyPropertyChanged <@ this.ErrorText @>

