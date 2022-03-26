using BrewNotifications.Entities.Core;
using System;

namespace BrewNotifications.Entities.Company
{

    public class CompanyEntity : CrawlableEntity, IEntity
    {
        public int MinEmployees { get; set; }
        public int MaxEmployees { get; set; }
        public string EntityType => "Company";
    }
}
