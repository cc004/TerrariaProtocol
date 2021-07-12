using System;

// Token: 0x0200000E RID: 14
public enum PacketTypes
{
    // Token: 0x04000028 RID: 40
    ConnectRequest = 1,
    // Token: 0x04000029 RID: 41
    Disconnect,
    // Token: 0x0400002A RID: 42
    ContinueConnecting,
    // Token: 0x0400002B RID: 43
    PlayerInfo,
    // Token: 0x0400002C RID: 44
    PlayerSlot,
    // Token: 0x0400002D RID: 45
    ContinueConnecting2,
    // Token: 0x0400002E RID: 46
    WorldInfo,
    // Token: 0x0400002F RID: 47
    TileGetSection,
    // Token: 0x04000030 RID: 48
    Status,
    // Token: 0x04000031 RID: 49
    TileSendSection,
    // Token: 0x04000032 RID: 50
    TileFrameSection,
    // Token: 0x04000033 RID: 51
    PlayerSpawn,
    // Token: 0x04000034 RID: 52
    PlayerUpdate,
    // Token: 0x04000035 RID: 53
    PlayerActive,
    // Token: 0x04000036 RID: 54
    PlayerHp = 16,
    // Token: 0x04000037 RID: 55
    Tile,
    // Token: 0x04000038 RID: 56
    TimeSet,
    // Token: 0x04000039 RID: 57
    DoorUse,
    // Token: 0x0400003A RID: 58
    TileSendSquare,
    // Token: 0x0400003B RID: 59
    ItemDrop,
    // Token: 0x0400003C RID: 60
    ItemOwner,
    // Token: 0x0400003D RID: 61
    NpcUpdate,
    // Token: 0x0400003E RID: 62
    NpcItemStrike,
    // Token: 0x0400003F RID: 63
    ProjectileNew = 27,
    // Token: 0x04000040 RID: 64
    NpcStrike,
    // Token: 0x04000041 RID: 65
    ProjectileDestroy,
    // Token: 0x04000042 RID: 66
    TogglePvp,
    // Token: 0x04000043 RID: 67
    ChestGetContents,
    // Token: 0x04000044 RID: 68
    ChestItem,
    // Token: 0x04000045 RID: 69
    ChestOpen,
    // Token: 0x04000046 RID: 70
    PlaceChest,
    // Token: 0x04000047 RID: 71
    EffectHeal,
    // Token: 0x04000048 RID: 72
    Zones,
    // Token: 0x04000049 RID: 73
    PasswordRequired,
    // Token: 0x0400004A RID: 74
    PasswordSend,
    // Token: 0x0400004B RID: 75
    RemoveItemOwner,
    // Token: 0x0400004C RID: 76
    NpcTalk,
    // Token: 0x0400004D RID: 77
    PlayerAnimation,
    // Token: 0x0400004E RID: 78
    PlayerMana,
    // Token: 0x0400004F RID: 79
    EffectMana,
    // Token: 0x04000050 RID: 80
    PlayerTeam = 45,
    // Token: 0x04000051 RID: 81
    SignRead,
    // Token: 0x04000052 RID: 82
    SignNew,
    // Token: 0x04000053 RID: 83
    LiquidSet,
    // Token: 0x04000054 RID: 84
    PlayerSpawnSelf,
    // Token: 0x04000055 RID: 85
    PlayerBuff,
    // Token: 0x04000056 RID: 86
    NpcSpecial,
    // Token: 0x04000057 RID: 87
    ChestUnlock,
    // Token: 0x04000058 RID: 88
    NpcAddBuff,
    // Token: 0x04000059 RID: 89
    NpcUpdateBuff,
    // Token: 0x0400005A RID: 90
    PlayerAddBuff,
    // Token: 0x0400005B RID: 91
    UpdateNPCName,
    // Token: 0x0400005C RID: 92
    UpdateGoodEvil,
    // Token: 0x0400005D RID: 93
    PlayHarp,
    // Token: 0x0400005E RID: 94
    HitSwitch,
    // Token: 0x0400005F RID: 95
    UpdateNPCHome,
    // Token: 0x04000060 RID: 96
    SpawnBossorInvasion,
    // Token: 0x04000061 RID: 97
    PlayerDodge,
    // Token: 0x04000062 RID: 98
    PaintTile,
    // Token: 0x04000063 RID: 99
    PaintWall,
    // Token: 0x04000064 RID: 100
    Teleport,
    // Token: 0x04000065 RID: x101
    PlayerHealOther,
    // Token: 0x04000066 RID: 102
    Placeholder,
    // Token: 0x04000067 RID: 103
    ClientUUID,
    // Token: 0x04000068 RID: 104
    ChestName,
    // Token: 0x04000069 RID: 105
    CatchNPC,
    // Token: 0x0400006A RID: 106
    ReleaseNPC,
    // Token: 0x0400006B RID: 107
    TravellingMerchantInventory,
    // Token: 0x0400006C RID: 108
    TeleportationPotion,
    // Token: 0x0400006D RID: 109
    AnglerQuest,
    // Token: 0x0400006E RID: 110
    CompleteAnglerQuest,
    // Token: 0x0400006F RID: 111
    NumberOfAnglerQuestsCompleted,
    // Token: 0x04000070 RID: 112
    CreateTemporaryAnimation,
    // Token: 0x04000071 RID: 113
    ReportInvasionProgress,
    // Token: 0x04000072 RID: 114
    PlaceObject,
    // Token: 0x04000073 RID: 115
    SyncPlayerChestIndex,
    // Token: 0x04000074 RID: 116
    CreateCombatText,
    // Token: 0x04000075 RID: 117
    LoadNetModule,
    // Token: 0x04000076 RID: 118
    NpcKillCount,
    // Token: 0x04000077 RID: 119
    PlayerStealth,
    // Token: 0x04000078 RID: 120
    ForceItemIntoNearestChest,
    // Token: 0x04000079 RID: 121
    UpdateTileEntity,
    // Token: 0x0400007A RID: 122
    PlaceTileEntity,
    // Token: 0x0400007B RID: 123
    TweakItem,
    // Token: 0x0400007C RID: 124
    PlaceItemFrame,
    // Token: 0x0400007D RID: 125
    UpdateItemDrop,
    // Token: 0x0400007E RID: 126
    EmoteBubble,
    // Token: 0x0400007F RID: 127
    SyncExtraValue,
    // Token: 0x04000080 RID: 128
    SocialHandshake,
    // Token: 0x04000081 RID: 129
    Deprecated,
    // Token: 0x04000082 RID: 130
    KillPortal,
    // Token: 0x04000083 RID: 131
    PlayerTeleportPortal,
    // Token: 0x04000084 RID: 132
    NotifyPlayerNpcKilled,
    // Token: 0x04000085 RID: 133
    NotifyPlayerOfEvent,
    // Token: 0x04000086 RID: 134
    UpdateMinionTarget,
    // Token: 0x04000087 RID: 135
    NpcTeleportPortal,
    // Token: 0x04000088 RID: 136
    UpdateShieldStrengths,
    // Token: 0x04000089 RID: 137
    NebulaLevelUp,
    // Token: 0x0400008A RID: 138
    MoonLordCountdown,
    // Token: 0x0400008B RID: 139
    NpcShopItem,
    // Token: 0x0400008C RID: 140
    GemLockToggle,
    // Token: 0x0400008D RID: 141
    PoofOfSmoke,
    // Token: 0x0400008E RID: 142
    SmartTextMessage,
    // Token: 0x0400008F RID: 143
    WiredCannonShot,
    // Token: 0x04000090 RID: 144
    MassWireOperation,
    // Token: 0x04000091 RID: 145
    MassWireOperationPay,
    // Token: 0x04000092 RID: 146
    ToggleParty,
    // Token: 0x04000093 RID: 147
    TreeGrowFX,
    // Token: 0x04000094 RID: 148
    CrystalInvasionStart,
    // Token: 0x04000095 RID: 149
    CrystalInvasionWipeAll,
    // Token: 0x04000096 RID: 150
    MinionAttackTargetUpdate,
    // Token: 0x04000097 RID: 151
    CrystalInvasionSendWaitTime,
    // Token: 0x04000098 RID: 152
    PlayerHurtV2,
    // Token: 0x04000099 RID: 153
    PlayerDeathV2,
    // Token: 0x0400009A RID: 154
    CreateCombatTextExtended,
    SyncMods = 251,
}
