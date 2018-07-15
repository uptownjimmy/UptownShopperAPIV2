using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Dapper;
using System.Data;
using Npgsql;
using UptownShopperApiV2.Models;

namespace UptownShopperApiV2.Repository
{
    public class StoreRepository : IRepository<Store>
    {
        private readonly string _connectionString;
        public StoreRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetValue<string>("DBInfo:ConnectionString");
        }

        private IDbConnection Connection => new NpgsqlConnection(_connectionString);

        public IEnumerable<Store> GetAll()
        {
            using (var dbConnection = Connection)
            {
                if (dbConnection.State == ConnectionState.Closed)
                {
                    dbConnection.Open();
                }

                var p = new DynamicParameters();
                p.Add("@_store_id"); //default value is null

                var stores = dbConnection.Query<Store>("shopper.fn_get_store", p, commandType: CommandType.StoredProcedure);

                return stores;
            }
        }

        public Store Find(long id)
        {
            using (var dbConnection = Connection)
            {
                if (dbConnection.State == ConnectionState.Closed)
                {
                    dbConnection.Open();
                }

                var p = new DynamicParameters();
                p.Add("@_store_id", id);

                var store = dbConnection.Query<Store>("shopper.fn_get_store", p, commandType: CommandType.StoredProcedure).SingleOrDefault();

                return store;
            }
        }

        public long Add(Store store)
        {
            long newStoreId;
            using (var dbConnection = Connection)
            {
                if (dbConnection.State == ConnectionState.Closed)
                {
                    dbConnection.Open();
                }

                var p = new DynamicParameters();
                p.Add("@_name", store.Name);
                p.Add("@_location", store.Location);
                p.Add("@_store_type", store.Store_Type);
                p.Add("@_created_by", store.Created_By);

                newStoreId = dbConnection.ExecuteScalar<long>("shopper.fn_add_store", p, commandType: CommandType.StoredProcedure);
            }

            return newStoreId;
        }

        public void Update(Store store)
        {
            using (var dbConnection = Connection)
            {
                if (dbConnection.State == ConnectionState.Closed)
                {
                    dbConnection.Open();
                }

                var p = new DynamicParameters();
                p.Add("@_store_id", store.Id);
                p.Add("@_name", store.Name);
                p.Add("@_location", store.Location);
                p.Add("@_store_type", store.Store_Type);
                p.Add("@_modified_by", store.Modified_By);

                dbConnection.Query<Store>("shopper.fn_update_store", p, commandType: CommandType.StoredProcedure);
            }
        }

        public void Remove(long id)
        {
            using (var dbConnection = Connection)
            {
                if (dbConnection.State == ConnectionState.Closed)
                {
                    dbConnection.Open();
                }

                var p = new DynamicParameters();
                p.Add("@_store_id", id);

                dbConnection.ExecuteScalar<long>("shopper.fn_delete_store", p, commandType: CommandType.StoredProcedure);
            }
        }
    }
}