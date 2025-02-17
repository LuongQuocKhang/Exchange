﻿using System.Runtime.Serialization;

namespace Exchange.BuildingBlock.Exceptions;


/// <summary>
/// Represents errors related to an external integration or service.
/// </summary>
[Serializable]
public class IntegrationException : Exception
{
    public IntegrationException()
    {
    }

    public IntegrationException(string? message) : base(message)
    {
    }

    public IntegrationException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
