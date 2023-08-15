using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Text;
using System.Text.RegularExpressions;

namespace MoqToNSubstituteConverter;

public class Conversion
{
    public ConversionResponse ConvertMoqToNSubstitute(string code)
    {
        List<string> stepComments = new();
        StringBuilder processedCode = new();

        //Run the first pass, using the Roslyn Syntax checker
        SyntaxTree tree = CSharpSyntaxTree.ParseText(code);
        CompilationUnitSyntax root = tree.GetCompilationUnitRoot();
        List<SyntaxNode> syntaxList = root.ChildNodes().ToList();
        StringBuilder sb = new();
        foreach (SyntaxNode item in syntaxList)
        {
            //Debug.WriteLine(item.Kind());
            //Debug.WriteLine(item.ToFullString());
            sb.Append(ProcessLine(item.ToFullString()));
        }

        //Run a second pass, just scanning the individual lines.
        string processedCodeFirstPass = sb.ToString();
        foreach (string line in processedCodeFirstPass.Split(Environment.NewLine))
        {
            //Feed the line back into the final result
            processedCode.AppendLine(ProcessLine(line));
        }

        //Return the final conversion result, with the original code, processed (actions) yaml, and any comments
        return new ConversionResponse
        {
            OriginalCode = code,
            ConvertedCode = processedCode.ToString(),
            Comments = stepComments
        };
    }

    private static string ProcessLine(string line)
    {
        //Replace using
        string processedLine = line.Replace("using Moq;", "using NSubstitute;");

        //Remove .Object
        processedLine = processedLine.Replace(".Object", "");

        //process Invocations.Clear
        processedLine = processedLine.Replace(".Invocations.Clear()", ".ClearReceivedCalls()");

        //Fix ReturnsAsync
        processedLine = processedLine.Replace("ReturnsAsync", "Returns");
        
        //process variable declarations
        processedLine = ProcessVariableDeclaration(processedLine);

        //process setup
        processedLine = ProcessSetup(processedLine);

        //process verify
        processedLine = ProcessVerify(processedLine);

        //process callback
        //TODO: processedLine = ProcessCallback(processedLine);

        //Feed the line back into the final result
        //processedCode.AppendLine(processedLine);
        return processedLine;
    }

    private static string ProcessVariableDeclaration(string code)
    {
        //Update variable declarations
        code = code.Replace("new Mock<", "Substitute.For<");

        //Find the interface
        string pattern = @"Substitute.For\<(.*?)\>";
        MatchCollection matches = Regex.Matches(code, pattern);

        // Process each match and perform replacements on the explict variable declaration (if this is a var - it doesn't do anything)
        foreach (Match match in matches)
        {
            // Extract the text between square brackets
            string variableText = match.Groups[1].Value;
            code = code.Replace("Mock<" + variableText + ">", variableText);
        }

        return code;
    }

    private static string ProcessSetup(string code)
    {
        code = code.Replace("It.IsAny", "Arg.Any");
        string setupPattern = @"\.Setup\((.*?)\=\>";

        Match setupMatch = Regex.Match(code, setupPattern);

        if (setupMatch.Success)
        {
            string extractedText = setupMatch.Groups[1].Value.Trim();
            code = code.Replace(".Setup(" + extractedText + " => " + extractedText, "");
            //Find and remove the last closed bracket
            int open = 0;
            StringBuilder processedString = new();
            foreach (char c in code)
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
            code = processedString.ToString();
        }
        return code;
    }

    private static string ProcessVerify(string code)
    {
        string verifyPattern = @"\.Verify\((.*?)\=\>";
        int timesExactlyValue = 0;

        Match verifyMatch = Regex.Match(code, verifyPattern);

        if (verifyMatch.Success)
        {
            string extractedText = verifyMatch.Groups[1].Value.Trim();

            //Replace the times exactly piece
            code = code.Replace(", Times.Once)", "");
            if (code.Contains(", Times.Exactly("))
            {
                //Find the number of times exactly
                string timesExactlyPattern = @"Times.Exactly\((.*?)\)";
                Match timesExactlyMatch = Regex.Match(code, timesExactlyPattern);
                if (timesExactlyMatch.Success)
                {
                    string extractedTimesExactlyText = timesExactlyMatch.Groups[1].Value.Trim();
                    timesExactlyValue = int.Parse(extractedTimesExactlyText);
                }
                code = code.Replace(", Times.Exactly(" + timesExactlyValue + ")", "");
            }

            //Add the received section, with the times exactly if it's more than 1
            if (timesExactlyValue > 0)
            {
                code = code.Replace(".Verify(" + extractedText + " => " + extractedText, ".Received(" + timesExactlyValue + ")");
            }
            else
            {
                code = code.Replace(".Verify(" + extractedText + " => " + extractedText, ".Received()");
            }
            //Find and remove the last closed bracket
            int open = 0;
            StringBuilder processedString = new();
            foreach (char c in code)
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
            code = processedString.ToString();
        }
        return code;
    }

    private static string ProcessCallback(string code)
    {
        //string callbackPattern = @"\.Callback\((.*?)";
        string callbackPattern = @"\.Callback*\((?<=\().*(?=\))\)";
        Match callbackMatch = Regex.Match(code, callbackPattern);

        if (callbackMatch.Success)
        {
            string extractedText = callbackMatch.Groups[0].Value.Trim().Replace(".Callback(","");
            extractedText = extractedText.Substring(0, extractedText.Length - 1);

            code = code.Replace(".Callback(" + extractedText + ")","");
        }


        return code;
    }

}
