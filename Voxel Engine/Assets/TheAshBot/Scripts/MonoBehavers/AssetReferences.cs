using UnityEngine;

namespace TheAshBot
{
    public class AssetReferences : MonoBehaviour
    {
        private static AssetReferences instance;

        public static AssetReferences Instance
        {
            get
            {
                if (instance == null) instance = (Instantiate(Resources.Load("GameAssets")) as GameObject).GetComponent<AssetReferences>();
                return instance;
            }
        }




        [field: SerializeField]
        public Sprite SinglePixelSprite
        {
            get;
            private set;
        }



    }
}
