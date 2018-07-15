using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Dapper;
using System.Data;
using Npgsql;
using UptownShopperApiV2.Models;

namespace UptownShopperApiV2.Repository
{
    public class ContactRepository : IRepository<Contact>
    {
        private readonly string _connectionString;
        public ContactRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetValue<string>("DBInfo:ConnectionString");
        }

        private IDbConnection Connection => new NpgsqlConnection(_connectionString);

        public IEnumerable<Contact> GetAll()
        {
            using (var dbConnection = Connection)
            {
                if (dbConnection.State == ConnectionState.Closed)
                {
                    dbConnection.Open();
                }

                var p = new DynamicParameters();
                p.Add("@_contact_id"); //default value is null

                var contacts = dbConnection.Query<Contact>("system.fn_get_contact", p, commandType: CommandType.StoredProcedure);

                return contacts;
            }
        }

        public Contact Find(long id)
        {
            using (var dbConnection = Connection)
            {
                if (dbConnection.State == ConnectionState.Closed)
                {
                    dbConnection.Open();
                }

                var p = new DynamicParameters();
                p.Add("@_contact_id", id);

                var contact = dbConnection.Query<Contact>("system.fn_get_contact", p, commandType: CommandType.StoredProcedure).SingleOrDefault();

                return contact;
            }
        }

        public long Add(Contact contact)
        {
            long newContactId;
            using (var dbConnection = Connection)
            {
                if (dbConnection.State == ConnectionState.Closed)
                {
                    dbConnection.Open();
                }

                var p = new DynamicParameters();
                p.Add("@_first_name", contact.First_Name);
                p.Add("@_middle_name", contact.Middle_Name);
                p.Add("@_last_name", contact.Last_Name);
                p.Add("@_display_name", contact.Display_Name);
                p.Add("@_user_id", contact.User_Id);
                p.Add("@_email", contact.Email);
                p.Add("@_mobile_phone", contact.Mobile_Phone);
                p.Add("@_home_phone", contact.Home_Phone);
                p.Add("@_work_phone", contact.Work_Phone);
                p.Add("@_fax", contact.Fax);
                p.Add("@_department", contact.Department);
                p.Add("@_title", contact.Title);
                p.Add("@_notes", contact.Notes);
                p.Add("@_created_by", contact.Created_By);

                newContactId = dbConnection.ExecuteScalar<long>("system.fn_add_contact", p, commandType: CommandType.StoredProcedure);
            }

            return newContactId;
        }

        public void Update(Contact contact)
        {
            using (var dbConnection = Connection)
            {
                if (dbConnection.State == ConnectionState.Closed)
                {
                    dbConnection.Open();
                }

                var p = new DynamicParameters();
                p.Add("@_contact_id", contact.Id);
                p.Add("@_first_name", contact.First_Name);
                p.Add("@_middle_name", contact.Middle_Name);
                p.Add("@_last_name", contact.Last_Name);
                p.Add("@_display_name", contact.Display_Name);
                p.Add("@_user_id", contact.User_Id);
                p.Add("@_email", contact.Email);
                p.Add("@_mobile_phone", contact.Mobile_Phone);
                p.Add("@_home_phone", contact.Home_Phone);
                p.Add("@_work_phone", contact.Work_Phone);
                p.Add("@_fax", contact.Fax);
                p.Add("@_department", contact.Department);
                p.Add("@_title", contact.Title);
                p.Add("@_notes", contact.Notes);
                p.Add("@_modified_by", contact.Modified_By);

                dbConnection.Query<Contact>("system.fn_update_contact", p, commandType: CommandType.StoredProcedure);
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
                p.Add("@_contact_id", id);

                dbConnection.ExecuteScalar<long>("system.fn_delete_contact", p, commandType: CommandType.StoredProcedure);
            }
        }
    }
}