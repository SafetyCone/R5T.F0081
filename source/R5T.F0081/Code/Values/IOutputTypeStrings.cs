using System;

using R5T.T0131;


namespace R5T.F0081
{
	[ValuesMarker]
	public partial interface IOutputTypeStrings : IValuesMarker,
		F0020.IOutputTypeStrings
	{
        /// <summary>
        /// The output type for consoles is: <see cref="F0020.IOutputTypeStrings.Exe"/>.
        /// </summary>
        public string Console => this.Exe;
	}
}