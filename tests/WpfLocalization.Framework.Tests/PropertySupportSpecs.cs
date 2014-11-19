using System;
using Machine.Specifications;

namespace WpfLocalization.Framework.Tests
{
    public class PropertySupportSpecs
    {
        [Subject(typeof(PropertySupport))]
        public class When_extracting_property_name_and_no_expression_tree_is_given
        {
            static Exception Exception;

            Because of = () => Exception = Catch.Exception(() => PropertySupport.ExtractPropertyName<string>(null));

            It should_fail = () => Exception.ShouldNotBeNull();
            It should_fail_with_an__ArgumentNullException__ = () => Exception
                .ShouldBeOfExactType<ArgumentNullException>();
        }

        [Subject(typeof(PropertySupport))]
        public class When_extracting_property_name_and_no_member_expression_is_given
        {
            static Exception Exception;

            Because of = () => Exception = Catch.Exception(() => PropertySupport.ExtractPropertyName(
                () => SomeClass.SomeTestMethod()));

            It should_fail = () => Exception.ShouldNotBeNull();
            It should_fail_with_an__ArgumentException__ = () => Exception
                .ShouldBeOfExactType<ArgumentException>();
            It should_fail_with_an__ArgumentException__with_our_custom_message = () => Exception
                .Message.ShouldEqual("The expression is not a member access expression." + Environment.NewLine +
                    "Parametername: propertyExpression");
        }

        [Subject(typeof(PropertySupport))]
        public class When_extracting_property_name_and_no_property_is_accessed
        {
            static Exception Exception;

            Because of = () => Exception = Catch.Exception(() => PropertySupport.ExtractPropertyName(
                () => new SomeClass().SomeStringField));

            It should_fail = () => Exception.ShouldNotBeNull();
            It should_fail_with_an__ArgumentException__ = () => Exception
                .ShouldBeOfExactType<ArgumentException>();
            It should_fail_with_an__ArgumentException__with_our_custom_message = () => Exception
                .Message.ShouldEqual("The member access expression does not access a property." + Environment.NewLine +
                    "Parametername: propertyExpression");
        }

        [Subject(typeof(PropertySupport))]
        public class When_extracting_property_name_and_the_referenced_property_is_a_static_one
        {
            static Exception Exception;

            Because of = () => Exception = Catch.Exception(() => PropertySupport.ExtractPropertyName<string>(
                () => SomeClass.SomeStaticStringProperty));

            It should_fail = () => Exception.ShouldNotBeNull();
            It should_fail_with_an__ArgumentException__ = () => Exception
                .ShouldBeOfExactType<ArgumentException>();
            It should_fail_with_an__ArgumentException__with_our_custom_message = () => Exception
                .Message.ShouldEqual("The referenced property is a static property." + Environment.NewLine +
                    "Parametername: propertyExpression");
        }

        [Subject(typeof(PropertySupport))]
        public class When_extracting_property_name_for_property__SomeStringProperty__
        {
            static string Result;

            Because of = () => Result = PropertySupport.ExtractPropertyName(() => new SomeClass().SomeStringProperty);

            It should_yield__SomeStringProperty__ = () => Result.ShouldEqual("SomeStringProperty");
        }

        public class SomeClass
        {
            public string SomeStringField;
            
            public static string SomeStaticStringProperty { get { return "some value"; } }

            public string SomeStringProperty { get; set; }

            public static string SomeTestMethod()
            {
                return null;
            }
        }
    }
}
