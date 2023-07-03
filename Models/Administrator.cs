namespace rowi_practice.Models;

public class Administartor
{
    public long Id { get; set; }
    public string LogCode { get; set; }
    public string PassCode { get; set; }
    
    public int AccessLevel { get; set; }
}