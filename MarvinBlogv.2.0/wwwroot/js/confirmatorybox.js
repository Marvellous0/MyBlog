
function JSalert() {

    // A confirm dialog

    alertify.confirm("Are you sure, you want to log Out?", function (e) {

        if (e) {

            <a class="btn btn-primary" asp-controller="User" asp-action="Logout">Yes</a>;

        } else {

             <a class="btn btn-primary" asp-controller="Blogger" asp-action="Index">No</a>;

        }

    });

}