$(document).ready(() => {
    addBroadcastEventHandler('EntityAdded', 'Skill', entityId => {
        window.location.reload()
    })

    addBroadcastEventHandler('EntityUpdated', 'Skill', entityId => {
        window.location.reload()
    })

    addBroadcastEventHandler('EntityRemoved', 'Skill', removeEntityFromList)
})

const removeEntityFromList = entityId => $(`tr[data-entity-id="${entityId}"]`).remove()