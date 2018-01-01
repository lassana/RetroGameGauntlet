namespace RetroGameGauntlet.Core.ValueConverters

open System
open System.Globalization
open Xamarin.Forms

/// The true-false boolean converter.
type BooleanNegationConverter() =

    interface IValueConverter with
        member this.Convert (value: obj, targetType: Type, parameter: obj, cuture: CultureInfo) = 
            not (value :?> bool) :> obj

        member this.ConvertBack (value: obj, targetType: Type, parameter: obj, cuture: CultureInfo) = 
            not (value :?> bool) :> obj