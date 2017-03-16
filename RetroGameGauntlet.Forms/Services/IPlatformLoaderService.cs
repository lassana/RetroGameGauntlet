using System.Collections.Generic;

namespace RetroGameGauntlet.Forms.Services
{
    public interface IPlatformLoaderService
    {
        string GetRandomGame(string platform);

        List<KeyValuePair<string, string>> FindGames(string query);
    }
}

