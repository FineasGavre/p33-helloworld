const broadcastConnection = new signalR.HubConnectionBuilder().withUrl("/hubs/notifications").build()

const eventHandlerStorage = {
    'EntityAdded': [],
    'EntityRemoved': [],
    'EntityUpdated': []
}

$(document).ready(() => {
    broadcastConnection.start()
        .then(() => {
           console.log("Connected to ClientNotificationsHub.")
        })
        .catch(err => {
            console.log(err)
        })
})

Object.keys(eventHandlerStorage).forEach(key => {
    broadcastConnection.on(key, (entityType, entityId) => {
        eventHandlerStorage[key].forEach(eventHandlerEntry => {
            if (eventHandlerEntry.entityType === entityType) {
                eventHandlerEntry.eventHandler(entityId)
            }
        })
    })
})

function addBroadcastEventHandler(eventType, entityType, eventHandler) {
    if (Object.keys(eventHandlerStorage).includes(eventType)) {
        eventHandlerStorage[eventType].push({
            entityType,
            eventHandler
        })
    }
}


