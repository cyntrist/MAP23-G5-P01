using UnityEngine;

public class DestroyBrick : MonoBehaviour //Hay que dar este component a la base del brick (BRICK BASE), child de Brick
{
    void OnCollisionEnter2D(Collision2D col)
    {
        //El unico tag importante es el de Player, he quitado el tag de Brick pk no es necesario
        if (col.gameObject.CompareTag("Player")) //cuando el player colisione con brick base
        {
            Destroy(transform.parent.gameObject); //destruye el gameObject Brick
        }
    }
}
