using com.tictactoe.dispatcher;

namespace com.tictactoe.game
{
    public class TileClickedEventModel : IEventModel
    {
        public TileRenderer tileRenderer;

        public TileClickedEventModel(TileRenderer tileRenderer)
        {
            this.tileRenderer = tileRenderer;
        }
    }

    public class TileClickedEvent : Event<TileClickedEventModel>
    {
    }
}
