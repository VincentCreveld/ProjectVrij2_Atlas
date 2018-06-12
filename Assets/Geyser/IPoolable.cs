using UnityEngine;
public delegate void PoolEvent(GameObject p);

public interface IPoolable {
	void InitialiseObj(Quaternion rot, float minSpread, float maxSpread, float speed, Transform t);
	void Recycle();
	PoolEvent PoolEvent {
		get;
		set;
	}
}
