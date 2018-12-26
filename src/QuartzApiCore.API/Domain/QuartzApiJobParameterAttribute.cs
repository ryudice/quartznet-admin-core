using System;

namespace QuartzApiCore.API.Domain
{
    [AttributeUsage(AttributeTargets.Class)]
    public class QuartzApiJobParameterAttribute : Attribute
    {
        public string Name { get; }

        public QuartzApiJobParameterAttribute(string name)
        {
            Name = name;
        }
    }
}