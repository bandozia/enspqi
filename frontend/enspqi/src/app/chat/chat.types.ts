interface ChatConn { id: string, url: string }

interface Room {id: string, name: string, isParticipant: boolean }

enum HubCallbacks {
    GeneralReceived = 'ReceiveGeneral',
    NewRoomCreated = 'NewRoomCreated',
    UserJoinedToRoom = 'UserJoinedToRoom',
    ReceiveInRoom = 'ReceiveInRoom'
}

export { ChatConn, HubCallbacks, Room }