using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp
{
    class Decrypter
    {
        string encryptedText;
        public string decryptedText;

        public Decrypter(string encryptedText)
        {
            this.encryptedText = encryptedText;
        }

        public string decrypt()
        {
            string text = encryptedText;
            char[] charArray = new char[text.Length];
            for (int i=0; i < text.Length; i++)
            {
                int charValue = (int)text[i];
                charValue -= i * i;
                charArray[i] = (char)charValue;
            }
            string decryptedText = new string(charArray);
            this.decryptedText = decryptedText;
            return decryptedText;
        }
    }
}
