using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonoServices.Core
{
    [System.Serializable]
    public class CommandRefrences
    {
        [SerializeField] List<RecieverHolder> _recievers = new List<RecieverHolder>();
        [SerializeField] List<InvokerHolder> _invokers = new List<InvokerHolder>();
        [SerializeField] List<ResetterHolder> _resetters = new List<ResetterHolder>();

        public List<RecieverHolder> Recievers { get => _recievers; set => _recievers = value; }
        public List<InvokerHolder> Invokers { get => _invokers; set => _invokers = value; }
        public List<ResetterHolder> Resetters { get => _resetters; set => _resetters = value; }


        public bool CommandHolderAlreadyExists<T>(List<T> commandsHolder, T mainCommandHolder) where T : CommandsHolder
        {
            bool commandExists = false;

            foreach (var commandHolder in commandsHolder)
            {
                var sameMonoService = commandHolder.MainMonoService == mainCommandHolder.MainMonoService;
                var sameCommandIndex = commandHolder.CommandIndex == mainCommandHolder.CommandIndex;
                var sameInvokerIndex = commandHolder.InvokerIndex == mainCommandHolder.InvokerIndex;
                var sameTag = commandHolder.MonoServiceTag == mainCommandHolder.MonoServiceTag;

                if (sameMonoService && sameCommandIndex && sameInvokerIndex && sameTag)
                {
                    commandExists = true;
                    break;
                }
            }

            return commandExists;
        }

    }
}


