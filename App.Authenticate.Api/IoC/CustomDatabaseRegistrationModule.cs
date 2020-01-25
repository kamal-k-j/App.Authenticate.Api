using App.Data.Conventions;
using FluentNHibernate.Automapping;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace App.Authenticate.Api.IoC
{
    public class CustomDatabaseRegistrationModule : DatabaseRegistrationModule
    {
        public CustomDatabaseRegistrationModule(
            IConfiguration configuration,
            Assembly automapAssemblies) : base(configuration, automapAssemblies)
        {
        }

        protected override AutoPersistenceModel AutomappingConventions()
        {
            return base.AutomappingConventions().UseOverridesFromAssembly(GetType().Assembly);
        }
    }
}