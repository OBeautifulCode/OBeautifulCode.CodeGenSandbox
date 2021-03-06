// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AssertionKind.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// <auto-generated>
//   Sourced from NuGet package. Will be overwritten with package update except in OBeautifulCode.Assertion.Recipes source.
// </auto-generated>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Assertion.Recipes
{
    using global::System;

    /// <summary>
    /// Specifies the kind of assertion.
    /// </summary>
#if !OBeautifulCodeAssertionSolution
    [global::System.CodeDom.Compiler.GeneratedCode("OBeautifulCode.Assertion.Recipes", "See package version number")]
    internal
#else
    public
#endif
    enum AssertionKind
    {
        /// <summary>
        /// Unknown (default).
        /// </summary>
        Unknown,

        /// <summary>
        /// An argument-verifying assertion.
        /// </summary>
        /// <remarks>
        /// When assertions of this kind fail, they throw an <see cref="ArgumentException"/> or a derivative.
        /// </remarks>
        Argument,

        /// <summary>
        /// An operation-related assertion.
        /// </summary>
        /// <remarks>
        /// When assertions of this kind fail, they throw an <see cref="InvalidOperationException"/>.
        /// </remarks>
        Operation,

        /// <summary>
        /// A test-verifying assertion.
        /// </summary>
        /// <remarks>
        /// When assertions of this kind fail, they throw an <see cref="TestAssertionVerificationFailedException"/>.
        /// </remarks>
        Test,
    }
}
