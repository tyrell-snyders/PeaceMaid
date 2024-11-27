using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PeaceMaid.Application.Interfaces;
using PeaceMaid.Domain.Entities;

namespace PeaceMaid.Presentation.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController(IReview review) : ControllerBase
    {
        private readonly IReview _review = review;

        [HttpGet]
        public async Task<IActionResult> GetReviews()
        {
            var data = await _review.GetAllReviewsAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetReview(int id)
        {
            var data = await _review.GetReviewAsync(id);
            return Ok(data);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> PostReview([FromBody] Review rvw)
        {
            var data = await _review.CreateReviewAsync(rvw);
            return Ok(data);
        }

        [HttpPut("update/{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateReview([FromBody] Review rvw)
        {
            var data = await _review.UpdateReviewAsync(rvw);
            return Ok(data);
        }

        [HttpDelete("delete/{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteReview(int id)
        {
            var data = await _review.DeleteReviewAsync(id);
            return Ok(data);
        }
    }
}
