﻿using System;
using Model;
using Xamarin.Forms;
using Foundation;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using DI;
using ViewModel;
using System.Globalization;

[assembly: Dependency (typeof (PlatformLoader))]
namespace DI
{
    public class PlatformLoader : IPlatformLoader
    {
        private List<KeyValuePair<string, string>> platforms;

        public PlatformLoader()
        {
        }

        #region IPlatformLoader implementation

        public string GetRandomGame(string platform)
        {
            string filePath = NSBundle.MainBundle.PathForResource("platforms/" + platform, "");
            var fileReader = new StreamReader(filePath);
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
                string filePath = NSBundle.MainBundle.PathForResource("platforms/" + platform.Key, "");
                var fileReader = new StreamReader(filePath);
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
