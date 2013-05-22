using UnityEngine;
using System.Collections;

public class RandomSpawn : MonoBehaviour
{
	[SerializeField]
	GameManager manager;

	[SerializeField]
	GameObject fishPrefab;
	
	[HideInInspector]
	public int spawnCount = 0;

	[SerializeField]
	Transform left = null, right = null;
	
	[SerializeField]
	StageRecodeObject stageRecode;
	
	public int SpawnNumber { get{ return stageRecode.stageRecodes.Length; }}

	void OnDrawGizmosSelected ()
	{
		if (left == null || right == null)
			return;
		
		Gizmos.DrawLine (left.position, right.position);
	}

	void OnEnable ()
	{
		StartCoroutine (SpawnLoop ());
	}
	
	void OnDisable ()
	{
		StopAllCoroutines ();
	}
	
	IEnumerator SpawnLoop ()
	{
		while (true) {
			yield return new WaitForSeconds (stageRecode.stageRecodes[spawnCount].secWait);
			Spawn ();
			if( spawnCount >= stageRecode.stageRecodes.Length)
				yield break;
		}
	}
	
	public void Spawn ()
	{
		// spawn limit
		if (SpawnNumber <= spawnCount)
			return;
		
		spawnCount ++;
		
		float position = Random.Range (left.transform.position.x, right.transform.position.x);
		
		GameObject fish = GameObject.Instantiate (fishPrefab) as GameObject;
		fish.transform.position = transform.position + Vector3.left * position + Vector3.forward * 20;
		fish.transform.parent = Dustbox.instance.transform;
	}
	
}


