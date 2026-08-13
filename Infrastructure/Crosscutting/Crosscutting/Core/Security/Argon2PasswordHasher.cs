using System.Text;
using System.Security.Cryptography;

using Application.Interfaces.Security;

using Konscious.Security.Cryptography;

namespace Crosscutting.Common.Core.Security {
    public sealed class Argon2PasswordHasher: IPasswordHasherService {

        #region Constants 

        private const int SaltLength = 16;
        private const int HashLength = 32;
        private const int MemorySize = 19456; // Kib
        private const int Iterations = 2;
        private const int DegreeOfParallelism = 1;
        private const int Version = 19;

        #endregion

        #region Methods

        #region Implementation

        public string Hash(string password) {

            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Senha não pode ser vazia.", nameof(password));

            var salt = RandomNumberGenerator.GetBytes(SaltLength);
            var hash = GenerateHash(password, salt, MemorySize, Iterations, DegreeOfParallelism, HashLength);

            return string.Join(
                "$",
                "",
                "argon2id",
                $"v={Version}", $"m={MemorySize},t={Iterations},p={DegreeOfParallelism}",
                Convert.ToBase64String(salt), Convert.ToBase64String(hash)
            );
        }

        public bool Verify(string password, string passwordHash) {

            if (string.IsNullOrWhiteSpace(password))
                return false; //Tratar retorno no serviço

            if (string.IsNullOrWhiteSpace(passwordHash))
                return false; //Tratar retorno no serviço

            var parts = passwordHash.Split('$', StringSplitOptions.RemoveEmptyEntries);

            bool validation = PartsValidation(parts);
            if (!validation)
                throw new FormatException("Formato inválido do hash.");

            var parameters = ParseParameters(parts[2]);

            var salt = Convert.FromBase64String(parts[3]);
            var expectedHash = Convert.FromBase64String(parts[4]);

            var actualHash = GenerateHash(
                password, salt, parameters.MemorySize, parameters.Iterations,
                parameters.DegreeOfParallelism, expectedHash.Length
            );

            return CryptographicOperations.FixedTimeEquals(actualHash, expectedHash);
        }

        #endregion

        #region Helpers

        private static bool PartsValidation(string[] parts) {

            if (parts.Length != 5)
                return false;

            if (parts[0] != "argon2id")
                return false;

            if (parts[1] != $"v={Version}")
                return false;

            return true;

        }

        private static byte[] GenerateHash(string password, byte[] salt, int memorySize, int iterations, int degreeOfParallelism, int hashLength) {
            var passwordBytes = Encoding.UTF8.GetBytes(password);

            using var argon2 = new Argon2id(passwordBytes) {
                Salt = salt,
                MemorySize = memorySize,
                Iterations = iterations,
                DegreeOfParallelism = degreeOfParallelism,
            };

            return argon2.GetBytes(hashLength);
        }

        private static Argon2Parameters ParseParameters(string parametersText) {

            var parts = parametersText.Split(',');

            var memorySize = 0;
            var iterations = 0;
            var degreeOfParallelism = 0;

            foreach (var part in parts) {

                var keyValue = part.Split('=');
                if (keyValue.Length != 2)
                    throw new FormatException("Formato inválido dos parâmetros do Argon2");

                var key = keyValue[0];
                var value = int.Parse(keyValue[1]);

                switch (key) {

                    case "m":
                        memorySize = value;
                        break;

                    case "t":
                        iterations = value;
                        break;

                    case "p":
                        degreeOfParallelism = value;
                        break;
                }
            }

            if (memorySize <= 0 || iterations <= 0 || degreeOfParallelism <= 0)
                throw new FormatException("Parâmetros inválidos do Argon2");

            return new Argon2Parameters(memorySize, iterations, degreeOfParallelism);
        }

        #endregion

        #endregion

        private sealed record Argon2Parameters( int MemorySize, int Iterations, int DegreeOfParallelism);
    }
}
