using System.Collections.Generic;

namespace Common.Validation
{
    public class ValidationResult
    {
        private readonly string _validationOperationName;
        private readonly List<string> _errors;

        public bool HasErrors => _errors.Count > 0;

        public ValidationResult(string validationOperationName)
        {
            _validationOperationName = validationOperationName;
            _errors = new List<string>();
        }

        public void AddError(string message)
        {
            _errors.Add(message);
        }

        public string GetErrors()
        {
            return $"{_validationOperationName}: {string.Join(";", _errors)}";
        }
    }
}
