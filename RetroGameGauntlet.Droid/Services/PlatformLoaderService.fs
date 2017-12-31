﻿namespace RetroGameGauntlet.Droid.Services

open System
open System.Collections.Generic
open System.Globalization
open System.IO
open System.Linq
open RetroGameGauntlet.PCL.Models
open RetroGameGauntlet.PCL.Services

type PlatformLoaderService () =
    let platforms = Platforms.All |> Seq.map(fun (arg) -> KeyValuePair<string, string>(arg.FileName, arg.Title))
    let includes(a:string, b:string) = CultureInfo.InvariantCulture.CompareInfo.IndexOf(a, b, CompareOptions.IgnoreCase) >= 0

    interface IPlatformLoaderService with
        /// Gets the random game for a platform.
        member this.GetRandomGameAsync platform =
            async {
                let filePath = Xamarin.Forms.Forms.Context.Assets.Open("platforms/" + platform)
                let fileReader = new StreamReader(filePath)
                let games = List<string>()
                let mutable nextLine: string = fileReader.ReadLine()
                while nextLine <> null do
                    games.Add nextLine
                    nextLine <- fileReader.ReadLine()
                fileReader.Close()
                let rnd = Random()
                return games.Item(rnd.Next(0, games.Count))
            }

        /// Gets the full list of games for a platform.
        member this.FindGamesForAsync query =
            async {
                let games = List<KeyValuePair<string, string>>()
                platforms 
                    |> Seq.iter(
                        fun platform -> 
                            let filePath = Xamarin.Forms.Forms.Context.Assets.Open("platforms/" + platform.Key)
                            let fileReader = new StreamReader(filePath)
                            let mutable nextLine: string = fileReader.ReadLine()
                            while nextLine <> null do
                                if includes(nextLine, query) then
                                    games.Add(KeyValuePair<string, string>(nextLine, platform.Value))
                                nextLine <- fileReader.ReadLine()
                            fileReader.Close()
                    )
                return games.AsEnumerable()
            }