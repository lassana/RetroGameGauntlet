using System.Collections.Generic;

namespace RetroGameGauntlet.Forms.Models
{
    public static class Platforms
    {
        public static List<PlatformModel> All
        {
            get
            {
                return new List<PlatformModel>
                {
                    new PlatformModel { Title = "NES",                  Comment = "852 games",  FileName = "nes" },
                    new PlatformModel { Title = "SNES",                 Comment = "693 games",  FileName = "snes" },
                    new PlatformModel { Title = "GB/GBC",               Comment = "672 games",  FileName = "gbc" },
                    new PlatformModel { Title = "Gamegear",             Comment = "255 games",  FileName = "gamegear" },
                    new PlatformModel { Title = "Megadrive/Genesis",    Comment = "535 games",  FileName = "megadrive" },
                    new PlatformModel { Title = "Turbografx16",         Comment = "151 games",  FileName = "tg16" },
                    new PlatformModel { Title = "DOS",                  Comment = "3655 games", FileName = "dos" },
                    new PlatformModel { Title = "NeoGeo",               Comment = "148 games",  FileName = "neogeo" },
                    new PlatformModel { Title = "GBA",                  Comment = "1018 games", FileName = "gba" },
                    new PlatformModel { Title = "Master System",        Comment = "298 games",  FileName = "mastersystem" },
                    new PlatformModel { Title = "Commodore 64",         Comment = "1924 games", FileName = "c64" },
                    new PlatformModel { Title = "Commodore Amiga",      Comment = "2436 games", FileName = "amiga" },
                    new PlatformModel { Title = "Sega Saturn",          Comment = "580 games",  FileName = "saturn" },
                    new PlatformModel { Title = "Playstation 1",        Comment = "1298 games", FileName = "ps1" },
                    new PlatformModel { Title = "Nintendo 64",          Comment = "320 games",  FileName = "n64" },
                    new PlatformModel { Title = "Sega Dreamcast",       Comment = "629 games",  FileName = "dreamcast" },
                    new PlatformModel { Title = "MAME",                 Comment = "8646 games", FileName = "mame" },
                };
            }
        }
    }
}
