using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Ilex.Server.Services.Contracts
{
    public interface IBlobStorage
    {
        Task<bool> DeleteBlobAsync(string ImageName);
        Task<string> SaveImageAsync(IFormFile image);
    }
}