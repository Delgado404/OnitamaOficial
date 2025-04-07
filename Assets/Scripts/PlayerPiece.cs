using System.Collections;
using UnityEngine;

public class PlayerPiece : MonoBehaviour
{
    
    public enum UnitType { pawn, king }; //pe�as do jogo 
    public enum Player { p1, p2 }; //jogadores

    public GameObject unitObj;
    public int unitId;
    public BoardTile node;
    public UnitType usType;
    public Player ptype;

    public void MoveToPosition(Vector3 pos)
    {
        StartCoroutine(MoveAnimation(pos));
    }

    //movimenta��o pe�a
    private IEnumerator MoveAnimation(Vector3 pos)
    {
        //    AudioManager.PlayClip("startMove"); // TODO: #Audio para movimenta��o inicial, verificar se colocar
        yield return StartCoroutine(MoveUtils.SmoothLerp(0.2f, unitObj.transform.position, pos + new Vector3(0, 0.5f, 0), unitObj));
        yield return StartCoroutine(MoveUtils.SmoothLerp(0.2f, unitObj.transform.position, pos, unitObj));
        //    AudioManager.PlayClip("endMove"); // TODO: #Audio para movimenta��o final, verificar se colocar
    }

    //mover a pe�a para um quadradinho do tabuleiro, n� especifico
    public void MoveToGridNode(BoardTile targetNode)
    {
        if (targetNode.occupyingUnit != null) return;

        targetNode.occupyingUnit = this;
        node.occupyingUnit = null;
        node = targetNode;
        MoveToPosition(targetNode.position);
    }

    public void Destroy()
    {
        Destroy(unitObj);
        Destroy(this);
    }
}
