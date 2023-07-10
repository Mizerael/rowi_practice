namespace rowi_practice.Models;

public class ExistingProblem
{
    public int Id { get; set; }
    public int Price { get; set; }
    public string Name { get; set; }
    public string Contains  { get; set; }
    public string Tests { get; set; }
    public DateTime DataCreated { get; set; }
    public DateTime EndPointDate { get; set; }
}
