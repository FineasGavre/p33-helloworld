$(document).ready(() => {
    const teamNameField = $("#teamNameField")

    $("#addTeamButton").click(() => {
        const newTeamName = teamNameField.val()

        $("#teamList").append(`<li>${newTeamName}</li>`)

        teamNameField.val("")
    })
})