using GenshinTrialGenerator.Application.Exceptions;

namespace GenshinTrialGenerator.Server.Exceptions
{
    public class GeneratorNotFoundException : GeneratorException
    {
        public GeneratorNotFoundException(string message) : base(message)
        {
            StatusCode = 400;
        }
    }
}
