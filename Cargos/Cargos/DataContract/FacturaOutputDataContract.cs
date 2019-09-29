namespace Cargos.API.DataContract
{
    public class FacturaOutputDataContract
    {
        public long User_Id { get; set; }
        //public IList<Cargo> Cargos { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
    }
}
