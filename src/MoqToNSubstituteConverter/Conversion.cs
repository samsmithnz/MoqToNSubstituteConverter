using System.Text;
using System.Text.RegularExpressions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

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

        //Process Setup
        convertedCode = convertedCode.Replace("It.IsAny", "Arg.Any");
        //convertedCode = convertedCode.Replace(".Setup(", "");

        string setupPattern = @"\.Setup\((.*?)\=\>";

        Match setupMatch = Regex.Match(convertedCode, setupPattern);

        if (setupMatch.Success)
        {
            string extractedText = setupMatch.Groups[1].Value.Trim();
            convertedCode = convertedCode.Replace(".Setup(" + extractedText + " => " + extractedText, "");
            //Find and remove the last closed bracket
            int open = 0;
            StringBuilder processedString = new();
            foreach (char c in convertedCode)
            {
                if (c == '(')
                {
                    open++;
                }
                else if (c == ')')
                {
                    open--;
                }
                //Only add the string back if there is a matching bracket
                if (open >= 0)
                {
                    processedString.Append(c);
                }
                else if (c == ')')
                {
                    //get open count back to 0
                    open++;
                }
            }
            convertedCode = processedString.ToString();
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
