const connection = new signalR.HubConnectionBuilder().withUrl("/hubs/roles").build()
let applicationRoles = []
let userRoles = []

$(document).ready(() => {
    connection.start().then(() => {
        loadRoles()
        addClickHandlers()
    }).catch(logAndDisplayError)

    connection.on('DisplayError', displayErrorToast)
    connection.on('DisplayWarning', displayWarningToast)

    connection.on('UserRoleAdded', () => loadRoles())
    connection.on('UserRoleRemoved', () => loadRoles())
})

const addClickHandlers = () => {
    $('#assignedRolesList').on('click', '[data-role-action="unassign"]', function () {
        $(this).prop('disabled', true)
        const roleId = $(this).parent().attr('data-role-id')

        unassignRole(roleId, userId)
            .then(() => $(this).prop('disabled', false))
            .catch(logAndDisplayError)
    })

    $('#unassignedRolesList').on('click', '[data-role-action="assign"]', function () {
        $(this).prop('disabled', true)
        const roleId = $(this).parent().attr('data-role-id')

        assignRole(roleId, userId)
            .then(() => $(this).prop('disabled', false))
            .catch(logAndDisplayError)
    })

    $('#invalidateSessionsButton').click(function () {
        $(this).prop('disabled', true)

        invalidateSessions(userId)
            .then(() => $(this).prop('disabled', false))
            .catch(logAndDisplayError)
    })
}

const loadRoles = () => {
    fetchRoles().then(fetchedRoles => {
        applicationRoles = fetchedRoles

        fetchUserRoles().then(fetchedUserRoles => {
            userRoles = fetchedUserRoles

            displayRoles()
        }).catch(logAndDisplayError)
    }).catch(logAndDisplayError)
}

const displayRoles = () => {
    const userRolesIds = userRoles.map(role => role.id)
    const unassignedRoles = applicationRoles.filter(role => !userRolesIds.includes(role.id))

    const assignedRolesElem = $('#assignedRolesList')
    const unassignedRolesElem = $('#unassignedRolesList')

    assignedRolesElem.html('')
    unassignedRolesElem.html('')

    userRoles.forEach(role => {
        const roleTitle = `
            <dt class="col-sm-2" data-role-id="${role.id}">
                ${role.name}
		    </dt>
        `

        const roleAction = `
            <dd class="col-sm-10" data-role-id="${role.id}">
                <button data-role-action="unassign">Unassign</button>
		    </dd>
        `

        assignedRolesElem.append(roleTitle + (canEditRoles ? roleAction : ''))
    })

    unassignedRoles.forEach(role => {
        const roleTitle = `
            <dt class="col-sm-2" data-role-id="${role.id}">
                ${role.name}
		    </dt>
        `

        const roleAction = `
            <dd class="col-sm-10" data-role-id="${role.id}">
                <button data-role-action="assign">Assign</button>
		    </dd>
        `

        unassignedRolesElem.append(roleTitle + (canEditRoles ? roleAction : ''))
    })
}

const fetchRoles = () => connection.invoke('GetAllRoles')
const fetchUserRoles = () => connection.invoke('GetUserRoles', userId)
const assignRole = (roleId, userId) => connection.invoke('AssignRoleToUser', roleId, userId)
const unassignRole = (roleId, userId) => connection.invoke('UnassignRoleFromUser', roleId, userId)
const invalidateSessions = userId => connection.invoke('InvalidateUserSessions', userId)

const logAndDisplayError = err => {
    console.log(err)
    displayErrorToast(err.message)
}

const displayErrorToast = message => {
    $('#errorToast .toast-body').text(message)
    $('#errorToast').toast('show')
}

const displayWarningToast = message => {
    $('#warningToast .toast-body').text(message)
    $('#warningToast').toast('show')
}