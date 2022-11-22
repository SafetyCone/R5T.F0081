using System;


namespace R5T.F0081
{
	public class ProjectXmlOperations : IProjectXmlOperations
	{
		#region Infrastructure

	    public static IProjectXmlOperations Instance { get; } = new ProjectXmlOperations();

	    private ProjectXmlOperations()
	    {
        }

	    #endregion
	}
}