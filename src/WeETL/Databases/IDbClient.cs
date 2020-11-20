using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WeETL.Databases
{
    public interface IDbClient
    {
        Task SaveSingleAsync<T>(T model);

        Task SaveManyAsync<T>(params T[] models);

        Task SaveBulkAsync<T>(params T[] models);

        Task DeleteAsync<T>(T model);

        Task UpdateAsync<T>(T model);

        Task SearchAsync<T>();
    }
}
