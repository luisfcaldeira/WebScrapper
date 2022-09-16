using Core.Infra.Services.Observers.Interfaces.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infra.Services.Observers.Entities.Messages
{
    public class Message : IMessage
    {
        public Tag Tag { get; private set; }
        public string Description { get; private set; }

        public Message(Tag tag, string description)
        {
            Tag = tag;
            Description = description ?? throw new ArgumentNullException(nameof(description));
        }
    }
}
