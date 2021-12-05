using MapApp.Pages;
using System;
using System.Collections.Generic;
using System.Text;

namespace MapApp.Hints
{
    internal class HintCircle : IHint
    {
        public HintCircle(HintPage hint)
        {

        }
        public void hideHint(HintPage hintPage)
        {
            //throw new NotImplementedException();
            hintPage.hintImage.IsVisible = false;

        }

        public void show(HintPage hintPage)
        {
            hintPage.hintImage.IsVisible = true;

            hintPage.hintImage.Source = "temp.jpg";
            //throw new NotImplementedException();
        }
    }
}
