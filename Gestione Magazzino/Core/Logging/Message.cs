using System;

namespace Gestione_Magazzino.Core.Logging
{
    public class Message
    {
        public enum EventTypes
        {
            Info,
            Errore
        }

        public Message(EventTypes eventType, string text)
        {
            EventType = eventType;
            Text = text;
            DateTime = DateTime.Now;
        }

        public DateTime DateTime { get; }
        public EventTypes EventType { get; }
        public string Text { get; }

        public override string ToString()
        {
            return DateTime.Now + " [" + EventType + "] " + Text;
        }
    }
}