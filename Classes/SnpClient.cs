namespace GenoTool.Classes;

using System;
using System.Net.Http;
using System.Threading.Tasks;

/*
   The SnpClient class is responsible for interacting with the external API that provides information about single nucleotide polymorphisms (SNPs) in the human genome.
   External API: https://clinicaltables.nlm.nih.gov/apidoc/snps/v3/doc.html#params
   
   This class is designed to perform searches for SNPs based on a given search term.

   Namespace: GenoTool.Classes

   Using Statements:
   - System: Provides fundamental types and base classes that are essential for the class.
   - System.Net.Http: Provides classes for sending HTTP requests and receiving HTTP responses.
   - System.Threading.Tasks: Provides types for managing asynchronous operations.

   Class Overview:
   - The class contains a constructor and a method for searching SNPs.
   - It uses an HttpClient to send HTTP GET requests to the API and retrieve JSON data.
   - The retrieved JSON data is deserialized into an object of type SNPResult.

   Class Members:
   - Private Fields:
     - _httpClient: An instance of HttpClient used for making HTTP requests.
     - BaseUrl: A constant string representing the base URL of the SNP API.

   Constructor:
   - SnpClient(): Initializes the _httpClient field.

   API Request Method:
   - SearchSnp(string searchTerm): Performs a search for SNPs based on the provided search term.
     - Parameters:
       - searchTerm: A string representing the search term (e.g., an rs number).
     - Returns:
       - A Task<string?>: Asynchronous task that represents the JSON response from the API.
         - The JSON response is a string that contains information about SNPs.

   Method Details:
   - The SearchSnp method constructs the API URL with query parameters, sends an HTTP GET request to the API, and handles the response.
   - If the request is successful (HTTP status code 200), it reads and returns the JSON response.
   - If there is an error or exception during the process, it handles and logs the error, returning null.

   Nested Class:
   - SNPResult: A data class representing the result of an SNP search.
     - Contains two properties: TotalResults (an integer) and Codes (a list of strings).
     - TotalResults indicates the total number of results.
     - Codes contains a list of SNP codes.

   Note: Additional functionality and error handling can be added to enhance the class as needed. :)
*/

public class SnpClient
{
    // Props
    private readonly HttpClient _httpClient;
    private static readonly string BaseUrl = "https://clinicaltables.nlm.nih.gov/api/snps/v3/search";

    // Constructor
    public SnpClient()
    {
        _httpClient = new HttpClient();
    }
    
    // API Request Method
    public async Task<string?> SearchSnp(string searchTerm)
    {
        var queryParams = new Dictionary<string, string>
        {
            { "terms", searchTerm }
            // add other params later
        };
        
        // Construct complete URL with query parameters
        var apiUrl = $"{BaseUrl}?{string.Join("&", queryParams.Select(kv => $"{kv.Key}={kv.Value}"))}";

        try
        {
            // Send the GET request
            var response = await _httpClient.GetAsync(apiUrl);

            // Check if the request was successful
            if (response.IsSuccessStatusCode)
            {
                // Read and return the response content, JSON data
                using (Stream responseStream = await response.Content.ReadAsStreamAsync())
                using (StreamReader reader = new StreamReader(responseStream))
                {
                    string? jsonData = await reader.ReadToEndAsync();
                    return jsonData;
                }
            }
            else
            {
                // Handle error cases here
                Console.WriteLine($"Error: {response.StatusCode}");
                return null;
            }
        }
        catch (Exception ex)
        {
            // Handle exception
            Console.WriteLine($"Exception: {ex.Message}");
            return null;
        }
    }
    
    // Nested SNPResult class
    public class SNPResult
    {
        public int TotalResults { get; set; }
        public List<string> Codes { get; set; }
    }
}
