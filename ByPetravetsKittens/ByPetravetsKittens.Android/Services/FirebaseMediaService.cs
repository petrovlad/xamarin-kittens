using Android.Gms.Extensions;
using Firebase.Storage;
using ByPetravetsKittens.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace ByPetravetsKittens.Droid.Services
{
    class FirebaseMediaService : IMediaService
    {
        private const string GalleryPath = "gallery";
        private const string VideosPath = "videos";
        private const string AvatarsPath = "avatars";
        private readonly FirebaseStorage _instance = FirebaseStorage.Instance;

        public async Task DeleteImages(IEnumerable<string> imageUrls)
        {
            var tasks = imageUrls.Select(async imageUrl =>
            {
                var imageReference = _instance.GetReferenceFromUrl(imageUrl);
                await imageReference.DeleteAsync();
            });

            await Task.WhenAll(tasks);
        }

        public async Task DeleteVideo(string videoUrl)
        {
            var videoReference = _instance.GetReferenceFromUrl(videoUrl);
            await videoReference.DeleteAsync();
        }

        public async Task<string> UploadAvatar(string userId, byte[] avatarData)
        {
            var avatarReference = _instance.Reference.Child(AvatarsPath).Child(userId);
            await avatarReference.PutBytes(avatarData);
            return (await avatarReference.GetDownloadUrl()).ToString();
        }

        public async Task<IEnumerable<string>> UploadImages(string userId, IEnumerable<FileResult> images)
        {
            var galleryReference = GetUserGalleyReference(userId);

            var tasks = images.Select(async image =>
            {
                var imageStream = await image.OpenReadAsync();
                var imageReference = galleryReference.Child(Guid.NewGuid().ToString());
                var uploadTask = await imageReference.PutStream(imageStream);
                return (await imageReference.GetDownloadUrl()).ToString();
            });

            return await Task.WhenAll(tasks);
        }

        public async Task<string> UploadVideo(string userId, FileResult video)
        {
            var videoReference = GetUserVideosReference(userId).Child(Guid.NewGuid().ToString());
            var videoStream = await video.OpenReadAsync();
            await videoReference.PutStream(videoStream);
            return (await videoReference.GetDownloadUrlAsync()).ToString();
        }

        private StorageReference GetUserGalleyReference(string userId)
        {
            return _instance.Reference.Child(userId).Child(GalleryPath);
        }       

        private StorageReference GetUserVideosReference(string userId)
        {
            return _instance.Reference.Child(userId).Child(VideosPath);
        }
    }
}