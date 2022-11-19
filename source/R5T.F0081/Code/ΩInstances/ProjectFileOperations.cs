using System;


namespace R5T.F0081
{
	public class ProjectFileOperations : IProjectFileOperations
	{
		#region Infrastructure

	    public static IProjectFileOperations Instance { get; } = new ProjectFileOperations();

	    private ProjectFileOperations()
	    {
        }

	    #endregion
	}
}