using UnityEditor;
using UnityEngine;

public class ShuttleObject : MonoBehaviour {
    public float speed;
    public Transform endPoint;
    public bool flip;
    public bool isEnemy;
    [HideInInspector]
    public int damage;
    [HideInInspector]
    public int score;

    private Vector3 startPosition;
    private Vector3 endPosition;
    private Vector3 target;
    private bool canMove;
    private LevelMgr levelMgr;
    private Transform playerGroundCheck;

    [CustomEditor(typeof(ShuttleObject))]
    public class ShuttleEditor : Editor {
        override public void OnInspectorGUI() {
            base.OnInspectorGUI();
            var shuttleObject = target as ShuttleObject;
            GUI.enabled = shuttleObject.isEnemy;
            shuttleObject.damage = EditorGUILayout.IntField("Damage", shuttleObject.damage);
            shuttleObject.score = EditorGUILayout.IntField("Score", shuttleObject.score);
        }
    }

    // Use this for initialization
    void Start () {
        startPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        endPosition = endPoint.position;
        target = endPosition;
        if (isEnemy) {
            levelMgr = FindObjectOfType<LevelMgr>();
            playerGroundCheck = levelMgr.GetGroundCheck();
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (canMove) {
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
            if (transform.position == startPosition) {
                target = endPosition;
                Flip();
            } else if (transform.position == endPosition) {
                target = startPosition;
                Flip();
            }
            //transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        }
	}

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Player" && isEnemy) {
            if (playerGroundCheck.position.y > transform.position.y) {
                other.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 200f));
                levelMgr.AddScore(score);
                Destroy(gameObject);
            } else {
                levelMgr.DamagePlayer(damage);
            }
        }
    }

    private void OnBecameVisible() {
        canMove = true;
    }

    private void Flip() {
        if (flip) {
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }

}



//[CustomEditor(typeof(ShuttleObject))]
//public class MyScriptEditor : Editor {
//    override public void OnInspectorGUI() {
//        var shuttleObject = target as ShuttleObject;
//        shuttleObject.speed = EditorGUILayout.FloatField("Speed", shuttleObject.speed);
//        shuttleObject.endPoint = EditorGUILayout.ObjectField("End Point",
//                shuttleObject.endPoint, typeof(Transform), true) as Transform;
//        shuttleObject.isThreat = GUILayout.Toggle(shuttleObject.isThreat, "Is Threat To Player");
//        GUI.enabled = shuttleObject.isThreat;
//        shuttleObject.damage = EditorGUILayout.IntField("Damage", shuttleObject.damage);
//    }
//}