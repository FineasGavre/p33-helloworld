$(document).ready(() => {
    fetchAndReplaceTeamMembers()

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

    putTeamMember(id, newName)
        .then(() => {
            $(`[data-member-id=${id}] [data-name]`).text(newName)
        })
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
    deleteTeamMember($(this).parent().attr('data-member-id'))
        .then(() => {
            $(this).parent().remove()
        })
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
}

const replaceContentsOfListWithTeamMembers = (teamMembers) => {
    const teamMemberList = $('#teamMemberList')

    teamMemberList.html('')

    teamMembers.forEach(teamMember => {
        teamMemberList.append(`
            <li data-member-id="${teamMember.id}">
                <span data-name>${teamMember.name}</span>
                <button data-edit><i class="fa fa-pencil"></i></button>
                <button data-delete><i class="fa fa-trash"></i></button>
            </li>
        `)
    })

    $('button[data-delete]').click(onDeleteButton)
    $('button[data-edit]').click(onEditButton)
}

const fetchAndReplaceTeamMembers = () => {
    fetchTeamMembers()
        .then(data => {
            replaceContentsOfListWithTeamMembers(data)
        })
        .catch(err => {
            console.log(err)
        })
}

const postTeamMemberToServer = (teamMember) => {
    addTeamMember(teamMember)
        .then(() => {
            console.log('Added successfully.')
            fetchAndReplaceTeamMembers()
        })
        .catch((err) => {
            console.log(err)
            fetchAndReplaceTeamMembers()
        })
}

const fetchTeamMembers = () => $.get('/Home/GetTeamMembers')

const addTeamMember = (teamMember) => $.post('/Home/AddTeamMember', { TeamMemberName: teamMember })

const deleteTeamMember = (teamMemberId) => $.ajax({
    method: 'DELETE',
    url: '/Home/DeleteTeamMember',
    data: {
        id: teamMemberId
    }
})

const putTeamMember = (teamMemberId, newName) => $.ajax({
    method: 'PUT',
    url: '/Home/UpdateTeamMember',
    data: {
        id: teamMemberId,
        name: newName
    }
})