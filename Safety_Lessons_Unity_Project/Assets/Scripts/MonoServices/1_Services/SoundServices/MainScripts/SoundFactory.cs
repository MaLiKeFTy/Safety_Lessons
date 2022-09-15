using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace MonoServices.Sound
{
    public static class SoundFactory
    {
        static Dictionary<SoundPlayOptionsEnum, SoundPlayOption> soundTypeDictionary = new Dictionary<SoundPlayOptionsEnum, SoundPlayOption>();
        static bool initialized = false;

        static void Initialize()
        {
            soundTypeDictionary.Clear();

            var allSoundTypes = Assembly.GetAssembly(typeof(SoundPlayOption)).GetTypes().Where(t => typeof(SoundPlayOption).IsAssignableFrom(t) && t.IsAbstract == false);

            foreach (var item in allSoundTypes)
            {
                SoundPlayOption soundType = Activator.CreateInstance(item) as SoundPlayOption;
                soundTypeDictionary[soundType.SoundType] = soundType;
            }

            initialized = true;
        }

        public static IEnumerator PlaySoundOfType(SoundGroup soundGroup, AudioSource audioSource, SoundPlayOptionsEnum soundType)
        {
            if (!initialized)
                Initialize();

            return soundTypeDictionary[soundType].PlaySoundOption(audioSource, soundGroup);
        }
    }
}