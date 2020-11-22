using UnityEngine;

namespace Cyberball.Weapons
{
    [System.Serializable]
    public class PlayerWeapon{

        [SerializeField]
        private string name = "AssaultRifle";

        [SerializeField]
        private int damages = 10;
        [SerializeField]
        private int headShotDamages = 20;
        [SerializeField]
        private float range = 100f;
        [SerializeField]
        private bool singleShot = false;
        [SerializeField]
        private float fireRate = .25f;
        [SerializeField]
        private int maxAmmunitions = 20;
        private int remainingAmmunitions;
        [SerializeField]
        private float reloadTime = 1f;
        //Pattern of recoil with multiple position
        [SerializeField]
        private Vector2[] recoilPattern;
        [SerializeField]
        private float timeForRecoilPatternReset = 1;
        [SerializeField]
        private float recoilDuration = 1;
        //Define in which hand the weapon will be spawn
        [SerializeField]
        private bool isRightHand = true;
        //Spawn 2 weapons, 1 in both hands if double handed
        [SerializeField]
        private bool isDoubleHanded = false;

        [SerializeField]
        private GameObject graphics;

        public PlayerWeapon()
        {
            remainingAmmunitions = maxAmmunitions;
        }

        public string Name { get => name; set => name = value; }
        public int Damages { get => damages; set => damages = value; }
        public float Range { get => range; set => range = value; }
        public float FireRate { get => fireRate; set => fireRate = value; }
        public int MaxAmmunitions { get => maxAmmunitions; set => maxAmmunitions = value; }
        public int RemainingAmmunitions { get => remainingAmmunitions; set => remainingAmmunitions = value; }
        public float ReloadTime { get => reloadTime; set => reloadTime = value; }
        public GameObject Graphics { get => graphics; set => graphics = value; }
        public bool SingleShot { get => singleShot; set => singleShot = value; }
        public bool IsRightHand { get => isRightHand; set => isRightHand = value; }
        public bool IsDoubleHanded { get => isDoubleHanded; set => isDoubleHanded = value; }
        public int HeadShotDamages { get => headShotDamages; set => headShotDamages = value; }
        public Vector2[] RecoilPattern { get => recoilPattern; set => recoilPattern = value; }
        public float RecoilDuration { get => recoilDuration; set => recoilDuration = value; }
        public float TimeForRecoilPatternReset { get => timeForRecoilPatternReset; set => timeForRecoilPatternReset = value; }
    }
}

