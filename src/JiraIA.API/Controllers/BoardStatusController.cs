using JiraIA.Domain.DTOs;
using JiraIA.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace JiraIA.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BoardStatusController : Controller
    {
        private readonly IBoardStatusService _boardStatusService;

        public BoardStatusController(IBoardStatusService boardStatusService)
        {
            _boardStatusService = boardStatusService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<BoardStatusDTO>> GetAllBoardStatus()
        {
            var boardStatus = _boardStatusService.GetAllBoardStatus();

            return Ok(boardStatus);
        }

        [HttpPost]
        public async Task<ActionResult<BoardStatusDTO>> AddBoardStatus([FromBody] BoardStatusDTO boardStatus)
        {
            var statusCreated = await _boardStatusService.AddBoardStatus(boardStatus);

            return Ok(statusCreated);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBoardStatus([FromRoute] string id)
        {
            var statusDeleted = await _boardStatusService.DeleteBoardStatus(id);

            return Ok(statusDeleted);
        }

    }
}
