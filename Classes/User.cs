namespace GenoTool.Classes;

/*
    User class for creating Users, nothing special going on here. 
*/
public class User 
{
    // Attributes
    public string FirstName;
    public string LastName;
    public Genome Genome; // Not to be initialized in constructor
    
    // Constructor
    public User(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }

    // Assigning genome after construction
    public void AcquireGenome(Genome genome)
    {
        Genome = genome;
    }
}