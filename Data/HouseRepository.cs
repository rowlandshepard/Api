using Microsoft.EntityFrameworkCore;

public interface IHouseRepository
{
    Task<List<HouseDto>> GetAll();
    Task<HouseDetailDto?> Get(int id);
}
public class HouseRepository : IHouseRepository
{
    private readonly HouseDbContext context;

    public HouseRepository(HouseDbContext context)
    {
        this.context = context;
    }

    public async Task<List<HouseDto>> GetAll()
    {
        return await context.Houses.Select(h => new HouseDto(h.Id, h.Address, h.Country, h.Price))
        .ToListAsync();
    }

    public async Task<HouseDetailDto?> Get(int id)
    {
        var h = await context.Houses.SingleOrDefaultAsync(
            e => e.Id == id);
        if (h == null)
            return null;

        return new HouseDetailDto(h.Id, h.Address, h.Country, h.Price, h.Description, h.Photo);
    }
}