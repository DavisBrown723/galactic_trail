using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMissile : MonoBehaviour
{
    private BoundsCheck bndCheck;
    private Transform lockedTarget;

    void Awake()
    {
        bndCheck = GetComponent<BoundsCheck>();
    }

    // Update is called once per frame
    void Update()
    {
        if (bndCheck.offUp)
        {
            Destroy(gameObject);
        }
    }

    void FixedUpdate() {
        EmitRadar();
        TrackToTarget();
    }

    void EmitRadar() {
        var radarSeekAngles = new List<Vector3>() {
            transform.TransformDirection(Vector3.up + Vector3.left * 0.7f),
            transform.TransformDirection(Vector3.up + Vector3.left * 0.5f),
            transform.TransformDirection(Vector3.up + Vector3.left * 0.25f),
            transform.TransformDirection(Vector3.up),
            transform.TransformDirection(Vector3.up + Vector3.right * 0.25f),
            transform.TransformDirection(Vector3.up + Vector3.right * 0.5f),
            transform.TransformDirection(Vector3.up + Vector3.right * 0.7f)
        };

        // find targets on radar

        int layers = LayerMask.GetMask("Enemy");

        var radarReturns = new List<Transform>();
        foreach(var angle in radarSeekAngles) {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, angle, out hit, Mathf.Infinity, layers)) {         
                radarReturns.Add(hit.transform);
            }
        }

        if (radarReturns.Count == 0)
            return;

        // find closest target

        float shortestDist = Mathf.Infinity;
        Transform closestTarget = null;
        foreach(var radarReturn in radarReturns) {
            float distToTarget = Vector3.Distance(transform.position, radarReturn.position);
            if (distToTarget < shortestDist) {
                shortestDist = distToTarget;
                closestTarget = radarReturn;
            }
        }

        lockedTarget = closestTarget;
    }

    void TrackToTarget() {
        if (lockedTarget == null) {
            GetComponent<Rigidbody>().velocity = Vector3.up * 25;
            return;
        }

        Vector3 offset = lockedTarget.position -  transform.position;

        if (offset.x < 0) {
            GetComponent<Rigidbody>().velocity = Vector3.up * 25f + Vector3.left * 25f;
        } else {
            GetComponent<Rigidbody>().velocity = Vector3.up * 25f + Vector3.right * 25f;
        }
    }
}
