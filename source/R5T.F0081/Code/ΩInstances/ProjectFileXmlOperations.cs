using System;


namespace R5T.F0081
{
	public class ProjectFileXmlOperations : IProjectFileXmlOperations
	{
		#region Infrastructure

	    public static IProjectFileXmlOperations Instance { get; } = new ProjectFileXmlOperations();

	    private ProjectFileXmlOperations()
	    {
        }

	    #endregion
	}
}