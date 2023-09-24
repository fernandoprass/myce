﻿using System.Text;
using System.Text.RegularExpressions;

namespace Myce.Extensions
{
   public static class StringExtensions
   {
      /// <summary>
      /// Return an empty string if the string is null
      /// </summary>
      /// <param name="value">The string value</param>
      /// <returns>Returns an empty string if the string is null</returns>
      public static string EmptyIfIsNull(this string value)
      {
         return value.IsNull() ? string.Empty : value;
      }

      /// <summary>
      /// Remove letters and simbols, keeping only numbers
      /// </summary>
      /// <param name="value"></param>
      /// <returns>Returns a string with only numbers</returns>
      public static string KeepOnlyNumbers(this string value)
      {
         if (value.IsNull())
         {
            return null;
         }
         return Regex.Replace(value, @"[^0-9]", "");
      }

      /// <summary>
      /// Remove simbols keeping only numbers and letters
      /// </summary>
      /// <param name="value">The string value</param>
      /// <returns>Returns a string with only numbers and letters</returns>
      public static string KeepOnlyNumbersAndLetters(this string value)
      {
         return RemoveSimbols(value, false);
      }

      /// <summary>
      /// Remove simbols keeping only numbers, letters and spaces
      /// </summary>
      /// <param name="value"></param>
      /// <returns>Returns a string with only numbers, letters, and spaces</returns>
      public static string KeepOnlyNumbersAndLettersAndSpaces(this string value)
      {
         return RemoveSimbols(value, true);
      }

      /// <summary>
      /// Remove simbols keeping only numbers, letters and spaces (if asked)
      /// </summary>
      /// <param name="value">The string value</param>
      /// <param name="keepSpaces">If TRUE white spaces are NOT removed</param>
      /// <returns>Returns a string with only numbers, letters, and spaces if required</returns>
      private static string RemoveSimbols(string value, bool keepSpaces)
      {
         if (value.IsNull())
         {
            return null;
         }

         string result = string.Empty;
         for (int i = 0; i < value.Length; i++)
         {
            if (char.IsLetterOrDigit(value[i]) || keepSpaces && char.IsWhiteSpace(value[i]))
            {
               result += value[i];
            }
         }

         return result;
      }

      /// <summary>
      /// Remove accents
      /// </summary>
      /// <param name="value">The string value</param>
      /// <returns>Returns a string without accents</returns>
      public static string RemoveAccents(this string value)
      {
         if (value.IsNull())
         {
            return null;
         }

         Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
         return Encoding.ASCII.GetString(
             Encoding.GetEncoding("Cyrillic").GetBytes(value)
         );
      }

      /// <summary>
      /// Converts string to Enum object
      /// </summary>
      /// <typeparam name="T">The type of enum</typeparam>
      /// <param name="value">The string value to convert</param>
      /// <returns>Returns and enum object</returns>
      public static T ToEnum<T>(this string value)
          where T : struct
      {
         return (T)System.Enum.Parse(typeof(T), value, true);
      }
   }
}
