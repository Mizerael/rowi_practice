namespace rowi_practice.Models;

public class ExistingProblem
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Contains  {get; set; }
    public string Tests { get; set; }
    public DateTime DataCreated { get; set; }
    public DateTime EndPointDate { get; set; }
    bool decided { get; set; } = false;
}
