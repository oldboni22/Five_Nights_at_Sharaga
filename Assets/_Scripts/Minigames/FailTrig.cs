using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace minigames.jumper
{
    public class FailTrig : MonoBehaviour
    {
        [Inject] private IScreamerUiController _controller;
        [SerializeField] private string _id;

        public void SetId(string id)
        { _id = id; }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            Debug.Log(_controller);
            _controller.Jumpscare(_id,false);
            Destroy(collision.gameObject);
        }
    }
}