using System;

namespace RSA
{
    class BigInteger:IComparable<BigInteger>,IEquatable<BigInteger>
    {


        //====================
        //Your Code is Here:
        //===================
        /// <summary>
        /// TODO: Write an efficient algorithm that multiplies 2 BigIntegers (A and B)
        /// </summary>
        /// <param name="X">first Operand, Non-Negative BigInteger</param>
        /// <param name="Y">second Operand, Non-Negative BigInteger</param>
        /// <returns>BigInteger, The result of Multiplying A by B</returns>
        static BigInteger break_into2(BigInteger A, int side)
        {
            byte[] arr1;
            if (A.Number.Length % 2 != 0 && side == 1)
            {
                arr1 = new byte[(A.Number.Length / 2) + 1];
            }
            else
                arr1 = new byte[A.Number.Length / 2];

            if (side == 1)
            {
                for (long i = 0; i < arr1.Length; i++)
                {
                    arr1[i] = A.Number[i];
                }
            }
            else
            {
                long index = arr1.Length;
                if (A.Number.Length % 2 != 0)
                    index++;

                for (int j = 0; j < arr1.Length; j++)
                {
                    arr1[j] = A.Number[index];
                    index++;
                }
            }
            return new BigInteger(arr1);
        }


        public static BigInteger Multiply(BigInteger A, BigInteger B)
        {

            if (A.Number.Length <= 0 || B.Number.Length <= 0 || A.CompareTo(new BigInteger(0)) == 0 || B.CompareTo(new BigInteger(0)) == 0)
            {
                return new BigInteger(0);
            }
            if (A.CompareTo(new BigInteger(1)) == 0)
            {
                return B;
            }
            if (B.CompareTo(new BigInteger(1)) == 0)
            {
                return A;
            }

            int N = Math.Max(A.Number.Length / 2, B.Number.Length / 2);

            if (N <= 4 && N >= 0)
            {
                string b1 = A.Number_getter();
                string b2 = B.Number_getter();
                BigInteger d = new BigInteger(Convert.ToInt64(b1) * Convert.ToInt64(b2));
                return d;
            }
            else
            {
                if (A.Number.Length != B.Number.Length)
                {
                    if (A.Number.Length < B.Number.Length)
                    {
                        A = AddLeadingZeros(A, B.Number.Length - A.Number.Length);
                    }
                    else if (A.Number.Length > B.Number.Length)
                    {
                        B = AddLeadingZeros(B, A.Number.Length - B.Number.Length);
                    }
                }
                BigInteger x = break_into2(A, 1);
                BigInteger y = break_into2(A, 2);
                BigInteger w = break_into2(B, 1);
                BigInteger z = break_into2(B, 2);
                BigInteger M2 = x * w;
                BigInteger M1 = y * z;
                BigInteger sum1 = x + y;
                BigInteger sum2 = w + z;
                BigInteger M3 = sum1 * sum2;
                return PadWithZeros(M2, 2 * N) + PadWithZeros(M3 - M1 - M2, N) + M1;
            }
        }


        //====================
        //Your Code is Here:
        //===================
        /// <summary>
        /// TODO: Write an efficient algorithm that calculates div-mod
        /// The function should calculates the quotient (A / B) and the remainder (A mod B)
        /// </summary>
        /// <param name="A">the dividend, non-negative BigInteger</param>
        /// <param name="B">the divisor, Positive BigInteger</param>
        /// <returns> 
        /// A Tuple (pair of BigIntegers)
        /// The first Item is the quotient (A / B)
        /// The second Item is the remainder (A mod B)
        /// </returns>
        public static Tuple<BigInteger, BigInteger> DivMod(BigInteger A, BigInteger B)
        {
            if (A.CompareTo(B) == 0)
            {
                return new Tuple<BigInteger, BigInteger>(new BigInteger("1"), new BigInteger("0"));
            }
            if (A.CompareTo(B) == -1)
            {
                return new Tuple<BigInteger, BigInteger>(new BigInteger("0"), A);
            }
            Tuple<BigInteger, BigInteger> T = DivMod(A, B + B);

            BigInteger q = Add(T.Item1, T.Item1);

            T.Item1.Number = q.Number;

            if (T.Item2.CompareTo(B) == -1)
            {
                return new Tuple<BigInteger, BigInteger>(T.Item1, T.Item2);
            }
            else
            {
                return new Tuple<BigInteger, BigInteger>(T.Item1 + new BigInteger("1"), T.Item2 - B);
            }
        }

