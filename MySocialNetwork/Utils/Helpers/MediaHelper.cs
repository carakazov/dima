using System;
using System.Web;
using System.Web.Mvc;
using MySocialNetwork.Models.SocialNetwork;


namespace MySocialNetwork.Utils.Helpers
{
    public static class MediaHelper
    {
        public static HtmlString RenderMedia(this HtmlHelper helper, byte[] content, ContentTypes type)
        {            
            TagBuilder div = new TagBuilder("div");
            TagBuilder tag = null;
            string base64;
            switch (type)
            {
                case ContentTypes.Image:
                    tag = new TagBuilder("img");
                    base64 = Convert.ToBase64String(content);
                    tag.Attributes["src"] = @"data:image/jpg;base64," + base64;
                    tag.Attributes["width"] = 300.ToString();
                    tag.Attributes["height"] = 300.ToString();
                    break;
                case ContentTypes.Audio:
                    tag = new TagBuilder("audio");
                    base64 = Convert.ToBase64String(content);
                    tag.Attributes["src"] = @"data:audio/mpeg;base64," + base64;
                    tag.Attributes["controls"] = "controls";
                    break;
                case ContentTypes.Video:
                    tag = new TagBuilder("video");
                    base64 = Convert.ToBase64String(content);
                    tag.Attributes["src"] = @"data:video/mp4;base64," + base64;
                    tag.Attributes["controls"] = "controls";
                    break;
            } 
            div.InnerHtml = tag.ToString();
            return new HtmlString(div.ToString());
        }
    }
}