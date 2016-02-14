using System;
using System.Collections.Generic;
using Zenject;
using NUnit.Framework;
using ModestTree;
using Assert=ModestTree.Assert;

namespace Zenject.Tests
{
    [TestFixture]
    public class TestConditionsTargetInstance : TestWithContainer
    {
        class Test0
        {
        }

        class Test1
        {
            [Inject]
            public Test0 test0 = null;
        }

        Test1 _test1;

        public override void Setup()
        {
            base.Setup();

            _test1 = new Test1();
            Binder.Bind<Test0>().ToSingle().When(r => r.ObjectInstance == _test1);
            Binder.Bind<Test1>().ToInstance(_test1);
        }

        [Test]
        public void TestTargetConditionError()
        {
            Resolver.Inject(_test1);

            Assert.That(_test1.test0 != null);
        }
    }
}
