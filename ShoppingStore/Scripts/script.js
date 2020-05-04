function ShowImagePreview(file, previewImage) {
    if (file.files && file.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $(previewImage).attr('src', e.target.result)
        }
        reader.readAsDataURL(file.files[0]);
    }
}

function refresh(resetUrl, showViewTab) {
    $.ajax({
        type: 'GET',
        url: resetUrl,
        success: function (response) {
            $('#myTabContent').html(response);
        }
    });
}