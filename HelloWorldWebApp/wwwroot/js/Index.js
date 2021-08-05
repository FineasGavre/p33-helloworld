$(document).ready(() => {
    fetchAndReplaceTeamMembers()

    $('#addTeamMemberButton').click(onAddTeamMemberButton)

    $('#addTeamMemberButton').attr('disabled', 'true')
    $('#clearButton').click(onClearButton)

    $('#teamMemberInput').keydown(onInputChange)
    $('#teamMemberInput').change(onInputChange)
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

function onDeleteButton() {
    deleteTeamMember($(this).attr('data-delete'))
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
            <li>
                <span>${teamMember.name}</span>
                <button><i class="fa fa-pencil"></i></button>
                <button data-delete="${teamMember.id}"><i class="fa fa-trash"></i></button>
            </li>
        `)
    })

    $('button[data-delete]').click(onDeleteButton)
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