namespace PMS.Application.Interfaces.Services
{
    public interface IDatabaseInitializer
    {
        Task SeedAsync();
    }

}
