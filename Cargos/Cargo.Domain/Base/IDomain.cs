using System;

namespace Cargos.Domain.Base
{
    public interface IDomain<TKey> where TKey : IEquatable<TKey>
    {
        TKey Id { get; set; }
    }
}
