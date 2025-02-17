﻿using FluentValidation.Results;

namespace Exchange.BuildingBlock.Exceptions;


/// <summary>
/// Represent various business rules violations.
/// </summary>
[Serializable]
public class BusinessRuleException : Exception
{
    public string ErrorCode { get; private set; } = string.Empty;

    public IEnumerable<ValidationFailure> ValidationFailures { get; private set; } = Enumerable.Empty<ValidationFailure>();

    public BusinessRuleException()
    {
    }

    public BusinessRuleException(string? message) : base(message)
    {
    }

    public BusinessRuleException(string errorCode, string message, IEnumerable<ValidationFailure> validationFailures) : base(message)
    {
        ErrorCode = errorCode;
        ValidationFailures = validationFailures;
    }

    public BusinessRuleException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