        //====================
        //Your Code is Here:
        //===================
        /// <summary>
        /// TODO: Write an efficient algorithm that calculates Mod of Power
        /// The function should calculates the result of the equation (B ^ P) mod M
        /// </summary>
        /// <param name="B">the base, non-negative BigInteger</param>
        /// <param name="P">the exponent, non-negative BigInteger</param>
        /// <param name="M">the modulus value, positive BigInteger</param>
        /// <returns>BigInteger, The result of (B ^ P) mod M</returns>
        static BigInteger cons = new BigInteger();
        public static BigInteger ModOfPower(BigInteger B, BigInteger P, BigInteger M)
        {
            if (P.CompareTo(new BigInteger(1)) == -1)
            {
                return new BigInteger(1);
            }
            if (P.CompareTo(new BigInteger(1)) == 0)
            {
                cons = DivMod(B, M).Item2;
                return cons;
            }
            else
            {
                if (Is_Even(P))
                {
                    BigInteger temp = ModOfPower(B, DivMod(P, new BigInteger(2)).Item1, M);

                    return DivMod(temp * temp, M).Item2;
                }
                else
                {
                    BigInteger temp = ModOfPower(B, DivMod(P, new BigInteger(2)).Item1, M);
                    return DivMod(temp * temp * cons, M).Item2;
                }
            }
        }

        #region "Constructors"
        /// <summary>
        /// Default Constructor
        /// Creates A BigInteger that has the value of zero
        /// </summary>
        public BigInteger()
        {
            Sign = false;
            Number = new byte[1] { 0 };
        }
        /// <summary>
        /// Creates a BigInteger from a numeric string
        /// </summary>
        /// <param name="number">Numeric string</param>
        public BigInteger(string number)
        {
            while (number[0] == '0' && number.Length != 1)
                number = number.Remove(0, 1);

            if (char.IsDigit(number[0]))
            {
                Number_setter(number);
                Sign = false;
            }
            else
            {
                if (number[0] == '-')
                {
                    Sign = true;
                    Number_setter(number.Substring(1));
                }
                else if (number[0] == '+')
                {
                    Sign = false;
                    Number_setter(number.Substring(1));
                }
                else
                {
                    Sign = false;
                    Number_setter(number);
                }
            }
        }
        /// <summary>
        /// Creates a BigInteger from a numeric String and a Sign
        /// </summary>
        /// <param name="number">Numeric string</param>
        /// <param name="sign">Boolean variable for the sign
        /// (sign = true for negatives, sign = false for positives)</param>
        public BigInteger(string number, bool sign)
        {
            while (number[0] == '0' && number.Length != 1)
                number = number.Remove(0, 1);
            Number_setter(number);
            Sign_setter(sign);
        }
        /// <summary>
        /// Creates a BigInteger form a byte array
        /// Each element in the array represents a digit in the number
        /// </summary>
        /// <param name="arr">byte array. Each digit represents a digit of the big number</param>
        public BigInteger(byte[] arr)
        {
            this.Number = arr;
        }
        /// <summary>
        /// Creates a BigInteger from a byte array and a sign
        /// Each element in the array represents a digit in the number
        /// </summary>
        /// <param name="arr">byte array. Each digit represents a digit of the big number</param>
        /// <param name="sign">Boolean variable for the sign
        /// (sign = true for negatives, sign = false for positives)</param>
        public BigInteger(byte[] arr, bool sign)
        {
            this.Number = arr;
            this.Sign = sign;
        }
        /// <summary>
        /// Creates a BigInteger from a variable with long data type
        /// </summary>
        /// <param name="number">the number whose value will be in the big number</param>
        public BigInteger(long number)
        {
            if (number < 0)
                Sign = true;
            else
                Sign = false;

            String s;
            s = number.ToString();

            Number_setter(s);
        }
        #endregion

