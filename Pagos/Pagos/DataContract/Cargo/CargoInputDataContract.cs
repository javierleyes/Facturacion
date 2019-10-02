namespace Pagos.API.DataContract
{
    public class CargoInputDataContract
    {
        public long Cargo_Id { get; set; }
        public long User_Id { get; set; }
        public decimal Amount { get; set; }
        public decimal Balance { get; set; }
        public string State { get; set; }
        public string Type { get; set; }
    }
}
