// Guids.cs
// MUST match guids.h
using System;

namespace Microsoft.ClojureExtension
{
    static class GuidList
    {
        public const string GuidClojureExtensionCmdSetString = "44f0b6b9-595a-426f-88d1-468f7af14242";
        public static readonly Guid GuidClojureExtensionCmdSet = new Guid(GuidClojureExtensionCmdSetString);
    };
}