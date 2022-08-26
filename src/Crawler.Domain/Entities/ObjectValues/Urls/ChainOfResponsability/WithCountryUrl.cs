﻿using Crawlers.Domain.Entities.Interfaces.ObjectValues.Urls;

namespace Crawlers.Domain.Entities.ObjectValues.Urls.ChainOfResponsability
{
    public class WithCountryUrl : AbstractUrlHandler, IUrlHandler
    {
        protected override string Pattern { get; } = @"^(?:(?<protocol>[htps]{4,5})\:\/\/)?(?<subdomain>[^\.]*)?\.?(?<domain_name>[^\.]*)\.(?<toplevel>[^\.]{2,3})\.(?<country>[^\.]{2})(?<directory>\/{1}.+)?$";
    }
}
