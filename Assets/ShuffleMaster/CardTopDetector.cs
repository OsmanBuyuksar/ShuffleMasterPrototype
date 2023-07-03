using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardTopDetector : MonoBehaviour
{
    [SerializeField] private bool left;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Card card))
        {
            if (card.gameObject.GetComponent<Rigidbody>() != null)
                Destroy(card.gameObject.GetComponent<Rigidbody>());
            if(left != card.left)
            {
                CardController.Instance.InstantiateCard(left);
                Destroy(card.gameObject);
            }
        }
        if(other.TryGetComponent(out IGate gate))
        {
            int i = gate.ScaleValue();
            if(i > 0)
            {
                for (; i > 0; i--)
                    CardController.Instance.InstantiateCard(left);
            }
            else
            {
                for (; i < 0; i++)
                    CardController.Instance.DestroyCard(left);
            }
        }
        if(other.TryGetComponent(out I_ShuffleMasterGate gate2))
        {
            int count = CardController.Instance.GetCardCount(left);
            int i = gate2.ScaleValue(count);
            if (i > 0)
            {
                for (; i > 0; i--)
                    CardController.Instance.InstantiateCard(left);
            }
            else
            {
                for (; i < 0; i++)
                    CardController.Instance.DestroyCard(left);
            }
        }
        if(other.TryGetComponent(out FinishLine finish))
        {
            GameManager.Instance.GameWin();
        }
    }
}
