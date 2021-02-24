using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using WebAPI_SQLserver_React;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using WebAPI_SQLserver_React.Models;

namespace WebAPI_SQLserver_React.Controllers
{
    public class DepartmentController : ApiController
    {
        public HttpResponseMessage Get()
        {
            DataTable table = new DataTable();
            string query = @"
                            select DepartmentID, DepartmentName from dbo.Department
                            ";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeAppDB"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }



        public string Post(Department dep)
        {
            try
            {
                DataTable table = new DataTable();
                string query = @"
                            insert into dbo.Department values ('" + dep.DepartmentName + @"')
                            ";

                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeAppDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }

                return "Added successfully";
            
                
            }
            catch (Exception)
            {
                return "Failed to Add";
            }
        }

        public string Put(Department dep)
        {
            try
            {
                string query = @"
                                update dbo.Department set DepartmentName = 
                                '" + dep.DepartmentName + @"'
                                where DepartmentID=" + dep.DepartmentID + @"
                                ";

                DataTable table = new DataTable();
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeAppDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }

                return "Updated Succesfully !!";

            }
            catch (Exception)
            {
                return "Failed to Update !!";
            }
        }


        public string Delete(int id)
        {
            try
            {
                string query = @"
                                delete from dbo.Department 
                                where DepartmentID=" + id + @"
                                ";

                DataTable table = new DataTable();
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeAppDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }

                return "Deleted Succesfully !!";

            }
            catch (Exception)
            {
                return "Failed to Delete !!";
            }
        }

    }
}
