using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHpPosition : MonoBehaviour
{
    [SerializeField] private Vector3 Distance;

    private GameObject Target;
    private RectTransform UITransform;

    public void Setup(GameObject target)
    {
        Target = target;
        UITransform = GetComponent<RectTransform>();

        switch (target.name)
        {
            case "Flying_eye(Clone)":
                Distance = new Vector3(0, 60f, 0);
                break;

            case "Goblin(Clone)":
                Distance = new Vector3(0, 100f, 0);
                break;

            case "Mushroom(Clone)":
                Distance = new Vector3(0, 110f, 0);
                break;

            case "Skeleton(Clone)":
                Distance = new Vector3(0, 130f, 0);
                break;

            default:
                break;
        }
    }
    private void Update()
    {
        if(Target == null || !Target.activeSelf)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 screenPostion =
            Camera.main.WorldToScreenPoint(Target.transform.position);

        UITransform.position = screenPostion + Distance;
    }
}
