using Xamarin.Forms;
using System.Collections.Generic;

namespace RetroGameGauntlet.ViewModel
{
    public class PlatformViewModel
    {
        public string Title { get; private set; }

        public string Comment { get; private set; }

        public ImageSource Image { get; private set; }

        public string FileName { get; private set; }

        public static List<PlatformViewModel> List
        {
            get
            {
                return new List<PlatformViewModel>
                {
                    new PlatformViewModel { Title = "NES",                  Comment = "852 games",  Image = "", FileName = "nes" },
                    new PlatformViewModel { Title = "SNES",                 Comment = "693 games",  Image = "", FileName = "snes" },
                    new PlatformViewModel { Title = "GB/GBC",               Comment = "672 games",  Image = "", FileName = "gbc" },
                    new PlatformViewModel { Title = "Gamegear",             Comment = "255 games",  Image = "", FileName = "gamegear" },
                    new PlatformViewModel { Title = "Megadrive/Genesis",    Comment = "535 games",  Image = "", FileName = "megadrive" },
                    new PlatformViewModel { Title = "Turbografx16",         Comment = "151 games",  Image = "", FileName = "tg16" },
                    new PlatformViewModel { Title = "DOS",                  Comment = "3655 games", Image = "", FileName = "dos" },
                    new PlatformViewModel { Title = "NeoGeo",               Comment = "148 games",  Image = "", FileName = "neogeo" },
                    new PlatformViewModel { Title = "GBA",                  Comment = "1018 games", Image = "", FileName = "gba" },
                    new PlatformViewModel { Title = "Master System",        Comment = "298 games",  Image = "", FileName = "mastersystem" },
                    new PlatformViewModel { Title = "Commodore 64",         Comment = "1924 games", Image = "", FileName = "c64" },
                    new PlatformViewModel { Title = "Commodore Amiga",      Comment = "2436 games", Image = "", FileName = "amiga" },
                    new PlatformViewModel { Title = "Sega Saturn",          Comment = "580 games",  Image = "", FileName = "saturn" },
                    new PlatformViewModel { Title = "Playstation 1",        Comment = "1298 games", Image = "", FileName = "ps1" },
                    new PlatformViewModel { Title = "Nintendo 64",          Comment = "320 games",  Image = "", FileName = "n64" },
                    new PlatformViewModel { Title = "Sega Dreamcast",       Comment = "629 games",  Image = "", FileName = "dreamcast" },
                    new PlatformViewModel { Title = "MAME",                 Comment = "8646 games", Image = "", FileName = "mame" },
                };
            }
        }
    }
}

