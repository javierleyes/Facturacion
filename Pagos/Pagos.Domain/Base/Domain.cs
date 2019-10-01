using System;

namespace Pagos.Domain.Base
{
    public class Domain<TKey> : IDomain<TKey> where TKey : IEquatable<TKey>
    {
        public TKey Id { get; set; }
    }
}
