namespace _Game.Scripts
{
    public static class GameLayers
    {
        public const int
            PLAYER_LAYER = 6,
            ROOM_LAYER = 7,
            ROOM_ITEM_LAYER = 8,
            CHARACTER_LAYER = 9,
            VEHICLE_LAYER = 10,
            DOORS_LAYER = 11;


        public const int
            PLAYER_MASK = 1 << PLAYER_LAYER,
            ROOM_MASK = 1 << ROOM_LAYER,
            ROOM_ITEM_MASK = 1 << ROOM_ITEM_LAYER,
            CHARACTER_MASK = 1 << CHARACTER_LAYER,
            VEHICLE_MASK = 1 << VEHICLE_LAYER,
            DOORS_MASK = 1 << DOORS_LAYER;
    }
}