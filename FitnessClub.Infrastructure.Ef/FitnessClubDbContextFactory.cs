using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace FitnessClub.Infrastructure.Ef;

public class FitnessClubDbContextFactory: IDesignTimeDbContextFactory<FitnessClubDbContext>
{
    public FitnessClubDbContext CreateDbContext(string[] args)
    {
        var options = new DbContextOptionsBuilder<FitnessClubDbContext>().UseSqlServer().Options;
        return new FitnessClubDbContext(options);
    }
}