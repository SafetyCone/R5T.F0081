using System;
using System.Threading.Tasks;

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

        public async Task NewProjectFile_DeployScripts(string projectFilePath)
        {
            await ProjectFileOperator.Instance.CreateProjectFile(
                projectFilePath,
                ProjectXmlOperations.Instance.SetupProjectElement_DeployScripts);
        }

        public async Task NewProjectFile_WindowsFormsLibrary(string projectFilePath)
        {
            await ProjectFileOperator.Instance.CreateProjectFile(
                projectFilePath,
                ProjectXmlOperations.Instance.SetupProjectElement_WindowsFormsLibrary);
        }

        public async Task NewProjectFile_WindowsFormsApplication(string projectFilePath)
        {
            await ProjectFileOperator.Instance.CreateProjectFile(
                projectFilePath,
                ProjectXmlOperations.Instance.SetupProjectElement_WindowsFormsApplication);
        }

        /// <summary>
        /// Creates the standard Blazor server web project file.
        /// </summary>
        public async Task NewProjectFile_WebServerForBlazorClient(string projectFilePath)
        {
            await ProjectFileOperator.Instance.CreateProjectFile(
                projectFilePath,
                ProjectXmlOperations.Instance.SetupProjectElement_WebServerForBlazorClient);
        }

        /// <summary>
        /// Creates the standard web project file.
        /// </summary>
        public async Task NewProjectFile_Web(string projectFilePath)
        {
            await ProjectFileOperator.Instance.CreateProjectFile(
                projectFilePath,
                ProjectXmlOperations.Instance.SetupProjectElement_Web);
        }

        /// <summary>
        /// Creates the standard web project file.
        /// </summary>
        public async Task NewProjectFile_Net6WebAssemblyServerProject(string projectFilePath)
        {
            await ProjectFileOperator.Instance.CreateProjectFile(
                projectFilePath,
                ProjectXmlOperations.Instance.SetupProjectElement_Net6WebAssemblyServer);
        }

        public async Task NewProjectFile_WebStaticRazorComponents(string projectFilePath)
        {
            await ProjectFileOperator.Instance.CreateProjectFile(
                projectFilePath,
                ProjectXmlOperations.Instance.SetupProjectElement_WebStaticRazorComponents);
        }

        /// <summary>
        /// Creates the standard Blazor server web project file.
        /// </summary>
        public async Task NewProjectFile_WebBlazorClient(string projectFilePath)
        {
            await ProjectFileOperator.Instance.CreateProjectFile(
                projectFilePath,
                ProjectXmlOperations.Instance.SetupProjectElement_WebBlazorClient);
        }

        public async Task NewProjectFile_RazorClassLibrary(string projectFilePath)
        {
            await ProjectFileOperator.Instance.CreateProjectFile(
                projectFilePath,
                ProjectXmlOperations.Instance.SetupProjectElement_RazorClassLibrary);
        }

        /// <summary>
        /// Creates the standard library project file.
        /// </summary>
        public async Task NewProjectFile_Console(string projectFilePath)
        {
            await ProjectFileOperator.Instance.CreateProjectFile(
                projectFilePath,
                ProjectXmlOperations.Instance.SetupProjectElement_Console);
        }

        /// <summary>
        /// Creates the standard library project file.
        /// </summary>
        public async Task NewProjectFile_Library(string projectFilePath)
        {
            await ProjectFileOperator.Instance.CreateProjectFile(
                projectFilePath,
                ProjectXmlOperations.Instance.SetupProjectElement_Library);
        }

        public async Task NewProjectFile_OnlyProjectElement(string projectFilePath)
        {
            await ProjectFileOperator.Instance.CreateProjectFile(
                projectFilePath,
                ProjectXmlOperations.Instance.SetupProjectElement_OnlyProjectElement);
        }

        public async Task NewProjectFile_OnlyProjectElementWithSdk(string projectFilePath)
        {
            await ProjectFileOperator.Instance.CreateProjectFile(
                projectFilePath,
                ProjectXmlOperations.Instance.SetupProjectElement_OnlyProjectElementWithSdk);
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