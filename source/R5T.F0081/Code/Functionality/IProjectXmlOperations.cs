using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

using R5T.F0000;
using R5T.F0020;
using R5T.T0132;
using R5T.T0152;
using R5T.T0152.Code.Extensions;
using R5T.Z0018;


namespace R5T.F0081
{
    [FunctionalityMarker]
	public partial interface IProjectXmlOperations : IFunctionalityMarker,
        F0020.IProjectXmlOperations
	{
        public IEnumerable<Action<XElement>> WithStandardModifiers(
            string expectedProjectFilePath,
            params Action<XElement>[] modifiers)
        {
            return this.WithStandardModifiers(
                expectedProjectFilePath,
                modifiers.AsEnumerable());
        }

        public IEnumerable<Action<XElement>> WithStandardModifiers(
            string expectedProjectFilePath,
            IEnumerable<Action<XElement>> modifiers)
        {
            var output = new[]
            {
                this.SetInitialProperties(expectedProjectFilePath),
            }
            .Append(modifiers)
            .Append(new Action<XElement>[]
            {
                this.OrderProperties,
            });

            return output;
        }

        public IEnumerable<Action<XElement>> SetupProjectElement_Library(string expectedProjectFilePath)
        {
            var output = this.WithStandardModifiers(
                expectedProjectFilePath,
                this.SetNetSdk,
                this.SetLibraryTargetFramework,
                this.AddLibraryProjectReferences(
                    expectedProjectFilePath));

            return output;
        }
        
        public IEnumerable<Action<XElement>> SetupProjectElement_Console_WithoutStandardModifiers(
            string expectedProjectFilePath)
        {
            var output = new[]
            {
                this.SetNetSdk,
                this.SetConsoleTargetFramework,
                this.SetConsoleOutputType,
                this.AddConsoleProjectReferences(
                    expectedProjectFilePath)
            };

            return output;
        }

        public IEnumerable<Action<XElement>> SetupProjectElement_Console(string expectedProjectFilePath)
        {
            var output = this.WithStandardModifiers(
                expectedProjectFilePath,
                this.SetupProjectElement_Console_WithoutStandardModifiers(expectedProjectFilePath));

            return output;
        }

        public IEnumerable<Action<XElement>> SetupProjectElement_WindowsForms(
            string expectedProjectFilePath,
            IEnumerable<Action<XElement>> setupProjectActions)
        {
            var allSetupProjectActions = new Action<XElement>[]
            {
                this.SetNetSdk,
                this.SetWindowsFormsTargetFramework,
                this.SetUseWindowsForms,
            }
            .Append(setupProjectActions);

            var output = this.WithStandardModifiers(
                expectedProjectFilePath,
                allSetupProjectActions);

            return output;
        }

        public IEnumerable<Action<XElement>> SetupProjectElement_DeployScripts(string expectedProjectFilePath)
        {
            var output = this.WithStandardModifiers(
                expectedProjectFilePath,
                this.SetupProjectElement_Console_WithoutStandardModifiers(expectedProjectFilePath)
                .Append(
                    this.SetProjectReferences(
                        expectedProjectFilePath,
                        ProjectFilePaths.Instance.DeployScriptsDependencies)));

                return output;
        }

        public Action<XElement> SetProjectReferences(
            string expectedProjectFilePath,
            IEnumerable<string> referenceProjectFilePaths)
        {
            return projectElement => ProjectXmlOperator.Instance.SetProjectReferences(
                projectElement,
                expectedProjectFilePath,
                referenceProjectFilePaths);
        }

        public IEnumerable<Action<XElement>> SetupProjectElement_WindowsForms(
            string expectedProjectFilePath,
            params Action<XElement>[] setupProjectActions)
        {
            return this.SetupProjectElement_WindowsForms(
                expectedProjectFilePath,
                setupProjectActions.AsEnumerable());
        }

        public IEnumerable<Action<XElement>> SetupProjectElement_WindowsFormsLibrary(string expectedProjectFilePath)
        {
            var output = this.SetupProjectElement_WindowsForms(expectedProjectFilePath,
                // No changes needed.
                Actions.Instance.None)
                ;

            return output;
        }

        public IEnumerable<Action<XElement>> SetupProjectElement_WindowsFormsApplication(string expectedProjectFilePath)
        {
            var output = this.SetupProjectElement_WindowsForms(expectedProjectFilePath,
                ProjectXmlOperations.Instance.SetWindowsExecutableOutputType);

            return output;
        }

        public IEnumerable<Action<XElement>> SetupProjectElement_WebServerForBlazorClient(string expectedProjectFilePath)
        {
            var output = this.WithStandardModifiers(
                expectedProjectFilePath,
                this.SetWebSdk,
                this.SetWebTargetFramework,
                this.AddWebBlazorServerPackageReferences);

            return output;
        }

        public IEnumerable<Action<XElement>> SetupProjectElement_Web(string expectedProjectFilePath)
        {
            var output = this.WithStandardModifiers(
                expectedProjectFilePath,
                this.SetWebSdk,
                this.SetWebTargetFramework);

            return output;
        }

        public IEnumerable<Action<XElement>> SetupProjectElement_Net6WebAssemblyServer(string expectedProjectFilePath)
        {
            var output = this.WithStandardModifiers(
                expectedProjectFilePath,
                this.SetWebSdk,
                this.SetWebTargetFramework,
                this.AddPackageReferences(
#pragma warning disable CS0618 // Type or member is obsolete
                    Z0020.Packages.Instance.Microsoft_AspNetCore_Components_WebAssembly_Server));
#pragma warning restore CS0618 // Type or member is obsolete

            return output;
        }

        public IEnumerable<Action<XElement>> SetupProjectElement_OnlyProjectElement(string expectedProjectFilePath)
        {
            // Do nothing.
            var output = Enumerable.Empty<Action<XElement>>();
            return output;
        }

        public IEnumerable<Action<XElement>> SetupProjectElement_OnlyProjectElementWithSdk(string expectedProjectFilePath)
        {
            // Do nothing.
            var output = new Action<XElement>[]
            {
                this.SetNetSdk,
            };

            return output;
        }

        public IEnumerable<Action<XElement>> SetupProjectElement_RazorClassLibrary(string expectedProjectFilePath)
        {
            var output = this.WithStandardModifiers(
                expectedProjectFilePath,
                this.SetRazorSdk,
                this.SetTargetFramework_NET6,
                this.AddBrowserSupportedPlatform,
                this.AddRazorClassLibraryPackageReferences);

            return output;
        }

        public IEnumerable<Action<XElement>> SetupProjectElement_WebBlazorClient(string expectedProjectFilePath)
        {
            var output = this.WithStandardModifiers(
                expectedProjectFilePath,
                this.SetBlazorWebAssemblySdk,
                this.SetWebTargetFramework,
                this.AddWebBlazorClientPackageReferences);

            return output;
        }

        public IEnumerable<Action<XElement>> SetupProjectElement_WebStaticRazorComponents()
        {
            var output = Instances.EnumerableOperator.From<Action<XElement>>(
                this.SetWebSdk,
                this.SetWebTargetFramework);

            return output;
        }

        public IEnumerable<Action<XElement>> SetupProjectElement_WebStaticRazorComponents(string expectedProjectFilePath)
        {
            var output = this.WithStandardModifiers(
                expectedProjectFilePath,
                this.SetupProjectElement_WebStaticRazorComponents());

            return output;
        }

        public IEnumerable<Action<XElement>> SetupProjectElement_Blog(string expectedProjectFilePath)
        {
            var output = this.WithStandardModifiers(
                expectedProjectFilePath,
                this.SetupProjectElement_WebStaticRazorComponents()
                    .Append(
                        this.SetProjectReferences(
                            expectedProjectFilePath,
                            ProjectFilePaths.Instance.BlogDependencies)));

            return output;
        }

        public void OrderProperties(XElement projectElement)
        {
            var orderedPropertyElementNames = ElementNameOperator.Instance.GetOrderedElementNames();

            var propertyGroupElement = ProjectXmlOperator.Instance.AcquireMainPropertyGroup(projectElement);

            var properties = XElementOperator.Instance.GetChildren(propertyGroupElement);

            var orderedProperties = properties.OrderByNames(
                XElementOperator.Instance.GetName,
                orderedPropertyElementNames);

            propertyGroupElement.ReplaceNodes(orderedProperties);
        }

        public Action<XElement> SetInitialProperties(
            string expectedProjectFilePath)
        {
            return projectElement =>
            {
                ProjectXmlOperator.Instance.SetGenerateDocumentationFile_True(projectElement);

                this.SetDisabledWarnings(projectElement);
            };
        }

        public Action<XElement> AddPackageReferences(
            params PackageReference[] packageReferences)
        {
            return this.AddPackageReferences(
                packageReferences.AsEnumerable());
        }

        public Action<XElement> AddPackageReferences(
            IEnumerable<PackageReference> packageReferences)
        {
            return projectElement =>
            {
                ProjectXmlOperator.Instance.AddPackageReferences_Idempotent(
                    projectElement,
                    packageReferences.ToStringBasedPackageReferences());
            };
        }

        public void AddWebBlazorServerPackageReferences(
            XElement projectElement)
        {
#pragma warning disable CS0618 // Type or member is obsolete
            ProjectXmlOperator.Instance.AddPackageReferences_Idempotent(
                projectElement,
                Z0020.Packages.Instance.ForWebBlazorServer.ToStringBasedPackageReferences());
#pragma warning restore CS0618 // Type or member is obsolete
        }

        public void AddWebBlazorClientPackageReferences(
            XElement projectElement)
        {
#pragma warning disable CS0618 // Type or member is obsolete
            ProjectXmlOperator.Instance.AddPackageReferences_Idempotent(
                projectElement,
                Z0020.Packages.Instance.ForWebBlazorClient.ToStringBasedPackageReferences());
#pragma warning restore CS0618 // Type or member is obsolete
        }

        public void AddRazorClassLibraryPackageReferences(
            XElement projectElement)
        {
#pragma warning disable CS0618 // Type or member is obsolete
            ProjectXmlOperator.Instance.AddPackageReferences_Idempotent(
                projectElement,
                Z0020.Packages.Instance.ForRazorClassLibrary.ToStringBasedPackageReferences());
#pragma warning restore CS0618 // Type or member is obsolete
        }

        public void AddBrowserSupportedPlatform(
            XElement projectElement)
        {
            ProjectXmlOperator.Instance.AddBrowserSupportedPlatform(projectElement);
        }

        public Action<XElement> AddLibraryProjectReferences(
            string expectedProjectFilePath)
        {
            return (projectElement) =>
            {
                ProjectXmlOperator.Instance.AddProjectReferences_Idempotent(
                    projectElement,
                    expectedProjectFilePath,
                    ProjectFilePaths.Instance.StandardDependenciesForLibrary);
            };
        }

        public Action<XElement> AddConsoleProjectReferences(
            string expectedProjectFilePath)
        {
            return projectElement =>
            {
                ProjectXmlOperator.Instance.AddProjectReferences_Idempotent(
                    projectElement,
                    expectedProjectFilePath,
                    ProjectFilePaths.Instance.StandardDependenciesForConsole);
            };
        }

        public void AddWindowsFormsProjectReferences(
            XElement projectElement,
            string expectedProjectFilePath)
        {
            ProjectXmlOperator.Instance.AddProjectReferences_Idempotent(
                projectElement,
                expectedProjectFilePath,
                ProjectFilePaths.Instance.R5T_F0062);
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
        /// Sets the target framework to the .NET 6.0 target framework. (<see cref="F0020.ITargetFrameworkMonikerStrings.NET_6"/>)
        /// </summary>
        public void SetTargetFramework_NET6(XElement projectElement)
        {
            ProjectXmlOperator.Instance.SetTargetFramework(
                projectElement,
                F0020.TargetFrameworkMonikerStrings.Instance.NET_6);
        }

        /// <summary>
        /// 
        /// </summary>
        public void SetDisabledWarnings(XElement projectElement)
        {
#pragma warning disable CS0618 // Type or member is obsolete
            ProjectXmlOperator.Instance.SetDisabledWarnings(projectElement,
                new[]
                {
                    Warnings.Instance.CS1573,
                    Warnings.Instance.CS1587,
                    Warnings.Instance.CS1591,
                });
#pragma warning restore CS0618 // Type or member is obsolete
        }

        /// <summary>
        /// Sets the target framework to the standard framwork for libraries (<see cref="ITargetFrameworkMonikerStrings.StandardForWeb"/>).
        /// </summary>
        public void SetWebTargetFramework(XElement projectElement)
        {
            ProjectXmlOperator.Instance.SetTargetFramework(
                projectElement,
                TargetFrameworkMonikerStrings.Instance.StandardForWeb);
        }

        /// <summary>
        /// Sets the target framework to the standard framwork for libraries (<see cref="ITargetFrameworkMonikerStrings.StandardForWindowsForms"/>).
        /// </summary>
        public void SetWindowsFormsTargetFramework(XElement projectElement)
        {
            ProjectXmlOperator.Instance.SetTargetFramework(
                projectElement,
                TargetFrameworkMonikerStrings.Instance.StandardForWindowsForms);
        }

        public void SetUseWindowsForms(XElement projectElement)
        {
            this.SetUseWindowsForms(projectElement, true);
        }

        public void SetUseWindowsForms(XElement projectElement,
            bool value = true)
        {
            ProjectXmlOperator.Instance.SetUseWindowsForms(
                projectElement,
                value);
        }

        /// <summary>
        /// Sets the output type to the value for console applications (<see cref="IOutputTypeStrings.Console"/>).
        /// </summary>
        public void SetConsoleOutputType(XElement projectElement)
        {
            ProjectXmlOperator.Instance.SetOutputType(
                projectElement,
                OutputTypeStrings.Instance.Console);
        }

        /// <summary>
        /// Sets the output type to the value for Windows applications (<see cref="IOutputTypeStrings.WindowsExecutable"/>).
        /// </summary>
        public void SetWindowsExecutableOutputType(XElement projectElement)
        {
            ProjectXmlOperator.Instance.SetOutputType(
                projectElement,
                OutputTypeStrings.Instance.WindowsExecutable);
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
        /// Sets the SDK to the Blazor .NET SDK (<see cref="IProjectSdkStrings.BlazorWebAssembly"/>).
        /// </summary>
        public void SetBlazorWebAssemblySdk(XElement projectElement)
        {
            ProjectXmlOperator.Instance.SetSdk(
                projectElement,
                Instances.ProjectSdkStrings.BlazorWebAssembly);
        }

        /// <summary>
        /// Sets the SDK to Razor .NET SDK (<see cref="IProjectSdkStrings.Razor"/>).
        /// </summary>
        public void SetRazorSdk(XElement projectElement)
        {
            ProjectXmlOperator.Instance.SetSdk(
                projectElement,
                Instances.ProjectSdkStrings.Razor);
        }
    }
}