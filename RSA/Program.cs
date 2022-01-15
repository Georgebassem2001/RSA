using System;
using System.IO;


namespace RSA
{
    class Program
    {
        #region static variables
        static StreamReader sr;
        static int testCasesCount;
        static BigInteger n1;
        static BigInteger n2;
        static BigInteger B;
        static BigInteger P;
        static BigInteger M;
        static BigInteger mOrEm;
        static BigInteger eOrD;
        static BigInteger e;
        static BigInteger d;
        static BigInteger N;
        static BigInteger expectedResult;
        static BigInteger actualResult;
        static Tuple<BigInteger, BigInteger> divModExpectedResult;
        static Tuple<BigInteger, BigInteger> divModActualResult;

        static bool isCorrect = true;
        #endregion

        #region IO
        static void Main(string[] args)
        {
            do
            {
                Console.WriteLine("RSA Task:\n[1] Multiplication\n[2] Div-Mod\n[3] Mod of Power\n[4] RSA Encryption\n[5] Text RSA Encryption(BONUS)");
                Console.Write("\nEnter your choice [1-4]: ");
                char choice = (char)Console.ReadLine()[0];
                isCorrect = true;
                switch (choice)
                {
                    case '1':
                        choice = ReadSampleOrComplete("Multiplication");
                        if (choice == '1')
                        {
                            sr = new StreamReader("MultiplicationSample.txt");
                            testCasesCount = int.Parse(sr.ReadLine());
                            for (int i = 1; i <= testCasesCount; i++)
                            {
                                n1 = new BigInteger(sr.ReadLine());
                                n2 = new BigInteger(sr.ReadLine());
                                expectedResult = new BigInteger(sr.ReadLine());
                                actualResult = BigInteger.Multiply(n1, n2);
                                Console.WriteLine("Case " + i + ": ");
                                Console.WriteLine("a = " + n1.Number_getter() + ", b = " + n2.Number_getter());
                                if (expectedResult.Equals(actualResult) == false)
                                {
                                    Console.WriteLine("Wrong Answer. \nYour output = " + actualResult.Number_getter() + "\nExpected Output = " + expectedResult.Number_getter());
                                    isCorrect = false;
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Correct! \nYour output = " + actualResult.Number_getter() + "\nExpected Output = " + expectedResult.Number_getter());
                                }
                            }

                            if (isCorrect)
                            {
                                Console.Write("Sample tests are correct. Do you want to run Complete tests [y/n]");
                                char ch = Console.ReadLine()[0];
                                if (ch == 'y' || ch == 'Y')
                                {
                                    choice = '2';
                                }
                            }
                        }
                        if (choice == '2')
                        {
                            sr = new StreamReader("MultiplicationComplete.txt");
                            int timeStampBefore = System.Environment.TickCount;
                            testCasesCount = int.Parse(sr.ReadLine());
                            for (int i = 1; i <= testCasesCount; i++)
                            {
                                n1 = new BigInteger(sr.ReadLine());
                                n2 = new BigInteger(sr.ReadLine());
                                expectedResult = new BigInteger(sr.ReadLine());
                                actualResult = BigInteger.Multiply(n1, n2);
                                int timeAfterTestCase = System.Environment.TickCount;
                                if (expectedResult.Equals(actualResult) == false)
                                {
                                    Console.WriteLine("\nWrong Answer in test case #" + i);
                                    isCorrect = false;
                                    break;
                                }
                                else
                                {
                                    Console.Write("\rCurrent Test Case# {0} ... {1} % Completed", i, (i - 1) * 100 / testCasesCount);
                                }
                            }

                            if (isCorrect)
                            {
                                Console.Write("\rCurrent Test Case# {0} ... {1} % Completed\n", testCasesCount, 100);
                                Console.WriteLine("Congratulations !! Your BigInteger multiplication worked correctly on the complete tests (Y)");
                                int totalTime = System.Environment.TickCount - timeStampBefore;
                                Console.Write("\rTotal Time = {0} ms", totalTime);
                                
                            }
                        }
                        break;
                    case '2':
                        choice = ReadSampleOrComplete("Div-Mod");
                        if (choice == '1')
                        {
                            sr = new StreamReader("DivModSample.txt");
                            testCasesCount = int.Parse(sr.ReadLine());
                            for (int i = 1; isCorrect && i <= testCasesCount; i++)
                            {
                                n1 = new BigInteger(sr.ReadLine());
                                n2 = new BigInteger(sr.ReadLine());
                                BigInteger q = new BigInteger(sr.ReadLine());
                                BigInteger r = new BigInteger(sr.ReadLine());
                                divModExpectedResult = new Tuple<BigInteger, BigInteger>(q, r);
                                divModActualResult = BigInteger.DivMod(n1, n2);
                                Console.WriteLine("Case " + i + ": ");
                                Console.WriteLine("a = " + n1.Number_getter() + ", b = " + n2.Number_getter());
                                if (divModActualResult.Item1.Equals(divModExpectedResult.Item1) == false || divModActualResult.Item2.Equals(divModExpectedResult.Item2) == false)
                                {
                                    Console.WriteLine("Wrong Answer.");
                                    isCorrect = false;
                                }
                                else
                                {
                                    Console.WriteLine("Correct!");
                                }
                                Console.WriteLine("Your output: q = " + divModActualResult.Item1.Number_getter() + " r = " + divModActualResult.Item2.Number_getter() +
                                        "\nExpected Output : q = " + divModExpectedResult.Item1.Number_getter() + " r = " + divModExpectedResult.Item2.Number_getter());
                            }

                            if (isCorrect)
                            {
                                Console.Write("Sample tests are correct. Do you want to run Complete tests [y/n]");
                                char ch = Console.ReadLine()[0];
                                if (ch == 'y' || ch == 'Y')
                                {
                                    choice = '2';
                                }
                            }
                        }
                        if (choice == '2')
                        {
                            sr = new StreamReader("DivModComplete.txt");
                            int timeStampBefore = System.Environment.TickCount;
                            testCasesCount = int.Parse(sr.ReadLine());
                            for (int i = 1; isCorrect && i <= testCasesCount; i++)
                            {
                                n1 = new BigInteger(sr.ReadLine());
                                n2 = new BigInteger(sr.ReadLine());
                                BigInteger q = new BigInteger(sr.ReadLine());
                                BigInteger r = new BigInteger(sr.ReadLine());
                                divModExpectedResult = new Tuple<BigInteger, BigInteger>(q, r);
                                divModActualResult = BigInteger.DivMod(n1, n2);
                                if (divModActualResult.Item1.Equals(divModExpectedResult.Item1) == false || divModActualResult.Item2.Equals(divModExpectedResult.Item2) == false)
                                {
                                    Console.WriteLine("\nWrong Answer in test case #" + i);
                                    isCorrect = false;
                                }
                                else
                                {
                                    Console.Write("\rCurrent Test Case# {0} ... {1} % Completed", i, (i - 1) * 100 / testCasesCount);
                                }
                            }

                            if (isCorrect)
                            {
                                Console.Write("\rCurrent Test Case# {0} ... {1} % Completed\n", testCasesCount, 100);
                                Console.WriteLine("\nCongratulations !! Your BigInteger div-mod worked correctly on the complete tests (Y)");
                                Console.Write("\rTotal Time = {0} ms", System.Environment.TickCount - timeStampBefore);
                            }
                        }
                        break;
                    case '3':
                        choice = ReadSampleOrComplete("Mod of Power");
                        if (choice == '1')
                        {
                            sr = new StreamReader("ModOfPowerSample.txt");
                            testCasesCount = int.Parse(sr.ReadLine());
                            for (int i = 1; isCorrect && i <= testCasesCount; i++)
                            {
                                B = new BigInteger(sr.ReadLine());
                                P = new BigInteger(sr.ReadLine());
                                M = new BigInteger(sr.ReadLine());
                                expectedResult = new BigInteger(sr.ReadLine());
                                actualResult = BigInteger.ModOfPower(B, P, M);
                                Console.WriteLine("Case " + i + ": ");
                                Console.WriteLine("B = " + B.Number_getter() + ", P = " + P.Number_getter() + ", M = " + M.Number_getter());
                                if (expectedResult.Equals(actualResult) == false)
                                {
                                    Console.WriteLine("Wrong Answer.");
                                    isCorrect = false;
                                }
                                else
                                {
                                    Console.WriteLine("Correct!");
                                }
                                Console.WriteLine("Your output = " + actualResult.Number_getter() +
                                        "\nExpected Output = " + expectedResult.Number_getter());
                            }

                            if (isCorrect)
                            {
                                Console.Write("Sample tests are correct. Do you want to run Complete tests [y/n]");
                                char ch = Console.ReadLine()[0];
                                if (ch == 'y' || ch == 'Y')
                                {
                                    choice = '2';
                                }
                            }
                        }
                        if (choice == '2')
                        {
                            sr = new StreamReader("ModOfPowerComplete.txt");
                            int timeStampBefore = System.Environment.TickCount;
                            testCasesCount = int.Parse(sr.ReadLine());
                            for (int i = 1; isCorrect && i <= testCasesCount; i++)
                            {
                                B = new BigInteger(sr.ReadLine());
                                P = new BigInteger(sr.ReadLine());
                                M = new BigInteger(sr.ReadLine());
                                expectedResult = new BigInteger(sr.ReadLine());
                                actualResult = BigInteger.ModOfPower(B, P, M);
                                if (expectedResult.Equals(actualResult) == false)
                                {
                                    Console.WriteLine("\nWrong Answer in test case #" + i);
                                    isCorrect = false;
                                }
                                else
                                {
                                    Console.Write("\rCurrent Test Case# {0} ... {1} % Completed", i, (i - 1) * 100 / testCasesCount);
                                }
                            }

                            if (isCorrect)
                            {
                                Console.Write("\rCurrent Test Case# {0} ... {1} % Completed\n", testCasesCount, 100);
                                Console.WriteLine("Congratulations !! Your BigInteger Mod of Power worked correctly on the complete tests (Y)");
                                Console.Write("\rTotal Time = {0} ms", System.Environment.TickCount - timeStampBefore);
                            }
                        }
                        break;
                    case '4':
                        choice = ReadSampleOrComplete("RSA Encryption");
                        if (choice == '1')
                        {
                            sr = new StreamReader("RSASample.txt");
                            testCasesCount = int.Parse(sr.ReadLine());
                            for (int i = 0; isCorrect && i < testCasesCount; i++)
                            {
                                N = new BigInteger(sr.ReadLine());
                                eOrD = new BigInteger(sr.ReadLine());
                                mOrEm = new BigInteger(sr.ReadLine());
                                string mode = sr.ReadLine();
                                expectedResult = new BigInteger(sr.ReadLine());
                                if(mode == "0")
                                {
                                    actualResult = RSA.Encrypt(mOrEm, eOrD, N);
                                }
                                else
                                {
                                    actualResult = RSA.Decrypt(mOrEm, eOrD, N);
                                }
                                
                                Console.WriteLine("Case " + i + ": ");
                                if (mode == "0") // encrypt
                                {
                                    Console.WriteLine("M = " + mOrEm.Number_getter() + ", e = " + eOrD.Number_getter() + ", N = " + N.Number_getter());
                                }
                                else // decrypt
                                {
                                    Console.WriteLine("E(M) = " + mOrEm.Number_getter() + ", d = " + eOrD.Number_getter() + ", N = " + N.Number_getter());
                                }

                                if (expectedResult.Equals(actualResult) == false)
                                {
                                    Console.WriteLine("Wrong Answer.");
                                    isCorrect = false;
                                }
                                else
                                {
                                    Console.WriteLine("Correct!");
                                }
                                Console.WriteLine("Your output = " + actualResult.Number_getter() +
                                        "\nExpected Output = " + expectedResult.Number_getter());
                            }

                            if (isCorrect)
                            {
                                Console.Write("Sample tests are correct. Do you want to run Complete tests [y/n]");
                                char ch = Console.ReadLine()[0];
                                if (ch == 'y' || ch == 'Y')
                                {
                                    choice = '2';
                                }
                            }
                        }
                        if (choice == '2')
                        {
                            sr = new StreamReader("RSAComplete.txt");
                            int timeStampBefore = System.Environment.TickCount;
                            testCasesCount = int.Parse(sr.ReadLine());
                            for (int i = 1; isCorrect && i <= testCasesCount; i++)
                            {
                                N = new BigInteger(sr.ReadLine());
                                eOrD = new BigInteger(sr.ReadLine());
                                mOrEm = new BigInteger(sr.ReadLine());
                                string mode = sr.ReadLine();
                                expectedResult = new BigInteger(sr.ReadLine());
                                if(mode == "0")
                                {
                                    actualResult = RSA.Encrypt(mOrEm, eOrD, N);
                                }
                                else
                                {
                                    actualResult = RSA.Decrypt(mOrEm, eOrD, N);
                                }
                                    

                                if (expectedResult.Equals(actualResult) == false)
                                {
                                    Console.WriteLine("\nWrong Answer in test case #" + i);
                                    isCorrect = false;
                                }
                                else
                                {
                                    Console.Write("\rCurrent Test Case# {0} ... {1} % Completed", i, (i - 1) * 100 / testCasesCount);
                                }
                            }

                            if (isCorrect)
                            {
                                Console.Write("\rCurrent Test Case# {0} ... {1} % Completed\n", testCasesCount, 100);
                                Console.WriteLine("Congratulations !! RSA worked correctly on the complete tests (Y)");
                                Console.Write("\rTotal Time = {0} ms", System.Environment.TickCount - timeStampBefore);
                            }
                        }
                        break;
                    case '5':
                        Console.WriteLine("Text RSA Encryption" + ":\n[1] Sample test case\n[2] Medium test case\n[3] Large test case");
                        Console.Write("\nEnter your choice [1-3]: ");
                        choice =  Console.ReadLine()[0];
                        if (choice == '1')
                        {
                            sr = new StreamReader("TextRSASample.txt");
                            N = new BigInteger(sr.ReadLine());
                            e = new BigInteger(sr.ReadLine());
                            d = new BigInteger(sr.ReadLine());
                            string message = sr.ReadLine();
                            int timeStampBefore = System.Environment.TickCount;
                            string encryptedMessage = RSA.EncryptText(message, e, N);
                            string decyrptedMessage = RSA.DecryptText(encryptedMessage, d, N);
                            Console.WriteLine("M = " + message + ", e = " + e.Number_getter() + ", d =" + d.Number_getter() +", N = " + N.Number_getter());
                            
                            if (decyrptedMessage.Equals(message) == false)
                            {
                                Console.WriteLine("Wrong Answer.\nRSA Text Encryption didn't work correctly");
                                isCorrect = false;
                            }
                            else
                            {
                                Console.WriteLine("Correct!");
                                Console.WriteLine("\rTotal Time = {0} ms", System.Environment.TickCount - timeStampBefore);
                            }



                            if (isCorrect)
                            {
                                Console.Write("Sample test is correct. Do you want to run Medium test [y/n]");
                                char ch = Console.ReadLine()[0];
                                if (ch == 'y' || ch == 'Y')
                                {
                                    choice = '2';
                                }
                            }
                        }
                        if (choice == '2')
                        {
                            sr = new StreamReader("TextRSAMedium.txt");
                            N = new BigInteger(sr.ReadLine());
                            e = new BigInteger(sr.ReadLine());
                            d = new BigInteger(sr.ReadLine());
                            string message = sr.ReadLine();
                            int timeStampBefore = System.Environment.TickCount;
                            string encryptedMessage = RSA.EncryptText(message, e, N);
                            string decyrptedMessage = RSA.DecryptText(encryptedMessage, d, N);
                            isCorrect = true;
                            Console.WriteLine("M = " + message + "\ne = " + e.Number_getter() + ", d =" + d.Number_getter() + ", N = " + N.Number_getter());

                            if (decyrptedMessage.Equals(message) == false)
                            {
                                Console.WriteLine("Wrong Answer.\nRSA Text Encryption didn't work correctly");
                                isCorrect = false;
                            }
                            else
                            {
                                Console.WriteLine("Correct!");
                                Console.WriteLine("\rTotal Time = {0} ms", System.Environment.TickCount - timeStampBefore);

                            }



                            if (isCorrect)
                            {
                                Console.Write("Medium test is correct. Do you want to run Large test [y/n]");
                                char ch = Console.ReadLine()[0];
                                if (ch == 'y' || ch == 'Y')
                                {
                                    choice = '3';
                                }
                            }
                            
                        }
                        if (choice == '3')
                        {
                            sr = new StreamReader("TextRSALarge.txt");
                            N = new BigInteger(sr.ReadLine());
                            e = new BigInteger(sr.ReadLine());
                            d = new BigInteger(sr.ReadLine());
                            string message = sr.ReadLine();
                            int timeStampBefore = System.Environment.TickCount;
                            string encryptedMessage = RSA.EncryptText(message, e, N);
                            string decyrptedMessage = RSA.DecryptText(encryptedMessage, d, N);
                            isCorrect = true;
                            Console.WriteLine("e = " + e.Number_getter() + ", d =" + d.Number_getter() + ", N = " + N.Number_getter());

                            if (decyrptedMessage.Equals(message) == false)
                            {
                                Console.WriteLine("Wrong Answer.\nRSA Text Encryption didn't work correctly");
                                isCorrect = false;
                            }
                            else
                            {
                                Console.WriteLine("Correct!");

                            }



                            if (isCorrect)
                            {
                                Console.Write("Large test is correct! RSA text encryption works correctly (mostly)!");
                                Console.WriteLine("\rTotal Time = {0} ms", System.Environment.TickCount - timeStampBefore);
                            }

                        }
                        break;
                    default:
                        Console.WriteLine("Invalid Input");
                        break;
                }
                Console.Write("\nDo you want to [c]ontinue or [q]uit \nEnter your choice [q or c]: ");
                char c = Console.ReadLine()[0];
                if (c == 'Q' || c == 'q')
                {
                    break;
                }
            } while (true);


        }

        static char ReadSampleOrComplete(string taskName)
        {
            Console.WriteLine(taskName + ":\n[1] Sample test cases\n[2] Complete test cases");
            Console.Write("\nEnter your choice [1-2]: ");
            return Console.ReadLine()[0];
        }
        #endregion
    }
}
