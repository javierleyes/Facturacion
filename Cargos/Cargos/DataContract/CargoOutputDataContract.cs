namespace Cargos.API.DataContract
{
    public class CargoOutputDataContract
    {
        public long User_Id { get; set; }
        public decimal Amount { get; set; }
        public decimal Balance { get; set; }
        public string State { get; set; }
        public string Type { get; set; }
    }
}
