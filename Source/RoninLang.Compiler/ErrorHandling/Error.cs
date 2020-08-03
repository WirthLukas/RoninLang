using System;
using RoninLang.Core.ErrorHandling;

namespace RoninLang.Compiler.ErrorHandling
{
    public class Error : IError
    {
        private readonly ErrorType _errorType;
        private readonly object[] _parameters = new object[0];

        public ErrorClass ErrorClass => _errorType.ErrorClass;
        public int LineNumber { get; set; }
        public int Number => _errorType.ErrorNumber;
        public ErrorType ErrorType => _errorType;

        public string Message => string.Format(_errorType.ErrorMessage, _parameters);

        /// <summary>
        /// Instantiates a simple error with no extra information.
        /// This constructor can only be used with ErrorTypes providing no %s
        /// format string.
        /// </summary>
        /// <param name="errorType">Type of error to be instantiated.</param>
        public Error(ErrorType errorType) => _errorType = errorType;

        /// <summary>
        /// Instantiates an error with one additional information parameter.
        /// This constructor of error can only be used with ErrorTypes providing one
        /// %s format string.
        /// </summary>
        /// <param name="errorType">Type of error to be instantiated.</param>
        /// <param name="parameter">Additional info parameter.</param>
        public Error(ErrorType errorType, object parameter) : this(errorType)
            => _parameters = new[] {parameter};

        /// <summary>
        /// Instantiates an error with a list of additional information parameters.
        /// This constructor can only be used with ErrorTypes providing a %l format string.
        /// </summary>
        /// <param name="errorType">Type of error to be instantiated.</param>
        /// <param name="parameters">List of additional info parameters</param>
        public Error(ErrorType errorType, object[] parameters) : this(errorType)
            => _parameters = parameters ?? throw new ArgumentNullException(nameof(parameters));

        /// <summary>
        /// Instantiates an error with more than one additional information parameters.
        /// This constructor can only be used with ErrorTypes providing more than
        /// one %s format strings.
        /// </summary>
        /// <param name="errorType">Type of error to be instantiated</param>
        /// <param name="firstParameter">First additional info parameter.</param>
        /// <param name="furtherParameter">Further info parameters.</param>
        public Error(ErrorType errorType, object firstParameter, params object[] furtherParameter) : this(errorType, firstParameter)
            => furtherParameter.CopyTo(_parameters, 1);
    }
}