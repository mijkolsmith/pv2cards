using System.Collections;

public abstract class State
{
	public virtual IEnumerator Start()
	{
		yield break;
	}

	public virtual IEnumerator Update()
	{
		yield break;
	}

	public virtual IEnumerator Exit()
	{
		yield break;
	}

	public virtual IEnumerator Attack()
	{
		yield break;
	}
}