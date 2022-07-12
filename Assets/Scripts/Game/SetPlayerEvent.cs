using com.tictactoe.dispatcher;

namespace com.tictactoe.game
{
    public class SetPlayerEventModel : IEventModel
    {
        public PlayerData player;

        public SetPlayerEventModel(PlayerData p)
        {
			player = p;
        }
    }

    public class SetPlayerEvent : Event<SetPlayerEventModel>
    {
    }
}
