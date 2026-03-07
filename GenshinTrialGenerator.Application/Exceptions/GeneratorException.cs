using System;
using System.Collections.Generic;
using System.Text;

namespace GenshinTrialGenerator.Application.Exceptions
{
    public class GeneratorException(string message) : Exception(message)
    {
        public int StatusCode = 500;
    }
}
