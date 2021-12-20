using UnityEngine;

public class GameSceneManager : MonoBehaviour
{
    [SerializeField] private GameObject wall;
    [SerializeField] private GameObject wallParent;
    [SerializeField] private float roomScale;
    private void OnEnable()
    {
        roomScale = 5f;
        CreateWallsFromPositionList();
    }

    void CreateWallsFromPositionList()
    {
        var posList = GameManager.Instance.GetPosList();
        if (posList==null)
        {
            print("posList is null");
            return;
        }
        Vector3 instantiatePos = Vector3.zero;
        Vector2 lowest=Vector2.zero, highest=Vector2.zero;
        for (int i = 0; i < posList.Count; i++)
        {
            if (instantiatePos.x>highest.x)
            {
                highest.x = instantiatePos.x;
            }
            if (instantiatePos.z>highest.y)
            {
                highest.y = instantiatePos.z;
            }
            //rotation
            var rot = Vector3.SignedAngle(transform.right, posList[i], transform.up);
            Quaternion appliedRotation = Quaternion.Euler(0, rot, 0);
            //scale
            var appliedXScale = Mathf.Sqrt(Mathf.Pow(posList[i].x, 2) + Mathf.Pow(posList[i].z, 2));
            //instantiating
            var instantiated = Instantiate(wall, instantiatePos, appliedRotation,wallParent.transform);
            //applying transforms
            var localScale = instantiated.transform.localScale;
            localScale.x = appliedXScale;
            instantiated.transform.localScale = localScale;
            //
            instantiatePos += posList[i];
        }

        var wallParentScale = wallParent.transform.localScale;
        wallParentScale *= roomScale;
        wallParent.transform.localScale = wallParentScale;
        highest *= roomScale;
        // highest *= .5f;
        // wallParent.transform.position = -1*new Vector3(highest.x, 0, highest.y);
        var c = Camera.main.GetComponent<CameraController>();
        c.SetPosition(highest);
    }
}