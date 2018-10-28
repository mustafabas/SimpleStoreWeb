using System;
using MvcProjectArch.Core;
using MvcProjectArch.Core.Data;

namespace MvcProjectArch.Data
{
    public partial class EfDataProviderManager : BaseDataProviderManager
    {
        public EfDataProviderManager():base()
        {
        }

        public override IDataProvider LoadDataProvider()
        {

            var providerName = "sqlserver"; 
            if (String.IsNullOrWhiteSpace(providerName))
                throw new ProjectNameException("Data Settings doesn't contain a providerName");

            switch (providerName.ToLowerInvariant())
            {
                case "sqlserver":
                    return new SqlServerDataProvider();
                //case "sqlce":
                //    return new SqlCeDataProvider();
                default:
                    throw new ProjectNameException(string.Format("Not supported dataprovider name: {0}", providerName));
            }
        }

    }
}
