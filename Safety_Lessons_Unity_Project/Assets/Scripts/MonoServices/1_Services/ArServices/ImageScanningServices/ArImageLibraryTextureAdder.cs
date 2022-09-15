using MonoServices.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace MonoServices.AR
{
    public class ArImageLibraryTextureAdder : MonoService
    {
        ARTrackedImageManager _arTrackedImageManager;
        readonly List<Texture2D> _textures = new List<Texture2D>();

        int _currTextureIndex;

        IEnumerator _AddingImageToImageLibraryCorotine;

        void GetArTrackedImageMangerCommand(ARTrackedImageManager arTrackedImageManger)
        {
            _arTrackedImageManager = arTrackedImageManger;

            InvokeCommand(0);
        }


        void AddSingleTextureCommand(Texture2D texture)
        {
            if (!_textures.Contains(texture))
                _textures.Add(texture);
        }

        void AddMultiTexturesCommand(Texture2D[] textures)
        {
            _textures.AddRange(textures);
        }

        void AddImageToImageLibraryCommand()
        {
            if (!_arTrackedImageManager)
            {
                Debug.Log("arTrackedImageManger is missing please listen to an event that passes the arTrackedImageManger.", this);
                return;
            }

            if (_AddingImageToImageLibraryCorotine == null)
            {
                _currTextureIndex = 0;
                StartCoroutine(_AddingImageToImageLibraryCorotine = AddingImageToImageLibrary());
            }
        }

        IEnumerator AddingImageToImageLibrary()
        {
            MutableRuntimeReferenceImageLibrary mutableRuntimeReferenceImageLibrary = _arTrackedImageManager.referenceLibrary as MutableRuntimeReferenceImageLibrary;

            var jobHandle = mutableRuntimeReferenceImageLibrary.ScheduleAddImageWithValidationJob(_textures[_currTextureIndex], Guid.NewGuid().ToString(), 0.03f);

            while (!jobHandle.status.IsComplete())
                yield return null;

            if (!jobHandle.status.IsSuccess())
            {
                StartCoroutine(AddingImageToImageLibrary());
                yield break;
            }

            Debug.Log("added " + _textures[_currTextureIndex]);

            _currTextureIndex++;


            Debug.Log($"referenceLibraryCount {_arTrackedImageManager.referenceLibrary.count}");

            if (_currTextureIndex < _textures.Count)
                StartCoroutine(AddingImageToImageLibrary());
            else
                AddedAllTexturesCommand();


        }

        void AddedAllTexturesCommand()
        {
            _AddingImageToImageLibraryCorotine = null;
            InvokeCommand(4, _arTrackedImageManager);
        }

        void RemoveTextureCommand(Texture2D texture)
        {
            if (!_textures.Contains(texture))
                return;

            _textures.Remove(texture);
        }

        void ClearRefrenceImageLibraryCommand()
        {
            if (!_arTrackedImageManager)
            {
                Debug.Log("arTrackedImageManger is missing please listen to an event that passes the arTrackedImageManger.");
                return;
            }

            _arTrackedImageManager.referenceLibrary = _arTrackedImageManager.CreateRuntimeLibrary();

            _currTextureIndex = 0;

        }

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            if (methodNumb == 0) GetArTrackedImageMangerCommand((ARTrackedImageManager)passedObj);
            if (methodNumb == 1) AddSingleTextureCommand((Texture2D)passedObj);
            if (methodNumb == 2) AddMultiTexturesCommand((Texture2D[])passedObj);
            if (methodNumb == 3) AddImageToImageLibraryCommand();
            if (methodNumb == 5) RemoveTextureCommand((Texture2D)passedObj);
            if (methodNumb == 6) ClearRefrenceImageLibraryCommand();
        }
    }
}