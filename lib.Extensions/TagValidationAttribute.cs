﻿using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace lib.Extensions;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class TagValidationAttribute : ValidationAttribute
{
    private readonly int _maxLength;
    private readonly string _pattern;

    public TagValidationAttribute(int maxLength, string pattern)
    {
        _maxLength = maxLength;
        _pattern = pattern;
    }

    public override bool IsValid(object value)
    {
        if (value is not IEnumerable<string> tags || tags.All(n => n == null)) return true;
        return tags.All(tag => tag.Length <= _maxLength && !Regex.IsMatch(tag, _pattern));
    }
}