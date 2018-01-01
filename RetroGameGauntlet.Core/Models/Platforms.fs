namespace RetroGameGauntlet.Core.Models

open RetroGameGauntlet.Core.Models

/// The full list of game platforms.
[<AbstractClass; Sealed>]
type Platforms = 
    static member All = 
        seq [
             PlatformModel("NES",               "852 games",    "nes" );
             PlatformModel("SNES",              "693 games",    "snes" );
             PlatformModel("GB/GBC",            "672 games",    "gbc" );
             PlatformModel("Gamegear",          "255 games",    "gamegear" );
             PlatformModel("Megadrive/Genesis", "535 games",    "megadrive" );
             PlatformModel("Turbografx16",      "151 games",    "tg16" );
             PlatformModel("DOS",               "3655 games",   "dos" );
             PlatformModel("NeoGeo",            "148 games",    "neogeo" );
             PlatformModel("GBA",               "1018 games",   "gba" );
             PlatformModel("Master System",     "298 games",    "mastersystem" );
             PlatformModel("Commodore 64",      "1924 games",   "c64" );
             PlatformModel("Commodore Amiga",   "2436 games",   "amiga" );
             PlatformModel("Sega Saturn",       "580 games",    "saturn" );
             PlatformModel("Playstation 1",     "1298 games",   "ps1" );
             PlatformModel("Nintendo 64",       "320 games",    "n64" );
             PlatformModel("Sega Dreamcast",    "629 games",    "dreamcast" );
             PlatformModel("MAME",              "8646 games",   "mame" )
        ]