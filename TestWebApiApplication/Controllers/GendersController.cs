using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using TestWebApiApplication.Models;


namespace TestWebApiApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GendersController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public GendersController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        [Route("GetGenders")]
        public List<TblGender> GetGenders()
        {
            return GetGenderList();
        }

        [HttpGet]
        [Route("GetGendersById")]
        public TblGender GetGendersById(int id)
        {
            return GetGenderList().FirstOrDefault(gender => gender.Id == id);
        }

        [HttpPost]
        [Route("InsertGender")]
        public string InsertGender(int id,string gender)
        {
            InsertGenderToDB(new TblGender() { Id = id,Gender = gender});
            return "Item Added Succesfully";
        }

        [HttpPut]
        [Route("UpdateGender")]
        public string UpdateGender(int id,string gender)
        {
            string query = $"UPDATE tblGender SET Gender = '{gender}' WHERE ID = {id}";
            SqlConnection con = new(_configuration.GetConnectionString("DefaultConnection"));
            SqlCommand cmd = new(query, con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            return "Gender Updated Successfully";
        }

        [HttpPut]
        [Route("DeleteGender")]
        public string DeleteGender(int id)
        {
            string deleteQuery = $"UPDATE tblGender SET IsDeleted = 1 WHERE ID = {id}";
            SqlConnection con = new(_configuration.GetConnectionString("DefaultConnection"));
            SqlCommand cmd = new(deleteQuery, con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            return "Gender Deleted Successfully";
        }

        private List<TblGender> GetGenderList()
        {
            List<TblGender> genders = new();
            string selectQuery = "Select * from tblGender";
            SqlConnection con = new(_configuration.GetConnectionString("DefaultConnection"));
            SqlCommand cmd = new(selectQuery, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new();
            da.Fill(dt);

            for(int i = 0; i < dt.Rows.Count; i++)
            {
                TblGender gender = new();
                gender.Id = Convert.ToInt32(dt.Rows[i]["ID"]);
                gender.Gender = dt.Rows[i]["Gender"].ToString();
                gender.IsDeleted = Convert.ToBoolean(dt.Rows[i]["IsDeleted"]);
                genders.Add(gender);
            }

            return genders;
        }

        private void InsertGenderToDB(TblGender tblGenderObj)
        {
            string insertQuery = $"INSERT INTO tblGender(ID,Gender) VALUES({tblGenderObj.Id},'{tblGenderObj.Gender}')";
            SqlConnection con = new(_configuration.GetConnectionString("DefaultConnection"));
            SqlCommand cmd = new(insertQuery, con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}
