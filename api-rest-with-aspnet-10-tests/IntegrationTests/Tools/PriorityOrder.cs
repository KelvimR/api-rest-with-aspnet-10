using System;
using System.Collections.Generic;
using System.Text;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace api_rest_with_aspnet_10_tests.IntegrationTests.Tools;

public class PriorityOrder : ITestCaseOrderer
{
    
    public IEnumerable<TTestCase> OrderTestCases<TTestCase>(IEnumerable<TTestCase> testCases) where TTestCase : ITestCase
    {
        var sortedMethods = testCases.OrderBy(
            tc => tc.TestMethod.Method
                .GetCustomAttributes(typeof(TestPriorityAttibute))
                .FirstOrDefault()
                ?.GetNamedArgument<int>("Priority") ?? 0
        );

        return sortedMethods;

    }

    //Oque esse metodo faz ?
    // 
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class TestPriorityAttibute : Attribute
    {
        public int Priority { get; }
        public TestPriorityAttibute(int priority) => Priority = priority;        
    }
}

