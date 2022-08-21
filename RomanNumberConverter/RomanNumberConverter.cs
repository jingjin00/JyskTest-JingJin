using System;
using System.Text.RegularExpressions;

namespace RomanNumberConverter
{
    public interface INumberConverter
    {
        public (int, string) RomanToInt(string romanNumber);
        public (string, string) IntToRoman(int number);
    }
    public class NumberConverter : INumberConverter
    {
        public (int, string) RomanToInt(string romanNumber)
        {
            if (string.IsNullOrEmpty(romanNumber))
                return (-1, "Roman number cannot be empty. Please input an valid romertal");

            romanNumber = romanNumber.ToUpper();
            string formattingError = " is not correct roman number format. Please check your input";
            string strRegex = @"[^IVXLCDM]|VV|LL|DD|[I]{4}|[X]{4}|[C]{4}|[M]{4}|IL|IC|ID|IM|VX|VL|VC|VD|VM|XD|XM|LC|LD|LM|DM|IIV|IIC|XXL|LLC|CCD|CCM";
            Regex regex = new Regex(strRegex);
            if (regex.IsMatch(romanNumber))
            {
                return (-1, romanNumber + formattingError);
            }
            int number = 0;
            try
            {
                for (int counter = 0; counter < romanNumber.Length; counter++)
                {
                    var value = (int)Enum.Parse(typeof(RomanNumberEnum), romanNumber[counter].ToString());
                    var nextValue = (counter < romanNumber.Length - 1) ? (int)Enum.Parse(typeof(RomanNumberEnum), romanNumber[counter + 1].ToString()) : 0;
                    number = nextValue > value ? number - value : number + value;

                }
                return (number, string.Empty);
            }
            catch (System.Exception ex)
            {
                return (0, romanNumber + " error: " + ex.StackTrace);
            }
        }
        /// <summary>
        /// Convert a number to roman number, here vi assum the number is always positive. Negative number is not in the topic
        /// </summary>
        /// <param name="number">Positive digital number</param>
        /// <returns>If input number is greater than 3000, return error. Else return roman number</returns>
        public (string, string) IntToRoman(int number)
        {
            if (number > 3000)
                return (string.Empty, "Number greater than 3000 is not supported");
            //start from 1000
            return (DivideIntIntoRomanCharactor(number, string.Empty, 3), String.Empty);
        }
        private string DivideIntIntoRomanCharactor(int number, string roman, int pow)
        {
            (string text, int remaining) = ConvertAIntToRoman((int)Math.Pow(10, pow), number, roman);
            if (remaining > 0)
            {
                pow--;
                text = DivideIntIntoRomanCharactor(remaining, text, pow);
            }
            return text;
        }
        private (string text, int rest) ConvertAIntToRoman(int pow, int number, string roman)
        {
            var index = number / pow;
            if (index == 0)
                return (roman, number);
            var halfIndex = index > 5 ? index - 5 : index;
            roman += halfIndex == 5 ? ((RomanNumberEnum)((index > 5 ? 10 : 5) * pow)).ToString() :
                   halfIndex == 4 ? ((RomanNumberEnum)pow).ToString() + (RomanNumberEnum)((index > 5 ? 10 : 5) * pow) :
                                     (index > 5 ? (RomanNumberEnum)(5 * pow) : string.Empty) + string.Empty.PadRight(halfIndex, char.Parse(((RomanNumberEnum)pow).ToString()));
            return (roman, number - index * pow);
        }
    }
    /// <summary>
    /// Enum all roman charactors to int.
    /// </summary>
    public enum RomanNumberEnum
    {
        I = 1,
        V = 5,
        X = 10,
        L = 50,
        C = 100,
        D = 500,
        M = 1000
    }

}

