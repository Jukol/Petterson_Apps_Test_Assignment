using System;
using UnityEngine;
namespace Level
{
    public interface ITargetable
    {
        public event Action<int> OnTargetClicked;
        public void MoveDown(float speed);
    }
}
