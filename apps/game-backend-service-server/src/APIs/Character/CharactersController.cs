using Microsoft.AspNetCore.Mvc;

namespace GameBackendService.APIs;

[ApiController()]
public class CharactersController : CharactersControllerBase
{
    public CharactersController(ICharactersService service)
        : base(service) { }
}
