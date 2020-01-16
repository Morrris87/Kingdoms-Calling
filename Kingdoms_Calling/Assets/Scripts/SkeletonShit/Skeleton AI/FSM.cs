//  Name: FSM.cs
//  Author: ZAC KINDY
//  Function:
using UnityEngine;
using System.Collections;

namespace Complete
{
    public class FSM : MonoBehaviour
    {
     
        protected virtual void Initialize() { }
        protected virtual void FSMUpdate() { }
        protected virtual void FSMFixedUpdate() { }

        // Use this for initialization
        void Start()
        {
            Initialize();
        }

        // Update is called once per frame
        void Update()
        {
            FSMUpdate();
        }

        void FixedUpdate()
        {
            FSMFixedUpdate();
        }
    }
}
