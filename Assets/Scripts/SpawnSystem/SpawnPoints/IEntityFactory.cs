using UnityEngine;

namespace teamFourFinalProject
{
    public interface IEntityFactory<T> where T : Entity
    {
        T Create(Transform spawnPoint);
    }
}
