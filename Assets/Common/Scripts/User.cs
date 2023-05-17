using Common.Scripts.UserData;
using UnityEngine;
using Zenject;

namespace Common.Scripts
{
    public class User : MonoBehaviour
    {
        private IUserData _userData;

        [Inject]
        void Construct(IUserData userData)
        {
            _userData = userData;
        }
    }
}