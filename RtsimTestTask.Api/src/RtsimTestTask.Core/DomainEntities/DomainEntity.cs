namespace RtsimTestTask.Core.DomainEntities;

public abstract record DomainEntity<TKey>(TKey Id, DateTime CreatedAt);