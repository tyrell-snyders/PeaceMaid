using Microsoft.EntityFrameworkCore;
using PeaceMaid.Application.DTOs;
using PeaceMaid.Application.Interfaces;
using PeaceMaid.Domain.Entities;
using PeaceMaid.Infrastructure.Data;

namespace PeaceMaid.Infrastructure.Implementation;

public class ReviewRepo(AppDbContext context) : IReview
{
    private readonly AppDbContext _context = context;

    public async Task<ServiceResponse> CreateReview(Review review)
    {
        await _context.Reviews.AddAsync(review);
        await SaveChangesAsync();

        return new(true, "Added");
    }

    public async Task<ServiceResponse> DeleteReview(int id)
    {
        var review = await _context.Reviews.FindAsync(id);
        if (review == null)
            return new(false, "Review not found!");

        _context.Reviews.Remove(review);
        await SaveChangesAsync();

        return new(true, "Deleted");
    }

    public async Task<List<Review>> GetAllReviews() =>
        await _context.Reviews.Distinct().Include(r => r.User).ToListAsync();

    public async Task<Review?> GetReviewAsync(int id) =>
        await _context.Reviews.FindAsync(id);

    public async Task<ServiceResponse> UpdateReview(Review review)
    {
        _context.Reviews.Update(review);
        await SaveChangesAsync();

        return new(true, "Updarted");
    }

    private async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
