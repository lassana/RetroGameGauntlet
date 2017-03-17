using System;
using System.Threading.Tasks;

namespace RetroGameGauntlet.Forms.Services
{
    public interface IImageSearchService
    {
        Task<string> GetImage();

        Task<string> GetImageForGame(string gameName, string platformName);
    }
}
