using System;
using System.Collections.Generic;
using System.Linq;
using Fclp.Internals.Extensions;
using NUnit.Framework;

namespace Fclp.Tests
{
    /// <summary>
    /// Tests for Uris
    /// </summary>
    [TestFixture]
    public class UriTests
    {
        public class ExampleArgsContainer
        {
            public string UriAsString { get; set; }
            public Uri Uri { get; set; }
        }

        [Test]
        public void GenericFclp_UriAsString()
        {
            const char shortKey = 'u';
            const string longKey = "uri";
            const string uri = "http://services.internal/backoffce/service/svc";

            var variations = CreateAllKeyVariations(shortKey, longKey, uri).ToList();

            foreach (var combination in variations)
            {
                var fclp = new FluentCommandLineParser<ExampleArgsContainer>();

                fclp.Setup(args => args.UriAsString)
                    .As(shortKey, longKey);

                var result = fclp.Parse(combination.Args);

                Assert.IsEmpty(result.Errors);
                Assert.IsEmpty(result.AdditionalOptionsFound);
                Assert.AreEqual(uri, fclp.Object.UriAsString);
            }
        }

        [Test]
        public void StandardFclp_UriAsString()
        {
            const char shortKey = 'u';
            const string longKey = "uri";
            const string uri = "http://services.internal/backoffce/service/svc";
            string uriAsString = null;
            var variations = CreateAllKeyVariations(shortKey, longKey, uri).ToList();

            foreach (var combination in variations)
            {
                var fclp = new Fclp.FluentCommandLineParser();

                fclp.Setup<string>(shortKey, longKey).Callback(val => uriAsString = val);

                var result = fclp.Parse(combination.Args);

                Assert.IsEmpty(result.Errors);
                Assert.IsEmpty(result.AdditionalOptionsFound);
                Assert.AreEqual(uri, uriAsString);
            }
        }

        [Test]
        public void GenericFclp_Uri()
        {
            const char shortKey = 'u';
            const string longKey = "uri";
            const string uri = "http://services.internal/backoffce/service/svc";

            var variations = CreateAllKeyVariations(shortKey, longKey, uri).ToList();

            foreach (var combination in variations)
            {
                var fclp = new FluentCommandLineParser<ExampleArgsContainer>();

                fclp.Setup(args => args.Uri)
                    .As(shortKey, longKey);

                var result = fclp.Parse(combination.Args);

                Assert.IsEmpty(result.Errors);
                Assert.IsEmpty(result.AdditionalOptionsFound);
                Assert.AreEqual(uri, fclp.Object.Uri.AbsoluteUri);
            }
        }

        [Test]
        public void StandardFclp_Uri()
        {
            const char shortKey = 'u';
            const string longKey = "uri";
            const string uri = "http://services.internal/backoffce/service/svc";
            Uri actualUri = null;
            var variations = CreateAllKeyVariations(shortKey, longKey, uri).ToList();

            foreach (var combination in variations)
            {
                var fclp = new Fclp.FluentCommandLineParser();

                fclp.Setup<Uri>(shortKey, longKey).Callback(val => actualUri = val);

                var result = fclp.Parse(combination.Args);

                Assert.IsEmpty(result.Errors);
                Assert.IsEmpty(result.AdditionalOptionsFound);
                Assert.AreEqual(uri, actualUri.AbsoluteUri);
            }
        }

        private static IEnumerable<TestArguments> CreateAllKeyVariations(char shortKey, string longKey, string value)
        {
            return CreateKeyVariations(new[] { "-", "/" }, shortKey.ToString(), value)
                .Union(CreateKeyVariations(new[] { "--", "/" }, longKey, value));
        }

        private static IEnumerable<TestArguments> CreateKeyVariations(IEnumerable<string> keys, string option, string value)
        {
            var valueIdentifiers = new[] { '=', ':', ' ' };

            foreach (string key in keys)
            {
                foreach (char valueIdentifier in valueIdentifiers)
                {
                    yield return new TestArguments(key, option, valueIdentifier, value);
                }
            }
        }

        public class TestArguments
        {
            public TestArguments(string key, string option, char valueIdentifier, string value)
            {
                FriendlyArgs = string.Format("{0}{1}{2}{3}", key, option, valueIdentifier, value);
            }

            public string[] Args { get { return ParseArguments(FriendlyArgs); } }
            public string FriendlyArgs { get; set; }

            static string[] ParseArguments(string args)
            {
                args = ReplaceWithDoubleQuotes(args);
                return args.SplitOnWhitespace().ToArray();
            }

            static string ReplaceWithDoubleQuotes(string args)
            {
                if (args == null) return null;
                return args.Replace('\'', '"');
            }
        }
    }
}
