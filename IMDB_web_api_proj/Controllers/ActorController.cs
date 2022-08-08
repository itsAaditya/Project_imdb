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
    public class ActorController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public ActorController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        //Get all Movie
        [HttpGet]
        public JsonResult Get(int id)
        {
            string query = @"
                            select * from actor";

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


        //Add Actor
        [HttpPost]
        public JsonResult Post(Actor actor)
        {
            string query = @"
                            insert into actor (actor_id, actor_name, actor_bio, gender)
                                values (@actor_id, @actor_name, @actor_bio, @gender)";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("IMDBApp");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@actor_id", actor.actor_id);
                    myCommand.Parameters.AddWithValue("@actor_name", actor.actor_name);
                    myCommand.Parameters.AddWithValue("@actor_bio", actor.actor_bio);
                    myCommand.Parameters.AddWithValue("@gender", actor.gender);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult("Added Successfully");
        }

        //Update Actor
        [HttpPut]
        public JsonResult Put(Actor actor)
        {
            string query = @"
                            Update actor set 
                            actor_name=@actor_name, actor_bio=@actor_bio, gender=@gender
                            where actor_id=@actor_id
                                ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("IMDBApp");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@actor_id", actor.actor_id);
                    myCommand.Parameters.AddWithValue("@actor_name", actor.actor_name);
                    myCommand.Parameters.AddWithValue("@actor_bio", actor.actor_bio);
                    myCommand.Parameters.AddWithValue("@gender", actor.gender);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult("Updated Successfully");
        }

        //Delete a Actor
        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"
                            delete from actor
                            where actor_id=@actor_id
                                ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("IMDBApp");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@actor_id", id);

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
