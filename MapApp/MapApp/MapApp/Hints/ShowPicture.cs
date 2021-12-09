using EncounterMe;
using MapApp.Pages;
using System;
using System.Collections.Generic;
using System.Text;

namespace MapApp.Hints
{
    internal class ShowPicture : IHint
    {
        public ShowPicture(HintPage hintPage, Location loc)
        {
            hintPage.hintImage.IsVisible = true;
            //change this to location img, it is not implemented now in Location class
            //and im scared to change something in case the database wont work after it
            hintPage.hintImage.Source = "pilis.jpg";
        }
        public void hideHint(HintPage hintPage)
        {
            //throw new NotImplementedException();
            hintPage.hintImage.IsVisible = false;

        }

        public void show(HintPage hintPage)
        {
            hintPage.hintImage.IsVisible = true;

            hintPage.hintImage.Source = "pilis.jpg";
            //throw new NotImplementedException();
        }
    }
}
