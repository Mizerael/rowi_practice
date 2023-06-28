// using rowi_practice.Models;
using Microsoft.AspNetCore.Mvc;

namespace rowi_practice.Controllers;

[ApiController]
[Route("task")]
public class TaskController : ControllerBase
{
    private readonly ILogger<TaskController> _logger;

    public TaskController(ILogger<TaskController> logger)
    {
        _logger = logger;
    }
  
    [HttpGet]
    public string GetAll()
    {
        return "All ttasks";
    }

    [HttpGet("{id}")] 
    public string Get(int id)
    {
        return "{id} task";
    }
    
    [HttpPost]
    protected string create(string s)
    {
        return "s";
    }
    
    [HttpPut("{id}")]
    protected string update(string s)
    {
        return "{id} " + s;
    }
}
