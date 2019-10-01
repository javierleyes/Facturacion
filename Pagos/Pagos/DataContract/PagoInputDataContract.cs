namespace Pagos.API.DataContract
{
    public class PagoInputDataContract
    {
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public long User_id { get; set; }
    }
}
