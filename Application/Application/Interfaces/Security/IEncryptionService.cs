using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Security {
    public interface IEncryptionService {

        string Encrypt(string plainText);
        string Decrypt(string cipherText);

    }
}
