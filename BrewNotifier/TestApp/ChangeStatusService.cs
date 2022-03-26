using BrewNotifications.Entities.Company;
using BrewNotifications.Entities.CompanyCompetitor;
using BrewNotifications.Entities.Core;
using BrewNotifier.Entities.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp
{
    public class ChangeStatusService
    {
        private readonly IEntitiesStateChangeHandler _entityStateChaneHandler;

        public ChangeStatusService(IEntitiesStateChangeHandler entityStateChaneHandler)
        {
            _entityStateChaneHandler = entityStateChaneHandler;
        }

        public async Task Run()
        {
            WriteToConsole("creating a new company by the name 'original'");
            var company = CreateNewCompany("original");
            HandleChange(company, null, company.EntityType);
            await Task.Delay(2000);

            WriteToConsole("Mark company for deletion");
            var makredCompany = ChangeCompanyDeletionStatus(company);
            HandleChange(makredCompany, company, company.EntityType);
            await Task.Delay(2000);

            WriteToConsole("Creating a competitor company by the name by the name 'competitor'");
            var competitor = CreateNewCompany("competitor");
            HandleChange(competitor, null, competitor.EntityType);
            await Task.Delay(2000);

            WriteToConsole("Mark 'competitor' a competition to 'original'");
            var companyCompetition = CreateNewCompanyCompetition(makredCompany, competitor);
            HandleChange(companyCompetition, null, companyCompetition.EntityType);
            await Task.Delay(2000);


            WriteToConsole("Delete 'original' company");
            HandleChange(null, makredCompany, makredCompany.EntityType);

            WriteToConsole("Done! No more Changes will be made...");


        }

        private CompanyEntity CreateNewCompany(string name)
        {
            var company = new CompanyEntity();
            company.Name = name;
            return company;
        }

        private CompanyCompetitorEntity CreateNewCompanyCompetition(CompanyEntity company, CompanyEntity competitor)
        {
            return new CompanyCompetitorEntity
            {
                Company = company,
                Competitor = competitor
            };
        }

        private CompanyEntity ChangeCompanyDeletionStatus(CompanyEntity companyEntity)
        {
            var markedCompany = new CompanyEntity(companyEntity);
            markedCompany.IsDeleted = !companyEntity.IsDeleted;

            return markedCompany;
        }

        private void HandleChange(IEntity entity, IEntity originalEntity, string entityType)
        {
            _entityStateChaneHandler.OnEntityStateChanged(entity, originalEntity, entityType);
        }

        private void WriteToConsole(string str)
        {
            Console.WriteLine($"ChangeStatusService says: {str} \n");
        }



    }
}
