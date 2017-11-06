using System.Collections.Generic;

namespace Common.Validation
{
    public class ValidationResult
    {
        private readonly string _validationOperationName;
        public readonly List<string> Errors;

        public bool HasErrors => Errors.Count > 0;

        public ValidationResult(string validationOperationName)
        {
            _validationOperationName = validationOperationName;
            Errors = new List<string>();
        }

        public void AddError(string message)
        {
            Errors.Add(message);
        }

        public string GetErrors()
        {
            return $"{_validationOperationName}: {string.Join(";", Errors)}";
        }
    }
}
