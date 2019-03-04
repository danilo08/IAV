using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace P1
{

    public class Tank : MonoBehaviour
    {
        //publico?
        
        private Position pos;
        public bool seleccionado =false;//private ,esta para verlo en el editor

        public void select() { seleccionado = true; }
        public void unSelect() { seleccionado = false; }
        public bool getMode() { Debug.Log(seleccionado); return seleccionado; }

        public void setPosition(Position p)
        {
            pos = p;

        }
        // Use this for initialization
        void Start()
        {
          // seleccionado = false;
        }

        // Update is called once per frame
        void Update()
        {
           /* if (seleccionado)
            {
            }
            else if (!seleccionado)
            {
            }*/
        }
    

        void OnMouseDown()
        {
            if (seleccionado) unSelect();
            else select();

        }


    }

}