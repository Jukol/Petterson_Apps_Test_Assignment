using System;

namespace Level.Targets
{
    public interface ITargetable
    {
        public event Action<int, int> OnTargetClicked;
        public void MoveDown(float speed);
    }
}