        public void Number_setter(string number)
        {
            if (number[0] == '-')
            {
                Sign_setter(true);
                Number = System.Text.Encoding.UTF8.GetBytes(number.Substring(1));
            }
            else
                Number = System.Text.Encoding.UTF8.GetBytes(number);
            for(int i = 0; i < Number.Length; i++)
            {
                Number[i] = (byte)(Number[i] - '0');
            }
        }
        public void Sign_setter(bool sign)
        {
            Sign = sign;
        }
        public string Number_getter()
        {
            string ret = "";
            for(int i = 0; i < Number.Length; i++)
            {
                ret = ret + ((char)(Number[i] + '0'));
            }
            return ret;
        }
        public bool Sign_getter()
        {
            return Sign;
        }
        public int GetDigitsCount()
        {
            return Number.Length;
        }
        

        #region "Privates"

        private bool Sign;

        public byte[] Number { get; set; }
        #endregion

        /// <summary>
        /// Add two positive BigIntegers and returns the result
        /// </summary>
        /// <param name="A">Big Integer (should be positive)</param>
        /// <param name="B">Big Integer (should be positive)</param>
        /// <returns>Big Integer (the result of adding A and B)</returns>
        public static BigInteger Add(BigInteger A, BigInteger B)
        {
            A = RemoveLeadingZeros(A);
            B = RemoveLeadingZeros(B);
            if (B.Number.Length > A.Number.Length)
            {
                byte[] tmp = A.Number;
                A.Number = B.Number;
                B.Number = tmp;
            }

            B = BigInteger.AddLeadingZeros(B, A.GetDigitsCount() - B.GetDigitsCount());
            byte[] sum = new byte[A.Number.Length];

            byte carry = 0;


            int cntr = A.Number.Length - 1;
            for (int i = B.Number.Length - 1; i >= 0; i--)
            {
                byte tmp = (byte)(A.Number[cntr] + B.Number[i] + carry);
                if (tmp > 9)
                {
                    tmp -= 10;
                    carry = 1;
                }
                else
                    carry = 0;

                sum[cntr] = tmp;
                cntr--;
            }

            while (carry == 1)
            {
                if (cntr == -1)
                {
                    byte[] extendedSum = new byte[sum.Length + 1];
                    extendedSum[0] = 1;
                    for (int i = 1; i <= sum.Length; i++)
                    {
                        extendedSum[i] = sum[i - 1];
                    }
                    return new BigInteger(extendedSum);
                }


                byte tmp = (byte)(sum[cntr] + 1);
                if (tmp > 9)
                {
                    tmp -= 10;
                    carry = 1;
                }
                else
                    carry = 0;

                sum[cntr] = tmp;
                cntr--;
            }


            return new BigInteger(sum);

        }

        /// <summary>
        /// Subtracts B from A, A and B are positive BigIntegers
        /// </summary>
        /// <param name="A">First Operand</param>
        /// <param name="B">Second Operand</param>
        /// <returns>Subtraction Result as a BigInteger (It may be negative)</returns>
        public static BigInteger Subtract(BigInteger A, BigInteger B)
        {

            bool swap = false;
            A = RemoveLeadingZeros(A);
            B = RemoveLeadingZeros(B);
            if (A.CompareTo(B) < 0)
            {
                BigInteger tmp = A;
                A = B;
                B = tmp;
                swap = true;
            }
            else if (A.Equals(B) == true)
            {
                return new BigInteger();
            }

            if (A.Number.Length > B.Number.Length)
            {
                B = AddLeadingZeros(B, A.Number.Length - B.Number.Length);
            }
            else
            {
                A = AddLeadingZeros(A, B.Number.Length - A.Number.Length);
            }

            BigInteger res = new BigInteger(A.Number);


            for (int i = A.Number.Length - 1; i >= 0; i--)
            {
                if (A.Number[i] < B.Number[i])
                {
                    int j = i - 1;
                    for(; j >= 0; j--)
                    {
                        if(A.Number[j] > 0)
                        {
                            A.Number[j]--;
                            j++;
                            break;
                        }
                    }
                    while(j < i)
                    {
                        A.Number[j] = 9;
                        j++;
                    }
                    A.Number[j] += 10;
                    
                }
                
                 res.Number[i] = (byte)(A.Number[i] - B.Number[i]);
                
            }

            res = RemoveLeadingZeros(res);

            if (swap)
                res.Sign = true;


            return res;
        }

