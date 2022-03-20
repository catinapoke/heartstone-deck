using Core.Cards;
using UnityEngine;

namespace Core.Drag
{
    public class DropPanel : CardsContainerView
    {
        protected override void ArrangeCards()
        {
            float width = _cards.Count > 0 ? (_cards[0].transform as RectTransform).rect.width : 0;
            for (int index = 0; index < _cards.Count; index++)
            {
                _cards[index].Animator.MoveTo(GetCardPosition(width, index), Vector3.zero);
            }
        }

        private Vector3 GetCardPosition(float cardWidth, int index)
        {
            return transform.position + Vector3.right * cardWidth * (index - (_cards.Count - 1) * 0.5f );
        }
    }
}
