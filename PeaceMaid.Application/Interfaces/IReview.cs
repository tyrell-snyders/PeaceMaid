using PeaceMaid.Application.DTOs;
using PeaceMaid.Domain.Entities;

namespace PeaceMaid.Application.Interfaces;

public interface IReview
{
    public Task<List<Review>> GetAllReviewsAsync();
    public Task<Review?> GetReviewAsync(int id);
    public Task<ServiceResponse> CreateReviewAsync(Review review);
    public Task<ServiceResponse> UpdateReviewAsync(Review review);
    public Task<ServiceResponse> DeleteReviewAsync(int id);
}
