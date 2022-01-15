using System;
using System.Text;

namespace RSA
{
    class RSA
    {
        public static BigInteger Encrypt(BigInteger M, BigInteger E, BigInteger N)
        {
            return BigInteger.ModOfPower(M, E, N);
        }

        public static BigInteger Decrypt(BigInteger EofM, BigInteger D, BigInteger N)
        {
            return BigInteger.ModOfPower(EofM, D, N);
        }

        //====================
        // *BONUS* Your Code is Here:
        //===================
        /// <summary>
        /// TODO: *BONUS* Adjust RSA to work with strings rather than integers. Note that string size is unknown
        /// This function takes a string to encrypt(along with required parameters for RSA encryption algorithm)
        /// The function should return the encrypted message as string
        /// </summary>
        /// <param name="M">a string, the text message to encrypt</param>
        /// <param name="E">a big integer, the public key</param>
        /// <param name="N">a big integer, the modulus</param>
        /// <returns>a string, the ecrypted message</returns>




        static int size;
        static int[] arr;
        public static string EncryptText(string M, BigInteger E, BigInteger N)
        {
            string y = "";
            string result = "";
            byte[] asciiBytes = Encoding.ASCII.GetBytes(M);
            size = asciiBytes.Length;
            arr = new int[size];
            int i = 0;
            foreach (byte b in asciiBytes)
            {
                arr[i]=b.ToString().Length;
                i++;
                y += b;
                if (i%42==0 ||i==size)
                {
                    BigInteger Mnew = new BigInteger(y);
                    result += BigInteger.ModOfPower(Mnew, E, N).Number_getter();
                    result += ' ';
                    y = "";
                }
            }
            return result;
        }


        //====================
        // *BONUS* Your Code is Here:
        //===================
        /// <summary>
        /// TODO: *BONUS* Adjust RSA to work with strings rather than integers. Note that string size is unknown
        /// This function takes a string to decrypt (along with required parameters for RSA encryption algorithm)
        /// The function should return the decrypted message as string
        /// </summary>
        /// <param name="EofM">a string, the encrypted message to decrypt</param>
        /// <param name="D">a big integer, the private key</param>
        /// <param name="N">a big integer, the modulus</param>
        /// <returns>a string, the decrypted message</returns>
        
        static string decodestring(string x)
        {
            string strAlpha="";
            int index=-1;
            int i = 0;
            while(i < x.Length) { 

                string y="";
                index++;

                for (int j = 0; j < arr[index]; j++)
                {
                    y += x[i];
                    i++;
                }

                int r = Convert.ToInt32(y);
                strAlpha += ((char)r).ToString();
            }
            return strAlpha;
        }

        public static string DecryptText(string EofM, BigInteger D, BigInteger N)
        {
            string y = "";
            string d = "";
         
            for (int i = 0; i < EofM.Length; i++)
            {
                if (EofM[i] == ' ')
                {
                    BigInteger Mnew = new BigInteger(y);
                    d += BigInteger.ModOfPower(Mnew, D, N).Number_getter();
                    y = "";
                }
                else
                    y += EofM[i];
            }
            string res = decodestring(d);

            return res;
        }
    }
}
