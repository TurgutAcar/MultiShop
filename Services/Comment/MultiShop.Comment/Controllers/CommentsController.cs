using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Comment.BusinessLayer.Abstract;
using MultiShop.Comment.Entities;

namespace MultiShop.Comment.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet]
        public  IActionResult GetCommentList()
        {
            var response = _commentService.TGetAll();
            return StatusCode(response.StatusCode, response);

        }
        [HttpGet("{id}")]
        public IActionResult GetComment(int id)
        {
            var response = _commentService.TGetById(id);
            return StatusCode(response.StatusCode, response);
          

        }
        [HttpPost]
        
        public IActionResult CreateComment(UserComment userComment)
        {
            var response = _commentService.TInsert(userComment);
            return StatusCode(response.StatusCode, response);
           

        }
        [HttpPut]
        public IActionResult UpdateComment(UserComment userComment)
        {
            var response = _commentService.TUpdate(userComment);
            return StatusCode(response.StatusCode, response);

        }
        [HttpDelete]
        public IActionResult DeleteComment(int id)
        {
            var response = _commentService.TDelete(id);
            return StatusCode(response.StatusCode, response);
        
        }
        [HttpGet("CommentListByProductId/{id}")]
        public IActionResult CommentListByProductId(string id)
        {

            var response = _commentService.CommentListByProductId(id);
            return StatusCode(response.StatusCode, response);

        }
        [HttpGet("GetActiveCommentCount")]
        public IActionResult GetActiveCommentCount()
        {
            var response = _commentService.GetActiveCommentCount();
            return StatusCode(response.StatusCode, response);
        }
        [HttpGet("GetPassiveCommentCount")]
        public IActionResult GetPassiveCommentCount()
        {
            var response = _commentService.GetPassiveCommentCount();
            return StatusCode(response.StatusCode, response);
        }
        [HttpGet("GetTotalCommentCount")]
        public IActionResult GetTotalCommentCount()
        {
            var response = _commentService.GetTotalCommentCount();
            return StatusCode(response.StatusCode, response);
        }
    }
}
