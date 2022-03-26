using BrewNotifications.Entities.Company;
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
            WriteToConsole("creating a new company");
            var company = CreateNewCompany();
            HandleChange(company, null, company.EntityType);

            await Task.Delay(2000);

            WriteToConsole("Mark company for deletion");
            var makredCompany = ChangeCompanyDeletionStatus(company);
            HandleChange(makredCompany, company, company.EntityType);

            await Task.Delay(2000);



            WriteToConsole("Delete company");
            HandleChange(null, makredCompany, makredCompany.EntityType);

            WriteToConsole("Done! No more Changes will be made...");


        }

        private CompanyEntity CreateNewCompany()
        {
            var company = new CompanyEntity();
            return company;
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
