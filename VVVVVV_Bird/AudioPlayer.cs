using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VVVVVV_Bird
{
    public class AudioPlayer
    {
        Dictionary<string, Song> songList;

        public AudioPlayer()
        {
            songList = new Dictionary<string, Song>();
        }

        public void Add(string songName, Song song)
        {
            songList.Add(songName, song);
        }

        public void Play(string song)
        {
            Song songObject;
            if (songList.TryGetValue(song, out songObject))
            {
                if (MediaPlayer.Queue.ActiveSong == null ||
                    (MediaPlayer.Queue.ActiveSong != null && !MediaPlayer.Queue.ActiveSong.FilePath.Equals(songObject.FilePath)))
                {
                    MediaPlayer.Stop();
                    MediaPlayer.Play(songObject);
                }
            }
            else
                throw new Exception("Fuck you"); //Very Mature Paul
        }

        public void Stop()
        {
            MediaPlayer.Stop();
        }
    }
}
