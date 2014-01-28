using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QueueSimulator
{
    public class EventQueue // Singleton
    {

        private PriorityQueue<int,Event> events;

        private static readonly EventQueue instance = new EventQueue();
        private EventQueue() 
        {
            events = new PriorityQueue<int,Event>();
        }

        public static EventQueue Instance
        {
            get 
            {
                return instance;
            }
        }

        public void addEvent(int timeOfEvent, Event e)
        {
            events.Enqueue(timeOfEvent, e);
        }

        public void addEvent(Event e)
        {
            events.Enqueue(e.getTime(), e);
        }

        public Event getEvent()
        {
            return events.Dequeue();
        }

        public bool isQueueEmpty()
        {
            return events.IsEmpty;
        }
    }
}
