﻿// ****************************************************************
// Copyright 2007, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org.
// ****************************************************************

using System;
using System.CodeDom.Compiler;

namespace NUnit.Framework.Syntax
{
    class TestCompiler
    {
        Microsoft.CSharp.CSharpCodeProvider provider;
#if !NET_2_0
		ICodeCompiler compiler;
#endif
		CompilerParameters options;

		public TestCompiler() : this( null, null ) { }

		public TestCompiler( string[] assemblyNames ) : this( assemblyNames, null ) { }

		public TestCompiler( string[] assemblyNames, string outputName )
		{
			this.provider = new Microsoft.CSharp.CSharpCodeProvider();
#if !NET_2_0
			this.compiler = provider.CreateCompiler();
#endif
			this.options = new CompilerParameters();

			if ( assemblyNames != null && assemblyNames.Length > 0 )
				options.ReferencedAssemblies.AddRange( assemblyNames );
			if ( outputName != null )
				options.OutputAssembly = outputName;

			options.IncludeDebugInformation = false;
			options.TempFiles = new TempFileCollection( ".", false );
			options.GenerateInMemory = false;
		}

		public CompilerParameters Options
		{
			get { return options; }
		}

		public CompilerResults CompileCode( string code )
		{
#if NET_2_0
			return provider.CompileAssemblyFromSource( options, code );
#else
            return compiler.CompileAssemblyFromSource(options, code);
#endif
        }
    }
}
