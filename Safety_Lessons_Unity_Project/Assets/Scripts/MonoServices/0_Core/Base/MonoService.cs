using System;
using System.Collections.Generic;
using UnityEngine;

namespace MonoServices.Core
{
    public abstract class MonoService : CoroutineMonoService
    {
        [Tooltip("To Receive commands from other services.")]
        [SerializeField] MonoServiceParams _monoService;

        public MonoServiceParams MonoServiceParams => _monoService;
        public bool UnsubOnDisabled { get; set; } = true;


        protected virtual void Awake()
        {
            RefreshUnrefrenceCommands();

            AddedReferenceCommandsToNewSpawedObjects();
        }

        protected virtual void OnValidate() { }

        protected virtual void Reset() { }

        protected virtual void Start() { }

        protected override void OnEnable()
        {
            base.OnEnable();
        }

        protected virtual void OnDisable() { }

        void AddedReferenceCommandsToNewSpawedObjects()
        {
            for (int i = _monoService.CommandRefrences.Invokers.Count - 1; i >= 0; i--)
            {
                var invoker = _monoService.CommandRefrences.Invokers[i];

                if (invoker.MainMonoService == null)
                {
                    _monoService.CommandRefrences.Invokers.RemoveAt(i);
                    continue;
                }


                var foundRecievers = SceneMonoServicesFinder.FindMonoServiceRecievers(invoker.MainMonoService, new MonoService[] { this });


                if (foundRecievers == null)
                    continue;

                if (foundRecievers.Length == 0)
                    continue;

                foreach (var foundReciever in foundRecievers)
                {
                    if (!_monoService.CommandRefrences.CommandHolderAlreadyExists(invoker.MainMonoService._monoService.CommandRefrences.Recievers, foundReciever))
                        invoker.MainMonoService._monoService.CommandRefrences.Recievers.Add(foundReciever);
                }


                var foundResetters = SceneMonoServicesFinder.FindMonoServiceCommandResetters(invoker.MainMonoService, new MonoService[] { this });




                if (foundResetters == null)
                    continue;

                if (foundResetters.Length == 0)
                    continue;


                foreach (var foundResetter in foundResetters)
                {
                    if (!_monoService.CommandRefrences.CommandHolderAlreadyExists(invoker.MainMonoService._monoService.CommandRefrences.Resetters, foundResetter))
                        invoker.MainMonoService._monoService.CommandRefrences.Resetters.Add(foundResetter);
                }

            }

            for (int i = _monoService.CommandRefrences.Recievers.Count - 1; i >= 0; i--)
            {
                var reciever = _monoService.CommandRefrences.Recievers[i];

                if (reciever.MainMonoService == null)
                {
                    _monoService.CommandRefrences.Recievers.RemoveAt(i);
                    continue;
                }

                var foundInvokers = SceneMonoServicesFinder.FindMonoServiceInvokers(reciever.MainMonoService, new MonoService[] { this });

                if (foundInvokers == null)
                    continue;

                if (foundInvokers.Length == 0)
                    continue;

                foreach (var foundInvoker in foundInvokers)
                {
                    if (!_monoService.CommandRefrences.CommandHolderAlreadyExists(reciever.MainMonoService._monoService.CommandRefrences.Invokers, foundInvoker))
                        reciever.MainMonoService._monoService.CommandRefrences.Invokers.Add(foundInvoker);
                }


                var foundResetters = SceneMonoServicesFinder.FindMonoServiceCommandResetters(reciever.MainMonoService, new MonoService[] { this });

                if (foundResetters == null)
                    continue;

                if (foundResetters.Length == 0)
                    continue;


                foreach (var foundResetter in foundResetters)
                {
                    if (!_monoService.CommandRefrences.CommandHolderAlreadyExists(reciever.MainMonoService._monoService.CommandRefrences.Resetters, foundResetter))
                        reciever.MainMonoService._monoService.CommandRefrences.Resetters.Add(foundResetter);
                }

            }

            for (int i = _monoService.CommandRefrences.Resetters.Count - 1; i >= 0; i--)
            {
                if (_monoService.CommandRefrences.Resetters[i].MainMonoService == null)
                {
                    _monoService.CommandRefrences.Resetters.RemoveAt(i);

                    continue;
                }
            }
        }
        
