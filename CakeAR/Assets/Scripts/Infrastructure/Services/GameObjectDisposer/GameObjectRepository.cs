using System.Collections.Generic;
using UnityEngine;

namespace Infrastructure.Services.GameObjectDisposer
{
    public class GameObjectRepository : IGameObjectRepository
    {
        private readonly List<GameObject> _objectsRepository = new();

        public GameObject CurrentGameObject { get; private set; }

        public void AddObjectToDispose(GameObject gameObjectToRepository)
        {
            _objectsRepository.Add(gameObjectToRepository);

            CurrentGameObject = gameObjectToRepository;
        }

        public void Dispose()
        {
            foreach (var objectDispose in _objectsRepository) 
                Object.Destroy(objectDispose);
            
            _objectsRepository.Clear();
        }
    }
}