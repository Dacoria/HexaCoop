using UnityEngine;

public class MountainScript : HexaEventCallback
{
    [ComponentInject(Required.OPTIONAL)] private Hex hex; // verwijderde mountain (animatie door grond) heeft dit niet

    public void Destroy(bool useAnimationToGround = true)
    {
        if (useAnimationToGround)
        {
            var mountainPrefab = HexStructureType.Mountain.GetStructurePrefabFromRrc();
            var mountainToVanishInGroundGo = Instantiate(mountainPrefab, gameObject.transform.position, Quaternion.identity);

            var lerpMovement = mountainToVanishInGroundGo.GetAdd<LerpMovement>();
            lerpMovement.MoveDown(distance: 2, duration: 2, destroyGoOnFinished: true);

            // Destroy(relatedMountainGo);  => GEBEURT AL DOOR ChangeHexStructureType      
        }

        hex.ChangeHexStructureType(HexStructureType.None);
    }
}