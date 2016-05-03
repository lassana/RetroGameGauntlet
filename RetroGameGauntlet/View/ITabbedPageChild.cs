using System;

using Xamarin.Forms;

namespace View
{
    public interface ITabbedPageChild
    {
        void Opened();
        void Closed();
    }
}


