using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Dapper;
using System.Data;
using Npgsql;
using UptownShopperApiV2.Models;

namespace UptownShopperApiV2.Repository
{
    public class ItemRepository //: IRepository<Item>
    {
        private readonly string _connectionString;
        public ItemRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetValue<string>("DBInfo:ConnectionString");
        }

        private IDbConnection Connection => new NpgsqlConnection(_connectionString);

        public IEnumerable<Item> GetAll()
        {
            using (var dbConnection = Connection)
            {
                if (dbConnection.State == ConnectionState.Closed)
                {
                    dbConnection.Open();
                }

                var p = new DynamicParameters();
                p.Add("@_item_id"); //default value is null

                var items = dbConnection.Query<Item>("shopper.fn_get_item", p, commandType: CommandType.StoredProcedure);

                return items;
            }
        }
        
        public IEnumerable<Item> GetAllActive(bool active)
        {
            using (var dbConnection = Connection)
            {
                if (dbConnection.State == ConnectionState.Closed)
                {
                    dbConnection.Open();
                }

                var p = new DynamicParameters();
                p.Add("@_item_id"); //default value is null
                p.Add("@_active", active); //default value is null

                var items = dbConnection.Query<Item>("shopper.fn_get_item", p, commandType: CommandType.StoredProcedure);

                return items;
            }
        }
        
        public Item Find(long id)
        {
            using (var dbConnection = Connection)
            {
                if (dbConnection.State == ConnectionState.Closed)
                {
                    dbConnection.Open();
                }

                var p = new DynamicParameters();
                p.Add("@_item_id", id);

                var item = dbConnection.Query<Item>("shopper.fn_get_item", p, commandType: CommandType.StoredProcedure).SingleOrDefault();

                return item;
            }
        }

        public long Add(Item item)
        {
            long newItemId;
            using (var dbConnection = Connection)
            {
                if (dbConnection.State == ConnectionState.Closed)
                {
                    dbConnection.Open();
                }

                var p = new DynamicParameters();
                p.Add("@_name", item.Name);
                p.Add("@_item_type", item.Item_Type);
                p.Add("@_active", item.Active);
                p.Add("@_notes", item.Notes);
                p.Add("@_created_by", item.Created_By);

                newItemId = dbConnection.ExecuteScalar<long>("shopper.fn_add_item", p, commandType: CommandType.StoredProcedure);
            }

            return newItemId;
        }

        public void Update(Item item)
        {
            using (var dbConnection = Connection)
            {
                if (dbConnection.State == ConnectionState.Closed)
                {
                    dbConnection.Open();
                }

                var p = new DynamicParameters();
                p.Add("@_item_id", item.Id);
                p.Add("@_name", item.Name);
                p.Add("@_item_type", item.Item_Type);
                p.Add("@_active", item.Active);
                p.Add("@_notes", item.Notes);
                p.Add("@_created_by", item.Created_By);
                p.Add("@_modified_by", item.Modified_By);

                dbConnection.Query<Item>("shopper.fn_update_item", p, commandType: CommandType.StoredProcedure);
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
                p.Add("@_item_id", id);

                dbConnection.ExecuteScalar<long>("shopper.fn_delete_item", p, commandType: CommandType.StoredProcedure);
            }
        }
    }
}