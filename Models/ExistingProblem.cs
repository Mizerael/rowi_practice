namespace rowi_practice.Models;

public class ExistingProblem
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Contains  {get; set; }
    public string Tests { get; set; }
    protected bool decided { get; set; }
}
