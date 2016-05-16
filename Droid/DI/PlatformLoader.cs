﻿using System;
using Xamarin.Forms;
using RetroGameGauntlet.Droid.DI;
using RetroGameGauntlet.Model;
using System.Collections.Generic;
using RetroGameGauntlet.ViewModel;
using System.Linq;
using System.IO;
using System.Globalization;

[assembly: Dependency (typeof (PlatformLoader))]
namespace RetroGameGauntlet.Droid.DI
{
    public class PlatformLoader : IPlatformLoader
    {
        private List<KeyValuePair<string, string>> platforms;

        #region IPlatformLoader implementation

        public string GetRandomGame(string platform)
        {
            var fileReader = new StreamReader(Forms.Context.Assets.Open("platforms/" + platform));
            string nextLine;
            List<string> games = new List<string>();
            while((nextLine=fileReader.ReadLine()) != null)
            {
                games.Add(nextLine);
            }
            fileReader.Close();

            Random rnd = new Random();
            return games[rnd.Next(0,games.Count)];
        }


        public List<KeyValuePair<string, string>> FindGames(string query)
        {
            if (platforms == null)
            {
                platforms = PlatformViewModel.List.Select((arg) => new KeyValuePair<string, string>(arg.FileName, arg.Title)).ToList();
            }
            List<KeyValuePair<string, string>> games = new List<KeyValuePair<string, string>>();
            foreach (var platform in platforms)
            {
                var fileReader = new StreamReader(Forms.Context.Assets.Open("platforms/" + platform.Key));
                string nextLine;
                while((nextLine=fileReader.ReadLine()) != null)
                {
                    if (CultureInfo.CurrentCulture.CompareInfo.IndexOf(nextLine, query, CompareOptions.IgnoreCase) >= 0)
                    {
                        games.Add(new KeyValuePair<string,string>(nextLine, platform.Value));
                    }
                }
                fileReader.Close();
            }
            return games;
        }
        #endregion
    }
}

