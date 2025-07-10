
using Microsoft.AspNetCore.Mvc;

namespace NDD.Api.Mapeamento.API.Base
{
    [Route("api/[controller]")]
    public class PublicController : ControllerBase
    {
        /// <summary>
        /// Informa para o client que está ativa
        /// Útil para validar tokens e para descobrir o estado da API
        /// </summary>
        [HttpGet]
        [Route("is-alive")]
        public IActionResult IsAlive()
        {
            return Ok(true);
        }

    }
}