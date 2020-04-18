public enum RoomOpeningTypes
{
    L_OPENING,
    R_OPENING,
    U_OPENING,
    D_OPENING,

    //2 OPENINGS
    L_R_OPENING,
    L_U_OPENING,
    L_D_OPENING,

    R_U_OPENING,
    R_D_OPENING,

    U_D_OPENING,

    //3 OPENINGS
    L_R_U_OPENING,
    L_R_D_OPENING,
    U_D_R_OPENING,
    U_D_L_OPENING,

    //4 room openings
    L_R_U_D_OPENING,
    NO_OPENING,
}

public enum RoomTypes
{
    START_ROOM,
    NORMAL_ROOM, //just normal mobs and stuff etc.
    BOSS_ROOM
}