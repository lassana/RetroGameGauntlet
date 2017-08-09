namespace RetroGameGauntlet.PCL.Services

open System.Collections.Generic
open System.Threading.Tasks

type IPlatformLoaderService = 

    abstract member GetRandomGame: string -> Task<string>

    abstract member FindGamesFor: string -> Task<IEnumerable<KeyValuePair<string, string>>>