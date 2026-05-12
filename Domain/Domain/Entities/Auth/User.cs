using Domain.Common;
using Domain.Common.Validation;
using Domain.Enums;

namespace Domain.Entities.Auth {
    public class User: BaseEntity {

        #region Attributes

        public string Name { get; private set; } 
        public string Email { get; private set; } 
        public SexEnm Sex { get; private set; }
        public DateTime DateBirth { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public string PasswordHash { get; private set; }
        public RoleEnm Role { get; private set; }    
        public UserLevelEnm UserLevel { get; private set; }  
        public DateTime CreatedAt { get; private set; } 
        public DateTime UpdatedAt { get; private set; }

        #endregion

        #region Constructors

        //EF
        private User() { }

        //Para o domínio
        public User(string name, string email, SexEnm sex, DateTime dateBirth, string city, 
            string state,string passwordHash, RoleEnm role, UserLevelEnm userLevel) {

            DomainValidator.Validator(
                ValidationRule.Required(name, "Name"),
                ValidationRule.Required(email, "Email"),
                ValidationRule.Required(city, "Cidade"),
                ValidationRule.Required(state, "Estado"),
                ValidationRule.Required(passwordHash, "Password Hash"),

                ValidationRule.EnumDefined(role, "Usuário"),
                ValidationRule.EnumDefined(userLevel, "Level do usuário"),
                ValidationRule.EnumDefined(sex, "Sexo do usuário"),

                ValidationRule.BirthDate(dateBirth, 15, "Idade"),

                ValidationRule.Argon2idHash(passwordHash, "Password Hash")
            );

            this.Name = name.Trim();
            this.Email = email.Trim().ToLower();
            this.Sex = sex;
            this.DateBirth = dateBirth; 
            this.City = city.Trim();   
            this.State = state.Trim().ToUpper();
            this.PasswordHash = passwordHash;
            this.Role = role;
            this.UserLevel = userLevel;
            this.CreatedAt = DateTime.UtcNow.AddHours(-3);
            this.UpdatedAt = DateTime.UtcNow.AddHours(-3);
        }

        #endregion

    }
}
