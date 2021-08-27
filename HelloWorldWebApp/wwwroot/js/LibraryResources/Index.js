$(document).ready(() => {
    addBroadcastEventHandler('EntityAdded', 'LibraryResource', entityId => {
        window.location.reload()
    })

    addBroadcastEventHandler('EntityUpdated', 'LibraryResource', entityId => {
        window.location.reload()
    })

    addBroadcastEventHandler('EntityRemoved', 'LibraryResource', removeEntityFromList)
})

const removeEntityFromList = entityId => $(`tr[data-entity-id="${entityId}"]`).remove()