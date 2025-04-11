using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace teamFourFinalProject
{
    public interface ISpawnPointStrategy
    {
        Transform NextSpawnPoint();
    }
}
