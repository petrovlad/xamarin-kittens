using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace ByPetravetsKittens.Services
{
    public interface IMediaService
    {
        Task<string> UploadAvatar(string userId, byte[] avatarData);
        Task<IEnumerable<string>> UploadImages(string userId, IEnumerable<FileResult> images);
        Task<string> UploadVideo(string userId, FileResult video);
        Task DeleteVideo(string videoUrl);
        Task DeleteImages(IEnumerable<string> imageUrls);
    }
}
