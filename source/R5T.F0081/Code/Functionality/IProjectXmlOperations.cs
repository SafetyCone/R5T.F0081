using System;
using System.Threading.Tasks;
using System.Xml.Linq;
using R5T.F0020;
using R5T.T0132;
using R5T.T0152.Code.Extensions;

namespace R5T.F0081
{
    [FunctionalityMarker]
	public partial interface IProjectXmlOperations : IFunctionalityMarker,
        F0020.IProjectXmlOperations
	{
        /// <summary>
        /// 
        /// </summary>
        public async Task<XElement> CreateNewProjectElement_Library(
            string expectedProjectFilePath,
            Func<XElement, Task> projectElementAction = default)
        {
            var output = await ProjectXmlOperator.Instance.CreateNewProjectElement(
                this.GetSetupProjectElement_Library(expectedProjectFilePath));

            await F0000.ActionOperator.Instance.Run(
                projectElementAction,
                output);

            return output;
        }

        public Func<XElement, Task> GetSetupProjectElement_Library(
            string expectedProjectFilePath)
        {
            Task Internal(XElement projectXElement)
            {
                this.SetupProjectElement_Library(
                    projectXElement,
                    expectedProjectFilePath);

                return Task.CompletedTask;
            }

            return Internal;
        }

        public Func<XElement, Task> GetSetupProjectElement_Console(
            string expectedProjectFilePath)
        {
            Task Internal(XElement projectXElement)
            {
                this.SetupProjectElement_Console(
                    projectXElement,
                    expectedProjectFilePath);

                return Task.CompletedTask;
            }

            return Internal;
        }

        public Func<XElement, Task> GetSetupProjectElement_Web(
            string expectedProjectFilePath)
        {
            Task Internal(XElement projectXElement)
            {
                this.SetupProjectElement_Web(
                    projectXElement,
                    expectedProjectFilePath);

                return Task.CompletedTask;
            }

            return Internal;
        }

		public Func<XElement, Task> GetSetupProjectElement_WebServerForBlazorClient(
			string expectedProjectFilePath)
		{
			Task Internal(XElement projectXElement)
			{
				this.SetupProjectElement_WebServerForBlazorClient(
					projectXElement,
					expectedProjectFilePath);

				return Task.CompletedTask;
			}

			return Internal;
		}

        public Func<XElement, Task> GetSetupProjectElement_WebBlazorClient(
            string expectedProjectFilePath)
        {
            Task Internal(XElement projectXElement)
            {
                this.SetupProjectElement_WebBlazorClient(
                    projectXElement,
                    expectedProjectFilePath);

                return Task.CompletedTask;
            }

            return Internal;
        }

        /// <summary>
        /// The main setup method for library project elements.
        /// Other setup methods that do not.
        /// </summary>
        public void SetupProjectElement_Library(
            XElement projectElement,
            string expectedProjectFilePath)
        {
            this.SetNetSdk(projectElement);
            this.SetLibraryTargetFramework(projectElement);
            this.AddLibraryProjectReferences(
                projectElement,
                expectedProjectFilePath);
        }

        public void SetupProjectElement_Console(
            XElement projectElement,
            string expectedProjectFilePath)
        {
            this.SetNetSdk(projectElement);
            this.SetConsoleTargetFramework(projectElement);
            this.SetConsoleOutputType(projectElement);
            this.AddConsoleProjectReferences(
                projectElement,
                expectedProjectFilePath);
        }

		public void SetupProjectElement_WebServerForBlazorClient(
		   XElement projectElement,
		   string expectedProjectFilePath)
		{
			this.SetWebSdk(projectElement);
			this.SetWebTargetFramework(projectElement);
            this.AddWebBlazorServerPackageReferences(projectElement);

			// Not setting any project reference depdencies, yet. So keep expected project file path input.
		}

        public void SetupProjectElement_WebBlazorClient(
           XElement projectElement,
           string expectedProjectFilePath)
        {
            this.SetBlazorWebAssemblySdk(projectElement);
            this.SetWebTargetFramework(projectElement);
            this.AddWebBlazorClientPackageReferences(projectElement);

            // Not setting any project reference depdencies, yet. So keep expected project file path input.
        }

        public void AddWebBlazorServerPackageReferences(
            XElement projectElement)
        {
            ProjectXmlOperator.Instance.AddPackageReferences_Idempotent(
                projectElement,
                Z0020.Packages.Instance.ForWebBlazorServer.ToStringBasedPackageReferences());
        }

        public void AddWebBlazorClientPackageReferences(
            XElement projectElement)
        {
            ProjectXmlOperator.Instance.AddPackageReferences_Idempotent(
                projectElement,
                Z0020.Packages.Instance.ForWebBlazorClient.ToStringBasedPackageReferences());
        }

        public void SetupProjectElement_Web(
           XElement projectElement,
           string expectedProjectFilePath)
        {
            this.SetWebSdk(projectElement);
            this.SetWebTargetFramework(projectElement);

            // Not setting any project reference depdencies, yet. So keep expected project file path input.
        }

        public void AddLibraryProjectReferences(
            XElement projectElement,
            string expectedProjectFilePath)
        {
            ProjectXmlOperator.Instance.AddProjectReferences_Idempotent(
                projectElement,
                expectedProjectFilePath,
                Z0018.ProjectFilePaths.Instance.StandardDependenciesForLibrary);
        }

        public void AddConsoleProjectReferences(
            XElement projectElement,
            string expectedProjectFilePath)
        {
            ProjectXmlOperator.Instance.AddProjectReferences_Idempotent(
                projectElement,
                expectedProjectFilePath,
                Z0018.ProjectFilePaths.Instance.StandardDependenciesForConsole);
        }

        /// <summary>
        /// Sets the target framework to the standard framwork for libraries (<see cref="ITargetFrameworkMonikerStrings.StandardForLibrary"/>).
        /// </summary>
        public void SetLibraryTargetFramework(XElement projectElement)
        {
            ProjectXmlOperator.Instance.SetTargetFramework(
                projectElement,
                TargetFrameworkMonikerStrings.Instance.StandardForLibrary);
        }

        /// <summary>
        /// Sets the target framework to the standard framwork for consoles (<see cref="ITargetFrameworkMonikerStrings.StandardForConsole"/>).
        /// </summary>
        public void SetConsoleTargetFramework(XElement projectElement)
        {
            ProjectXmlOperator.Instance.SetTargetFramework(
                projectElement,
                TargetFrameworkMonikerStrings.Instance.StandardForConsole);
        }

        /// <summary>
        /// Sets the target framework to the standard framwork for libraries (<see cref="ITargetFrameworkMonikerStrings.StandardForLibrary"/>).
        /// </summary>
        public void SetWebTargetFramework(XElement projectElement)
        {
            ProjectXmlOperator.Instance.SetTargetFramework(
                projectElement,
                TargetFrameworkMonikerStrings.Instance.StandardForWeb);
        }

        /// <summary>
        /// Sets the target framework to the standard framwork for consoles (<see cref="ITargetFrameworkMonikerStrings.StandardForConsole"/>).
        /// </summary>
        public void SetConsoleOutputType(XElement projectElement)
        {
            ProjectXmlOperator.Instance.SetOutputType(
                projectElement,
                OutputTypeStrings.Instance.Console);
        }

        public Func<XElement, Task> GetSetupProjectElement_SdkOnly()
        {
            Task Internal(XElement projectXElement)
            {
                this.SetupProjectElement_SdkOnly(
                    projectXElement);

                return Task.CompletedTask;
            }

            return Internal;
        }

        /// <summary>
        /// Sets the SDK for a project element.
        /// </summary>
        public void SetupProjectElement_SdkOnly(
            XElement projectElement)
        {
            this.SetNetSdk(projectElement);
        }

        /// <summary>
        /// Sets the SDK to the standard .NET SDK (<see cref="F0020.IProjectSdkStrings.NET"/>).
        /// </summary>
        public void SetNetSdk(XElement projectElement)
        {
            ProjectXmlOperator.Instance.SetSdk(
                projectElement,
                Instances.ProjectSdkStrings.NET);
        }

        /// <summary>
        /// Sets the SDK to the standard .NET SDK (<see cref="F0020.IProjectSdkStrings.Web"/>).
        /// </summary>
        public void SetWebSdk(XElement projectElement)
        {
            ProjectXmlOperator.Instance.SetSdk(
                projectElement,
                Instances.ProjectSdkStrings.Web);
        }

        /// <summary>
        /// Sets the SDK to the standard .NET SDK (<see cref="F0020.IProjectSdkStrings.Web"/>).
        /// </summary>
        public void SetBlazorWebAssemblySdk(XElement projectElement)
        {
            ProjectXmlOperator.Instance.SetSdk(
                projectElement,
                Instances.ProjectSdkStrings.BlazorWebAssembly);
        }
    }
}