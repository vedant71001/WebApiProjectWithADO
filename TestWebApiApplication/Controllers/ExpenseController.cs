
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using TestWebApiApplication.Models;

namespace TestWebApiApplication.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public ExpenseController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        [Route("GetExpenses")]
        public List<Expense> GetExpenses()
        {
            List<Expense> expenses = new();

            string selectQuery = "Select * from expense";
            SqlConnection con = new(_configuration.GetConnectionString("DefaultConnection"));
            SqlCommand cmd = new(selectQuery, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new();
            da.Fill(dt);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Expense expense = new();
                expense.Id = Convert.ToInt64(dt.Rows[i]["id"]);
                expense.Title = dt.Rows[i]["title"].ToString();
                expense.Amount = Convert.ToDouble(dt.Rows[i]["amount"]);
                expense.Date = Convert.ToDateTime(dt.Rows[i]["date"]);
                expenses.Add(expense);
            }

            return expenses;
        }

        [HttpPost]
        [Route("AddExpense")]
        public ContentResult InsertExpense(Expense expense)
        {
            try
            {
                string insertQuery = $"INSERT INTO expense(title, amount, date) VALUES('{expense.Title}',{expense.Amount},'{expense.Date.ToString("yyyy-MM-dd")}')";
                SqlConnection con = new(_configuration.GetConnectionString("DefaultConnection"));
                SqlCommand cmd = new(insertQuery, con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return Content("Expense added successfully");
            }
            catch (Exception ex)
            {
                return Content("Expense add failed"+ex.Message);
            }
        }
    }
}
