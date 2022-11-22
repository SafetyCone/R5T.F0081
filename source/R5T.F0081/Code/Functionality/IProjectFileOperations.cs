using System;
using System.Threading.Tasks;
using System.Xml.Linq;

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
        /// Creates the standard web project file.
        /// </summary>
        public async Task CreateNewProjectFile_Web(string projectFilePath)
        {
            await ProjectFileOperator.Instance.CreateNewProjectFile(
                projectFilePath,
                ProjectXmlOperations.Instance.GetSetupProjectElement_Web(
                    projectFilePath));
        }

		/// <summary>
		/// Creates the standard Blazor server web project file.
		/// </summary>
		public async Task CreateNewProjectFile_WebServerForBlazorClient(string projectFilePath)
		{
			await ProjectFileOperator.Instance.CreateNewProjectFile(
				projectFilePath,
				ProjectXmlOperations.Instance.GetSetupProjectElement_WebServerForBlazorClient(
					projectFilePath));
		}

        /// <summary>
		/// Creates the standard Blazor server web project file.
		/// </summary>
		public async Task CreateNewProjectFile_WebServerForBlazorClient(string projectFilePath)
        {
            await ProjectFileOperator.Instance.CreateNewProjectFile(
                projectFilePath,
                ProjectXmlOperations.Instance.GetSetupProjectElement_WebServerForBlazorClient(
                    projectFilePath));
        }

        /// <summary>
        /// Creates the standard library project file.
        /// </summary>
        public async Task CreateNewProjectFile_Library(string projectFilePath)
        {
            await ProjectFileOperator.Instance.CreateNewProjectFile(
                projectFilePath,
                ProjectXmlOperations.Instance.GetSetupProjectElement_Library(
                    projectFilePath));
        }

        /// <summary>
        /// Creates the standard library project file.
        /// </summary>
        public async Task CreateNewProjectFile_Console(string projectFilePath)
        {
            await ProjectFileOperator.Instance.CreateNewProjectFile(
                projectFilePath,
                ProjectXmlOperations.Instance.GetSetupProjectElement_Console(
                    projectFilePath));
        }

        /// <summary>
		/// Creates a project file with a project element with the SDK attribute with the standard .NET value.
		/// </summary>
		public async Task Create_OnlyProjectElementWithSdk(string projectFilePath)
        {
            await ProjectFileOperator.Instance.CreateNewProjectFile(
                projectFilePath,
                ProjectXmlOperations.Instance.GetSetupProjectElement_SdkOnly());
        }

        /// <summary>
		/// Creates a project file with *only* the project element.
		/// </summary>
		public async Task Create_OnlyProjectElement(string projectFilePath)
        {
            await ProjectFileOperator.Instance.CreateNewProjectFile(
                projectFilePath);
        }

        /// <summary>
        /// Resolves the NETSDK1138: The target framework is out of support warning by adding the CheckEolTargetFramework=false property to the project file.
        /// </summary>
        public async Task Resolve_NETSDK1138_TargetFrameworkOutOfSupportWarning_NoCheck(
            string projectFilePath)
        {
            await ProjectFileOperator.Instance.SetCheckEolTargetFramework(
                projectFilePath,
                false);
        }
    }
}