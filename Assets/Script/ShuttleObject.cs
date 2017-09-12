using UnityEditor;
using UnityEngine;

public class ShuttleObject : MonoBehaviour {
    public float speed;
    public Transform endPoint;
    public bool flip;

    private Vector3 startPos;
    private Vector3 endPos;
    private Vector3 targetPos;
    private bool canMove;

    // Use this for initialization
    void Start() {
        startPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        endPos = endPoint.position;
        targetPos = endPos;
    }

    // Update is called once per frame
    void Update() {
        if (canMove) {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
            if (transform.position == startPos) {
                targetPos = endPos;
                Flip();
            }
            else if (transform.position == endPos) {
                targetPos = startPos;
                Flip();
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