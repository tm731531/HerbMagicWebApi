using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HerbMagicWebApi.Models
{
    public class ReplyModel
    {
        public string replyToken { get; set; }
        public List<ReplyMessage> messages { get; set; }
    }

    public class ReplyMessage
    {
        public string id { get; set; }
        public string type { get; set; }
        public string text { get; set; }
        public string packageId { get; set; }
        public string stickerId { get; set; }
        public string altText { get; set; }
        public string title { get; set; }
        public string address { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public Styles styles { get; set; }
        public Header2 header { get; set; }
        public Hero hero { get; set; }
        public Body2 body { get; set; }
        public Footer2 footer { get; set; }
        public string thumbnailImageUrl { get; set; }
        public string originalContentUrl { get; set; }
        public string previewImageUrl { get; set; }
        public Template template { get; set; }
        public  int duration { get; set; }
        public ContentProvider contentProvider { get; set; }
        


    }

    public class Header
    {
        public string backgroundColor { get; set; }
    }

    public class Body
    {
        public string backgroundColor { get; set; }
    }

    public class Footer
    {
        public string backgroundColor { get; set; }
    }

    public class Styles
    {
        public Header header { get; set; }
        public Body body { get; set; }
        public Footer footer { get; set; }
    }

    public class Content
    {
        public string type { get; set; }
        public string text { get; set; }
    }

    public class Header2
    {
        public string type { get; set; }
        public string layout { get; set; }
        public List<Content> contents { get; set; }
    }

    public class Hero
    {
        public string type { get; set; }
        public string url { get; set; }
        public string size { get; set; }
        public string aspectRatio { get; set; }
    }

    public class Content2
    {
        public string type { get; set; }
        public string text { get; set; }
    }

    public class Body2
    {
        public string type { get; set; }
        public string layout { get; set; }
        public List<Content2> contents { get; set; }
    }

    public class Content3
    {
        public string type { get; set; }
        public string text { get; set; }
    }

    public class Footer2
    {
        public string type { get; set; }
        public string layout { get; set; }
        public List<Content3> contents { get; set; }
    }





    public class ContentProvider
    {/// <summary>
     /// external  line
     /// </summary>
        public string type { get; set; }
        public string originalContentUrl { get; set; }
        public string previewImageUrl { get; set; }
        
    }

    public class Template
    {
        public string type { get; set; }
        public List<Column> columns { get; set; }
        public string thumbnailImageUrl { get; set; }
        public string imageAspectRatio { get; set; }
        public string imageSize { get; set; }
        public string imageBackgroundColor { get; set; }
        public string title { get; set; }
        public string text { get; set; }
        public Defaultaction defaultAction { get; set; }
        public List<Action> actions { get; set; }
    }

    public class Action
    {
        public string type { get; set; }
        public string label { get; set; }
        public string data { get; set; }
        public string uri { get; set; }
        public string linkUri { get; set; }
        
        public string text { get; set; }
    }

    public class Column
    {
        
        public string imageUrl { get; set; }
        public string thumbnailImageUrl { get; set; }
        public string title { get; set; }
        public string text { get; set; }
        public List<Action> actions { get; set; }
        public Action action { get; set; }
        

    }

    public class Defaultaction
    {
        public string type { get; set; }
        public string label { get; set; }
        public string uri { get; set; }
    }
}
