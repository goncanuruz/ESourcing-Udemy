using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBusRabbitMQ.Events.Interfaces
{
    //ctor birkaç işlem yapabilmek için abstract class
    public abstract class IEvent
    {
        public Guid RequestId { get;private init; }
        public DateTime CreationDate { get; private init; }

        public IEvent()
        {
            RequestId = Guid.NewGuid();
            CreationDate = DateTime.UtcNow;
        }
    }
}
