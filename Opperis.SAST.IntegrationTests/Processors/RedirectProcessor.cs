﻿using Opperis.SAST.Engine;
using Opperis.SAST.Engine.Analyzers;
using Opperis.SAST.Engine.Findings;
using Opperis.SAST.Engine.SyntaxWalkers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opperis.SAST.IntegrationTests.Processors;

internal static class RedirectProcessor
{
    internal static List<BaseFinding> GetAllExternalRedirects()
    { 
        var retVal = new List<BaseFinding>();

        foreach (var project in Globals.Solution.Projects)
        {
            Globals.Compilation = project.GetCompilationAsync().Result;

            foreach (var syntaxTree in Globals.Compilation.SyntaxTrees)
            {
                var root = syntaxTree.GetRoot();

                var walker = new ExternalRedirectSyntaxWalker();
                retVal.AddRange(ExternalRedirectAnalyzer.FindProblematicExternalRedirects(walker, root));
            }
        }

        return retVal;
    }
}
