namespace MoqToNSubstituteConverter;

public class Conversion
{
    public ConversionResponse ConvertMoqToNSubstitute(string code)
    {
        List<string> stepComments = new();
        string convertedCode = code.Replace("using Moq;", "using NSubstitute;");

        //Return the final conversion result, with the original (pipeline) yaml, processed (actions) yaml, and any comments
        return new ConversionResponse
        {
            OriginalCode = code,
            ConvertedCode = convertedCode,
            Comments = stepComments
        };
    }

}
