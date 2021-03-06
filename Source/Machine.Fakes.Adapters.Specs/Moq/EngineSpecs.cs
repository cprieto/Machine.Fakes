using System;
using System.ComponentModel.Design;
using Machine.Fakes.Adapters.Moq;
using Machine.Fakes.Adapters.Specs.SampleCode;
using Machine.Fakes.Internal;
using Machine.Specifications;

namespace Machine.Fakes.Adapters.Specs.Moq
{
    [Subject(typeof(MoqFakeEngine))]
    [Tags("Moq")]
    public class AfterInitializingANewFakeCurrentEngine : WithCurrentEngine<MoqFakeEngine>
    {
        static IServiceContainer _fake;

        Because of = () => _fake = FakeEngineGateway.Fake<IServiceContainer>();

        It should_be_able_to_create_an_instance = () => _fake.ShouldNotBeNull();
    }

    [Subject(typeof(MoqFakeEngine))]
    [Tags("Verifying a mock (without inline constaints)", "Moq")]
    public class Given_that_a_call_was_not_expected_to_happen_but_happened_when_verifying : WithCurrentEngine<MoqFakeEngine>
    {
        static Exception _exception;
        static IServiceContainer _fake;

        Establish context = () =>
        {
            _fake = FakeEngineGateway.Fake<IServiceContainer>();
            _fake.RemoveService(null);
        };

        Because of = () => _exception = Catch.Exception(() => _fake.WasNotToldTo(f => f.RemoveService(null)));

        It should_have_thrown_an_exception = () => _exception.ShouldNotBeNull();
    }

    [Subject(typeof(MoqFakeEngine))]
    [Tags("Verifying a mock (without inline constaints)", "Moq")]
    public class Given_that_a_call_was_not_expected_to_happen_and_did_not_happened_when_verifying :
        WithCurrentEngine<MoqFakeEngine>
    {
        static Exception _exception;
        static IServiceContainer _fake;

        Establish context = () => _fake = FakeEngineGateway.Fake<IServiceContainer>();

        Because of = () => _exception = Catch.Exception(() => _fake.WasNotToldTo(f => f.RemoveService(null)));

        It should_not_have_thrown_an_exception = () => _exception.ShouldBeNull();
    }

    [Subject(typeof(MoqFakeEngine))]
    [Tags("Moq", "Constructing an instance")]
    public class Now_we_can_initialize_a_class_with_no_default_ctor :
        WithCurrentEngine<MoqFakeEngine>
    {
        static DummyNoDefaultCtorClass _fake;
        static object[] _args = new object[] {1};

        Because of = () => _fake = FakeEngineGateway.Fake<DummyNoDefaultCtorClass>(_args);

        It should_be_able_to_create_an_instance = () => _fake.ShouldNotBeNull();
    }
}