using System;
using System.Linq;

namespace CosolemWS
{
    static class edmCosolemFunctions
    {
        public static DateTime getFechaHora()
        {
            using (dbCosolemEntities _dbCosolemEntities = new dbCosolemEntities())
                return _dbCosolemEntities.ExecuteStoreQuery<DateTime>("SELECT GETDATE()").First();
        }
    }
}