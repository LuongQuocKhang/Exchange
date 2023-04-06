using Exchange.Data.Entities;
using Exchange.Data.Repositories;
using Exchange.Signal.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Exchange.Signal.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class SignalController : ControllerBase
    {
        private readonly ISignalService _signalService;
        private readonly ILogger<SignalController> _logger;

        public SignalController(ISignalService signalService, ILogger<SignalController> logger)
        {
            _signalService = signalService ?? throw new ArgumentNullException(nameof(signalService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(IEnumerable<Signals>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Signals>>> GetAllSignals(SearchConditionModel model)
        {
            var products = await _signalService.GetAllSignalsAsync(model);

            return Ok(products);
        }

        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(IEnumerable<Signals>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Signals>>> GetAllSignalsByRSI(SearchConditionModel model)
        {
            var products = await _signalService.GetAllSignalsByRSIAsync(model);

            return Ok(products);
        }

        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(IEnumerable<Signals>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Signals>>> GetAllSignalsByTrendLine(SearchConditionModel model)
        {
            var products = await _signalService.GetAllSignalsByTrendLineAsync(model);

            return Ok(products);
        }

        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(IEnumerable<Signals>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Signals>>> GetAllSignalsByCrossEMA(SearchConditionModel model)
        {
            var products = await _signalService.GetAllSignalsByCrossEMAAsync(model);

            return Ok(products);
        }

        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(typeof(IEnumerable<Signals>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Signals>>> CalculateSignalsByHistoricalData(SearchConditionModel model)
        {
            await _signalService.CalculateSignalsByHistoricalDataAsync(model);

            return Ok();
        }
    }
}
