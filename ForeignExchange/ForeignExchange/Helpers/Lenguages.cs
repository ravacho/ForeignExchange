using ForeignExchange.Resources;
using System;
using System.Collections.Generic;
using System.Text;
using ForeignExchange.Interfaces;

namespace ForeignExchange.Helpers
{
    public static class Lenguages
    {
        static Lenguages()
        {
            var ci = Xamarin.Forms.DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
            Resource.Culture = ci;
            Xamarin.Forms.DependencyService.Get<ILocalize>().SetLocale(ci);
        }


        public static string AmountValidation
        {
            get { return Resource.AmountValidation; }
        }

        public static string Title
        {
            get { return Resource.Title; }
        }

    }

}
