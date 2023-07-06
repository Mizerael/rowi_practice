namespace rowi_practice.Models;

enum Role : int
{
    administrator,
    user
}

public class User
{
    public long Id { get; set; }
    public string LogCode { get; set; }
    public string PassCode { get; set; }
    public string Email { get; set; }
    public int Role { get; set; }

}