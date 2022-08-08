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
    public class MovieController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public MovieController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        //Get details of a movie
        [HttpGet("{id}")]
        public JsonResult Get(int id)
        {
            string query = @"
                            SELECT mov.movie_name, mov.plot, mov.release_date, prod.producer_name, act.actor_name
                            FROM movie mov
                            INNER JOIN actor act ON mov.actor_id = act.actor_id
                            INNER JOIN producer prod ON mov.producer_id   = prod.producer_id
                            Where mov.movie_id = @movie_id";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("IMDBApp");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@movie_id", id);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult(table);
        }

        //Get all the movie
        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                            select * from movie";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("IMDBApp");
            MySqlDataReader myReader;
            using(MySqlConnection mycon=new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand= new MySqlCommand(query, mycon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult(table);
        }


        //Add new Movie
        [HttpPost]
        public JsonResult Post(Movie movie)
        {
            string query = @"
                            insert into movie (movie_id, movie_name, plot, release_date, actor_id, producer_id, poster)
                                values (@movie_id, @movie_name, @plot, @release_date, @actor_id, @producer_id, @poster)";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("IMDBApp");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@movie_id", movie.movie_id);
                    myCommand.Parameters.AddWithValue("@movie_name", movie.movie_name);
                    myCommand.Parameters.AddWithValue("@plot", movie.plot);
                    myCommand.Parameters.AddWithValue("@release_date", movie.release_date);
                    myCommand.Parameters.AddWithValue("@actor_id", movie.actor_id);
                    myCommand.Parameters.AddWithValue("@producer_id", movie.producer_id);
                    myCommand.Parameters.AddWithValue("@poster", movie.poster);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult("Added Successfully");
        }


        [HttpPut]
        public JsonResult Put(Movie movie)
        {
            string query = @"
                            Update movie set 
                            movie_name=@movie_name, plot=@plot, release_date=@release_date, 
                            actor_id=@actor_id, producer_id=@producer_id, poster=@poster
                            where movie_id=@movie_id
                                ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("IMDBApp");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@movie_id", movie.movie_id);
                    myCommand.Parameters.AddWithValue("@movie_name", movie.movie_name);
                    myCommand.Parameters.AddWithValue("@plot", movie.plot);
                    myCommand.Parameters.AddWithValue("@release_date", movie.release_date);
                    myCommand.Parameters.AddWithValue("@actor_id", movie.actor_id);
                    myCommand.Parameters.AddWithValue("@producer_id", movie.producer_id);
                    myCommand.Parameters.AddWithValue("@poster", movie.poster);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult("Updated Successfully");
        }


        //Delete Movie
        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"
                            delete from movie
                            where movie_id=@movie_id
                                ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("IMDBApp");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@movie_id", id);

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
