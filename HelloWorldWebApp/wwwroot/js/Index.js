const connection = new signalR.HubConnectionBuilder().withUrl("/hubs/teammember").build();

$(document).ready(() => {
    connection.on('AddedTeamMember', function (teamMember) {
        pushTeamMemberInTeamMemberList(teamMember)
    })

    connection.on('DeletedTeamMember', function (teamMemberId) {
        removeTeamMemberFromList(teamMemberId)
    })

    connection.on('UpdatedTeamMember', function (teamMember) {
        updateTeamMemberFromList(teamMember)
    })

    connection.start().then(function () {
        fetchAndReplaceTeamMembers()
    }).catch(function (err) {
        console.log(err)
    })

    $('#addTeamMemberButton').click(onAddTeamMemberButton)

    $('#addTeamMemberButton').attr('disabled', 'true')
    $('#clearButton').click(onClearButton)

    $('#teamMemberInput').keydown(onInputChange)
    $('#teamMemberInput').change(onInputChange)

    $('#editSubmit').click(onEditModalSubmit)
})

function onClearButton() {
    $('#teamMemberInput').val('').change()
}

function onInputChange() {
    if ($('#teamMemberInput').val().length === 0) {
        $('#addTeamMemberButton').attr('disabled', 'true')
    } else {
        $('#addTeamMemberButton').removeAttr('disabled')
    }
}

function onEditModalSubmit() {
    const newName = $('#classmateName').val()
    const id = $('#editClassmate').attr('data-member-id')

    if (newName === '' || newName === null) {
        return
    }

    putTeamMemberToServer(parseInt(id), newName)
        .catch(err => {
            console.log(err)
        })
}

function onEditButton() {
    const targetMemberTag = $(this).parent()
    const id = targetMemberTag.attr('data-member-id')
    const currentName = targetMemberTag.find("[data-name]").text()
    const modalTag = $('#editClassmate')

    $('#classmateName').val(currentName)
    modalTag.attr("data-member-id", id)
    modalTag.modal('show')
}

function onDeleteButton() {
    deleteTeamMemberFromServer(parseInt($(this).parent().attr('data-member-id')))
        .catch(err => {
            console.log(err)
        })
}

const onAddTeamMemberButton = () => {
    const teamMemberInput = $('#teamMemberInput')
    const teamMember = teamMemberInput.val()

    if (teamMember === '') {
        return
    }

    teamMemberInput.val('').change()

    postTeamMemberToServer(teamMember)
        .catch(err => {
            console.log(err)
        })
}

const removeTeamMemberFromList = (teamMemberId) => $(`li[data-member-id=${teamMemberId}]`).remove()

const updateTeamMemberFromList = (teamMember) => {
    const memberItem = $(`li[data-member-id=${teamMember.id}]`)
    memberItem.children('[data-name]').text(teamMember.name)
}

const replaceContentsOfListWithTeamMembers = (teamMembers) => {
    const teamMemberList = $('#teamMemberList')

    teamMemberList.html('')

    const actions = loggedIn ? `
        <button data-edit><i class="fa fa-pencil"></i></button>
        <button data-delete><i class="fa fa-trash"></i></button>
    ` : ''

    teamMembers.forEach(teamMember => {
        teamMemberList.append(`
            <li data-member-id="${teamMember.id}">
                <span data-name>${teamMember.name}</span>
                ${actions}
            </li>
        `)
    })

    $('button[data-delete]').click(onDeleteButton)
    $('button[data-edit]').click(onEditButton)
}

const pushTeamMemberInTeamMemberList = (teamMember) => {
    const teamMemberList = $('#teamMemberList')

    const actions = loggedIn ? `
        <button data-edit><i class="fa fa-pencil"></i></button>
        <button data-delete><i class="fa fa-trash"></i></button>
    ` : ''

    teamMemberList.append(`
        <li data-member-id="${teamMember.id}">
            <span data-name>${teamMember.name}</span>
            ${actions}
        </li>
    `)

    $(`[data-member-id=${teamMember.id}] button[data-delete]`).click(onDeleteButton)
    $(`[data-member-id=${teamMember.id}] button[data-edit]`).click(onEditButton)
}

const fetchAndReplaceTeamMembers = () => {
    connection.invoke("GetTeamMembers")
        .then((result) => {
            replaceContentsOfListWithTeamMembers(result)
        })
        .catch((err) => {
            console.log(err)
        })
}

const postTeamMemberToServer = (teamMember) =>  connection.invoke("AddTeamMember", teamMember)

const deleteTeamMemberFromServer = (teamMemberId) => connection.invoke("DeleteTeamMember", teamMemberId)

const putTeamMemberToServer = (teamMemberId, newName) => connection.invoke("UpdateTeamMember", { id: teamMemberId, name: newName })