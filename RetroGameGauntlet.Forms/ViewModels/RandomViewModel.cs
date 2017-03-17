using System.Collections.Generic;
using System.Linq;

namespace RetroGameGauntlet.Forms.ViewModels
{
    public class RandomViewModel : BaseViewModel
    {
        private readonly IEnumerable<PlatformItemViewModel> _platforms;
        public IEnumerable<PlatformItemViewModel> Platforms { get { return _platforms; } }

        public RandomViewModel()
        {
            _platforms = Models.Platforms.All.Select(arg => new PlatformItemViewModel(arg)).ToList();
        }
    }
}
