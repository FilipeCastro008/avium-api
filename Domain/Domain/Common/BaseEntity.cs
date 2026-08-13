using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common {
    public abstract class BaseEntity {

        #region Atributtes

        public int Id { get; protected set; }
        private List<ValidationResult> _validationsErros = new List<ValidationResult>();

        #endregion

        #region Methods

        public bool IsValid() {
            this._validationsErros = new List<ValidationResult>();
            var context = new ValidationContext(this);
            return Validator.TryValidateObject(this, context, _validationsErros, true);
        }

        public List<ValidationResult> GetValidationResults() {
            return _validationsErros;
        }

        public bool ExistProperty(object obj, string property) {
            return obj.GetType().GetProperty(property) != null;
        }

        #endregion

    }
}
