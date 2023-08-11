using System.Text.RegularExpressions;

namespace MoqToNSubstituteConverter;

public class Conversion
{
    public ConversionResponse ConvertMoqToNSubstitute(string code)
    {
        List<string> stepComments = new();

        //Replace using
        string convertedCode = code.Replace("using Moq;", "using NSubstitute;");

        //Remove .Object
        convertedCode = convertedCode.Replace(".Object", "");

        //Update variable declarations
        convertedCode = convertedCode.Replace("new Mock<", "Substitute.For<");

        //Find the interface
        string pattern = @"Substitute.For\<(.*?)\>";
        MatchCollection matches = Regex.Matches(convertedCode, pattern);

        // Process each match and perform replacements
        foreach (Match match in matches)
        {
            // Extract the text between square brackets
            string variableText = match.Groups[1].Value;
            convertedCode = convertedCode.Replace("Mock<" + variableText + ">", variableText);
        }




        //Return the final conversion result, with the original (pipeline) yaml, processed (actions) yaml, and any comments
        return new ConversionResponse
        {
            OriginalCode = code,
            ConvertedCode = convertedCode,
            Comments = stepComments
        };
    }

}
