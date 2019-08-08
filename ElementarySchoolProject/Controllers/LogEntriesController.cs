using ElementarySchoolProject.Models;
using ElementarySchoolProject.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace ElementarySchoolProject.Controllers
{
    [RoutePrefix("api/logs")]
    public class LogEntriesController : ApiController
    {
        ILogEntriesService service;
        public LogEntriesController(ILogEntriesService service)
        {
            this.service = service;
        }

        [HttpGet]
        [Route("")]
        public IEnumerable<LogEntry> GetAll()
        {
            return service.GetAll();
        }

        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult GetById(int id)
        {
            var retVal = service.GetById(id);

            return Ok(retVal);
        }

        [Route("dl")]
        [HttpGet]
        public HttpResponseMessage ExportCSV()
        {
            string constr = ConfigurationManager.ConnectionStrings["Elementary"].ConnectionString;

            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM LogEntries"))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);

                            //Build the CSV file data as a Comma separated string.
                            string csv = string.Empty;

                            foreach (DataColumn column in dt.Columns)
                            {
                                //Add the Header row for CSV file.
                                csv += column.ColumnName + ',';
                            }

                            //Add new line.
                            csv += "\r\n";

                            foreach (DataRow row in dt.Rows)
                            {
                                foreach (DataColumn column in dt.Columns)
                                {
                                    //Add the Data rows.
                                    csv += row[column.ColumnName].ToString().Replace(",", ";") + ',';
                                }

                                //Add new line.
                                csv += "\r\n";
                            }

                            //Download the CSV file.
                            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                            response.Content = new StringContent(csv);

                            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/text");
                            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment") { FileName = "log.csv" };

                            return response;                            
                        }
                    }
                }
            }
        }
    }
}
