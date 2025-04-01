using UnityEngine;

public abstract class Character: MonoBehaviour
{
	public virtual void Initialize()
	{
		Debug.Log("Character Intialized");
	}

	void Start()
	{
		Initialize();
	}
}