using System;


namespace R5T.F0081
{
	public class ProjectXmlOperator : IProjectXmlOperator
	{
		#region Infrastructure

	    public static IProjectXmlOperator Instance { get; } = new ProjectXmlOperator();

	    private ProjectXmlOperator()
	    {
        }

	    #endregion
	}
}