using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI_SQLserver_React.Models;

namespace WebAPI_SQLserver_React.Controllers
{
    public class EmployeeController : ApiController
    {
        public HttpResponseMessage Get()
        {
            DataTable table = new DataTable();
            string query = @"
                            select EmployeeID, EmployeeName, MailID, convert(varchar(10),DOJ, 120) as DOJ, DepartmentName  from dbo.Employees
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


        public string Post(Employee emp)
        {
            try
            {
                DataTable table = new DataTable();
                string query = @"
                                insert into dbo.Employees
                                (EmployeeName, DepartmentName, MailID, DOJ) values
                                (
                                '" + emp.EmployeeName + @"'
                                ,'" + emp.DepartmentName + @"'
                                ,'" + emp.MailID + @"'
                                ,'" + emp.DOJ + @"'
                                )
                                ";
                //DataTable table = new DataTable();
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeAppDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }

                return "Added Succesfully !!";

            }
            catch (Exception)
            {
                return "Failed to Add !!";
            }
        }

        public string Put(Employee emp)
        {
            try
            {
                string query = @"
                                update dbo.Employees set 
                                EmployeeName = '" + emp.EmployeeName + @"'
                                ,DepartmentName = '" + emp.DepartmentName + @"'
                                ,MailID = '" + emp.MailID + @"'
                                ,DOJ = '" + emp.DOJ + @"'
                                where EmployeeID=" + emp.EmployeeID + @"
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
                                delete from dbo.Employees 
                                where EmployeeID=" + id + @"
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
