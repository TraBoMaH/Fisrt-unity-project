using ToolBox.Serialization;
using UnityEngine;
public class GlobalSave : MonoBehaviour
{
    [SerializeField] private Vector3 name1;
    [SerializeField] private float HP;
    [SerializeField] private int mones;
    [SerializeField] private bool[] unlocked;
    [SerializeField] private int wave;
    [SerializeField] private int ZombieAmount;
    [SerializeField] private float interval;
    [SerializeField] private int flasks;
    [SerializeField] private int AirStrikecount;
    [SerializeField] private int BaseLvl;
    private void save()
    {
        HP = health.righthp;
        mones = Money.money;
        name1 = gameObject.transform.position;
        unlocked = Money.unlockedWeapons;
        wave = Spawner.wave;
        ZombieAmount = Spawner.ZombieAmount;
        interval = Spawner.interval;
        flasks = health.flasks;
        AirStrikecount = AirStrike.AirStrikecount;
        BaseLvl = Money.BaseLvl;
        DataSerializer.Save("transform", name1);
        DataSerializer.Save("money", mones);
        DataSerializer.Save("unlocked", unlocked);
        DataSerializer.Save("HP", HP);
        DataSerializer.Save("wave", wave);
        DataSerializer.Save("zombies", ZombieAmount);
        DataSerializer.Save("interval", interval);
        DataSerializer.Save("flasks", flasks);
        DataSerializer.Save("AirStrikecount", AirStrikecount);
        DataSerializer.Save("baselvl", BaseLvl);
    }
    private void load()
    {
        gameObject.transform.position = DataSerializer.Load<Vector3>("transform");
        Money.money = DataSerializer.Load<int>("money");
        Money.unlockedWeapons = DataSerializer.Load<bool[]>("unlocked");
        health.righthp = DataSerializer.Load<float>("HP");
        Spawner.wave = DataSerializer.Load<int>("wave");
        Spawner.ZombieAmount = DataSerializer.Load<int>("zombies");
        Spawner.interval = DataSerializer.Load<float>("interval");
        health.flasks = DataSerializer.Load<int>("flasks");
        AirStrike.AirStrikecount = DataSerializer.Load<int>("AirStrikecount");
        Money.BaseLvl = DataSerializer.Load<int>("baselvl");
    }
    private void Update() {
        if(Input.GetKeyDown(KeyCode.F11))
        {
            save();
        }
        if(Input.GetKeyDown(KeyCode.F12))
        {
            load();
        }
    }
}