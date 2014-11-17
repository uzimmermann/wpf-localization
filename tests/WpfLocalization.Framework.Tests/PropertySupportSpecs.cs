using System;
using Machine.Specifications;

namespace WpfLocalization.Framework.Tests
{
    public class PropertySupportSpecs
    {
        [Subject(typeof(PropertySupport))]
        public class When_extracting_property_name_and_no_lambda_expression_is_given
        {
            static Exception Exception;

            Because of = () => Exception = Catch.Exception(() => PropertySupport.ExtractPropertyName<string>(null));

            It should_fail = () => Exception.ShouldNotBeNull();
        }
    }
}
