// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace CarbonBlazor
{
    /// <summary>
    /// A base class for form input components. This base class automatically
    /// integrates with an <see cref="Forms.EditContext"/>, which must be supplied
    /// as a cascading parameter.
    /// </summary>
    public abstract class InputBase<TValue> : BaseComponent, IDisposable
    {
        private readonly EventHandler<ValidationStateChangedEventArgs> _validationStateChangedHandler;
        private bool _previousParsingAttemptFailed;
        private ValidationMessageStore? _parsingValidationMessages;
        private Type? _nullableUnderlyingType;

        /// <summary>
        /// Gets or sets a collection of additional attributes that will be applied to the created element.
        /// </summary>
        [Parameter(CaptureUnmatchedValues = true)] 
        public IReadOnlyDictionary<string, object>? AdditionalAttributes { get; set; }

        private TValue _property;

        /// <summary>
        /// Gets or sets the value of the input. This should be used with two-way binding.
        /// </summary>
        /// <example>
        /// @bind-Value="model.PropertyName"
        /// </example>
        [Parameter]
        public TValue? Property
        {
            get { return _property; }
            set
            {
                var hasChanged = !EqualityComparer<TValue>.Default.Equals(value, Property);
                if (hasChanged)
                {
                    _property = value;
                    OnValueChange(value);
                }
            }
        }

        /// <summary>
        /// Gets or sets a callback that updates the bound value.
        /// </summary>
        [Parameter] 
        public EventCallback<TValue> PropertyChanged { get; set; }

        /// <summary>
        /// Gets the associated <see cref="Forms.EditContext"/>.
        /// </summary>
        protected EditContext EditContext { get; set; } = default!;

        /// <summary>
        /// Gets the <see cref="FieldIdentifier"/> for the bound value.
        /// </summary>
        protected internal FieldIdentifier FieldIdentifier { get; set; }

        /// <summary>
        /// Gets or sets the current value of the input.
        /// </summary>
        protected TValue? CurrentValue
        {
            get => Property;
            set
            {
                var hasChanged = !EqualityComparer<TValue>.Default.Equals(value, Property);

                if (hasChanged)
                {
                    Property = value;
                    PropertyChanged.InvokeAsync(Property);
                }
            }
        }

        /// <summary>
        /// Gets or sets the current value of the input, represented as a string.
        /// </summary>
        protected string? CurrentValueAsString
        {
            get => FormatValueAsString(CurrentValue);
            set
            {
                _parsingValidationMessages?.Clear();

                bool parsingFailed;

                if (_nullableUnderlyingType != null && string.IsNullOrEmpty(value))
                {
                    // Assume if it's a nullable type, null/empty inputs should correspond to default(T)
                    // Then all subclasses get nullable support almost automatically (they just have to
                    // not reject Nullable<T> based on the type itself).
                    parsingFailed = false;
                    CurrentValue = default!;
                }
                else if (TryParseValueFromString(value, out var parsedValue, out var validationErrorMessage))
                {
                    parsingFailed = false;
                    CurrentValue = parsedValue!;
                }
                else
                {
                    parsingFailed = true;

                    if (_parsingValidationMessages == null)
                    {
                        _parsingValidationMessages = new ValidationMessageStore(EditContext);
                    }

                    _parsingValidationMessages.Add(FieldIdentifier, validationErrorMessage);

                    // Since we're not writing to CurrentValue, we'll need to notify about modification from here
                    EditContext.NotifyFieldChanged(FieldIdentifier);
                }

                // We can skip the validation notification if we were previously valid and still are
                if (parsingFailed || _previousParsingAttemptFailed)
                {
                    EditContext.NotifyValidationStateChanged();
                    _previousParsingAttemptFailed = parsingFailed;
                }
            }
        }

        /// <summary>
        /// Formats the value as a string. Derived classes can override this to determine the formating used for <see cref="CurrentValueAsString"/>.
        /// </summary>
        /// <param name="value">The value to format.</param>
        /// <returns>A string representation of the value.</returns>
        protected virtual string? FormatValueAsString(TValue? value) => value?.ToString();

        protected virtual void OnValueChange(TValue value)
        {
            Console.WriteLine(value);
            CurrentValue = value;
        }

        /// <summary>
        /// Parses a string to create an instance of <typeparamref name="TValue"/>. Derived classes can override this to change how
        /// <see cref="CurrentValueAsString"/> interprets incoming values.
        /// </summary>
        /// <param name="value">The string value to be parsed.</param>
        /// <param name="result">An instance of <typeparamref name="TValue"/>.</param>
        /// <param name="validationErrorMessage">If the value could not be parsed, provides a validation error message.</param>
        /// <returns>True if the value could be parsed; otherwise false.</returns>
        protected virtual bool TryParseValueFromString(string value, out TValue result, out string validationErrorMessage)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                result = default;
                validationErrorMessage = null;
                return true;
            }

            var success = BindConverter.TryConvertTo<TValue>(
               value, CultureInfo.CurrentCulture, out var parsedValue);

            if (success)
            {
                result = parsedValue;
                validationErrorMessage = null;

                return true;
            }
            else
            {
                result = default;
                validationErrorMessage = $"{FieldIdentifier.FieldName} field isn't valid.";

                return false;
            }
        }

        /// <inheritdoc/>
        protected virtual void Dispose(bool disposing) {}

        void IDisposable.Dispose()
        {
            EditContext.OnValidationStateChanged -= _validationStateChangedHandler;
            Dispose(disposing: true);
        }
    }
}