using UnityEngine;

public class Chunk : MonoBehaviour
{
    public Transform Begin1;
    public Transform End1;
    public Transform Begin2;
    public Transform End2;
    public Transform Begin3;
    public Transform End3;
    public Transform Begin4;
    public Transform End4;

    //public Mesh[] BlockMeshes;

    public AnimationCurve ChanceFromDistance;

    private void Start()
    {
     /*   foreach (var filter in GetComponentsInChildren<MeshFilter>())
        {
            if (filter.sharedMesh == BlockMeshes[0])
            {
                filter.sharedMesh = BlockMeshes[Random.Range(0, BlockMeshes.Length)];
                filter.transform.rotation = Quaternion.Euler(-90, 0, 90 * Random.Range(0,4));
            }
        }*/
    }
}