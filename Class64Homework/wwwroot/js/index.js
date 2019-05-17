$(() => {
    const connection = new signalR.HubConnectionBuilder()
        .withUrl("/jobHub").build();

    connection.start();

    $("#add-job").on('click', () => {
        const title = $("#job-title").val();
        connection.invoke("NewJob", { title });
        $("#job-title").val('');
    });

    connection.on("NewJob", function (job) {
        $("#job-table").append
            (`<tr>
                <td>
                    ${job.title}
                </td>
                <td>
                    <button style="display:"
                            id="job-open-${job.id}"
                            class="btn btn-success job-open" 
                            data-id="${job.id}">Do this job
                    </button>
                    <button id="job-in-use-by-me-${job.id}"
                            style="display:none"
                            class="btn btn-info job-in-use-by-me" 
                            data-id="${job.id}">Job done!
                    </button>
                    <button id="job-in-use-${job.id}"
                            style="display:none"
                            disabled ="disabled"
                            class="btn btn-warning job-in-use"
                            data-id="${job.id}" >
                    </button>
                    <label  id="job-done-${job.id}"
                            style="display:none"
                            class="label label-default job-done"
                            data-id="${job.id}" >
                    </label>
                </td>
            </tr>`)
    });

    $("#job-table").on('click', '.job-open', function () {
        const id = $(this).data('id');
        const jobStatus = 1;
        connection.invoke("UpdateJob", { id, jobStatus });
    });

    connection.on("JobInUse", function (job) {
        const userId = $("#user-id").val();
        if (job.user.id == userId) {
            $(`#job-in-use-by-me-${job.id}`).show();
        } else {
            $(`#job-in-use-${job.id}`).show();
            $(`#job-in-use-${job.id}`).text(`${job.user.firstName} ${job.user.lastName} is working on this`);
        }
        $(`#job-open-${job.id}`).hide();
    });

    $("#job-table").on('click', '.job-in-use-by-me', function () {
        const id = $(this).data('id');
        const jobStatus = 2;
        connection.invoke("UpdateJob", { id, jobStatus });
    });

    connection.on("JobDone", function (job) {
        $(`#job-in-use-by-me-${job.id}`).hide();
        $(`#job-done-${job.id}`).show();
        $(`#job-done-${job.id}`).text(`This has been completed by ${job.user.firstName} ${job.user.lastName}`);
        $(`#job-open-${job.id}`).hide();
        $(`#job-in-use-${job.id}`).hide();
    });
});