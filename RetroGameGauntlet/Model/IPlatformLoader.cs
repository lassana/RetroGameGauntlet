using System.Collections.Generic;

namespace Model
{
    public interface IPlatformLoader
    {
        string GetRandomGame(string platform);

        List<KeyValuePair<string, string>> FindGames(string query);
    }
}

