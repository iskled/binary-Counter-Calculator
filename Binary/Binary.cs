using System;
using System.Diagnostics.Metrics;
using System.Reflection.Metadata;

namespace CP
{
    // Requirements, Assignment/Implementation Rules:
    //  You must work with 16 bits, hint: Use an int array of 16 as a field
    //  Binary operators: ~, <<, >>
    //  Binary, Arithmetic, and Logical operators: +, -, *, /, ==, !=, <, >, <=, >= algorithms
    //      must perform in binary. Algorthims that convert to denary, perform in denary, and convert
    //      back to binary carry 0 marks.

    class Binary
    {
        #region(Fields)
        int[] array = new int[16];
        public char sign;
           
        #endregion
        #region(Properties)

        public int[] Array
        {
            get
            {
                return array;
            }

            set
            {
                this.array = value;
            }
        }
       
        #endregion
        #region Constructors

       
        #endregion
        #region(Index operator)
        #endregion
        #region(Implicit Convertors: int to Binary, Binary to int)
        public static implicit operator Binary(int a)
        {
            Binary binary = new Binary();
            Binary binary1 = new Binary();
            int num = a;
            if (a < 0)
            {
                num = a * -1;
                binary.sign = '-';
            }

            int rem = num;
            int whole = num;
            int j = 0;
            do
            {
                rem = rem % 2;
                whole = whole / 2;
                binary.Array[j] = rem;
                j++;
                rem = whole;
            } while (whole > 0);
            for (int i = 15; i >= 0; i--)
            {
                binary1.Array[15 - i] = binary.Array[i];
            }
            if (binary.sign == '-')
            {
                for (int i = 0; i <= 15; i++)
                {
                    binary1.Array[i] = Flip(binary1.Array[i]);
                }
                int[] x1 = new int[16];
                x1[15] = 1;

                int total = 0;
                rem = 0;
                int convToInt1 = 0;
                int convToInt2 = 0;
                j = 15;
                while (j >= 0)
                {
                    convToInt1 = binary1.Array[j];
                    convToInt2 = x1[j];
                    total = convToInt1 + convToInt2 + rem;
                    if (total == 0)
                    {
                        rem = 0;
                        binary1.Array[j] = 0;
                    }
                    else if (total == 1)
                    {
                        rem = 0;
                        binary1.Array[j] = 1;
                    }
                    else if (total == 2)
                    {
                        rem = 1;
                        binary1.Array[j] = 0;
                    }
                    else if (total == 3)
                    {
                        rem = 1;
                        binary1.Array[j] = 1;
                    }
                    else if (j == 0)
                    {
                        return binary1;
                    }
                    j = j - 1;
                }
            }
            return binary1;
        }
        public static implicit operator int(Binary b)
        {
            string numString = "1010"; //b.ToDecimal();
            int[] numInt = new int[16];
            int index = 0;
            int total = 0;
            int[] array = new int[16];
            string[] numS = new string[16];
            foreach (char c in numString)
            {
                string num = Convert.ToString(c);
                int number = int.Parse(num);
                numInt[index] = number;
                total = total + number;
                array[index] = number;
                index++;
            }
            int denary = 0;
            int i = 0;
            for (int j = (index - 1); j >= 0; j--)
            {
                int powTotal = 1; //powTotal stores total value of the exponential

                for (int k = 0; i > k; k++) //k is the number of the exponential
                {
                    powTotal = powTotal * 2;
                }
                i++;
                denary = denary + (array[j] * powTotal);

            }
            return denary;
        }
        #endregion
        #region(Methods: ToDecimal, ToString)
        public int ToDecimal()
        {
            int[] tempArray = new int[16];
            int sign = 1;

            for (int m = 0; m < 16; m++)
            {
                tempArray[m] = this.Array[m];
            }
            if (this.Array[0] == 1)
            {
                this.sign = '-';
                sign = -1;
                for (int j = 0; j <= 15; j++)
                {
                    tempArray[j] = Flip(tempArray[j]);
                }
            }

            if (this.sign == '-')
            {

                sign = -1;

                int[] x1 = new int[16];
                x1[15] = 1;


                int total = 0;
                int rem = 0;
                int convToInt1 = 0;
                int convToInt2 = 0;
                int j = 15;
                while (j >= 0)
                {

                    convToInt1 = tempArray[j];
                    convToInt2 = x1[j];
                    total = convToInt1 + convToInt2 + rem;
                    if (total == 0)
                    {
                        rem = 0;
                        tempArray[j] = 0;
                    }
                    else if (total == 1)
                    {
                        rem = 0;
                        tempArray[j] = 1;
                    }
                    else if (total == 2)
                    {
                        rem = 1;
                        tempArray[j] = 0;
                    }
                    else if (total == 3)
                    {
                        rem = 1;
                        tempArray[j] = 1;
                    }
                    
                    j = j - 1;
                }         

            }

            int index = 16;
            int denary = 0;
            int i = 0;
            for (int j = (index - 1); j >= 0; j--)
            {
                int powTotal = 1; //powTotal stores total value of the exponential

                for (int k = 0; i > k; k++) // k = number of exponential
                {
                    powTotal = powTotal * 2;
                }
                i++;
                denary = denary + (tempArray[j] * powTotal);

            }

            return denary * sign;
        }

        public override string ToString()
        {

            string s = "";
            int counter = 0;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    s = s + Convert.ToString((this.Array[counter]));

                    counter++;
                }
                s = s + " ";
            }

