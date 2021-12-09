using MapApp.Pages;
using System;
using System.Collections.Generic;
using System.Text;

namespace MapApp.Hints
{
    internal class HintCompass : IHint
    {
        public HintCompass(HintPage hintPage)
        {
            hintPage.hintImage.IsVisible = true;
            hintPage.hintImage.Source = "temp.png";
        }
        public void hideHint(HintPage hintPage)
        {
            //throw new NotImplementedException();
            hintPage.hintImage.IsVisible = false;

        }

        public void show(HintPage hintPage)
        {
            hintPage.hintImage.IsVisible = true;
            hintPage.hintImage.Source = "temp.png";

            //throw new NotImplementedException();
        }
    }
}