        #region "Operators"
        public static BigInteger operator -(BigInteger A, BigInteger B)
        {
            BigInteger result = Subtract(A, B);
            return result;
        }
        public static BigInteger operator +(BigInteger A, BigInteger B)
        {
            return Add(A, B);
        }
        public static BigInteger operator *(BigInteger A, BigInteger B)
        {
            return Multiply(A, B);
        }
        #endregion

        #region Static Data Members
        /// <summary>
        /// a big integer with value equal to zero
        /// to be used as a helper object during the implementation (e.g. to compare bigintegers with it)
        /// </summary>
        public static BigInteger zero = new BigInteger();
        /// <summary>
        /// a big integer with value equal to one
        /// to be used as a helper object during the implementation (e.g. to add it to another BigInteger)
        /// </summary>
        public static BigInteger one = new BigInteger(new byte[] { 1 });
        /// <summary>
        /// a big integer with value equal to two
        /// to be used as a helper object during the implementation (e.g. to multiply it with another bigInteger)
        /// </summary>
        public static BigInteger two = new BigInteger(new byte[] { 2 });
        #endregion

        /// <summary>
        /// Calculates the parity of the big integer (is it odd or even?)
        /// </summary>
        /// <param name="bigInteger">the big integer</param>
        /// <returns>
        /// true if the integer is even.
        /// false if the integer is odd.
        /// </returns>
        public static bool Is_Even(BigInteger bigInteger)
        {
            return bigInteger.Number[bigInteger.Number.Length - 1] % 2 == 0;
        }

        /// <summary>
        /// Check if the big integer equals zero or not
        /// </summary>
        /// <param name="bigInteger">a big integer</param>
        /// <returns>
        /// true if the big integer is zero
        /// false otherwise
        /// </returns>
        public static bool Is_Zero(BigInteger bigInteger)
        {
            for(int i = 0; i < bigInteger.Number.Length; i++)
            {
                if(bigInteger.Number[i] != 0)
                {
                    return false;
                }
            }

            return true;
        }

        public void Display_Biginteger()
        {
            if (Sign_getter())
                Console.Write('-');
            Console.WriteLine(Number_getter());
        }

        /// <summary>
        /// pad the big integer with zeros (to the right)
        /// for example: if A = 29398, numberOfZeros = 5
        /// the funciton should return : 2939800000
        /// </summary>
        /// <param name="A">big integer</param>
        /// <param name="numberOfZeros">the number of zeros to pad</param>
        /// <returns>the big integer after padding it with zeros</returns>
        public static BigInteger PadWithZeros(BigInteger A, int numberOfZeros)
        {
            byte[] arr = new byte[A.Number.Length + numberOfZeros];
            for (int i = 0; i < arr.Length; i++)
            {
                if(i < A.Number.Length)
                {
                    arr[i] = A.Number[i];
                }
                else
                {
                    arr[i] = 0;
                }
            }

            return new BigInteger(arr);
        }