            return s; //returns the string value of the array
        }
        #endregion
        #region(Shift Opertors: Shift to left by n (<<), Shift to right by n (>>))
        public static Binary operator <<(Binary y, int n)
        {
            Binary x1 = new Binary();
            Binary x2 = new Binary();

            x1 = y;
            for (int shift = 0; shift <= 15; shift++)
            {
                if ((shift - n) < 0 || shift > 15)
                { x2.Array[shift] = 0; }
                else if ((shift - n) <= 15)
                { x2.Array[shift - n] = x1.Array[shift]; }
            }
            return x2;
        }
        public static Binary operator >>(Binary z, int n)
        {
            Binary x3 = new Binary();
            int newN = -n;
            x3 = z << newN;    //Refactoring of the right shift

            return x3;
        }


        #endregion
        #region Utilities
        public static int Flip(int i)
        {
            if (i == 0)
            {
                i = 1;
            }
            else if (i == 1)
                i = 0;
            return i;
        }
        public static int Multiply(int a, int b) 
        {
            if (a == 0 || b == 0)
                return 0;
            return 1;
        }
        public static int Division(int a, int b)
        {
            if (a == 0 || b == 0)
                return 0;
            return 1;
        }
        #endregion
        #region(Binary Operators: Ones' complement, Negation)
        public static Binary operator ~(Binary a)
        {
            if (a.Array[0] == 1)
            {
                a.sign = '-';
            }
            for (int i = 0; i <= 15; i++)
            {
                a.Array[i] = Flip(a.Array[i]);
            }
            return a;
        }
        public static Binary operator -(Binary a)
        {
            if (a.Array[0] == 1)
                for (int i = 0; i <= 15; i++)
                {
                    a.Array[i] = Flip(a.Array[i]);
                }
            Binary c = new Binary();
            c = ~a + 1; 
            return c;
        }
        #endregion
        #region(Binary Arithmatic Opertors: +, -, *, /)
        public static Binary operator +(Binary d, Binary f)
        {
            Binary a = new Binary();
            int total = 0;
            int rem = 0;
            int convToInt1 = 0;
            int convToInt2 = 0;
            int j = 15;
            while (j >= 0)
            {
                convToInt1 = d.Array[j];
                convToInt2 = f.Array[j];
                total = convToInt1 + convToInt2 + rem;
                if (total == 0)
                {
                    rem = 0;
                    a.Array[j] = 0;
                }
                else if (total == 1)
                {
                    rem = 0;
                    a.Array[j] = 1;
                }
                else if (total == 2)
                {
                    rem = 1;
                    a.Array[j] = 0;
                }
                else if (total == 3)
                {
                    rem = 1;
                    a.Array[j] = 1;
                }
                else if (j == 0)
                {
                    return a;
                }
                j = j - 1;
            }
            return a;
        }
        public static Binary operator -(Binary g, Binary h)
        {   
            return g + (1 + (~h));
        }
        public static Binary operator *(Binary i, Binary j)
        {
            int l;
            Binary a = new Binary();
            Binary b = new Binary();
            for (int o = 15; o >= 0; o--)
            {
                a.Array[o] = Multiply(i.Array[o], j.Array[15]);
            }
            l = 0;
            for (int m = 14; m >= 0; m--)
            {
                for (int n = 15; n >= 0; n--)
                {
                    b.Array[n] = Multiply(i.Array[n], j.Array[m]);
                    
                }
                l++;
                b <<= l;
                a = a + b;
            }                                           
            return a;
        }
        public static Binary operator /(Binary d, Binary f)
        {

            int l;
            Binary a = new Binary();
            Binary b = new Binary();
            for (int o = 15; o >= 0; o--)
            {
                a.Array[o] = Division(d.Array[o], f.Array[15]);
            }
            l = 0;
            for (int m = 14; m >= 0; m--)
            {
                for (int n = 15; n >= 0; n--)
                {
                    b.Array[n] = Division(d.Array[n], f.Array[m]);

                }
                l++;
                b >>= l;
                a = a + b;
            }
            return a;
           
        }
        public static Binary operator %(Binary d, Binary f)
        {
            Binary a = new();
            return a;
        }
        #endregion
        #region(Logical Operators: ==, !=, <, >, <=, >=)
        public static bool operator ==(Binary a, Binary b)
        {
            
            for (int i = 0; i < 16; i++)
            {
                if (a.Array[i] != b.Array[i])
                {
                    return false;
                }
            }
            return true;
        }
        public static bool operator !=(Binary a, Binary b)
        {
            if (a == b)
            { return false;}
            return true;
        }
        public static bool operator <(Binary a, Binary b)
        {
            if (a.Array[0] < b.Array[0])
            {
                return false;
            }
            else if (a.Array[0] == b.Array[0])
            {
                for (int i = 1; i < 16; i++)
                {
                    if (a.Array[i] < b.Array[i])
                    { return true; }
                }
            }
            else if (a.Array[0] > b.Array[0])
            {
                return true;
            }
            return default;
        }
        public static bool operator >(Binary a, Binary b)
        {
           
           if (a == b && a < b)
           {
               return false;
           }
            return true;
        }
        public static bool operator <=(Binary a, Binary b)
        {
            if (a < b || a == b )
                { return true; }
            return false;
        }
        public static bool operator >=(Binary a, Binary b)
        {
            if (a > b || a == b)
            { return true;}
            return false;
        }
        #endregion

    }
}
