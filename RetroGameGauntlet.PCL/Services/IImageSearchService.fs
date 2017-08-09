namespace RetroGameGauntlet.PCL.Services

open System.Threading.Tasks

type IImageSearchService = 

    abstract member GetImage: Task<string>

    abstract member GetImageForGame: gameName:string -> platformName:string -> Task<string>