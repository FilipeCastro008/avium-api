namespace Domain.Common.Validation {

    public static class DomainValidator {

        #region Methods

        public static void Validator(params ValidationRule[] rules) {

            foreach (var rule in rules) {
                rule.Validate();
            }
        }

        #endregion
    }

    public class ValidationRule {

        #region Attributes

        private readonly bool _isInvalid;
        private readonly string _message;

        #endregion

        #region Constructors

        private ValidationRule(bool isInvalid, string message) {

            this._isInvalid = isInvalid;
            this._message = message;

        }

        #endregion

        #region Methods

        public void Validate() {

            if (_isInvalid) {
                throw new ArgumentException(_message);
            }
        }

        public static ValidationRule Required( string value, string fieldName) {
            return new(string.IsNullOrWhiteSpace(value), $"{fieldName} é obrigatório.");
        }

        public static ValidationRule MaxLength(string value, int maxLength, string fieldName) {
            return new(!string.IsNullOrWhiteSpace(value) && value.Length > maxLength, $"{fieldName} deve ter no máximo {maxLength} caracteres.");
        }

        public static ValidationRule ExactLength(string value, int length, string fieldName) {
            return new(!string.IsNullOrWhiteSpace(value) && value.Length != length, $"{fieldName} deve conter {length} caracteres.");
        }
           
        public static ValidationRule EnumDefined<TEnum>(TEnum value, string fieldName) where TEnum : struct, Enum {
            return new(!Enum.IsDefined(typeof(TEnum), value), $"{fieldName} inválido.");
        }

        public static ValidationRule BirthDate(DateTime date, int minAge, string fieldName) {
            var today = DateTime.UtcNow.Date;

            var age = today.Year - date.Year;

            if (date.Date > today.AddYears(-age))
                age--;

            return new(
                date == default || date > today || age < minAge,
                $"{fieldName} inválida. Idade mínima: {minAge} anos."
            );
        }

        public static ValidationRule Argon2idHash(string value, string fieldName) {

            var isInvalid = true;

            if(!string.IsNullOrWhiteSpace(value)) {

                var parts = value.Split("$", StringSplitOptions.RemoveEmptyEntries);

                isInvalid = parts[0] != "argon2id" || parts[1] != "v=19" || !parts[2].Contains("m=") || !parts[2].Contains("t=") ||
                    !parts[2].Contains("p=") || parts.Length != 5 || string.IsNullOrWhiteSpace(parts[3]) ||
                    string.IsNullOrWhiteSpace(parts[4]);

            }

            return new(isInvalid, $"Formato inválido do {fieldName}");
        }

        #endregion

    }
}
