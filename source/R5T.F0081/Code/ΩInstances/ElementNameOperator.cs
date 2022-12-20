using System;


namespace R5T.F0081
{
    public class ElementNameOperator : IElementNameOperator
    {
        #region Infrastructure

        public static IElementNameOperator Instance { get; } = new ElementNameOperator();


        private ElementNameOperator()
        {
        }

        #endregion
    }
}
