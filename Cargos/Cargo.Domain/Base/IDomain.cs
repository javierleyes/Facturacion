using System;

namespace Cargo.Domain.Base
{
    public interface IDomain<TKey> where TKey : IEquatable<TKey>
    {
        TKey Id { get; set; }
    }
}
