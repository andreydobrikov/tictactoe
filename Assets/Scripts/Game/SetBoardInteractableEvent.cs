using com.tictactoe.dispatcher;

namespace com.tictactoe.game
{
    public class SetBoardInteractableEventModel : IEventModel
    {
        public bool interactable;

        public SetBoardInteractableEventModel(bool isInteractable)
        {
			interactable = isInteractable;
        }
    }

    public class SetBoardInteractableEvent : Event<SetBoardInteractableEventModel>
    {
    }
}
