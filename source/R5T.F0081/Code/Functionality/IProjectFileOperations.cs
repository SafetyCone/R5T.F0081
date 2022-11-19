using System;
using System.Threading.Tasks;

using R5T.F0020;
using R5T.T0132;


namespace R5T.F0081
{
    /// <summary>
    /// Simple project file operations.
    /// </summary>
	[FunctionalityMarker]
	public partial interface IProjectFileOperations : IFunctionalityMarker
	{
        /// <summary>
        /// Resolves the NETSDK1138: The target framework is out of support warning by adding the CheckEolTargetFramework=false property to the project file.
        /// </summary>
        public async Task Resolve_NETSDK1138_TargetFrameworkOutOfSupportWarning_NoCheck(string projectFilePath)
        {
            await ProjectFileOperator.Instance.SetCheckEolTargetFramework(
                projectFilePath,
                false);
        }
    }
}