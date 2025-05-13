using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultiShop.Comment.Context;
using MultiShop.Comment.Entities;

namespace MultiShop.Comment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class CommentsController : ControllerBase
    {
        private readonly CommentContext _commentContext;

        public CommentsController(CommentContext commentContext)
        {
            _commentContext = commentContext;
        }
        [HttpGet]
        public  IActionResult GetCommentList()
        {
            var values =  _commentContext.UserComments.ToList();
            return Ok(values);

        }
        [HttpGet("{id}")]
        public IActionResult GetComment(int id)
        {
            var value = _commentContext.UserComments.Find(id);
            return Ok(value);

        }
        [HttpPost]
        public IActionResult CreateComment(UserComment userComment)
        {
           _commentContext.UserComments.Add(userComment); 
            _commentContext.SaveChanges();
            return Ok("Comment olusturuldu");

        }
        [HttpPut]
        public IActionResult UpdateComment(UserComment userComment)
        {
            _commentContext.UserComments.Update(userComment);
            _commentContext.SaveChanges();
            return Ok("Comment Güncellendi");

        }
        [HttpDelete]
        public IActionResult DeleteComment(int id)
        {
            var comment=_commentContext.UserComments.Find(id);
            _commentContext.UserComments.Remove(comment);   
            _commentContext.SaveChanges();
            return Ok("Comment silindi");

        }
        [HttpGet("CommentListByProductId")]
        public IActionResult CommentListByProductId(string id)
        {
            var value = _commentContext.UserComments.Where(x=>x.ProductId==id).ToList();
            return Ok(value);

        }
    }
}
