using TwoOneTwoGames.UIManager.Data.ExtensionsData;
using UnityEngine.Video;

namespace TwoOneTwoGames.UIManager.Utilities.Extensions
{
    public static class VideoPlayerExtensions
    {
        public static void SetData(this VideoPlayer videoPlayer, VideoPlayerViewData data)
        {
            videoPlayer.clip = data.VideoClip;
            videoPlayer.isLooping = data.IsLooping;
            if (data.IsAutoPlay)
            {
                videoPlayer.Play();
            }
        }
    }
}