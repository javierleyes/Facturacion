using System;

namespace Pagos.Domain.Base
{
    public interface IDomain<TKey> where TKey : IEquatable<TKey>
    {
        TKey Id { get; set; }
    }
}
