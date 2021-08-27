$(document).ready(() => {
    addBroadcastEventHandler('EntityAdded', 'Intern', entityId => {
        window.location.reload()
    })

    addBroadcastEventHandler('EntityUpdated', 'Intern', entityId => {
        window.location.reload()
    })

    addBroadcastEventHandler('EntityRemoved', 'Intern', removeEntityFromList)
})

const removeEntityFromList = entityId => $(`tr[data-entity-id="${entityId}"]`).remove()

const fetchInternById = id => $.ajax({
    method: 'GET',
    url: '/Interns/DetailsJson/' + id
})