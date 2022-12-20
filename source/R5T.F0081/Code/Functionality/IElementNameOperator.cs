using System;

using R5T.F0020;
using R5T.T0132;


namespace R5T.F0081
{
    [FunctionalityMarker]
    public partial interface IElementNameOperator : IFunctionalityMarker
    {
        public string[] GetOrderedElementNames()
        {
            var orderedPropertyElementNames = new[]
            {
                ElementNames.Instance.OutputType,
                ElementNames.Instance.TargetFramework,
            };

            return orderedPropertyElementNames;
        }
    }
}
