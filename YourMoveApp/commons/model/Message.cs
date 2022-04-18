using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YourMoveApp.commons.model
{
    internal class Message
    {
        public string Id { get; }
        
        public string RecepientId { get; }

        public string Title { get; }
        
        public string Text { get; }

        public object Attachment { get; }

        public bool IsRead { 
            get {
                return _isRead;
            } 
        }

        bool _isRead;

        public Message(string id, string recepientId, string title, string text, object attachement)
        {
            this.Id = id;
            this.RecepientId = recepientId;
            this.Title = title;
            this.Text = text;
            this.Attachment = attachement;
            this._isRead = false;
        }

        public void MarkAsRead()
        {
            this._isRead = true;
        }
    }
}
