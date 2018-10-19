﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvHelper.Tests.Serializing
{
	[TestClass]
    public class SanitizationTests
    {    
		[TestMethod]
		public void NoQuoteTest()
		{
			using( var writer = new StringWriter() )
			using( var csv = new CsvSerializer( writer ) )
			{
				csv.Configuration.SanitizeForInjection = true;
				csv.Write( new[] { "=one" } );
				writer.Flush();

				Assert.AreEqual( "\t=one", writer.ToString() );
			}
		}

		[TestMethod]
		public void QuoteTest()
		{
			using( var writer = new StringWriter() )
			using( var csv = new CsvSerializer( writer ) )
			{
				csv.Configuration.SanitizeForInjection = true;
				csv.Write( new[] { "\"=one\"" } );
				writer.Flush();

				Assert.AreEqual( "\"\t=one\"", writer.ToString() );
			}
		}

		[TestMethod]
		public void NoQuoteChangeEscapeCharacterTest()
		{
			using( var writer = new StringWriter() )
			using( var csv = new CsvSerializer( writer ) )
			{
				csv.Configuration.SanitizeForInjection = true;
				csv.Configuration.InjectionEscapeCharacter = '\'';
				csv.Write( new[] { "=one" } );
				writer.Flush();

				Assert.AreEqual( "'=one", writer.ToString() );
			}
		}

		[TestMethod]
		public void QuoteChangeEscapeCharacterTest()
		{
			using( var writer = new StringWriter() )
			using( var csv = new CsvSerializer( writer ) )
			{
				csv.Configuration.SanitizeForInjection = true;
				csv.Configuration.InjectionEscapeCharacter = '\'';
				csv.Write( new[] { "\"=one\"" } );
				writer.Flush();

				Assert.AreEqual( "\"'=one\"", writer.ToString() );
			}
		}
	}
}
