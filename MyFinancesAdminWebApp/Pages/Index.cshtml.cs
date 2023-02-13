using System.Diagnostics;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Npgsql;

namespace MyFinancesAdminWebApp.Pages;
[Authorize(Roles = "admin")]
public class IndexModel : PageModel
{
    private const string CONN_STR = "Server=localhost;Port=5432;Database=myfinances_api;User Id=adef;Password=199as55";
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public int CountUsers { get; set; }
    public int CountTransaction { get; set; }
    public decimal AvgCountTransactionByUser { get; set; }
    public int CountTransactionNowMonth { get; set; }
    
    
    public async Task OnGetAsync()
    {
        OnGetCountUser();
        CountTransaction = GetCountTransaction();
        AvgCountTransactionByUser = GetAvgCountTransactionByUser();
        CountTransactionNowMonth = GetCountTransactionNowMonth();
    }

    public ActionResult OnGetCountUser()
    {
        var sqlCommand = "SELECT count(*) FROM users WHERE role='user';";

        using (var connection = new NpgsqlConnection(CONN_STR))
        {
            connection.Open();
            var result = connection.Query<int>(sqlCommand).FirstOrDefault();
         
            CountUsers = result;
            return Content(CountUsers.ToString());
        }
    }

    private int GetCountTransaction()
    {
        var sqlCommand = "SELECT count(*) FROM transactions;";
        using (var connection = new NpgsqlConnection(CONN_STR))
        {
            connection.Open();
            
            return connection.Query<int>(sqlCommand).FirstOrDefault();
        }
    }

    private decimal GetAvgCountTransactionByUser()
    {
        var sqlCommand = "SELECT round(avg(cnt), 2) FROM (SELECT count(*) cnt FROM transactions GROUP BY login) AS c;";
        using (var connection = new NpgsqlConnection(CONN_STR))
        {
            connection.Open();
            
            return connection.Query<decimal>(sqlCommand).FirstOrDefault();
        }
    }
    
    private int GetCountTransactionNowMonth()
    {
        var sqlCommand = "SELECT count(*) FROM transactions WHERE extract(year from transaction_date)=extract(year from current_date) AND extract(month from transaction_date)=extract(month from current_date);";
        using (var connection = new NpgsqlConnection(CONN_STR))
        {
            connection.Open();
            
            return connection.Query<int>(sqlCommand).FirstOrDefault();
        }
    }

    public ActionResult OnGetDateTimeNow()
    {
        return new ObjectResult(DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss"));
    }
}
