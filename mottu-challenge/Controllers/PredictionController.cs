
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.ML;      // <-- VOCÊ ADICIONOU ESTA LINHA?
using Microsoft.ML.Data;           // <-- ESTA TAMBÉM PODE SER NECESSÁRIA
using mottu_challenge.DataModels;


namespace mottu_challenge.Controllers
{

    
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class PredictionController : Controller
    {
        private readonly PredictionEnginePool<ModelInput, ModelOutput> _predictionPool;

        public PredictionController(PredictionEnginePool<ModelInput, ModelOutput> predictionPool)
        {
            _predictionPool = predictionPool;
        }

        [HttpPost]
        public ActionResult<ModelOutput> Predict([FromBody] ModelInput input)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            ModelOutput prediction = _predictionPool.Predict(input);
            return Ok(prediction);
        }
    }
}
