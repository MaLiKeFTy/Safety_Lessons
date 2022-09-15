using System.Collections.Generic;
using UnityEngine;

namespace MonoServices.Core
{
    public static class SceneMonoServicesFinder
    {
        public static List<MonoService> sceneEventCallers = new List<MonoService>();

        public static MonoService[] FindMonoServices(string objTag)
        {
            List<MonoService> tempListenerTags = new List<MonoService>();

            sceneEventCallers.Clear();
            sceneEventCallers = GetAllSceneTags();

            foreach (var tag in sceneEventCallers)
            {
                if (Equals(tag.MonoServiceParams.MonoServiceTag, objTag))
                    tempListenerTags.Add(tag);
            }

            return tempListenerTags.ToArray();
        }

        public static string[] GetMonoServiceTags()
        {
            List<string> tempListenerTags = new List<string>();

            foreach (var tag in GetAllSceneTags())
                tempListenerTags.Add(tag.MonoServiceParams.MonoServiceTag);

            tempListenerTags.Sort();

            return tempListenerTags.ToArray();
        }

        static List<MonoService> GetAllSceneTags()
        {
            List<MonoService> tempMonoServices = new List<MonoService>();

            var monoServicesInScene = Object.FindObjectsOfType<MonoService>();

            foreach (var monoService in monoServicesInScene)
            {
                if (monoService.MonoServiceParams.MonoServiceTag != "")
                    tempMonoServices.Add(monoService);
            }

            return tempMonoServices;
        }

        public static void RefreshCommandsReferences()
        {
            var monoServicesInScene = Object.FindObjectsOfType<MonoService>(false);

            foreach (var monoService in monoServicesInScene)
            {
                monoService.MonoServiceParams.CommandRefrences.Recievers.Clear();
                monoService.MonoServiceParams.CommandRefrences.Invokers.Clear();
                monoService.MonoServiceParams.CommandRefrences.Resetters.Clear();

                monoService.MonoServiceParams.CommandRefrences.Recievers.AddRange(FindMonoServiceRecievers(monoService, monoServicesInScene));
                monoService.MonoServiceParams.CommandRefrences.Invokers.AddRange(FindMonoServiceInvokers(monoService, monoServicesInScene));
                monoService.MonoServiceParams.CommandRefrences.Resetters.AddRange(FindMonoServiceCommandResetters(monoService, monoServicesInScene));
            }
        }

        public static InvokerHolder[] FindMonoServiceInvokers(MonoService passedMonoSevice, MonoService[] monoServicesInScene)
        {
            List<InvokerHolder> tempMonoServices = new List<InvokerHolder>();
            List<InvokerHolder> tempFilteredMonoServices = new List<InvokerHolder>();

            if (passedMonoSevice.MonoServiceParams.MonoServiceCommands.Length == 0)
                return tempMonoServices.ToArray();

            foreach (var monoService in monoServicesInScene)
            {
                if (!monoService.enabled)
                    continue;

                for (int i = 0; i < passedMonoSevice.MonoServiceParams.MonoServiceCommands.Length; i++)
                {
                    var monoServiceCommand = passedMonoSevice.MonoServiceParams.MonoServiceCommands[i];

                    foreach (var invokerCommand in monoServiceCommand.InvokerCommands)
                    {
                        var invokerTag = invokerCommand.Params.CurrSelectedMonoServiceTag;

                        if (monoService.MonoServiceParams.MonoServiceTag == invokerTag)
                            tempMonoServices.Add(new InvokerHolder(monoService, monoService.MonoServiceParams.MonoServiceTag, monoService.name, monoService.GetType().Name, i, 0));
                    }
                }
            }


            foreach (var monoService in tempMonoServices)
            {
                foreach (var monoServiceCommand in passedMonoSevice.MonoServiceParams.MonoServiceCommands)
                {
                    foreach (var invokerCommand in monoServiceCommand.InvokerCommands)
                    {
                        if (invokerCommand.Params.MonoServiceIsAFamRelative)
                        {
                            bool isRelative = true;

                            var parentLevel = TransformParentFinder.TranformParent(
                                passedMonoSevice.transform,
                                invokerCommand.Params.ParentNumber);

                            foreach (var childMonoService in parentLevel.GetComponentsInChildren<Transform>())
                            {
                                if (childMonoService != monoService.MainMonoService.transform)
                                    isRelative = false;
                                else
                                {
                                    isRelative = true;
                                    break;
                                }
                            }

                            if (!isRelative)
                                continue;
                        }

                        if (!tempFilteredMonoServices.Contains(monoService))
                            tempFilteredMonoServices.Add(monoService);

                    }
                }
            }



            return tempFilteredMonoServices.ToArray();
        }

