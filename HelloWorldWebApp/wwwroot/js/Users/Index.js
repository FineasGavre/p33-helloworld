const connection = new signalR.HubConnectionBuilder().withUrl("/hubs/roles").build()
let applicationRoles = []
let userRoles = []

$(document).ready(() => {
    connection.start().then(() => {
        loadRoles()
        addClickHandlers()
    }).catch(err => {
        console.log(err)
    })

    connection.on('DisplayError', errorMessage => {
        $('#errorToast .toast-body').text(errorMessage)
        $('#errorToast').toast('show')
    })

    connection.on('DisplayWarning', warningMessage => {
        $('#warningToast .toast-body').text(errorMessage)
        $('#warningToasts').toast('show')
    })

    connection.on('UserRoleAdded', () => loadRoles())
    connection.on('UserRoleRemoved', () => loadRoles())
})

const addClickHandlers = () => {
    const assignedRolesElem = $('#assignedRolesList')
    const unassignedRolesElem = $('#unassignedRolesList')

    assignedRolesElem.on('click', '[data-role-action="unassign"]', function () {
        $(this).prop('disabled', true)
        const roleId = $(this).parent().attr('data-role-id')

        unassignRole(roleId, userId)
            .then(() => $(this).prop('disabled', false))
            .catch(err => console.log(err))
    })

    unassignedRolesElem.on('click', '[data-role-action="assign"]', function () {
        $(this).prop('disabled', true)
        const roleId = $(this).parent().attr('data-role-id')

        assignRole(roleId, userId)
            .then(() => $(this).prop('disabled', false))
            .catch(err => console.log(err))
    })
}

const loadRoles = () => {
    fetchRoles().then(fetchedRoles => {
        applicationRoles = fetchedRoles

        fetchUserRoles().then(fetchedUserRoles => {
            userRoles = fetchedUserRoles

            displayRoles()
        })
    }).catch(err => console.log(err))
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