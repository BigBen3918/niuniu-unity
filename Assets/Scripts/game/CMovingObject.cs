using UnityEngine;
using System.Collections;

public class CMovingObject : MonoBehaviour {

	public Vector3 begin;
	public Vector3 to;
	public float duration = 2f;
	float startTime;

	bool isEndCoin = false;
	int toPosition = 0;
	int balance;

	void Awake()
	{
		

	}

    private void Start()
    {
		// Debug.Log(gameObject.GetComponent<RectTransform>().position);
        // begin = gameObject.GetComponent<RectTransform>().position;
        // to = target.position;
		startTime = Time.time;

	}

	private void Update(){
		if((Time.time - startTime) > duration + 0.4f){
			DestoryObject();
		}
	}

	void DestoryObject(){
		if(isEndCoin){
			GameRoom.instance.players[toPosition].SetFocus();
			GameRoom.instance.GotCoin(toPosition, balance * GameRoom.instance.antes);
		}
		Destroy(gameObject);
	}



	public void run()
	{
		StopAllCoroutines();
		StartCoroutine(run_moving());
	}

	public void SetEndCoinInfo(bool _isEndCoin, int _toPosition, int _balance){
		isEndCoin = _isEndCoin;
		toPosition = _toPosition;
		balance = _balance;
	}


	IEnumerator run_moving()
	{

		
		float begin_time = Time.time;
		while (Time.time - begin_time <= duration)
		{
		

			float t = (Time.time - begin_time) / duration;

			float x = EasingUtil.linear(begin.x, to.x, t);
			float y = EasingUtil.linear(begin.y, to.y, t);
            // float x = EasingUtil.linear(begin.x, to.x, t);
			// float y = EasingUtil.linear(begin.y, to.y, t);
			transform.position = new Vector3(x, y, begin.z);

			yield return 0;
		}

		transform.position = to;
	}
}
