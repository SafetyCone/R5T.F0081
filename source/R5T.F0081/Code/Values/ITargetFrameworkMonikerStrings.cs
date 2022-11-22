using System;

using R5T.T0131;


namespace R5T.F0081
{
	[ValuesMarker]
	public partial interface ITargetFrameworkMonikerStrings : IValuesMarker,
		F0020.ITargetFrameworkMonikerStrings
	{
		/// <summary>
		/// The standard target framwork for libraries is: <see cref="F0020.ITargetFrameworkMonikerStrings.NET_Standard2_1"/>.
		/// </summary>
		public string StandardForLibrary => this.NET_Standard2_1;

        /// <summary>
        /// The standard target framwork for consoles is: <see cref="F0020.ITargetFrameworkMonikerStrings.NET_6"/>.
        /// </summary>
        public string StandardForConsole => this.NET_6;

        /// <summary>
        /// The standard target framwork for web projects is: <see cref="F0020.ITargetFrameworkMonikerStrings.NET_6"/>.
        /// </summary>
        public string StandardForWeb => this.NET_6;
    }
}