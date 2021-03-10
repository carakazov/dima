using System.Collections.Generic;
using System.Web;
using MySocialNetwork.DAO;
using MySocialNetwork.Models.SocialNetwork;

namespace MySocialNetwork.Utils
{
    public class ContentMapper
    {
        private ContentManager contentManager = new ContentManager();
        public Content FromPostedFileToContent(HttpPostedFileBase file)
        {
            Content content = new Content();
            byte[] media = new byte[file.ContentLength];
            file.InputStream.Read(media, 0, media.Length);
            content.Material = media;
            string contentType = file.ContentType;
            ContentType type;
            if (contentType.Contains(ContentTypes.Image.ToString().ToLower()))
            {
                type = contentManager.GetContentType(ContentTypes.Image);
            }
            else if (contentType.Contains(ContentTypes.Video.ToString().ToLower()))
            {
                type = contentManager.GetContentType(ContentTypes.Video);
            }
            else
            {
                type = contentManager.GetContentType(ContentTypes.Audio);
            }

            content.ContentTypeId = type.Id;
            return content;
        }


        public List<Content> GetContentList(List<HttpPostedFileBase> files)
        {
            List<Content> contents = new List<Content>();
            foreach (HttpPostedFileBase file in files)
            {
                contents.Add(FromPostedFileToContent(file));
            }

            return contents;
        }
    }
}