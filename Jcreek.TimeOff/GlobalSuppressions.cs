// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1633:File should have header", Justification = "Not something we need at this stage - jcreek")]
[assembly: SuppressMessage("StyleCop.CSharp.OrderingRules", "SA1208:System using directives should be placed before other using directives", Justification = "I'd prefer to have the using directives in alphabetical order - jcreek")]
[assembly: SuppressMessage("Critical Code Smell", "S927:parameter names should match base declaration and other partial definitions", Justification = "It's a smell, but not one that really matters for this project. Better to have parameter names that make sense in context, in my opinion - jcreek")]
[assembly: SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1101:Prefix local calls with this", Justification = "Visual Studio itself says this is unnecessary, who are we to argue? Plus, it simplifies the code - jcreek")]
[assembly: SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1124:Do not use regions", Justification = "They are really useful, get off my back already - jcreek")]
[assembly: SuppressMessage("StyleCop.CSharp.LayoutRules", "SA1516:Elements should be separated by blank line", Justification = "This makes readability worse in some cases - jcreek")]
[assembly: SuppressMessage("Major Code Smell", "S1854:Unused assignments should be removed", Justification = "This picks up variables that ARE used, unclear why but it's probably something I've done - jcreek")]
[assembly: SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Ignoring this for now as there's so many to do, but eventually this should be done - jcreek")]
