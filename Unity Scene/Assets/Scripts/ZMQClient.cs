using UnityEngine;

public class ZMQClient : MonoBehaviour {

    private ZMQRequester zmqRequester;
    GameObject subject;
    private readonly int rayCastLayerMask = 1 << 9;

    void Start () {
        zmqRequester = new ZMQRequester();
        zmqRequester.Start();
	}
	
    void OnDestroy()
    {
        zmqRequester.Stop();
    }

    private void Update()
    {
        subject = GameObject.Find("subject");

        if (subject == null)
            return;

        RaycastHit hit;
        string hit_id = "";
        string hit_lane = "";
        bool hit_pedWalk = false;

        if (Physics.Raycast(subject.transform.position, subject.transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity, rayCastLayerMask))
        {
            GameObject hit_go = hit.collider.gameObject;
            hit_id = hit_go.GetComponent<NetworkData>().id;
            hit_lane = hit_go.name;
            hit_pedWalk = hit_go.GetComponent<NetworkData>().pedWalk;
        }
        
        zmqRequester.UpdateSubject(subject.transform.position.x, subject.transform.position.z, subject.transform.rotation.eulerAngles.y, hit_id, hit_lane, hit_pedWalk);
    }
}
