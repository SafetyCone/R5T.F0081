using System;
using System.Xml.Linq;
using System.Threading.Tasks;

using R5T.T0132;


namespace R5T.F0081
{
	[FunctionalityMarker]
	public partial interface IProjectFileOperator : IFunctionalityMarker,
        F0020.IProjectFileOperator
	{
        //public async Task CreateNewProjectFile(
        //    string projectFilePath,
        //    Func<XElement, Task> projectElementAction = default)
        //{
        //    var projectElement = await ProjectXmlOperations.Instance.CreateNewProjectElement(
        //        projectElementAction);

        //    await this.Save(
        //        projectFilePath,
        //        projectElement);
        //}
    }
}