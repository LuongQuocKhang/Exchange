using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Messages.Events
{
    public class IntegrationBaseEvent
    {

        public IntegrationBaseEvent()
        {
            Id = Guid.NewGuid();
            creationDate = DateTime.UtcNow;
        }

        public IntegrationBaseEvent(Guid id, DateTime createDate)
        {
            Id = id;
            creationDate = createDate;
        }

        public Guid Id { get; private set; }
        public DateTime creationDate { get; private set; }
    }
}
