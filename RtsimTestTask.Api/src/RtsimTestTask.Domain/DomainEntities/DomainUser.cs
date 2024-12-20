﻿namespace RtsimTestTask.Domain.DomainEntities;

public record DomainUser(
    Guid Id,
    DateTime CreatedAt,
    string Username,
    string Position,
    Guid OrganizationId,
    string? Email = null,
    string? PhoneNumber = null
) : DomainEntity<Guid>(Id, CreatedAt);