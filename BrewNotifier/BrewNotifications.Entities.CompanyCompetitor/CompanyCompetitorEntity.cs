using BrewNotifications.Entities.Company;
using BrewNotifications.Entities.Core;
using System;

namespace BrewNotifications.Entities.CompanyCompetitor
{
    public class CompanyCompetitorEntity: IEntity, IDeleteable
    {
        public const string EntityName = "CompanyCompetitor";
        public CompanyEntity Company { get; set; }
        public CompanyEntity Competitor { get; set; }
        public bool IsDeleted { get; set; }

        public string EntityType => EntityName;
    }
}
