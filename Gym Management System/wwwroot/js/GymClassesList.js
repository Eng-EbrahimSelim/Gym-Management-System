$("#TrainerSelect").change(function () {
    let value = $(this).val();
    console.log("Selected value:", value);

    $.ajax({
        url: `/GymClass/GetClassesByTrainer`, data: { trainerId: value }, success: function (result) {
            console.log(result);
            if (result == null)
            {
                result="there is no gym classes "
            }
            $("#gymClassesContainer").html(result);
        }
    });
});