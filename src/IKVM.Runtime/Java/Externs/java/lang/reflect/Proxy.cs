﻿/*
  Copyright (C) 2007-2013 Jeroen Frijters

  This software is provided 'as-is', without any express or implied
  warranty.  In no event will the authors be held liable for any damages
  arising from the use of this software.

  Permission is granted to anyone to use this software for any purpose,
  including commercial applications, and to alter it and redistribute it
  freely, subject to the following restrictions:

  1. The origin of this software must not be misrepresented; you must not
     claim that you wrote the original software. If you use this software
     in a product, an acknowledgment in the product documentation would be
     appreciated but is not required.
  2. Altered source versions must be plainly marked as such, and must not be
     misrepresented as being the original software.
  3. This notice may not be removed or altered from any source distribution.

  Jeroen Frijters
  jeroen@frijters.net
  
*/
using System;

using IKVM.Internal;

namespace IKVM.Java.Externs.java.lang.reflect
{

	static class Proxy
	{

		public static object defineClass0(global::java.lang.ClassLoader classLoader, string name, byte[] b, int off, int len)
		{
			return IKVM.Java.Externs.java.lang.ClassLoader.defineClass1(classLoader, name, b, off, len, null, null);
		}

		public static global::java.lang.Class getPrecompiledProxy(global::java.lang.ClassLoader classLoader, string proxyName, global::java.lang.Class[] interfaces)
		{
			AssemblyClassLoader acl = ClassLoaderWrapper.GetClassLoaderWrapper(classLoader) as AssemblyClassLoader;
			if (acl == null)
			{
				return null;
			}
			TypeWrapper[] wrappers = new TypeWrapper[interfaces.Length];
			for (int i = 0; i < wrappers.Length; i++)
			{
				wrappers[i] = TypeWrapper.FromClass(interfaces[i]);
			}
			// TODO support multi assembly class loaders
			Type type = acl.MainAssembly.GetType(TypeNameUtil.GetProxyName(wrappers));
			if (type == null)
			{
				return null;
			}
			TypeWrapper tw = CompiledTypeWrapper.newInstance(proxyName, type);
			TypeWrapper tw2 = acl.RegisterInitiatingLoader(tw);
			if (tw != tw2)
			{
				return null;
			}
			// we need to explicitly register the type, because the type isn't visible by normal means
			tw.GetClassLoader().SetWrapperForType(type, tw);
			TypeWrapper[] wrappers2 = tw.Interfaces;
			if (wrappers.Length != wrappers.Length)
			{
				return null;
			}
			for (int i = 0; i < wrappers.Length; i++)
			{
				if (wrappers[i] != wrappers2[i])
				{
					return null;
				}
			}
			return tw.ClassObject;
		}

	}

}