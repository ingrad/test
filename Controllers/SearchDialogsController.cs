using iMessengerCoreAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestWeb.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SearchDialogsController : ControllerBase
    {

        private readonly ILogger<SearchDialogsController> _logger;
        private readonly RGDialogsClients context;

        public SearchDialogsController(ILogger<SearchDialogsController> logger)
        {
            _logger = logger;
            context = new RGDialogsClients();
        }

        /// <summary>
        /// Осуществляет поиск диалога
        /// </summary>
        /// <remarks>
        ///     POST SearchByAllClients/
        ///         [
        ///             "4b6a6b9a-2303-402a-9970-6e71f4a47151",
        ///             "c72e5cb5-d6b4-4c0c-9992-d7ae1c53a820"
        ///         ]
        /// </remarks>
        /// <param name="clientIds">идентификаторы клиентов</param>
        /// <returns></returns>
        /// <response code="200">Возвращает первый найденный диалог</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<Guid> SearchByAllClients(Guid[] clientIds)
        {
            var dialogId = context.Init()
                            .GroupBy(c => c.IDRGDialog)
                            .Where(g => clientIds.All(c => g.Any(h => h.IDClient == c)))
                            .Select(c => c.Key)
                            .FirstOrDefault();
            
            return Ok(dialogId);
        }
    }
}
