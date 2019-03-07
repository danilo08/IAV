using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace P1
{

    public class Tank : Block
    {
        //publico?    
        private Position pos;
        public bool seleccionado ;//private ,esta para verlo en el editor
        private int pasos;
        public void select() { seleccionado = true; }
        public void unSelect() { seleccionado = false; }
        public bool getMode() {  return seleccionado; }
       

        private Position objetive;

        public void swithcSelect()
        {
            seleccionado = !seleccionado;

            
        }
        public void setPosition(Position p)
        {
            pos = p;

        }
        // Use this for initialization
        void Start()
        {
           
        }

        // Update is called once per frame
        void Update()
        {
            if (seleccionado)
            {
                //objetive = board.getMyMeta().position;

            }
            else if (!seleccionado)
            {
            }
        }
    

        public void move(Position nextPos)
        {
           // transform.position += objetive.normalized * 2 * Time.deltaTime;
        }

        void OnMouseDown()
        {
            swithcSelect();
        }


    }

}