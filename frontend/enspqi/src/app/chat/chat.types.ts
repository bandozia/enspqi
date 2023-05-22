interface ChatConn { id: string, url: string }

enum HubCallbacks {
    GeneralReceived = 'ReceiveGeneral'
}

export { ChatConn, HubCallbacks }