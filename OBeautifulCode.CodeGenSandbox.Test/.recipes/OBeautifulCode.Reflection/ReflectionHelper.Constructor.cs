// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReflectionHelper.Constructor.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// <auto-generated>
//   Sourced from NuGet package. Will be overwritten with package update except in OBeautifulCode.Reflection.Recipes source.
// </auto-generated>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Reflection.Recipes
{
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Diagnostics.CodeAnalysis;
    using global::System.Linq;
    using global::System.Reflection;

    using OBeautifulCode.CodeAnalysis.Recipes;

    using static global::System.FormattableString;

#if !OBeautifulCodeReflectionSolution
    internal
#else
    public
#endif
    static partial class ReflectionHelper
    {
        /// <summary>
        /// Gets the constructors of the specified type,
        /// with various options to control the scope of constructors included and optionally order the constructors.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="memberRelationships">OPTIONAL value that scopes the search for members based on their relationship to <paramref name="type"/>.  DEFAULT is to include the members declared in or inherited by the specified type.</param>
        /// <param name="memberOwners">OPTIONAL value that scopes the search for members based on who owns the member.  DEFAULT is to include members owned by an object or owned by the type itself.</param>
        /// <param name="memberAccessModifiers">OPTIONAL value that scopes the search for members based on access modifiers.  DEFAULT is to include members having any supported access modifier.</param>
        /// <param name="memberAttributes">OPTIONAL value that scopes the search for members based on the presence or absence of certain attributes on those members.  DEFAULT is to include members that are not compiler generated.</param>
        /// <param name="orderMembersBy">OPTIONAL value that specifies how to the members.  DEFAULT is return the members in no particular order.</param>
        /// <returns>
        /// The constructors in the specified order.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="type"/> is null.</exception>
        public static IReadOnlyList<ConstructorInfo> GetConstructorsFiltered(
            this Type type,
            MemberRelationships memberRelationships = MemberRelationships.DeclaredOrInherited,
            MemberOwners memberOwners = MemberOwners.All,
            MemberAccessModifiers memberAccessModifiers = MemberAccessModifiers.All,
            MemberAttributes memberAttributes = MemberAttributes.NotCompilerGenerated,
            OrderMembersBy orderMembersBy = OrderMembersBy.None)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            var result = type
                .GetMembersFiltered(memberRelationships, memberOwners, memberAccessModifiers, MemberKinds.Constructor, MemberMutability.All, memberAttributes, orderMembersBy)
                .Cast<ConstructorInfo>()
                .ToList();

            return result;
        }

        /// <summary>
        /// Constructs an object of the specified type.
        /// </summary>
        /// <param name="type">The type of object to construct.</param>
        /// <param name="parameters">
        /// An array of arguments that match in number, order, and type the parameters of the constructor to invoke.
        /// If an empty array or null, the constructor that takes no parameters (the default constructor) is invoked.
        /// </param>
        /// <returns>
        /// A reference to the newly created object.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="type"/> is null.</exception>
        /// <exception cref="Exception">Various exceptions thrown by <see cref="Activator.CreateInstance(Type, object[])"/>.</exception>
        public static object Construct(
            this Type type,
            params object[] parameters)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            var result = Activator.CreateInstance(type, parameters);

            return result;
        }

        /// <summary>
        /// Constructs an object of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of object to create.</typeparam>
        /// <param name="parameters">
        /// An array of arguments that match in number, order, and type the parameters of the constructor to invoke.
        /// If an empty array or null, the constructor that takes no parameters (the default constructor) is invoked.
        /// </param>
        /// <returns>
        /// A reference to the newly created object.
        /// </returns>
        /// <exception cref="Exception">Any exception thrown by <see cref="Activator.CreateInstance(Type, object[])"/>.</exception>
        public static T Construct<T>(
            params object[] parameters)
        {
            var result = typeof(T).Construct<T>(parameters);

            return result;
        }

        /// <summary>
        /// Constructs an object of the specified type.
        /// </summary>
        /// <typeparam name="T">The return type.</typeparam>
        /// <param name="type">The type of object to construct.</param>
        /// <param name="parameters">
        /// An array of arguments that match in number, order, and type the parameters of the constructor to invoke.
        /// If an empty array or null, the constructor that takes no parameters (the default constructor) is invoked.
        /// </param>
        /// <returns>
        /// A reference to the newly created object.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="type"/> is null.</exception>
        /// <exception cref="Exception">Any exception thrown by <see cref="Activator.CreateInstance(Type, object[])"/>.</exception>
        /// <exception cref="InvalidCastException">The created object could not be cast to a <typeparamref name="T"/>.</exception>
        public static T Construct<T>(
            this Type type,
            params object[] parameters)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            var objectResult = type.Construct(parameters);

            var result = (T)objectResult;

            return result;
        }

        /// <summary>
        /// Finds constructors having parameters that correspond to a set of properties, matching on name (case-insensitive) and type.
        /// </summary>
        /// <remarks>
        /// For type matching, we check that a constructor parameter type can be assigned it's corresponding property's type
        /// and vice-versa.  If either direction succeeds, we consider it a match.
        /// </remarks>
        /// <param name="classType">The class type.</param>
        /// <param name="properties">The properties.</param>
        /// <param name="matchStrategy">Determines which constructors will be deemed as matching.</param>
        /// <param name="memberAccessModifiers">OPTIONAL value that scopes the search for constructors based on access modifiers.  DEFAULT is to include public constructors.</param>
        /// <param name="memberRelationships">OPTIONAL value that scopes the search for constructors based on their relationship to <paramref name="classType"/>.  DEFAULT is to include constructors declared on the type.</param>
        /// <returns>
        /// The matching constructors, or an empty collection if there are no matches.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="classType"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="classType"/> is not a class.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="properties"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="properties"/> has a null element.</exception>
        /// <exception cref="ArgumentException"><paramref name="properties"/> contains two or more members with the same name.</exception>
        /// <exception cref="ArgumentException"><paramref name="matchStrategy"/> is <see cref="ConstructorsMatchedToPropertiesStrategy.Invalid"/>.</exception>
        [SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = ObcSuppressBecause.CA1502_AvoidExcessiveComplexity_DisagreeWithAssessment)]
        public static IReadOnlyCollection<ConstructorInfo> GetConstructorsMatchedToProperties(
            this Type classType,
            IReadOnlyCollection<PropertyInfo> properties,
            ConstructorsMatchedToPropertiesStrategy matchStrategy,
            MemberAccessModifiers memberAccessModifiers = MemberAccessModifiers.Public,
            MemberRelationships memberRelationships = MemberRelationships.DeclaredInType)
        {
            if (classType == null)
            {
                throw new ArgumentNullException(nameof(classType));
            }

            if (!classType.IsClass)
            {
                throw new ArgumentException(Invariant($"{nameof(classType)} must be a class."));
            }

            if (properties == null)
            {
                throw new ArgumentNullException(nameof(properties));
            }

            if (properties.Any(_ => _ == null))
            {
                throw new ArgumentException(Invariant($"{nameof(properties)} has a null element."));
            }

            if (properties.Select(_ => _.Name).Distinct(StringComparer.OrdinalIgnoreCase).Count() != properties.Count)
            {
                throw new ArgumentException(Invariant($"{nameof(properties)} contains two or more members with the same name."));
            }

            if (matchStrategy == ConstructorsMatchedToPropertiesStrategy.Invalid)
            {
                throw new ArgumentException(Invariant($"{nameof(matchStrategy)} is {nameof(ConstructorsMatchedToPropertiesStrategy.Invalid)}."));
            }

            var constructors = classType.GetConstructorsFiltered(memberRelationships, MemberOwners.Instance, memberAccessModifiers);

            var propertyNameToPropertyTypeMap = properties.ToDictionary(p => p.Name, p => p.PropertyType, StringComparer.OrdinalIgnoreCase);

            var candidates = constructors
                .Where(
                    _ =>
                    {
                        var parameters = _.GetParameters();

                        foreach (var parameter in parameters)
                        {
                            // property matching parameter by name?
                            if (!propertyNameToPropertyTypeMap.ContainsKey(parameter.Name))
                            {
                                return false;
                            }

                            var propertyType = propertyNameToPropertyTypeMap[parameter.Name];

                            // parameter type is assignable to property type OR vice-versa?
                            if ((!propertyType.IsAssignableFrom(parameter.ParameterType)) && (!parameter.ParameterType.IsAssignableFrom(propertyType)))
                            {
                                return false;
                            }
                        }

                        return true;
                    })
                .ToList();

            IReadOnlyCollection<ConstructorInfo> result;

            if (matchStrategy == ConstructorsMatchedToPropertiesStrategy.AllConstructorParametersHaveMatchingProperty)
            {
                result = candidates;
            }
            else if (matchStrategy == ConstructorsMatchedToPropertiesStrategy.AllConstructorParametersHaveMatchingPropertyWithNoUnmatchedProperties)
            {
                result = candidates
                    .Where(_ => _.GetParameters().Length == properties.Count)
                    .ToList();
            }
            else
            {
                throw new NotSupportedException(Invariant($"This {nameof(ConstructorsMatchedToPropertiesStrategy)} is not supported: {matchStrategy}."));
            }

            return result;
        }

        /// <summary>
        /// Determines if the specified constructor is the default (parameterless) constructor.
        /// </summary>
        /// <param name="constructor">The constructor.</param>
        /// <returns>
        /// true if the specified constructor is the default (parameterless) constructor; otherwise false.
        /// </returns>
        public static bool IsDefaultConstructor(
            this ConstructorInfo constructor)
        {
            if (constructor == null)
            {
                throw new ArgumentNullException(nameof(constructor));
            }

            var result = constructor.GetParameters().Length == 0;

            return result;
        }
    }
}
