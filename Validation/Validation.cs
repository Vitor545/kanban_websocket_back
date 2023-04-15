using kanban_websocket_back.Interfaces.Login;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Text;

namespace kanban_websocket_back.Validation
{
    public class LoginValidation : ILoginValidation
    {
        public string NewEncrypt(string text, int key)
        {
            string result = "";
            string salt = "pFpsOyaekMRDTfreOQCAvlFLxo46yY_3vePGUemOGfv7-QIlFg_W4ZrQc75wYROi-94zvRN2f_DfHH1FwI_Jug";
            for (int i = 0; i < text.Length; i++)
            {
                result += (char)((int)text[i] + key);
            }
            var valueBytes = KeyDerivation.Pbkdf2(
                                password: result,
                                salt: Encoding.UTF8.GetBytes(salt),
                                prf: KeyDerivationPrf.HMACSHA512,
                                iterationCount: 10000,
                                numBytesRequested: 256 / 8);

            return Convert.ToBase64String(valueBytes);
        }

        public bool Validate(string value, int key, string hash)
            => NewEncrypt(value, key) == hash;
    }
}