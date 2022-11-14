namespace apibasics.Controllers;

public class StatusController : ControllerBase
{
    [HttpGet("/status")]
    public ActionResult Get()
    {
        return Ok("good");
    }
}
