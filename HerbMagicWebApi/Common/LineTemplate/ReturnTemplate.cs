using HerbMagicWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HerbMagicWebApi.Common.LineTemplate
{
    public class ReturnTemplate
    {
        /// <summary>
        /// https://developers.line.biz/media/messaging-api/sticker_list.pdf
        /// </summary>
        /// <param name="ls"></param>
        /// <returns></returns>
        public List<ReplyMessage> text(List<string> ls)
        {
            var lrm = new List<ReplyMessage>();

            foreach (var msg in ls)
            {
                lrm.Add(new ReplyMessage()
                {
                    type = "text",
                    text = msg,
                });
            }
            return lrm;
        }
        public List<ReplyMessage> image(List<string> ls)
        {

            var lrm = new List<ReplyMessage>();

            foreach (var msg in ls)
            {
                lrm.Add(new ReplyMessage() { type = "image", previewImageUrl = msg+"t.png", originalContentUrl = msg + ".png"
                      }
              );
            }
            return lrm;
        }
        public List<ReplyMessage> carousel(string altText, List<List<Column>> llc)
        {


            var lrm = new List<ReplyMessage>();
            foreach (var lc in llc)
            {
                var rm = new ReplyMessage();
                rm.altText = altText;
                rm.type = "template";

                var lcolumn = new List<Column>();
                foreach (var c in lc)
                {
                    lcolumn.Add(c);

                }
                rm.template = new Template()
                {
                    type = "carousel",
                    columns = lcolumn
                };
                lrm.Add(rm);
            }
            return lrm;
        }


        public List<ReplyMessage> imageCarousel(string altText, List<List<Column>> llc)
        {


            var lrm = new List<ReplyMessage>();
            foreach (var lc in llc)
            {
                var rm = new ReplyMessage();
                rm.altText = altText;
                rm.type = "template";

                var lcolumn = new List<Column>();
                foreach (var c in lc)
                {
                    lcolumn.Add(c);

                }
                rm.template = new Template()
                {
                    type = "image_carousel",
                    columns = lcolumn
                };
                lrm.Add(rm);
            }
            return lrm;
        }



        public List<ReplyMessage> bubble()
        {


            var lrm = new List<ReplyMessage>();
           // foreach (var lc in llc)
            {
                var rm = new ReplyMessage();
                rm.type = "flex";
                rm.altText = "test";
                //rm.styles = new Styles()
                //{
                //    header = new Header() { backgroundColor = "#ffaaaa" },
                //    body = new Body() { backgroundColor = "#aaffaa" },
                //    footer = new Footer() { backgroundColor = "#aaaaff" },
                //};
                rm.header = new Header2
                {
                    type = "box",
                    layout = "vertical",
                    contents = new List<Content>() {
                     new Content() { type="text", text="header"} }
                };
                rm.hero = new Hero()
                {
                    type = "image",
                    //aspectRatio = "2:1",
                    //size = "full",
                    url = "https://i.imgur.com/wEl3Uvz.png"
                };
                rm.body = new Body2()
                {
                    type = "box",
                    layout = "vertical",
                    contents = new List<Content2>() {
                     new Content2() { type="text", text="body"} }
                };
                rm.footer = new Footer2()
                {
                    type = "box",
                    layout = "vertical",
                    contents = new List<Content3>() {
                     new Content3() { type="text", text="footer"} }
                };

               
                lrm.Add(rm);
            }
            return lrm;
        }
        public List<ReplyMessage> sticker(List<string> ls)
        {
            //"325708"
            var lrm = new List<ReplyMessage>();

            foreach (var msg in ls)
            {
                lrm.Add(new ReplyMessage()
                {
                    type = "sticker",
                    id = msg,
                    packageId = "1",
                    stickerId = "1"
                });
            }
            return lrm;
        }
        public List<ReplyMessage> button(string altText, string title, string text, string thumbnailImageUrl, List<List<Models.Action>> lla)
        {

            var lrm = new List<ReplyMessage>();
            foreach (var lc in lla)
            {
                var rm = new ReplyMessage();
                rm.altText = altText;
                rm.type = "template";

                var lcolumn = new List<Models.Action>();
                foreach (var c in lc)
                {
                    lcolumn.Add(c);

                }
                rm.template = new Template()
                {
                    type = "buttons",
                    thumbnailImageUrl = thumbnailImageUrl,
                    imageSize = "cover",
                    imageBackgroundColor = "#FFFFFF",
                    title = title,
                    text = text,
                    actions = lcolumn
                };
                lrm.Add(rm);
            }
            return lrm;


        }
        public List<ReplyMessage> location(List<ReplyMessage> ls)
        {
            var lrm = new List<ReplyMessage>();

            foreach (var msg in ls)
            {
                lrm.Add(new ReplyMessage()
                {
                    type = "location",
                    id = msg.id,
                    title = msg.title,
                    address = msg.address,
                    latitude = msg.latitude,
                    longitude = msg.longitude,
                });
            }
            return lrm;
        }
        public List<ReplyMessage> video(List<ReplyMessage> ls)
        {
            var lrm = new List<ReplyMessage>();

            foreach (var msg in ls)
            {
                lrm.Add(new ReplyMessage()
                {
                    type = "video",
                    duration = msg.duration,
                    id = msg.id,
                    contentProvider = new ContentProvider()
                    {
                        type = "external",
                        originalContentUrl = "",
                        previewImageUrl = ""

                    },
                });
            }
            return lrm;
        }

    }
}