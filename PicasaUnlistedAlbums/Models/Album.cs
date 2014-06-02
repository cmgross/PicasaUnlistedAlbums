using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using Google.GData.Photos;
using Google.GData.Client;
using Google.GData.Extensions;
using Google.GData.Extensions.Location;
using Google.Picasa;


namespace PicasaUnlistedAlbums.Models
{
    public class PicasaAlbum
    {
        public string Title { get; set; }
        public string Link { get; set; }
        public string Thumbnail { get; set; }

        public static IEnumerable<PicasaAlbum> GetAlbums()
        {
            var albums = new List<PicasaAlbum>();
            PicasaService service = new PicasaService("PicasaUnlistedAlbums");
            service.setUserCredentials(WebConfigurationManager.AppSettings["Username"], WebConfigurationManager.AppSettings["Password"]);
            AlbumQuery query = new AlbumQuery(PicasaQuery.CreatePicasaUri("default"));
            PicasaFeed feed = service.Query(query);
            var albumsToExclude = WebConfigurationManager.AppSettings["AlbumsToExclude"].Split(',').ToList();
            foreach (var atomEntry in feed.Entries)
            {
                var entry = (PicasaEntry)atomEntry;
                if (albumsToExclude.Contains(entry.Title.Text) || entry.Title.Text.Contains("Hangout")) continue;
                var album = new PicasaAlbum
                {
                    Title = entry.Title.Text,
                    Thumbnail = entry.Media.Thumbnails[0].Url,
                    Link = entry.AlternateUri.Content
                };
                albums.Add(album);
            }
            return albums;
        }
    }
}