        /// <summary>
        /// add trailing zeros to the big integer (to the left)
        /// for example: if A = 29398, numberOfZeros = 5
        /// the funciton should return a big integer with the value: 0000029398
        /// </summary>
        /// <param name="A">big integer</param>
        /// <param name="numberOfZeros">the number of zeros to add</param>
        /// <returns>the big integer after adding trailing zeros</returns>
        public static BigInteger AddLeadingZeros(BigInteger A, int numberOfZeros)
        {
            byte[] arr = new byte[A.Number.Length + numberOfZeros];
            for (int i = 0; i < arr.Length; i++)
            {
                if (i < numberOfZeros)
                {
                    arr[i] = 0;
                }
                else
                {
                    arr[i] = A.Number[i - numberOfZeros];
                }
            }

            return new BigInteger(arr);
        }

        /// <summary>
        /// remove trailing zeros from the big integer
        /// for example: if A = 0000293
        /// the funciton should return 293
        /// </summary>
        /// <param name="A">a big integer, to remove the trailing zeros from</param>
        /// <returns>the number after removing trailing zeros</returns>
        public static BigInteger RemoveLeadingZeros(BigInteger A)
        {
            BigInteger res;
            int firstNonZeroIndex = -1;
            for (int i = 0; i < A.Number.Length; i++)
            {
                if (A.Number[i] != 0)
                {
                    firstNonZeroIndex = i;
                    break;
                }
            }

            if (firstNonZeroIndex == -1) // the number is zero
            {
                res = new BigInteger();
            }
            else
            {
                byte[] arr = new byte[A.Number.Length - firstNonZeroIndex];
                for (int i = firstNonZeroIndex; i < A.Number.Length; i++)
                {
                    arr[i - firstNonZeroIndex] = A.Number[i];
                }
                res = new BigInteger(arr);
            }

            return res;
        }

        /// <summary>
        /// Compare the instance of BigInteger with other Biginteger
        /// </summary>
        /// <param name="other">another BigInteger, to compare the current BigInteger with</param>
        /// <returns>
        /// -1 if this instance is less than other
        /// 0 if this instance is equal to other
        /// 1 if this instance is greater than other
        /// </returns>
        public int CompareTo(BigInteger other)
        {
            BigInteger A = RemoveLeadingZeros(this);
            this.Number = A.Number;
            other = RemoveLeadingZeros(other);
            if (other.Sign == true && this.Sign == true)
            {
                if (this.Number.Length < other.Number.Length)
                {
                    return 1;
                }
                else if (this.Number.Length > other.Number.Length)
                {
                    return -1;
                }
                else
                {
                    for (int i = 0; i < this.Number.Length; i++)
                    {
                        if (this.Number[i] < other.Number[i])
                        {
                            return 1;
                        }
                        else if (this.Number[i] > other.Number[i])
                        {
                            return -1;
                        }
                    }

                    return 0;
                }
            }
            else if (other.Sign == true && this.Sign == false)
            {
                return 1;
            }
            else if (other.Sign == false && this.Sign == true)
            {
                return -1;
            }
            else // both are false (positive)
            {
                if(this.Number.Length < other.Number.Length)
                {
                    return -1;
                }
                else if(this.Number.Length > other.Number.Length)
                {
                    return 1;
                }
                else
                {
                    for(int i = 0; i < this.Number.Length; i++)
                    {
                        if(this.Number[i] < other.Number[i])
                        {
                            return -1;
                        }
                        else if (this.Number[i] > other.Number[i])
                        {
                            return 1;
                        }
                    }

                    return 0;
                }
            }
        }

        /// <summary>
        /// Check if the instance of BigInteger and other BigInteger are equal or not
        /// </summary>
        /// <param name="other">another BigInteger, to check the equality of it with current BigInteger</param>
        /// <returns>
        /// true if this instanc is equal to other
        /// false if they are not equal
        /// </returns>
        public bool Equals(BigInteger other)
        {
            Number = RemoveLeadingZeros(new BigInteger(Number)).Number;
            other = RemoveLeadingZeros(other);
            if(Sign != other.Sign || other.Number.Length != Number.Length)
            {
                return false;
            }
            else
            {
                for(int i = 0; i < Number.Length; i++)
                {
                    if(Number[i] != other.Number[i])
                    {
                        return false;
                    }
                }

                return true;
            }
        }
    }
}