        void RefreshUnrefrenceCommands()
        {
            HashSet<string> typeNames = new HashSet<string>();

            List<CommandsHolder> commandsHolders = new List<CommandsHolder>();

            commandsHolders.AddRange(_monoService.CommandRefrences.Invokers);
            commandsHolders.AddRange(_monoService.CommandRefrences.Recievers);
            commandsHolders.AddRange(_monoService.CommandRefrences.Resetters);

            for (int i = commandsHolders.Count - 1; i >= 0; i--)
            {
                var commandsHolder = commandsHolders[i];

                if (commandsHolder.MainMonoService != null)
                    continue;

                typeNames.Add(commandsHolder.MonoServiceType);

                commandsHolders.RemoveAt(i);
            }

            List<MonoService> monoServices = new List<MonoService>();

            HashSet<Type> foundTypes = new HashSet<Type>();

            for (int n = 0; n < MonoServiceFinder.Types.Count; n++)
            {
                foreach (var typeName in typeNames)
                {
                    if (MonoServiceFinder.Types[n].Name == typeName)
                        foundTypes.Add(MonoServiceFinder.Types[n]);
                }
            }

            foreach (var type in foundTypes)
            {
                foreach (var obj in FindObjectsOfType(type))
                {
                    if (obj is MonoService monoService)
                        monoServices.Add(monoService);
                }
            }

            foreach (var newInvoker in SceneMonoServicesFinder.FindMonoServiceInvokers(this, monoServices.ToArray()))
            {
                if (!_monoService.CommandRefrences.CommandHolderAlreadyExists(_monoService.CommandRefrences.Invokers, newInvoker))
                    _monoService.CommandRefrences.Invokers.Add(newInvoker);
            }

            foreach (var newReciever in SceneMonoServicesFinder.FindMonoServiceRecievers(this, monoServices.ToArray()))
            {
                if (!_monoService.CommandRefrences.CommandHolderAlreadyExists(_monoService.CommandRefrences.Recievers, newReciever))
                    _monoService.CommandRefrences.Recievers.Add(newReciever);
            }

            foreach (var newResetter in SceneMonoServicesFinder.FindMonoServiceCommandResetters(this, monoServices.ToArray()))
            {
                if (!_monoService.CommandRefrences.CommandHolderAlreadyExists(_monoService.CommandRefrences.Resetters, newResetter))
                    _monoService.CommandRefrences.Resetters.Add(newResetter);
            }
        }

        protected void InvokeCommand(int methodNumb, object passedObj = null)
        {
            foreach (var receiver in _monoService.CommandRefrences.Recievers)
            {
                if (receiver.MainMonoService == null || !receiver.MainMonoService.isActiveAndEnabled)
                    continue;

                var InvokerCommands = receiver.MainMonoService.MonoServiceParams.MonoServiceCommands[receiver.CommandIndex].InvokerCommands;

                if (InvokerCommands.Length == 0)
                    continue;

                if (InvokerCommands.Length == 1)
                {
                    var receiverTag = InvokerCommands[0].Params.CurrSelectedMonoServiceTag;

                    if (InvokerCommands[0].Params.SelectedInvokerCommandIndex == methodNumb && _monoService.MonoServiceTag == receiverTag)
                    {


                        receiver.MainMonoService.ReceiveCommands(this, receiver.MonoServiceCommand.RecieverCommand.SelectedReciverCommandIndex, passedObj);
                        InvokerCommands[0].Params.AlreadyCalled = true;
                    }

                    continue;
                }

                bool rightCall = false;

                for (int i = 0; i < InvokerCommands.Length; i++)
                {
                    var invokerCommand = InvokerCommands[i];

                    var receiverTag = invokerCommand.Params.CurrSelectedMonoServiceTag;

                    if (invokerCommand.Params.SelectedInvokerCommandIndex == methodNumb && _monoService.MonoServiceTag == receiverTag)
                    {
                        invokerCommand.Params.AlreadyCalled = true;

                        if ((InvokerCommands.Length - 1) == i)
                            receiver.MonoServiceCommand.PassedObj = passedObj;


                        rightCall = true;
                    }
                }

                if (!rightCall)
                    continue;

                bool invokersAreAllCalled = true;

                foreach (var invokerCommand1 in InvokerCommands)
                {
                    if (!invokerCommand1.Params.AlreadyCalled)
                    {
                        invokersAreAllCalled = false;
                        break;
                    }
                }


                if (invokersAreAllCalled)
                {

                    receiver.MainMonoService.ReceiveCommands(
                        this,
                        receiver.MonoServiceCommand.RecieverCommand.SelectedReciverCommandIndex,
                        receiver.MonoServiceCommand.PassedObj
                        );


                    var resetAllCommands = receiver.MainMonoService.MonoServiceParams.MonoServiceCommands[receiver.CommandIndex].ResetAllAlreadyCalledCommands;

                    if (resetAllCommands)
                    {
                        foreach (var invokerCommand1 in InvokerCommands)
                        {
                            invokerCommand1.Params.AlreadyCalled = false;
                        }
                    }

                }

            }

            foreach (var resetter in _monoService.CommandRefrences.Resetters)
            {
                var resetterCommand = resetter.Resetter;

                var receiverTag = resetterCommand.CurrSelectedMonoServiceTag;

                if (resetterCommand.SelectedInvokerCommandIndex == methodNumb && _monoService.MonoServiceTag == receiverTag)
                    resetter.MainMonoService.MonoServiceParams.MonoServiceCommands[resetter.CommandIndex].InvokerCommands[resetter.InvokerIndex].Params.AlreadyCalled = false;
            }
        }

        protected abstract void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj);

    }
}