using CarApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarApp.Helpers
{
    public static class CarDataValidator
    {
        private static bool LenghtOfData(string data, int requiredLenght)
        {
            if (string.IsNullOrEmpty(data)) return false;
            return data.Length == requiredLenght ? true : false;
        }
        internal static string InputDataValidator(Car carDataToValidate)
        {
            string message = "";
            bool IsVinValid = LenghtOfData(carDataToValidate.VIN, 17) || LenghtOfData(carDataToValidate.VIN, 7);
            bool IsRegisterNumberValid = LenghtOfData(carDataToValidate.RegistrationNumber, 7);
            if (!IsVinValid && !IsRegisterNumberValid)
            {
                carDataToValidate.RegistrationNumber = "";
                carDataToValidate.VIN = "";
                return "Registration number must contain 7 characters and VIN Number must contain 7 or 17 Characters";
            }
            if (!IsRegisterNumberValid)
            {
                carDataToValidate.VIN = "";
                return "Registration number must contain 7 characters";
            }
            if (!IsVinValid)
            {
                carDataToValidate.RegistrationNumber = "";
                return "VIN Number must contain 7 or 17 Characters";
            }
            return message;
        }
    }
}
