using System;
using RetroGameGauntlet.Forms.Models;

namespace RetroGameGauntlet.Forms.ViewModels
{
    public class PlatformItemViewModel
    {
        private readonly PlatformModel _platformModel;

        public PlatformModel PlatformModel { get { return _platformModel; } }

        public string Title { get { return _platformModel.Title; } }

        public string Description { get { return _platformModel.Comment; } }

        public PlatformItemViewModel(PlatformModel platformModel)
        {
            if (platformModel == null)
            {
                throw new ArgumentNullException(nameof(platformModel));
            }
            _platformModel = platformModel;
        }
    }
}
