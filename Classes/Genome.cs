namespace GenoTool.Classes;

/*
    This code defines two classes, Gene and Genome, for managing genetic data within the GenoTool application.

    Class: Gene
    - Represents a single gene with attributes such as RSID, chromosome, location, and genotype.
    - Provides a constructor to initialize these attributes when creating a Gene object.

    Class: Genome
    - Represents a genome composed of Gene objects.
    - Contains attributes for the owner (User), raw data file, and an array of Gene objects.
    - Provides a constructor to create a Genome object from raw genetic data stored in a file.
    - Includes methods for finding a Gene by its RSID and retrieving the genotype for a specific RSID.

    Class Details:
    - Gene Class:
      - Attributes:
        - RSID: A string representing the Reference SNP ID.
        - Chromosome: A string representing the chromosome where the gene is located.
        - Location: A string representing the gene's location.
        - Genotype: A string representing the gene's genotype.
      - Constructor:
        - Gene(string rsid, string chromosome, string location, string genotype): Initializes the attributes when creating a Gene object.

    - Genome Class:
      - Attributes:
        - Owner: A reference to the User who owns the genome.
        - RawDataFile: A string representing the path to the raw genetic data file.
        - Genes: An array of Gene objects representing the genes in the genome.
      - Constructor:
        - Genome(User owner, string rawdata): Initializes the attributes by reading genetic data from a file.
          - Parses the data in the file and creates Gene objects for each gene.
      - Methods:
        - FindGene(string rsid): Finds and returns a Gene object by its RSID.
        - FindGenoType(string rsid): Returns the genotype of a specific RSID.
    
    Note:
    - The code assumes a specific file format for the raw genetic data, with tab-separated values in each line.
    - Error handling and file path management should be added for robustness.
*/

public class Gene
{
    // Gene Attributes
    public string RSID;
    public string Chromosome;
    public string Location;
    public string Genotype;

    // Gene Constructor
    public Gene(string rsid, string chromosome, string location, string genotype)
    {
        RSID = rsid;
        Chromosome = chromosome;
        Location = location;
        Genotype = genotype;
    }
}

/*
    Genome class to create Genome objects composed of Gene objects
*/
public class Genome
{
    // Genome Attributes
    public User Owner;
    public string RawDataFile;
    public Gene[] Genes;

    // Genome Constructor
    public Genome(User owner, string rawdata) 
    {
        Owner = owner;
        RawDataFile = rawdata;
        List<Gene> genes = new List<Gene>();

        // Access raw data
        StreamReader reader = new StreamReader("C:/Users/Stephan/RiderProjects/GenoTool/GenoTool/GeneticData/genome.txt");

        // Read each line
        while (!reader.EndOfStream)
        {
            string line = reader.ReadLine();
            string[] fields = line.Split('\t');
            
            // Assign Gene data from fields
            Gene gene = new Gene(fields[0], fields[1], fields[2], fields[3]);
            genes.Add(gene);
        }
        // Convert list to an array
        Genes = genes.ToArray();
    }

    // FindGene method to locate a gene by it's RSID
    public Gene FindGene(string rsid) 
    {
        foreach (Gene gene in Genes)
        {
            if (gene.RSID == rsid)
            {
                return gene;
            }
        }
        return null; // gene not found
    }
    
    // FindGenoType method to return the genotype of a specific RSID
    public string FindGenoType(string rsid)
    {
        foreach (Gene gene in Genes)
        {
            if (gene.RSID == rsid)
            {
                return gene.Genotype;
            }
        }

        return "";
    }
}