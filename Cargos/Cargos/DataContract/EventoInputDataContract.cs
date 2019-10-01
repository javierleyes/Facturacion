using System;

namespace Cargos.API.DataContract
{
    public class EventoInputDataContract
    {
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public long User_id { get; set; }
        public string Event_type { get; set; }
        public DateTime Date { get; set; }
    }
}
