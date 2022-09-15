using UnityEngine;
using MonoServices.Core;

namespace MonoServices.CharacterAttributes
{
    public class HealthHandler : MonoService
    {
        [SerializeField] float _healthAmount;
        [SerializeField] float _maxHealth = 100;
        [SerializeField] bool _canUpdateHealth = true;

        void DamageHealthCommand(float amount)
        {
            if (!_canUpdateHealth)
                return;

            _healthAmount -= amount;

            if (_healthAmount <= 0)
            {
                _healthAmount = 0;
                DeathCommand();
            }

            GetHealthAmountCommand();

            InvokeCommand(0, _healthAmount);
        }

        void RepairHealthCommand(float amount)
        {
            if (!_canUpdateHealth)
                return;

            if (_healthAmount >= _maxHealth)
            {
                OnFullHealthCommand();
                _healthAmount = _maxHealth;
                return;
            }

            if (_healthAmount < _maxHealth)
            {
                _healthAmount += amount;
                GetHealthAmountCommand();

                InvokeCommand(1, _healthAmount);
            }

        }

        void DeathCommand() =>
            InvokeCommand(2);

        void GetHealthAmountCommand()
        {
            InvokeCommand(3, _healthAmount);
        }

        void ActivateCanUpdateHealthCommand()
        {
            InvokeCommand(4, _healthAmount);

            _canUpdateHealth = true;
        }

        void OnFullHealthCommand() =>
            InvokeCommand(5, _healthAmount);

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            if (methodNumb == 0) DamageHealthCommand((float)passedObj);
            if (methodNumb == 1) RepairHealthCommand((float)passedObj);
            if (methodNumb == 4) ActivateCanUpdateHealthCommand();
        }
    }
}