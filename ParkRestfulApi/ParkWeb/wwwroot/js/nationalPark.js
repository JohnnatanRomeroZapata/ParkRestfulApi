
$(document).ready(() => {
    $("#tableData").DataTable();
    CreateUpdate();
});

function Delete(nationalParkId) {
    Swal.fire({
        title: "Are you sure you want to Delete?",
        text: "You will not be able to restore the detail",
        icon: "warning",
        showCancelButton: true,
        confirmButtonText: "Yes, delete it"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: "/NationalPark/DeleteNationalPark/",
                data: { id: nationalParkId },
                type: "DELETE"
            }).done((data) => {
                if (data.success) {
                    toastr.success(data.message);
                } else {
                    toastr.error(data.message);
                }
            });
        }
    });
};

function CreateUpdate() {

    var message = $("#toastrMessagge").text();

    if (message === "National Park was not created") {
        toastr.error(message);
    }else if (message === "National Park was not updated") {
        toastr.error(message);
    }else if (message === "National Park was created") {
        toastr.success(message);
    }else if (message === "National Park was updated") {
        toastr.info(message);
    }
};