using Microsoft.AspNetCore.Mvc;
using SpinGameApp.Model;
using SpinGameApp.Repositories;
using SpinGameApp.Extensions;
using MongoDB.Bson;
using SpinGameApp.Requests;

namespace SpinGameApp.Controllers;

[ApiController]
[Route("api/spingames")]
public class SpinGamesController : ControllerBase
{
    private readonly ISpinGameRepository _spinGameRepo;
    private readonly ISpinResultRepository _spinResultRepo;
    private ObjectId _;

    public SpinGamesController(
            ISpinGameRepository spinGameRepo,
            ISpinResultRepository spinResultRepo
        )
    {
        _spinGameRepo = spinGameRepo;
        _spinResultRepo = spinResultRepo;
    }

    // GET: api/spingames
    [HttpGet]
    public IActionResult GetSpinGame(int page = 1, int pageSize = 10)
    {
        page = page < 1 ? 1 : page;
        pageSize = pageSize < 1 ? 1 : pageSize;

        var spinGames = _spinGameRepo.GetAll(page, pageSize);
        return Ok(spinGames);
    }

    // GET: api/spingames/{id}
    [HttpGet("{id}")]
    public IActionResult GetSpinGame(string id)
    {
        var spinGame = _spinGameRepo.Get(id);
        return spinGame == null ? NotFound() : Ok(spinGame);
    }

    // POST: api/spingames
    [HttpPost]
    public IActionResult CreateSpin([FromBody] CreateSpinGameRequest req)
    {
        var spinGame = CreateSpinGameRequest.To(req);        
        var spinGameId = _spinGameRepo.Create(spinGame);
        var routeValues = new { id = spinGameId };
        var result = new { id = spinGameId };
        return CreatedAtAction(nameof(GetSpinGame), routeValues, result);
    }

    // POST: api/spingames/spin
    [HttpPost("spin")]
    public IActionResult Spin(string spinGameId, string playerId)
    {
        if (string.IsNullOrEmpty(spinGameId))
            return BadRequest($"{nameof(spinGameId)} is empty");
        if (string.IsNullOrEmpty(playerId))
            return BadRequest($"{nameof(playerId)} is empty");

        if (!ObjectId.TryParse(spinGameId, out _))
            return BadRequest($"Invalid {nameof(spinGameId)}");

        var spinGame = _spinGameRepo.Get(spinGameId);
        if (spinGame == null)
            return NotFound();

        if (spinGame.Prizes.Count == 0)
            return BadRequest("Spin game should have prize(s)");

        // [10, 20, 30, 40, 50, 60, 70, 80, 90, 100]
        var sequences = Enumerable.Range(1, 100).Where(x => x % 10 == 0).ToList();
        spinGame.Prizes.Shuffle(spinGame.Prizes.Count);
        sequences.Shuffle(spinGame.Prizes.Count);

        var prize = spinGame.Prizes.First();
        var prizeProbability = prize.Probability;
        List<int> prizeResultSequences = prizeProbability switch
        {
            100 => new List<int> { 100 },
            >= 80 and < 100 => new List<int> { 80, 90, 100 },
            >= 60 and < 80 => new List<int> { 60, 70, 80 },
            >= 30 and < 60 => new List<int> { 30, 40, 50 },
            >= 20 and < 30 => new List<int> { 10, 20, 30 },
            >= 0 and < 20 => new List<int> { 10, 20 },
            _ => new List<int> { 10 },
        };

        var spinResult = new SpinResult
        {
            PlayerId = playerId,
            SpinGameId = spinGameId,
            PrizeName = prize.Name,
            Timestamp = DateTime.UtcNow
        };

        prizeResultSequences.Shuffle(spinGame.Prizes.Count);
        spinResult.PrizeResult = prizeResultSequences.First();
        _spinResultRepo.Create(spinResult);

        return Ok(spinResult);
    }
}
