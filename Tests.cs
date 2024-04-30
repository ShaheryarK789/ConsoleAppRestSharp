using NUnit.Framework;
using RestSharp;
using System;
using ConsoleAppRestSharp3;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

[TestFixture]
public class Tests
{
    private RestClient client;
    private  RestRequest request;
    private  RestResponse response;


    [SetUp]
    public void SetUp()
    {
        // Initialize RestClient with base URL
        client = new RestClient("https://simple-books-api.glitch.me");
        // Create a request
        var request1 = new RestRequest("/books", Method.Get);
        request = request1;

        // Execute the request
        var response1 = client.Execute(request);
        response = response1;

    }

    [Test]
   public void VerifyStatusCode()

    {
        // Verify the status code
        Assert.That(response.StatusCode,Is.EqualTo(System.Net.HttpStatusCode.OK));

    }

    [Test]
    public void VerifyPropertiesInResponse()
    
    {
        // Deserialize JSON response
        dynamic jsonData = JsonConvert.DeserializeObject(response.Content);

        // Verify properties in response
        Assert.That(jsonData[0].ContainsKey("id"));
        Assert.That(jsonData[0].ContainsKey("name"));
    }


    [Test]
    public void VerifyFirstNameValue()
    {

        // Deserialize JSON response
        dynamic jsonData = JsonConvert.DeserializeObject(response.Content);

        // Verify the value of the first name
        Assert.That("The Russian", Is.EqualTo(jsonData[0].name.ToString()));
    }


    [Test]
    public void VerifyFirstIdValue()
    
    {
        // Deserialize JSON response
        JArray jsonData = JArray.Parse(response.Content);

        // Verify value of 1st id
        Assert.That(1, Is.EqualTo((int)jsonData[0]["id"]));
    }


}
