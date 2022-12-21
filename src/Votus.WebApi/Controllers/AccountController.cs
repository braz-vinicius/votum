using System.Collections.Generic;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Votus.Application;
using Votus.Domain.Abstractions;
using Votus.Infrastructure.Data;
using Votus.Infrastructure.Identity;

namespace Votus.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IStorageClient _storageClient;
        private readonly ApplicationDbContext _applicationDbContext;


        public AccountController(IStorageClient storageClient, ApplicationDbContext applicationDbContext)
        {
            _storageClient = storageClient;
            _applicationDbContext = applicationDbContext;
        }

        [Route("{id}/image")]
        [HttpPost]
        public async Task Post([FromRoute] string id, IFormFile file)
        {
            await using var stream = new MemoryStream();

            await file.CopyToAsync(stream);

            var fileName = $"{id}.jfif";

            await _storageClient.UploadFileAsync(fileName, stream.ToArray());

            var user = _applicationDbContext.Users.Find(id);
            //var user = await _userManager.GetUserAsync(ClaimsPrincipal.Current);
            
            user.ProfileImageUrl = $"https://votuscontentstorage.blob.core.windows.net/profile-images/{fileName}";

            _applicationDbContext.Users.Update(user);

            await _applicationDbContext.SaveChangesAsync();
        }
    }
}
