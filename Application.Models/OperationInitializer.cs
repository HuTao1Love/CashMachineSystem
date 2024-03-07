namespace Application.Models;

public record OperationInitializer(
    DateTime DateTime,
    long AccountId,
    long Money);