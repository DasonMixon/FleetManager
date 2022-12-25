using System.Runtime.CompilerServices;

namespace FleetManager.Exceptions
{
    public class ControllerParameterValidationException : Exception
    {
        public IList<string> ParameterNames { get; private init; }

        public ControllerParameterValidationException(IList<string> parameterNames)
        {
            ParameterNames = parameterNames;
        }

        public static void ThrowIfNullEmptyOrWhitespace(params Param[] parameters)
        {
            var emptyParams = parameters.Where(p => string.IsNullOrWhiteSpace(p.Value)).Select(p => p.Name).ToList();
            if (emptyParams.Any())
            {
                throw new ControllerParameterValidationException(emptyParams);
            }
        }
    }

    public class Param
    {
        public string Name { get; private set; } = default!;
        public string? Value { get; private set; }

        public static Param From(string? argument, [CallerArgumentExpression("argument")] string? paramName = null)
        {
            return new Param
            {
                Name = paramName!,
                Value = argument
            };
        }
    }
}
