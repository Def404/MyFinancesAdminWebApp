using System.Net;
using RestSharp;

namespace MyFinancesAdminWebApp.Controllers;

public static class LoginController
{
    public static RestResponse GetUserToken(string login, string password)
    {
        var client = new RestClient("https://localhost:7274/");
        var request = new RestRequest("/api/Login"){
            RequestFormat = DataFormat.Json
        };
        request.AddJsonBody(new{login = login, password = password});

        try
        {
            var response =  client.Post(request);
            return response;
        }
        catch (Exception e)
        {
            var errorResponse = new RestResponse{
                Content = "Нет подключения к серверу",
                StatusCode = HttpStatusCode.InternalServerError
            };

            return errorResponse;
        }
    }
}
