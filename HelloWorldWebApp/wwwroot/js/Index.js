$(document).ready(() => {
    fetchAndReplaceTeamMembers()

    $('#addTeamMemberButton').click(onAddTeamMemberButton)
})

const onAddTeamMemberButton = () => {
    const teamMemberInput = $('#teamMemberInput')
    const teamMember = teamMemberInput.val()

    teamMemberInput.val('')

    postTeamMemberToServer(teamMember)
}

const replaceContentsOfListWithTeamMembers = (teamMembers) => {
    const teamMemberList = $('#teamMemberList')

    teamMemberList.html('')

    teamMembers.forEach(teamMember => {
        teamMemberList.append(`<li>${teamMember}</li>`)
    })
}

const appendTeamMemberToList = (teamMember) => {
    $('#teamMemberList').append(`<li>${teamMember}</li>`)
}

const fetchAndReplaceTeamMembers = () => {
    fetchTeamMembers()
        .then(data => {
            replaceContentsOfListWithTeamMembers(data.teamMembers)
        })
        .catch(err => {
            console.log(err)
        })
}

const postTeamMemberToServer = (teamMember) => {
    addTeamMember(teamMember)
        .then(() => {
            console.log('Added successfully.')
        })
        .catch((err) => {
            console.log(err)
            fetchAndReplaceTeamMembers()
        })

    appendTeamMemberToList(teamMember)
}

const fetchTeamMembers = () => $.get('/Home/GetTeamMembers')

const addTeamMember = (teamMember) => $.post('/Home/AddTeamMember', { TeamMemberName: teamMember })