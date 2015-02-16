using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace FluentCommandLineParser
{
	internal static class TypeExtensions
	{
		internal static IEnumerable<Type> GetGenericTypeArguments(this Type type)
		{
#if PORTABLE
			return type.GetTypeInfo().GenericTypeArguments;
#else
			return type.GetGenericArguments();
#endif
		}

		internal static 
#if PORTABLE
			TypeInfo
#else
			Type
#endif
			GetTypeInfoEx(this Type type)
		{
#if PORTABLE
			return type.GetTypeInfo();
#else
			return type;
#endif
		}
	}
}