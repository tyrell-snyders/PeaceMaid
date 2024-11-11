using PeaceMaid.Application.DTOs;
using PeaceMaid.Domain.Entities;

namespace PeaceMaid.Application.Interfaces;

public interface IReview
{
    public Task<List<Review>> GetAllReviews();
    public Task<Review?> GetReviewAsync(int id);
    public Task<ServiceResponse> CreateReview(Review review);
    public Task<ServiceResponse> UpdateReview(Review review);
    public Task<ServiceResponse> DeleteReview(int id);
}
