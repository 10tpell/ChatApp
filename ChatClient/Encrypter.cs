using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp
{
    class Encrypter
    {
        string originalText = "";
        public string encryptedText = "";

        public Encrypter(string originalText)
        {
            this.originalText = originalText;
        }

        public string encrypt()
        {
            string text = this.originalText;
            string encryptedText;
            char[] charArray = new char[text.Length];
            for (int i=0; i < text.Length; i++)
            {
                int charValue = (int)text[i];
                charValue += i * i;
                charArray[i] = (char)charValue;
            }
            encryptedText = new string(charArray);
            this.encryptedText = encryptedText;
            return encryptedText;
        }
    }
}
