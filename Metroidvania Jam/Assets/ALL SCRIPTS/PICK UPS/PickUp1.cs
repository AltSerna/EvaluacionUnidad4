using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp1 : MonoBehaviour,IPickUps // O (por la interfaz cerrada a cambios pero abierta a cambios) y D (depende de una interfaz y depende de un clase inventory),
                                              // L (entre el inventory y el pickup se permite expandir sin generar daños a la base de la estructura)
{
    Inventory invScript;
    public GameObject Player;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PickingUp();
            SoundFx();
        }
    }

    public void PickingUp() // nuevo Cashing implementado...
    {
        gameObject.SetActive(false);
        Inventory.Instance.GetMoney();
        Inventory.Instance.MoneyDisplay();
    }

    public void SoundFx()
    {
       //codigo para añadir sonido 
    }
}

