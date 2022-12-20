using System;
using System.Threading.Tasks;
using System.Xml.Linq;

using R5T.T0132;


namespace R5T.F0081
{
	[FunctionalityMarker]
	public partial interface IProjectFileXmlOperations : IFunctionalityMarker
	{
        /// <summary>
        /// Creates a project element that is *only* the project element.
        /// </summary>
        public XElement Create_OnlyProjectElement()
        {
            var projectElement = F0020.ProjectXmlOperations.Instance.NewProjectElement();

            return projectElement;
        }

        public XElement New()
        {
            var projectElement = F0020.ProjectXmlOperations.Instance.NewProjectElement();

            return projectElement;
        }
    }
}