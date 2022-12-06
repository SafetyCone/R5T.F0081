using System;
using System.Threading.Tasks;
using System.Xml.Linq;

using R5T.F0020;
using R5T.T0132;
using R5T.Z0020;

namespace R5T.F0081
{
    /// <summary>
    /// Simple project file operations.
    /// </summary>
	[FunctionalityMarker]
	public partial interface IProjectFileOperations : IFunctionalityMarker
	{
        /// <summary>
        /// Determine if a project is a Razor class library.
        /// </summary>
        public async Task<bool> Is_RazorComponentsLibrary(string projectFilePath)
        {
            var isRazorClassLibrary = await ProjectFileOperator.Instance.InQueryProjectFileContext(
                projectFilePath,
                projectElement =>
                {
                    // Test that the project file has Sdk="Microsoft.NET.Sdk.Razor".
                    var hasSdk = ProjectXmlOperator.Instance.HasSdk(projectElement);
                    if(!hasSdk)
                    {
                        return false;
                    }

                    var sdk = hasSdk.Result;

                    var isRazorSdk = ProjectSdkStringOperations.Instance.Is_RazorSdk(sdk);
                    if(!isRazorSdk)
                    {
                        return false;
                    }

                    // Test that the SupportedPlatform of "browser".
                    var hasSupportedPlatformElement = ProjectXmlOperator.Instance.HasSupportedPlatformElement(projectElement);
                    if(!hasSupportedPlatformElement)
                    {
                        return false;
                    }

                    var supportedPlatformElement = hasSupportedPlatformElement.Result;

                    var isForBrowser = SupportedPlatformsOperations.Instance.Is_ForBrowser(supportedPlatformElement);
                    if (!isForBrowser)
                    {
                        return false;
                    }

                    // Test that the project has a package reference to "Microsoft.AspNetCore.Components.Web".
                    var hasPackageReference = ProjectXmlOperator.Instance.HasPackageReferenceElement(projectElement,
                        PackageIdentities.Instance.Microsoft_AspNetCore_Components_Web);

                    if(!hasPackageReference)
                    {
                        return false;
                    }

                    // Finally, success.
                    return true;
                });

            return isRazorClassLibrary;
        }

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

        public async Task CreateNewProjectFile_WebStaticRazorComponents(string projectFilePath)
        {
            await ProjectFileOperator.Instance.CreateNewProjectFile(
                projectFilePath,
                ProjectXmlOperations.Instance.GetSetupProjectElement_WebStaticRazorComponents(
                    projectFilePath));
        }

        /// <summary>
		/// Creates the standard Blazor server web project file.
		/// </summary>
		public async Task CreateNewProjectFile_WebBlazorClient(string projectFilePath)
        {
            await ProjectFileOperator.Instance.CreateNewProjectFile(
                projectFilePath,
                ProjectXmlOperations.Instance.GetSetupProjectElement_WebBlazorClient(
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

        public async Task CreateNewProjectFile_RazorClassLibrary(string projectFilePath)
        {
            await ProjectFileOperator.Instance.CreateNewProjectFile(
                projectFilePath,
                ProjectXmlOperations.Instance.GetSetupProjectElement_RazorClassLibrary(
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