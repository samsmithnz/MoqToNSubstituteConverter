# MoqToNSubstituteConverter
[![CI/CD](https://github.com/samsmithnz/MoqToNSubstituteConverter/actions/workflows/workflow.yml/badge.svg)](https://github.com/samsmithnz/MoqToNSubstituteConverter/actions/workflows/workflow.yml)

This is an early version of a tool to convert Moq tests to NSubstitute. It is not perfect, but it does a reasonable job. It is not intended to be a perfect conversion, but to get you most of the way there.

Please log issues if you find any problems.

To use, browse to [MoqToNSubstitute.AzureWebsites.Net](https://moqtonsubstitute.azurewebsites.net)

## Current limitations:
- Likely doesn't handle code spread over multiple lines. I think we can solve this with Roslyn and the code analyzer, to identify full lines (spread over multiple).
