﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HerbMagicWebApi.Models
{
    public class WebhookModel
    {
        public List<Event> events { get; set; }
    }

    public class Event
    {
        public string replyToken { get; set; }
        public string type { get; set; }
        public long timestamp { get; set; }
        public Source source { get; set; }
        public Message message { get; set; }
        public Postback postback { get; set; }
        public Members joined { get; set; }
        public Members left { get; set; }
        public Members lefted { get; set; }

    }

    
    public class Members
    {
        public List<Source> members { get; set; }
    }
    
    public class Postback
    {
        public string data { get; set; }
    }

    public class Source
    {
        public string type { get; set; }
        public string userId { get; set; }
        public string groupId { get; set; }
        public string roomId { get; set; }
    }

    public class Message
    {
        public string id { get; set; }
        public string type { get; set; }
        public string text { get; set; }
        public string title { get; set; }
        public string address { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public string packageId { get; set; }
        public string stickerId { get; set; }
    }
}