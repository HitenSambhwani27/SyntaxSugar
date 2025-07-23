using Microsoft.EntityFrameworkCore;
using SyntaxSugar.Core.Entities;
using SyntaxSugar.Core.Interfaces;
using SyntaxSugar.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SyntaxSugar.Infrastructure.Repositories
{
    public class ProblemRepository : IProblemRepository, IProblemWriteRepository, IUserProblemRepository
    {
        private readonly ApplicationDbContext _context;
        public ProblemRepository(ApplicationDbContext applicationDb)
        {
            this._context = applicationDb;
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
           return await _context.Categories.ToListAsync();
        }

        public async Task<Problem> GetProblemByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Problem ID cannot be empty.", nameof(id));
            }
            return await _context.Problems.Include(p =>p.Category)
                .Include(p => p.ProblemTags)
                .ThenInclude(pt =>pt.Tag)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Problem>> GetProblemsAsync()
        {
            return await _context.Problems
                .Include(p => p.Category)
                .Include(p => p.ProblemTags)
                .ThenInclude(pt => pt.Tag)
                .ToListAsync();
        }

        public async Task<IEnumerable<Tag>> GetTagsAsync()
        {
            return await _context.Tags.ToListAsync();
        }

        public async Task<UserProblemStatus> GetUserProblemStatusAsync(Guid userId, Guid problemId)
        {
            if (userId == Guid.Empty || problemId == Guid.Empty)
            {
                throw new ArgumentException("Any one of the id is null", nameof(userId));
            }
            return await _context.UserProblemStatuses.FirstOrDefaultAsync(ups => ups.UserId == userId && ups.ProblemId == problemId);
        }

        public async Task AddCategoryAsync(Category category)
        {
            if (category == null)
            {
                throw new ArgumentNullException(nameof(category));
            }
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
        }

        public async Task AddProblemAsync(Problem problem)
        {
            if (problem == null)
            {
                throw new ArgumentNullException(nameof(problem));
            }
            await _context.Problems.AddAsync(problem);
            await _context.SaveChangesAsync();
        }

        public async Task AddTagAsync(Tag tag)
        {
            if (tag == null)
            {
                throw new ArgumentNullException(nameof(tag));
            }
            await _context.Tags.AddAsync(tag);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProblemAsync(Guid id)
        {
            if(id == Guid.Empty)
            {
                throw new ArgumentException("Problem ID cannot be empty.", nameof(id));
            }
            await _context.Problems.Where(p => p.Id == id).ExecuteDeleteAsync();
            await _context.SaveChangesAsync();
        }

        public  Task UpdateCategoryAsync(Category category)
        {
           if(category == null)
           {
                throw new ArgumentNullException(nameof(category));
           }
             _context.Categories.Update(category);
             return _context.SaveChangesAsync();
        }

        public  Task UpdateProblemAsync(Problem problem)
        {
            if (problem == null)
            {
                throw new ArgumentNullException(nameof(problem));
            }
            _context.Problems.Update(problem);
            return _context.SaveChangesAsync();
        }

        public Task UpdateTagAsync(Tag tag)
        {
            if (tag == null)
            {
                throw new ArgumentNullException(nameof(tag));
            }
            _context.Tags.Update(tag);
            return _context.SaveChangesAsync();
        }

        public async Task AddOrUpdateUserProblemStatusAsync(UserProblemStatus status)
        {
            if(status == null)
            {
                throw new ArgumentNullException(nameof(status));
            }
            var existingStatus = await _context.UserProblemStatuses
                .FirstOrDefaultAsync(ups => ups.UserId == status.UserId && ups.ProblemId == status.ProblemId);

            if (existingStatus == null)
            {             
                await _context.UserProblemStatuses.AddAsync(status);
            }
            else
            {
                existingStatus.IsSolved = status.IsSolved;
                existingStatus.LastAttemptedAt = status.LastAttemptedAt;
                _context.UserProblemStatuses.Update(existingStatus);
            }
            await _context.SaveChangesAsync();
        }
    }
}