        public static RecieverHolder[] FindMonoServiceRecievers(MonoService monoservicesToAdd, MonoService[] monoServicesInScene)
        {
            List<RecieverHolder> tempMonoServices = new List<RecieverHolder>();

            foreach (var monoService in monoServicesInScene)
            {
                if (!monoService.enabled)
                    continue;


                for (int i = 0; i < monoService.MonoServiceParams.MonoServiceCommands.Length; i++)
                {
                    var monoServiceCommand = monoService.MonoServiceParams.MonoServiceCommands[i];


                    foreach (var invokerCommand in monoServiceCommand.InvokerCommands)
                    {
                        if (invokerCommand.Params.MonoServiceIsAFamRelative)
                        {
                            bool isRelative = true;

                            var parentLevel = TransformParentFinder.TranformParent(
                                monoService.transform,
                                invokerCommand.Params.ParentNumber);

                            foreach (var childMonoService in parentLevel.GetComponentsInChildren<Transform>())
                            {
                                if (childMonoService != monoservicesToAdd.transform)
                                    isRelative = false;
                                else
                                {
                                    isRelative = true;
                                    break;
                                }
                            }

                            if (!isRelative)
                                continue;
                        }

                        if (invokerCommand.Params.CurrSelectedMonoServiceTag == monoservicesToAdd.MonoServiceParams.MonoServiceTag)
                            tempMonoServices.Add(new RecieverHolder(monoServiceCommand, monoService, invokerCommand.Params.CurrSelectedMonoServiceTag, monoService.name, monoService.GetType().Name, i, 0));

                    }
                }
            }

            return tempMonoServices.ToArray();
        }

        public static ResetterHolder[] FindMonoServiceCommandResetters(MonoService monoservicesToAdd, MonoService[] monoServicesInScene)
        {
            List<ResetterHolder> tempMonoServices = new List<ResetterHolder>();

            foreach (var monoService in monoServicesInScene)
            {
                if (!monoService.enabled)
                    continue;


                for (int i = 0; i < monoService.MonoServiceParams.MonoServiceCommands.Length; i++)
                {
                    var monoServiceCommand = monoService.MonoServiceParams.MonoServiceCommands[i];

                    for (int j = 0; j < monoServiceCommand.InvokerCommands.Length; j++)
                    {
                        var invokerCommand = monoServiceCommand.InvokerCommands[j];


                        foreach (var resetter in invokerCommand.InvokerResetter)
                        {

                            if (resetter.Params.MonoServiceIsAFamRelative)
                            {
                                bool isRelative = true;

                                var parentLevel = TransformParentFinder.TranformParent(
                                    monoService.transform,
                                    invokerCommand.Params.ParentNumber);

                                foreach (var childMonoService in parentLevel.GetComponentsInChildren<Transform>())
                                {
                                    if (childMonoService != monoservicesToAdd.transform)
                                        isRelative = false;
                                    else
                                    {
                                        isRelative = true;
                                        break;
                                    }
                                }

                                if (!isRelative)
                                    continue;
                            }


                            if (resetter.Params.CurrSelectedMonoServiceTag == monoservicesToAdd.MonoServiceParams.MonoServiceTag)
                                tempMonoServices.Add(new ResetterHolder(resetter.Params, monoService, resetter.Params.CurrSelectedMonoServiceTag, monoService.name, monoService.GetType().Name, i, j));
                        }


                    }

                }
            }

            return tempMonoServices.ToArray();
        }
    }
}

