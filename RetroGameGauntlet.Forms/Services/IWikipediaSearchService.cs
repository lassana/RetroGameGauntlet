using System.Collections.Generic;
using System.Threading.Tasks;
using RetroGameGauntlet.Forms.Models.Api;
using RetroGameGauntlet.Forms.ViewModels;

namespace RetroGameGauntlet.Forms.Services
{
    public interface IWikipediaSearchService
    {
        Task<List<WikipediaApiSearchResponseModel.QueryModel.SearchModel>> GetWikipediaLinks(string query);

        Task<List<WikipediaItemViewModel>> GetItemsForQuery(string query);
    }
}
