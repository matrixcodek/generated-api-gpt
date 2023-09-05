using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class FormController : ControllerBase
{
    [HttpGet("form")]
    public IActionResult Form(string param1 = null, int? param2 = null, string param3 = null, string param4 = null)
    {
        return Ok(new 
        {
            param1,
            param2,
            param3,
            param4
        });
    }
}