using GenoTool.Classes;
using Newtonsoft.Json.Linq;

// Main hasn't been the center of attention yet. I'm not sure I want this to be a CLI application, but I don't want to be bogged down with building a GUI just yet.
void Main() 
{
    // CLI logic
}

// User stephan = new User("Stephan", "Haloftis"); // Create user
// Genome genome = new Genome(stephan, "C:/Users/Stephan/RiderProjects/GenoTool/GenoTool/GeneticData/genome.txt"); // Create genome
// stephan.AcquireGenome(genome); // Assign genome to user
string id = "rs76761725";
// Gene gene = genome.FindGene(id); // Testing FindGene() method
// if (gene != null)
// {
//     Console.WriteLine($"Found gene with ID {gene.RSID} and Genotype {gene.Genotype}");
// }
// else
// {
//     Console.WriteLine($"Could not locate gene with ID {id} in the given genome.");
// }

/*
 * The code below tests the functionality of the SnpClient class
 */

SnpClient client = new SnpClient();
var response = await client.SearchSnp(id); // Replace with your search term

if (!string.IsNullOrEmpty(response))
{
    // Deserialize the JSON response as a JArray
    JArray jsonResponse = JArray.Parse(response);

    // Extract the required elements from the JArray
    int totalResults = jsonResponse[0].Value<int>();
    List<string> codes = jsonResponse[1].ToObject<List<string>>();
    JArray detailedInfoArray = jsonResponse[3].ToObject<JArray>();

    // Console.WriteLine($"Total Results: {totalResults}");
    // Console.WriteLine("Codes:");
    // foreach (var code in codes)
    // {
    //     Console.WriteLine(code);
    // }

    // Process the detailed information array
    foreach (var item in detailedInfoArray)
    {
        // You can access the elements within each item in the array
        string rsNum = item[0].Value<string>();
        string chr = item[1].Value<string>();
        string pos = item[2].Value<string>();
        string alleles = item[3].Value<string>();
        string gene = item[4].Value<string>();

        if (rsNum == id)
        {
            // Perform further processing as needed
            Console.WriteLine($"rsNum: {rsNum}, chr: {chr}, pos: {pos}, alleles: {alleles}, gene: {gene}");   
        }
    }
}

