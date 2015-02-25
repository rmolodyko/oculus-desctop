using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oculus
{
    public delegate void UserEventHandler(); // Пользовательский тип делегата

    public class Subject
    {
        public Dictionary<String,List<UserEventHandler>> events = new Dictionary<String,List<UserEventHandler>>();

        public void subscribe(String type, UserEventHandler ev){
            if (events != null)
            if (events.ContainsKey(type)) {
                events[type].Add(ev);
            }
            List<UserEventHandler> e = new List<UserEventHandler>();
            e.Add(ev);
            events.Add(type,e);
        }

        public void publish(String type)
        {
            if (events.ContainsKey(type))
            foreach (UserEventHandler i in events[type])
            {
                i();
            }
        }

        public void unsubscribe(String type) {
            if (events.ContainsKey(type))
                events.Remove(type);
        }
    }
}
