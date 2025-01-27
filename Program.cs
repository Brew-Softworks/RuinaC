using System.Threading.Tasks.Dataflow;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Spectre.Console;
using RestSharp;

// Ruina Objects
string Token = "";
var Client = new RestClient("https://discord.com/api/v9");

// Init Ruina
AnsiConsole.Markup(@"[green]                   
 █▀█ █░█ █ █▄░█ ▄▀█
 █▀▄ █▄█ █ █░▀█ █▀█[/]");
Console.WriteLine("");

while (true)
{
    Console.Write("%> ");
    string userInput = Console.ReadLine();

    if (userInput == "message")
    {
        var Request = new RestRequest("channels/1299193628371058729/messages");
        Request.AddParameter("content", "@everyone");
        Request.AddHeader("Authorization", Token);
        var Response = Client.ExecutePost(Request);
        var Content = Response.Content;
        var jsonContent = JObject.Parse(Content!); // ;3 "jsonContent["channel_id"]"
        if (Response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            AnsiConsole.Markup("[green]200 - SUCCESS[/]");
            // https://discord.com/api/v9/channels/1299193628371058729/messages/1333548781601296417
            var Delete = new RestRequest($"channels/{jsonContent["channel_id"]}/messages/{jsonContent["id"]}");
            Delete.AddHeader("Authorization", Token);
            var ResponseDelete = Client.ExecuteDelete(Delete);
            Console.WriteLine(ResponseDelete.Content);
        }
        else
        {
            AnsiConsole.Markup($"[red]{Convert.ToString(Response.StatusCode)} - FAIL[/]");
        }
    }
    else
    {
        AnsiConsole.Markup("[red]Invalid command[/]");
    }
    Console.WriteLine("");
}