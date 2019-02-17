using System;

namespace Cqrs.WebApi.Infrastructure
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class ApiFeatureAttribute : Attribute
    {
        public ApiFeatureAttribute(string name, int order)
        {
            Name = name;
            Order = order;
        }

        public string Name { get; }
        public int Order { get; }
    }
}