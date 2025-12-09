using IPA;
using System;
using System.Collections;
using System.Security.Policy;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using BS_Utils;
using RealTimeClanker;
//using SiraUtil.Submissions;


namespace RealTimeClanker
{
    public class RTController : MonoBehaviour
    {
        public static RTController Instance { get; private set; }

        private int posRange = 20;

        private int rotRange = 90;

        private float scaleRange = 20;

        private float gravityRange = 1000;

        private int chances = 5;

        private RandomizationHelper _randHelper = new RandomizationHelper();


        //private readonly Submission _submissions;

        // messes with transforms and colors
        private void FuckUpGOs()
        {
            // before big loop
            Plugin.Log.Info("Just got called into work today :3");
            Physics.gravity = _randHelper.randomVector3(gravityRange);


            // stolen from elkay
            int sceneCount = SceneManager.sceneCount;
            for (int i = 0; i < sceneCount; i++)
            {
                Scene sceneAt = SceneManager.GetSceneAt(i);
                Plugin.Log.Debug($"Shuffling up {sceneAt.name}...");
                GameObject[] rootGameObjects = sceneAt.GetRootGameObjects();

                // loop through game objects in scene
                foreach (GameObject val in rootGameObjects)
                {
                    Transform[] gos = val.GetComponentsInChildren<Transform>();
                    MeshRenderer[] meshRenderers = val.GetComponentsInChildren<MeshRenderer>();
                    SpriteRenderer[] spriteRenderers = val.GetComponentsInChildren<SpriteRenderer>();
                    TextMeshPro[] tmPros = val.GetComponentsInChildren<TextMeshPro>();
                    TextMeshProUGUI[] tmPros2 = val.GetComponentsInChildren<TextMeshProUGUI>();
                    TubeBloomPrePassLight[] tubeLights = val.GetComponentsInChildren<TubeBloomPrePassLight>();

                    // init
                    _randHelper.newSeed();

                    // disable objects
                    if (_randHelper.randomBool(chances))
                    {
                        val.gameObject.SetActive(false);
                    }

                    // loop through transforms
                    foreach (Transform go in gos)
                    {
                        if (_randHelper.randomBool(chances, 30))
                        {
                            go.Translate(_randHelper.randomFloat(posRange), _randHelper.randomFloat(posRange), _randHelper.randomFloat(posRange));
                            go.localScale = _randHelper.randomVector3(scaleRange);
                            go.Rotate(_randHelper.randomFloat(rotRange), _randHelper.randomFloat(rotRange), _randHelper.randomFloat(rotRange));
                        }

                    }

                    // loop through mesh renderers
                    foreach (MeshRenderer mr in meshRenderers)
                    {
                        if (_randHelper.randomBool(1))
                        {
                            Color clr = _randHelper.randomColor(posRange);


                            mr.material.color = clr;
                            mr.material.SetFloat("_DisplacementStrength", UnityEngine.Random.Range(0, 1024));
                            mr.material.SetFloat("_Smoothness", UnityEngine.Random.Range(0f, 1f));
                            mr.material.SetFloat("_BumpIntensity", UnityEngine.Random.Range(0f, 10f));
                            mr.material.SetFloat("_DirtIntensity", UnityEngine.Random.Range(0f, 10f));
                            mr.material.SetFloat("_FogScale", _randHelper.randomFloat(10f));
                        }
                    }

                    // im not commenting the rest of this shit
                    foreach (SpriteRenderer sprite in spriteRenderers)
                    {
                        if (_randHelper.randomBool(chances))
                        {
                            Color clr = _randHelper.randomColor(scaleRange);

                            sprite.material.color = clr;
                        }
                    }

                    foreach (TextMeshProUGUI tm in tmPros2)
                    {
                        if (_randHelper.randomBool(chances))
                        {
                            Color clr = _randHelper.randomColor(scaleRange);

                            tm.color = clr;
                            tm.fontSize = UnityEngine.Random.Range(1, 15);
                        }
                    }

                    foreach (TextMeshPro tm in tmPros)
                    {
                        if (_randHelper.randomBool(chances))
                        {
                            Color clr = _randHelper.randomColor(scaleRange);

                            tm.color = clr;
                            tm.fontSize = UnityEngine.Random.Range(1, 15);
                        }
                    }

                    foreach (TubeBloomPrePassLight tubes in tubeLights)
                    {
                        if (_randHelper.randomBool(chances))
                        {
                            Color clr = _randHelper.randomColor(scaleRange);

                            tubes.color = clr;
                            tubes.width = _randHelper.randomFloat(scaleRange);
                            tubes.length = _randHelper.randomFloat(scaleRange);
                            tubes.bloomFogIntensityMultiplier = UnityEngine.Random.Range(1, 100);
                        }
                    }
                }

                // disable score submission 
                // TODO: Change to SiraUtil
                //_submissions?.DisableScoreSubmission("RealTimeClanker", "GameObject Randomization");
                BS_Utils.Gameplay.ScoreSubmission.DisableSubmission("RealTimeClanker");
            }
        }


        // on enable
        [OnEnable]
        public void OnEnable()
        {
            SceneManager.activeSceneChanged += OnSceneChanged;
        }

        // on disable
        [OnDisable]
        public void OnDisable()
        {
            SceneManager.activeSceneChanged -= OnSceneChanged;
        }

        // called from scene manager
        private void OnSceneChanged(Scene oldScene, Scene newScene)
        {
            // this is a bit redundant to keep calling the song disabling thing but it doesnt work when its added with start soooo
            StartCoroutine(SleeperAgent()); // and then start this
        }

        public void Start()
        {
            Plugin.Log.Debug("I've arrived :3");
            DontDestroyOnLoad(this.gameObject);
            Plugin.Log.Warn("I also marked myself to not delete itself so I can do my work! :D");
        }

        // wait statement for thing
        IEnumerator SleeperAgent()
        {
            yield return new WaitForSeconds(0.1f);
            FuckUpGOs();
        }
    }
}
