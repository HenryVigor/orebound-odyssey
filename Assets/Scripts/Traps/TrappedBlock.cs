using UnityEngine;

public class TrappedBlock : BlockObject {
    [SerializeField] GameObject SpawnedEnemy;
    
    public override void ObjectDestroy() {
        Instantiate(
            SpawnedEnemy, transform.position, Quaternion.identity,
            transform.parent
        );
        base.ObjectDestroy();
    }
}
