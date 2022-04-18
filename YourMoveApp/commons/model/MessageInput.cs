using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YourMoveApp.commons.model
{
    internal class MessageInput
    {
        public string RecepientId { get; }

        public string Title { get; }

        public string Text { get; }

        public object Attachment { get; }

        public MessageInput(string recepientId, string title, string text, object attachment)
        {
            this.RecepientId = recepientId;
            this.Title = title;
            this.Text = text;
            this.Attachment = attachment;
        }
    }
}
