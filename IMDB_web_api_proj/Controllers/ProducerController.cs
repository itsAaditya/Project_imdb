using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using IMDB_web_api_proj.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace IMDB_web_api_proj.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProducerController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public ProducerController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                            select * from producer";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("IMDBApp");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult(table);
        }

        //Add new Producer
        [HttpPost]
        public JsonResult Post(Producer prod)
        {
            string query = @"
                            insert into producer (producer_id, producer_name, bio, company, gender)
                                values (@producer_id, @producer_name, @bio, @company, @gender)";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("IMDBApp");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@producer_id", prod.producer_id);
                    myCommand.Parameters.AddWithValue("@producer_name", prod.producer_name);
                    myCommand.Parameters.AddWithValue("@bio", prod.bio);
                    myCommand.Parameters.AddWithValue("@company", prod.company);
                    myCommand.Parameters.AddWithValue("@gender", prod.gender);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult("Added Successfully");
        }


        //Update Producer
        [HttpPut]
        public JsonResult Put(Producer prod)
        {
            string query = @"
                            Update producer set 
                            producer_name=@producer_name, bio=@bio, company=@company, gender=@gender
                            where producer_id=@producer_id
                                ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("IMDBApp");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@producer_id", prod.producer_id);
                    myCommand.Parameters.AddWithValue("@producer_name", prod.producer_name);
                    myCommand.Parameters.AddWithValue("@bio", prod.bio);
                    myCommand.Parameters.AddWithValue("@company", prod.company);
                    myCommand.Parameters.AddWithValue("@gender", prod.gender);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult("Updated Successfully");
        }


        //Delete a Producer
        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"
                            delete from producer
                            where producer_id=@producer_id
                                ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("IMDBApp");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@producer_id", id);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult("Deleted Successfully");
        }
    }
}
