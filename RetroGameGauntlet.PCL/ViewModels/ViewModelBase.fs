namespace RetroGameGauntlet.PCL.ViewModels

open System
open System.ComponentModel
open Microsoft.FSharp.Quotations.Patterns

/// The base view model.
[<AbstractClass>]
type ViewModelBase() =
    let mutable isBusy: bool = false
    let mutable errorText: string = System.String.Empty
    let propertyChanged = Event<_, _>()

    // The implementation of INotifyPropertyChanged is required
    // to use Xamarin.Forms bindings.
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

    /// A general-purpose member discribes if the view model is currently doing a long task.
    member this.IsBusy
        with get() = isBusy
        and set parameter =
            isBusy <- parameter
            this.NotifyPropertyChanged <@ this.IsBusy @>

    /// A general-purpose member describes the last error.
    member this.ErrorText
        with get() = errorText
        and set parameter =
            errorText <- parameter
            this.NotifyPropertyChanged <@ this.ErrorText @>

