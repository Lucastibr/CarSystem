using System;

namespace Eventon.Core.Domain.Base
{
    [AttributeUsage(AttributeTargets.Property)]
    public class SequentialAttribute : Attribute
    {
        public int InitialSequence { get; }

        public string[] GroupByProperties { get; }

        public SequentialAttribute(int initialSequence = 0, params string[] groupByProperties)
        {
            InitialSequence   = initialSequence;
            GroupByProperties = groupByProperties;
        }
    }
}
