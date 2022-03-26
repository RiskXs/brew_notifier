using BrewNotifications.Entities.Core;
using System;

namespace BrewNotifications.Entities.Company
{

    public class CompanyEntity : CrawlableEntity, IEntity
    {
        public CompanyEntity() {}
        public CompanyEntity(CompanyEntity companyEntity)
        {
            this.CrawlingStatus = companyEntity.CrawlingStatus;
            this.IsBlacklisted = companyEntity.IsBlacklisted;
            this.IsDeleted = companyEntity.IsDeleted;
            this.LastCrawled = companyEntity.LastCrawled;
            this.Link = companyEntity.Link;
            this.Name = companyEntity.Name;
            this.MaxEmployees = companyEntity.MaxEmployees;
            this.MinEmployees = companyEntity.MinEmployees;
        }
        public const string EntityName = "Company";
        public int MinEmployees { get; set; }
        public int MaxEmployees { get; set; }
        public string EntityType => EntityName;
    }
}
