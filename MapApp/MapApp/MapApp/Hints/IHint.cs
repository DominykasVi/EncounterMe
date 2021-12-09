using MapApp.Pages;
using System;
using System.Collections.Generic;
using System.Text;

namespace MapApp.Hints
{
    internal interface IHint
    {
        public void show(HintPage hintPage);

        public void hideHint(HintPage hintPage);
    }
}
